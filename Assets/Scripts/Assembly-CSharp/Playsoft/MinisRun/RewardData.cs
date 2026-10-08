using System.Runtime.InteropServices;

namespace Playsoft.MinisRun
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct RewardData
	{
		public ItemType Type { get; set; }

		public Rarity Rarity { get; set; }

		public int Index { get; set; }

		public int Amount { get; set; }
	}
}
