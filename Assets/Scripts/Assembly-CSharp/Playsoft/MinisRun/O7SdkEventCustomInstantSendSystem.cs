using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkEventCustomInstantSendSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public O7SdkEventCustomInstantSendSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.EventCustomSend.Added());
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
				manager.AddEventAndSend(entity.eventCustomSend.component.eid, entity.eventCustomSend.component.gid, entity.eventCustomSend.component.p1, entity.eventCustomSend.component.p2, entity.eventCustomSend.component.p3, entity.eventCustomSend.component.p4, entity.eventCustomSend.component.p5, entity.eventCustomSend.component.res, entity.eventCustomSend.component.jsonData, entity.eventCustomSend.component.isOnDemand);
			}
		}
	}
}
