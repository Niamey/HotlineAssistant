using System.Text;

namespace Vostok.Hotline.Gateway.Client.Counterpart.Models
{
	public class CounterpartAddressModel
	{
		public string RegionName { get; set; }
		public string DistrictName { get; set; }
		public string CityName { get; set; }
		public string StreetName { get; set; }
		public string HouseNumber { get; set; }
		public string HouseNumberAdd { get; set; }
		public string PostIndex { get; set; }
		public string Flat { get; set; }
		public string Korp { get; set; }

		public CounterpartAddressModel()
		{
		}

		public string GetAddress()
		{
			StringBuilder address = new StringBuilder(CityName);

			if (!string.IsNullOrEmpty(DistrictName))
			{
				if (address.Length > 0)
					address.Append(" ");

				address.Append(DistrictName);
			}

			if (!string.IsNullOrEmpty(RegionName))
			{
				if (address.Length > 0)
					address.Append(" ");

				address.Append(RegionName);
			}

			if (!string.IsNullOrEmpty(StreetName))
			{
				if (address.Length > 0)
					address.Append(" ");

				address.Append(StreetName);
			}

			if (!string.IsNullOrEmpty(HouseNumber))
			{
				if (address.Length > 0)
					address.Append(" ");

				address.Append(HouseNumber);

				if (!string.IsNullOrEmpty(HouseNumberAdd))
				{
					address.Append(HouseNumberAdd);
				}
			}

			if (!string.IsNullOrEmpty(Flat))
			{
				if (address.Length > 0)
					address.Append(" ");

				address.Append(Flat);
			}

			return address.ToString();
		}
	}
}
