using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PaidUserUpdateSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public PaidUserUpdateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.Boolean);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "PaidUser";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (stateContext.o7SdkEntity == null)
				{
					break;
				}
				stateContext.o7SdkEntity.isO7SdkPaidUser = entity.boolean.Value;
			}
		}
	}
}
