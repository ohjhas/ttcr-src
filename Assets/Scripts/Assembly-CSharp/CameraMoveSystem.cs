using Entitas;
using UnityEngine;

public class CameraMoveSystem : IExecuteSystem, ISystem
{
	private readonly GameContext gameContext;

	private readonly Transform MovingTransform;

	public CameraMoveSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		MovingTransform = Camera.main.transform.parent;
	}

	public void Execute()
	{
		GameEntity worldEntity = gameContext.worldEntity;
		Vector3 position = MovingTransform.position;
		position.x = worldEntity.world.Distance;
		MovingTransform.position = position;
	}
}
