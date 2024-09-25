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

* Enabled: A boolean value indicating whether OIDC authentication is enabled. Set to true to enable.
* AuthenticationType: Specifies the type of authentication. For OIDC, this should be set to "oidc".
* Authority: The URL of the OIDC provider. This is the base address of the identity provider, e.g., https://localhost:5001.
* AuthenticationCaption: A user-friendly name for the authentication method, e.g., "OpenID Connect".
* ApplicationId: The unique identifier for the application registered with the OIDC provider.
* ClientId: The client identifier issued to the application by the OIDC provider.
* ClientSecret: The client secret issued to the application by the OIDC provider. This should be kept confidential.
* DefaultUserType: Specifies the default user type upon successful authentication, e.g., "Manager".
* ResponseMode: Defines how the authorization response is returned. Common values are "query" or "fragment".
* ResponseType: Specifies the type of response expected from the OIDC provider. For example, "code" for authorization code flow.
* RequireHttpsMetadata: A boolean value indicating whether HTTPS metadata is required. Set to false for development environments.
* SaveTokens: A boolean value indicating whether to save the tokens received from the OIDC provider.
* UseTokenLifetime: A boolean value indicating whether to use the token’s lifetime as provided by the OIDC provider.
* Scope: An array of strings specifying the scopes requested from the OIDC provider, e.g., ["profile", "email"].
* GetClaimsFromUserInfoEndpoint: A boolean value indicating whether to retrieve additional claims from the user info endpoint.
* CallbackPath: The path to which the OIDC provider will redirect after authentication, by default "/signin-openid-connect"

> Note: If you other external sign-in providers installed (Microsoft Entra ID or Google SSO) you need to make sure to use unique callback paths for each provider.

```json
  "oidc": {
    "Enabled": true,
    "AuthenticationType": "oidc",
    "Authority": "https://localhost:5001",
    "AuthenticationCaption": "OpenID Connect",
    "ApplicationId": "cf4cb5a0-17c8-4cde-91fd-f23f0891ae20",
    "ClientId": "cf4cb5a0-17c8-4cde-91fd-f23f0891ae20",
    "ClientSecret": "ad724695-ca42-4271-a9ba-636a2d50f7ec",
    "DefaultUserType": "Manager",
    "ResponseMode" : "query",
    "ResponseType" : "code",
    "RequireHttpsMetadata" : false,
    "SaveTokens" : true,
    "UseTokenLifetime" : true,
    "Scope" : ["profile", "email"],
    "GetClaimsFromUserInfoEndpoint" : true,
    "CallbackPath": "/signin-openid-connect"
  }
```

## Known limitation
1. The module was designed and tested with this version of the platform [VCST-1415: Platform as authorization server](https://github.com/VirtoCommerce/vc-platform/pull/2809)
2. Supports ResponseMode query only.

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
