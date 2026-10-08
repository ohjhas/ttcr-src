using System.Runtime.InteropServices;

namespace Playsoft.MinisRun
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct ChestRewardData
	{
		public GroupType Group { get; set; }

		public ItemType Type { get; set; }

		public Rarity Rarity { get; set; }

		public int Index { get; set; }

		public float Probability { get; set; }

		public int AmountMin { get; set; }

		public int AmountMax { get; set; }
	}
}
