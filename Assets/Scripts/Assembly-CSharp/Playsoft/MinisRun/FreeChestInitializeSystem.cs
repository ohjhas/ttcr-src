using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class FreeChestInitializeSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private readonly IGroup<StateEntity> freeChests;

		public FreeChestInitializeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			freeChests = stateContext.GetGroup(StateMatcher.FreeChest);
		}

		public void Initialize()
		{
			if (freeChests.GetEntities().Length > 0)
			{
				return;
			}
			IEnumerable<KeyValuePair<int, ChestData>> freeChestsData = GetFreeChestsData();
			foreach (KeyValuePair<int, ChestData> item in freeChestsData)
			{
				stateContext.CreateEntity().AddFreeChest(true, GetNewFreeUnlockDate(item.Value));
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ToLoad.Removed());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "FreeChestData";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("FreeChestData").SingleEntity();
			StateEntity[] entities2 = freeChests.GetEntities();
			foreach (StateEntity stateEntity2 in entities2)
			{
				stateEntity2.ReplaceFreeChest(true, GetLoadedFreeUnlockDate(stateEntity.dateTime.Value));
			}
		}

		private DateTime GetLoadedFreeUnlockDate(DateTime loaDateTime)
		{
			return loaDateTime.AddSeconds(stateContext.balancingDataEntity.gatchaBalancingData.Map.FirstOrDefault((KeyValuePair<int, ChestData> x) => x.Value.WaitTime > 0).Value.WaitTime);
		}

		private DateTime GetNewFreeUnlockDate(ChestData data)
		{
			return stateContext.currentRealTime.CurrentTime.AddSeconds(data.WaitTime);
		}

		private IEnumerable<KeyValuePair<int, ChestData>> GetFreeChestsData()
		{
			return stateContext.balancingDataEntity.gatchaBalancingData.Map.Where((KeyValuePair<int, ChestData> x) => x.Value.WaitTime > 0 && x.Value.PriceHC <= 0 && x.Value.PriceSC <= 0);
		}
	}
}
