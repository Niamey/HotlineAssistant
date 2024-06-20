using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Models.Responses.Commands;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Commands;
using Vostok.Hotline.Gateway.Client.Statements.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Statements.Services;
using Vostok.Hotline.Gateway.Client.Statements.ViewModels;

namespace Vostok.Hotline.Gateway.Client.Statements.Requests.Handlers
{
	public class StatementSendEmailCommandHandler : IRequestHandler<StatementSendEmailCommand, StatusCommandViewModel>
	{
		private readonly StatementService _statementService;

		public StatementSendEmailCommandHandler(StatementService statementService)
		{
			_statementService = statementService;
		}

		public async Task<StatusCommandViewModel> Handle(StatementSendEmailCommand searchRequest, CancellationToken cancellationToken)
		{
			return await _statementService.SendEmailAsync(searchRequest, cancellationToken);
		}
	}
}
