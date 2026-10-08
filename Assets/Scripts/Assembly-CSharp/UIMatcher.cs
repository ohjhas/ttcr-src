using Entitas;

public sealed class UIMatcher
{
	private static IMatcher<UIEntity> _matcherAbilityPopup;

	private static IMatcher<UIEntity> _matcherAsyncRequest;

	private static IMatcher<UIEntity> _matcherChangeScene;

	private static IMatcher<UIEntity> _matcherCharacterSelectScreen;

	private static IMatcher<UIEntity> _matcherChestRevealScreen;

	private static IMatcher<UIEntity> _matcherChestShopScreen;

	private static IMatcher<UIEntity> _matcherCloseScreen;

	private static IMatcher<UIEntity> _matcherCoinsListener;

	private static IMatcher<UIEntity> _matcherCompanionSelectScreen;

	private static IMatcher<UIEntity> _matcherDestroyed;

	private static IMatcher<UIEntity> _matcherDisplayUDID;

	private static IMatcher<UIEntity> _matcherDisplayUDIDListener;

	private static IMatcher<UIEntity> _matcherFlyingElementEffect;

	private static IMatcher<UIEntity> _matcherFreeChestStateListener;

	private static IMatcher<UIEntity> _matcherHardCurrencyListener;

	private static IMatcher<UIEntity> _matcherHomeScreen;

	private static IMatcher<UIEntity> _matcherHUD;

	private static IMatcher<UIEntity> _matcherIntialSceneDecider;

	private static IMatcher<UIEntity> _matcherIntroDone;

	private static IMatcher<UIEntity> _matcherLoadingSpinnerHide;

	private static IMatcher<UIEntity> _matcherLoadingSpinnerShow;

	private static IMatcher<UIEntity> _matcherNewSceneLoaded;

	private static IMatcher<UIEntity> _matcherO7SdkAdTrackingActiveListener;

	private static IMatcher<UIEntity> _matcherO7SdkAdTrackingListener;

	private static IMatcher<UIEntity> _matcherO7SdkCall;

	private static IMatcher<UIEntity> _matcherO7SdkCountryCodeListener;

	private static IMatcher<UIEntity> _matcherO7SdkDataListener;

	private static IMatcher<UIEntity> _matcherO7SdkGdprAdProviderSelectionListener;

	private static IMatcher<UIEntity> _matcherO7SdkGdprOptOutListener;

	private static IMatcher<UIEntity> _matcherO7SdkGridButtonUpdateRequest;

	private static IMatcher<UIEntity> _matcherO7SdkManualNewsListener;

	private static IMatcher<UIEntity> _matcherOpenedScreens;

	private static IMatcher<UIEntity> _matcherRaccoonIntrosListener;

	private static IMatcher<UIEntity> _matcherRefreshScreen;

	private static IMatcher<UIEntity> _matcherRelayPlayerScreen;

	private static IMatcher<UIEntity> _matcherResurectPlayerScreen;

	private static IMatcher<UIEntity> _matcherRevivePlayerScreen;

	private static IMatcher<UIEntity> _matcherSaveDataLoadedListener;

	private static IMatcher<UIEntity> _matcherSceneChangeRequestListener;

	private static IMatcher<UIEntity> _matcherScreenMap;

	private static IMatcher<UIEntity> _matcherSettingsScreen;

	private static IMatcher<UIEntity> _matcherSetup;

	private static IMatcher<UIEntity> _matcherShopScreen;

	private static IMatcher<UIEntity> _matcherStaminasListener;

	private static IMatcher<UIEntity> _matcherToggleMusicsListener;

	private static IMatcher<UIEntity> _matcherToggleSoundsListener;

	private static IMatcher<UIEntity> _matcherTutorialLayer;

	private static IMatcher<UIEntity> _matcherTutorialLevelPassedAvailability;

	private static IMatcher<UIEntity> _matcherTutorialsListener;

	private static IMatcher<UIEntity> _matcherUIAction;

	private static IMatcher<UIEntity> _matcherUIActionFinishedCallback;

	private static IMatcher<UIEntity> _matcherUIChapterListRequest;

	private static IMatcher<UIEntity> _matcherUIChapterListResponse;

	private static IMatcher<UIEntity> _matcherUICharacterSelected;

	private static IMatcher<UIEntity> _matcherUICharacterUnlocked;

	private static IMatcher<UIEntity> _matcherUICharacterUpgraded;

	private static IMatcher<UIEntity> _matcherUICharListRequest;

	private static IMatcher<UIEntity> _matcherUICharListResponse;

	private static IMatcher<UIEntity> _matcherUICharSelectedRequest;

	private static IMatcher<UIEntity> _matcherUICharSelectedResponse;

	private static IMatcher<UIEntity> _matcherUICharSingleRequest;

	private static IMatcher<UIEntity> _matcherUICharSingleResponse;

	private static IMatcher<UIEntity> _matcherUICheckPlayerProgress;

	private static IMatcher<UIEntity> _matcherUIChestBought;

	private static IMatcher<UIEntity> _matcherUIChestListRequest;

	private static IMatcher<UIEntity> _matcherUIChestListResponse;

	private static IMatcher<UIEntity> _matcherUIChestRewardRequest;

	private static IMatcher<UIEntity> _matcherUIChestRewardResponse;

	private static IMatcher<UIEntity> _matcherUIChestWon;

	private static IMatcher<UIEntity> _matcherUIClaimDailyMissionReward;

	private static IMatcher<UIEntity> _matcherUIClaimReward;

	private static IMatcher<UIEntity> _matcherUICompanionListRequest;

	private static IMatcher<UIEntity> _matcherUICompanionListResponse;

	private static IMatcher<UIEntity> _matcherUICompanionSelected;

	private static IMatcher<UIEntity> _matcherUICompanionUnlocked;

	private static IMatcher<UIEntity> _matcherUICompanionUpgraded;

	private static IMatcher<UIEntity> _matcherUI;

	private static IMatcher<UIEntity> _matcherUICompSelectedRequest;

	private static IMatcher<UIEntity> _matcherUICompSelectedResponse;

	private static IMatcher<UIEntity> _matcherUICompSingleRequest;

	private static IMatcher<UIEntity> _matcherUICompSingleResponse;

	private static IMatcher<UIEntity> _matcherUICurrentDailyMissionsRequest;

	private static IMatcher<UIEntity> _matcherUICurrentDailyMissionsResponse;

	private static IMatcher<UIEntity> _matcherUIDisplayAbility;

	private static IMatcher<UIEntity> _matcherUIDisplayExplanation;

	private static IMatcher<UIEntity> _matcherUIDisplayMissionDetails;

	private static IMatcher<UIEntity> _matcherUIDisplayTransactionConfrimation;

	private static IMatcher<UIEntity> _matcherUIExplanationPopupShown;

	private static IMatcher<UIEntity> _matcherUIFreeChestOpened;

	private static IMatcher<UIEntity> _matcherUIFreeChestRequest;

	private static IMatcher<UIEntity> _matcherUIFreeChestResponse;

	private static IMatcher<UIEntity> _matcherUIGiveDailyMissionsDailyReward;

	private static IMatcher<UIEntity> _matcherUIHideAbility;

	private static IMatcher<UIEntity> _matcherUIHideExplanation;

	private static IMatcher<UIEntity> _matcherUIHideMissionsDetails;

	private static IMatcher<UIEntity> _matcherUIHideTransactionConfirmation;

	private static IMatcher<UIEntity> _matcherUIIAPBought;

	private static IMatcher<UIEntity> _matcherUILastSelectedMissionRequest;

	private static IMatcher<UIEntity> _matcherUILastSelectedMissionResponse;

	private static IMatcher<UIEntity> _matcherUIMissionListRequest;

	private static IMatcher<UIEntity> _matcherUIMissionListResponse;

	private static IMatcher<UIEntity> _matcherUINewCharactersUnlockedRequest;

	private static IMatcher<UIEntity> _matcherUINewCharactersUnlockedResponse;

	private static IMatcher<UIEntity> _matcherUINewCompanionsUnlockedResponse;

	private static IMatcher<UIEntity> _matcherUINewCompanionUnlockedRequest;

	private static IMatcher<UIEntity> _matcherUINewMissionUnlockedRequest;

	private static IMatcher<UIEntity> _matcherUINewMissionUnlockedResponse;

	private static IMatcher<UIEntity> _matcherUINewUnlockedCharacterDisplayed;

	private static IMatcher<UIEntity> _matcherUINewUnlockedCompanionDisplayed;

	private static IMatcher<UIEntity> _matcherUIOpenCharSelection;

	private static IMatcher<UIEntity> _matcherUIOpenChestShop;

	private static IMatcher<UIEntity> _matcherUIOpenCompSelection;

	private static IMatcher<UIEntity> _matcherUIOpenContacts;

	private static IMatcher<UIEntity> _matcherUIOpenCountryCodes;

	private static IMatcher<UIEntity> _matcherUIOpenDailyMissions;

	private static IMatcher<UIEntity> _matcherUIOpenEPrivacy;

	private static IMatcher<UIEntity> _matcherUIOpenEula;

	private static IMatcher<UIEntity> _matcherUIOpenGdpr;

	private static IMatcher<UIEntity> _matcherUIOpenPrivacyAndLegacy;

	private static IMatcher<UIEntity> _matcherUIOpenPrivacyPolicy;

	private static IMatcher<UIEntity> _matcherUIOpenPvE;

	private static IMatcher<UIEntity> _matcherUIOpenSettings;

	private static IMatcher<UIEntity> _matcherUIOpenShop;

	private static IMatcher<UIEntity> _matcherUIPlayerLevel;

	private static IMatcher<UIEntity> _matcherUIPlayerProgressionStateRequest;

	private static IMatcher<UIEntity> _matcherUIPlayerProgressionStateResponse;

	private static IMatcher<UIEntity> _matcherUIPlayMission;

	private static IMatcher<UIEntity> _matcherUIRefillChar;

	private static IMatcher<UIEntity> _matcherUIRewardCollected;

	private static IMatcher<UIEntity> _matcherUISettingsFooterPress;

	private static IMatcher<UIEntity> _matcherUIShopIAPsListRequest;

	private static IMatcher<UIEntity> _matcherUIShopIAPsListResponse;

	private static IMatcher<UIEntity> _matcherUIShopScreenExit;

	private static IMatcher<UIEntity> _matcherUIShopSoftCurrencyPacksRequest;

	private static IMatcher<UIEntity> _matcherUIShopSoftCurrencyPacksResponse;

	private static IMatcher<UIEntity> _matcherUIShowCharacterInfo;

	private static IMatcher<UIEntity> _matcherUIShowCompanionInfo;

	private static IMatcher<UIEntity> _matcherUIShowCurrencyUpdate;

	private static IMatcher<UIEntity> _matcherUISoftCurrencyPackBought;

	private static IMatcher<UIEntity> _matcherUITotalCoinsRequest;

	private static IMatcher<UIEntity> _matcherUITotalCoinsResponse;

	private static IMatcher<UIEntity> _matcherUITotalGemsRequest;

	private static IMatcher<UIEntity> _matcherUITotalGemsResponse;

	private static IMatcher<UIEntity> _matcherUITotalStarsRequest;

	private static IMatcher<UIEntity> _matcherUITotalStarsResponse;

	private static IMatcher<UIEntity> _matcherUIUnlockedMissionRequest;

	private static IMatcher<UIEntity> _matcherUIUnlockedMissionResponse;

	private static IMatcher<UIEntity> _matcherUIUnlockNewMission;

	private static IMatcher<UIEntity> _matcherWinChestOnResultScreen;

	private static IMatcher<UIEntity> _matcherWorldSelectScreen;

	public static IMatcher<UIEntity> AbilityPopup
	{
		get
		{
			if (_matcherAbilityPopup == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(20);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherAbilityPopup = matcher;
			}
			return _matcherAbilityPopup;
		}
	}

	public static IMatcher<UIEntity> AsyncRequest
	{
		get
		{
			if (_matcherAsyncRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(default(int));
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherAsyncRequest = matcher;
			}
			return _matcherAsyncRequest;
		}
	}

	public static IMatcher<UIEntity> ChangeScene
	{
		get
		{
			if (_matcherChangeScene == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(21);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherChangeScene = matcher;
			}
			return _matcherChangeScene;
		}
	}

	public static IMatcher<UIEntity> CharacterSelectScreen
	{
		get
		{
			if (_matcherCharacterSelectScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(22);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherCharacterSelectScreen = matcher;
			}
			return _matcherCharacterSelectScreen;
		}
	}

	public static IMatcher<UIEntity> ChestRevealScreen
	{
		get
		{
			if (_matcherChestRevealScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(23);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherChestRevealScreen = matcher;
			}
			return _matcherChestRevealScreen;
		}
	}

	public static IMatcher<UIEntity> ChestShopScreen
	{
		get
		{
			if (_matcherChestShopScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(24);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherChestShopScreen = matcher;
			}
			return _matcherChestShopScreen;
		}
	}

	public static IMatcher<UIEntity> CloseScreen
	{
		get
		{
			if (_matcherCloseScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(25);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherCloseScreen = matcher;
			}
			return _matcherCloseScreen;
		}
	}

	public static IMatcher<UIEntity> CoinsListener
	{
		get
		{
			if (_matcherCoinsListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(1);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherCoinsListener = matcher;
			}
			return _matcherCoinsListener;
		}
	}

	public static IMatcher<UIEntity> CompanionSelectScreen
	{
		get
		{
			if (_matcherCompanionSelectScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(26);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherCompanionSelectScreen = matcher;
			}
			return _matcherCompanionSelectScreen;
		}
	}

	public static IMatcher<UIEntity> Destroyed
	{
		get
		{
			if (_matcherDestroyed == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(2);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherDestroyed = matcher;
			}
			return _matcherDestroyed;
		}
	}

	public static IMatcher<UIEntity> DisplayUDID
	{
		get
		{
			if (_matcherDisplayUDID == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(3);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherDisplayUDID = matcher;
			}
			return _matcherDisplayUDID;
		}
	}

	public static IMatcher<UIEntity> DisplayUDIDListener
	{
		get
		{
			if (_matcherDisplayUDIDListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(4);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherDisplayUDIDListener = matcher;
			}
			return _matcherDisplayUDIDListener;
		}
	}

	public static IMatcher<UIEntity> FlyingElementEffect
	{
		get
		{
			if (_matcherFlyingElementEffect == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(27);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherFlyingElementEffect = matcher;
			}
			return _matcherFlyingElementEffect;
		}
	}

	public static IMatcher<UIEntity> FreeChestStateListener
	{
		get
		{
			if (_matcherFreeChestStateListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(5);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherFreeChestStateListener = matcher;
			}
			return _matcherFreeChestStateListener;
		}
	}

	public static IMatcher<UIEntity> HardCurrencyListener
	{
		get
		{
			if (_matcherHardCurrencyListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(6);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherHardCurrencyListener = matcher;
			}
			return _matcherHardCurrencyListener;
		}
	}

	public static IMatcher<UIEntity> HomeScreen
	{
		get
		{
			if (_matcherHomeScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(28);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherHomeScreen = matcher;
			}
			return _matcherHomeScreen;
		}
	}

	public static IMatcher<UIEntity> HUD
	{
		get
		{
			if (_matcherHUD == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(29);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherHUD = matcher;
			}
			return _matcherHUD;
		}
	}

	public static IMatcher<UIEntity> IntialSceneDecider
	{
		get
		{
			if (_matcherIntialSceneDecider == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(7);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherIntialSceneDecider = matcher;
			}
			return _matcherIntialSceneDecider;
		}
	}

	public static IMatcher<UIEntity> IntroDone
	{
		get
		{
			if (_matcherIntroDone == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(8);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherIntroDone = matcher;
			}
			return _matcherIntroDone;
		}
	}

	public static IMatcher<UIEntity> LoadingSpinnerHide
	{
		get
		{
			if (_matcherLoadingSpinnerHide == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(9);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherLoadingSpinnerHide = matcher;
			}
			return _matcherLoadingSpinnerHide;
		}
	}

	public static IMatcher<UIEntity> LoadingSpinnerShow
	{
		get
		{
			if (_matcherLoadingSpinnerShow == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(10);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherLoadingSpinnerShow = matcher;
			}
			return _matcherLoadingSpinnerShow;
		}
	}

	public static IMatcher<UIEntity> NewSceneLoaded
	{
		get
		{
			if (_matcherNewSceneLoaded == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(30);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherNewSceneLoaded = matcher;
			}
			return _matcherNewSceneLoaded;
		}
	}

	public static IMatcher<UIEntity> O7SdkAdTrackingActiveListener
	{
		get
		{
			if (_matcherO7SdkAdTrackingActiveListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(11);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkAdTrackingActiveListener = matcher;
			}
			return _matcherO7SdkAdTrackingActiveListener;
		}
	}

	public static IMatcher<UIEntity> O7SdkAdTrackingListener
	{
		get
		{
			if (_matcherO7SdkAdTrackingListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(12);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkAdTrackingListener = matcher;
			}
			return _matcherO7SdkAdTrackingListener;
		}
	}

	public static IMatcher<UIEntity> O7SdkCall
	{
		get
		{
			if (_matcherO7SdkCall == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(13);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkCall = matcher;
			}
			return _matcherO7SdkCall;
		}
	}

	public static IMatcher<UIEntity> O7SdkCountryCodeListener
	{
		get
		{
			if (_matcherO7SdkCountryCodeListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(14);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkCountryCodeListener = matcher;
			}
			return _matcherO7SdkCountryCodeListener;
		}
	}

	public static IMatcher<UIEntity> O7SdkDataListener
	{
		get
		{
			if (_matcherO7SdkDataListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(15);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkDataListener = matcher;
			}
			return _matcherO7SdkDataListener;
		}
	}

	public static IMatcher<UIEntity> O7SdkGdprAdProviderSelectionListener
	{
		get
		{
			if (_matcherO7SdkGdprAdProviderSelectionListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(16);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkGdprAdProviderSelectionListener = matcher;
			}
			return _matcherO7SdkGdprAdProviderSelectionListener;
		}
	}

	public static IMatcher<UIEntity> O7SdkGdprOptOutListener
	{
		get
		{
			if (_matcherO7SdkGdprOptOutListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(17);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkGdprOptOutListener = matcher;
			}
			return _matcherO7SdkGdprOptOutListener;
		}
	}

	public static IMatcher<UIEntity> O7SdkGridButtonUpdateRequest
	{
		get
		{
			if (_matcherO7SdkGridButtonUpdateRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(18);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkGridButtonUpdateRequest = matcher;
			}
			return _matcherO7SdkGridButtonUpdateRequest;
		}
	}

	public static IMatcher<UIEntity> O7SdkManualNewsListener
	{
		get
		{
			if (_matcherO7SdkManualNewsListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(19);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherO7SdkManualNewsListener = matcher;
			}
			return _matcherO7SdkManualNewsListener;
		}
	}

	public static IMatcher<UIEntity> OpenedScreens
	{
		get
		{
			if (_matcherOpenedScreens == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(31);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherOpenedScreens = matcher;
			}
			return _matcherOpenedScreens;
		}
	}

	public static IMatcher<UIEntity> RaccoonIntrosListener
	{
		get
		{
			if (_matcherRaccoonIntrosListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(142);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherRaccoonIntrosListener = matcher;
			}
			return _matcherRaccoonIntrosListener;
		}
	}

	public static IMatcher<UIEntity> RefreshScreen
	{
		get
		{
			if (_matcherRefreshScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(32);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherRefreshScreen = matcher;
			}
			return _matcherRefreshScreen;
		}
	}

	public static IMatcher<UIEntity> RelayPlayerScreen
	{
		get
		{
			if (_matcherRelayPlayerScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(33);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherRelayPlayerScreen = matcher;
			}
			return _matcherRelayPlayerScreen;
		}
	}

	public static IMatcher<UIEntity> ResurectPlayerScreen
	{
		get
		{
			if (_matcherResurectPlayerScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(34);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherResurectPlayerScreen = matcher;
			}
			return _matcherResurectPlayerScreen;
		}
	}

	public static IMatcher<UIEntity> RevivePlayerScreen
	{
		get
		{
			if (_matcherRevivePlayerScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(35);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherRevivePlayerScreen = matcher;
			}
			return _matcherRevivePlayerScreen;
		}
	}

	public static IMatcher<UIEntity> SaveDataLoadedListener
	{
		get
		{
			if (_matcherSaveDataLoadedListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(143);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherSaveDataLoadedListener = matcher;
			}
			return _matcherSaveDataLoadedListener;
		}
	}

	public static IMatcher<UIEntity> SceneChangeRequestListener
	{
		get
		{
			if (_matcherSceneChangeRequestListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(144);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherSceneChangeRequestListener = matcher;
			}
			return _matcherSceneChangeRequestListener;
		}
	}

	public static IMatcher<UIEntity> ScreenMap
	{
		get
		{
			if (_matcherScreenMap == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(36);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherScreenMap = matcher;
			}
			return _matcherScreenMap;
		}
	}

	public static IMatcher<UIEntity> SettingsScreen
	{
		get
		{
			if (_matcherSettingsScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(37);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherSettingsScreen = matcher;
			}
			return _matcherSettingsScreen;
		}
	}

	public static IMatcher<UIEntity> Setup
	{
		get
		{
			if (_matcherSetup == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(38);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherSetup = matcher;
			}
			return _matcherSetup;
		}
	}

	public static IMatcher<UIEntity> ShopScreen
	{
		get
		{
			if (_matcherShopScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(39);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherShopScreen = matcher;
			}
			return _matcherShopScreen;
		}
	}

	public static IMatcher<UIEntity> StaminasListener
	{
		get
		{
			if (_matcherStaminasListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(145);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherStaminasListener = matcher;
			}
			return _matcherStaminasListener;
		}
	}

	public static IMatcher<UIEntity> ToggleMusicsListener
	{
		get
		{
			if (_matcherToggleMusicsListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(146);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherToggleMusicsListener = matcher;
			}
			return _matcherToggleMusicsListener;
		}
	}

	public static IMatcher<UIEntity> ToggleSoundsListener
	{
		get
		{
			if (_matcherToggleSoundsListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(147);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherToggleSoundsListener = matcher;
			}
			return _matcherToggleSoundsListener;
		}
	}

	public static IMatcher<UIEntity> TutorialLayer
	{
		get
		{
			if (_matcherTutorialLayer == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(40);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherTutorialLayer = matcher;
			}
			return _matcherTutorialLayer;
		}
	}

	public static IMatcher<UIEntity> TutorialLevelPassedAvailability
	{
		get
		{
			if (_matcherTutorialLevelPassedAvailability == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(148);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherTutorialLevelPassedAvailability = matcher;
			}
			return _matcherTutorialLevelPassedAvailability;
		}
	}

	public static IMatcher<UIEntity> TutorialsListener
	{
		get
		{
			if (_matcherTutorialsListener == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(149);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherTutorialsListener = matcher;
			}
			return _matcherTutorialsListener;
		}
	}

	public static IMatcher<UIEntity> UIAction
	{
		get
		{
			if (_matcherUIAction == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(41);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIAction = matcher;
			}
			return _matcherUIAction;
		}
	}

	public static IMatcher<UIEntity> UIActionFinishedCallback
	{
		get
		{
			if (_matcherUIActionFinishedCallback == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(42);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIActionFinishedCallback = matcher;
			}
			return _matcherUIActionFinishedCallback;
		}
	}

	public static IMatcher<UIEntity> UIChapterListRequest
	{
		get
		{
			if (_matcherUIChapterListRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(43);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChapterListRequest = matcher;
			}
			return _matcherUIChapterListRequest;
		}
	}

	public static IMatcher<UIEntity> UIChapterListResponse
	{
		get
		{
			if (_matcherUIChapterListResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(44);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChapterListResponse = matcher;
			}
			return _matcherUIChapterListResponse;
		}
	}

	public static IMatcher<UIEntity> UICharacterSelected
	{
		get
		{
			if (_matcherUICharacterSelected == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(45);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharacterSelected = matcher;
			}
			return _matcherUICharacterSelected;
		}
	}

	public static IMatcher<UIEntity> UICharacterUnlocked
	{
		get
		{
			if (_matcherUICharacterUnlocked == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(46);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharacterUnlocked = matcher;
			}
			return _matcherUICharacterUnlocked;
		}
	}

	public static IMatcher<UIEntity> UICharacterUpgraded
	{
		get
		{
			if (_matcherUICharacterUpgraded == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(47);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharacterUpgraded = matcher;
			}
			return _matcherUICharacterUpgraded;
		}
	}

	public static IMatcher<UIEntity> UICharListRequest
	{
		get
		{
			if (_matcherUICharListRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(48);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharListRequest = matcher;
			}
			return _matcherUICharListRequest;
		}
	}

	public static IMatcher<UIEntity> UICharListResponse
	{
		get
		{
			if (_matcherUICharListResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(49);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharListResponse = matcher;
			}
			return _matcherUICharListResponse;
		}
	}

	public static IMatcher<UIEntity> UICharSelectedRequest
	{
		get
		{
			if (_matcherUICharSelectedRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(50);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharSelectedRequest = matcher;
			}
			return _matcherUICharSelectedRequest;
		}
	}

	public static IMatcher<UIEntity> UICharSelectedResponse
	{
		get
		{
			if (_matcherUICharSelectedResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(51);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharSelectedResponse = matcher;
			}
			return _matcherUICharSelectedResponse;
		}
	}

	public static IMatcher<UIEntity> UICharSingleRequest
	{
		get
		{
			if (_matcherUICharSingleRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(52);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharSingleRequest = matcher;
			}
			return _matcherUICharSingleRequest;
		}
	}

	public static IMatcher<UIEntity> UICharSingleResponse
	{
		get
		{
			if (_matcherUICharSingleResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(53);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICharSingleResponse = matcher;
			}
			return _matcherUICharSingleResponse;
		}
	}

	public static IMatcher<UIEntity> UICheckPlayerProgress
	{
		get
		{
			if (_matcherUICheckPlayerProgress == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(54);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICheckPlayerProgress = matcher;
			}
			return _matcherUICheckPlayerProgress;
		}
	}

	public static IMatcher<UIEntity> UIChestBought
	{
		get
		{
			if (_matcherUIChestBought == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(55);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChestBought = matcher;
			}
			return _matcherUIChestBought;
		}
	}

	public static IMatcher<UIEntity> UIChestListRequest
	{
		get
		{
			if (_matcherUIChestListRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(56);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChestListRequest = matcher;
			}
			return _matcherUIChestListRequest;
		}
	}

	public static IMatcher<UIEntity> UIChestListResponse
	{
		get
		{
			if (_matcherUIChestListResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(57);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChestListResponse = matcher;
			}
			return _matcherUIChestListResponse;
		}
	}

	public static IMatcher<UIEntity> UIChestRewardRequest
	{
		get
		{
			if (_matcherUIChestRewardRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(58);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChestRewardRequest = matcher;
			}
			return _matcherUIChestRewardRequest;
		}
	}

	public static IMatcher<UIEntity> UIChestRewardResponse
	{
		get
		{
			if (_matcherUIChestRewardResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(59);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChestRewardResponse = matcher;
			}
			return _matcherUIChestRewardResponse;
		}
	}

	public static IMatcher<UIEntity> UIChestWon
	{
		get
		{
			if (_matcherUIChestWon == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(60);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIChestWon = matcher;
			}
			return _matcherUIChestWon;
		}
	}

	public static IMatcher<UIEntity> UIClaimDailyMissionReward
	{
		get
		{
			if (_matcherUIClaimDailyMissionReward == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(61);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIClaimDailyMissionReward = matcher;
			}
			return _matcherUIClaimDailyMissionReward;
		}
	}

	public static IMatcher<UIEntity> UIClaimReward
	{
		get
		{
			if (_matcherUIClaimReward == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(62);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIClaimReward = matcher;
			}
			return _matcherUIClaimReward;
		}
	}

	public static IMatcher<UIEntity> UICompanionListRequest
	{
		get
		{
			if (_matcherUICompanionListRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(63);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompanionListRequest = matcher;
			}
			return _matcherUICompanionListRequest;
		}
	}

	public static IMatcher<UIEntity> UICompanionListResponse
	{
		get
		{
			if (_matcherUICompanionListResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(64);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompanionListResponse = matcher;
			}
			return _matcherUICompanionListResponse;
		}
	}

	public static IMatcher<UIEntity> UICompanionSelected
	{
		get
		{
			if (_matcherUICompanionSelected == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(65);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompanionSelected = matcher;
			}
			return _matcherUICompanionSelected;
		}
	}

	public static IMatcher<UIEntity> UICompanionUnlocked
	{
		get
		{
			if (_matcherUICompanionUnlocked == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(66);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompanionUnlocked = matcher;
			}
			return _matcherUICompanionUnlocked;
		}
	}

	public static IMatcher<UIEntity> UICompanionUpgraded
	{
		get
		{
			if (_matcherUICompanionUpgraded == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(67);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompanionUpgraded = matcher;
			}
			return _matcherUICompanionUpgraded;
		}
	}

	public static IMatcher<UIEntity> UI
	{
		get
		{
			if (_matcherUI == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(68);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUI = matcher;
			}
			return _matcherUI;
		}
	}

	public static IMatcher<UIEntity> UICompSelectedRequest
	{
		get
		{
			if (_matcherUICompSelectedRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(69);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompSelectedRequest = matcher;
			}
			return _matcherUICompSelectedRequest;
		}
	}

	public static IMatcher<UIEntity> UICompSelectedResponse
	{
		get
		{
			if (_matcherUICompSelectedResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(70);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompSelectedResponse = matcher;
			}
			return _matcherUICompSelectedResponse;
		}
	}

	public static IMatcher<UIEntity> UICompSingleRequest
	{
		get
		{
			if (_matcherUICompSingleRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(71);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompSingleRequest = matcher;
			}
			return _matcherUICompSingleRequest;
		}
	}

	public static IMatcher<UIEntity> UICompSingleResponse
	{
		get
		{
			if (_matcherUICompSingleResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(72);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICompSingleResponse = matcher;
			}
			return _matcherUICompSingleResponse;
		}
	}

	public static IMatcher<UIEntity> UICurrentDailyMissionsRequest
	{
		get
		{
			if (_matcherUICurrentDailyMissionsRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(73);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICurrentDailyMissionsRequest = matcher;
			}
			return _matcherUICurrentDailyMissionsRequest;
		}
	}

	public static IMatcher<UIEntity> UICurrentDailyMissionsResponse
	{
		get
		{
			if (_matcherUICurrentDailyMissionsResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(74);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUICurrentDailyMissionsResponse = matcher;
			}
			return _matcherUICurrentDailyMissionsResponse;
		}
	}

	public static IMatcher<UIEntity> UIDisplayAbility
	{
		get
		{
			if (_matcherUIDisplayAbility == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(75);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIDisplayAbility = matcher;
			}
			return _matcherUIDisplayAbility;
		}
	}

	public static IMatcher<UIEntity> UIDisplayExplanation
	{
		get
		{
			if (_matcherUIDisplayExplanation == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(76);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIDisplayExplanation = matcher;
			}
			return _matcherUIDisplayExplanation;
		}
	}

	public static IMatcher<UIEntity> UIDisplayMissionDetails
	{
		get
		{
			if (_matcherUIDisplayMissionDetails == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(77);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIDisplayMissionDetails = matcher;
			}
			return _matcherUIDisplayMissionDetails;
		}
	}

	public static IMatcher<UIEntity> UIDisplayTransactionConfrimation
	{
		get
		{
			if (_matcherUIDisplayTransactionConfrimation == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(78);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIDisplayTransactionConfrimation = matcher;
			}
			return _matcherUIDisplayTransactionConfrimation;
		}
	}

	public static IMatcher<UIEntity> UIExplanationPopupShown
	{
		get
		{
			if (_matcherUIExplanationPopupShown == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(79);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIExplanationPopupShown = matcher;
			}
			return _matcherUIExplanationPopupShown;
		}
	}

	public static IMatcher<UIEntity> UIFreeChestOpened
	{
		get
		{
			if (_matcherUIFreeChestOpened == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(80);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIFreeChestOpened = matcher;
			}
			return _matcherUIFreeChestOpened;
		}
	}

	public static IMatcher<UIEntity> UIFreeChestRequest
	{
		get
		{
			if (_matcherUIFreeChestRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(81);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIFreeChestRequest = matcher;
			}
			return _matcherUIFreeChestRequest;
		}
	}

	public static IMatcher<UIEntity> UIFreeChestResponse
	{
		get
		{
			if (_matcherUIFreeChestResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(82);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIFreeChestResponse = matcher;
			}
			return _matcherUIFreeChestResponse;
		}
	}

	public static IMatcher<UIEntity> UIGiveDailyMissionsDailyReward
	{
		get
		{
			if (_matcherUIGiveDailyMissionsDailyReward == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(83);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIGiveDailyMissionsDailyReward = matcher;
			}
			return _matcherUIGiveDailyMissionsDailyReward;
		}
	}

	public static IMatcher<UIEntity> UIHideAbility
	{
		get
		{
			if (_matcherUIHideAbility == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(84);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIHideAbility = matcher;
			}
			return _matcherUIHideAbility;
		}
	}

	public static IMatcher<UIEntity> UIHideExplanation
	{
		get
		{
			if (_matcherUIHideExplanation == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(85);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIHideExplanation = matcher;
			}
			return _matcherUIHideExplanation;
		}
	}

	public static IMatcher<UIEntity> UIHideMissionsDetails
	{
		get
		{
			if (_matcherUIHideMissionsDetails == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(86);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIHideMissionsDetails = matcher;
			}
			return _matcherUIHideMissionsDetails;
		}
	}

	public static IMatcher<UIEntity> UIHideTransactionConfirmation
	{
		get
		{
			if (_matcherUIHideTransactionConfirmation == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(87);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIHideTransactionConfirmation = matcher;
			}
			return _matcherUIHideTransactionConfirmation;
		}
	}

	public static IMatcher<UIEntity> UIIAPBought
	{
		get
		{
			if (_matcherUIIAPBought == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(88);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIIAPBought = matcher;
			}
			return _matcherUIIAPBought;
		}
	}

	public static IMatcher<UIEntity> UILastSelectedMissionRequest
	{
		get
		{
			if (_matcherUILastSelectedMissionRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(89);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUILastSelectedMissionRequest = matcher;
			}
			return _matcherUILastSelectedMissionRequest;
		}
	}

	public static IMatcher<UIEntity> UILastSelectedMissionResponse
	{
		get
		{
			if (_matcherUILastSelectedMissionResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(90);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUILastSelectedMissionResponse = matcher;
			}
			return _matcherUILastSelectedMissionResponse;
		}
	}

	public static IMatcher<UIEntity> UIMissionListRequest
	{
		get
		{
			if (_matcherUIMissionListRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(91);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIMissionListRequest = matcher;
			}
			return _matcherUIMissionListRequest;
		}
	}

	public static IMatcher<UIEntity> UIMissionListResponse
	{
		get
		{
			if (_matcherUIMissionListResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(92);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIMissionListResponse = matcher;
			}
			return _matcherUIMissionListResponse;
		}
	}

	public static IMatcher<UIEntity> UINewCharactersUnlockedRequest
	{
		get
		{
			if (_matcherUINewCharactersUnlockedRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(93);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewCharactersUnlockedRequest = matcher;
			}
			return _matcherUINewCharactersUnlockedRequest;
		}
	}

	public static IMatcher<UIEntity> UINewCharactersUnlockedResponse
	{
		get
		{
			if (_matcherUINewCharactersUnlockedResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(94);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewCharactersUnlockedResponse = matcher;
			}
			return _matcherUINewCharactersUnlockedResponse;
		}
	}

	public static IMatcher<UIEntity> UINewCompanionsUnlockedResponse
	{
		get
		{
			if (_matcherUINewCompanionsUnlockedResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(95);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewCompanionsUnlockedResponse = matcher;
			}
			return _matcherUINewCompanionsUnlockedResponse;
		}
	}

	public static IMatcher<UIEntity> UINewCompanionUnlockedRequest
	{
		get
		{
			if (_matcherUINewCompanionUnlockedRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(96);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewCompanionUnlockedRequest = matcher;
			}
			return _matcherUINewCompanionUnlockedRequest;
		}
	}

	public static IMatcher<UIEntity> UINewMissionUnlockedRequest
	{
		get
		{
			if (_matcherUINewMissionUnlockedRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(97);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewMissionUnlockedRequest = matcher;
			}
			return _matcherUINewMissionUnlockedRequest;
		}
	}

	public static IMatcher<UIEntity> UINewMissionUnlockedResponse
	{
		get
		{
			if (_matcherUINewMissionUnlockedResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(98);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewMissionUnlockedResponse = matcher;
			}
			return _matcherUINewMissionUnlockedResponse;
		}
	}

	public static IMatcher<UIEntity> UINewUnlockedCharacterDisplayed
	{
		get
		{
			if (_matcherUINewUnlockedCharacterDisplayed == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(99);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewUnlockedCharacterDisplayed = matcher;
			}
			return _matcherUINewUnlockedCharacterDisplayed;
		}
	}

	public static IMatcher<UIEntity> UINewUnlockedCompanionDisplayed
	{
		get
		{
			if (_matcherUINewUnlockedCompanionDisplayed == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(100);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUINewUnlockedCompanionDisplayed = matcher;
			}
			return _matcherUINewUnlockedCompanionDisplayed;
		}
	}

	public static IMatcher<UIEntity> UIOpenCharSelection
	{
		get
		{
			if (_matcherUIOpenCharSelection == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(101);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenCharSelection = matcher;
			}
			return _matcherUIOpenCharSelection;
		}
	}

	public static IMatcher<UIEntity> UIOpenChestShop
	{
		get
		{
			if (_matcherUIOpenChestShop == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(102);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenChestShop = matcher;
			}
			return _matcherUIOpenChestShop;
		}
	}

	public static IMatcher<UIEntity> UIOpenCompSelection
	{
		get
		{
			if (_matcherUIOpenCompSelection == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(103);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenCompSelection = matcher;
			}
			return _matcherUIOpenCompSelection;
		}
	}

	public static IMatcher<UIEntity> UIOpenContacts
	{
		get
		{
			if (_matcherUIOpenContacts == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(104);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenContacts = matcher;
			}
			return _matcherUIOpenContacts;
		}
	}

	public static IMatcher<UIEntity> UIOpenCountryCodes
	{
		get
		{
			if (_matcherUIOpenCountryCodes == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(105);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenCountryCodes = matcher;
			}
			return _matcherUIOpenCountryCodes;
		}
	}

	public static IMatcher<UIEntity> UIOpenDailyMissions
	{
		get
		{
			if (_matcherUIOpenDailyMissions == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(106);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenDailyMissions = matcher;
			}
			return _matcherUIOpenDailyMissions;
		}
	}

	public static IMatcher<UIEntity> UIOpenEPrivacy
	{
		get
		{
			if (_matcherUIOpenEPrivacy == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(107);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenEPrivacy = matcher;
			}
			return _matcherUIOpenEPrivacy;
		}
	}

	public static IMatcher<UIEntity> UIOpenEula
	{
		get
		{
			if (_matcherUIOpenEula == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(108);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenEula = matcher;
			}
			return _matcherUIOpenEula;
		}
	}

	public static IMatcher<UIEntity> UIOpenGdpr
	{
		get
		{
			if (_matcherUIOpenGdpr == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(109);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenGdpr = matcher;
			}
			return _matcherUIOpenGdpr;
		}
	}

	public static IMatcher<UIEntity> UIOpenPrivacyAndLegacy
	{
		get
		{
			if (_matcherUIOpenPrivacyAndLegacy == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(110);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenPrivacyAndLegacy = matcher;
			}
			return _matcherUIOpenPrivacyAndLegacy;
		}
	}

	public static IMatcher<UIEntity> UIOpenPrivacyPolicy
	{
		get
		{
			if (_matcherUIOpenPrivacyPolicy == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(111);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenPrivacyPolicy = matcher;
			}
			return _matcherUIOpenPrivacyPolicy;
		}
	}

	public static IMatcher<UIEntity> UIOpenPvE
	{
		get
		{
			if (_matcherUIOpenPvE == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(112);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenPvE = matcher;
			}
			return _matcherUIOpenPvE;
		}
	}

	public static IMatcher<UIEntity> UIOpenSettings
	{
		get
		{
			if (_matcherUIOpenSettings == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(113);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenSettings = matcher;
			}
			return _matcherUIOpenSettings;
		}
	}

	public static IMatcher<UIEntity> UIOpenShop
	{
		get
		{
			if (_matcherUIOpenShop == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(114);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIOpenShop = matcher;
			}
			return _matcherUIOpenShop;
		}
	}

	public static IMatcher<UIEntity> UIPlayerLevel
	{
		get
		{
			if (_matcherUIPlayerLevel == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(115);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIPlayerLevel = matcher;
			}
			return _matcherUIPlayerLevel;
		}
	}

	public static IMatcher<UIEntity> UIPlayerProgressionStateRequest
	{
		get
		{
			if (_matcherUIPlayerProgressionStateRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(116);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIPlayerProgressionStateRequest = matcher;
			}
			return _matcherUIPlayerProgressionStateRequest;
		}
	}

	public static IMatcher<UIEntity> UIPlayerProgressionStateResponse
	{
		get
		{
			if (_matcherUIPlayerProgressionStateResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(117);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIPlayerProgressionStateResponse = matcher;
			}
			return _matcherUIPlayerProgressionStateResponse;
		}
	}

	public static IMatcher<UIEntity> UIPlayMission
	{
		get
		{
			if (_matcherUIPlayMission == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(118);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIPlayMission = matcher;
			}
			return _matcherUIPlayMission;
		}
	}

	public static IMatcher<UIEntity> UIRefillChar
	{
		get
		{
			if (_matcherUIRefillChar == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(119);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIRefillChar = matcher;
			}
			return _matcherUIRefillChar;
		}
	}

	public static IMatcher<UIEntity> UIRewardCollected
	{
		get
		{
			if (_matcherUIRewardCollected == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(120);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIRewardCollected = matcher;
			}
			return _matcherUIRewardCollected;
		}
	}

	public static IMatcher<UIEntity> UISettingsFooterPress
	{
		get
		{
			if (_matcherUISettingsFooterPress == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(121);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUISettingsFooterPress = matcher;
			}
			return _matcherUISettingsFooterPress;
		}
	}

	public static IMatcher<UIEntity> UIShopIAPsListRequest
	{
		get
		{
			if (_matcherUIShopIAPsListRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(122);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShopIAPsListRequest = matcher;
			}
			return _matcherUIShopIAPsListRequest;
		}
	}

	public static IMatcher<UIEntity> UIShopIAPsListResponse
	{
		get
		{
			if (_matcherUIShopIAPsListResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(123);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShopIAPsListResponse = matcher;
			}
			return _matcherUIShopIAPsListResponse;
		}
	}

	public static IMatcher<UIEntity> UIShopScreenExit
	{
		get
		{
			if (_matcherUIShopScreenExit == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(124);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShopScreenExit = matcher;
			}
			return _matcherUIShopScreenExit;
		}
	}

	public static IMatcher<UIEntity> UIShopSoftCurrencyPacksRequest
	{
		get
		{
			if (_matcherUIShopSoftCurrencyPacksRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(125);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShopSoftCurrencyPacksRequest = matcher;
			}
			return _matcherUIShopSoftCurrencyPacksRequest;
		}
	}

	public static IMatcher<UIEntity> UIShopSoftCurrencyPacksResponse
	{
		get
		{
			if (_matcherUIShopSoftCurrencyPacksResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(126);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShopSoftCurrencyPacksResponse = matcher;
			}
			return _matcherUIShopSoftCurrencyPacksResponse;
		}
	}

	public static IMatcher<UIEntity> UIShowCharacterInfo
	{
		get
		{
			if (_matcherUIShowCharacterInfo == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(127);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShowCharacterInfo = matcher;
			}
			return _matcherUIShowCharacterInfo;
		}
	}

	public static IMatcher<UIEntity> UIShowCompanionInfo
	{
		get
		{
			if (_matcherUIShowCompanionInfo == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(128);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShowCompanionInfo = matcher;
			}
			return _matcherUIShowCompanionInfo;
		}
	}

	public static IMatcher<UIEntity> UIShowCurrencyUpdate
	{
		get
		{
			if (_matcherUIShowCurrencyUpdate == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(129);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIShowCurrencyUpdate = matcher;
			}
			return _matcherUIShowCurrencyUpdate;
		}
	}

	public static IMatcher<UIEntity> UISoftCurrencyPackBought
	{
		get
		{
			if (_matcherUISoftCurrencyPackBought == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(130);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUISoftCurrencyPackBought = matcher;
			}
			return _matcherUISoftCurrencyPackBought;
		}
	}

	public static IMatcher<UIEntity> UITotalCoinsRequest
	{
		get
		{
			if (_matcherUITotalCoinsRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(131);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUITotalCoinsRequest = matcher;
			}
			return _matcherUITotalCoinsRequest;
		}
	}

	public static IMatcher<UIEntity> UITotalCoinsResponse
	{
		get
		{
			if (_matcherUITotalCoinsResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(132);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUITotalCoinsResponse = matcher;
			}
			return _matcherUITotalCoinsResponse;
		}
	}

	public static IMatcher<UIEntity> UITotalGemsRequest
	{
		get
		{
			if (_matcherUITotalGemsRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(133);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUITotalGemsRequest = matcher;
			}
			return _matcherUITotalGemsRequest;
		}
	}

	public static IMatcher<UIEntity> UITotalGemsResponse
	{
		get
		{
			if (_matcherUITotalGemsResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(134);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUITotalGemsResponse = matcher;
			}
			return _matcherUITotalGemsResponse;
		}
	}

	public static IMatcher<UIEntity> UITotalStarsRequest
	{
		get
		{
			if (_matcherUITotalStarsRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(135);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUITotalStarsRequest = matcher;
			}
			return _matcherUITotalStarsRequest;
		}
	}

	public static IMatcher<UIEntity> UITotalStarsResponse
	{
		get
		{
			if (_matcherUITotalStarsResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(136);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUITotalStarsResponse = matcher;
			}
			return _matcherUITotalStarsResponse;
		}
	}

	public static IMatcher<UIEntity> UIUnlockedMissionRequest
	{
		get
		{
			if (_matcherUIUnlockedMissionRequest == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(137);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIUnlockedMissionRequest = matcher;
			}
			return _matcherUIUnlockedMissionRequest;
		}
	}

	public static IMatcher<UIEntity> UIUnlockedMissionResponse
	{
		get
		{
			if (_matcherUIUnlockedMissionResponse == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(138);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIUnlockedMissionResponse = matcher;
			}
			return _matcherUIUnlockedMissionResponse;
		}
	}

	public static IMatcher<UIEntity> UIUnlockNewMission
	{
		get
		{
			if (_matcherUIUnlockNewMission == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(139);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherUIUnlockNewMission = matcher;
			}
			return _matcherUIUnlockNewMission;
		}
	}

	public static IMatcher<UIEntity> WinChestOnResultScreen
	{
		get
		{
			if (_matcherWinChestOnResultScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(140);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherWinChestOnResultScreen = matcher;
			}
			return _matcherWinChestOnResultScreen;
		}
	}

	public static IMatcher<UIEntity> WorldSelectScreen
	{
		get
		{
			if (_matcherWorldSelectScreen == null)
			{
				Matcher<UIEntity> matcher = (Matcher<UIEntity>)Matcher<UIEntity>.AllOf(141);
				matcher.componentNames = UIComponentsLookup.componentNames;
				_matcherWorldSelectScreen = matcher;
			}
			return _matcherWorldSelectScreen;
		}
	}

	public static IAllOfMatcher<UIEntity> AllOf(params int[] indices)
	{
		return Matcher<UIEntity>.AllOf(indices);
	}

	public static IAllOfMatcher<UIEntity> AllOf(params IMatcher<UIEntity>[] matchers)
	{
		return Matcher<UIEntity>.AllOf(matchers);
	}

	public static IAnyOfMatcher<UIEntity> AnyOf(params int[] indices)
	{
		return Matcher<UIEntity>.AnyOf(indices);
	}

	public static IAnyOfMatcher<UIEntity> AnyOf(params IMatcher<UIEntity>[] matchers)
	{
		return Matcher<UIEntity>.AnyOf(matchers);
	}
}
