using Entitas;
using UnityEngine;

public class CloudsExecuteSystem : IExecuteSystem, ISystem
{
	private const string cloudPrefab = "LevelElements/Cloud";

	private const float endPos = 5f;

	private readonly float YPos;

	private readonly float cloudSize;

	private readonly GameContext gameContext;

	private readonly IGroup<GameEntity> cloudGroup;

	public CloudsExecuteSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		cloudGroup = gameContext.GetGroup(GameMatcher.Cloud);
		cloudSize = SettingsService.SettingsSO.CloudSize;
		YPos = SettingsService.SettingsSO.CloudsYPosition;
	}

	public void Execute()
	{
		GameEntity playerEntity = gameContext.playerEntity;
		if (playerEntity.hasView && (playerEntity.isGodMode || (playerEntity.hasOnClouds && !FinishOnDuration(playerEntity))))
		{
			GameEntity closestCloud = GetClosestCloud();
			SpawnClouds(playerEntity, closestCloud);
		}
	}

	private bool FinishOnDuration(GameEntity player)
	{
		player.onClouds.Duration -= Time.deltaTime;
		if (player.onClouds.Duration <= 0f)
		{
			player.RemoveOnClouds();
			return true;
		}
		return false;
	}

	private GameEntity GetClosestCloud()
	{
		GameEntity gameEntity = null;
		GameEntity[] entities = cloudGroup.GetEntities();
		foreach (GameEntity gameEntity2 in entities)
		{
			if (gameEntity == null || gameEntity2.position.Value.x > gameEntity.position.Value.x)
			{
				gameEntity = gameEntity2;
			}
		}
		return gameEntity;
	}

	private void SpawnClouds(GameEntity player, GameEntity closestCloud)
	{
		Vector3 position = player.view.gameObject.transform.position;
		float num = ((closestCloud == null) ? (position.x - cloudSize / 2f) : closestCloud.position.Value.x);
		float num2 = position.x + 5f;
		float num3 = num;
		while (num3 < num2)
		{
			num3 += cloudSize;
			GameEntity gameEntity = gameContext.CreateEntity();
			gameEntity.AddAsset("LevelElements/Cloud");
			gameEntity.AddPosition(new Vector3(num3, YPos, 0f));
			gameEntity.isCloud = true;
			gameEntity.isPoolable = true;
			gameEntity.isPlatform = true;
		}
	}
}
