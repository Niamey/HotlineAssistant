using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using Vostok.HotLineAssistant.Common.Helpers;

namespace Vostok.HotLineAssistant.WebApi.Authorization
{
	public class QueryAuthorizationSettings
	{
		public static readonly QueryAuthorizationSettings None =
			new QueryAuthorizationSettings(new AssertionRequirement(c => true), AuthorizationQueryScope.None);

		public IAuthorizationRequirement Requirement { get; }

		public AuthorizationQueryScope Scope { get; }

		public bool IsQueryAuthorizationRequired => Scope.HasFlag(AuthorizationQueryScope.Query);

		public bool IsResourceAuthorizationRequired => Scope.HasFlag(AuthorizationQueryScope.Resource);

		public QueryAuthorizationSettings(IAuthorizationRequirement requirement, AuthorizationQueryScope scope = AuthorizationQueryScope.All)
		{
			Requirement = Assure.ArgumentNotNull(requirement, nameof(requirement));
			Scope = scope;
		}
	}

	[Flags]
	public enum AuthorizationQueryScope
	{
		None = 0,
		Query = 1,
		Resource = 2,
		All = Query | Resource
	}
}