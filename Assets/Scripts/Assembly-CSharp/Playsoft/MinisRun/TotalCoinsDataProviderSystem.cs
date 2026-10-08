using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TotalCoinsDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public TotalCoinsDataProviderSystem(Contexts contexts)
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
			return entity.isUITotalCoinsRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
			foreach (UIEntity entity in entities)
			{
				entity.AddUITotalCoinsResponse(stateEntity.integer.Amount);
			}
		}
	}
}
