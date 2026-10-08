using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ShopFreeChestDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public ShopFreeChestDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIFreeChestRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				FreeChestData freeChestData = GetFreeChestData();
				entity.AddUIFreeChestResponse(freeChestData);
			}
		}

		private FreeChestData GetFreeChestData()
		{
			StateEntity stateEntity = stateContext.GetEntities(StateMatcher.FreeChest).FirstOrDefault();
			if (stateEntity == null)
			{
				return default(FreeChestData);
			}
			return new FreeChestData
			{
				IsLocked = stateEntity.freeChest.IsLocked,
				UnlockTime = stateEntity.freeChest.UnlockTime
			};
		}
	}
}
