using System.Collections.Generic;
using VirtoCommerce.Platform.Core.Settings;

namespace VirtoCommerce.OpenIdConnectModule.Core;

public static class ModuleConstants
{
    public const string OidcAuthenticationType = "oidc";
    public const int ProviderPriority = 200;

    public static class Security
    {
        public static class Permissions
        {
            public const string Access = "oidc:access";
            public const string Create = "oidc:create";
            public const string Read = "oidc:read";
            public const string Update = "oidc:update";
            public const string Delete = "oidc:delete";

            public static string[] AllPermissions { get; } =
            {
                Access,
                Create,
                Read,
                Update,
                Delete,
            };
        }
    }

    public static class Settings
    {
        public static class General
        {
            public static SettingDescriptor OidcEnabled { get; } = new()
            {
                Name = "oidc.oidcEnabled",
                GroupName = "oidc|General",
                ValueType = SettingValueType.Boolean,
                DefaultValue = false,
            };

            public static SettingDescriptor OidcPassword { get; } = new()
            {
                Name = "oidc.oidcPassword",
                GroupName = "oidc|Advanced",
                ValueType = SettingValueType.SecureString,
                DefaultValue = "qwerty",
            };

            public static IEnumerable<SettingDescriptor> AllGeneralSettings
            {
                get
                {
                    yield return OidcEnabled;
                    yield return OidcPassword;
                }
            }
        }

        public static IEnumerable<SettingDescriptor> AllSettings
        {
            get
            {
                return General.AllGeneralSettings;
            }
        }
    }
}
