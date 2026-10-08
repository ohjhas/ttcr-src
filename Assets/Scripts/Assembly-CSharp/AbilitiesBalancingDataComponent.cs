using System.Collections.Generic;
using Entitas;

[State]
public sealed class AbilitiesBalancingDataComponent : IComponent
{
	public Dictionary<AbilityType, AbilitiesBalancingData> Map;
}
