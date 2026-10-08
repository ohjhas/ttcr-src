using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshCurrentTimeSystem : ReactiveSystem<StateEntity>
	{
		private readonly IGroup<StateEntity> timeListeners;

		private readonly StateContext stateContext;

		public RefreshCurrentTimeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			timeListeners = stateContext.GetGroup(StateMatcher.CurrentTimeListener);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.CurrentRealTime.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity[] entities2 = timeListeners.GetEntities();
				foreach (StateEntity stateEntity in entities2)
				{
					stateEntity.currentTimeListener.listener.CurrentTimeChanged(entity.currentRealTime.CurrentTime);
				}
			}
		}
	}
}
