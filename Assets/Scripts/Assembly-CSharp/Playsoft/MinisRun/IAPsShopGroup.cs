using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class IAPsShopGroup : GenericShopGroup<ShopIAPsEntriesCollection>
	{
		public override ShopGroupType GroupType
		{
			get
			{
				return ShopGroupType.HardCurrency;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (parameters != null)
			{
				if (!parameters.hasUIShopIAPsListResponse)
				{
					Debug.LogWarning("[UI][Shop] Chests list response was not provided");
				}
				else
				{
					SetEntriesData(parameters.uIShopIAPsListResponse.IAPsDataList);
				}
			}
		}

		private void SetEntriesData(List<IAPsData> dataList)
		{
			entries.CreateEntriesIfNeeded(entriesParent, dataList.Count);
			entries.SetupEntries(dataList);
			entries.SetupEntriesState(OnChestBuyPressed);
		}

		private void OnChestBuyPressed(int chestId)
		{
		}
	}
}
