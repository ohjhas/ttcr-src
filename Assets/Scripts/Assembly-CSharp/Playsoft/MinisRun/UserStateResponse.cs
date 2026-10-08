using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class UserStateResponse
	{
		public uint responseCode;

		public ulong lastResponseTs;

		public UserStateData u;
	}
}
