namespace Vostok.Hotline.Api.CRM.Responses.Enums
{
	/// <summary>
	/// Types of Access Methods
	/// </summary>
	public enum CounterpartSearchCodeCrmEnum
	{
		/// <summary>
		/// Undefined
		/// </summary>
		NONE = 0,

		/// <summary>
		/// Person id in the crm.
		/// </summary>
		PERSON_ID = 1,

		/// <summary>
		/// Solar id.
		/// </summary>
		SOLAR_ID = 2,

		/// <summary>
		/// Main Transaction Number???
		/// </summary>
		SR_ID = 3,

		/// <summary>
		/// SRBank code.
		/// </summary>
		SR_CODE = 4,

		///// <summary>
		///// Previous SRBank id.
		///// </summary>
		//P_SR_PREV_ID = 5,

		/// <summary>
		/// Financial phone number.
		/// </summary>
		P_FINANCIALPHONE = 6,

		/// <summary>
		/// Taxpayer identification number.
		/// </summary>
		IDENTCODE = 7,

		/// <summary>
		/// Passport series and number.
		/// </summary>
		//P_PSERIE = 8,

		/// <summary>
		/// Passport series and number.
		/// </summary>
		P_PNUMBER = 9,

		/// <summary>
		/// Full person name.
		/// </summary>
		FULLNAME = 10,

		/// <summary>
		/// Main account number
		/// </summary>
		IBAN = 14,

		/// <summary>
		/// Plastic card number.
		/// </summary>
		CARD = 15,

		/// <summary>
		/// Agreement number.
		/// </summary>
		AGREEMENT_NUMBER = 16,

		/// <summary>
		/// Counterpart id in the crm.
		/// </summary>
		COUNTERPART_ID = 17,

		/// <summary>
		/// Email
		/// </summary>
		EMAIL = 18
	}
}
