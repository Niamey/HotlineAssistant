using System.Text.Json.Serialization;

namespace Vostok.Hotline.Core.Common.Enums.Agreements
{
	public enum AgreementStatusEnum : byte
	{
		Undefined = UndefinedEnum.Undefined,
		/// <summary>
		/// Активный
		/// </summary>
		Active = 1,
		/// <summary>
		/// Приостановленный
		/// </summary>
		Suspended = 2,
		/// <summary>
		/// Замороженный
		/// </summary>
		Frozen = 3,
		/// <summary>
		/// Закрытый
		/// </summary>
		Closed = 4,
		/// <summary>
		/// Списан за счет резерва
		/// </summary>
		Reserve = 5
	}
}
