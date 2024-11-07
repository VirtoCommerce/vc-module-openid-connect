using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.OpenIdConnectModule.Core.Models;
using VirtoCommerce.OpenIdConnectModule.Data.Services;
using VirtoCommerce.Platform.Core.Modularity;
using VirtoCommerce.Platform.Core.Security.ExternalSignIn;
using VirtoCommerce.Platform.Security.ExternalSignIn;

namespace VirtoCommerce.OpenIdConnectModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        var oidcSection = Configuration.GetSection("oidc");
        if (oidcSection.GetChildren().Any())
        {
            var options = new OidcOptions();
            oidcSection.Bind(options);
            serviceCollection.Configure<OidcOptions>(oidcSection);

            if (options.Enabled)
            {
                var authBuilder = new AuthenticationBuilder(serviceCollection);

                authBuilder.AddOpenIdConnect(options.AuthenticationType, options.AuthenticationCaption,
                    openIdConnectOptions =>
                    {
                        openIdConnectOptions.MapInboundClaims = false;

                        openIdConnectOptions.Scope.Clear();
                        if (!oidcSection.GetSection("Scope").Exists())
                        {
                            openIdConnectOptions.Scope.Add("openid");
                            openIdConnectOptions.Scope.Add("profile");
                            openIdConnectOptions.Scope.Add("email");
                        }

                        oidcSection.Bind(openIdConnectOptions);

                        openIdConnectOptions.Events.OnRedirectToIdentityProvider = context =>
                        {
                            var oidcUrl = context.Properties.GetOidcUrl();
                            if (!string.IsNullOrEmpty(oidcUrl))
                            {
                                context.ProtocolMessage.RedirectUri = oidcUrl;
                            }

                            return Task.CompletedTask;
                        };

                        openIdConnectOptions.Events.OnAccessDenied = context =>
                        {
                            // Need a base URI (any) to work with relative URLs
                            var baseUri = new Uri("https://localhost");
                            var uri = new Uri(baseUri, context.ReturnUrl);
                            var returnUrl = HttpUtility.ParseQueryString(uri.Query).GetValues(context.ReturnUrlParameter)?.FirstOrDefault();

                            context.Response.Redirect(returnUrl ?? "/");
                            context.HandleResponse();

                            return Task.CompletedTask;
                        };
                    });

                // Register external sign in provider implementation
                serviceCollection.AddSingleton<OidcExternalSignInProvider>();
                serviceCollection.AddSingleton(provider => new ExternalSignInProviderConfiguration
                {
                    AuthenticationType = options.AuthenticationType,
                    Provider = provider.GetService<OidcExternalSignInProvider>(),
                    LogoUrl = "Modules/$(VirtoCommerce.OpenIdConnectModule)/Content/openid-icon.webp",
                });
            }
        }
    }

    public void PostInitialize(IApplicationBuilder appBuilder)
    {
        // Nothing to do here
    }

    public void Uninstall()
    {
        // Nothing to do here
    }
}
