using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Gateway.Client.Travels.ViewModels
{
	public class TravelCountryViewModel
	{
		public string CountryCode { get; set; }

		public string CountryName { get; set; }

		public string CountryA2 { get; set; }

		public bool IsRisky { get; set; }
	}
}
