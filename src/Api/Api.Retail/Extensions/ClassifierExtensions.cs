using System;
using System.Collections.Generic;
using System.Linq;
using Vostok.Hotline.Api.Retail.Models.Classifiers;

namespace Vostok.Hotline.Api.Retail.Extensions
{
	public static class ClassifierExtensions
	{
		static readonly List<string> _locksCollection = new List<string> {
				"INACTIVE_AGREEMENT", "BLOCK_LIMIT_ARREST", "BLOCK_FULL_ARREST", "BLOCK_FM_DEBIT", "BLOCK_FM_CREDIT",
				"BLOCK_FM_FULL", "BLOCK_FM_IDENTIFICATION", "BLOCK_DECISION_BANK", "BLOCK_GRODI", "BLOCK_GNA",
				"BLOCK_TERRORIST", "BLOCK_PEP", "BLOCK_DEATH_CLIENT", "BLOCK_OVERDUE_LOAN", "BLOCK_CUSTOM",
				"BLOCK_RESIDENT_STATUS_CHANGE", "BLOCK_CLIENT_MISSIN_LISTS", "BLOCK_INVALID_PASSPORT"
			};

		public static ClassifierCollectionApiModel GetActiveLocks(this ClassifierCollectionApiModel classifiers)
		{
			var currentDateTime = DateTime.Now;
			var locks = classifiers.Where(i => _locksCollection.Any(x => x == i.Type.Code));
			var locksResult = locks.Where(i => i.Value.Code == "YES" && currentDateTime >= i.ValidFrom && (i.ValidTo == null || currentDateTime <= i.ValidTo));
			return new ClassifierCollectionApiModel(locksResult);
		}

		public static ClassifierApiModel GetActiveClassifier(this ClassifierCollectionApiModel classifiers, string classifierCode)
		{
			var currentDateTime = DateTime.Now;
			return classifiers.Where(i => i.Type.Code == classifierCode && i.Value.Code == "YES" && currentDateTime >= i.ValidFrom && (i.ValidTo == null || currentDateTime <= i.ValidTo)).FirstOrDefault();
		}
	}
}
