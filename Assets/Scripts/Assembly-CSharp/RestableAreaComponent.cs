using Entitas;
using UnityEngine;

[Game]
public sealed class RestableAreaComponent : IComponent
{
	public Vector2 MoveVector;

	public bool MovingFinished;
}
