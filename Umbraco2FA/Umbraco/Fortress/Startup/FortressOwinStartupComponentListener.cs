using System;
using Orc.Fortress.UserManagement;
using Owin;
using Umbraco.Core;
using Umbraco.Core.Configuration;
using Umbraco.Core.Logging;
using Umbraco.Core.Models.Identity;
using Umbraco.Core.Security;
using Umbraco.Web;
using Umbraco.Web.Security.Identity;

namespace Orc.Fortress.Startup
{
    public class FortressOwinStartupComponentListener : ApplicationEventHandler
    { 
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            UmbracoDefaultOwinStartup.MiddlewareConfigured += (_, e) => ConfigureServices(e.AppBuilder);
            UmbracoDefaultOwinStartup.MiddlewareConfigured += (_, e) => ConfigureMiddleware(e.AppBuilder);
        }
        
        private void ConfigureMiddleware(IAppBuilder app)
        {
            // this is called when Umbraco is done configuring the Owin middleware
            LogHelper.Info(typeof(FortressOwinStartup), "OFFROADCODE: ConfigureMiddleware");

            app.UseTwoFactorSignInCookie(global::Umbraco.Core.Constants.Security.BackOfficeTwoFactorAuthenticationType, TimeSpan.FromMinutes(5));
        }

        private void ConfigureServices(IAppBuilder app)
        {
            app.SetUmbracoLoggerFactory();

            var applicationContext = ApplicationContext.Current;
            LogHelper.Info(typeof(FortressOwinStartup), "Fortress: Startup");
            //Here's where we assign a custom UserManager called MyBackOfficeUserManager
            app.ConfigureUserManagerForUmbracoBackOffice<FortressBackOfficeUserManager, BackOfficeIdentityUser>(
                applicationContext,
                (options, context) =>
                {
                    var membershipProvider = Umbraco.Core.Security.MembershipProviderExtensions.GetUsersMembershipProvider().AsUmbracoMembershipProvider();

                    //Create the custom MyBackOfficeUserManager
                    var userManager = FortressBackOfficeUserManager.Create(options,
                            applicationContext.Services.UserService,
                            applicationContext.Services.EntityService,
                            applicationContext.Services.ExternalLoginService,
                            membershipProvider,
                             UmbracoConfig.For.UmbracoSettings().Content);
                    return userManager;
                });
        }
    }
}
