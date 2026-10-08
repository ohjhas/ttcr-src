using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshScreenSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public RefreshScreenSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.RefreshScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			if (uiContext.uIEntity != null && uiContext.uIEntity.hasHUD && uiContext.uIEntity.hasOpenedScreens)
			{
				BaseDialog baseDialog = uiContext.FindCurrentScreen();
				if (baseDialog != null)
				{
					TryToRefresh(baseDialog);
				}
			}
			foreach (UIEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
		}

		private void TryToRefresh(BaseDialog currentScreen)
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(currentScreen);
			if (currentScreen as WorldSelectScreen != null)
			{
				uIEntity.isUIMissionListRequest = true;
				uIEntity.isUIChapterListRequest = true;
				uIEntity.isUILastSelectedMissionRequest = true;
				uIEntity.isUIUnlockedMissionRequest = true;
				uIEntity.isUINewMissionUnlockedRequest = true;
				uIEntity.isUITotalStarsRequest = true;
				uIEntity.isUIChestListRequest = true;
				uIEntity.isUICharSelectedRequest = true;
				uIEntity.isUICurrentDailyMissionsRequest = true;
				uIEntity.isUICompSelectedRequest = true;
				uIEntity.isUINewCharactersUnlockedRequest = true;
				uIEntity.isUINewCompanionUnlockedRequest = true;
			}
			else if (currentScreen as CharSelectScreen != null)
			{
				uIEntity.isUITotalCoinsRequest = true;
				uIEntity.isUICharListRequest = true;
				uIEntity.isUIPlayerProgressionStateRequest = true;
				uIEntity.isUINewCharactersUnlockedRequest = true;
			}
			else if (currentScreen as CompSelectScreen != null)
			{
				uIEntity.isUITotalCoinsRequest = true;
				uIEntity.isUICompanionListRequest = true;
				uIEntity.isUIPlayerProgressionStateRequest = true;
				uIEntity.isUINewCompanionUnlockedRequest = true;
			}
			else if (currentScreen as ShopScreen != null)
			{
				uIEntity.isUITotalCoinsRequest = true;
				uIEntity.isUITotalGemsRequest = true;
				uIEntity.isUIChestListRequest = true;
				uIEntity.isUIShopSoftCurrencyPacksRequest = true;
				uIEntity.isUIShopIAPsListRequest = true;
				uIEntity.isUIFreeChestRequest = true;
			}
			else if (currentScreen as DailyMissionsScreen != null)
			{
				uIEntity.isUITotalCoinsRequest = true;
				uIEntity.isUITotalGemsRequest = true;
				uIEntity.isUICurrentDailyMissionsRequest = true;
				uIEntity.isUIChestListRequest = true;
			}
			else if (currentScreen as ChestRevealScreen != null)
			{
				uIEntity.isUITotalCoinsRequest = true;
				uIEntity.isUITotalGemsRequest = true;
				uIEntity.isUICharListRequest = true;
				uIEntity.isUICompanionListRequest = true;
			}
			else if (currentScreen as HomeScreen != null)
			{
				uIEntity.isUICharListRequest = true;
				uIEntity.isUICompanionListRequest = true;
				uIEntity.isUICharSelectedRequest = true;
				uIEntity.isUICompSelectedRequest = true;
				uIEntity.isUIPlayerProgressionStateRequest = true;
				uIEntity.isUICurrentDailyMissionsRequest = true;
				uIEntity.isUINewCharactersUnlockedRequest = true;
				uIEntity.isUINewCompanionUnlockedRequest = true;
			}
			else if (currentScreen as CharOutOfEnergyPopup != null)
			{
				uIEntity.isUICharSelectedRequest = true;
				uIEntity.isUITotalGemsRequest = true;
			}
		}
	}
}
