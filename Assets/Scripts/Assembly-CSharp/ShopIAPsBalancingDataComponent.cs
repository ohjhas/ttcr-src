using System.Collections.Generic;
using Entitas;

[State]
public sealed class ShopIAPsBalancingDataComponent : IComponent
{
	public Dictionary<int, ShopIAPsBalancingData> Map;
}
