using MediatR;
using System.Runtime.Serialization;
using Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Dtos;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Queries
{
	[DataContract]
	public class AccessorByIbanQuery : IRequest<ModelsResponse<AccessorDto>>
	{
		[DataMember]
		public string Iban { get; set; }
	}
}