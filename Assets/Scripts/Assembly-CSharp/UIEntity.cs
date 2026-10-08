using System;
using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class UIEntity : Entity, IAsyncRequest, ICoinsListener, IDestroyed, IO7SdkCall, IRaccoonIntrosListener, IStaminasListener, ITutorialsListener
{
	private static readonly AbilityPopupComponent abilityPopupComponent = new AbilityPopupComponent();

	private static readonly CharacterSelectScreenComponent characterSelectScreenComponent = new CharacterSelectScreenComponent();

	private static readonly ChestRevealScreenComponent chestRevealScreenComponent = new ChestRevealScreenComponent();

	private static readonly ChestShopScreenComponent chestShopScreenComponent = new ChestShopScreenComponent();

	private static readonly CloseScreenComponent closeScreenComponent = new CloseScreenComponent();

	private static readonly CompanionSelectScreenComponent companionSelectScreenComponent = new CompanionSelectScreenComponent();

	private static readonly DestroyedComponent destroyedComponent = new DestroyedComponent();

	private static readonly DisplayUDIDComponent displayUDIDComponent = new DisplayUDIDComponent();

	private static readonly HomeScreenComponent homeScreenComponent = new HomeScreenComponent();

	private static readonly IntialSceneDecider intialSceneDeciderComponent = new IntialSceneDecider();

	private static readonly IntroDoneComponent introDoneComponent = new IntroDoneComponent();

	private static readonly LoadingSpinnerHideComponent loadingSpinnerHideComponent = new LoadingSpinnerHideComponent();

	private static readonly NewSceneLoadedComponent newSceneLoadedComponent = new NewSceneLoadedComponent();

	private static readonly O7SdkCallComponent o7SdkCallComponent = new O7SdkCallComponent();

	private static readonly O7SdkGridButtonUpdateRequestComponent o7SdkGridButtonUpdateRequestComponent = new O7SdkGridButtonUpdateRequestComponent();

	private static readonly RefreshScreenComponent refreshScreenComponent = new RefreshScreenComponent();

	private static readonly RelayPlayerScreenComponent relayPlayerScreenComponent = new RelayPlayerScreenComponent();

	private static readonly ResurectPlayerScreenComponent resurectPlayerScreenComponent = new ResurectPlayerScreenComponent();

	private static readonly RevivePlayerScreenComponent revivePlayerScreenComponent = new RevivePlayerScreenComponent();

	private static readonly SettingsScreenComponent settingsScreenComponent = new SettingsScreenComponent();

	private static readonly ShopScreenComponent shopScreenComponent = new ShopScreenComponent();

	private static readonly UIActionComponent uIActionComponent = new UIActionComponent();

	private static readonly UIChapterListRequestComponent uIChapterListRequestComponent = new UIChapterListRequestComponent();

	private static readonly UICharListRequestComponent uICharListRequestComponent = new UICharListRequestComponent();

	private static readonly UICharSelectedRequestComponent uICharSelectedRequestComponent = new UICharSelectedRequestComponent();

	private static readonly UICheckPlayerProgressComponent uICheckPlayerProgressComponent = new UICheckPlayerProgressComponent();

	private static readonly UIChestListRequestComponent uIChestListRequestComponent = new UIChestListRequestComponent();

	private static readonly UICompanionListRequestComponent uICompanionListRequestComponent = new UICompanionListRequestComponent();

	private static readonly UIComponent uIComponent = new UIComponent();

	private static readonly UICompSelectedRequestComponent uICompSelectedRequestComponent = new UICompSelectedRequestComponent();

	private static readonly UICurrentDailyMissionsRequestComponent uICurrentDailyMissionsRequestComponent = new UICurrentDailyMissionsRequestComponent();

	private static readonly UIExplanationPopupShownComponent uIExplanationPopupShownComponent = new UIExplanationPopupShownComponent();

	private static readonly UIFreeChestOpenedComponent uIFreeChestOpenedComponent = new UIFreeChestOpenedComponent();

	private static readonly UIFreeChestRequestComponent uIFreeChestRequestComponent = new UIFreeChestRequestComponent();

	private static readonly UIGiveDailyMissionsDailyRewardComponent uIGiveDailyMissionsDailyRewardComponent = new UIGiveDailyMissionsDailyRewardComponent();

	private static readonly UIHideAbilityComponent uIHideAbilityComponent = new UIHideAbilityComponent();

	private static readonly UIHideExplanation uIHideExplanationComponent = new UIHideExplanation();

	private static readonly UIHideMissionsDetails uIHideMissionsDetailsComponent = new UIHideMissionsDetails();

	private static readonly UIHideTransactionConfirmation uIHideTransactionConfirmationComponent = new UIHideTransactionConfirmation();

	private static readonly UILastSelectedMissionRequestComponent uILastSelectedMissionRequestComponent = new UILastSelectedMissionRequestComponent();

	private static readonly UIMissionListRequestComponent uIMissionListRequestComponent = new UIMissionListRequestComponent();

	private static readonly UINewCharactersUnlockedRequestComponent uINewCharactersUnlockedRequestComponent = new UINewCharactersUnlockedRequestComponent();

	private static readonly UINewCompanionUnlockedRequestComponent uINewCompanionUnlockedRequestComponent = new UINewCompanionUnlockedRequestComponent();

	private static readonly UINewMissionUnlockedRequestComponent uINewMissionUnlockedRequestComponent = new UINewMissionUnlockedRequestComponent();

	private static readonly UIOpenCharSelectionComponent uIOpenCharSelectionComponent = new UIOpenCharSelectionComponent();

	private static readonly UIOpenChestShopComponent uIOpenChestShopComponent = new UIOpenChestShopComponent();

	private static readonly UIOpenCompSelectionComponent uIOpenCompSelectionComponent = new UIOpenCompSelectionComponent();

	private static readonly UIOpenContactsComponent uIOpenContactsComponent = new UIOpenContactsComponent();

	private static readonly UIOpenCountryCodesComponent uIOpenCountryCodesComponent = new UIOpenCountryCodesComponent();

	private static readonly UIOpenDailyMissionsComponent uIOpenDailyMissionsComponent = new UIOpenDailyMissionsComponent();

	private static readonly UIOpenEPrivacyComponent uIOpenEPrivacyComponent = new UIOpenEPrivacyComponent();

	private static readonly UIOpenEulaComponent uIOpenEulaComponent = new UIOpenEulaComponent();

	private static readonly UIOpenGdprComponent uIOpenGdprComponent = new UIOpenGdprComponent();

	private static readonly UIOpenPrivacyAndLegacyComponent uIOpenPrivacyAndLegacyComponent = new UIOpenPrivacyAndLegacyComponent();

	private static readonly UIOpenPrivacyPolicyComponent uIOpenPrivacyPolicyComponent = new UIOpenPrivacyPolicyComponent();

	private static readonly UIOpenPvEComponent uIOpenPvEComponent = new UIOpenPvEComponent();

	private static readonly UIOpenSettingsComponent uIOpenSettingsComponent = new UIOpenSettingsComponent();

	private static readonly UIPlayerProgressionStateRequestComponent uIPlayerProgressionStateRequestComponent = new UIPlayerProgressionStateRequestComponent();

	private static readonly UIShopIAPsListRequestComponent uIShopIAPsListRequestComponent = new UIShopIAPsListRequestComponent();

	private static readonly UIShopScreenExitComponent uIShopScreenExitComponent = new UIShopScreenExitComponent();

	private static readonly UIShopSoftCurrencyPacksRequestComponent uIShopSoftCurrencyPacksRequestComponent = new UIShopSoftCurrencyPacksRequestComponent();

	private static readonly UITotalCoinsRequestComponent uITotalCoinsRequestComponent = new UITotalCoinsRequestComponent();

	private static readonly UITotalGemsRequestComponent uITotalGemsRequestComponent = new UITotalGemsRequestComponent();

	private static readonly UITotalStarsRequestComponent uITotalStarsRequestComponent = new UITotalStarsRequestComponent();

	private static readonly UIUnlockedMissionRequestComponent uIUnlockedMissionRequestComponent = new UIUnlockedMissionRequestComponent();

	private static readonly UIUnlockNewMissionComponent uIUnlockNewMissionComponent = new UIUnlockNewMissionComponent();

	private static readonly WinChestOnResultScreen winChestOnResultScreenComponent = new WinChestOnResultScreen();

	private static readonly WorldSelectScreenComponent worldSelectScreenComponent = new WorldSelectScreenComponent();

	public bool isAbilityPopup
	{
		get
		{
			return HasComponent(20);
		}
		set
		{
			if (value != isAbilityPopup)
			{
				if (value)
				{
					AddComponent(20, abilityPopupComponent);
				}
				else
				{
					RemoveComponent(20);
				}
			}
		}
	}

	public AsyncRequestComponent asyncRequest
	{
		get
		{
			return (AsyncRequestComponent)GetComponent(0);
		}
	}

	public bool hasAsyncRequest
	{
		get
		{
			return HasComponent(0);
		}
	}

	public ChangeSceneComponent changeScene
	{
		get
		{
			return (ChangeSceneComponent)GetComponent(21);
		}
	}

	public bool hasChangeScene
	{
		get
		{
			return HasComponent(21);
		}
	}

	public bool isCharacterSelectScreen
	{
		get
		{
			return HasComponent(22);
		}
		set
		{
			if (value != isCharacterSelectScreen)
			{
				if (value)
				{
					AddComponent(22, characterSelectScreenComponent);
				}
				else
				{
					RemoveComponent(22);
				}
			}
		}
	}

	public bool isChestRevealScreen
	{
		get
		{
			return HasComponent(23);
		}
		set
		{
			if (value != isChestRevealScreen)
			{
				if (value)
				{
					AddComponent(23, chestRevealScreenComponent);
				}
				else
				{
					RemoveComponent(23);
				}
			}
		}
	}

	public bool isChestShopScreen
	{
		get
		{
			return HasComponent(24);
		}
		set
		{
			if (value != isChestShopScreen)
			{
				if (value)
				{
					AddComponent(24, chestShopScreenComponent);
				}
				else
				{
					RemoveComponent(24);
				}
			}
		}
	}

	public bool isCloseScreen
	{
		get
		{
			return HasComponent(25);
		}
		set
		{
			if (value != isCloseScreen)
			{
				if (value)
				{
					AddComponent(25, closeScreenComponent);
				}
				else
				{
					RemoveComponent(25);
				}
			}
		}
	}

	public CoinsListenerComponent coinsListener
	{
		get
		{
			return (CoinsListenerComponent)GetComponent(1);
		}
	}

	public bool hasCoinsListener
	{
		get
		{
			return HasComponent(1);
		}
	}

	public bool isCompanionSelectScreen
	{
		get
		{
			return HasComponent(26);
		}
		set
		{
			if (value != isCompanionSelectScreen)
			{
				if (value)
				{
					AddComponent(26, companionSelectScreenComponent);
				}
				else
				{
					RemoveComponent(26);
				}
			}
		}
	}

	public bool isDestroyed
	{
		get
		{
			return HasComponent(2);
		}
		set
		{
			if (value != isDestroyed)
			{
				if (value)
				{
					AddComponent(2, destroyedComponent);
				}
				else
				{
					RemoveComponent(2);
				}
			}
		}
	}

	public bool isDisplayUDID
	{
		get
		{
			return HasComponent(3);
		}
		set
		{
			if (value != isDisplayUDID)
			{
				if (value)
				{
					AddComponent(3, displayUDIDComponent);
				}
				else
				{
					RemoveComponent(3);
				}
			}
		}
	}

	public DisplayUDIDListenerComponent displayUDIDListener
	{
		get
		{
			return (DisplayUDIDListenerComponent)GetComponent(4);
		}
	}

	public bool hasDisplayUDIDListener
	{
		get
		{
			return HasComponent(4);
		}
	}

	public FlyingElementEffectComponent flyingElementEffect
	{
		get
		{
			return (FlyingElementEffectComponent)GetComponent(27);
		}
	}

	public bool hasFlyingElementEffect
	{
		get
		{
			return HasComponent(27);
		}
	}

	public FreeChestStateListenerComponent freeChestStateListener
	{
		get
		{
			return (FreeChestStateListenerComponent)GetComponent(5);
		}
	}

	public bool hasFreeChestStateListener
	{
		get
		{
			return HasComponent(5);
		}
	}

	public HardCurrencyListenerComponent hardCurrencyListener
	{
		get
		{
			return (HardCurrencyListenerComponent)GetComponent(6);
		}
	}

	public bool hasHardCurrencyListener
	{
		get
		{
			return HasComponent(6);
		}
	}

	public bool isHomeScreen
	{
		get
		{
			return HasComponent(28);
		}
		set
		{
			if (value != isHomeScreen)
			{
				if (value)
				{
					AddComponent(28, homeScreenComponent);
				}
				else
				{
					RemoveComponent(28);
				}
			}
		}
	}

	public HUDComponent hUD
	{
		get
		{
			return (HUDComponent)GetComponent(29);
		}
	}

	public bool hasHUD
	{
		get
		{
			return HasComponent(29);
		}
	}

	public bool isIntialSceneDecider
	{
		get
		{
			return HasComponent(7);
		}
		set
		{
			if (value != isIntialSceneDecider)
			{
				if (value)
				{
					AddComponent(7, intialSceneDeciderComponent);
				}
				else
				{
					RemoveComponent(7);
				}
			}
		}
	}

	public bool isIntroDone
	{
		get
		{
			return HasComponent(8);
		}
		set
		{
			if (value != isIntroDone)
			{
				if (value)
				{
					AddComponent(8, introDoneComponent);
				}
				else
				{
					RemoveComponent(8);
				}
			}
		}
	}

	public bool isLoadingSpinnerHide
	{
		get
		{
			return HasComponent(9);
		}
		set
		{
			if (value != isLoadingSpinnerHide)
			{
				if (value)
				{
					AddComponent(9, loadingSpinnerHideComponent);
				}
				else
				{
					RemoveComponent(9);
				}
			}
		}
	}

	public LoadingSpinnerShowComponent loadingSpinnerShow
	{
		get
		{
			return (LoadingSpinnerShowComponent)GetComponent(10);
		}
	}

	public bool hasLoadingSpinnerShow
	{
		get
		{
			return HasComponent(10);
		}
	}

	public bool isNewSceneLoaded
	{
		get
		{
			return HasComponent(30);
		}
		set
		{
			if (value != isNewSceneLoaded)
			{
				if (value)
				{
					AddComponent(30, newSceneLoadedComponent);
				}
				else
				{
					RemoveComponent(30);
				}
			}
		}
	}

	public O7SdkAdTrackingActiveListenerComponent o7SdkAdTrackingActiveListener
	{
		get
		{
			return (O7SdkAdTrackingActiveListenerComponent)GetComponent(11);
		}
	}

	public bool hasO7SdkAdTrackingActiveListener
	{
		get
		{
			return HasComponent(11);
		}
	}

	public O7SdkAdTrackingListenerComponent o7SdkAdTrackingListener
	{
		get
		{
			return (O7SdkAdTrackingListenerComponent)GetComponent(12);
		}
	}

	public bool hasO7SdkAdTrackingListener
	{
		get
		{
			return HasComponent(12);
		}
	}

	public bool isO7SdkCall
	{
		get
		{
			return HasComponent(13);
		}
		set
		{
			if (value != isO7SdkCall)
			{
				if (value)
				{
					AddComponent(13, o7SdkCallComponent);
				}
				else
				{
					RemoveComponent(13);
				}
			}
		}
	}

	public O7SdkCountryCodeListenerComponent o7SdkCountryCodeListener
	{
		get
		{
			return (O7SdkCountryCodeListenerComponent)GetComponent(14);
		}
	}

	public bool hasO7SdkCountryCodeListener
	{
		get
		{
			return HasComponent(14);
		}
	}

	public O7SdkDataListenerComponent o7SdkDataListener
	{
		get
		{
			return (O7SdkDataListenerComponent)GetComponent(15);
		}
	}

	public bool hasO7SdkDataListener
	{
		get
		{
			return HasComponent(15);
		}
	}

	public O7SdkGdprAdProviderSelectionListenerComponent o7SdkGdprAdProviderSelectionListener
	{
		get
		{
			return (O7SdkGdprAdProviderSelectionListenerComponent)GetComponent(16);
		}
	}

	public bool hasO7SdkGdprAdProviderSelectionListener
	{
		get
		{
			return HasComponent(16);
		}
	}

	public O7SdkGdprOptOutListenerComponent o7SdkGdprOptOutListener
	{
		get
		{
			return (O7SdkGdprOptOutListenerComponent)GetComponent(17);
		}
	}

	public bool hasO7SdkGdprOptOutListener
	{
		get
		{
			return HasComponent(17);
		}
	}

	public bool isO7SdkGridButtonUpdateRequest
	{
		get
		{
			return HasComponent(18);
		}
		set
		{
			if (value != isO7SdkGridButtonUpdateRequest)
			{
				if (value)
				{
					AddComponent(18, o7SdkGridButtonUpdateRequestComponent);
				}
				else
				{
					RemoveComponent(18);
				}
			}
		}
	}

	public O7SdkManualNewsListenerComponent o7SdkManualNewsListener
	{
		get
		{
			return (O7SdkManualNewsListenerComponent)GetComponent(19);
		}
	}

	public bool hasO7SdkManualNewsListener
	{
		get
		{
			return HasComponent(19);
		}
	}

	public OpenedScreensComponent openedScreens
	{
		get
		{
			return (OpenedScreensComponent)GetComponent(31);
		}
	}

	public bool hasOpenedScreens
	{
		get
		{
			return HasComponent(31);
		}
	}

	public RaccoonIntrosListenerComponent raccoonIntrosListener
	{
		get
		{
			return (RaccoonIntrosListenerComponent)GetComponent(142);
		}
	}

	public bool hasRaccoonIntrosListener
	{
		get
		{
			return HasComponent(142);
		}
	}

	public bool isRefreshScreen
	{
		get
		{
			return HasComponent(32);
		}
		set
		{
			if (value != isRefreshScreen)
			{
				if (value)
				{
					AddComponent(32, refreshScreenComponent);
				}
				else
				{
					RemoveComponent(32);
				}
			}
		}
	}

	public bool isRelayPlayerScreen
	{
		get
		{
			return HasComponent(33);
		}
		set
		{
			if (value != isRelayPlayerScreen)
			{
				if (value)
				{
					AddComponent(33, relayPlayerScreenComponent);
				}
				else
				{
					RemoveComponent(33);
				}
			}
		}
	}

	public bool isResurectPlayerScreen
	{
		get
		{
			return HasComponent(34);
		}
		set
		{
			if (value != isResurectPlayerScreen)
			{
				if (value)
				{
					AddComponent(34, resurectPlayerScreenComponent);
				}
				else
				{
					RemoveComponent(34);
				}
			}
		}
	}

	public bool isRevivePlayerScreen
	{
		get
		{
			return HasComponent(35);
		}
		set
		{
			if (value != isRevivePlayerScreen)
			{
				if (value)
				{
					AddComponent(35, revivePlayerScreenComponent);
				}
				else
				{
					RemoveComponent(35);
				}
			}
		}
	}

	public SaveDataLoadedListenerComponent saveDataLoadedListener
	{
		get
		{
			return (SaveDataLoadedListenerComponent)GetComponent(143);
		}
	}

	public bool hasSaveDataLoadedListener
	{
		get
		{
			return HasComponent(143);
		}
	}

	public SceneChangeRequestListenerComponent sceneChangeRequestListener
	{
		get
		{
			return (SceneChangeRequestListenerComponent)GetComponent(144);
		}
	}

	public bool hasSceneChangeRequestListener
	{
		get
		{
			return HasComponent(144);
		}
	}

	public ScreenMapComponent screenMap
	{
		get
		{
			return (ScreenMapComponent)GetComponent(36);
		}
	}

	public bool hasScreenMap
	{
		get
		{
			return HasComponent(36);
		}
	}

	public bool isSettingsScreen
	{
		get
		{
			return HasComponent(37);
		}
		set
		{
			if (value != isSettingsScreen)
			{
				if (value)
				{
					AddComponent(37, settingsScreenComponent);
				}
				else
				{
					RemoveComponent(37);
				}
			}
		}
	}

	public SetupComponent setup
	{
		get
		{
			return (SetupComponent)GetComponent(38);
		}
	}

	public bool hasSetup
	{
		get
		{
			return HasComponent(38);
		}
	}

	public bool isShopScreen
	{
		get
		{
			return HasComponent(39);
		}
		set
		{
			if (value != isShopScreen)
			{
				if (value)
				{
					AddComponent(39, shopScreenComponent);
				}
				else
				{
					RemoveComponent(39);
				}
			}
		}
	}

	public StaminasListenerComponent staminasListener
	{
		get
		{
			return (StaminasListenerComponent)GetComponent(145);
		}
	}

	public bool hasStaminasListener
	{
		get
		{
			return HasComponent(145);
		}
	}

	public ToggleMusicsListenerComponent toggleMusicsListener
	{
		get
		{
			return (ToggleMusicsListenerComponent)GetComponent(146);
		}
	}

	public bool hasToggleMusicsListener
	{
		get
		{
			return HasComponent(146);
		}
	}

	public ToggleSoundsListenerComponent toggleSoundsListener
	{
		get
		{
			return (ToggleSoundsListenerComponent)GetComponent(147);
		}
	}

	public bool hasToggleSoundsListener
	{
		get
		{
			return HasComponent(147);
		}
	}

	public TutorialLayerComponent tutorialLayer
	{
		get
		{
			return (TutorialLayerComponent)GetComponent(40);
		}
	}

	public bool hasTutorialLayer
	{
		get
		{
			return HasComponent(40);
		}
	}

	public TutorialLevelPassedAvailabilityComponent tutorialLevelPassedAvailability
	{
		get
		{
			return (TutorialLevelPassedAvailabilityComponent)GetComponent(148);
		}
	}

	public bool hasTutorialLevelPassedAvailability
	{
		get
		{
			return HasComponent(148);
		}
	}

	public TutorialsListenerComponent tutorialsListener
	{
		get
		{
			return (TutorialsListenerComponent)GetComponent(149);
		}
	}

	public bool hasTutorialsListener
	{
		get
		{
			return HasComponent(149);
		}
	}

	public bool isUIAction
	{
		get
		{
			return HasComponent(41);
		}
		set
		{
			if (value != isUIAction)
			{
				if (value)
				{
					AddComponent(41, uIActionComponent);
				}
				else
				{
					RemoveComponent(41);
				}
			}
		}
	}

	public UIActionFinishedCallbackComponent uIActionFinishedCallback
	{
		get
		{
			return (UIActionFinishedCallbackComponent)GetComponent(42);
		}
	}

	public bool hasUIActionFinishedCallback
	{
		get
		{
			return HasComponent(42);
		}
	}

	public bool isUIChapterListRequest
	{
		get
		{
			return HasComponent(43);
		}
		set
		{
			if (value != isUIChapterListRequest)
			{
				if (value)
				{
					AddComponent(43, uIChapterListRequestComponent);
				}
				else
				{
					RemoveComponent(43);
				}
			}
		}
	}

	public UIChapterListResponseComponent uIChapterListResponse
	{
		get
		{
			return (UIChapterListResponseComponent)GetComponent(44);
		}
	}

	public bool hasUIChapterListResponse
	{
		get
		{
			return HasComponent(44);
		}
	}

	public UICharacterSelectedComponent uICharacterSelected
	{
		get
		{
			return (UICharacterSelectedComponent)GetComponent(45);
		}
	}

	public bool hasUICharacterSelected
	{
		get
		{
			return HasComponent(45);
		}
	}

	public UICharacterUnlockedComponent uICharacterUnlocked
	{
		get
		{
			return (UICharacterUnlockedComponent)GetComponent(46);
		}
	}

	public bool hasUICharacterUnlocked
	{
		get
		{
			return HasComponent(46);
		}
	}

	public UICharacterUpgradedComponent uICharacterUpgraded
	{
		get
		{
			return (UICharacterUpgradedComponent)GetComponent(47);
		}
	}

	public bool hasUICharacterUpgraded
	{
		get
		{
			return HasComponent(47);
		}
	}

	public bool isUICharListRequest
	{
		get
		{
			return HasComponent(48);
		}
		set
		{
			if (value != isUICharListRequest)
			{
				if (value)
				{
					AddComponent(48, uICharListRequestComponent);
				}
				else
				{
					RemoveComponent(48);
				}
			}
		}
	}

	public UICharListResponseComponent uICharListResponse
	{
		get
		{
			return (UICharListResponseComponent)GetComponent(49);
		}
	}

	public bool hasUICharListResponse
	{
		get
		{
			return HasComponent(49);
		}
	}

	public bool isUICharSelectedRequest
	{
		get
		{
			return HasComponent(50);
		}
		set
		{
			if (value != isUICharSelectedRequest)
			{
				if (value)
				{
					AddComponent(50, uICharSelectedRequestComponent);
				}
				else
				{
					RemoveComponent(50);
				}
			}
		}
	}

	public UICharSelectedResponseComponent uICharSelectedResponse
	{
		get
		{
			return (UICharSelectedResponseComponent)GetComponent(51);
		}
	}

	public bool hasUICharSelectedResponse
	{
		get
		{
			return HasComponent(51);
		}
	}

	public UICharSingleRequestComponent uICharSingleRequest
	{
		get
		{
			return (UICharSingleRequestComponent)GetComponent(52);
		}
	}

	public bool hasUICharSingleRequest
	{
		get
		{
			return HasComponent(52);
		}
	}

	public UICharSingleResponseComponent uICharSingleResponse
	{
		get
		{
			return (UICharSingleResponseComponent)GetComponent(53);
		}
	}

	public bool hasUICharSingleResponse
	{
		get
		{
			return HasComponent(53);
		}
	}

	public bool isUICheckPlayerProgress
	{
		get
		{
			return HasComponent(54);
		}
		set
		{
			if (value != isUICheckPlayerProgress)
			{
				if (value)
				{
					AddComponent(54, uICheckPlayerProgressComponent);
				}
				else
				{
					RemoveComponent(54);
				}
			}
		}
	}

	public UIChestBoughtComponent uIChestBought
	{
		get
		{
			return (UIChestBoughtComponent)GetComponent(55);
		}
	}

	public bool hasUIChestBought
	{
		get
		{
			return HasComponent(55);
		}
	}

	public bool isUIChestListRequest
	{
		get
		{
			return HasComponent(56);
		}
		set
		{
			if (value != isUIChestListRequest)
			{
				if (value)
				{
					AddComponent(56, uIChestListRequestComponent);
				}
				else
				{
					RemoveComponent(56);
				}
			}
		}
	}

	public UIChestListResponseComponent uIChestListResponse
	{
		get
		{
			return (UIChestListResponseComponent)GetComponent(57);
		}
	}

	public bool hasUIChestListResponse
	{
		get
		{
			return HasComponent(57);
		}
	}

	public UIChestRewardRequestComponent uIChestRewardRequest
	{
		get
		{
			return (UIChestRewardRequestComponent)GetComponent(58);
		}
	}

	public bool hasUIChestRewardRequest
	{
		get
		{
			return HasComponent(58);
		}
	}

	public UIChestRewardResponseComponent uIChestRewardResponse
	{
		get
		{
			return (UIChestRewardResponseComponent)GetComponent(59);
		}
	}

	public bool hasUIChestRewardResponse
	{
		get
		{
			return HasComponent(59);
		}
	}

	public UIChestWonComponent uIChestWon
	{
		get
		{
			return (UIChestWonComponent)GetComponent(60);
		}
	}

	public bool hasUIChestWon
	{
		get
		{
			return HasComponent(60);
		}
	}

	public UIClaimDailyMissionRewardComponent uIClaimDailyMissionReward
	{
		get
		{
			return (UIClaimDailyMissionRewardComponent)GetComponent(61);
		}
	}

	public bool hasUIClaimDailyMissionReward
	{
		get
		{
			return HasComponent(61);
		}
	}

	public UIClaimRewardComponent uIClaimReward
	{
		get
		{
			return (UIClaimRewardComponent)GetComponent(62);
		}
	}

	public bool hasUIClaimReward
	{
		get
		{
			return HasComponent(62);
		}
	}

	public bool isUICompanionListRequest
	{
		get
		{
			return HasComponent(63);
		}
		set
		{
			if (value != isUICompanionListRequest)
			{
				if (value)
				{
					AddComponent(63, uICompanionListRequestComponent);
				}
				else
				{
					RemoveComponent(63);
				}
			}
		}
	}

	public UICompanionListResponseComponent uICompanionListResponse
	{
		get
		{
			return (UICompanionListResponseComponent)GetComponent(64);
		}
	}

	public bool hasUICompanionListResponse
	{
		get
		{
			return HasComponent(64);
		}
	}

	public UICompanionSelectedComponent uICompanionSelected
	{
		get
		{
			return (UICompanionSelectedComponent)GetComponent(65);
		}
	}

	public bool hasUICompanionSelected
	{
		get
		{
			return HasComponent(65);
		}
	}

	public UICompanionUnlockedComponent uICompanionUnlocked
	{
		get
		{
			return (UICompanionUnlockedComponent)GetComponent(66);
		}
	}

	public bool hasUICompanionUnlocked
	{
		get
		{
			return HasComponent(66);
		}
	}

	public UICompanionUpgradedComponent uICompanionUpgraded
	{
		get
		{
			return (UICompanionUpgradedComponent)GetComponent(67);
		}
	}

	public bool hasUICompanionUpgraded
	{
		get
		{
			return HasComponent(67);
		}
	}

	public bool isUI
	{
		get
		{
			return HasComponent(68);
		}
		set
		{
			if (value != isUI)
			{
				if (value)
				{
					AddComponent(68, uIComponent);
				}
				else
				{
					RemoveComponent(68);
				}
			}
		}
	}

	public bool isUICompSelectedRequest
	{
		get
		{
			return HasComponent(69);
		}
		set
		{
			if (value != isUICompSelectedRequest)
			{
				if (value)
				{
					AddComponent(69, uICompSelectedRequestComponent);
				}
				else
				{
					RemoveComponent(69);
				}
			}
		}
	}

	public UICompSelectedResponseComponent uICompSelectedResponse
	{
		get
		{
			return (UICompSelectedResponseComponent)GetComponent(70);
		}
	}

	public bool hasUICompSelectedResponse
	{
		get
		{
			return HasComponent(70);
		}
	}

	public UICompSingleRequestComponent uICompSingleRequest
	{
		get
		{
			return (UICompSingleRequestComponent)GetComponent(71);
		}
	}

	public bool hasUICompSingleRequest
	{
		get
		{
			return HasComponent(71);
		}
	}

	public UICompSingleResponseComponent uICompSingleResponse
	{
		get
		{
			return (UICompSingleResponseComponent)GetComponent(72);
		}
	}

	public bool hasUICompSingleResponse
	{
		get
		{
			return HasComponent(72);
		}
	}

	public bool isUICurrentDailyMissionsRequest
	{
		get
		{
			return HasComponent(73);
		}
		set
		{
			if (value != isUICurrentDailyMissionsRequest)
			{
				if (value)
				{
					AddComponent(73, uICurrentDailyMissionsRequestComponent);
				}
				else
				{
					RemoveComponent(73);
				}
			}
		}
	}

	public UICurrentDailyMissionsResponse uICurrentDailyMissionsResponse
	{
		get
		{
			return (UICurrentDailyMissionsResponse)GetComponent(74);
		}
	}

	public bool hasUICurrentDailyMissionsResponse
	{
		get
		{
			return HasComponent(74);
		}
	}

	public UIDisplayAbilityComponent uIDisplayAbility
	{
		get
		{
			return (UIDisplayAbilityComponent)GetComponent(75);
		}
	}

	public bool hasUIDisplayAbility
	{
		get
		{
			return HasComponent(75);
		}
	}

	public UIDisplayExplanation uIDisplayExplanation
	{
		get
		{
			return (UIDisplayExplanation)GetComponent(76);
		}
	}

	public bool hasUIDisplayExplanation
	{
		get
		{
			return HasComponent(76);
		}
	}

	public UIDisplayMissionDetails uIDisplayMissionDetails
	{
		get
		{
			return (UIDisplayMissionDetails)GetComponent(77);
		}
	}

	public bool hasUIDisplayMissionDetails
	{
		get
		{
			return HasComponent(77);
		}
	}

	public UIDisplayTransactionConfrimation uIDisplayTransactionConfrimation
	{
		get
		{
			return (UIDisplayTransactionConfrimation)GetComponent(78);
		}
	}

	public bool hasUIDisplayTransactionConfrimation
	{
		get
		{
			return HasComponent(78);
		}
	}

	public bool isUIExplanationPopupShown
	{
		get
		{
			return HasComponent(79);
		}
		set
		{
			if (value != isUIExplanationPopupShown)
			{
				if (value)
				{
					AddComponent(79, uIExplanationPopupShownComponent);
				}
				else
				{
					RemoveComponent(79);
				}
			}
		}
	}

	public bool isUIFreeChestOpened
	{
		get
		{
			return HasComponent(80);
		}
		set
		{
			if (value != isUIFreeChestOpened)
			{
				if (value)
				{
					AddComponent(80, uIFreeChestOpenedComponent);
				}
				else
				{
					RemoveComponent(80);
				}
			}
		}
	}

	public bool isUIFreeChestRequest
	{
		get
		{
			return HasComponent(81);
		}
		set
		{
			if (value != isUIFreeChestRequest)
			{
				if (value)
				{
					AddComponent(81, uIFreeChestRequestComponent);
				}
				else
				{
					RemoveComponent(81);
				}
			}
		}
	}

	public UIFreeChestResponseComponent uIFreeChestResponse
	{
		get
		{
			return (UIFreeChestResponseComponent)GetComponent(82);
		}
	}

	public bool hasUIFreeChestResponse
	{
		get
		{
			return HasComponent(82);
		}
	}

	public bool isUIGiveDailyMissionsDailyReward
	{
		get
		{
			return HasComponent(83);
		}
		set
		{
			if (value != isUIGiveDailyMissionsDailyReward)
			{
				if (value)
				{
					AddComponent(83, uIGiveDailyMissionsDailyRewardComponent);
				}
				else
				{
					RemoveComponent(83);
				}
			}
		}
	}

	public bool isUIHideAbility
	{
		get
		{
			return HasComponent(84);
		}
		set
		{
			if (value != isUIHideAbility)
			{
				if (value)
				{
					AddComponent(84, uIHideAbilityComponent);
				}
				else
				{
					RemoveComponent(84);
				}
			}
		}
	}

	public bool isUIHideExplanation
	{
		get
		{
			return HasComponent(85);
		}
		set
		{
			if (value != isUIHideExplanation)
			{
				if (value)
				{
					AddComponent(85, uIHideExplanationComponent);
				}
				else
				{
					RemoveComponent(85);
				}
			}
		}
	}

	public bool isUIHideMissionsDetails
	{
		get
		{
			return HasComponent(86);
		}
		set
		{
			if (value != isUIHideMissionsDetails)
			{
				if (value)
				{
					AddComponent(86, uIHideMissionsDetailsComponent);
				}
				else
				{
					RemoveComponent(86);
				}
			}
		}
	}

	public bool isUIHideTransactionConfirmation
	{
		get
		{
			return HasComponent(87);
		}
		set
		{
			if (value != isUIHideTransactionConfirmation)
			{
				if (value)
				{
					AddComponent(87, uIHideTransactionConfirmationComponent);
				}
				else
				{
					RemoveComponent(87);
				}
			}
		}
	}

	public UIIAPBought uIIAPBought
	{
		get
		{
			return (UIIAPBought)GetComponent(88);
		}
	}

	public bool hasUIIAPBought
	{
		get
		{
			return HasComponent(88);
		}
	}

	public bool isUILastSelectedMissionRequest
	{
		get
		{
			return HasComponent(89);
		}
		set
		{
			if (value != isUILastSelectedMissionRequest)
			{
				if (value)
				{
					AddComponent(89, uILastSelectedMissionRequestComponent);
				}
				else
				{
					RemoveComponent(89);
				}
			}
		}
	}

	public UILastSelectedMissionResponseComponent uILastSelectedMissionResponse
	{
		get
		{
			return (UILastSelectedMissionResponseComponent)GetComponent(90);
		}
	}

	public bool hasUILastSelectedMissionResponse
	{
		get
		{
			return HasComponent(90);
		}
	}

	public bool isUIMissionListRequest
	{
		get
		{
			return HasComponent(91);
		}
		set
		{
			if (value != isUIMissionListRequest)
			{
				if (value)
				{
					AddComponent(91, uIMissionListRequestComponent);
				}
				else
				{
					RemoveComponent(91);
				}
			}
		}
	}

	public UIMissionListResponseComponent uIMissionListResponse
	{
		get
		{
			return (UIMissionListResponseComponent)GetComponent(92);
		}
	}

	public bool hasUIMissionListResponse
	{
		get
		{
			return HasComponent(92);
		}
	}

	public bool isUINewCharactersUnlockedRequest
	{
		get
		{
			return HasComponent(93);
		}
		set
		{
			if (value != isUINewCharactersUnlockedRequest)
			{
				if (value)
				{
					AddComponent(93, uINewCharactersUnlockedRequestComponent);
				}
				else
				{
					RemoveComponent(93);
				}
			}
		}
	}

	public UINewCharactersUnlockedResponseComponent uINewCharactersUnlockedResponse
	{
		get
		{
			return (UINewCharactersUnlockedResponseComponent)GetComponent(94);
		}
	}

	public bool hasUINewCharactersUnlockedResponse
	{
		get
		{
			return HasComponent(94);
		}
	}

	public UINewCompanionsUnlockedResponseComponent uINewCompanionsUnlockedResponse
	{
		get
		{
			return (UINewCompanionsUnlockedResponseComponent)GetComponent(95);
		}
	}

	public bool hasUINewCompanionsUnlockedResponse
	{
		get
		{
			return HasComponent(95);
		}
	}

	public bool isUINewCompanionUnlockedRequest
	{
		get
		{
			return HasComponent(96);
		}
		set
		{
			if (value != isUINewCompanionUnlockedRequest)
			{
				if (value)
				{
					AddComponent(96, uINewCompanionUnlockedRequestComponent);
				}
				else
				{
					RemoveComponent(96);
				}
			}
		}
	}

	public bool isUINewMissionUnlockedRequest
	{
		get
		{
			return HasComponent(97);
		}
		set
		{
			if (value != isUINewMissionUnlockedRequest)
			{
				if (value)
				{
					AddComponent(97, uINewMissionUnlockedRequestComponent);
				}
				else
				{
					RemoveComponent(97);
				}
			}
		}
	}

	public UINewMissionUnlockedResponseComponent uINewMissionUnlockedResponse
	{
		get
		{
			return (UINewMissionUnlockedResponseComponent)GetComponent(98);
		}
	}

	public bool hasUINewMissionUnlockedResponse
	{
		get
		{
			return HasComponent(98);
		}
	}

	public UINewUnlockedCharacterDisplayed uINewUnlockedCharacterDisplayed
	{
		get
		{
			return (UINewUnlockedCharacterDisplayed)GetComponent(99);
		}
	}

	public bool hasUINewUnlockedCharacterDisplayed
	{
		get
		{
			return HasComponent(99);
		}
	}

	public UINewUnlockedCompanionDisplayed uINewUnlockedCompanionDisplayed
	{
		get
		{
			return (UINewUnlockedCompanionDisplayed)GetComponent(100);
		}
	}

	public bool hasUINewUnlockedCompanionDisplayed
	{
		get
		{
			return HasComponent(100);
		}
	}

	public bool isUIOpenCharSelection
	{
		get
		{
			return HasComponent(101);
		}
		set
		{
			if (value != isUIOpenCharSelection)
			{
				if (value)
				{
					AddComponent(101, uIOpenCharSelectionComponent);
				}
				else
				{
					RemoveComponent(101);
				}
			}
		}
	}

	public bool isUIOpenChestShop
	{
		get
		{
			return HasComponent(102);
		}
		set
		{
			if (value != isUIOpenChestShop)
			{
				if (value)
				{
					AddComponent(102, uIOpenChestShopComponent);
				}
				else
				{
					RemoveComponent(102);
				}
			}
		}
	}

	public bool isUIOpenCompSelection
	{
		get
		{
			return HasComponent(103);
		}
		set
		{
			if (value != isUIOpenCompSelection)
			{
				if (value)
				{
					AddComponent(103, uIOpenCompSelectionComponent);
				}
				else
				{
					RemoveComponent(103);
				}
			}
		}
	}

	public bool isUIOpenContacts
	{
		get
		{
			return HasComponent(104);
		}
		set
		{
			if (value != isUIOpenContacts)
			{
				if (value)
				{
					AddComponent(104, uIOpenContactsComponent);
				}
				else
				{
					RemoveComponent(104);
				}
			}
		}
	}

	public bool isUIOpenCountryCodes
	{
		get
		{
			return HasComponent(105);
		}
		set
		{
			if (value != isUIOpenCountryCodes)
			{
				if (value)
				{
					AddComponent(105, uIOpenCountryCodesComponent);
				}
				else
				{
					RemoveComponent(105);
				}
			}
		}
	}

	public bool isUIOpenDailyMissions
	{
		get
		{
			return HasComponent(106);
		}
		set
		{
			if (value != isUIOpenDailyMissions)
			{
				if (value)
				{
					AddComponent(106, uIOpenDailyMissionsComponent);
				}
				else
				{
					RemoveComponent(106);
				}
			}
		}
	}

	public bool isUIOpenEPrivacy
	{
		get
		{
			return HasComponent(107);
		}
		set
		{
			if (value != isUIOpenEPrivacy)
			{
				if (value)
				{
					AddComponent(107, uIOpenEPrivacyComponent);
				}
				else
				{
					RemoveComponent(107);
				}
			}
		}
	}

	public bool isUIOpenEula
	{
		get
		{
			return HasComponent(108);
		}
		set
		{
			if (value != isUIOpenEula)
			{
				if (value)
				{
					AddComponent(108, uIOpenEulaComponent);
				}
				else
				{
					RemoveComponent(108);
				}
			}
		}
	}

	public bool isUIOpenGdpr
	{
		get
		{
			return HasComponent(109);
		}
		set
		{
			if (value != isUIOpenGdpr)
			{
				if (value)
				{
					AddComponent(109, uIOpenGdprComponent);
				}
				else
				{
					RemoveComponent(109);
				}
			}
		}
	}

	public bool isUIOpenPrivacyAndLegacy
	{
		get
		{
			return HasComponent(110);
		}
		set
		{
			if (value != isUIOpenPrivacyAndLegacy)
			{
				if (value)
				{
					AddComponent(110, uIOpenPrivacyAndLegacyComponent);
				}
				else
				{
					RemoveComponent(110);
				}
			}
		}
	}

	public bool isUIOpenPrivacyPolicy
	{
		get
		{
			return HasComponent(111);
		}
		set
		{
			if (value != isUIOpenPrivacyPolicy)
			{
				if (value)
				{
					AddComponent(111, uIOpenPrivacyPolicyComponent);
				}
				else
				{
					RemoveComponent(111);
				}
			}
		}
	}

	public bool isUIOpenPvE
	{
		get
		{
			return HasComponent(112);
		}
		set
		{
			if (value != isUIOpenPvE)
			{
				if (value)
				{
					AddComponent(112, uIOpenPvEComponent);
				}
				else
				{
					RemoveComponent(112);
				}
			}
		}
	}

	public bool isUIOpenSettings
	{
		get
		{
			return HasComponent(113);
		}
		set
		{
			if (value != isUIOpenSettings)
			{
				if (value)
				{
					AddComponent(113, uIOpenSettingsComponent);
				}
				else
				{
					RemoveComponent(113);
				}
			}
		}
	}

	public UIOpenShopComponent uIOpenShop
	{
		get
		{
			return (UIOpenShopComponent)GetComponent(114);
		}
	}

	public bool hasUIOpenShop
	{
		get
		{
			return HasComponent(114);
		}
	}

	public UIPlayerLevelComponent uIPlayerLevel
	{
		get
		{
			return (UIPlayerLevelComponent)GetComponent(115);
		}
	}

	public bool hasUIPlayerLevel
	{
		get
		{
			return HasComponent(115);
		}
	}

	public bool isUIPlayerProgressionStateRequest
	{
		get
		{
			return HasComponent(116);
		}
		set
		{
			if (value != isUIPlayerProgressionStateRequest)
			{
				if (value)
				{
					AddComponent(116, uIPlayerProgressionStateRequestComponent);
				}
				else
				{
					RemoveComponent(116);
				}
			}
		}
	}

	public UIPlayerProgressionStateResponseComponent uIPlayerProgressionStateResponse
	{
		get
		{
			return (UIPlayerProgressionStateResponseComponent)GetComponent(117);
		}
	}

	public bool hasUIPlayerProgressionStateResponse
	{
		get
		{
			return HasComponent(117);
		}
	}

	public UIPlayMissionComponent uIPlayMission
	{
		get
		{
			return (UIPlayMissionComponent)GetComponent(118);
		}
	}

	public bool hasUIPlayMission
	{
		get
		{
			return HasComponent(118);
		}
	}

	public UIRefillCharComponent uIRefillChar
	{
		get
		{
			return (UIRefillCharComponent)GetComponent(119);
		}
	}

	public bool hasUIRefillChar
	{
		get
		{
			return HasComponent(119);
		}
	}

	public UIRewardCollectedComponent uIRewardCollected
	{
		get
		{
			return (UIRewardCollectedComponent)GetComponent(120);
		}
	}

	public bool hasUIRewardCollected
	{
		get
		{
			return HasComponent(120);
		}
	}

	public UISettingsFooterPressComponent uISettingsFooterPress
	{
		get
		{
			return (UISettingsFooterPressComponent)GetComponent(121);
		}
	}

	public bool hasUISettingsFooterPress
	{
		get
		{
			return HasComponent(121);
		}
	}

	public bool isUIShopIAPsListRequest
	{
		get
		{
			return HasComponent(122);
		}
		set
		{
			if (value != isUIShopIAPsListRequest)
			{
				if (value)
				{
					AddComponent(122, uIShopIAPsListRequestComponent);
				}
				else
				{
					RemoveComponent(122);
				}
			}
		}
	}

	public UIShopIAPsListResponseComponent uIShopIAPsListResponse
	{
		get
		{
			return (UIShopIAPsListResponseComponent)GetComponent(123);
		}
	}

	public bool hasUIShopIAPsListResponse
	{
		get
		{
			return HasComponent(123);
		}
	}

	public bool isUIShopScreenExit
	{
		get
		{
			return HasComponent(124);
		}
		set
		{
			if (value != isUIShopScreenExit)
			{
				if (value)
				{
					AddComponent(124, uIShopScreenExitComponent);
				}
				else
				{
					RemoveComponent(124);
				}
			}
		}
	}

	public bool isUIShopSoftCurrencyPacksRequest
	{
		get
		{
			return HasComponent(125);
		}
		set
		{
			if (value != isUIShopSoftCurrencyPacksRequest)
			{
				if (value)
				{
					AddComponent(125, uIShopSoftCurrencyPacksRequestComponent);
				}
				else
				{
					RemoveComponent(125);
				}
			}
		}
	}

	public UIShopSoftCurrencyPacksResponseComponent uIShopSoftCurrencyPacksResponse
	{
		get
		{
			return (UIShopSoftCurrencyPacksResponseComponent)GetComponent(126);
		}
	}

	public bool hasUIShopSoftCurrencyPacksResponse
	{
		get
		{
			return HasComponent(126);
		}
	}

	public UIShowCharacterInfoComponent uIShowCharacterInfo
	{
		get
		{
			return (UIShowCharacterInfoComponent)GetComponent(127);
		}
	}

	public bool hasUIShowCharacterInfo
	{
		get
		{
			return HasComponent(127);
		}
	}

	public UIShowCompanionInfoComponent uIShowCompanionInfo
	{
		get
		{
			return (UIShowCompanionInfoComponent)GetComponent(128);
		}
	}

	public bool hasUIShowCompanionInfo
	{
		get
		{
			return HasComponent(128);
		}
	}

	public UIShowCurrencyUpdateComponent uIShowCurrencyUpdate
	{
		get
		{
			return (UIShowCurrencyUpdateComponent)GetComponent(129);
		}
	}

	public bool hasUIShowCurrencyUpdate
	{
		get
		{
			return HasComponent(129);
		}
	}

	public UISoftCurrencyPackBought uISoftCurrencyPackBought
	{
		get
		{
			return (UISoftCurrencyPackBought)GetComponent(130);
		}
	}

	public bool hasUISoftCurrencyPackBought
	{
		get
		{
			return HasComponent(130);
		}
	}

	public bool isUITotalCoinsRequest
	{
		get
		{
			return HasComponent(131);
		}
		set
		{
			if (value != isUITotalCoinsRequest)
			{
				if (value)
				{
					AddComponent(131, uITotalCoinsRequestComponent);
				}
				else
				{
					RemoveComponent(131);
				}
			}
		}
	}

	public UITotalCoinsResponseComponent uITotalCoinsResponse
	{
		get
		{
			return (UITotalCoinsResponseComponent)GetComponent(132);
		}
	}

	public bool hasUITotalCoinsResponse
	{
		get
		{
			return HasComponent(132);
		}
	}

	public bool isUITotalGemsRequest
	{
		get
		{
			return HasComponent(133);
		}
		set
		{
			if (value != isUITotalGemsRequest)
			{
				if (value)
				{
					AddComponent(133, uITotalGemsRequestComponent);
				}
				else
				{
					RemoveComponent(133);
				}
			}
		}
	}

	public UITotalGemsResponseComponent uITotalGemsResponse
	{
		get
		{
			return (UITotalGemsResponseComponent)GetComponent(134);
		}
	}

	public bool hasUITotalGemsResponse
	{
		get
		{
			return HasComponent(134);
		}
	}

	public bool isUITotalStarsRequest
	{
		get
		{
			return HasComponent(135);
		}
		set
		{
			if (value != isUITotalStarsRequest)
			{
				if (value)
				{
					AddComponent(135, uITotalStarsRequestComponent);
				}
				else
				{
					RemoveComponent(135);
				}
			}
		}
	}

	public UITotalStarsResponseComponent uITotalStarsResponse
	{
		get
		{
			return (UITotalStarsResponseComponent)GetComponent(136);
		}
	}

	public bool hasUITotalStarsResponse
	{
		get
		{
			return HasComponent(136);
		}
	}

	public bool isUIUnlockedMissionRequest
	{
		get
		{
			return HasComponent(137);
		}
		set
		{
			if (value != isUIUnlockedMissionRequest)
			{
				if (value)
				{
					AddComponent(137, uIUnlockedMissionRequestComponent);
				}
				else
				{
					RemoveComponent(137);
				}
			}
		}
	}

	public UIUnlockedMissionResponseComponent uIUnlockedMissionResponse
	{
		get
		{
			return (UIUnlockedMissionResponseComponent)GetComponent(138);
		}
	}

	public bool hasUIUnlockedMissionResponse
	{
		get
		{
			return HasComponent(138);
		}
	}

	public bool isUIUnlockNewMission
	{
		get
		{
			return HasComponent(139);
		}
		set
		{
			if (value != isUIUnlockNewMission)
			{
				if (value)
				{
					AddComponent(139, uIUnlockNewMissionComponent);
				}
				else
				{
					RemoveComponent(139);
				}
			}
		}
	}

	public bool isWinChestOnResultScreen
	{
		get
		{
			return HasComponent(140);
		}
		set
		{
			if (value != isWinChestOnResultScreen)
			{
				if (value)
				{
					AddComponent(140, winChestOnResultScreenComponent);
				}
				else
				{
					RemoveComponent(140);
				}
			}
		}
	}

	public bool isWorldSelectScreen
	{
		get
		{
			return HasComponent(141);
		}
		set
		{
			if (value != isWorldSelectScreen)
			{
				if (value)
				{
					AddComponent(141, worldSelectScreenComponent);
				}
				else
				{
					RemoveComponent(141);
				}
			}
		}
	}

	public void AddAsyncRequest(ResourceRequest newRequest, Action newCallback)
	{
		int index = 0;
		AsyncRequestComponent asyncRequestComponent = CreateComponent<AsyncRequestComponent>(index);
		asyncRequestComponent.Request = newRequest;
		asyncRequestComponent.callback = newCallback;
		AddComponent(index, asyncRequestComponent);
	}

	public void ReplaceAsyncRequest(ResourceRequest newRequest, Action newCallback)
	{
		int index = 0;
		AsyncRequestComponent asyncRequestComponent = CreateComponent<AsyncRequestComponent>(index);
		asyncRequestComponent.Request = newRequest;
		asyncRequestComponent.callback = newCallback;
		ReplaceComponent(index, asyncRequestComponent);
	}

	public void RemoveAsyncRequest()
	{
		RemoveComponent(0);
	}

	public void AddChangeScene(string newName, bool newInitialLoading)
	{
		int index = 21;
		ChangeSceneComponent changeSceneComponent = CreateComponent<ChangeSceneComponent>(index);
		changeSceneComponent.Name = newName;
		changeSceneComponent.InitialLoading = newInitialLoading;
		AddComponent(index, changeSceneComponent);
	}

	public void ReplaceChangeScene(string newName, bool newInitialLoading)
	{
		int index = 21;
		ChangeSceneComponent changeSceneComponent = CreateComponent<ChangeSceneComponent>(index);
		changeSceneComponent.Name = newName;
		changeSceneComponent.InitialLoading = newInitialLoading;
		ReplaceComponent(index, changeSceneComponent);
	}

	public void RemoveChangeScene()
	{
		RemoveComponent(21);
	}

	public void AddCoinsListener(ICoinListener newListener)
	{
		int index = 1;
		CoinsListenerComponent coinsListenerComponent = CreateComponent<CoinsListenerComponent>(index);
		coinsListenerComponent.listener = newListener;
		AddComponent(index, coinsListenerComponent);
	}

	public void ReplaceCoinsListener(ICoinListener newListener)
	{
		int index = 1;
		CoinsListenerComponent coinsListenerComponent = CreateComponent<CoinsListenerComponent>(index);
		coinsListenerComponent.listener = newListener;
		ReplaceComponent(index, coinsListenerComponent);
	}

	public void RemoveCoinsListener()
	{
		RemoveComponent(1);
	}

	public void AddDisplayUDIDListener(IDisplayUDIDListener newListener)
	{
		int index = 4;
		DisplayUDIDListenerComponent displayUDIDListenerComponent = CreateComponent<DisplayUDIDListenerComponent>(index);
		displayUDIDListenerComponent.listener = newListener;
		AddComponent(index, displayUDIDListenerComponent);
	}

	public void ReplaceDisplayUDIDListener(IDisplayUDIDListener newListener)
	{
		int index = 4;
		DisplayUDIDListenerComponent displayUDIDListenerComponent = CreateComponent<DisplayUDIDListenerComponent>(index);
		displayUDIDListenerComponent.listener = newListener;
		ReplaceComponent(index, displayUDIDListenerComponent);
	}

	public void RemoveDisplayUDIDListener()
	{
		RemoveComponent(4);
	}

	public void AddFlyingElementEffect(GameObject newElementPrefab, RectTransform newStartPoint, RectTransform newEndPoint, int newElementsAmount, float newEffectDuration, Action newCallback)
	{
		int index = 27;
		FlyingElementEffectComponent flyingElementEffectComponent = CreateComponent<FlyingElementEffectComponent>(index);
		flyingElementEffectComponent.ElementPrefab = newElementPrefab;
		flyingElementEffectComponent.StartPoint = newStartPoint;
		flyingElementEffectComponent.EndPoint = newEndPoint;
		flyingElementEffectComponent.ElementsAmount = newElementsAmount;
		flyingElementEffectComponent.EffectDuration = newEffectDuration;
		flyingElementEffectComponent.Callback = newCallback;
		AddComponent(index, flyingElementEffectComponent);
	}

	public void ReplaceFlyingElementEffect(GameObject newElementPrefab, RectTransform newStartPoint, RectTransform newEndPoint, int newElementsAmount, float newEffectDuration, Action newCallback)
	{
		int index = 27;
		FlyingElementEffectComponent flyingElementEffectComponent = CreateComponent<FlyingElementEffectComponent>(index);
		flyingElementEffectComponent.ElementPrefab = newElementPrefab;
		flyingElementEffectComponent.StartPoint = newStartPoint;
		flyingElementEffectComponent.EndPoint = newEndPoint;
		flyingElementEffectComponent.ElementsAmount = newElementsAmount;
		flyingElementEffectComponent.EffectDuration = newEffectDuration;
		flyingElementEffectComponent.Callback = newCallback;
		ReplaceComponent(index, flyingElementEffectComponent);
	}

	public void RemoveFlyingElementEffect()
	{
		RemoveComponent(27);
	}

	public void AddFreeChestStateListener(IFreeChestStateListener newListener)
	{
		int index = 5;
		FreeChestStateListenerComponent freeChestStateListenerComponent = CreateComponent<FreeChestStateListenerComponent>(index);
		freeChestStateListenerComponent.listener = newListener;
		AddComponent(index, freeChestStateListenerComponent);
	}

	public void ReplaceFreeChestStateListener(IFreeChestStateListener newListener)
	{
		int index = 5;
		FreeChestStateListenerComponent freeChestStateListenerComponent = CreateComponent<FreeChestStateListenerComponent>(index);
		freeChestStateListenerComponent.listener = newListener;
		ReplaceComponent(index, freeChestStateListenerComponent);
	}

	public void RemoveFreeChestStateListener()
	{
		RemoveComponent(5);
	}

	public void AddHardCurrencyListener(IHardCurrencyListener newListener)
	{
		int index = 6;
		HardCurrencyListenerComponent hardCurrencyListenerComponent = CreateComponent<HardCurrencyListenerComponent>(index);
		hardCurrencyListenerComponent.listener = newListener;
		AddComponent(index, hardCurrencyListenerComponent);
	}

	public void ReplaceHardCurrencyListener(IHardCurrencyListener newListener)
	{
		int index = 6;
		HardCurrencyListenerComponent hardCurrencyListenerComponent = CreateComponent<HardCurrencyListenerComponent>(index);
		hardCurrencyListenerComponent.listener = newListener;
		ReplaceComponent(index, hardCurrencyListenerComponent);
	}

	public void RemoveHardCurrencyListener()
	{
		RemoveComponent(6);
	}

	public void AddHUD(BaseDialog newHud)
	{
		int index = 29;
		HUDComponent hUDComponent = CreateComponent<HUDComponent>(index);
		hUDComponent.hud = newHud;
		AddComponent(index, hUDComponent);
	}

	public void ReplaceHUD(BaseDialog newHud)
	{
		int index = 29;
		HUDComponent hUDComponent = CreateComponent<HUDComponent>(index);
		hUDComponent.hud = newHud;
		ReplaceComponent(index, hUDComponent);
	}

	public void RemoveHUD()
	{
		RemoveComponent(29);
	}

	public void AddLoadingSpinnerShow(bool newBlockRaycasts)
	{
		int index = 10;
		LoadingSpinnerShowComponent loadingSpinnerShowComponent = CreateComponent<LoadingSpinnerShowComponent>(index);
		loadingSpinnerShowComponent.blockRaycasts = newBlockRaycasts;
		AddComponent(index, loadingSpinnerShowComponent);
	}

	public void ReplaceLoadingSpinnerShow(bool newBlockRaycasts)
	{
		int index = 10;
		LoadingSpinnerShowComponent loadingSpinnerShowComponent = CreateComponent<LoadingSpinnerShowComponent>(index);
		loadingSpinnerShowComponent.blockRaycasts = newBlockRaycasts;
		ReplaceComponent(index, loadingSpinnerShowComponent);
	}

	public void RemoveLoadingSpinnerShow()
	{
		RemoveComponent(10);
	}

	public void AddO7SdkAdTrackingActiveListener(IO7SdkAdTrackingActiveListener newListener)
	{
		int index = 11;
		O7SdkAdTrackingActiveListenerComponent o7SdkAdTrackingActiveListenerComponent = CreateComponent<O7SdkAdTrackingActiveListenerComponent>(index);
		o7SdkAdTrackingActiveListenerComponent.listener = newListener;
		AddComponent(index, o7SdkAdTrackingActiveListenerComponent);
	}

	public void ReplaceO7SdkAdTrackingActiveListener(IO7SdkAdTrackingActiveListener newListener)
	{
		int index = 11;
		O7SdkAdTrackingActiveListenerComponent o7SdkAdTrackingActiveListenerComponent = CreateComponent<O7SdkAdTrackingActiveListenerComponent>(index);
		o7SdkAdTrackingActiveListenerComponent.listener = newListener;
		ReplaceComponent(index, o7SdkAdTrackingActiveListenerComponent);
	}

	public void RemoveO7SdkAdTrackingActiveListener()
	{
		RemoveComponent(11);
	}

	public void AddO7SdkAdTrackingListener(IO7SdkAdTrackingListener newListener)
	{
		int index = 12;
		O7SdkAdTrackingListenerComponent o7SdkAdTrackingListenerComponent = CreateComponent<O7SdkAdTrackingListenerComponent>(index);
		o7SdkAdTrackingListenerComponent.listener = newListener;
		AddComponent(index, o7SdkAdTrackingListenerComponent);
	}

	public void ReplaceO7SdkAdTrackingListener(IO7SdkAdTrackingListener newListener)
	{
		int index = 12;
		O7SdkAdTrackingListenerComponent o7SdkAdTrackingListenerComponent = CreateComponent<O7SdkAdTrackingListenerComponent>(index);
		o7SdkAdTrackingListenerComponent.listener = newListener;
		ReplaceComponent(index, o7SdkAdTrackingListenerComponent);
	}

	public void RemoveO7SdkAdTrackingListener()
	{
		RemoveComponent(12);
	}

	public void AddO7SdkCountryCodeListener(IO7SdkCountryCodeListener newListener)
	{
		int index = 14;
		O7SdkCountryCodeListenerComponent o7SdkCountryCodeListenerComponent = CreateComponent<O7SdkCountryCodeListenerComponent>(index);
		o7SdkCountryCodeListenerComponent.listener = newListener;
		AddComponent(index, o7SdkCountryCodeListenerComponent);
	}

	public void ReplaceO7SdkCountryCodeListener(IO7SdkCountryCodeListener newListener)
	{
		int index = 14;
		O7SdkCountryCodeListenerComponent o7SdkCountryCodeListenerComponent = CreateComponent<O7SdkCountryCodeListenerComponent>(index);
		o7SdkCountryCodeListenerComponent.listener = newListener;
		ReplaceComponent(index, o7SdkCountryCodeListenerComponent);
	}

	public void RemoveO7SdkCountryCodeListener()
	{
		RemoveComponent(14);
	}

	public void AddO7SdkDataListener(IO7SdkVideoGalleryListener newListener)
	{
		int index = 15;
		O7SdkDataListenerComponent o7SdkDataListenerComponent = CreateComponent<O7SdkDataListenerComponent>(index);
		o7SdkDataListenerComponent.listener = newListener;
		AddComponent(index, o7SdkDataListenerComponent);
	}

	public void ReplaceO7SdkDataListener(IO7SdkVideoGalleryListener newListener)
	{
		int index = 15;
		O7SdkDataListenerComponent o7SdkDataListenerComponent = CreateComponent<O7SdkDataListenerComponent>(index);
		o7SdkDataListenerComponent.listener = newListener;
		ReplaceComponent(index, o7SdkDataListenerComponent);
	}

	public void RemoveO7SdkDataListener()
	{
		RemoveComponent(15);
	}

	public void AddO7SdkGdprAdProviderSelectionListener(IO7SdkGdprAdProviderSelectionListener newListener)
	{
		int index = 16;
		O7SdkGdprAdProviderSelectionListenerComponent o7SdkGdprAdProviderSelectionListenerComponent = CreateComponent<O7SdkGdprAdProviderSelectionListenerComponent>(index);
		o7SdkGdprAdProviderSelectionListenerComponent.listener = newListener;
		AddComponent(index, o7SdkGdprAdProviderSelectionListenerComponent);
	}

	public void ReplaceO7SdkGdprAdProviderSelectionListener(IO7SdkGdprAdProviderSelectionListener newListener)
	{
		int index = 16;
		O7SdkGdprAdProviderSelectionListenerComponent o7SdkGdprAdProviderSelectionListenerComponent = CreateComponent<O7SdkGdprAdProviderSelectionListenerComponent>(index);
		o7SdkGdprAdProviderSelectionListenerComponent.listener = newListener;
		ReplaceComponent(index, o7SdkGdprAdProviderSelectionListenerComponent);
	}

	public void RemoveO7SdkGdprAdProviderSelectionListener()
	{
		RemoveComponent(16);
	}

	public void AddO7SdkGdprOptOutListener(IO7SdkGdprOptOutListener newListener)
	{
		int index = 17;
		O7SdkGdprOptOutListenerComponent o7SdkGdprOptOutListenerComponent = CreateComponent<O7SdkGdprOptOutListenerComponent>(index);
		o7SdkGdprOptOutListenerComponent.listener = newListener;
		AddComponent(index, o7SdkGdprOptOutListenerComponent);
	}

	public void ReplaceO7SdkGdprOptOutListener(IO7SdkGdprOptOutListener newListener)
	{
		int index = 17;
		O7SdkGdprOptOutListenerComponent o7SdkGdprOptOutListenerComponent = CreateComponent<O7SdkGdprOptOutListenerComponent>(index);
		o7SdkGdprOptOutListenerComponent.listener = newListener;
		ReplaceComponent(index, o7SdkGdprOptOutListenerComponent);
	}

	public void RemoveO7SdkGdprOptOutListener()
	{
		RemoveComponent(17);
	}

	public void AddO7SdkManualNewsListener(IO7SdkManualNewsButtonUpdateListener newListener)
	{
		int index = 19;
		O7SdkManualNewsListenerComponent o7SdkManualNewsListenerComponent = CreateComponent<O7SdkManualNewsListenerComponent>(index);
		o7SdkManualNewsListenerComponent.listener = newListener;
		AddComponent(index, o7SdkManualNewsListenerComponent);
	}

	public void ReplaceO7SdkManualNewsListener(IO7SdkManualNewsButtonUpdateListener newListener)
	{
		int index = 19;
		O7SdkManualNewsListenerComponent o7SdkManualNewsListenerComponent = CreateComponent<O7SdkManualNewsListenerComponent>(index);
		o7SdkManualNewsListenerComponent.listener = newListener;
		ReplaceComponent(index, o7SdkManualNewsListenerComponent);
	}

	public void RemoveO7SdkManualNewsListener()
	{
		RemoveComponent(19);
	}

	public void AddOpenedScreens(List<BaseDialog> newDialogs)
	{
		int index = 31;
		OpenedScreensComponent openedScreensComponent = CreateComponent<OpenedScreensComponent>(index);
		openedScreensComponent.Dialogs = newDialogs;
		AddComponent(index, openedScreensComponent);
	}

	public void ReplaceOpenedScreens(List<BaseDialog> newDialogs)
	{
		int index = 31;
		OpenedScreensComponent openedScreensComponent = CreateComponent<OpenedScreensComponent>(index);
		openedScreensComponent.Dialogs = newDialogs;
		ReplaceComponent(index, openedScreensComponent);
	}

	public void RemoveOpenedScreens()
	{
		RemoveComponent(31);
	}

	public void AddRaccoonIntrosListener(IRaccoonIntroListener newListener)
	{
		int index = 142;
		RaccoonIntrosListenerComponent raccoonIntrosListenerComponent = CreateComponent<RaccoonIntrosListenerComponent>(index);
		raccoonIntrosListenerComponent.listener = newListener;
		AddComponent(index, raccoonIntrosListenerComponent);
	}

	public void ReplaceRaccoonIntrosListener(IRaccoonIntroListener newListener)
	{
		int index = 142;
		RaccoonIntrosListenerComponent raccoonIntrosListenerComponent = CreateComponent<RaccoonIntrosListenerComponent>(index);
		raccoonIntrosListenerComponent.listener = newListener;
		ReplaceComponent(index, raccoonIntrosListenerComponent);
	}

	public void RemoveRaccoonIntrosListener()
	{
		RemoveComponent(142);
	}

	public void AddSaveDataLoadedListener(ISaveDataLoadedListener newListener)
	{
		int index = 143;
		SaveDataLoadedListenerComponent saveDataLoadedListenerComponent = CreateComponent<SaveDataLoadedListenerComponent>(index);
		saveDataLoadedListenerComponent.listener = newListener;
		AddComponent(index, saveDataLoadedListenerComponent);
	}

	public void ReplaceSaveDataLoadedListener(ISaveDataLoadedListener newListener)
	{
		int index = 143;
		SaveDataLoadedListenerComponent saveDataLoadedListenerComponent = CreateComponent<SaveDataLoadedListenerComponent>(index);
		saveDataLoadedListenerComponent.listener = newListener;
		ReplaceComponent(index, saveDataLoadedListenerComponent);
	}

	public void RemoveSaveDataLoadedListener()
	{
		RemoveComponent(143);
	}

	public void AddSceneChangeRequestListener(ISceneChangeRequestListener newListener)
	{
		int index = 144;
		SceneChangeRequestListenerComponent sceneChangeRequestListenerComponent = CreateComponent<SceneChangeRequestListenerComponent>(index);
		sceneChangeRequestListenerComponent.listener = newListener;
		AddComponent(index, sceneChangeRequestListenerComponent);
	}

	public void ReplaceSceneChangeRequestListener(ISceneChangeRequestListener newListener)
	{
		int index = 144;
		SceneChangeRequestListenerComponent sceneChangeRequestListenerComponent = CreateComponent<SceneChangeRequestListenerComponent>(index);
		sceneChangeRequestListenerComponent.listener = newListener;
		ReplaceComponent(index, sceneChangeRequestListenerComponent);
	}

	public void RemoveSceneChangeRequestListener()
	{
		RemoveComponent(144);
	}

	public void AddScreenMap(GameObject newRoot, Dictionary<Type, GameObject> newScreenMap)
	{
		int index = 36;
		ScreenMapComponent screenMapComponent = CreateComponent<ScreenMapComponent>(index);
		screenMapComponent.Root = newRoot;
		screenMapComponent.ScreenMap = newScreenMap;
		AddComponent(index, screenMapComponent);
	}

	public void ReplaceScreenMap(GameObject newRoot, Dictionary<Type, GameObject> newScreenMap)
	{
		int index = 36;
		ScreenMapComponent screenMapComponent = CreateComponent<ScreenMapComponent>(index);
		screenMapComponent.Root = newRoot;
		screenMapComponent.ScreenMap = newScreenMap;
		ReplaceComponent(index, screenMapComponent);
	}

	public void RemoveScreenMap()
	{
		RemoveComponent(36);
	}

	public void AddSetup(BaseUI newDialog)
	{
		int index = 38;
		SetupComponent setupComponent = CreateComponent<SetupComponent>(index);
		setupComponent.dialog = newDialog;
		AddComponent(index, setupComponent);
	}

	public void ReplaceSetup(BaseUI newDialog)
	{
		int index = 38;
		SetupComponent setupComponent = CreateComponent<SetupComponent>(index);
		setupComponent.dialog = newDialog;
		ReplaceComponent(index, setupComponent);
	}

	public void RemoveSetup()
	{
		RemoveComponent(38);
	}

	public void AddStaminasListener(IStaminaListener newListener)
	{
		int index = 145;
		StaminasListenerComponent staminasListenerComponent = CreateComponent<StaminasListenerComponent>(index);
		staminasListenerComponent.listener = newListener;
		AddComponent(index, staminasListenerComponent);
	}

	public void ReplaceStaminasListener(IStaminaListener newListener)
	{
		int index = 145;
		StaminasListenerComponent staminasListenerComponent = CreateComponent<StaminasListenerComponent>(index);
		staminasListenerComponent.listener = newListener;
		ReplaceComponent(index, staminasListenerComponent);
	}

	public void RemoveStaminasListener()
	{
		RemoveComponent(145);
	}

	public void AddToggleMusicsListener(IToggleMusicListener newListener)
	{
		int index = 146;
		ToggleMusicsListenerComponent toggleMusicsListenerComponent = CreateComponent<ToggleMusicsListenerComponent>(index);
		toggleMusicsListenerComponent.listener = newListener;
		AddComponent(index, toggleMusicsListenerComponent);
	}

	public void ReplaceToggleMusicsListener(IToggleMusicListener newListener)
	{
		int index = 146;
		ToggleMusicsListenerComponent toggleMusicsListenerComponent = CreateComponent<ToggleMusicsListenerComponent>(index);
		toggleMusicsListenerComponent.listener = newListener;
		ReplaceComponent(index, toggleMusicsListenerComponent);
	}

	public void RemoveToggleMusicsListener()
	{
		RemoveComponent(146);
	}

	public void AddToggleSoundsListener(IToggleSoundListener newListener)
	{
		int index = 147;
		ToggleSoundsListenerComponent toggleSoundsListenerComponent = CreateComponent<ToggleSoundsListenerComponent>(index);
		toggleSoundsListenerComponent.listener = newListener;
		AddComponent(index, toggleSoundsListenerComponent);
	}

	public void ReplaceToggleSoundsListener(IToggleSoundListener newListener)
	{
		int index = 147;
		ToggleSoundsListenerComponent toggleSoundsListenerComponent = CreateComponent<ToggleSoundsListenerComponent>(index);
		toggleSoundsListenerComponent.listener = newListener;
		ReplaceComponent(index, toggleSoundsListenerComponent);
	}

	public void RemoveToggleSoundsListener()
	{
		RemoveComponent(147);
	}

	public void AddTutorialLayer(TutorialLayer newLayer)
	{
		int index = 40;
		TutorialLayerComponent tutorialLayerComponent = CreateComponent<TutorialLayerComponent>(index);
		tutorialLayerComponent.layer = newLayer;
		AddComponent(index, tutorialLayerComponent);
	}

	public void ReplaceTutorialLayer(TutorialLayer newLayer)
	{
		int index = 40;
		TutorialLayerComponent tutorialLayerComponent = CreateComponent<TutorialLayerComponent>(index);
		tutorialLayerComponent.layer = newLayer;
		ReplaceComponent(index, tutorialLayerComponent);
	}

	public void RemoveTutorialLayer()
	{
		RemoveComponent(40);
	}

	public void AddTutorialLevelPassedAvailability(ITutorialLevelPassedAvailibilityListener newListener)
	{
		int index = 148;
		TutorialLevelPassedAvailabilityComponent tutorialLevelPassedAvailabilityComponent = CreateComponent<TutorialLevelPassedAvailabilityComponent>(index);
		tutorialLevelPassedAvailabilityComponent.Listener = newListener;
		AddComponent(index, tutorialLevelPassedAvailabilityComponent);
	}

	public void ReplaceTutorialLevelPassedAvailability(ITutorialLevelPassedAvailibilityListener newListener)
	{
		int index = 148;
		TutorialLevelPassedAvailabilityComponent tutorialLevelPassedAvailabilityComponent = CreateComponent<TutorialLevelPassedAvailabilityComponent>(index);
		tutorialLevelPassedAvailabilityComponent.Listener = newListener;
		ReplaceComponent(index, tutorialLevelPassedAvailabilityComponent);
	}

	public void RemoveTutorialLevelPassedAvailability()
	{
		RemoveComponent(148);
	}

	public void AddTutorialsListener(ITutorialListener newListener)
	{
		int index = 149;
		TutorialsListenerComponent tutorialsListenerComponent = CreateComponent<TutorialsListenerComponent>(index);
		tutorialsListenerComponent.listener = newListener;
		AddComponent(index, tutorialsListenerComponent);
	}

	public void ReplaceTutorialsListener(ITutorialListener newListener)
	{
		int index = 149;
		TutorialsListenerComponent tutorialsListenerComponent = CreateComponent<TutorialsListenerComponent>(index);
		tutorialsListenerComponent.listener = newListener;
		ReplaceComponent(index, tutorialsListenerComponent);
	}

	public void RemoveTutorialsListener()
	{
		RemoveComponent(149);
	}

	public void AddUIActionFinishedCallback(Action newCallback)
	{
		int index = 42;
		UIActionFinishedCallbackComponent uIActionFinishedCallbackComponent = CreateComponent<UIActionFinishedCallbackComponent>(index);
		uIActionFinishedCallbackComponent.Callback = newCallback;
		AddComponent(index, uIActionFinishedCallbackComponent);
	}

	public void ReplaceUIActionFinishedCallback(Action newCallback)
	{
		int index = 42;
		UIActionFinishedCallbackComponent uIActionFinishedCallbackComponent = CreateComponent<UIActionFinishedCallbackComponent>(index);
		uIActionFinishedCallbackComponent.Callback = newCallback;
		ReplaceComponent(index, uIActionFinishedCallbackComponent);
	}

	public void RemoveUIActionFinishedCallback()
	{
		RemoveComponent(42);
	}

	public void AddUIChapterListResponse(List<WSChapterData> newChapterDataList)
	{
		int index = 44;
		UIChapterListResponseComponent uIChapterListResponseComponent = CreateComponent<UIChapterListResponseComponent>(index);
		uIChapterListResponseComponent.ChapterDataList = newChapterDataList;
		AddComponent(index, uIChapterListResponseComponent);
	}

	public void ReplaceUIChapterListResponse(List<WSChapterData> newChapterDataList)
	{
		int index = 44;
		UIChapterListResponseComponent uIChapterListResponseComponent = CreateComponent<UIChapterListResponseComponent>(index);
		uIChapterListResponseComponent.ChapterDataList = newChapterDataList;
		ReplaceComponent(index, uIChapterListResponseComponent);
	}

	public void RemoveUIChapterListResponse()
	{
		RemoveComponent(44);
	}

	public void AddUICharacterSelected(int newFirstCharIndex, int newSecondCharIndex)
	{
		int index = 45;
		UICharacterSelectedComponent uICharacterSelectedComponent = CreateComponent<UICharacterSelectedComponent>(index);
		uICharacterSelectedComponent.FirstCharIndex = newFirstCharIndex;
		uICharacterSelectedComponent.SecondCharIndex = newSecondCharIndex;
		AddComponent(index, uICharacterSelectedComponent);
	}

	public void ReplaceUICharacterSelected(int newFirstCharIndex, int newSecondCharIndex)
	{
		int index = 45;
		UICharacterSelectedComponent uICharacterSelectedComponent = CreateComponent<UICharacterSelectedComponent>(index);
		uICharacterSelectedComponent.FirstCharIndex = newFirstCharIndex;
		uICharacterSelectedComponent.SecondCharIndex = newSecondCharIndex;
		ReplaceComponent(index, uICharacterSelectedComponent);
	}

	public void RemoveUICharacterSelected()
	{
		RemoveComponent(45);
	}

	public void AddUICharacterUnlocked(int newCharId)
	{
		int index = 46;
		UICharacterUnlockedComponent uICharacterUnlockedComponent = CreateComponent<UICharacterUnlockedComponent>(index);
		uICharacterUnlockedComponent.CharId = newCharId;
		AddComponent(index, uICharacterUnlockedComponent);
	}

	public void ReplaceUICharacterUnlocked(int newCharId)
	{
		int index = 46;
		UICharacterUnlockedComponent uICharacterUnlockedComponent = CreateComponent<UICharacterUnlockedComponent>(index);
		uICharacterUnlockedComponent.CharId = newCharId;
		ReplaceComponent(index, uICharacterUnlockedComponent);
	}

	public void RemoveUICharacterUnlocked()
	{
		RemoveComponent(46);
	}

	public void AddUICharacterUpgraded(int newCharId)
	{
		int index = 47;
		UICharacterUpgradedComponent uICharacterUpgradedComponent = CreateComponent<UICharacterUpgradedComponent>(index);
		uICharacterUpgradedComponent.CharId = newCharId;
		AddComponent(index, uICharacterUpgradedComponent);
	}

	public void ReplaceUICharacterUpgraded(int newCharId)
	{
		int index = 47;
		UICharacterUpgradedComponent uICharacterUpgradedComponent = CreateComponent<UICharacterUpgradedComponent>(index);
		uICharacterUpgradedComponent.CharId = newCharId;
		ReplaceComponent(index, uICharacterUpgradedComponent);
	}

	public void RemoveUICharacterUpgraded()
	{
		RemoveComponent(47);
	}

	public void AddUICharListResponse(List<CSCharacterData> newCharDataList)
	{
		int index = 49;
		UICharListResponseComponent uICharListResponseComponent = CreateComponent<UICharListResponseComponent>(index);
		uICharListResponseComponent.CharDataList = newCharDataList;
		AddComponent(index, uICharListResponseComponent);
	}

	public void ReplaceUICharListResponse(List<CSCharacterData> newCharDataList)
	{
		int index = 49;
		UICharListResponseComponent uICharListResponseComponent = CreateComponent<UICharListResponseComponent>(index);
		uICharListResponseComponent.CharDataList = newCharDataList;
		ReplaceComponent(index, uICharListResponseComponent);
	}

	public void RemoveUICharListResponse()
	{
		RemoveComponent(49);
	}

	public void AddUICharSelectedResponse(int newFirstCharId, int newSecondCharId)
	{
		int index = 51;
		UICharSelectedResponseComponent uICharSelectedResponseComponent = CreateComponent<UICharSelectedResponseComponent>(index);
		uICharSelectedResponseComponent.FirstCharId = newFirstCharId;
		uICharSelectedResponseComponent.SecondCharId = newSecondCharId;
		AddComponent(index, uICharSelectedResponseComponent);
	}

	public void ReplaceUICharSelectedResponse(int newFirstCharId, int newSecondCharId)
	{
		int index = 51;
		UICharSelectedResponseComponent uICharSelectedResponseComponent = CreateComponent<UICharSelectedResponseComponent>(index);
		uICharSelectedResponseComponent.FirstCharId = newFirstCharId;
		uICharSelectedResponseComponent.SecondCharId = newSecondCharId;
		ReplaceComponent(index, uICharSelectedResponseComponent);
	}

	public void RemoveUICharSelectedResponse()
	{
		RemoveComponent(51);
	}

	public void AddUICharSingleRequest(int newCharId)
	{
		int index = 52;
		UICharSingleRequestComponent uICharSingleRequestComponent = CreateComponent<UICharSingleRequestComponent>(index);
		uICharSingleRequestComponent.CharId = newCharId;
		AddComponent(index, uICharSingleRequestComponent);
	}

	public void ReplaceUICharSingleRequest(int newCharId)
	{
		int index = 52;
		UICharSingleRequestComponent uICharSingleRequestComponent = CreateComponent<UICharSingleRequestComponent>(index);
		uICharSingleRequestComponent.CharId = newCharId;
		ReplaceComponent(index, uICharSingleRequestComponent);
	}

	public void RemoveUICharSingleRequest()
	{
		RemoveComponent(52);
	}

	public void AddUICharSingleResponse(CSCharacterData newCharData)
	{
		int index = 53;
		UICharSingleResponseComponent uICharSingleResponseComponent = CreateComponent<UICharSingleResponseComponent>(index);
		uICharSingleResponseComponent.CharData = newCharData;
		AddComponent(index, uICharSingleResponseComponent);
	}

	public void ReplaceUICharSingleResponse(CSCharacterData newCharData)
	{
		int index = 53;
		UICharSingleResponseComponent uICharSingleResponseComponent = CreateComponent<UICharSingleResponseComponent>(index);
		uICharSingleResponseComponent.CharData = newCharData;
		ReplaceComponent(index, uICharSingleResponseComponent);
	}

	public void RemoveUICharSingleResponse()
	{
		RemoveComponent(53);
	}

	public void AddUIChestBought(int newChestId)
	{
		int index = 55;
		UIChestBoughtComponent uIChestBoughtComponent = CreateComponent<UIChestBoughtComponent>(index);
		uIChestBoughtComponent.ChestId = newChestId;
		AddComponent(index, uIChestBoughtComponent);
	}

	public void ReplaceUIChestBought(int newChestId)
	{
		int index = 55;
		UIChestBoughtComponent uIChestBoughtComponent = CreateComponent<UIChestBoughtComponent>(index);
		uIChestBoughtComponent.ChestId = newChestId;
		ReplaceComponent(index, uIChestBoughtComponent);
	}

	public void RemoveUIChestBought()
	{
		RemoveComponent(55);
	}

	public void AddUIChestListResponse(List<ChestData> newChestDataList)
	{
		int index = 57;
		UIChestListResponseComponent uIChestListResponseComponent = CreateComponent<UIChestListResponseComponent>(index);
		uIChestListResponseComponent.ChestDataList = newChestDataList;
		AddComponent(index, uIChestListResponseComponent);
	}

	public void ReplaceUIChestListResponse(List<ChestData> newChestDataList)
	{
		int index = 57;
		UIChestListResponseComponent uIChestListResponseComponent = CreateComponent<UIChestListResponseComponent>(index);
		uIChestListResponseComponent.ChestDataList = newChestDataList;
		ReplaceComponent(index, uIChestListResponseComponent);
	}

	public void RemoveUIChestListResponse()
	{
		RemoveComponent(57);
	}

	public void AddUIChestRewardRequest(ChestData newChest, List<GeneratedReward> newRewards)
	{
		int index = 58;
		UIChestRewardRequestComponent uIChestRewardRequestComponent = CreateComponent<UIChestRewardRequestComponent>(index);
		uIChestRewardRequestComponent.Chest = newChest;
		uIChestRewardRequestComponent.Rewards = newRewards;
		AddComponent(index, uIChestRewardRequestComponent);
	}

	public void ReplaceUIChestRewardRequest(ChestData newChest, List<GeneratedReward> newRewards)
	{
		int index = 58;
		UIChestRewardRequestComponent uIChestRewardRequestComponent = CreateComponent<UIChestRewardRequestComponent>(index);
		uIChestRewardRequestComponent.Chest = newChest;
		uIChestRewardRequestComponent.Rewards = newRewards;
		ReplaceComponent(index, uIChestRewardRequestComponent);
	}

	public void RemoveUIChestRewardRequest()
	{
		RemoveComponent(58);
	}

	public void AddUIChestRewardResponse(ChestData newChest, List<GeneratedReward> newRewards)
	{
		int index = 59;
		UIChestRewardResponseComponent uIChestRewardResponseComponent = CreateComponent<UIChestRewardResponseComponent>(index);
		uIChestRewardResponseComponent.Chest = newChest;
		uIChestRewardResponseComponent.Rewards = newRewards;
		AddComponent(index, uIChestRewardResponseComponent);
	}

	public void ReplaceUIChestRewardResponse(ChestData newChest, List<GeneratedReward> newRewards)
	{
		int index = 59;
		UIChestRewardResponseComponent uIChestRewardResponseComponent = CreateComponent<UIChestRewardResponseComponent>(index);
		uIChestRewardResponseComponent.Chest = newChest;
		uIChestRewardResponseComponent.Rewards = newRewards;
		ReplaceComponent(index, uIChestRewardResponseComponent);
	}

	public void RemoveUIChestRewardResponse()
	{
		RemoveComponent(59);
	}

	public void AddUIChestWon(int newChestId)
	{
		int index = 60;
		UIChestWonComponent uIChestWonComponent = CreateComponent<UIChestWonComponent>(index);
		uIChestWonComponent.ChestId = newChestId;
		AddComponent(index, uIChestWonComponent);
	}

	public void ReplaceUIChestWon(int newChestId)
	{
		int index = 60;
		UIChestWonComponent uIChestWonComponent = CreateComponent<UIChestWonComponent>(index);
		uIChestWonComponent.ChestId = newChestId;
		ReplaceComponent(index, uIChestWonComponent);
	}

	public void RemoveUIChestWon()
	{
		RemoveComponent(60);
	}

	public void AddUIClaimDailyMissionReward(DailyMissionGroup newGroup, RectTransform newOrigin)
	{
		int index = 61;
		UIClaimDailyMissionRewardComponent uIClaimDailyMissionRewardComponent = CreateComponent<UIClaimDailyMissionRewardComponent>(index);
		uIClaimDailyMissionRewardComponent.group = newGroup;
		uIClaimDailyMissionRewardComponent.origin = newOrigin;
		AddComponent(index, uIClaimDailyMissionRewardComponent);
	}

	public void ReplaceUIClaimDailyMissionReward(DailyMissionGroup newGroup, RectTransform newOrigin)
	{
		int index = 61;
		UIClaimDailyMissionRewardComponent uIClaimDailyMissionRewardComponent = CreateComponent<UIClaimDailyMissionRewardComponent>(index);
		uIClaimDailyMissionRewardComponent.group = newGroup;
		uIClaimDailyMissionRewardComponent.origin = newOrigin;
		ReplaceComponent(index, uIClaimDailyMissionRewardComponent);
	}

	public void RemoveUIClaimDailyMissionReward()
	{
		RemoveComponent(61);
	}

	public void AddUIClaimReward(RewardData newData, RectTransform newOrigin, bool newShouldRefreshScreen)
	{
		int index = 62;
		UIClaimRewardComponent uIClaimRewardComponent = CreateComponent<UIClaimRewardComponent>(index);
		uIClaimRewardComponent.data = newData;
		uIClaimRewardComponent.origin = newOrigin;
		uIClaimRewardComponent.shouldRefreshScreen = newShouldRefreshScreen;
		AddComponent(index, uIClaimRewardComponent);
	}

	public void ReplaceUIClaimReward(RewardData newData, RectTransform newOrigin, bool newShouldRefreshScreen)
	{
		int index = 62;
		UIClaimRewardComponent uIClaimRewardComponent = CreateComponent<UIClaimRewardComponent>(index);
		uIClaimRewardComponent.data = newData;
		uIClaimRewardComponent.origin = newOrigin;
		uIClaimRewardComponent.shouldRefreshScreen = newShouldRefreshScreen;
		ReplaceComponent(index, uIClaimRewardComponent);
	}

	public void RemoveUIClaimReward()
	{
		RemoveComponent(62);
	}

	public void AddUICompanionListResponse(List<CSCompanionData> newCompDataList)
	{
		int index = 64;
		UICompanionListResponseComponent uICompanionListResponseComponent = CreateComponent<UICompanionListResponseComponent>(index);
		uICompanionListResponseComponent.CompDataList = newCompDataList;
		AddComponent(index, uICompanionListResponseComponent);
	}

	public void ReplaceUICompanionListResponse(List<CSCompanionData> newCompDataList)
	{
		int index = 64;
		UICompanionListResponseComponent uICompanionListResponseComponent = CreateComponent<UICompanionListResponseComponent>(index);
		uICompanionListResponseComponent.CompDataList = newCompDataList;
		ReplaceComponent(index, uICompanionListResponseComponent);
	}

	public void RemoveUICompanionListResponse()
	{
		RemoveComponent(64);
	}

	public void AddUICompanionSelected(int newCompId)
	{
		int index = 65;
		UICompanionSelectedComponent uICompanionSelectedComponent = CreateComponent<UICompanionSelectedComponent>(index);
		uICompanionSelectedComponent.CompId = newCompId;
		AddComponent(index, uICompanionSelectedComponent);
	}

	public void ReplaceUICompanionSelected(int newCompId)
	{
		int index = 65;
		UICompanionSelectedComponent uICompanionSelectedComponent = CreateComponent<UICompanionSelectedComponent>(index);
		uICompanionSelectedComponent.CompId = newCompId;
		ReplaceComponent(index, uICompanionSelectedComponent);
	}

	public void RemoveUICompanionSelected()
	{
		RemoveComponent(65);
	}

	public void AddUICompanionUnlocked(int newCompId)
	{
		int index = 66;
		UICompanionUnlockedComponent uICompanionUnlockedComponent = CreateComponent<UICompanionUnlockedComponent>(index);
		uICompanionUnlockedComponent.CompId = newCompId;
		AddComponent(index, uICompanionUnlockedComponent);
	}

	public void ReplaceUICompanionUnlocked(int newCompId)
	{
		int index = 66;
		UICompanionUnlockedComponent uICompanionUnlockedComponent = CreateComponent<UICompanionUnlockedComponent>(index);
		uICompanionUnlockedComponent.CompId = newCompId;
		ReplaceComponent(index, uICompanionUnlockedComponent);
	}

	public void RemoveUICompanionUnlocked()
	{
		RemoveComponent(66);
	}

	public void AddUICompanionUpgraded(int newCompId)
	{
		int index = 67;
		UICompanionUpgradedComponent uICompanionUpgradedComponent = CreateComponent<UICompanionUpgradedComponent>(index);
		uICompanionUpgradedComponent.CompId = newCompId;
		AddComponent(index, uICompanionUpgradedComponent);
	}

	public void ReplaceUICompanionUpgraded(int newCompId)
	{
		int index = 67;
		UICompanionUpgradedComponent uICompanionUpgradedComponent = CreateComponent<UICompanionUpgradedComponent>(index);
		uICompanionUpgradedComponent.CompId = newCompId;
		ReplaceComponent(index, uICompanionUpgradedComponent);
	}

	public void RemoveUICompanionUpgraded()
	{
		RemoveComponent(67);
	}

	public void AddUICompSelectedResponse(int newSelectedCompId, bool newIsSelected)
	{
		int index = 70;
		UICompSelectedResponseComponent uICompSelectedResponseComponent = CreateComponent<UICompSelectedResponseComponent>(index);
		uICompSelectedResponseComponent.SelectedCompId = newSelectedCompId;
		uICompSelectedResponseComponent.IsSelected = newIsSelected;
		AddComponent(index, uICompSelectedResponseComponent);
	}

	public void ReplaceUICompSelectedResponse(int newSelectedCompId, bool newIsSelected)
	{
		int index = 70;
		UICompSelectedResponseComponent uICompSelectedResponseComponent = CreateComponent<UICompSelectedResponseComponent>(index);
		uICompSelectedResponseComponent.SelectedCompId = newSelectedCompId;
		uICompSelectedResponseComponent.IsSelected = newIsSelected;
		ReplaceComponent(index, uICompSelectedResponseComponent);
	}

	public void RemoveUICompSelectedResponse()
	{
		RemoveComponent(70);
	}

	public void AddUICompSingleRequest(int newCompId)
	{
		int index = 71;
		UICompSingleRequestComponent uICompSingleRequestComponent = CreateComponent<UICompSingleRequestComponent>(index);
		uICompSingleRequestComponent.CompId = newCompId;
		AddComponent(index, uICompSingleRequestComponent);
	}

	public void ReplaceUICompSingleRequest(int newCompId)
	{
		int index = 71;
		UICompSingleRequestComponent uICompSingleRequestComponent = CreateComponent<UICompSingleRequestComponent>(index);
		uICompSingleRequestComponent.CompId = newCompId;
		ReplaceComponent(index, uICompSingleRequestComponent);
	}

	public void RemoveUICompSingleRequest()
	{
		RemoveComponent(71);
	}

	public void AddUICompSingleResponse(CSCompanionData newCompData)
	{
		int index = 72;
		UICompSingleResponseComponent uICompSingleResponseComponent = CreateComponent<UICompSingleResponseComponent>(index);
		uICompSingleResponseComponent.CompData = newCompData;
		AddComponent(index, uICompSingleResponseComponent);
	}

	public void ReplaceUICompSingleResponse(CSCompanionData newCompData)
	{
		int index = 72;
		UICompSingleResponseComponent uICompSingleResponseComponent = CreateComponent<UICompSingleResponseComponent>(index);
		uICompSingleResponseComponent.CompData = newCompData;
		ReplaceComponent(index, uICompSingleResponseComponent);
	}

	public void RemoveUICompSingleResponse()
	{
		RemoveComponent(72);
	}

	public void AddUICurrentDailyMissionsResponse(DailyMissionsCollection newDailyMissionsCollection)
	{
		int index = 74;
		UICurrentDailyMissionsResponse uICurrentDailyMissionsResponse = CreateComponent<UICurrentDailyMissionsResponse>(index);
		uICurrentDailyMissionsResponse.DailyMissionsCollection = newDailyMissionsCollection;
		AddComponent(index, uICurrentDailyMissionsResponse);
	}

	public void ReplaceUICurrentDailyMissionsResponse(DailyMissionsCollection newDailyMissionsCollection)
	{
		int index = 74;
		UICurrentDailyMissionsResponse uICurrentDailyMissionsResponse = CreateComponent<UICurrentDailyMissionsResponse>(index);
		uICurrentDailyMissionsResponse.DailyMissionsCollection = newDailyMissionsCollection;
		ReplaceComponent(index, uICurrentDailyMissionsResponse);
	}

	public void RemoveUICurrentDailyMissionsResponse()
	{
		RemoveComponent(74);
	}

	public void AddUIDisplayAbility(int newId, bool newIsChar)
	{
		int index = 75;
		UIDisplayAbilityComponent uIDisplayAbilityComponent = CreateComponent<UIDisplayAbilityComponent>(index);
		uIDisplayAbilityComponent.Id = newId;
		uIDisplayAbilityComponent.IsChar = newIsChar;
		AddComponent(index, uIDisplayAbilityComponent);
	}

	public void ReplaceUIDisplayAbility(int newId, bool newIsChar)
	{
		int index = 75;
		UIDisplayAbilityComponent uIDisplayAbilityComponent = CreateComponent<UIDisplayAbilityComponent>(index);
		uIDisplayAbilityComponent.Id = newId;
		uIDisplayAbilityComponent.IsChar = newIsChar;
		ReplaceComponent(index, uIDisplayAbilityComponent);
	}

	public void RemoveUIDisplayAbility()
	{
		RemoveComponent(75);
	}

	public void AddUIDisplayExplanation(ExplanationType newExplanationType)
	{
		int index = 76;
		UIDisplayExplanation uIDisplayExplanation = CreateComponent<UIDisplayExplanation>(index);
		uIDisplayExplanation.ExplanationType = newExplanationType;
		AddComponent(index, uIDisplayExplanation);
	}

	public void ReplaceUIDisplayExplanation(ExplanationType newExplanationType)
	{
		int index = 76;
		UIDisplayExplanation uIDisplayExplanation = CreateComponent<UIDisplayExplanation>(index);
		uIDisplayExplanation.ExplanationType = newExplanationType;
		ReplaceComponent(index, uIDisplayExplanation);
	}

	public void RemoveUIDisplayExplanation()
	{
		RemoveComponent(76);
	}

	public void AddUIDisplayMissionDetails(int newLevelId)
	{
		int index = 77;
		UIDisplayMissionDetails uIDisplayMissionDetails = CreateComponent<UIDisplayMissionDetails>(index);
		uIDisplayMissionDetails.LevelId = newLevelId;
		AddComponent(index, uIDisplayMissionDetails);
	}

	public void ReplaceUIDisplayMissionDetails(int newLevelId)
	{
		int index = 77;
		UIDisplayMissionDetails uIDisplayMissionDetails = CreateComponent<UIDisplayMissionDetails>(index);
		uIDisplayMissionDetails.LevelId = newLevelId;
		ReplaceComponent(index, uIDisplayMissionDetails);
	}

	public void RemoveUIDisplayMissionDetails()
	{
		RemoveComponent(77);
	}

	public void AddUIDisplayTransactionConfrimation(StateEntity newTransaction)
	{
		int index = 78;
		UIDisplayTransactionConfrimation uIDisplayTransactionConfrimation = CreateComponent<UIDisplayTransactionConfrimation>(index);
		uIDisplayTransactionConfrimation.transaction = newTransaction;
		AddComponent(index, uIDisplayTransactionConfrimation);
	}

	public void ReplaceUIDisplayTransactionConfrimation(StateEntity newTransaction)
	{
		int index = 78;
		UIDisplayTransactionConfrimation uIDisplayTransactionConfrimation = CreateComponent<UIDisplayTransactionConfrimation>(index);
		uIDisplayTransactionConfrimation.transaction = newTransaction;
		ReplaceComponent(index, uIDisplayTransactionConfrimation);
	}

	public void RemoveUIDisplayTransactionConfrimation()
	{
		RemoveComponent(78);
	}

	public void AddUIFreeChestResponse(FreeChestData newFreeChest)
	{
		int index = 82;
		UIFreeChestResponseComponent uIFreeChestResponseComponent = CreateComponent<UIFreeChestResponseComponent>(index);
		uIFreeChestResponseComponent.FreeChest = newFreeChest;
		AddComponent(index, uIFreeChestResponseComponent);
	}

	public void ReplaceUIFreeChestResponse(FreeChestData newFreeChest)
	{
		int index = 82;
		UIFreeChestResponseComponent uIFreeChestResponseComponent = CreateComponent<UIFreeChestResponseComponent>(index);
		uIFreeChestResponseComponent.FreeChest = newFreeChest;
		ReplaceComponent(index, uIFreeChestResponseComponent);
	}

	public void RemoveUIFreeChestResponse()
	{
		RemoveComponent(82);
	}

	public void AddUIIAPBought(int newIAPId, RectTransform newOrigin, string newProductId)
	{
		int index = 88;
		UIIAPBought uIIAPBought = CreateComponent<UIIAPBought>(index);
		uIIAPBought.IAPId = newIAPId;
		uIIAPBought.origin = newOrigin;
		uIIAPBought.productId = newProductId;
		AddComponent(index, uIIAPBought);
	}

	public void ReplaceUIIAPBought(int newIAPId, RectTransform newOrigin, string newProductId)
	{
		int index = 88;
		UIIAPBought uIIAPBought = CreateComponent<UIIAPBought>(index);
		uIIAPBought.IAPId = newIAPId;
		uIIAPBought.origin = newOrigin;
		uIIAPBought.productId = newProductId;
		ReplaceComponent(index, uIIAPBought);
	}

	public void RemoveUIIAPBought()
	{
		RemoveComponent(88);
	}

	public void AddUILastSelectedMissionResponse(int newIndex)
	{
		int index = 90;
		UILastSelectedMissionResponseComponent uILastSelectedMissionResponseComponent = CreateComponent<UILastSelectedMissionResponseComponent>(index);
		uILastSelectedMissionResponseComponent.Index = newIndex;
		AddComponent(index, uILastSelectedMissionResponseComponent);
	}

	public void ReplaceUILastSelectedMissionResponse(int newIndex)
	{
		int index = 90;
		UILastSelectedMissionResponseComponent uILastSelectedMissionResponseComponent = CreateComponent<UILastSelectedMissionResponseComponent>(index);
		uILastSelectedMissionResponseComponent.Index = newIndex;
		ReplaceComponent(index, uILastSelectedMissionResponseComponent);
	}

	public void RemoveUILastSelectedMissionResponse()
	{
		RemoveComponent(90);
	}

	public void AddUIMissionListResponse(List<WSLevelData> newLevelDataList)
	{
		int index = 92;
		UIMissionListResponseComponent uIMissionListResponseComponent = CreateComponent<UIMissionListResponseComponent>(index);
		uIMissionListResponseComponent.LevelDataList = newLevelDataList;
		AddComponent(index, uIMissionListResponseComponent);
	}

	public void ReplaceUIMissionListResponse(List<WSLevelData> newLevelDataList)
	{
		int index = 92;
		UIMissionListResponseComponent uIMissionListResponseComponent = CreateComponent<UIMissionListResponseComponent>(index);
		uIMissionListResponseComponent.LevelDataList = newLevelDataList;
		ReplaceComponent(index, uIMissionListResponseComponent);
	}

	public void RemoveUIMissionListResponse()
	{
		RemoveComponent(92);
	}

	public void AddUINewCharactersUnlockedResponse(NewUnlocksData newUnlocks)
	{
		int index = 94;
		UINewCharactersUnlockedResponseComponent uINewCharactersUnlockedResponseComponent = CreateComponent<UINewCharactersUnlockedResponseComponent>(index);
		uINewCharactersUnlockedResponseComponent.Unlocks = newUnlocks;
		AddComponent(index, uINewCharactersUnlockedResponseComponent);
	}

	public void ReplaceUINewCharactersUnlockedResponse(NewUnlocksData newUnlocks)
	{
		int index = 94;
		UINewCharactersUnlockedResponseComponent uINewCharactersUnlockedResponseComponent = CreateComponent<UINewCharactersUnlockedResponseComponent>(index);
		uINewCharactersUnlockedResponseComponent.Unlocks = newUnlocks;
		ReplaceComponent(index, uINewCharactersUnlockedResponseComponent);
	}

	public void RemoveUINewCharactersUnlockedResponse()
	{
		RemoveComponent(94);
	}

	public void AddUINewCompanionsUnlockedResponse(NewUnlocksData newUnlocks)
	{
		int index = 95;
		UINewCompanionsUnlockedResponseComponent uINewCompanionsUnlockedResponseComponent = CreateComponent<UINewCompanionsUnlockedResponseComponent>(index);
		uINewCompanionsUnlockedResponseComponent.Unlocks = newUnlocks;
		AddComponent(index, uINewCompanionsUnlockedResponseComponent);
	}

	public void ReplaceUINewCompanionsUnlockedResponse(NewUnlocksData newUnlocks)
	{
		int index = 95;
		UINewCompanionsUnlockedResponseComponent uINewCompanionsUnlockedResponseComponent = CreateComponent<UINewCompanionsUnlockedResponseComponent>(index);
		uINewCompanionsUnlockedResponseComponent.Unlocks = newUnlocks;
		ReplaceComponent(index, uINewCompanionsUnlockedResponseComponent);
	}

	public void RemoveUINewCompanionsUnlockedResponse()
	{
		RemoveComponent(95);
	}

	public void AddUINewMissionUnlockedResponse(bool newValue)
	{
		int index = 98;
		UINewMissionUnlockedResponseComponent uINewMissionUnlockedResponseComponent = CreateComponent<UINewMissionUnlockedResponseComponent>(index);
		uINewMissionUnlockedResponseComponent.Value = newValue;
		AddComponent(index, uINewMissionUnlockedResponseComponent);
	}

	public void ReplaceUINewMissionUnlockedResponse(bool newValue)
	{
		int index = 98;
		UINewMissionUnlockedResponseComponent uINewMissionUnlockedResponseComponent = CreateComponent<UINewMissionUnlockedResponseComponent>(index);
		uINewMissionUnlockedResponseComponent.Value = newValue;
		ReplaceComponent(index, uINewMissionUnlockedResponseComponent);
	}

	public void RemoveUINewMissionUnlockedResponse()
	{
		RemoveComponent(98);
	}

	public void AddUINewUnlockedCharacterDisplayed(int newCharacterId)
	{
		int index = 99;
		UINewUnlockedCharacterDisplayed uINewUnlockedCharacterDisplayed = CreateComponent<UINewUnlockedCharacterDisplayed>(index);
		uINewUnlockedCharacterDisplayed.CharacterId = newCharacterId;
		AddComponent(index, uINewUnlockedCharacterDisplayed);
	}

	public void ReplaceUINewUnlockedCharacterDisplayed(int newCharacterId)
	{
		int index = 99;
		UINewUnlockedCharacterDisplayed uINewUnlockedCharacterDisplayed = CreateComponent<UINewUnlockedCharacterDisplayed>(index);
		uINewUnlockedCharacterDisplayed.CharacterId = newCharacterId;
		ReplaceComponent(index, uINewUnlockedCharacterDisplayed);
	}

	public void RemoveUINewUnlockedCharacterDisplayed()
	{
		RemoveComponent(99);
	}

	public void AddUINewUnlockedCompanionDisplayed(int newCompanionId)
	{
		int index = 100;
		UINewUnlockedCompanionDisplayed uINewUnlockedCompanionDisplayed = CreateComponent<UINewUnlockedCompanionDisplayed>(index);
		uINewUnlockedCompanionDisplayed.CompanionId = newCompanionId;
		AddComponent(index, uINewUnlockedCompanionDisplayed);
	}

	public void ReplaceUINewUnlockedCompanionDisplayed(int newCompanionId)
	{
		int index = 100;
		UINewUnlockedCompanionDisplayed uINewUnlockedCompanionDisplayed = CreateComponent<UINewUnlockedCompanionDisplayed>(index);
		uINewUnlockedCompanionDisplayed.CompanionId = newCompanionId;
		ReplaceComponent(index, uINewUnlockedCompanionDisplayed);
	}

	public void RemoveUINewUnlockedCompanionDisplayed()
	{
		RemoveComponent(100);
	}

	public void AddUIOpenShop(ShopGroupType newEntryPoint)
	{
		int index = 114;
		UIOpenShopComponent uIOpenShopComponent = CreateComponent<UIOpenShopComponent>(index);
		uIOpenShopComponent.EntryPoint = newEntryPoint;
		AddComponent(index, uIOpenShopComponent);
	}

	public void ReplaceUIOpenShop(ShopGroupType newEntryPoint)
	{
		int index = 114;
		UIOpenShopComponent uIOpenShopComponent = CreateComponent<UIOpenShopComponent>(index);
		uIOpenShopComponent.EntryPoint = newEntryPoint;
		ReplaceComponent(index, uIOpenShopComponent);
	}

	public void RemoveUIOpenShop()
	{
		RemoveComponent(114);
	}

	public void AddUIPlayerLevel(int newCurrentLevel)
	{
		int index = 115;
		UIPlayerLevelComponent uIPlayerLevelComponent = CreateComponent<UIPlayerLevelComponent>(index);
		uIPlayerLevelComponent.CurrentLevel = newCurrentLevel;
		AddComponent(index, uIPlayerLevelComponent);
	}

	public void ReplaceUIPlayerLevel(int newCurrentLevel)
	{
		int index = 115;
		UIPlayerLevelComponent uIPlayerLevelComponent = CreateComponent<UIPlayerLevelComponent>(index);
		uIPlayerLevelComponent.CurrentLevel = newCurrentLevel;
		ReplaceComponent(index, uIPlayerLevelComponent);
	}

	public void RemoveUIPlayerLevel()
	{
		RemoveComponent(115);
	}

	public void AddUIPlayerProgressionStateResponse(int newPlayerLevel, int newPlayerExperience, int newCurrentRequiredExp, int newNextRequiredExp)
	{
		int index = 117;
		UIPlayerProgressionStateResponseComponent uIPlayerProgressionStateResponseComponent = CreateComponent<UIPlayerProgressionStateResponseComponent>(index);
		uIPlayerProgressionStateResponseComponent.PlayerLevel = newPlayerLevel;
		uIPlayerProgressionStateResponseComponent.PlayerExperience = newPlayerExperience;
		uIPlayerProgressionStateResponseComponent.CurrentRequiredExp = newCurrentRequiredExp;
		uIPlayerProgressionStateResponseComponent.NextRequiredExp = newNextRequiredExp;
		AddComponent(index, uIPlayerProgressionStateResponseComponent);
	}

	public void ReplaceUIPlayerProgressionStateResponse(int newPlayerLevel, int newPlayerExperience, int newCurrentRequiredExp, int newNextRequiredExp)
	{
		int index = 117;
		UIPlayerProgressionStateResponseComponent uIPlayerProgressionStateResponseComponent = CreateComponent<UIPlayerProgressionStateResponseComponent>(index);
		uIPlayerProgressionStateResponseComponent.PlayerLevel = newPlayerLevel;
		uIPlayerProgressionStateResponseComponent.PlayerExperience = newPlayerExperience;
		uIPlayerProgressionStateResponseComponent.CurrentRequiredExp = newCurrentRequiredExp;
		uIPlayerProgressionStateResponseComponent.NextRequiredExp = newNextRequiredExp;
		ReplaceComponent(index, uIPlayerProgressionStateResponseComponent);
	}

	public void RemoveUIPlayerProgressionStateResponse()
	{
		RemoveComponent(117);
	}

	public void AddUIPlayMission(int newIndex)
	{
		int index = 118;
		UIPlayMissionComponent uIPlayMissionComponent = CreateComponent<UIPlayMissionComponent>(index);
		uIPlayMissionComponent.Index = newIndex;
		AddComponent(index, uIPlayMissionComponent);
	}

	public void ReplaceUIPlayMission(int newIndex)
	{
		int index = 118;
		UIPlayMissionComponent uIPlayMissionComponent = CreateComponent<UIPlayMissionComponent>(index);
		uIPlayMissionComponent.Index = newIndex;
		ReplaceComponent(index, uIPlayMissionComponent);
	}

	public void RemoveUIPlayMission()
	{
		RemoveComponent(118);
	}

	public void AddUIRefillChar(int newCharIndex)
	{
		int index = 119;
		UIRefillCharComponent uIRefillCharComponent = CreateComponent<UIRefillCharComponent>(index);
		uIRefillCharComponent.CharIndex = newCharIndex;
		AddComponent(index, uIRefillCharComponent);
	}

	public void ReplaceUIRefillChar(int newCharIndex)
	{
		int index = 119;
		UIRefillCharComponent uIRefillCharComponent = CreateComponent<UIRefillCharComponent>(index);
		uIRefillCharComponent.CharIndex = newCharIndex;
		ReplaceComponent(index, uIRefillCharComponent);
	}

	public void RemoveUIRefillChar()
	{
		RemoveComponent(119);
	}

	public void AddUIRewardCollected(GeneratedReward newReward)
	{
		int index = 120;
		UIRewardCollectedComponent uIRewardCollectedComponent = CreateComponent<UIRewardCollectedComponent>(index);
		uIRewardCollectedComponent.Reward = newReward;
		AddComponent(index, uIRewardCollectedComponent);
	}

	public void ReplaceUIRewardCollected(GeneratedReward newReward)
	{
		int index = 120;
		UIRewardCollectedComponent uIRewardCollectedComponent = CreateComponent<UIRewardCollectedComponent>(index);
		uIRewardCollectedComponent.Reward = newReward;
		ReplaceComponent(index, uIRewardCollectedComponent);
	}

	public void RemoveUIRewardCollected()
	{
		RemoveComponent(120);
	}

	public void AddUISettingsFooterPress(SettingsFooter.SettingsFooterHyperlink newLinkType)
	{
		int index = 121;
		UISettingsFooterPressComponent uISettingsFooterPressComponent = CreateComponent<UISettingsFooterPressComponent>(index);
		uISettingsFooterPressComponent.LinkType = newLinkType;
		AddComponent(index, uISettingsFooterPressComponent);
	}

	public void ReplaceUISettingsFooterPress(SettingsFooter.SettingsFooterHyperlink newLinkType)
	{
		int index = 121;
		UISettingsFooterPressComponent uISettingsFooterPressComponent = CreateComponent<UISettingsFooterPressComponent>(index);
		uISettingsFooterPressComponent.LinkType = newLinkType;
		ReplaceComponent(index, uISettingsFooterPressComponent);
	}

	public void RemoveUISettingsFooterPress()
	{
		RemoveComponent(121);
	}

	public void AddUIShopIAPsListResponse(List<IAPsData> newIAPsDataList)
	{
		int index = 123;
		UIShopIAPsListResponseComponent uIShopIAPsListResponseComponent = CreateComponent<UIShopIAPsListResponseComponent>(index);
		uIShopIAPsListResponseComponent.IAPsDataList = newIAPsDataList;
		AddComponent(index, uIShopIAPsListResponseComponent);
	}

	public void ReplaceUIShopIAPsListResponse(List<IAPsData> newIAPsDataList)
	{
		int index = 123;
		UIShopIAPsListResponseComponent uIShopIAPsListResponseComponent = CreateComponent<UIShopIAPsListResponseComponent>(index);
		uIShopIAPsListResponseComponent.IAPsDataList = newIAPsDataList;
		ReplaceComponent(index, uIShopIAPsListResponseComponent);
	}

	public void RemoveUIShopIAPsListResponse()
	{
		RemoveComponent(123);
	}

	public void AddUIShopSoftCurrencyPacksResponse(List<SoftCurrencyPacksData> newSoftCurrencyPacksDataList)
	{
		int index = 126;
		UIShopSoftCurrencyPacksResponseComponent uIShopSoftCurrencyPacksResponseComponent = CreateComponent<UIShopSoftCurrencyPacksResponseComponent>(index);
		uIShopSoftCurrencyPacksResponseComponent.SoftCurrencyPacksDataList = newSoftCurrencyPacksDataList;
		AddComponent(index, uIShopSoftCurrencyPacksResponseComponent);
	}

	public void ReplaceUIShopSoftCurrencyPacksResponse(List<SoftCurrencyPacksData> newSoftCurrencyPacksDataList)
	{
		int index = 126;
		UIShopSoftCurrencyPacksResponseComponent uIShopSoftCurrencyPacksResponseComponent = CreateComponent<UIShopSoftCurrencyPacksResponseComponent>(index);
		uIShopSoftCurrencyPacksResponseComponent.SoftCurrencyPacksDataList = newSoftCurrencyPacksDataList;
		ReplaceComponent(index, uIShopSoftCurrencyPacksResponseComponent);
	}

	public void RemoveUIShopSoftCurrencyPacksResponse()
	{
		RemoveComponent(126);
	}

	public void AddUIShowCharacterInfo(int newCharacterId)
	{
		int index = 127;
		UIShowCharacterInfoComponent uIShowCharacterInfoComponent = CreateComponent<UIShowCharacterInfoComponent>(index);
		uIShowCharacterInfoComponent.CharacterId = newCharacterId;
		AddComponent(index, uIShowCharacterInfoComponent);
	}

	public void ReplaceUIShowCharacterInfo(int newCharacterId)
	{
		int index = 127;
		UIShowCharacterInfoComponent uIShowCharacterInfoComponent = CreateComponent<UIShowCharacterInfoComponent>(index);
		uIShowCharacterInfoComponent.CharacterId = newCharacterId;
		ReplaceComponent(index, uIShowCharacterInfoComponent);
	}

	public void RemoveUIShowCharacterInfo()
	{
		RemoveComponent(127);
	}

	public void AddUIShowCompanionInfo(int newCompanionId)
	{
		int index = 128;
		UIShowCompanionInfoComponent uIShowCompanionInfoComponent = CreateComponent<UIShowCompanionInfoComponent>(index);
		uIShowCompanionInfoComponent.CompanionId = newCompanionId;
		AddComponent(index, uIShowCompanionInfoComponent);
	}

	public void ReplaceUIShowCompanionInfo(int newCompanionId)
	{
		int index = 128;
		UIShowCompanionInfoComponent uIShowCompanionInfoComponent = CreateComponent<UIShowCompanionInfoComponent>(index);
		uIShowCompanionInfoComponent.CompanionId = newCompanionId;
		ReplaceComponent(index, uIShowCompanionInfoComponent);
	}

	public void RemoveUIShowCompanionInfo()
	{
		RemoveComponent(128);
	}

	public void AddUIShowCurrencyUpdate(int newItemIndex, int newItemAmount, RectTransform newOrigin, bool newUpdateTargetManually)
	{
		int index = 129;
		UIShowCurrencyUpdateComponent uIShowCurrencyUpdateComponent = CreateComponent<UIShowCurrencyUpdateComponent>(index);
		uIShowCurrencyUpdateComponent.ItemIndex = newItemIndex;
		uIShowCurrencyUpdateComponent.ItemAmount = newItemAmount;
		uIShowCurrencyUpdateComponent.origin = newOrigin;
		uIShowCurrencyUpdateComponent.UpdateTargetManually = newUpdateTargetManually;
		AddComponent(index, uIShowCurrencyUpdateComponent);
	}

	public void ReplaceUIShowCurrencyUpdate(int newItemIndex, int newItemAmount, RectTransform newOrigin, bool newUpdateTargetManually)
	{
		int index = 129;
		UIShowCurrencyUpdateComponent uIShowCurrencyUpdateComponent = CreateComponent<UIShowCurrencyUpdateComponent>(index);
		uIShowCurrencyUpdateComponent.ItemIndex = newItemIndex;
		uIShowCurrencyUpdateComponent.ItemAmount = newItemAmount;
		uIShowCurrencyUpdateComponent.origin = newOrigin;
		uIShowCurrencyUpdateComponent.UpdateTargetManually = newUpdateTargetManually;
		ReplaceComponent(index, uIShowCurrencyUpdateComponent);
	}

	public void RemoveUIShowCurrencyUpdate()
	{
		RemoveComponent(129);
	}

	public void AddUISoftCurrencyPackBought(int newPackId, RectTransform newOrigin)
	{
		int index = 130;
		UISoftCurrencyPackBought uISoftCurrencyPackBought = CreateComponent<UISoftCurrencyPackBought>(index);
		uISoftCurrencyPackBought.PackId = newPackId;
		uISoftCurrencyPackBought.origin = newOrigin;
		AddComponent(index, uISoftCurrencyPackBought);
	}

	public void ReplaceUISoftCurrencyPackBought(int newPackId, RectTransform newOrigin)
	{
		int index = 130;
		UISoftCurrencyPackBought uISoftCurrencyPackBought = CreateComponent<UISoftCurrencyPackBought>(index);
		uISoftCurrencyPackBought.PackId = newPackId;
		uISoftCurrencyPackBought.origin = newOrigin;
		ReplaceComponent(index, uISoftCurrencyPackBought);
	}

	public void RemoveUISoftCurrencyPackBought()
	{
		RemoveComponent(130);
	}

	public void AddUITotalCoinsResponse(int newAmount)
	{
		int index = 132;
		UITotalCoinsResponseComponent uITotalCoinsResponseComponent = CreateComponent<UITotalCoinsResponseComponent>(index);
		uITotalCoinsResponseComponent.Amount = newAmount;
		AddComponent(index, uITotalCoinsResponseComponent);
	}

	public void ReplaceUITotalCoinsResponse(int newAmount)
	{
		int index = 132;
		UITotalCoinsResponseComponent uITotalCoinsResponseComponent = CreateComponent<UITotalCoinsResponseComponent>(index);
		uITotalCoinsResponseComponent.Amount = newAmount;
		ReplaceComponent(index, uITotalCoinsResponseComponent);
	}

	public void RemoveUITotalCoinsResponse()
	{
		RemoveComponent(132);
	}

	public void AddUITotalGemsResponse(int newAmount)
	{
		int index = 134;
		UITotalGemsResponseComponent uITotalGemsResponseComponent = CreateComponent<UITotalGemsResponseComponent>(index);
		uITotalGemsResponseComponent.Amount = newAmount;
		AddComponent(index, uITotalGemsResponseComponent);
	}

	public void ReplaceUITotalGemsResponse(int newAmount)
	{
		int index = 134;
		UITotalGemsResponseComponent uITotalGemsResponseComponent = CreateComponent<UITotalGemsResponseComponent>(index);
		uITotalGemsResponseComponent.Amount = newAmount;
		ReplaceComponent(index, uITotalGemsResponseComponent);
	}

	public void RemoveUITotalGemsResponse()
	{
		RemoveComponent(134);
	}

	public void AddUITotalStarsResponse(int newAmount)
	{
		int index = 136;
		UITotalStarsResponseComponent uITotalStarsResponseComponent = CreateComponent<UITotalStarsResponseComponent>(index);
		uITotalStarsResponseComponent.Amount = newAmount;
		AddComponent(index, uITotalStarsResponseComponent);
	}

	public void ReplaceUITotalStarsResponse(int newAmount)
	{
		int index = 136;
		UITotalStarsResponseComponent uITotalStarsResponseComponent = CreateComponent<UITotalStarsResponseComponent>(index);
		uITotalStarsResponseComponent.Amount = newAmount;
		ReplaceComponent(index, uITotalStarsResponseComponent);
	}

	public void RemoveUITotalStarsResponse()
	{
		RemoveComponent(136);
	}

	public void AddUIUnlockedMissionResponse(int newIndex)
	{
		int index = 138;
		UIUnlockedMissionResponseComponent uIUnlockedMissionResponseComponent = CreateComponent<UIUnlockedMissionResponseComponent>(index);
		uIUnlockedMissionResponseComponent.Index = newIndex;
		AddComponent(index, uIUnlockedMissionResponseComponent);
	}

	public void ReplaceUIUnlockedMissionResponse(int newIndex)
	{
		int index = 138;
		UIUnlockedMissionResponseComponent uIUnlockedMissionResponseComponent = CreateComponent<UIUnlockedMissionResponseComponent>(index);
		uIUnlockedMissionResponseComponent.Index = newIndex;
		ReplaceComponent(index, uIUnlockedMissionResponseComponent);
	}

	public void RemoveUIUnlockedMissionResponse()
	{
		RemoveComponent(138);
	}
}
