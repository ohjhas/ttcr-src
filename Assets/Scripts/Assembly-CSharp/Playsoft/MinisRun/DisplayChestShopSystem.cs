using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DisplayChestShopSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext _uiContext;

		public DisplayChestShopSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			_uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIOpenShop;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				OpenChestShopScreen(entity.uIOpenShop.EntryPoint);
				entity.isDestroyed = true;
			}
		}

		private void OpenChestShopScreen(ShopGroupType groupType)
		{
			BaseDialog baseDialog = _uiContext.OpenDialog<ShopScreen>();
			((ShopScreen)baseDialog).ShopEntryPoint = groupType;
			UIEntity uIEntity = _uiContext.CreateEntity();
			uIEntity.AddSetup(baseDialog);
			uIEntity.isUITotalCoinsRequest = true;
			uIEntity.isUITotalGemsRequest = true;
			uIEntity.isUIChestListRequest = true;
			uIEntity.isUIShopSoftCurrencyPacksRequest = true;
			uIEntity.isUIShopIAPsListRequest = true;
			uIEntity.isUIFreeChestRequest = true;
		}
	}
}
