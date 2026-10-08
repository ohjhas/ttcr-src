using System.Collections.Generic;
using Entitas;

[State]
public sealed class MissionChestsBalancingDataComponent : IComponent
{
	public Dictionary<int, MissionChestsBalancingData> Map;
}
