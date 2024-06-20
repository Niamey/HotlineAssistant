using Solar.Models.Models;
using System.Threading.Tasks;
using Vostok.HotLineAssistant.Common.Requests.Common;
using Vostok.HotLineAssistant.Common.Requests.Specific;
using Vostok.HotLineAssistant.Common.Response.Common;
using Vostok.HotlineAssistant.SolarCore;

namespace Vostok.HotLineAssistant.AccessorManager.Domain.Accessors
{
	public interface IAccessorService : IService
	{
		Task<ModelsResponse<Accessor>> GetAccessorById(ByIdRequest request);
		Task<ModelsResponse<Accessor>> GetAccessorByNumber(ByNumberRequest request);
		Task<ModelsResponse<Accessor>> GetAccessorByCardNumber(ByCardNumberRequest request);
		Task<ModelsResponse<Accessor>> GetAccessorByIBan(ByIBanRequest request);
	}
}