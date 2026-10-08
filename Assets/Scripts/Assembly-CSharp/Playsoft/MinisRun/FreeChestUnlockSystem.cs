using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class FreeChestUnlockSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly IGroup<StateEntity> freeChests;

		public FreeChestUnlockSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			freeChests = stateContext.GetGroup(StateMatcher.FreeChest);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.CurrentRealTime.Added(), StateMatcher.FreeChest.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			DateTime currentTime = stateContext.currentRealTime.CurrentTime;
			StateEntity[] entities2 = freeChests.GetEntities();
			foreach (StateEntity stateEntity in entities2)
			{
				if (stateEntity.freeChest.IsLocked && stateEntity.freeChest.UnlockTime <= currentTime)
				{
					stateEntity.ReplaceFreeChest(false, currentTime);
				}
			}
		}
	}
}
