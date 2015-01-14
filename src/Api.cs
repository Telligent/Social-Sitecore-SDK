using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Configuration;
using Telligent.Evolution.Extensibility.OAuthClient.Version1;
using Telligent.Evolution.Extensibility.UI.Version1;


namespace Zimbra.Social.RemotingSDK.Sitecore
{
    public static class Api
    {
        public static SitecoreHost GetHost(string hostName,bool forceRegister = false)
        {
            var config = Factory.GetConfigNode("Zimbra/hosts/host[@name='" + hostName + "']");
            if (config == null)
                throw new ConfigurationErrorsException("host configuration has not been defined for this object");

            var id = config.GetGuidAttributeValueOrDefault("id", null);
            if (id == null || !id.HasValue)
                throw new ConfigurationErrorsException("host node must contain a valid guid id");

            
            var host = RemoteScriptedContentFragmentHost.Get(id.Value);
            if (host == null && forceRegister)
            {
                host = new SitecoreHost(hostName);
                Register(host as SitecoreHost);
            }
               

            return host as SitecoreHost;
        }

        public static void Register(SitecoreHost host)
        {
            RemoteScriptedContentFragmentHost.Register(host);
            OAuthAuthentication.RegisterConfiguration(host);
        }

      
        
    }
}
