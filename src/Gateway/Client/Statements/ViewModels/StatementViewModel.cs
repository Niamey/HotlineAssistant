using System;
using Vostok.Hotline.Core.Common.Base.Abstractions.Responses;

namespace Vostok.Hotline.Gateway.Client.Statements.ViewModels
{
	public class StatementViewModel : ResponseBaseModel
	{
		public byte[] Data { get; set; }
	}

}