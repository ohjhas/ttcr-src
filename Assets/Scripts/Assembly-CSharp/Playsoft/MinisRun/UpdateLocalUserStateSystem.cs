using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UpdateLocalUserStateSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public UpdateLocalUserStateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ToSave.Removed());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isUserStateData && stateContext.localUserStateEntity != null;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			UserStateData stateData = stateContext.localUserStateEntity.localUserState.StateData;
			stateData.FillWithData(entities, false);
		}
	}
}
