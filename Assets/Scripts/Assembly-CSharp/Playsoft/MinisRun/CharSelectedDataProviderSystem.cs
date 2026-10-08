using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharSelectedDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public CharSelectedDataProviderSystem(Contexts contexts)
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
			return entity.isUICharSelectedRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("FirstCharSelectData");
			int newFirstCharId = 0;
			if (entitiesWithName.Count > 0)
			{
				StateEntity stateEntity = entitiesWithName.SingleEntity();
				newFirstCharId = stateEntity.integer.Amount;
			}
			entitiesWithName = stateContext.GetEntitiesWithName("SecondCharSelectData");
			int newSecondCharId = 1;
			if (entitiesWithName.Count > 0)
			{
				StateEntity stateEntity2 = entitiesWithName.SingleEntity();
				newSecondCharId = stateEntity2.integer.Amount;
			}
			foreach (UIEntity entity in entities)
			{
				entity.AddUICharSelectedResponse(newFirstCharId, newSecondCharId);
			}
		}
	}
}
