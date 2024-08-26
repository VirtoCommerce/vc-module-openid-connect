using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VirtoCommerce.Platform.Core.Modularity;
using Microsoft.AspNetCore.Authentication;
using VirtoCommerce.Platform.Security.ExternalSignIn;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using VirtoCommerce.OpenIdConnectModule.Data.Services;
using VirtoCommerce.OpenIdConnectModule.Core;
using VirtoCommerce.OpenIdConnectModule.Core.Models;

namespace VirtoCommerce.OpenIdConnectModule.Web;

public class Module : IModule, IHasConfiguration
{
    public ManifestModuleInfo ModuleInfo { get; set; }
    public IConfiguration Configuration { get; set; }

    public void Initialize(IServiceCollection serviceCollection)
    {
        Microsoft.IdentityModel.JsonWebTokens.JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

        var OidcSection = Configuration.GetSection(ModuleConstants.OidcAuthenticationType);
        if (OidcSection.GetChildren().Any())
        {
            var options = new OidcOptions();
            OidcSection.Bind(options);
            serviceCollection.Configure<OidcOptions>(OidcSection);

            if (options.Enabled)
            {
                serviceCollection.AddSession(options =>
                {
                    options.Cookie.HttpOnly = true;
                    options.Cookie.IsEssential = true;
                });
                serviceCollection.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = ModuleConstants.OidcAuthenticationType;
                });
                serviceCollection.AddCookiePolicy(options =>
                  {
                      options.MinimumSameSitePolicy = SameSiteMode.None; 
                  });

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
                    });

                // register default external provider implementation
                serviceCollection.AddSingleton<OidcExternalSignInProvider>();
                serviceCollection.AddSingleton(provider => new ExternalSignInProviderConfiguration
                {
                    AuthenticationType = ModuleConstants.OidcAuthenticationType,
                    Provider = provider.GetService<OidcExternalSignInProvider>(),
                    LogoUrl = "Modules/$(VirtoCommerce.OpenIdConnectModule)/Content/openid-icon.webp"
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
