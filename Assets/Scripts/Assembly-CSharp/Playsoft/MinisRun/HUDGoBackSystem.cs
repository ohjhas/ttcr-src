using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class HUDGoBackSystem : ReactiveSystem<InputEntity>
	{
		private readonly UIContext uiContext;

		private readonly StateContext stateContext;

		public HUDGoBackSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.GoBack);
		}

		protected override bool Filter(InputEntity entity)
		{
			return true;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			if (uiContext.uIEntity != null && uiContext.uIEntity.hasHUD)
			{
				BaseDialog baseDialog = uiContext.FindCurrentScreen();
				if (baseDialog != null)
				{
					TryGoBack(baseDialog);
				}
			}
			for (int i = 0; i < entities.Count; i++)
			{
				entities[i].Destroy();
			}
		}

		private void TryGoBack(BaseDialog currentScreen)
		{
			if (currentScreen.IsClickable && !TryGoBackFromHUD(currentScreen))
			{
				TryGoBackFromScreen(currentScreen);
			}
		}

		private bool TryGoBackFromHUD(BaseDialog currentScreen)
		{
			bool result = false;
			if (currentScreen as WorldSelectScreen != null)
			{
				UIEntity uIEntity = uiContext.CreateEntity();
				uIEntity.AddChangeScene(ProjectConstants.HOME_SCENE_NAME, false);
				result = true;
			}
			else if (currentScreen as CharSelectScreen != null)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("LastScene").SingleEntity();
				UIEntity uIEntity2 = uiContext.CreateEntity();
				uIEntity2.AddChangeScene(stateEntity.stringData.Value, false);
				result = true;
			}
			else if (currentScreen as CompSelectScreen != null)
			{
				StateEntity stateEntity2 = stateContext.GetEntitiesWithName("LastScene").SingleEntity();
				UIEntity uIEntity3 = uiContext.CreateEntity();
				uIEntity3.AddChangeScene(stateEntity2.stringData.Value, false);
				result = true;
			}
			else if (currentScreen as IngameHUD != null && !Contexts.sharedInstance.game.worldEntity.isPaused)
			{
				Contexts.sharedInstance.game.worldEntity.isPaused = true;
			}
			return result;
		}

		private void TryGoBackFromScreen(BaseDialog currentScreen)
		{
			if (currentScreen as AbilityPopup != null || currentScreen as ConfirmTransactionPopup != null || currentScreen as ExplanationPopup != null || currentScreen as MissionDetailsPopup != null || currentScreen as RelayPlayerScreen != null || currentScreen as RevivePlayerScreen != null || currentScreen as ResurectPlayerScreen != null || currentScreen as SettingsScreen != null || currentScreen as CharOutOfEnergyPopup != null || currentScreen is PrivacyAndLegalPopup || currentScreen is EPrivacyPopup || currentScreen is CharSelectInfoPopup || currentScreen is CompSelectInfoPopup || currentScreen is CountryCodesPopup)
			{
				uiContext.CloseDialog(currentScreen);
			}
			else if (currentScreen as ShopScreen != null || currentScreen as DailyMissionsScreen != null || currentScreen is RateMePopup)
			{
				uiContext.CloseDialog(currentScreen);
				UIEntity uIEntity = uiContext.CreateEntity();
				uIEntity.isRefreshScreen = true;
			}
			else if (currentScreen as PauseScreen != null)
			{
				uiContext.CloseDialog(currentScreen);
				IngameHUD ingameHUD = uiContext.uIEntity.hUD.hud as IngameHUD;
				if (ingameHUD != null)
				{
					ingameHUD.ResumeGame();
				}
				else
				{
					Debug.LogError("HUD not found");
				}
			}
			else if (currentScreen as FailScreen != null)
			{
				Contexts.sharedInstance.game.worldEntity.isDead = true;
			}
			else if (currentScreen is GDPRProvidersSelectionScreen)
			{
				uiContext.CloseDialog(currentScreen);
				uiContext.OpenDialog<SettingsScreen>();
			}
		}
	}
}
