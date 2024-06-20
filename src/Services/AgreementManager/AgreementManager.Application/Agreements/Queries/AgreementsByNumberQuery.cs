using MediatR;
using System.Runtime.Serialization;
using Vostok.HotLineAssistant.AgreementManager.Application.Agreement.Dtos;
using Vostok.HotLineAssistant.Common.Response.Common;

namespace Vostok.HotLineAssistant.AgreementManager.Application.Agreements.Queries
{
	[DataContract]
	public class AgreementsByNumberQuery : IRequest<ModelsResponse<AgreementDto>>
	{
		[DataMember]
		public string Number { get; set; }
	}
}