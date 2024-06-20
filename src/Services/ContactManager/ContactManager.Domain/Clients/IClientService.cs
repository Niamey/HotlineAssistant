using System.Threading.Tasks;

namespace Vostok.HotLineAssistant.ContactManager.Domain.Clients
{
	public interface IClientService
	{
		Task<CounterPartResponseModel> ClientSearch(ClientSearchCriteria searchCriteria);
	}
}