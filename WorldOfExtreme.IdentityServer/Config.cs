using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace WorldOfExtreme.IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(), 
                new IdentityResource("dataeventrecordsir",new []{ "role", "admin", "user"} )
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("dataEventRecords", "Scope for the dataEventRecords ApiResource",
                    new List<string> { "role", "admin", "user"}),
               
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("dataEventRecordsApi")
                {
                    ApiSecrets =
                    {
                        new Secret("dataEventRecordsSecret".Sha256())
                    }, 
                    Scopes = new List<string> { "dataEventRecords" }
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
                    ClientName = "angularclient",
                    ClientId = "angularclient",
                    AccessTokenType = AccessTokenType.Reference,
                    AccessTokenLifetime = 360,// 120 seconds, default 60 minutes
                    IdentityTokenLifetime = 30,
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                        "https://localhost:44372",
                        "https://localhost:44372/silent-renew.html"

                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "https://localhost:44372/Unauthorized"
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                        "https://localhost:44372",
                        "http://localhost:44372"
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "role",
                        "profile",
                        "email"
                    }
                }
            };
        }
    }
}