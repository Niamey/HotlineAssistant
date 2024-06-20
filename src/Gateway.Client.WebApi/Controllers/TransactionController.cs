using System;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;
using Vostok.Hotline.Core.Common.Base.Controllers;
using Vostok.Hotline.Core.Common.Models.Responses;
using Vostok.Hotline.Core.Common.Models.Validations;
using Vostok.Hotline.Gateway.Client.Transactions.Requests.Queries;
using Vostok.Hotline.Gateway.Client.Transactions.ViewModels;

namespace Vostok.Hotline.Gateway.Client.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : MediatrControllerBase
	{
		public TransactionController(IServiceProvider provider)
			: base(provider)
		{
		}

		[HttpGet("List")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<TransactionViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetTransactionListAsync([BindRequired, FromQuery] TransactionListQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("List/Financial/ForBlocking")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<TransactionFinancialViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetTransactionListFinancialAsync([BindRequired, FromQuery] TransactionListFInancialBlockingQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("List/Financial/ForUnBlocking")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<TransactionFinancialViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetTransactionListFinancialAsync([BindRequired, FromQuery] TransactionListFinancialUnblockingQuery query)
			=> await ExecuteQueryAsync(query);

		[HttpGet("Reversal/List")]
		[Consumes(MediaTypeNames.Application.Json)]
		[ProducesResponseType(typeof(SearchRowsResponseRowModel<TransactionViewModel>), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ValidationResultModel), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ResponseFailedResultModel<ResponseErrorModel>), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetTransactionReversalListAsync([BindRequired, FromQuery] TransactionReversalListQuery query)
			=> await ExecuteQueryAsync(query);
	}
}
