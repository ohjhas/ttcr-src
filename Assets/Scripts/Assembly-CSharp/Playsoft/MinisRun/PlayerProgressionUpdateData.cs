using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class PlayerProgressionUpdateData
	{
		public int LastVisbileLevel;

		public int LastVisibleExp;

		public PlayerProgressionUpdateData(int lastVisibleLevel, int lastVisibleExp)
		{
			LastVisbileLevel = lastVisibleLevel;
			LastVisibleExp = lastVisibleExp;
		}
	}
}
