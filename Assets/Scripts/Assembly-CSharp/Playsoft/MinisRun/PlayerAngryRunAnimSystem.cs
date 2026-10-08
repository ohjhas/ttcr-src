using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerAngryRunAnimSystem : ReactiveSystem<GameEntity>
	{
		private const string ANGRY_RUN_BOOL = "AngryRun";

		private readonly GameContext gameContext;

		public PlayerAngryRunAnimSystem(Contexts contexts)
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
			return context.CreateCollector(GameMatcher.AngryRun.AddedOrRemoved());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (playerEntity.isDead || !playerEntity.hasView)
			{
				return;
			}
			Animator component = playerEntity.view.gameObject.GetComponent<Animator>();
			foreach (GameEntity entity in entities)
			{
				component.SetBool("AngryRun", entity.isAngryRun);
			}
		}
	}
}
