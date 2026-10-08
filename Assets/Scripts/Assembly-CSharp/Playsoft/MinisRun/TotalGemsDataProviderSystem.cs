using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TotalGemsDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public TotalGemsDataProviderSystem(Contexts contexts)
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
			return entity.isUITotalGemsRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
			foreach (UIEntity entity in entities)
			{
				entity.AddUITotalGemsResponse(stateEntity.integer.Amount);
			}
		}
	}
}
