using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DailyMissionUpgradeCharacterSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public DailyMissionUpgradeCharacterSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.UpgradeCharacter.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasUpgradeCharacter && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.DailyMissions);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			DailyMissionsCollectionComponent dailyMissionsCollection = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection;
			for (int i = 0; i < dailyMissionsCollection.Collection.DailyMissions.Length; i++)
			{
				if (dailyMissionsCollection.Collection.DailyMissions[i].MissionData.DAILYMISSIONTYPE.Equals(DailyMissionType.Upgrade_Character))
				{
					StateEntity stateEntity = stateContext.CreateEntity();
					stateEntity.isSavedData = true;
					stateEntity.AddName("DailyMissionDataSend");
					DailyMissionGroup dAILYMISSIONGROUP = dailyMissionsCollection.Collection.DailyMissions[i].MissionData.DAILYMISSIONGROUP;
					int currentProgress = dailyMissionsCollection.Collection.DailyMissions[i].CurrentProgress;
					stateEntity.AddDailyMissionProgressUpdate(dAILYMISSIONGROUP, currentProgress + entities.Count);
				}
			}
		}
	}
}
