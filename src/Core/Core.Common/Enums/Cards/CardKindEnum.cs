namespace Vostok.Hotline.Core.Common.Enums.Cards
{
	/// <summary>
	/// Вид карты
	/// </summary>
	public enum CardKindEnum : byte
	{
		Undefined = UndefinedEnum.Undefined,
		
		DebitWorldBVR,
		MagstripePredProd516818,
		MCMagstripe,
		DebitWorldNoMagstripe
	}
}
