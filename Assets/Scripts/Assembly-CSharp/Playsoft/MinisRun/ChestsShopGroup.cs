using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ChestsShopGroup : GenericShopGroup<ShopChestsEntriesCollection>
	{
		public override ShopGroupType GroupType
		{
			get
			{
				return ShopGroupType.Chests;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (parameters == null)
			{
				return;
			}
			if (!parameters.hasUIChestListResponse)
			{
				Debug.LogWarning("[UI][Shop] Chests list response was not provided");
				return;
			}
			List<ChestData> dataList = parameters.uIChestListResponse.ChestDataList.FindAll((ChestData chest) => chest.IsVisibleInShop);
			SetChestDataList(dataList, parameters.uIFreeChestResponse.FreeChest);
		}

		private void SetChestDataList(List<ChestData> dataList, FreeChestData freeChestData)
		{
			entries.CreateEntriesIfNeeded(entriesParent, dataList.Count);
			entries.SetupEntries(dataList);
			entries.SetupEntriesState(dataList.Count, freeChestData, OnChestBuyPressed);
		}

		private void OnChestBuyPressed(int chestId)
		{
		}
	}
}
