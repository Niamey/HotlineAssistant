using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Statements.Services;
using Vostok.Hotline.Gateway.Client.Statements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Statements.Requests.Handlers
{
	public class StatementQueryHandler : IRequestHandler<StatementQuery, StatementViewModel>
	{
		private readonly StatementService _statementService;

		public StatementQueryHandler(StatementService statementService)
		{
			_statementService = statementService;
		}

		public async Task<StatementViewModel> Handle(StatementQuery searchRequest, CancellationToken cancellationToken)
		{
			return await _statementService.GetStatementAsync(searchRequest, cancellationToken);
		}
	}
}
