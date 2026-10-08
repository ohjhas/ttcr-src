using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshScoreSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		private readonly IGroup<GameEntity> scoreListeners;

		public RefreshScoreSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			scoreListeners = gameContext.GetGroup(GameMatcher.ScoreListener);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasScore;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Score.AddedOrRemoved());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			int value = gameContext.worldEntity.score.Value;
			GameEntity[] entities2 = scoreListeners.GetEntities();
			foreach (GameEntity gameEntity in entities2)
			{
				gameEntity.scoreListener.listener.ScoreChanged(value);
			}
		}
	}
}
