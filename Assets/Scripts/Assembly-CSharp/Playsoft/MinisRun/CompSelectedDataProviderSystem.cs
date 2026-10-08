using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CompSelectedDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public CompSelectedDataProviderSystem(Contexts contexts)
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
			return entity.isUICompSelectedRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("CompSelectData");
			int newSelectedCompId = 0;
			bool newIsSelected = false;
			if (entitiesWithName.Count > 0)
			{
				StateEntity stateEntity = entitiesWithName.SingleEntity();
				newSelectedCompId = stateEntity.integer.Amount;
				newIsSelected = stateEntity.integer.Amount != -1;
			}
			foreach (UIEntity entity in entities)
			{
				entity.AddUICompSelectedResponse(newSelectedCompId, newIsSelected);
			}
		}
	}
}
