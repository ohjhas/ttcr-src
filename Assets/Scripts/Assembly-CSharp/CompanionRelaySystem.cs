using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class CompanionRelaySystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	public CompanionRelaySystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.PlayerRelay);
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
			gameObject.GetComponent<Animator>().SetTrigger("Relay");
		}
	}
}
