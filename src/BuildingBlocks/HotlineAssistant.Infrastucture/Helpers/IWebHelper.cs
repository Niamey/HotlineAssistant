using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.Infrastucture.Helpers
{
	public interface IWebHelper
	{
		Task<string> GetAsync(string url);
		Task<string> PostAsync(string url, string jsonData);
	}
}