using System.Collections.Generic;
using Entitas;

[State]
public sealed class RestoreSettingsBalancingDataComponent : IComponent
{
	public Dictionary<RestoreSettings, int> Map;
}
