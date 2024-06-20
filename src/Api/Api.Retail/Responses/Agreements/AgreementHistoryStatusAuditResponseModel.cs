using System;
using Vostok.Hotline.Api.Retail.Responses.Agreements.Enums;

namespace Vostok.Hotline.Api.Retail.Responses.Agreements
{
	internal class AgreementHistoryStatusAuditResponseModel
	{

		/// <summary>
		/// Комментарий о причинах установки
		/// </summary>
		public string Comment { get; set; }
		/// <summary>
		/// Имя системы через которые производились правки, как пример BVR, Hotline
		/// </summary>
		public string SystemName { get; set; }
		/// <summary>
		/// Логин пользователя, тут может быть как доменная учетная запись например bank\m.lazorenko так и информация о пользователях ФИО
		/// </summary>
		public string UserLogin { get; set; }
		/// <summary>
		/// Является ли сотрудником банка или клиентом
		/// </summary>
		public bool IsEmployee { get; set; }
		/// <summary>
		/// Дата действия
		/// </summary>
		public DateTime ModifyDate { get; set; }
	}
}
