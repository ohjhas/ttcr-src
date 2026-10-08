using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerAbilityRunAnimSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public PlayerAbilityRunAnimSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.AbilityRun.AddedOrRemoved());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (!playerEntity.isDead && playerEntity.hasView)
			{
				Animator component = playerEntity.view.gameObject.GetComponent<Animator>();
				component.SetFloat("SpecialRun", (!playerEntity.isAbilityRun) ? 0f : 2f);
			}
		}
	}
}
