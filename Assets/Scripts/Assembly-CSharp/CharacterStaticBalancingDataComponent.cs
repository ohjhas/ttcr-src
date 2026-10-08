using System.Collections.Generic;
using Entitas;

[State]
public sealed class CharacterStaticBalancingDataComponent : IComponent
{
	public Dictionary<int, CharactersStaticBalancingData> Map;
}
