using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIMissionListResponseComponent : IComponent
	{
		public List<WSLevelData> LevelDataList;
	}
}
