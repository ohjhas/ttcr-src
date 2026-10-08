using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MissionDestroyObstaclesSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		private IGroup<GameEntity> missionListeners;

		private bool isMissionActive;

		public MissionDestroyObstaclesSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			missionListeners = gameContext.GetGroup(GameMatcher.MissionUpdateListener);
		}

		public void Initialize()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			isMissionActive = worldEntity.hasMissionType && worldEntity.missionType.Type == MissionType.Destroy_Obstacles;
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
				RefreshUI();
			}
		}

		private void CountObstacles(List<GameEntity> entities)
		{
			gameContext.worldEntity.missionCounter.Value += entities.Count;
		}

		private void RefreshUI()
		{
			GameEntity[] entities = missionListeners.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				IMissionUpdateListener listener = gameEntity.missionUpdateListener.listener;
				GameEntity worldEntity = gameContext.worldEntity;
				listener.OnMissionUpdate(worldEntity.missionCounter.Value, worldEntity.missionThresholds.FirstStar, worldEntity.missionThresholds.SecondStar, worldEntity.missionThresholds.ThirdStar);
			}
		}
	}
}
