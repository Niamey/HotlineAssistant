namespace Vostok.Hotline.Core.Common.Enums.Transactions
{
	public enum TxnStatusEnum : byte
	{
		Undefined = UndefinedEnum.Undefined,

		/// <summary>
		/// Новая NW
		/// </summary>
		New = 1,

		/// <summary>
		/// Загружена LD
		/// </summary>
		Loaded = 2,

		/// <summary>
		/// Ошибка загрузки LE
		/// </summary>
		LoadError = 3,

		/// <summary>
		/// Подготовлена PR
		/// </summary>
		Prepared = 4,

		/// <summary>
		/// Обработана FD
		/// </summary>
		Finished = 5,

		/// <summary>
		/// Отклонена RJ
		/// </summary>
		Rejected = 6,

		/// <summary>
		/// Закрыта CD
		/// </summary>
		Closed = 7,

		/// <summary>
		/// Предобработана PP
		/// </summary>
		PreProcessing = 8,

		/// <summary>
		/// Переопределена SH
		/// </summary>
		Shaded = 9,

		/// <summary>
		/// Обработана как финансовая PF
		/// </summary>
		PreAppliedAsFin = 10
	}
}
