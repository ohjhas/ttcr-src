using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MissionCountTokenSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		private IGroup<GameEntity> missionListeners;

		private bool isMissionActive;

		public MissionCountTokenSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			missionListeners = gameContext.GetGroup(GameMatcher.MissionUpdateListener);
		}

		public void Initialize()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			isMissionActive = worldEntity.hasMissionType && worldEntity.hasMissionCollect;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasCollectibleData;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Collected);
		}

		protected override void Execute(List<GameEntity> entities)
		{
			if (isMissionActive)
			{
				CountTokens(entities);
				RefreshUI();
			}
		}

		private void CountTokens(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				if (entity.collectibleData.Type == gameContext.worldEntity.missionCollect.TokenType)
				{
					gameContext.worldEntity.missionCounter.Value++;
				}
			}
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
