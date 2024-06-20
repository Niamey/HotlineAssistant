using System.Collections.Generic;
using System.Runtime.Serialization;
using MediatR;
using Solar.Models.Models;

namespace Vostok.HotLineAssistant.ContactManager.Application.ClientBalances.Queries
{
	[DataContract]
	public class ClientBalancesByIbanQuery : IRequest<IEnumerable<Balance>>
	{
		[DataMember]
		public string Iban { get; set; }
	}
}