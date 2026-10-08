using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkEventTutorialBeginSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public O7SdkEventTutorialBeginSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.EventTutorialBegin.Added());
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
				manager.AddEventTutorialBegin(entity.eventTutorialBegin.stepId);
			}
		}
	}
}
