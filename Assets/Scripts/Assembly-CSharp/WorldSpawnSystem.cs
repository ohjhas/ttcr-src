using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

public class WorldSpawnSystem : IExecuteSystem, ISystem
{
	private readonly GameContext gameContext;

	private const float spawnRightDistance = 30f;

	private GameEntity world;

	private bool isFirstFrame = true;

	public WorldSpawnSystem(Contexts contexts)
	{
		gameContext = contexts.game;
	}

	public void Execute()
	{
		world = gameContext.worldEntity;
		SpawnLevelElements();
		if (isFirstFrame)
		{
			isFirstFrame = false;
		}
	}

	private void SpawnLevelElements()
	{
		float distance = world.world.Distance;
		List<LevelElementData> list = new List<LevelElementData>();
		foreach (LevelElementData levelElement in world.world.levelElements)
		{
			if (levelElement.Position.x <= distance + 30f)
			{
				gameContext.CreateLevelElement(levelElement, isFirstFrame);
				list.Add(levelElement);
			}
		}
		foreach (LevelElementData item in list)
		{
			world.world.levelElements.Remove(item);
		}
	}
}
