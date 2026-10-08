using System.Collections.Generic;
using Entitas;

public sealed class AddSparklesSystem : ReactiveSystem<GameEntity>
{
	private GameContext gameContext;

	public AddSparklesSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Destroyed);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isCollectible && entity.isCollected && entity.hasSparkle;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			GameEntity gameEntity = gameContext.CreateEntity();
			gameEntity.AddAsset(entity.sparkle.AssetName);
			gameEntity.AddPosition(entity.position.Value);
			gameEntity.isMovable = true;
			gameEntity.isPoolable = true;
		}
	}
}
