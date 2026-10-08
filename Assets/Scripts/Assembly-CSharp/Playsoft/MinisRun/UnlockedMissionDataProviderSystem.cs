using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UnlockedMissionDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public UnlockedMissionDataProviderSystem(Contexts contexts)
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
			return entity.isUIUnlockedMissionRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("UnlockedLevel").SingleEntity();
			foreach (UIEntity entity in entities)
			{
				entity.AddUIUnlockedMissionResponse(stateEntity.integer.Amount);
			}
		}
	}
}
