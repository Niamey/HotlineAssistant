using System;
using System.Collections.Generic;
using System.Text;
using Vostok.Hotline.Api.Services.Enums.MDES;
using Vostok.Hotline.Core.Common.Enums.MDES;

namespace Vostok.Hotline.Api.Services.Mapper.MDES
{
	public static class PaymentSystemTypeMapperExtensions
	{
		public static PaymentSystemTypeServiceEnum ToPaymentSystemTypeServiceEnum(this PaymentSystemTypeMdesEnum response)
			=> response switch
			{
				PaymentSystemTypeMdesEnum.MasterCard => PaymentSystemTypeServiceEnum.MC,
				PaymentSystemTypeMdesEnum.Visa => PaymentSystemTypeServiceEnum.V
			};
	}
}
