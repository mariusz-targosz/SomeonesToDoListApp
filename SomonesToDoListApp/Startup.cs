using System;
using SomeonesToDoListApp;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;

[assembly: OwinStartup(typeof(Startup))]
namespace SomeonesToDoListApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            });
            appBuilder.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            appBuilder.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            appBuilder.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
            appBuilder.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "",
                ClientSecret = ""
            });
        }
    }
}
