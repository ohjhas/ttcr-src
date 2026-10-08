using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UICompanionListResponseComponent : IComponent
	{
		public List<CSCompanionData> CompDataList;
	}
}
