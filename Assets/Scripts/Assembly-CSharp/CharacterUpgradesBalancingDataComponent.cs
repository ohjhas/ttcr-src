using System.Collections.Generic;
using Entitas;

[State]
public sealed class CharacterUpgradesBalancingDataComponent : IComponent
{
	public Dictionary<int, List<CharactersUpgradesBalancingData>> Map;
}
