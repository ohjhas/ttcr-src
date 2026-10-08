using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerHitAnimSystem : ReactiveSystem<InputEntity>
	{
		private GameContext gameContext;

		public PlayerHitAnimSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasCollisionFeedback;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.CollisionFeedback);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (!playerEntity.hasView || playerEntity.isDead)
			{
				return;
			}
			Animator component = playerEntity.view.gameObject.GetComponent<Animator>();
			foreach (InputEntity entity in entities)
			{
				component.SetTrigger("HitObstacle");
				component.SetFloat("HittingAnim", (!entity.collisionFeedback.TouchPointIsLow) ? 1f : 0f);
			}
		}
	}
}
