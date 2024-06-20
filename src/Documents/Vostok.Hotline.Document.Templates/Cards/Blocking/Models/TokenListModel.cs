using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Document.Templates.Cards.Blocking.Models
{
	public class TokenListModel: List<TokenModel>
	{
		public override string ToString()
		{
			if (this?.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				foreach (var item in this)
				{
					sb.AppendLine($"tokenId: { item.TokenId } wallet: { item.Wallet}");
				}

				return sb.ToString();
			}
			else
				return null;

		}
	}
}
