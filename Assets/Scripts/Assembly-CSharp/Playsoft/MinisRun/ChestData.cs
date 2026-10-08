using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Playsoft.MinisRun
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ChestData
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public string ContentDescription { get; set; }

		public string AssetName { get; set; }

		public ItemType ItemType { get; set; }

		public Rarity Rarity { get; set; }

		public bool IsVisibleInShop { get; set; }

		public int InitialWaitTime { get; set; }

		public int WaitTime { get; set; }

		public int PriceSC { get; set; }

		public int PriceHC { get; set; }

		public int MinPartsAmount { get; set; }

		public int MaxPartsAmount { get; set; }

		public List<ChestRewardData> RewardsData { get; set; }
	}
}
