using IdentityServer4.Models;
using System.Collections.Generic;
using IdentityServer4;

namespace Vostok.HotLineAssistant.Identity.Api
{
	public class Config
	{
		public static IEnumerable<ApiResource> GetApiResources()
		{
			return new List<ApiResource>
			{
				new ApiResource("api1", "My API")
			};
        }

		public static IEnumerable<Client> GetClients()
		{
			return new List<Client>
			{
				new Client
				{
					ClientId = "client",
					ClientName = "MVC Client",
					// no interactive user, use the clientid/secret for authentication
					AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
					RequireConsent = false,
					// secret for authentication
					ClientSecrets =
					{
						new Secret("secret".Sha256())
					},

					// scopes that client has access to
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						"api1"
					},
					AllowOfflineAccess = true
				}
			};
		}

    }
}
