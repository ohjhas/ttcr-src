using System.Collections.Generic;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using Playsoft.MinisRun;

[Game]
[Unique]
public sealed class WorldComponent : IComponent
{
	public float Distance;

	public float Speed;

	public List<LevelElementData> levelElements;
}
