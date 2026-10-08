using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerSpecialRunAnimSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public PlayerSpecialRunAnimSystem(Contexts contexts)
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
			return context.CreateCollector(GameMatcher.SpecialRun.AddedOrRemoved());
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
				if (entity.isSpecialRun)
				{
					component.SetFloat("SpecialRun", 1f);
					component.SetFloat("RunAnim", Mathf.Round(Random.Range(0f, 1f)));
				}
				else
				{
					component.SetFloat("SpecialRun", 0f);
					component.SetFloat("RunAnim", 0f);
				}
			}
		}
	}
}
