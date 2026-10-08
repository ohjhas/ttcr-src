using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIChestListResponseComponent : IComponent
	{
		public List<ChestData> ChestDataList;
	}
}
