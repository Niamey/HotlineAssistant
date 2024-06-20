using MediatR;
using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.Common.Helpers;

namespace Vostok.HotLineAssistant.WebApi.Authorization
{
	public abstract class RequestAuthorizationFilter<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly ICurrentUserProvider _userProvider;

		protected RequestAuthorizationFilter(ICurrentUserProvider userProvider)
		{
			_userProvider = Assure.ArgumentNotNull(userProvider, nameof(userProvider));
		}

		public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			var user = _userProvider.GetUser();

			if (request != null && user != null)
				Filter(request, user);

			return next();
		}

		protected abstract void Filter(TRequest request, ClaimsPrincipal user);
	}
}