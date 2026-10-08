using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DailyMissionResultSendSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext gameContext;

		public DailyMissionResultSendSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(InputEntity entity)
		{
			return (entity.isLevelEndAnimationFinished || entity.isPlayerLost) && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.DailyMissions);
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.LevelEndAnimationFinished.Added(), InputMatcher.PlayerLost.Added());
		}

		protected override void Execute(List<InputEntity> entities)
		{
			UpdateDailyMissionsProgress(gameContext.worldEntity, entities[0].isLevelEndAnimationFinished);
		}

		private void UpdateDailyMissionsProgress(GameEntity world, bool updatePlayedMissions)
		{
			DailyMissionsCollectionComponent dailyMissionsCollection = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection;
			if (world.hasDailyMissionCollect)
			{
				foreach (KeyValuePair<CollectibleType, KeyValuePair<DailyMissionGroup, int>> item in world.dailyMissionCollect.MissionDict)
				{
					StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
					stateEntity.isSavedData = true;
					stateEntity.AddName("DailyMissionDataSend");
					int currentProgress = dailyMissionsCollection.Collection.DailyMissions[(int)item.Value.Key].CurrentProgress;
					stateEntity.AddDailyMissionProgressUpdate(item.Value.Key, currentProgress + item.Value.Value);
				}
			}
			if (world.hasDailyMissionDestroyObstacle)
			{
				StateEntity stateEntity2 = Contexts.sharedInstance.state.CreateEntity();
				stateEntity2.isSavedData = true;
				stateEntity2.AddName("DailyMissionDataSend");
				int currentProgress2 = dailyMissionsCollection.Collection.DailyMissions[(int)world.dailyMissionDestroyObstacle.Group].CurrentProgress;
				stateEntity2.AddDailyMissionProgressUpdate(world.dailyMissionDestroyObstacle.Group, currentProgress2 + world.dailyMissionDestroyObstacle.Value);
			}
			if (world.hasDailyMissionPlayMissions && updatePlayedMissions)
			{
				StateEntity stateEntity3 = Contexts.sharedInstance.state.CreateEntity();
				stateEntity3.isSavedData = true;
				stateEntity3.AddName("DailyMissionDataSend");
				int currentProgress3 = dailyMissionsCollection.Collection.DailyMissions[(int)world.dailyMissionPlayMissions.Group].CurrentProgress;
				stateEntity3.AddDailyMissionProgressUpdate(world.dailyMissionPlayMissions.Group, currentProgress3 + 1);
			}
		}
	}
}
