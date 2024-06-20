namespace Vostok.Hotline.Api.CRM.Responses.Enums
{
	public enum CounterpartSearchTypeCrmEnum
	{
		/// <summary>
		/// Undefined counterpart.
		/// </summary>
		None = 0,

		/// <summary>
		/// Opened counterpart.
		/// </summary>
		Opened = 1,

		/// <summary>
		/// Closed counterpart.
		/// </summary>
		Closed = 2,

		/// <summary>
		/// Dummy counterpart (without SrId & IsCardId).
		/// </summary>
		Dummy = 4,
	}
}
