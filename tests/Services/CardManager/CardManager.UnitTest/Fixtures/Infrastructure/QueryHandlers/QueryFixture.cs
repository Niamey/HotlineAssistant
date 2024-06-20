using MediatR;
using System;
using System.Threading.Tasks;

namespace CardManager.UnitTests.Fixtures.Infrastructure.QueryHandlers
{
	public abstract class QueryFixture<TQuery, TResponse> where TQuery : IRequest<TResponse>
	{
		protected bool IsReady;

		//public readonly IMapper Mapper = AutoMapperFactory.Create();

		public virtual Task Setup()
		{
			IsReady = true;

			return Task.CompletedTask;
		}

		public abstract TQuery GetQuery();

		public abstract TQuery GetUnpreparedQuery();

		public abstract TResponse GetResponse();

		protected void ThrowSetupIsRequired()
		{
			if (!IsReady)
				throw new InvalidOperationException("Setup must be called first.");
		}
    }
}