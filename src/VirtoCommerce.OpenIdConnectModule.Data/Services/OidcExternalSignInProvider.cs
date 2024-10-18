using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using VirtoCommerce.OpenIdConnectModule.Core;
using VirtoCommerce.OpenIdConnectModule.Core.Models;
using VirtoCommerce.Platform.Security.ExternalSignIn;

namespace VirtoCommerce.OpenIdConnectModule.Data.Services;

public class OidcExternalSignInProvider : IExternalSignInProvider
{
    private readonly OidcOptions _oidcOptions;

    public OidcExternalSignInProvider(IOptions<OidcOptions> oidcOptions)
    {
        _oidcOptions = oidcOptions.Value;
    }

    public int Priority => ModuleConstants.ProviderPriority;

    public bool HasLoginForm => _oidcOptions.HasLoginForm;

    public bool AllowCreateNewUser => _oidcOptions.AllowCreateNewUser;

    public string GetUserName(ExternalLoginInfo externalLoginInfo)
    {
        var userName = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Name);

        return userName;
    }

    public string[] GetUserRoles()
    {
        return _oidcOptions.DefaultUserRoles;
    }

    public string GetUserType()
    {
        return _oidcOptions.DefaultUserType;
    }
}
