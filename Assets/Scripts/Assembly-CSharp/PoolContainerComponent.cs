using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

[Game]
[Unique]
public sealed class PoolContainerComponent : IComponent
{
	public Dictionary<string, Stack<GameObject>> Pools;
}
