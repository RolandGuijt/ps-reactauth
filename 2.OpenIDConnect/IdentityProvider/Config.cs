using Duende.IdentityServer.Models;

namespace Globomantics.Idp;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "react",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,

                RedirectUris = { "https://localhost:7180/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:7180/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:7180/signout-callback-oidc" },

                AlwaysIncludeUserClaimsInIdToken = true,
                AllowedScopes = { "openid", "profile" }
            },
        };
}