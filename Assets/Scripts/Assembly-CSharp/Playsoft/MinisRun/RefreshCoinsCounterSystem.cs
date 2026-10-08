using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshCoinsCounterSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly IGroup<UIEntity> coinListeners;

		private StateContext stateContext;

		private UIContext uiContext;

		public RefreshCoinsCounterSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
			coinListeners = uiContext.GetGroup(UIMatcher.CoinsListener);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.Integer.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasName && entity.name.Name == "Coins";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				UIEntity[] entities2 = coinListeners.GetEntities();
				foreach (UIEntity uIEntity in entities2)
				{
					uIEntity.coinsListener.listener.CoinsChanged(entity.integer.Amount);
				}
			}
		}

		public void Initialize()
		{
			HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("Coins");
			foreach (StateEntity item in entitiesWithName)
			{
				UIEntity[] entities = coinListeners.GetEntities();
				foreach (UIEntity uIEntity in entities)
				{
					uIEntity.coinsListener.listener.CoinsChanged(item.integer.Amount);
				}
			}
		}
	}
}
