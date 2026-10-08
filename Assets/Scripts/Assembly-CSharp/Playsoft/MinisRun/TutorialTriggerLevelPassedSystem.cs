using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialTriggerLevelPassedSystem : ReactiveSystem<StateEntity>, ISaveDataLoadedListener
	{
		private readonly StateContext stateContext;

		public TutorialTriggerLevelPassedSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			contexts.uI.CreateEntity().AddSaveDataLoadedListener(this);
		}

		public void OnDataLoaded()
		{
			int amount = stateContext.GetEntitiesWithName("UnlockedLevel").SingleEntity().integer.Amount;
			CheckTriggers(amount);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.NewLevelUnlocked.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return stateContext.tutorialEntity.isTutorialInitialized;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				int levelNumber = entity.newLevelUnlocked.LevelNumber;
				CheckTriggers(levelNumber);
			}
		}

		private void CheckTriggers(int levelNumber)
		{
			foreach (TutorialTrigger trigger in stateContext.tutorialEntity.tutorialTriggersList.Triggers)
			{
				TutorialLevelPassedTrigger tutorialLevelPassedTrigger = trigger as TutorialLevelPassedTrigger;
				TutorialLevelNotPassedTrigger tutorialLevelNotPassedTrigger = trigger as TutorialLevelNotPassedTrigger;
				if (tutorialLevelPassedTrigger != null)
				{
					tutorialLevelPassedTrigger.IsTriggered = tutorialLevelPassedTrigger.LevelId < levelNumber;
					if (tutorialLevelPassedTrigger.LevelId >= levelNumber)
					{
					}
				}
				else if (tutorialLevelNotPassedTrigger != null)
				{
					tutorialLevelNotPassedTrigger.IsTriggered = tutorialLevelNotPassedTrigger.LevelId >= levelNumber;
					if (tutorialLevelNotPassedTrigger.LevelId < levelNumber)
					{
					}
				}
			}
		}
	}
}
