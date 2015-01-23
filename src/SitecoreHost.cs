using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Sitecore;
using Sitecore.Configuration;
using Telligent.Evolution.Extensibility.OAuthClient.Version1;
using Telligent.Evolution.Extensibility.Rest.Version1;


namespace Zimbra.Social.RemotingSDK.Sitecore
{

    public class SitecoreHost : RestHost, IUserCreatableOAuthClientConfiguration,
        IUserSynchronizedOAuthClientConfiguration
    {
        private static readonly string LanguageContextKey = "Sitecore-User-Language";
        private static readonly string SiteContextKey = "sc-host";
        private bool _enableSync;
        private NetworkCredential _credentials;
        private bool _enableUserCreation;
        private bool _generateSitecoreUrls;
        private Guid _id;
        private string _callbackUrl;
        private string _communityStartItem;
        private string _connectorHandlerUrl = "";
        private string _cookieName;
        private string _defaultLanguageKey;
        private string _defaultReturnUrl = "";
        private string _defaultUserName;
        private string _evolutionRootUrl = "";
        private string _evolutionUserSynchronizationManagementUserName;
        private string _excludeHeaders;
        private string _loginUrl = "";
        private string _logoutUrl = "";
        private string _modalThemeUrl;
        private string _networkDomain;
        private string _networkPassword;
        private string _networkUsername;
        private string _oauthCallbackHandler;
        private string _oauthClientId;
        private string _oauthSecret;
        private string _siteName;
        private string _themeUrl;
        private string _userSyncCookieName = "evosync";
        private string _widgetPath;
        private string[] _includeHeaders;
        private bool _stripDomain = false;

        #region Constructor

        public SitecoreHost(string name)
        {
            Name = name;
            LoadConfiguration(Name);
        }

        #endregion

        #region Sitecore Host Specific Members

        public virtual string Name { get; private set; }

        public virtual string InitiateLogin(string returnUrl)
        {
            if (_enableUserCreation && _enableSync)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    throw new ArgumentException("returnUrl is required","returnUrl");

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("rtn", returnUrl);
                var evoUser = Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.GetAuthenticatedUser(Id, nvc, (x) => returnUrl = x.OriginalString);
            
            }
            return returnUrl;
           
        }

        public string InitiateLogout(string returnUrl)
        {

            if (_enableSync && _enableUserCreation)
            {
                if (string.IsNullOrEmpty(returnUrl))
                    throw new ArgumentException("returnUrl is required", "returnUrl");

                NameValueCollection nvc = new NameValueCollection();
                nvc.Add("rtn", returnUrl);
                var evoUri = Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.EvolutionLogOut(Id, nvc);
                if (evoUri != null)
                    returnUrl = evoUri.OriginalString;
            }
            Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.LogOut(Id);

            return returnUrl;
        }
        #endregion


        #region RemoteSctriptedContentFragmentHost Members

        public override void LogError(string message, Exception ex)
        {
            base.LogError(message, ex);
           // Sitecore.Diagnostics.Log.Error(message, ex);
        }

        public override void ApplyAuthenticationToHostRequest(System.Net.HttpWebRequest request, bool forAccessingUser)
        {
            Telligent.Evolution.Extensibility.OAuthClient.Version1.User user = null;

            if (forAccessingUser)
                user = GetAccessingUser();

            if (user == null)
                user = Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.GetDefaultUser(this.Id);

            if (user != null)
                request.Headers["Authorization"] = "OAuth " + user.OAuthToken;

            request.Credentials = EvolutionCredentials;
        }

     

        public override string EvolutionRootUrl
        {
            get { return _evolutionRootUrl.EndsWith("/") ? _evolutionRootUrl : _evolutionRootUrl + "/"; }
        }

       

      
      
        #endregion

        #region OAuth Client Options

        public virtual bool EnableEvolutionUserCreation
        {
            get { return _enableUserCreation; }
        }

        public virtual string EvolutionUserCreationManagementUserName
        {
            get { return _evolutionUserSynchronizationManagementUserName; }
        }

        public virtual Dictionary<string, string> LocalUserDetails
        {
            get
            {
                //You can use this to pass in profile fields if so desired
                return new Dictionary<string, string>();
            }
        }

        public virtual string LocalUserEmailAddress
        {
            get
            {
                if (Context.User == null || !Context.User.IsAuthenticated)
                    return null;

                if (string.IsNullOrEmpty(Context.User.Profile.Email))
                    throw new ApplicationException("Local user must have a valid unique address");

                return Context.User.Profile.Email;
            }
        }

        public virtual string LocalUserName
        {
            get
            {
                if (Context.User == null || !Context.User.IsAuthenticated)
                    return null;

                return _stripDomain ? Context.User.LocalName : Context.User.Name;
            }
        }

        public virtual void UserCreationFailed(string username, string emailAddress,
            IDictionary<string, string> userData, string message, ErrorResponse errorResponse)
        {
            LogError(errorResponse.ToString() + ":" + message,
                new ApplicationException(string.Format("Failed to create account for {0},{1}:{2}", username,
                    emailAddress, errorResponse)));
        }

        public virtual string DefaultUserLanguageKey
        {
            get { return _defaultLanguageKey; }
        }

        public virtual string DefaultUserName
        {
            get { return _defaultUserName; }
        }

        public Uri EvolutionBaseUrl
        {
            get { return new Uri(this.EvolutionRootUrl); }
        }

        public virtual System.Net.NetworkCredential EvolutionCredentials
        {
            get
            {
                if (_credentials == null)
                {
                    if (!string.IsNullOrWhiteSpace(_networkUsername) && !string.IsNullOrWhiteSpace(_networkPassword))
                    {
                        _credentials = new NetworkCredential(_networkUsername, _networkPassword);

                        if (!string.IsNullOrWhiteSpace(_networkDomain))
                            _credentials.Domain = _networkDomain;
                    }
                }

                return _credentials;
            }
        }

        public virtual string GetAuthorizationCookieValue()
        {
            HttpContextBase context = GetCurrentHttpContext();
            HttpRequestBase request;

            try
            {
                request = context.Request;
            }
            catch
            {
                return null;
            }

            if (request != null)
            {
                var cookie = request.Cookies[_cookieName];
                if (cookie != null)
                    return cookie.Value;
            }

            return null;
            ;
        }

        public Uri LocalOAuthClientHttpHandlerUrl
        {
            get { return new Uri(GetCurrentHttpContext().Request.Url, GetCurrentHttpContext().Response.ApplyAppPathModifier(_oauthCallbackHandler)); }
        }

        public string OAuthClientId
        {
            get { return _oauthClientId; }
        }

        public string OAuthClientSecret
        {
            get { return _oauthSecret; }
        }

        public void SetAuthorizationCookie(string value)
        {
            HttpContextBase context = GetCurrentHttpContext();
            HttpResponseBase response = null;
            HttpRequestBase request = null;

            try
            {
                response = context.Response;
                request = context.Request;
            }
            catch
            {
            }

            if (response != null && request != null)
            {
                var cookie = response.Cookies[_cookieName];

                if (cookie != null)
                    response.Cookies.Remove(cookie.Name);

                cookie = request.Cookies[_cookieName];

                if (cookie == null)
                    cookie = new HttpCookie(_cookieName);

                cookie.HttpOnly = true;
                if (!string.IsNullOrEmpty(value))
                {
                    cookie.Value = value;
                    cookie.Expires = DateTime.Now.AddDays(30);
                }
                else
                {
                    cookie.Value = string.Empty;
                    cookie.Expires = DateTime.Now.AddDays(-30);
                }

                response.Cookies.Add(cookie);
            }
            ;
        }

        public void UserLogOutFailed(NameValueCollection state)
        {

        }

        public virtual void UserLoggedIn(NameValueCollection state)
        {
            var usr = Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.GetAuthenticatedUser(this.Id);
            string rtnUrl = null;
            if (state != null)
            {
                rtnUrl = state["rtn"];
            }

            if (string.IsNullOrEmpty(rtnUrl))
                rtnUrl = "~/";

            GetCurrentHttpContext().Response.Redirect(rtnUrl);
        }

        public virtual void UserLoggedOut(NameValueCollection state)
        {
            string rtnUrl = null;
            if (state != null)
            {
                rtnUrl = state["rtn"];
            }

            if (string.IsNullOrEmpty(rtnUrl))
                rtnUrl = "~/";

            GetCurrentHttpContext().Response.Redirect(rtnUrl);

          
        }

        public void UserLoginFailed(NameValueCollection state)
        {

        }

        public bool EnableEvolutionUserSynchronization
        {
            get { return _enableSync; }
        }

        public string GetEvolutionUserSynchronizationCookieValue()
        {
            var context = GetCurrentHttpContext();
            if (context != null)
            {
                var cookie = context.Request.Cookies[_userSyncCookieName];
                if (cookie != null)
                    return cookie.Value;
            }
            return null;
        }

        #endregion

        #region SitecoreHost Internals

        protected virtual void LoadConfiguration(string hostname)
        {
            var config = Factory.GetConfigNode("Zimbra/hosts/host[@name='" + hostname + "']");
            if (config == null)
                throw new ConfigurationErrorsException("host configuration has not been defined for this object");

            var id = config.GetGuidAttributeValueOrDefault("id", null);
            if (id == null || !id.HasValue)
                throw new ConfigurationErrorsException("host node must contain a valid guid id");

            _id = id.Value;


            _evolutionRootUrl = config.GetStringAttributeValueOrDefault("socialSiteUrl", null);
            if (string.IsNullOrWhiteSpace(_evolutionRootUrl))
                throw new ConfigurationErrorsException("You must specify an social site url on the host or hosts node");

            _callbackUrl = config.GetStringAttributeValueOrDefault("callbackUrl", null);
            if (string.IsNullOrWhiteSpace(_callbackUrl))
                throw new ConfigurationErrorsException("You must specify a callback url");

         
            _oauthClientId = config.GetStringAttributeValueOrDefault("oauthClientId", null);
            if (string.IsNullOrWhiteSpace(_oauthClientId))
                throw new ConfigurationErrorsException("You must specify an oauth client Id");


            _oauthSecret = config.GetStringAttributeValueOrDefault("oauthSecret", null);
            if (string.IsNullOrWhiteSpace(_oauthSecret))
                throw new ConfigurationErrorsException("You must specify a an oauth secret key");

            _cookieName = config.GetStringAttributeValueOrDefault("cookieName", "CSUser");
            _stripDomain = config.GetBoolAttributeValueOrDefault("stripLocalUserDomain", true);


            _defaultUserName = config.GetStringAttributeValueOrDefault("defaultUsername", "Anonymous");
            _defaultLanguageKey = config.GetStringAttributeValueOrDefault("defaultLanguageKey", "en-US");
            _userSyncCookieName = config.GetStringAttributeValueOrDefault("userSynchronizationCookieName", "evosync");
            _enableSync = config.GetBoolAttributeValueOrDefault("enableLocalUserSynchronization", false);
            _enableUserCreation = config.GetBoolAttributeValueOrDefault("useLocalAuthenticatedUser", false);

            if (_enableUserCreation)
            {
                _evolutionUserSynchronizationManagementUserName =
                    config.GetStringAttributeValueOrDefault("socialMembershipAdministrationUsername", null);

                if(string.IsNullOrWhiteSpace(_evolutionUserSynchronizationManagementUserName))
                    throw new ConfigurationErrorsException(
                         "When using sitecore's authenticated a user a social username must be provided that has membership administration capabilities.");
            }

            _oauthCallbackHandler = config.GetStringAttributeValueOrDefault("oauthCallbackHandler", null);
            _networkUsername = config.GetStringAttributeValueOrDefault("networkUsername", null);
            _networkPassword = config.GetStringAttributeValueOrDefault("networkPassword", null);
            _networkDomain = config.GetStringAttributeValueOrDefault("networkDomain", null);


        }

        private User GetAccessingUser()
        {
            bool isGet = false;
            var context = GetCurrentHttpContext();
            var url = "";
            try
            {
                isGet = context.Request.HttpMethod.ToUpper() == "GET";
                url = context.Request.Url.OriginalString;
            }
            catch (Exception)
            {

                isGet = false;
            }

            var user = context.Items["Sitecore-User"] as Telligent.Evolution.Extensibility.OAuthClient.Version1.User;
            if (user == null)
            {

                if (isGet)
                {
                    NameValueCollection col = new NameValueCollection();
                    col.Add("rtn", url);
                    user =
                        Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.GetAuthenticatedUser(
                            this.Id, col, redirectUrl => context.Response.Redirect(redirectUrl.OriginalString, true));
                }
                else
                    user =
                        Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.GetAuthenticatedUser(
                            this.Id);

                if (user == null)
                    user =
                        Telligent.Evolution.Extensibility.OAuthClient.Version1.OAuthAuthentication.GetDefaultUser(
                            this.Id);

                context.Items["Sitecore-User"] = user;
            }

            return user;
        }
        #endregion




        public override Guid Id
        {
            get { return _id  ; }
        }
    }
}
