using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkDestroySystem : ReactiveSystem<StateEntity>
	{
		public O7SdkDestroySystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkCall.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
		}
	}
}
