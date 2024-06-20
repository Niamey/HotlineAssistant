using System;
using System.Threading;
using System.Threading.Tasks;
using Vostok.Hotline.Api.Base.Models.Responses.Commands;
using Vostok.Hotline.Api.Services.Models.Statement;

namespace Vostok.Hotline.Api.Services.Abstractions
{
	public interface IStatementApiService
	{
		Task<AuthLogoutApiModel> AuthLogoutAsync(CancellationToken cancellationToken);
		Task<AuthLoginApiModel> AuthLoginAsync(string xsrfToken, string jSessionId, CancellationToken cancellationToken);
		Task<long?> GenerateDocumentAsync(string xsrfToken, string jSessionId, long agreementId, DateTime dateStart, DateTime dateEnd, CancellationToken cancellationToken);
		Task<byte[]> GetDocumentAsync(string xsrfToken, string jSessionId, long agreementId, long genId, CancellationToken cancellationToken);
	}
}
