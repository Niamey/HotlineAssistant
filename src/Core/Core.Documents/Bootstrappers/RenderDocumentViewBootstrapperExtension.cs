using Microsoft.Extensions.DependencyInjection;
using Vostok.Hotline.Core.Documents.Services;

namespace Vostok.Hotline.Core.Documents.Bootstrappers
{
	public static class RenderDocumentViewBootstrapperExtension
	{
		public static void AddDocumentGenerationRules(this IServiceCollection services)
		{
			services.AddTransient<DocumentService>();
			services.AddRazorTemplating();
		}
	}
}
