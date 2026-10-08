using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class SoftCurrencyShopGroup : GenericShopGroup<ShopSoftCurrencyEntriesCollection>
	{
		public override ShopGroupType GroupType
		{
			get
			{
				return ShopGroupType.SoftCurrency;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (parameters != null)
			{
				if (!parameters.hasUIShopSoftCurrencyPacksResponse)
				{
					Debug.LogWarning("[UI][Shop] Soft currency packs list response was not provided");
				}
				else
				{
					SetEntriesData(parameters.uIShopSoftCurrencyPacksResponse.SoftCurrencyPacksDataList);
				}
			}
		}

		private void SetEntriesData(List<SoftCurrencyPacksData> packsData)
		{
			entries.CreateEntriesIfNeeded(entriesParent, packsData.Count);
			entries.SetupEntries(packsData);
			entries.SetupEntriesState(packsData.Count);
		}
	}
}
