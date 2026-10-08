using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class GatchaBalancingDataComponent : IComponent
{
	public Dictionary<int, ChestData> Map;
}
