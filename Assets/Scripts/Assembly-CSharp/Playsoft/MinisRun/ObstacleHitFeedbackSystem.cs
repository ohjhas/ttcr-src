using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public sealed class ObstacleHitFeedbackSystem : ReactiveSystem<InputEntity>
	{
		private GameContext gameContext;

		private IGroup<GameEntity> hitListeners;

		public ObstacleHitFeedbackSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
			hitListeners = gameContext.GetGroup(GameMatcher.ObstacleHitListener);
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
			foreach (InputEntity entity in entities)
			{
				GameEntity[] entities2 = hitListeners.GetEntities();
				foreach (GameEntity gameEntity in entities2)
				{
					gameEntity.obstacleHitListener.listener.UpdateView();
				}
			}
		}
	}
}
