using System;
using System.Collections.Generic;
using System.Text;

namespace Vostok.Hotline.Api.Retail.Models.Classifiers
{
	public class ClassifierCollectionApiModel : List<ClassifierApiModel>
	{
		public ClassifierCollectionApiModel() : base() { 
		}

		public ClassifierCollectionApiModel(IList<ClassifierApiModel> list)
			   : base(list)
		{
		}

		public ClassifierCollectionApiModel(IEnumerable<ClassifierApiModel> list)
			: base(list)
		{
		}
	}
}
