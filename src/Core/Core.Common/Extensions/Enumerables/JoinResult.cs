namespace Vostok.Hotline.Core.Common.Extensions.Enumerables
{
	public class JoinResult<TL, TR>
	{
		public TL Outer { get; set; }

		public TR Inner { get; set; }
	}
}
