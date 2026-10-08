using System.Collections.Generic;
using Entitas;

public sealed class CollectibleCoinSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	private readonly IGroup<GameEntity> coinListeners;

	public CollectibleCoinSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
		coinListeners = gameContext.GetGroup(GameMatcher.CoinsListener);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasCollectibleCoin;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Collected);
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			int value = gameContext.worldEntity.coins.Value;
			value += entity.collectibleCoin.Value;
			gameContext.worldEntity.ReplaceCoins(value);
			GameEntity[] entities2 = coinListeners.GetEntities();
			foreach (GameEntity gameEntity in entities2)
			{
				gameEntity.coinsListener.listener.CoinsChanged(value);
			}
		}
	}
}
