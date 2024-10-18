using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

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
                        openIdConnectOptions.ClientId = options.ClientId;
                        openIdConnectOptions.ClientSecret = options.ClientSecret;
                        openIdConnectOptions.Authority = options.Authority;
                        openIdConnectOptions.UseTokenLifetime = options.UseTokenLifetime;
                        openIdConnectOptions.SaveTokens = options.SaveTokens;
                        openIdConnectOptions.ResponseMode = options.ResponseMode;
                        openIdConnectOptions.ResponseType = options.ResponseType;
                        openIdConnectOptions.MetadataAddress = options.MetadataAddress;
                        openIdConnectOptions.RequireHttpsMetadata = options.RequireHttpsMetadata;
                        openIdConnectOptions.SignInScheme = options.SignInScheme;
                        openIdConnectOptions.SignOutScheme = options.SignOutScheme;
                        openIdConnectOptions.CallbackPath = options.CallbackPath;
                        openIdConnectOptions.SignedOutCallbackPath = options.SignedOutCallbackPath;
                        openIdConnectOptions.SignedOutRedirectUri = options.SignedOutRedirectUri;
                        openIdConnectOptions.GetClaimsFromUserInfoEndpoint = options.GetClaimsFromUserInfoEndpoint;
                        options.Scope.ForEach(scope => openIdConnectOptions.Scope.Add(scope));

                        openIdConnectOptions.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                        openIdConnectOptions.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");

                        openIdConnectOptions.Events.OnRedirectToIdentityProvider = context =>
                        {
                            var oidcUrl = context.Properties.GetOidcUrl();
                            if (!string.IsNullOrEmpty(oidcUrl))
                            {
                                context.ProtocolMessage.RedirectUri = oidcUrl;
                            }

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
