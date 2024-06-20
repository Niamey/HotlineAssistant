using MediatR;
using System.Runtime.Serialization;
using Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Dtos;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AccessorManager.Application.Accessors.Queries
{
	[DataContract]
	public class AccessorByCardQuery : IRequest<ModelsResponse<AccessorDto>>
	{
		[DataMember]
		public string Card { get; set; }
	}
}