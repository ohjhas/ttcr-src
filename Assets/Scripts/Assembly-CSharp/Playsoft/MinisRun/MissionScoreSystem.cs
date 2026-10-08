using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MissionScoreSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		private IGroup<GameEntity> missionListeners;

		private bool isMissionActive;

		public MissionScoreSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			missionListeners = gameContext.GetGroup(GameMatcher.MissionUpdateListener);
		}

		public void Initialize()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			isMissionActive = worldEntity.hasMissionType && worldEntity.missionType.Type == MissionType.Get_Score;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasWorld;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Score);
		}

		protected override void Execute(List<GameEntity> entities)
		{
			if (isMissionActive)
			{
				RefreshUI();
			}
		}

		private void RefreshUI()
		{
			GameEntity[] entities = missionListeners.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				IMissionUpdateListener listener = gameEntity.missionUpdateListener.listener;
				GameEntity worldEntity = gameContext.worldEntity;
				int value = worldEntity.score.Value;
				worldEntity.ReplaceMissionCounter(value);
				listener.OnMissionUpdate(value, worldEntity.missionThresholds.FirstStar, worldEntity.missionThresholds.SecondStar, worldEntity.missionThresholds.ThirdStar);
			}
		}
	}
}
