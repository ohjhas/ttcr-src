using Entitas;
using UnityEngine;

[Game]
public sealed class MovableDataComponent : IComponent
{
	public float StartThreshold;

	public float EndThreshold;

	public Vector2 MoveVector;
}
