using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class PlayerLevelBalancingDataComponent : IComponent
{
	public Dictionary<int, PlayerLevelData> Map;
}
