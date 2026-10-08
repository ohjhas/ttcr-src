using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkAdTrackingRefreshSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private IGroup<UIEntity> adTrackingListeners;

		private StateContext stateContext;

		private UIContext uiContext;

		public O7SdkAdTrackingRefreshSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			uiContext = context.uI;
			stateContext = context.state;
			adTrackingListeners = uiContext.GetGroup(UIMatcher.O7SdkAdTrackingListener);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.Boolean);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasName && entity.name.Name == "AdTrackingEnabled";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			InformListeners(entities);
		}

		public void Initialize()
		{
			HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("AdTrackingEnabled");
			InformListeners(entitiesWithName.ToList());
		}

		private void InformListeners(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				UIEntity[] entities2 = adTrackingListeners.GetEntities();
				foreach (UIEntity uIEntity in entities2)
				{
					uIEntity.o7SdkAdTrackingListener.listener.UpdateAdTrackingButton(entity.boolean.Value);
				}
			}
		}
	}
}
