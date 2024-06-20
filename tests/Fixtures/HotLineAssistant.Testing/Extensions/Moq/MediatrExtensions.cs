using MediatR;
using System;
using System.Linq.Expressions;
using System.Threading;
using Moq;
using Moq.Language.Flow;

namespace HotLineAssistant.Testing.Extensions.Moq
{
	public static class MediatrExtensions
	{
		public static IReturnsResult<IMediator> SetupCreate(this Mock<IMediator> mediator, IRequest<int> command, int result = 1)
		{
			return mediator.Setup(m => m.Send(command, CancellationToken.None))
				.ReturnsAsync(result);
		}

		public static IReturnsResult<IMediator> SetupUpdate(this Mock<IMediator> mediator, IRequest command, bool result = true)
		{
			return mediator.Setup(m => m.Send(command, CancellationToken.None))
				.ReturnsAsync(Unit.Value);
		}

		public static IReturnsResult<IMediator> SetupQuery<T>(this Mock<IMediator> mediator, IRequest<T> query, T result)
			where T : class
		{
			return mediator.Setup(m => m.Send(query, CancellationToken.None))
				.ReturnsAsync(result);
		}

		public static IReturnsResult<IMediator> SetupQuery<TRequest, TResponse>(this Mock<IMediator> mediator, Expression<Func<TRequest, bool>> match, TResponse result)
			where TRequest : IRequest<TResponse> where TResponse : class
		{
			return mediator.Setup(m => m.Send(It.Is(match), CancellationToken.None))
				.ReturnsAsync(result);
		}

		public static IReturnsResult<IMediator> SetupFailingQuery<TRequest, TResponse>(this Mock<IMediator> mediator, Expression<Func<TRequest, bool>> match)
			where TRequest : IRequest<TResponse> where TResponse : class
		{
			return SetupQuery<TRequest, TResponse>(mediator, match, null);
		}

		public static IReturnsResult<IMediator> SetupFailingQuery<TRequest, TResponse>(this Mock<IMediator> mediator)
			where TRequest : IRequest<TResponse> where TResponse : class
		{
			return mediator.Setup(m => m.Send(It.IsAny<TRequest>(), CancellationToken.None))
				.ReturnsAsync(null as TResponse);
		}
    }
}