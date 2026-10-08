using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Playsoft.MinisRun
{
	public static class CurrencyAndExperienceHelper
	{
		public static void OpenScreenForHomeScreen(UnityAction onScAction, UnityAction onHcAction)
		{
			if (TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.PlayAgain))
			{
				CurrencyAndExperienceScreen currencyAndExperienceScreen = Contexts.sharedInstance.uI.OpenDialog<CurrencyAndExperienceScreen>(false) as CurrencyAndExperienceScreen;
				if (currencyAndExperienceScreen != null)
				{
					currencyAndExperienceScreen.SetData();
					currencyAndExperienceScreen.SetElementsVisibility();
					currencyAndExperienceScreen.TurnOnButtonMode(onScAction, onHcAction);
				}
			}
		}

		public static void OpenScreenForWorldSelectScreen(UnityAction onScAction, UnityAction onHcAction)
		{
			if (TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.PlayAgain))
			{
				CurrencyAndExperienceScreen currencyAndExperienceScreen = Contexts.sharedInstance.uI.OpenDialog<CurrencyAndExperienceScreen>(false) as CurrencyAndExperienceScreen;
				if (currencyAndExperienceScreen != null)
				{
					currencyAndExperienceScreen.SetData();
					currencyAndExperienceScreen.SetElementsVisibility();
					currencyAndExperienceScreen.TurnOnButtonMode(onScAction, onHcAction);
				}
			}
		}

		public static void OpenScreenForDailyMissionsScreen()
		{
			if (TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.PlayAgain))
			{
				CurrencyAndExperienceScreen currencyAndExperienceScreen = Contexts.sharedInstance.uI.OpenDialog<CurrencyAndExperienceScreen>(false) as CurrencyAndExperienceScreen;
				if (currencyAndExperienceScreen != null && Contexts.sharedInstance.state.previousScreen.ScreenId == ScreenId.ChestRevealScreen)
				{
					currencyAndExperienceScreen.SetElementsVisibility(false);
					currencyAndExperienceScreen.SetData();
					currencyAndExperienceScreen.TurnOffButtonMode();
				}
			}
		}

		public static void OpenScreenForShopScreen()
		{
			if (TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.PlayAgain))
			{
				CurrencyAndExperienceScreen currencyAndExperienceScreen = Contexts.sharedInstance.uI.OpenDialog<CurrencyAndExperienceScreen>(false) as CurrencyAndExperienceScreen;
				ScreenId screenId = Contexts.sharedInstance.state.previousScreen.ScreenId;
				if ((currencyAndExperienceScreen != null && screenId == ScreenId.ChestRevealScreen) || screenId == ScreenId.CharSelectScreen || screenId == ScreenId.CharInfoPopup || screenId == ScreenId.CompInfoPopup || screenId == ScreenId.CompSelectScreen)
				{
					currencyAndExperienceScreen.SetData();
					currencyAndExperienceScreen.SetElementsVisibility();
				}
				currencyAndExperienceScreen.TurnOffButtonMode();
			}
		}

		public static void OpenScreenForResultScreen(int coinsAmount, RectTransform animationOrigin)
		{
			if (!TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.PlayAgain))
			{
				return;
			}
			CurrencyAndExperienceScreen currencyAndExperienceScreen = Contexts.sharedInstance.uI.OpenDialog<CurrencyAndExperienceScreen>(false) as CurrencyAndExperienceScreen;
			if (currencyAndExperienceScreen != null)
			{
				currencyAndExperienceScreen.SetData();
				currencyAndExperienceScreen.SetElementsVisibility(false, true, false, false);
				currencyAndExperienceScreen.ShowBarWithAnimation();
				currencyAndExperienceScreen.TurnOffButtonMode();
				DOVirtual.DelayedCall(0.5f, delegate
				{
					StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
					stateEntity.isTransaction = true;
					stateEntity.isTransactionBegin = true;
					stateEntity.AddBoughtSoftCurrency(coinsAmount);
					stateEntity.AddTransactionOrigin(animationOrigin);
				});
				DOVirtual.DelayedCall(2f, currencyAndExperienceScreen.HideBarWithAnimation);
			}
		}

		public static void OpenScreenForExperienceUpdate(int currentLevelNumber, int currentExpAmount, Dictionary<int, PlayerLevelData> playerLevelsData, PlayerProgressionUpdateData progressUpdateData)
		{
			if (!TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.PlayAgain))
			{
				return;
			}
			CurrencyAndExperienceScreen currencyAndExperienceScreen = Contexts.sharedInstance.uI.GetDialog<CurrencyAndExperienceScreen>();
			bool flag = false;
			if (currencyAndExperienceScreen == null || !currencyAndExperienceScreen.gameObject.activeInHierarchy)
			{
				currencyAndExperienceScreen = Contexts.sharedInstance.uI.OpenDialog<CurrencyAndExperienceScreen>(false) as CurrencyAndExperienceScreen;
				flag = true;
			}
			if (currencyAndExperienceScreen != null)
			{
				if (flag)
				{
					currencyAndExperienceScreen.SetElementsVisibility(true, false, false, false);
					currencyAndExperienceScreen.TurnOffButtonMode();
				}
				currencyAndExperienceScreen.SetData();
				currencyAndExperienceScreen.ShowExperienceAnimation(currentLevelNumber, currentExpAmount, playerLevelsData, progressUpdateData);
			}
		}

		public static void OpenScreenForCurrencyUpdate(int itemIndex, int amount, RectTransform origin, bool updateTargetManually)
		{
			CurrencyAndExperienceScreen.Type type = ((itemIndex == ProjectConstants.SOFT_CURRENCY_ID) ? CurrencyAndExperienceScreen.Type.Coins : CurrencyAndExperienceScreen.Type.Diamonds);
			CurrencyAndExperienceScreen dialog = Contexts.sharedInstance.uI.GetDialog<CurrencyAndExperienceScreen>();
			bool flag = dialog == null || !dialog.gameObject.activeInHierarchy;
			dialog = Contexts.sharedInstance.uI.OpenDialog<CurrencyAndExperienceScreen>(false) as CurrencyAndExperienceScreen;
			if (dialog != null)
			{
				if (flag)
				{
					dialog.SetElementsVisibility(false, true, true, false);
					dialog.TurnOffButtonMode();
					dialog.SetData();
				}
				dialog.ShowCurrencyAnimation(type, amount, origin, updateTargetManually);
			}
		}

		public static void CloseScreen()
		{
			UIContext uI = Contexts.sharedInstance.uI;
			CurrencyAndExperienceScreen dialog = uI.GetDialog<CurrencyAndExperienceScreen>();
			if (!(null == dialog))
			{
				uI.CloseDialog(dialog);
			}
		}
	}
}
