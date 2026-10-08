using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class FreeChestSetInitialTimeSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public FreeChestSetInitialTimeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ActiveScreen);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.activeScreen.ScreenId == ScreenId.ShopScreen;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntities(StateMatcher.FreeChest).FirstOrDefault();
			StateEntity stateEntity2 = stateContext.GetEntitiesWithName("FreeChestInitialData").SingleEntity();
			if (!stateEntity2.boolean.Value)
			{
				stateEntity.ReplaceFreeChest(true, GetNewFreeUnlockDate());
				StateEntity stateEntity3 = stateContext.CreateEntity();
				stateEntity3.isSavedData = true;
				stateEntity3.AddName("FreeChestInitialDataSend");
				stateEntity3.AddBoolean(true);
			}
		}

		private DateTime GetNewFreeUnlockDate()
		{
			KeyValuePair<int, ChestData> keyValuePair = stateContext.balancingDataEntity.gatchaBalancingData.Map.FirstOrDefault((KeyValuePair<int, ChestData> x) => x.Value.InitialWaitTime > 0);
			return stateContext.currentRealTime.CurrentTime.AddSeconds(keyValuePair.Value.InitialWaitTime);
		}
	}
}
