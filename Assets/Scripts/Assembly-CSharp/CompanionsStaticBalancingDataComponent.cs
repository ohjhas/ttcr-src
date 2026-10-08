using System.Collections.Generic;
using Entitas;

[State]
public sealed class CompanionsStaticBalancingDataComponent : IComponent
{
	public Dictionary<int, CompanionsStaticBalancingData> Map;
}
