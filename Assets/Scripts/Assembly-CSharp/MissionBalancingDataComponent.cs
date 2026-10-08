using System.Collections.Generic;
using Entitas;

[State]
public sealed class MissionBalancingDataComponent : IComponent
{
	public Dictionary<int, MissionsBalancingData> Map;
}
