using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshHardCurrencyCounterSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly IGroup<UIEntity> hardListeners;

		private StateContext stateContext;

		private UIContext uiContext;

		public RefreshHardCurrencyCounterSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
			hardListeners = uiContext.GetGroup(UIMatcher.HardCurrencyListener);
		}

		public void Initialize()
		{
			HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("HardCurrency");
			foreach (StateEntity item in entitiesWithName)
			{
				UIEntity[] entities = hardListeners.GetEntities();
				foreach (UIEntity uIEntity in entities)
				{
					uIEntity.hardCurrencyListener.listener.HardCurrencyChanged(item.integer.Amount);
				}
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.Integer);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasName && entity.name.Name == "HardCurrency";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				UIEntity[] entities2 = hardListeners.GetEntities();
				foreach (UIEntity uIEntity in entities2)
				{
					uIEntity.hardCurrencyListener.listener.HardCurrencyChanged(entity.integer.Amount);
				}
			}
		}
	}
}
