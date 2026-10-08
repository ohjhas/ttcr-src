using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialNotifyLevelUnlockedSystem : ReactiveSystem<UIEntity>, IInitializeSystem, ISystem
	{
		private readonly IGroup<UIEntity> levelUnlockedListeners;

		private readonly UIContext uiContext;

		public TutorialNotifyLevelUnlockedSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
			levelUnlockedListeners = uiContext.GetGroup(UIMatcher.TutorialLevelPassedAvailability);
		}

		public void Initialize()
		{
			NotifyLevelUnlocks();
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.TutorialLevelPassedAvailability.Added());
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			NotifyLevelUnlocks();
		}

		private void NotifyLevelUnlocks()
		{
			List<TutorialOutgameLevel> list = new List<TutorialOutgameLevel>();
			foreach (object value in Enum.GetValues(typeof(TutorialOutgameLevel)))
			{
				if (TutorialHelpers.IsTutorialLevelUnlocked((TutorialOutgameLevel)value))
				{
					list.Add((TutorialOutgameLevel)value);
				}
			}
			UIEntity[] entities = levelUnlockedListeners.GetEntities();
			foreach (UIEntity uIEntity in entities)
			{
				uIEntity.tutorialLevelPassedAvailability.Listener.LevelUnlocked(list);
			}
		}
	}
}
