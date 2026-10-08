using System.Runtime.InteropServices;

namespace Playsoft.MinisRun
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct WSLevelData
	{
		public string Name { get; set; }

		public int Highscore { get; set; }

		public int Stars { get; set; }

		public int ResultValue { get; set; }

		public int Chapter { get; set; }

		public MissionType MissionType { get; set; }

		public int[] ItemsToCollect { get; set; }

		public int ChestRewardId { get; set; }

		public bool ChestCollected { get; set; }
	}
}
