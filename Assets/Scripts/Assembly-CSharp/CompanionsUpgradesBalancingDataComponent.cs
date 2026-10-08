using System.Collections.Generic;
using Entitas;

[State]
public sealed class CompanionsUpgradesBalancingDataComponent : IComponent
{
	public Dictionary<int, List<CompanionsUpgradesBalancingData>> Map;
}
