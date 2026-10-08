using System.Collections.Generic;
using Entitas;

[State]
public sealed class DailyMissionsBalancingDataComponent : IComponent
{
	public Dictionary<int, DailyMissionsBalancingData> Map;
}
