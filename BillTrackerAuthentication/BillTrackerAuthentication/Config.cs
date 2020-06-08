// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace BillTrackerAuthentication
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource("trackerApi", "Bill Tracker")
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                
                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",                 

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "https://localhost:44388/callback.html"},

                    PostLogoutRedirectUris = { "https://localhost:44388/index.html" },
                    AllowedCorsOrigins = { "https://localhost:44388" },

                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId,
                                      IdentityServerConstants.StandardScopes.Profile,
                                      "trackerApi" }
                }
            };
    }
}