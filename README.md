# OpenID Connect
OpenID Connect is an identity module on top of the OAuth 2.0 protocol, allowing clients to verify the identity of end-users based on the authentication performed by an authorization server. It also provides basic user profile information.

## Key Features
* Authentication: Ensures secure user authentication and authorization.
* Single Sign-On (SSO): Allows users to log in once and gain access to multiple applications.
* User Information: Provides access to user profile information.
* Interoperability: Works with various identity providers like Google, Microsoft, and others.
* Security: Implements robust security measures to protect user data.

## Configuration
The module configuration for OpenID Connect (OIDC) authentication is defined in the appsettings.json file under the `oidc` section. This configuration enables the application to authenticate users using the OIDC protocol. Below are the parameters and their descriptions:

* `Enabled`: A boolean value indicating whether OIDC authentication is enabled. Set to `true` to enable.
* `AuthenticationType`: Specifies the unique name of the authentication method. Default value is `"oidc"`.
* `AuthenticationCaption`: A user-friendly name for the authentication method. Default value is `"OpenID Connect"`.
* `AllowCreateNewUser`: A boolean value indicating whether a new user should be created upon successful authentication. Default value is `true`.
* `DefaultUserType`: Specifies the user type of a new user. Default value is `"Manager"`.
* `DefaultUserRoles`: Specifies the list of user roles of a new user. Default value is `[]`.
* `HasLoginForm`: A boolean value indicating whether to display a dedicated login form or not. Default value is `true`.
* `Priority`: An integer value specifying the sorting order of the authentication method. Default value is `0`.
* `Authority`: The URL of the OIDC provider. This is the base address of the identity provider, e.g., https://localhost:5001.
* `ClientId`: The client identifier issued to the application by the OIDC provider.
* `ClientSecret`: The client secret issued to the application by the OIDC provider. This should be kept confidential.
* `ResponseMode`: Defines how the authorization response is returned. Default value is `"form_post"`.
* `ResponseType`: Specifies the type of response expected from the OIDC provider. Default value is `"id_token"`.
* `Scope`: An array of strings specifying the scopes requested from the OIDC provider. Default value is `["openid", "profile"]`.
* `GetClaimsFromUserInfoEndpoint`: A boolean value indicating whether to retrieve additional claims from the user info endpoint.
* `CallbackPath`: The path to which the OIDC provider will redirect after authentication. Default value is `"/signin-oidc"`.
* `RemoteSignOutPath`: Requests received on this path will cause the handler to invoke SignOut. Default value is `"/signout-oidc"`.
* `SignedOutCallbackPath`: The path to which the OIDC provider will redirect after signing out. Default value is `"/signout-openid-connect"`.
* `SignedOutRedirectUri`: The URI where the user agent will be redirected to after application is signed out from the identity provider. Default value is `"/"`.

The list of other parameters can be found in the [OpenIdConnectOptions](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.openidconnectoptions?view=aspnetcore-1.1&viewFallbackFrom=aspnetcore-8.0) documentation.

> [!IMPORTANT] 
> Note: If you have other external sign-in providers installed (Microsoft Entra ID or Google SSO) you need to make sure to use unique authentication types and callback paths for each provider.

> [!NOTE]
> The module was designed and tested with this version of the platform [VCST-1415: Platform as authorization server](https://github.com/VirtoCommerce/vc-platform/pull/2809)

### Example settings for Virto Commerce
```json
  "oidc": {
    "Enabled": true,
    "AuthenticationType": "virto",
    "AuthenticationCaption": "Virto Commerce",
    "Authority": "https://localhost:5001",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret",
    "ResponseMode" : "query",
    "ResponseType" : "code",
    "Scope" : ["openid", "profile", "email"],
    "GetClaimsFromUserInfoEndpoint" : true
  }
```

### Example settings for Google
```json
  "oidc": {
    "Enabled": true,
    "AuthenticationType": "google",
    "AuthenticationCaption": "Google",
    "Authority": "https://accounts.google.com",
    "ClientId": "your-client-id",
    "ClientSecret": "your-client-secret"
  }
```

## License
Copyright (c) Virto Solutions LTD.  All rights reserved.

Licensed under the Virto Commerce Open Software License (the "License"); you
may not use this file except in compliance with the License. You may
obtain a copy of the License at

http://virtocommerce.com/opensourcelicense

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
implied.
