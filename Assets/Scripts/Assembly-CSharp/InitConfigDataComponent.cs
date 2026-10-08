using System.Collections.Generic;
using Entitas;

[State]
public sealed class InitConfigDataComponent : IComponent
{
	public Dictionary<EnumSettings, int> Map;
}
