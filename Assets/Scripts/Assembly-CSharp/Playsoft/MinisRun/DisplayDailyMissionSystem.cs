using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DisplayDailyMissionSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public DisplayDailyMissionSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIOpenDailyMissions;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				OpenDailyMissions();
				stateContext.CreateEntity().AddPassLevelAction(TutorialOutgameLevel.DailyMissions);
				entity.isDestroyed = true;
			}
		}

		private void OpenDailyMissions()
		{
			HashSet<StateEntity> entitiesWithName = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionData");
			if (entitiesWithName.Count > 0)
			{
				uiContext.OpenDialog<DailyMissionsScreen>();
				uiContext.RefreshScreen();
			}
		}
	}
}
