using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore;
using Telligent.Evolution.Extensibility.Rest.Version1;

namespace Telligent.SitecoreSDK
{
    public class SitecoreLocalUserResolver:ILocalUserResolver
    {
        public LocalUser GetLocalUserDetails(System.Web.HttpContextBase context, Host host)
        {

            if (Context.User == null || !Context.User.IsAuthenticated)
                return null;

            return new LocalUser(Context.User.LocalName, Context.User.Profile.Email);
        }
    }

    public class SitecoreLocalUserResolverWithDomain : ILocalUserResolver
    {
        public LocalUser GetLocalUserDetails(System.Web.HttpContextBase context, Host host)
        {

            if (Context.User == null || !Context.User.IsAuthenticated)
                return null;

            return new LocalUser(Context.User.Name, Context.User.Profile.Email);
        }
    }
}
