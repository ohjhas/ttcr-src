using System.Runtime.InteropServices;

namespace Playsoft.MinisRun
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct PlayerLevelExperienceAmountData
	{
		public int Level { get; set; }

		public int ExpAmount { get; set; }
	}
}
