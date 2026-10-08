using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class FreeChestResetSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public FreeChestResetSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.OpenFreeChest.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntities(StateMatcher.FreeChest).FirstOrDefault();
				stateEntity.ReplaceFreeChest(true, GetNewFreeUnlockDate());
				entity.isDestroyed = true;
				StateEntity stateEntity2 = stateContext.CreateEntity();
				stateEntity2.isSavedData = true;
				stateEntity2.AddName("FreeChestDataSend");
				stateEntity2.AddDateTime(stateContext.currentRealTime.CurrentTime);
			}
		}

		private DateTime GetNewFreeUnlockDate()
		{
			KeyValuePair<int, ChestData> keyValuePair = stateContext.balancingDataEntity.gatchaBalancingData.Map.FirstOrDefault((KeyValuePair<int, ChestData> x) => x.Value.WaitTime > 0);
			return stateContext.currentRealTime.CurrentTime.AddSeconds(keyValuePair.Value.WaitTime);
		}
	}
}
