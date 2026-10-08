using Playsoft.MinisRun;

public static class GameExtensions
{
	public static GameEntity CreateLevelElement(this GameContext context, LevelElementData mapData, bool isAsyncLoad = false)
	{
		GameEntity gameEntity = context.CreateEntity();
		gameEntity.isMovable = true;
		gameEntity.isPoolable = true;
		gameEntity.AddAsset("LevelElements/" + mapData.AssetName);
		gameEntity.isLoadAssetAsync = isAsyncLoad;
		gameEntity.AddPosition(mapData.Position);
		mapData.Platform.AddToEntity(gameEntity);
		mapData.Obstacle.AddToEntity(gameEntity);
		mapData.Collectible.AddToEntity(gameEntity);
		mapData.Trigger.AddToEntity(gameEntity);
		return gameEntity;
	}
}
