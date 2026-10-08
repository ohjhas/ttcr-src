using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkAdTrackingEnableSystem : ReactiveSystem<UIEntity>
	{
		public readonly StateContext stateContext;

		public O7SdkAdTrackingEnableSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.O7SdkAdTrackingActiveListener);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				StateEntity o7SdkEntity = stateContext.o7SdkEntity;
				O7SDKGateManager manager = o7SdkEntity.o7SdkAgeGateManager.Manager;
				O7SDKGdprManager manager2 = o7SdkEntity.o7SdkGdprManager.manager;
				entity.o7SdkAdTrackingActiveListener.listener.EnableAdTracking(!manager2.IsGdprConsentRequired() && manager.DidUserPassAgeGate());
				entity.isDestroyed = true;
			}
		}
	}
}
