namespace Vostok.Hotline.Api.Retail.Responses.Agreements.Enums
{
	internal enum AgreementCreditPeriodTypeRetailEnum: sbyte
	{
		/// <summary>
		/// Пользователю еще не предложен кредитный лимит вообще/Есть кредитный лимит, не пользуется
		/// </summary>
		NotUsed = -2,
		/// <summary>
		/// нет (есть просрочка),
		/// </summary>
		NotInGracePeriodWithDebt = -1,
		/// <summary>
		/// нет(нет просрочки)
		/// </summary>
		NotInGracePeriod = 0,
		/// <summary>
		/// 1-й месяц в льготе
		/// </summary>
		InGracePeriodFirstMonth = 1,
		/// <summary>
		/// 2-й месяц в льготе
		/// </summary>
		InGracePeriodSecondMonth = 2
	}
}
