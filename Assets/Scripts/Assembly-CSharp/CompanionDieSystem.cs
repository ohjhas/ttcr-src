using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CompanionDieSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	public CompanionDieSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Dead);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isPlayer;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		GameEntity companionEntity = gameContext.companionEntity;
		if (companionEntity != null)
		{
			GameObject gameObject = companionEntity.view.gameObject;
			gameObject.GetComponent<Animator>().SetTrigger("Die");
		}
	}
}
