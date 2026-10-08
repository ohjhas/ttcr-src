using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkGdprOptOutDeciderSystem : ReactiveSystem<UIEntity>
	{
		public readonly StateContext stateContext;

		public O7SdkGdprOptOutDeciderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.O7SdkGdprOptOutListener.Added());
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
				O7SDKGdprManager manager = o7SdkEntity.o7SdkGdprManager.manager;
				entity.o7SdkGdprOptOutListener.listener.ShowOptOut(manager.IsGdprConsentRequired());
				entity.isDestroyed = true;
			}
		}
	}
}
