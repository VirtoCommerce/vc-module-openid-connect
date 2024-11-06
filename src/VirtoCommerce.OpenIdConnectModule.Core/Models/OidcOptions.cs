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
        /// Allow creating new user when a user authenticates via IDP for the first time
        /// </summary>
        public bool AllowCreateNewUser { get; set; } = true;

        /// <summary>
        /// Default user type for users created by OpenId Connect accounts.
        /// </summary>
        public string DefaultUserType { get; set; } = "Manager";

        /// <summary>
        /// Default user roles for users created by OpenId Connect accounts.
        /// </summary>
        public string[] DefaultUserRoles { get; set; } = [];

        /// <summary>
        /// Display dedicated login form or not
        /// </summary>
        public bool HasLoginForm { get; set; } = true;

        /// <summary>
        /// The sorting order of the external sign-in provider.
        /// </summary>
        public int Priority { get; set; }
    }
}
