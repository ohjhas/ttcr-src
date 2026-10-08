using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogIAPTransactionSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public LogIAPTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.LogO7SdkIapBuyRequest.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				LogIapPurchaseEvent(entity.logO7SdkIapBuyRequest.productId);
				entity.isDestroyed = true;
			}
		}

		private void LogIapPurchaseEvent(string productId)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("iap", "iap-click", ScreenId.ShopScreen.ToString(), productId, null, null, productId, null, null, false);
		}
	}
}
