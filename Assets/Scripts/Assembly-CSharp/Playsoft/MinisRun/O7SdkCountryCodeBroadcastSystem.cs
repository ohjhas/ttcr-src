using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkCountryCodeBroadcastSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly UIContext uiContext;

		private readonly StateContext stateContext;

		private readonly IGroup<UIEntity> uiListeners;

		public O7SdkCountryCodeBroadcastSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
			uiContext = context.uI;
			uiListeners = uiContext.GetGroup(UIMatcher.O7SdkCountryCodeListener);
		}

		public void Initialize()
		{
			HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("countryCodeKey");
			InformListeners(entitiesWithName.ToList());
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.StringData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasName && entity.name.Name == "countryCodeKey";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			InformListeners(entities);
		}

		private void InformListeners(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				UIEntity[] entities2 = uiListeners.GetEntities();
				foreach (UIEntity uIEntity in entities2)
				{
					uIEntity.o7SdkCountryCodeListener.listener.SetCountryCodeOnButton(entity.stringData.Value);
				}
			}
		}
	}
}
