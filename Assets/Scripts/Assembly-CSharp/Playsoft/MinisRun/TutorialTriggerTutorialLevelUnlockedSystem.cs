using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialTriggerTutorialLevelUnlockedSystem : ReactiveSystem<StateEntity>, ISaveDataLoadedListener
	{
		private readonly StateContext stateContext;

		public TutorialTriggerTutorialLevelUnlockedSystem(Contexts contexts)
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
				TutorialTutorialLevelUnlockedTrigger tutorialTutorialLevelUnlockedTrigger = trigger as TutorialTutorialLevelUnlockedTrigger;
				if (tutorialTutorialLevelUnlockedTrigger != null && !(tutorialTutorialLevelUnlockedTrigger.IsTriggered = TutorialHelpers.IsTutorialLevelUnlocked(levelNumber, tutorialTutorialLevelUnlockedTrigger.TutorialLevel)))
				{
				}
			}
		}
	}
}
