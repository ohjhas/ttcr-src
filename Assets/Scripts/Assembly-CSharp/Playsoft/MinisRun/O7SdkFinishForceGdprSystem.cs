using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkFinishForceGdprSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public O7SdkFinishForceGdprSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkGdprRequest);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkForceCheckProcessed;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				stateContext.sessionEntity.isO7SdkForceGdpr = false;
				entity.isDestroyed = true;
			}
		}
	}
}
