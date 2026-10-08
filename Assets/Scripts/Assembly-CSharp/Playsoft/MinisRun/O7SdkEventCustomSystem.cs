using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkEventCustomSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public O7SdkEventCustomSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.EventCustom.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return stateContext.o7SdkEntity != null && stateContext.o7SdkEntity.hasO7SdkEventManager;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			O7SDKEventManager manager = stateContext.o7SdkEntity.o7SdkEventManager.Manager;
			foreach (StateEntity entity in entities)
			{
				manager.AddEvent(entity.eventCustom.eid, entity.eventCustom.gid, entity.eventCustom.p1, entity.eventCustom.p2, entity.eventCustom.p3, entity.eventCustom.p4, entity.eventCustom.p5, entity.eventCustom.res, entity.eventCustom.jsonData, entity.eventCustom.isOnDemand);
			}
		}
	}
}
