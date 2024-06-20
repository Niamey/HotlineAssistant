using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Vostok.HotLineAssistant.ContactManager.Application.Client.Dtos;

namespace Vostok.HotLineAssistant.ContactManager.Application.Client.Queries
{
	[DataContract]
	public class ClientSearchQuery : IRequest<PagedResponseEx<ClientDto>>
	{
		[DataMember]
		[Required]
		public string Code { get; set; }

		[DataMember]
		[Required]
		public string Criteria { get; set; }

		[DataMember]
		public string Type { get; set; }

		[DataMember]
		public int? PageNumber { get; set; }

		[DataMember]
		public int? PageSize { get; set; }

		[DataMember]
		public string SortColumn { get; set; }

		[DataMember]
		public SortDirection? SortDirection { get; set; }
	}

	public enum SortDirection
	{
		Ascending = 0,
		Descending = 1
	}
}