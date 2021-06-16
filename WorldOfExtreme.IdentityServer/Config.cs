using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Security.Claims;

namespace WorldOfExtreme.IdentityServer
{
    public class Config
    {
        public static string HOST_URL = "https://localhost:65027";
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                     new ApiScope("worldOfExtreme_profile", "Scope for the hooray_Api ApiResource")
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                 new ApiResource("worldOfExtremeProfile")
                {
                    ApiSecrets =
                    {
                        new Secret("worldOfExtremeProfileSecret".Sha256())
                    },
                    UserClaims = { "role", "admin", "user", "worldOfExtremeProfileSecret", "worldOfExtremeProfile.admin", "worldOfExtremeProfile.user" }
                },

            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients(IConfigurationSection stsConfig)
        {
            // client credentials client
            return new List<Client>
            {
                  new Client
                {
                    ClientName = "worldofextremeclient",
                    ClientId = "worldofextremeclient",

                    AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    IdentityTokenLifetime = 45,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:4200"
                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:4200/unauthorized",
                        "https://localhost:4200"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:4200"
                    },

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowedScopes = {
                         "openid",
                         "profile",
                         "email",
                         "worldOfExtreme_profile",
                     },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly
                },
            };
        }
    }
}