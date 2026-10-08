using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class EulaClickedEventSendSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public EulaClickedEventSendSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.EulaClickedEventToBeSent.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isO7SdkCall = true;
				stateEntity.AddEventCustom("eula-click", "app-features", null, null, null, null, null, null, null, false);
				entity.isDestroyed = true;
			}
		}
	}
}
