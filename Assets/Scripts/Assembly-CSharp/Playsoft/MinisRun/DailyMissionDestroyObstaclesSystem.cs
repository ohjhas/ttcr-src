using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DailyMissionDestroyObstaclesSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		private bool isMissionActive;

		public DailyMissionDestroyObstaclesSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		public void Initialize()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			isMissionActive = worldEntity.hasDailyMissionDestroyObstacle;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isFlyAway;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.FlyAway.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			if (isMissionActive)
			{
				CountObstacles(entities);
			}
		}

		private void CountObstacles(List<GameEntity> entities)
		{
			gameContext.worldEntity.dailyMissionDestroyObstacle.Value += entities.Count;
		}
	}
}
