using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DailyMissionsDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public DailyMissionsDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUICurrentDailyMissionsRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			DailyMissionsCollection collection = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection.Collection;
			foreach (UIEntity entity in entities)
			{
				entity.AddUICurrentDailyMissionsResponse(collection);
			}
		}
	}
}
