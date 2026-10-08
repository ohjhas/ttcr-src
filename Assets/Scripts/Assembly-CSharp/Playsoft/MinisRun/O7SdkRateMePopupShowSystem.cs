using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkRateMePopupShowSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		private readonly StateContext stateContext;

		public O7SdkRateMePopupShowSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
			uiContext = context.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkRateMe.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkRateMe;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkRateMe)
				{
					StateEntity stateEntity = stateContext.GetEntitiesWithName("RateMeShown").SingleEntity();
					if (stateEntity.integer.Amount < stateContext.balancingDataEntity.o7SdkBalancingData.Data.Ratemeshowfrequency)
					{
						uiContext.OpenDialog<RateMePopup>();
						entity.isDestroyed = true;
					}
				}
			}
		}
	}
}
