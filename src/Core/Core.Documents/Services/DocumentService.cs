using System;
using System.Threading;
using System.Threading.Tasks;
using Razor.Templating.Core;
using Vostok.Hotline.Core.Documents.Abstractions;
using Vostok.Hotline.Core.Documents.Models.Responses;
using Vostok.Hotline.Core.Documents.Requests;

namespace Vostok.Hotline.Core.Documents.Services
{
	public class DocumentService
    {
		public async Task<RenderResponseModel> RenderAsync(RenderQuery<ITemplateViewModel> request, CancellationToken cancellationToken)
		{
			RenderResponseModel renderResponse = new RenderResponseModel();
			try
			{
				if(request.ViewData != null)
					renderResponse.Result = await RazorTemplateEngine.RenderAsync(request.TemplatePath, request.TokenModel, request.ViewData); 
				else
					renderResponse.Result = await RazorTemplateEngine.RenderAsync(request.TemplatePath, request.TokenModel);
			}
			catch (Exception ex)
			{
				renderResponse.ErrorMessage = ex.Message;
			}
			return renderResponse;
		}
	}
}
