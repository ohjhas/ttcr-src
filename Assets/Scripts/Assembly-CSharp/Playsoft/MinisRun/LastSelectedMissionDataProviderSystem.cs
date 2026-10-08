using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LastSelectedMissionDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public LastSelectedMissionDataProviderSystem(Contexts contexts)
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
			return entity.isUILastSelectedMissionRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("SelectedLevel").SingleEntity();
			foreach (UIEntity entity in entities)
			{
				entity.AddUILastSelectedMissionResponse(stateEntity.integer.Amount);
			}
		}
	}
}
