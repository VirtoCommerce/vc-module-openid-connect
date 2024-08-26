# OpenID Connect


appsetting.json must have section "oidc" with following parameters:

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
    "CallbackPath": "/signin-oidc"
  }


  Please note:
  1. VC platform supports ResponseMode query only
