using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UICharListResponseComponent : IComponent
	{
		public List<CSCharacterData> CharDataList;
	}
}
