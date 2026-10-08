using Entitas;
using UnityEngine;

[Game]
public sealed class SwingingDataComponent : IComponent
{
	public Vector2 Anchor;

	public Vector2 StartEndRotation;

	public float StartThreshold;

	public float EndThreshold;

	public float AverageSpeed;
}
