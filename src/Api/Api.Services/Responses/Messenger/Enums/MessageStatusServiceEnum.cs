namespace Vostok.Hotline.Api.Services.Responses.Messenger.Enums
{
	public enum MessageStatusServiceEnum
	{
		/// <summary>
		/// Повідомлення створено
		/// </summary>
		CREATED = 1,
		/// <summary>
		/// Повідомлення відправлено
		/// </summary>
		SENT = 2,
		/// <summary>
		/// При створенні або надсиланні повідомлення виникла помилка
		/// </summary>
		ERROR = 3,
		/// <summary>
		/// При створенні або надсиланні повідомлення виникла внутрішня помилка сервісу
		/// </summary>
		EXCEPTION = 4,
		/// <summary>
		/// Повідомлення не відправлено
		/// </summary>
		NOTSENT = 5,
		/// <summary>
		/// Повідомлення в процесі доставки
		/// </summary>
		DELIVERING = 7,
		/// <summary>
		/// Повідомлення доставлено
		/// </summary>
		DELIVERED = 9,
		/// <summary>
		/// Повідомлення, яке не вдалося доставити
		/// </summary>
		UNDELIV = 10
	}
}
