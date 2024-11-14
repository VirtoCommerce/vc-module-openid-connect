using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using VirtoCommerce.OpenIdConnectModule.Core.Models;
using VirtoCommerce.Platform.Security.ExternalSignIn;

namespace VirtoCommerce.OpenIdConnectModule.Data.Services;

public class OidcExternalSignInProvider : IExternalSignInProvider
{
    private readonly OidcOptions _oidcOptions;

    public OidcExternalSignInProvider(OidcOptions oidcOptions)
    {
        _oidcOptions = oidcOptions;
    }

    public int Priority => _oidcOptions.Priority;

    public bool HasLoginForm => _oidcOptions.HasLoginForm;

    public bool AllowCreateNewUser => _oidcOptions.AllowCreateNewUser;

    public string GetUserName(ExternalLoginInfo externalLoginInfo)
    {
        var userName = externalLoginInfo.Principal.FindFirstValue(_oidcOptions.UserNameClaimType);

        return userName;
    }

    public string GetEmail(ExternalLoginInfo externalLoginInfo)
    {
        var email = externalLoginInfo.Principal.FindFirstValue(_oidcOptions.EmailClaimType);

        return email;
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
