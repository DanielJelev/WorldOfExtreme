using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace WorldOfExtreme.IdentityServer
{
    public class Config
    {
        public static string HOST_URL = "https://localhost:51786";
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(), 
                new IdentityResource("worldOfExtreme.identity",new []{ "role", "admin", "user"} )
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("worldOfExtreme.api", "Scope for the worldOfExtreme ApiResource",
                    new List<string> { "role", "admin", "user"}),
               
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("worldOfExtreme.apiresourec")
                {
                    ApiSecrets =
                    {
                        new Secret("worldOfExtremeSecret".Sha256())
                    }, 
                    Scopes = new List<string> { "worldOfExtreme.api" }
                }
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
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 360,// 120 seconds, default 60 minutes
                    IdentityTokenLifetime = 30,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:4200",
                        "https://localhost:4200/silent-renew.html"

                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:4200/Unauthorized"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:4200",
                        "http://localhost:4200"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "role",
                        "profile",
                        "email",
                        "user"
                    }
                }
            };
        }
    }
}