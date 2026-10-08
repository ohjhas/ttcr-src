using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CollectibleScoreSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	public CollectibleScoreSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasCollectibleScore;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Collected);
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			int value = gameContext.worldEntity.score.Value;
			float num = ((!gameContext.playerEntity.hasScoreMultiplier) ? 1f : gameContext.playerEntity.scoreMultiplier.Factor);
			float num2 = (gameContext.playerEntity.hasAbilityScoreBonus ? gameContext.playerEntity.abilityScoreBonus.Amount : 0);
			value += Mathf.RoundToInt((float)entity.collectibleScore.Value * num + num2);
			gameContext.worldEntity.ReplaceScore(value);
		}
	}
}
