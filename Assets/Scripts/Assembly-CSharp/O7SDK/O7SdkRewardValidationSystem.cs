using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

namespace O7SDK
{
	public class O7SdkRewardValidationSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uIContext;

		public O7SdkRewardValidationSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			uIContext = context.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkRewardNotAwarded.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasBoughtPlayerResurect || entity.isBoughtPlayerRevive)
				{
					uIContext.OpenDialog<FailScreen>();
					InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
					inputEntity.isInput = true;
					inputEntity.isPlayerLost = true;
				}
				entity.isDestroyed = true;
			}
		}
	}
}
