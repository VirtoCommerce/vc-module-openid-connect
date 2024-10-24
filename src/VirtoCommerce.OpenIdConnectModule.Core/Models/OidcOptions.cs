using System.Collections.Generic;

namespace VirtoCommerce.OpenIdConnectModule.Core.Models
{
    public class OidcOptions
    {
        /// <summary>
        /// Determines whether the user authentication via OpenId Connect is enabled.
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// Sets AuthenticationType value for OpenId Connect authentication provider.
        /// </summary>
        public string AuthenticationType { get; set; } = "oidc";

        /// <summary>
        /// Sets human-readable caption for OpenId Connect authentication provider. It is visible on sign-in page.
        /// </summary>
        public string AuthenticationCaption { get; set; } = "OpenID Connect";

        /// <summary>
        /// URL of the OpenId Connect endpoint used for authentication.
        /// </summary>
        public string Authority { get; set; }

        /// <summary>
        /// Default user type for users created by OpenId Connect accounts.
        /// </summary>
        public string DefaultUserType { get; set; } = "Manager";

        /// <summary>
        /// Default user roles for users created by OpenId Connect accounts.
        /// </summary>
        public string[] DefaultUserRoles { get; set; } = [];

        /// <summary>
        /// Allow creating new user when a user authenticates via IDP for the first time
        /// </summary>
        public bool AllowCreateNewUser { get; set; } = true;

        /// <summary>
        /// Display dedicated login form or not
        /// </summary>
        public bool HasLoginForm { get; set; } = true;

        /// <summary>
        ///  Gets or sets the discovery endpoint for obtaining metadata
        /// </summary>
        public string MetadataAddress { get; set; }

        /// <summary>
        /// Client Id 
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Client Id 
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Client Id 
        /// </summary>
        public string CallbackPath { get; set; } = "/signin-openid-connect";

        /// <summary>
        /// Response type 
        /// </summary>
        public string ResponseType { get; set; }

        /// <summary>
        /// Response mode
        /// </summary>
        public string ResponseMode { get; set; }

        /// <summary>
        /// Indicates that the authentication session lifetime (e.g. cookies) should match that of the authentication token.
        /// If the token does not provide lifetime information then normal session lifetimes will be used.
        /// This is disabled by default.
        /// </summary>
        public bool UseTokenLifetime { get; set; }

        /// <summary>
        /// Defines whether access and refresh tokens should be stored in the AuthenticationProperties after a successful authorization.
        /// This property is set to false by default to reduce the size of the final authentication cookie.
        /// </summary>
        public bool SaveTokens { get; set; }

        /// <summary>
        /// Gets or sets if HTTPS is required for the metadata address or authority. The default is true.
        /// This should be disabled only in development environments.
        /// </summary>
        public bool RequireHttpsMetadata { get; set; }

        /// <summary>
        /// Gets or sets the authentication scheme corresponding to the middleware responsible for persisting user's identity after a successful authentication.
        /// This value typically corresponds to a cookie middleware registered in the Startup class. When omitted, SignInScheme is used as a fallback value.
        /// </summary>
        public string SignInScheme { get; set; }

        /// <summary>
        /// Gets the list of permissions to request.
        /// </summary>
        public List<string> Scope { get; set; }

        /// <summary>
        /// Boolean to set whether the middleware should go to user info endpoint to retrieve additional claims or not after creating an identity from id_token received from token endpoint.
        /// The default is 'false'.
        /// </summary>
        public bool GetClaimsFromUserInfoEndpoint { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SignedOutCallbackPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SignedOutRedirectUri { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SignOutScheme { get; set; }

        /// <summary>
        /// The sorting order of the external sign-in provider.
        /// </summary>
        public int Priority { get; set; } = 200;
    }
}
