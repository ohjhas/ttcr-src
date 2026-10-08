using System.Collections.Generic;
using Entitas;

[State]
public sealed class ChapterBalancingDataComponent : IComponent
{
	public Dictionary<int, ChaptersBalancingData> Map;
}
