
using Sitecore;
using Sitecore.Pipelines;
using Telligent.Evolution.Extensibility.Rest.Version1;

namespace Telligent.Rest.SDK.Sitecore.Pipelines
{
    public class InitializeHost
    {
        public virtual void Process(PipelineArgs args)
        {
           


            Host.Get("default").ResolveLocalUser = (host, resolveArgs) =>
            {
                if (Context.User == null || !Context.User.IsAuthenticated)
                {
                    return null; 
                }
                return new LocalUser(Context.User.Name, Context.User.Profile.Email);
            };

            //If you do not wish to use domain accounts
            //Host.Get("default").ResolveLocalUser = (host, resolveArgs) =>
            //{
            //    if (Context.User == null || !Context.User.IsAuthenticated)
            //        return null;

            //    return new LocalUser(Context.User.LocalName, Context.User.Profile.Email);
            //};
        }
    }
}