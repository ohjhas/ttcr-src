using Entitas;

namespace Playsoft.MinisRun
{
	public class MissionStaminaSystem : IExecuteSystem, IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		private IGroup<GameEntity> missionListeners;

		private bool isMissionActive;

		public MissionStaminaSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			missionListeners = gameContext.GetGroup(GameMatcher.MissionUpdateListener);
		}

		public void Initialize()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			isMissionActive = worldEntity.hasMissionType && worldEntity.missionType.Type == MissionType.Have_Stamina;
		}

		public void Execute()
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
				GameEntity playerEntity = gameContext.playerEntity;
				GameEntity worldEntity = gameContext.worldEntity;
				float num = playerEntity.stamina.Value / SettingsService.SettingsSO.StartingStamina * 100f;
				worldEntity.ReplaceMissionCounter((int)num);
				listener.OnMissionUpdate((int)num, worldEntity.missionThresholds.FirstStar, worldEntity.missionThresholds.SecondStar, worldEntity.missionThresholds.ThirdStar);
			}
		}
	}
}
