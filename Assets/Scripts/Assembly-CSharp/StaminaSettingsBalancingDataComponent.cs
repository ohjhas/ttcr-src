using System.Collections.Generic;
using Entitas;

[State]
public sealed class StaminaSettingsBalancingDataComponent : IComponent
{
	public Dictionary<StaminaSettings, float> Map;
}
