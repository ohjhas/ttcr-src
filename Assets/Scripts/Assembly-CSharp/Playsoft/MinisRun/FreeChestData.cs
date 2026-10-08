using System;
using System.Runtime.InteropServices;

namespace Playsoft.MinisRun
{
	[StructLayout(LayoutKind.Sequential, Size = 1)]
	public struct FreeChestData
	{
		public bool IsLocked { get; set; }

		public DateTime UnlockTime { get; set; }
	}
}
