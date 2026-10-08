using System.Collections.Generic;
using Entitas;

[State]
public sealed class ShopSoftCurrencyPacksDataComponent : IComponent
{
	public Dictionary<int, ShopSoftCurrencyPacksBalancingData> Map;
}
