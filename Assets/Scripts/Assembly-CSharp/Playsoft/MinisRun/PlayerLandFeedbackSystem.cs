using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayerLandFeedbackSystem : ReactiveSystem<InputEntity>
	{
		private GameContext gameContext;

		private IGroup<GameEntity> landListeners;

		public PlayerLandFeedbackSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
			landListeners = gameContext.GetGroup(GameMatcher.PlayerLandListener);
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isLanding;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Landing);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				if (entity.isLanding && gameContext.playerEntity.hasSizeIncreased)
				{
					GameEntity[] entities2 = landListeners.GetEntities();
					foreach (GameEntity gameEntity in entities2)
					{
						gameEntity.playerLandListener.listener.OnPlayerLand();
					}
				}
			}
		}
	}
}
