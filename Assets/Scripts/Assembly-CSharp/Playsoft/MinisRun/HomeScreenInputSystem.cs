using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class HomeScreenInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public HomeScreenInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.HomeScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUIOpenShop)
				{
					OpenShopScreen(entity.uIOpenShop.EntryPoint);
				}
				else if (entity.isUIOpenCharSelection)
				{
					OpenCharSelectScreen();
				}
				else if (entity.isUIOpenPvE)
				{
					OpenPvEScreen();
				}
				else if (entity.isUIOpenSettings)
				{
					OpenSettingsScreen();
				}
				else if (entity.isUIOpenCompSelection)
				{
					OpenCompSelectScreen();
				}
				entity.isDestroyed = true;
			}
		}

		private void OpenShopScreen(ShopGroupType entryPoint)
		{
			((ShopScreen)uiContext.OpenDialog<ShopScreen>()).ShopEntryPoint = entryPoint;
			uiContext.RefreshScreen();
		}

		private void OpenCharSelectScreen()
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddChangeScene(ProjectConstants.CHAR_SELECT_SCENE_NAME, false);
		}

		private void OpenCompSelectScreen()
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddChangeScene(ProjectConstants.COMP_SELECT_SCENE_NAME, false);
		}

		private void OpenPvEScreen()
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddChangeScene(ProjectConstants.WORLD_SELECT_SCENE_NAME, false);
		}

		private void OpenSettingsScreen()
		{
			uiContext.OpenDialog<SettingsScreen>();
			uiContext.RefreshScreen();
		}
	}
}
