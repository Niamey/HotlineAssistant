using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Services.Abstractions;

namespace Vostok.Hotline.Api.Services.Managers
{
	public class StatementApiManager
	{
		private readonly IStatementApiService _statementApiService;

		public StatementApiManager(IStatementApiService statementApiService) {
			_statementApiService = statementApiService;
		}	

		public async Task<byte[]> GetStatementAsync(long agreementId, DateTime dateStart, DateTime dateEnd, CancellationToken cancellationToken)
		{
			var logout = await _statementApiService.AuthLogoutAsync(cancellationToken);
			var login = await _statementApiService.AuthLoginAsync(logout.XSRFToken, logout.JSessionId, cancellationToken);
			var genDocumentId = await _statementApiService.GenerateDocumentAsync(login.XSRFToken, login.JSessionId, agreementId, dateStart, dateEnd, cancellationToken);
			var getDocument = await _statementApiService.GetDocumentAsync(login.XSRFToken, login.JSessionId, agreementId, genDocumentId.Value, cancellationToken);

			return getDocument;
		}
	}
}
