using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class WorldSelectInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public WorldSelectInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.WorldSelectScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			if (entities.Count <= 0)
			{
				return;
			}
			foreach (UIEntity entity in entities)
			{
				entity.isDestroyed = true;
				if (entity.isUIUnlockNewMission)
				{
					StateEntity stateEntity = stateContext.GetEntitiesWithName("NewLevelUnlocked").SingleEntity();
					stateEntity.ReplaceBoolean(false);
				}
				else if (entity.isUIOpenCharSelection)
				{
					OpenCharSelectScreen();
				}
				else if (entity.isUIOpenCompSelection)
				{
					OpenCompSelectScreen();
				}
				else if (entity.hasUIOpenShop)
				{
					OpenShopScreen(entity.uIOpenShop.EntryPoint);
				}
				else if (entity.hasUIPlayMission)
				{
					PlayLevelIfPossible(entity.uIPlayMission.Index);
					break;
				}
			}
		}

		private void PlayLevelIfPossible(int worldIndex)
		{
			if (CharacterHasEnergy())
			{
				PlayMission(worldIndex);
			}
			else
			{
				OpenOutOfEnergyPopup(worldIndex);
			}
		}

		private bool CharacterHasEnergy()
		{
			int amount = stateContext.GetEntitiesWithName("FirstCharSelectData").SingleEntity().integer.Amount;
			int amount2 = stateContext.GetEntitiesWithName("CharLifesData" + amount).SingleEntity().integer.Amount;
			return amount2 > 0;
		}

		private void PlayMission(int index)
		{
			SettingsService.SettingsSO.WorldIndex = index;
			StateEntity stateEntity = stateContext.GetEntitiesWithName("SelectedLevel").SingleEntity();
			stateEntity.integer.Amount = index;
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddChangeScene(ProjectConstants.INGAME_SCENE_NAME, false);
		}

		private void OpenOutOfEnergyPopup(int index)
		{
			int amount = stateContext.GetEntitiesWithName("FirstCharSelectData").SingleEntity().integer.Amount;
			BaseDialog newDialog = uiContext.OpenDialog<CharOutOfEnergyPopup>();
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(newDialog);
			uIEntity.AddUICharSingleRequest(amount);
			uIEntity.isUITotalGemsRequest = true;
			uIEntity.AddUIActionFinishedCallback(delegate
			{
				PlayMission(index);
			});
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

		private void OpenShopScreen(ShopGroupType entryPoint)
		{
			((ShopScreen)uiContext.OpenDialog<ShopScreen>()).ShopEntryPoint = entryPoint;
			uiContext.RefreshScreen();
		}
	}
}
