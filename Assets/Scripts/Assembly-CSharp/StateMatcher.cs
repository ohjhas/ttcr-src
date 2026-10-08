using Entitas;

public sealed class StateMatcher
{
	private static IMatcher<StateEntity> _matcherAbilitiesBalancingData;

	private static IMatcher<StateEntity> _matcherActionDelay;

	private static IMatcher<StateEntity> _matcherActiveScreen;

	private static IMatcher<StateEntity> _matcherBalancingData;

	private static IMatcher<StateEntity> _matcherBalancingDataOverrideConfig;

	private static IMatcher<StateEntity> _matcherBoolean;

	private static IMatcher<StateEntity> _matcherBoughtCharacterUpgrade;

	private static IMatcher<StateEntity> _matcherBoughtChest;

	private static IMatcher<StateEntity> _matcherBoughtChestPrepared;

	private static IMatcher<StateEntity> _matcherBoughtCompanionUpgrade;

	private static IMatcher<StateEntity> _matcherBoughtEnergyRefill;

	private static IMatcher<StateEntity> _matcherBoughtHardCurrency;

	private static IMatcher<StateEntity> _matcherBoughtPlayerResurect;

	private static IMatcher<StateEntity> _matcherBoughtPlayerRevive;

	private static IMatcher<StateEntity> _matcherBoughtSoftCurrency;

	private static IMatcher<StateEntity> _matcherChangeTimeScale;

	private static IMatcher<StateEntity> _matcherChapterBalancingData;

	private static IMatcher<StateEntity> _matcherCharacterPartsPrice;

	private static IMatcher<StateEntity> _matcherCharacterStaticBalancingData;

	private static IMatcher<StateEntity> _matcherCharacterUpgradesBalancingData;

	private static IMatcher<StateEntity> _matcherCloseScreenAction;

	private static IMatcher<StateEntity> _matcherCompanionPartsPrice;

	private static IMatcher<StateEntity> _matcherCompanionsStaticBalancingData;

	private static IMatcher<StateEntity> _matcherCompanionsUpgradesBalancingData;

	private static IMatcher<StateEntity> _matcherCurrentRealTime;

	private static IMatcher<StateEntity> _matcherCurrentTimeListener;

	private static IMatcher<StateEntity> _matcherDailyMissionChestStateUpdate;

	private static IMatcher<StateEntity> _matcherDailyMissionClaimUpdate;

	private static IMatcher<StateEntity> _matcherDailyMissionProgressUpdate;

	private static IMatcher<StateEntity> _matcherDailyMissionsBalancingData;

	private static IMatcher<StateEntity> _matcherDailyMissionsCollection;

	private static IMatcher<StateEntity> _matcherDateTime;

	private static IMatcher<StateEntity> _matcherDestroyed;

	private static IMatcher<StateEntity> _matcherDisplayLockLayerAction;

	private static IMatcher<StateEntity> _matcherEulaClickedEventToBeSent;

	private static IMatcher<StateEntity> _matcherEventAchievementComplete;

	private static IMatcher<StateEntity> _matcherEventCustom;

	private static IMatcher<StateEntity> _matcherEventCustomSend;

	private static IMatcher<StateEntity> _matcherEventEarnVirtualCurrency;

	private static IMatcher<StateEntity> _matcherEventInAppPurchase;

	private static IMatcher<StateEntity> _matcherEventLevelComplete;

	private static IMatcher<StateEntity> _matcherEventLevelUp;

	private static IMatcher<StateEntity> _matcherEventSelectContent;

	private static IMatcher<StateEntity> _matcherEventSpendVirtualCurrency;

	private static IMatcher<StateEntity> _matcherEventTutorialBegin;

	private static IMatcher<StateEntity> _matcherEventTutorialComplete;

	private static IMatcher<StateEntity> _matcherFindTargetAction;

	private static IMatcher<StateEntity> _matcherFreeChest;

	private static IMatcher<StateEntity> _matcherFreePrice;

	private static IMatcher<StateEntity> _matcherGameFirstLaunch;

	private static IMatcher<StateEntity> _matcherGatchaBalancingData;

	private static IMatcher<StateEntity> _matcherGoBackAction;

	private static IMatcher<StateEntity> _matcherGrantPlayerExperience;

	private static IMatcher<StateEntity> _matcherHardCurrencyPrice;

	private static IMatcher<StateEntity> _matcherHiglightTargetAction;

	private static IMatcher<StateEntity> _matcherInitConfigData;

	private static IMatcher<StateEntity> _matcherInitialStartSceneDisplayed;

	private static IMatcher<StateEntity> _matcherInitLocalUserState;

	private static IMatcher<StateEntity> _matcherInteger;

	private static IMatcher<StateEntity> _matcherLabel;

	private static IMatcher<StateEntity> _matcherLastLoggedScreen;

	private static IMatcher<StateEntity> _matcherLastSceneFramesCount;

	private static IMatcher<StateEntity> _matcherLastSceneTransitionTime;

	private static IMatcher<StateEntity> _matcherLevelCompleted;

	private static IMatcher<StateEntity> _matcherLocalUserState;

	private static IMatcher<StateEntity> _matcherLogO7SdkIapBuyRequest;

	private static IMatcher<StateEntity> _matcherMissionBalancingData;

	private static IMatcher<StateEntity> _matcherMissionChestsBalancingData;

	private static IMatcher<StateEntity> _matcherMoveTargetToTopAction;

	private static IMatcher<StateEntity> _matcherMuteMusic;

	private static IMatcher<StateEntity> _matcherMuteSound;

	private static IMatcher<StateEntity> _matcherMuteSoundSource;

	private static IMatcher<StateEntity> _matcherName;

	private static IMatcher<StateEntity> _matcherNewLevelUnlocked;

	private static IMatcher<StateEntity> _matcherO7SdkAdTracking;

	private static IMatcher<StateEntity> _matcherO7SdkAgeGateDecider;

	private static IMatcher<StateEntity> _matcherO7SdkAgeGateManager;

	private static IMatcher<StateEntity> _matcherO7SdkAgeGateSelected;

	private static IMatcher<StateEntity> _matcherO7SdkAgeGateShow;

	private static IMatcher<StateEntity> _matcherO7SdkAutoNewsRequest;

	private static IMatcher<StateEntity> _matcherO7SdkBalancingData;

	private static IMatcher<StateEntity> _matcherO7SdkBannerAdHide;

	private static IMatcher<StateEntity> _matcherO7SdkBannerAdManager;

	private static IMatcher<StateEntity> _matcherO7SdkBannerAdShow;

	private static IMatcher<StateEntity> _matcherO7SdkCall;

	private static IMatcher<StateEntity> _matcherO7Sdk;

	private static IMatcher<StateEntity> _matcherO7SdkConfigItems;

	private static IMatcher<StateEntity> _matcherO7SdkCountryCodes;

	private static IMatcher<StateEntity> _matcherO7SdkCrossPromo;

	private static IMatcher<StateEntity> _matcherO7SdkCrossPromoManager;

	private static IMatcher<StateEntity> _matcherO7SdkData;

	private static IMatcher<StateEntity> _matcherO7SdkDataManager;

	private static IMatcher<StateEntity> _matcherO7SdkEventManager;

	private static IMatcher<StateEntity> _matcherO7SdkExtConfig;

	private static IMatcher<StateEntity> _matcherO7SdkForceAgeGate;

	private static IMatcher<StateEntity> _matcherO7SdkForceCheckProcessed;

	private static IMatcher<StateEntity> _matcherO7SdkForceGdpr;

	private static IMatcher<StateEntity> _matcherO7SdkGateDone;

	private static IMatcher<StateEntity> _matcherO7SdkGdprAdProviders;

	private static IMatcher<StateEntity> _matcherO7SdkGdprAdProvidersFetch;

	private static IMatcher<StateEntity> _matcherO7SdkGdprAdProvidersSelectAll;

	private static IMatcher<StateEntity> _matcherO7SdkGdprAdProvidersSubmitSelection;

	private static IMatcher<StateEntity> _matcherO7SdkGdprAdProvidersUnSelectAll;

	private static IMatcher<StateEntity> _matcherO7SdkGdprCheck;

	private static IMatcher<StateEntity> _matcherO7SdkGdprConsentAccepted;

	private static IMatcher<StateEntity> _matcherO7SdkGdprConsentDeclined;

	private static IMatcher<StateEntity> _matcherO7SdkGdprConsentMustShow;

	private static IMatcher<StateEntity> _matcherO7SdkGdprConsentPrefSelected;

	private static IMatcher<StateEntity> _matcherO7SdkGdprConsentRequired;

	private static IMatcher<StateEntity> _matcherO7SdkGdprConsentShow;

	private static IMatcher<StateEntity> _matcherO7SdkGdprDone;

	private static IMatcher<StateEntity> _matcherO7SdkGdprEnableAdProvider;

	private static IMatcher<StateEntity> _matcherO7SdkGdprManager;

	private static IMatcher<StateEntity> _matcherO7SdkGdprProvidersSendRequest;

	private static IMatcher<StateEntity> _matcherO7SdkGdprRequest;

	private static IMatcher<StateEntity> _matcherO7SdkGdprShowAdProvidersSelection;

	private static IMatcher<StateEntity> _matcherO7SdkGdprShowConsentTool;

	private static IMatcher<StateEntity> _matcherO7SdkGdprShowConsentToolType;

	private static IMatcher<StateEntity> _matcherO7SdkGdprShowDecider;

	private static IMatcher<StateEntity> _matcherO7SdkGdprShowFromSetting;

	private static IMatcher<StateEntity> _matcherO7SdkGenderGateSelected;

	private static IMatcher<StateEntity> _matcherO7SdkGenderGateShow;

	private static IMatcher<StateEntity> _matcherO7SdkGridDownloaded;

	private static IMatcher<StateEntity> _matcherO7SdkIapBuyError;

	private static IMatcher<StateEntity> _matcherO7SdkIapBuyFail;

	private static IMatcher<StateEntity> _matcherO7SdkIapBuyRequest;

	private static IMatcher<StateEntity> _matcherO7SdkIapBuySuccess;

	private static IMatcher<StateEntity> _matcherO7SdkIapItemPriceData;

	private static IMatcher<StateEntity> _matcherO7SdkIapItemsFetchRequest;

	private static IMatcher<StateEntity> _matcherO7SdkIapPacks;

	private static IMatcher<StateEntity> _matcherO7SdkInterstitialAd;

	private static IMatcher<StateEntity> _matcherO7SdkInterstitialAdManager;

	private static IMatcher<StateEntity> _matcherO7SdkInterstitialShown;

	private static IMatcher<StateEntity> _matcherO7SdkManager;

	private static IMatcher<StateEntity> _matcherO7SdkPaidUser;

	private static IMatcher<StateEntity> _matcherO7SdkPrivacyURL;

	private static IMatcher<StateEntity> _matcherO7SdkPurchaseManager;

	private static IMatcher<StateEntity> _matcherO7SdkRateMeClosed;

	private static IMatcher<StateEntity> _matcherO7SdkRateMe;

	private static IMatcher<StateEntity> _matcherO7SdkRateMeDontLike;

	private static IMatcher<StateEntity> _matcherO7SdkRateMeLater;

	private static IMatcher<StateEntity> _matcherO7SdkRateMeNow;

	private static IMatcher<StateEntity> _matcherO7SdkReadyToLoadGame;

	private static IMatcher<StateEntity> _matcherO7SdkRewardAdFetched;

	private static IMatcher<StateEntity> _matcherO7SdkRewardAdsFetchRequested;

	private static IMatcher<StateEntity> _matcherO7SdkRewardAdsShow;

	private static IMatcher<StateEntity> _matcherO7SdkRewardAwarded;

	private static IMatcher<StateEntity> _matcherO7SdkRewardedAdManager;

	private static IMatcher<StateEntity> _matcherO7SdkRewardNotAwarded;

	private static IMatcher<StateEntity> _matcherO7SdkRewardOfferType;

	private static IMatcher<StateEntity> _matcherO7SdkRewardTransaction;

	private static IMatcher<StateEntity> _matcherO7SdkSelectCountryCode;

	private static IMatcher<StateEntity> _matcherO7SdkSendLogs;

	private static IMatcher<StateEntity> _matcherO7SdkUserStateManager;

	private static IMatcher<StateEntity> _matcherO7SdkUtilityManager;

	private static IMatcher<StateEntity> _matcherO7SdkValidIapPacks;

	private static IMatcher<StateEntity> _matcherO7SdkVideoGallery;

	private static IMatcher<StateEntity> _matcherO7SdkVideoGalleryManager;

	private static IMatcher<StateEntity> _matcherOpenFreeChest;

	private static IMatcher<StateEntity> _matcherOpenPrivacyUrl;

	private static IMatcher<StateEntity> _matcherOpenUrl;

	private static IMatcher<StateEntity> _matcherOverrideBalancingConfiguration;

	private static IMatcher<StateEntity> _matcherPassLevelAction;

	private static IMatcher<StateEntity> _matcherPendingAsyncRequests;

	private static IMatcher<StateEntity> _matcherPlayerExperienceGranted;

	private static IMatcher<StateEntity> _matcherPlayerLevelBalancingData;

	private static IMatcher<StateEntity> _matcherPlayerLevelExperienceAmountData;

	private static IMatcher<StateEntity> _matcherPlayerLevelListener;

	private static IMatcher<StateEntity> _matcherPlayerLevelUp;

	private static IMatcher<StateEntity> _matcherPlayerProgressionUpdated;

	private static IMatcher<StateEntity> _matcherPlayLoopSoundEffect;

	private static IMatcher<StateEntity> _matcherPlayMusic;

	private static IMatcher<StateEntity> _matcherPlaySoundEffect;

	private static IMatcher<StateEntity> _matcherPreviousScreen;

	private static IMatcher<StateEntity> _matcherPrivacyAndLegacyBalancingData;

	private static IMatcher<StateEntity> _matcherPrivacyAndLegalClickedEventToBeSent;

	private static IMatcher<StateEntity> _matcherRemoteUserState;

	private static IMatcher<StateEntity> _matcherReplaceTargetEvent;

	private static IMatcher<StateEntity> _matcherReplaceUserState;

	private static IMatcher<StateEntity> _matcherRestoreSettingsBalancingData;

	private static IMatcher<StateEntity> _matcherSavedData;

	private static IMatcher<StateEntity> _matcherSession;

	private static IMatcher<StateEntity> _matcherShopIAPsBalancingData;

	private static IMatcher<StateEntity> _matcherShopSoftCurrencyPacksData;

	private static IMatcher<StateEntity> _matcherShowHandAciton;

	private static IMatcher<StateEntity> _matcherSoftCurrnecyPrice;

	private static IMatcher<StateEntity> _matcherStaminaSettingsBalancingData;

	private static IMatcher<StateEntity> _matcherStopLoopSoundEffect;

	private static IMatcher<StateEntity> _matcherStopMusic;

	private static IMatcher<StateEntity> _matcherStringData;

	private static IMatcher<StateEntity> _matcherToLoad;

	private static IMatcher<StateEntity> _matcherToSave;

	private static IMatcher<StateEntity> _matcherTransactionBegin;

	private static IMatcher<StateEntity> _matcherTransactionCallback;

	private static IMatcher<StateEntity> _matcherTransactionComplete;

	private static IMatcher<StateEntity> _matcherTransaction;

	private static IMatcher<StateEntity> _matcherTransactionGranted;

	private static IMatcher<StateEntity> _matcherTransactionImage;

	private static IMatcher<StateEntity> _matcherTransactionName;

	private static IMatcher<StateEntity> _matcherTransactionNotValidated;

	private static IMatcher<StateEntity> _matcherTransactionOrigin;

	private static IMatcher<StateEntity> _matcherTransactionRequireConfirmation;

	private static IMatcher<StateEntity> _matcherTransactionValidated;

	private static IMatcher<StateEntity> _matcherTransitionInformation;

	private static IMatcher<StateEntity> _matcherTutorialActiveStep;

	private static IMatcher<StateEntity> _matcherTutorial;

	private static IMatcher<StateEntity> _matcherTutorialDisabled;

	private static IMatcher<StateEntity> _matcherTutorialDisableStep;

	private static IMatcher<StateEntity> _matcherTutorialEnableStep;

	private static IMatcher<StateEntity> _matcherTutorialInitialized;

	private static IMatcher<StateEntity> _matcherTutorialOutgameUnlockLevelsBalancingData;

	private static IMatcher<StateEntity> _matcherTutorialPossibleGroups;

	private static IMatcher<StateEntity> _matcherTutorialStepDisableTriggered;

	private static IMatcher<StateEntity> _matcherTutorialStepEnableTriggered;

	private static IMatcher<StateEntity> _matcherTutorialStepsQueue;

	private static IMatcher<StateEntity> _matcherTutorialTargetElement;

	private static IMatcher<StateEntity> _matcherTutorialTriggersList;

	private static IMatcher<StateEntity> _matcherUDIDHash;

	private static IMatcher<StateEntity> _matcherUpgradeCharacter;

	private static IMatcher<StateEntity> _matcherUpgradeCompanion;

	private static IMatcher<StateEntity> _matcherUserStateData;

	private static IMatcher<StateEntity> _matcherUserStateLastUpdateTime;

	private static IMatcher<StateEntity> _matcherUserStateShow;

	private static IMatcher<StateEntity> _matcherUserStateSolveConflict;

	private static IMatcher<StateEntity> _matcherUserStateTryStartNewGame;

	private static IMatcher<StateEntity> _matcherWorldSelectUnlockingAnimationStoped;

	public static IMatcher<StateEntity> AbilitiesBalancingData
	{
		get
		{
			if (_matcherAbilitiesBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(default(int));
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherAbilitiesBalancingData = matcher;
			}
			return _matcherAbilitiesBalancingData;
		}
	}

	public static IMatcher<StateEntity> ActionDelay
	{
		get
		{
			if (_matcherActionDelay == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(1);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherActionDelay = matcher;
			}
			return _matcherActionDelay;
		}
	}

	public static IMatcher<StateEntity> ActiveScreen
	{
		get
		{
			if (_matcherActiveScreen == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(2);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherActiveScreen = matcher;
			}
			return _matcherActiveScreen;
		}
	}

	public static IMatcher<StateEntity> BalancingData
	{
		get
		{
			if (_matcherBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(3);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBalancingData = matcher;
			}
			return _matcherBalancingData;
		}
	}

	public static IMatcher<StateEntity> BalancingDataOverrideConfig
	{
		get
		{
			if (_matcherBalancingDataOverrideConfig == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(4);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBalancingDataOverrideConfig = matcher;
			}
			return _matcherBalancingDataOverrideConfig;
		}
	}

	public static IMatcher<StateEntity> Boolean
	{
		get
		{
			if (_matcherBoolean == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(5);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoolean = matcher;
			}
			return _matcherBoolean;
		}
	}

	public static IMatcher<StateEntity> BoughtCharacterUpgrade
	{
		get
		{
			if (_matcherBoughtCharacterUpgrade == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(6);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtCharacterUpgrade = matcher;
			}
			return _matcherBoughtCharacterUpgrade;
		}
	}

	public static IMatcher<StateEntity> BoughtChest
	{
		get
		{
			if (_matcherBoughtChest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(7);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtChest = matcher;
			}
			return _matcherBoughtChest;
		}
	}

	public static IMatcher<StateEntity> BoughtChestPrepared
	{
		get
		{
			if (_matcherBoughtChestPrepared == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(8);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtChestPrepared = matcher;
			}
			return _matcherBoughtChestPrepared;
		}
	}

	public static IMatcher<StateEntity> BoughtCompanionUpgrade
	{
		get
		{
			if (_matcherBoughtCompanionUpgrade == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(9);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtCompanionUpgrade = matcher;
			}
			return _matcherBoughtCompanionUpgrade;
		}
	}

	public static IMatcher<StateEntity> BoughtEnergyRefill
	{
		get
		{
			if (_matcherBoughtEnergyRefill == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(10);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtEnergyRefill = matcher;
			}
			return _matcherBoughtEnergyRefill;
		}
	}

	public static IMatcher<StateEntity> BoughtHardCurrency
	{
		get
		{
			if (_matcherBoughtHardCurrency == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(11);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtHardCurrency = matcher;
			}
			return _matcherBoughtHardCurrency;
		}
	}

	public static IMatcher<StateEntity> BoughtPlayerResurect
	{
		get
		{
			if (_matcherBoughtPlayerResurect == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(12);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtPlayerResurect = matcher;
			}
			return _matcherBoughtPlayerResurect;
		}
	}

	public static IMatcher<StateEntity> BoughtPlayerRevive
	{
		get
		{
			if (_matcherBoughtPlayerRevive == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(13);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtPlayerRevive = matcher;
			}
			return _matcherBoughtPlayerRevive;
		}
	}

	public static IMatcher<StateEntity> BoughtSoftCurrency
	{
		get
		{
			if (_matcherBoughtSoftCurrency == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(14);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherBoughtSoftCurrency = matcher;
			}
			return _matcherBoughtSoftCurrency;
		}
	}

	public static IMatcher<StateEntity> ChangeTimeScale
	{
		get
		{
			if (_matcherChangeTimeScale == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(15);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherChangeTimeScale = matcher;
			}
			return _matcherChangeTimeScale;
		}
	}

	public static IMatcher<StateEntity> ChapterBalancingData
	{
		get
		{
			if (_matcherChapterBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(16);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherChapterBalancingData = matcher;
			}
			return _matcherChapterBalancingData;
		}
	}

	public static IMatcher<StateEntity> CharacterPartsPrice
	{
		get
		{
			if (_matcherCharacterPartsPrice == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(17);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCharacterPartsPrice = matcher;
			}
			return _matcherCharacterPartsPrice;
		}
	}

	public static IMatcher<StateEntity> CharacterStaticBalancingData
	{
		get
		{
			if (_matcherCharacterStaticBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(18);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCharacterStaticBalancingData = matcher;
			}
			return _matcherCharacterStaticBalancingData;
		}
	}

	public static IMatcher<StateEntity> CharacterUpgradesBalancingData
	{
		get
		{
			if (_matcherCharacterUpgradesBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(19);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCharacterUpgradesBalancingData = matcher;
			}
			return _matcherCharacterUpgradesBalancingData;
		}
	}

	public static IMatcher<StateEntity> CloseScreenAction
	{
		get
		{
			if (_matcherCloseScreenAction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(20);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCloseScreenAction = matcher;
			}
			return _matcherCloseScreenAction;
		}
	}

	public static IMatcher<StateEntity> CompanionPartsPrice
	{
		get
		{
			if (_matcherCompanionPartsPrice == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(21);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCompanionPartsPrice = matcher;
			}
			return _matcherCompanionPartsPrice;
		}
	}

	public static IMatcher<StateEntity> CompanionsStaticBalancingData
	{
		get
		{
			if (_matcherCompanionsStaticBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(22);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCompanionsStaticBalancingData = matcher;
			}
			return _matcherCompanionsStaticBalancingData;
		}
	}

	public static IMatcher<StateEntity> CompanionsUpgradesBalancingData
	{
		get
		{
			if (_matcherCompanionsUpgradesBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(23);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCompanionsUpgradesBalancingData = matcher;
			}
			return _matcherCompanionsUpgradesBalancingData;
		}
	}

	public static IMatcher<StateEntity> CurrentRealTime
	{
		get
		{
			if (_matcherCurrentRealTime == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(24);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCurrentRealTime = matcher;
			}
			return _matcherCurrentRealTime;
		}
	}

	public static IMatcher<StateEntity> CurrentTimeListener
	{
		get
		{
			if (_matcherCurrentTimeListener == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(25);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherCurrentTimeListener = matcher;
			}
			return _matcherCurrentTimeListener;
		}
	}

	public static IMatcher<StateEntity> DailyMissionChestStateUpdate
	{
		get
		{
			if (_matcherDailyMissionChestStateUpdate == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(26);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDailyMissionChestStateUpdate = matcher;
			}
			return _matcherDailyMissionChestStateUpdate;
		}
	}

	public static IMatcher<StateEntity> DailyMissionClaimUpdate
	{
		get
		{
			if (_matcherDailyMissionClaimUpdate == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(27);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDailyMissionClaimUpdate = matcher;
			}
			return _matcherDailyMissionClaimUpdate;
		}
	}

	public static IMatcher<StateEntity> DailyMissionProgressUpdate
	{
		get
		{
			if (_matcherDailyMissionProgressUpdate == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(28);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDailyMissionProgressUpdate = matcher;
			}
			return _matcherDailyMissionProgressUpdate;
		}
	}

	public static IMatcher<StateEntity> DailyMissionsBalancingData
	{
		get
		{
			if (_matcherDailyMissionsBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(29);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDailyMissionsBalancingData = matcher;
			}
			return _matcherDailyMissionsBalancingData;
		}
	}

	public static IMatcher<StateEntity> DailyMissionsCollection
	{
		get
		{
			if (_matcherDailyMissionsCollection == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(30);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDailyMissionsCollection = matcher;
			}
			return _matcherDailyMissionsCollection;
		}
	}

	public static IMatcher<StateEntity> DateTime
	{
		get
		{
			if (_matcherDateTime == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(31);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDateTime = matcher;
			}
			return _matcherDateTime;
		}
	}

	public static IMatcher<StateEntity> Destroyed
	{
		get
		{
			if (_matcherDestroyed == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(32);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDestroyed = matcher;
			}
			return _matcherDestroyed;
		}
	}

	public static IMatcher<StateEntity> DisplayLockLayerAction
	{
		get
		{
			if (_matcherDisplayLockLayerAction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(33);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherDisplayLockLayerAction = matcher;
			}
			return _matcherDisplayLockLayerAction;
		}
	}

	public static IMatcher<StateEntity> EulaClickedEventToBeSent
	{
		get
		{
			if (_matcherEulaClickedEventToBeSent == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(34);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEulaClickedEventToBeSent = matcher;
			}
			return _matcherEulaClickedEventToBeSent;
		}
	}

	public static IMatcher<StateEntity> EventAchievementComplete
	{
		get
		{
			if (_matcherEventAchievementComplete == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(35);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventAchievementComplete = matcher;
			}
			return _matcherEventAchievementComplete;
		}
	}

	public static IMatcher<StateEntity> EventCustom
	{
		get
		{
			if (_matcherEventCustom == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(36);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventCustom = matcher;
			}
			return _matcherEventCustom;
		}
	}

	public static IMatcher<StateEntity> EventCustomSend
	{
		get
		{
			if (_matcherEventCustomSend == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(37);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventCustomSend = matcher;
			}
			return _matcherEventCustomSend;
		}
	}

	public static IMatcher<StateEntity> EventEarnVirtualCurrency
	{
		get
		{
			if (_matcherEventEarnVirtualCurrency == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(38);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventEarnVirtualCurrency = matcher;
			}
			return _matcherEventEarnVirtualCurrency;
		}
	}

	public static IMatcher<StateEntity> EventInAppPurchase
	{
		get
		{
			if (_matcherEventInAppPurchase == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(39);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventInAppPurchase = matcher;
			}
			return _matcherEventInAppPurchase;
		}
	}

	public static IMatcher<StateEntity> EventLevelComplete
	{
		get
		{
			if (_matcherEventLevelComplete == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(40);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventLevelComplete = matcher;
			}
			return _matcherEventLevelComplete;
		}
	}

	public static IMatcher<StateEntity> EventLevelUp
	{
		get
		{
			if (_matcherEventLevelUp == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(41);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventLevelUp = matcher;
			}
			return _matcherEventLevelUp;
		}
	}

	public static IMatcher<StateEntity> EventSelectContent
	{
		get
		{
			if (_matcherEventSelectContent == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(42);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventSelectContent = matcher;
			}
			return _matcherEventSelectContent;
		}
	}

	public static IMatcher<StateEntity> EventSpendVirtualCurrency
	{
		get
		{
			if (_matcherEventSpendVirtualCurrency == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(43);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventSpendVirtualCurrency = matcher;
			}
			return _matcherEventSpendVirtualCurrency;
		}
	}

	public static IMatcher<StateEntity> EventTutorialBegin
	{
		get
		{
			if (_matcherEventTutorialBegin == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(44);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventTutorialBegin = matcher;
			}
			return _matcherEventTutorialBegin;
		}
	}

	public static IMatcher<StateEntity> EventTutorialComplete
	{
		get
		{
			if (_matcherEventTutorialComplete == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(45);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherEventTutorialComplete = matcher;
			}
			return _matcherEventTutorialComplete;
		}
	}

	public static IMatcher<StateEntity> FindTargetAction
	{
		get
		{
			if (_matcherFindTargetAction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(46);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherFindTargetAction = matcher;
			}
			return _matcherFindTargetAction;
		}
	}

	public static IMatcher<StateEntity> FreeChest
	{
		get
		{
			if (_matcherFreeChest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(47);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherFreeChest = matcher;
			}
			return _matcherFreeChest;
		}
	}

	public static IMatcher<StateEntity> FreePrice
	{
		get
		{
			if (_matcherFreePrice == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(48);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherFreePrice = matcher;
			}
			return _matcherFreePrice;
		}
	}

	public static IMatcher<StateEntity> GameFirstLaunch
	{
		get
		{
			if (_matcherGameFirstLaunch == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(49);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherGameFirstLaunch = matcher;
			}
			return _matcherGameFirstLaunch;
		}
	}

	public static IMatcher<StateEntity> GatchaBalancingData
	{
		get
		{
			if (_matcherGatchaBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(50);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherGatchaBalancingData = matcher;
			}
			return _matcherGatchaBalancingData;
		}
	}

	public static IMatcher<StateEntity> GoBackAction
	{
		get
		{
			if (_matcherGoBackAction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(51);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherGoBackAction = matcher;
			}
			return _matcherGoBackAction;
		}
	}

	public static IMatcher<StateEntity> GrantPlayerExperience
	{
		get
		{
			if (_matcherGrantPlayerExperience == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(52);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherGrantPlayerExperience = matcher;
			}
			return _matcherGrantPlayerExperience;
		}
	}

	public static IMatcher<StateEntity> HardCurrencyPrice
	{
		get
		{
			if (_matcherHardCurrencyPrice == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(53);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherHardCurrencyPrice = matcher;
			}
			return _matcherHardCurrencyPrice;
		}
	}

	public static IMatcher<StateEntity> HiglightTargetAction
	{
		get
		{
			if (_matcherHiglightTargetAction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(54);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherHiglightTargetAction = matcher;
			}
			return _matcherHiglightTargetAction;
		}
	}

	public static IMatcher<StateEntity> InitConfigData
	{
		get
		{
			if (_matcherInitConfigData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(55);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherInitConfigData = matcher;
			}
			return _matcherInitConfigData;
		}
	}

	public static IMatcher<StateEntity> InitialStartSceneDisplayed
	{
		get
		{
			if (_matcherInitialStartSceneDisplayed == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(56);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherInitialStartSceneDisplayed = matcher;
			}
			return _matcherInitialStartSceneDisplayed;
		}
	}

	public static IMatcher<StateEntity> InitLocalUserState
	{
		get
		{
			if (_matcherInitLocalUserState == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(57);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherInitLocalUserState = matcher;
			}
			return _matcherInitLocalUserState;
		}
	}

	public static IMatcher<StateEntity> Integer
	{
		get
		{
			if (_matcherInteger == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(58);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherInteger = matcher;
			}
			return _matcherInteger;
		}
	}

	public static IMatcher<StateEntity> Label
	{
		get
		{
			if (_matcherLabel == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(59);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherLabel = matcher;
			}
			return _matcherLabel;
		}
	}

	public static IMatcher<StateEntity> LastLoggedScreen
	{
		get
		{
			if (_matcherLastLoggedScreen == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(60);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherLastLoggedScreen = matcher;
			}
			return _matcherLastLoggedScreen;
		}
	}

	public static IMatcher<StateEntity> LastSceneFramesCount
	{
		get
		{
			if (_matcherLastSceneFramesCount == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(61);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherLastSceneFramesCount = matcher;
			}
			return _matcherLastSceneFramesCount;
		}
	}

	public static IMatcher<StateEntity> LastSceneTransitionTime
	{
		get
		{
			if (_matcherLastSceneTransitionTime == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(62);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherLastSceneTransitionTime = matcher;
			}
			return _matcherLastSceneTransitionTime;
		}
	}

	public static IMatcher<StateEntity> LevelCompleted
	{
		get
		{
			if (_matcherLevelCompleted == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(63);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherLevelCompleted = matcher;
			}
			return _matcherLevelCompleted;
		}
	}

	public static IMatcher<StateEntity> LocalUserState
	{
		get
		{
			if (_matcherLocalUserState == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(64);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherLocalUserState = matcher;
			}
			return _matcherLocalUserState;
		}
	}

	public static IMatcher<StateEntity> LogO7SdkIapBuyRequest
	{
		get
		{
			if (_matcherLogO7SdkIapBuyRequest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(65);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherLogO7SdkIapBuyRequest = matcher;
			}
			return _matcherLogO7SdkIapBuyRequest;
		}
	}

	public static IMatcher<StateEntity> MissionBalancingData
	{
		get
		{
			if (_matcherMissionBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(66);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherMissionBalancingData = matcher;
			}
			return _matcherMissionBalancingData;
		}
	}

	public static IMatcher<StateEntity> MissionChestsBalancingData
	{
		get
		{
			if (_matcherMissionChestsBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(67);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherMissionChestsBalancingData = matcher;
			}
			return _matcherMissionChestsBalancingData;
		}
	}

	public static IMatcher<StateEntity> MoveTargetToTopAction
	{
		get
		{
			if (_matcherMoveTargetToTopAction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(68);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherMoveTargetToTopAction = matcher;
			}
			return _matcherMoveTargetToTopAction;
		}
	}

	public static IMatcher<StateEntity> MuteMusic
	{
		get
		{
			if (_matcherMuteMusic == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(69);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherMuteMusic = matcher;
			}
			return _matcherMuteMusic;
		}
	}

	public static IMatcher<StateEntity> MuteSound
	{
		get
		{
			if (_matcherMuteSound == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(70);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherMuteSound = matcher;
			}
			return _matcherMuteSound;
		}
	}

	public static IMatcher<StateEntity> MuteSoundSource
	{
		get
		{
			if (_matcherMuteSoundSource == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(71);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherMuteSoundSource = matcher;
			}
			return _matcherMuteSoundSource;
		}
	}

	public static IMatcher<StateEntity> Name
	{
		get
		{
			if (_matcherName == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(72);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherName = matcher;
			}
			return _matcherName;
		}
	}

	public static IMatcher<StateEntity> NewLevelUnlocked
	{
		get
		{
			if (_matcherNewLevelUnlocked == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(73);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherNewLevelUnlocked = matcher;
			}
			return _matcherNewLevelUnlocked;
		}
	}

	public static IMatcher<StateEntity> O7SdkAdTracking
	{
		get
		{
			if (_matcherO7SdkAdTracking == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(74);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkAdTracking = matcher;
			}
			return _matcherO7SdkAdTracking;
		}
	}

	public static IMatcher<StateEntity> O7SdkAgeGateDecider
	{
		get
		{
			if (_matcherO7SdkAgeGateDecider == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(75);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkAgeGateDecider = matcher;
			}
			return _matcherO7SdkAgeGateDecider;
		}
	}

	public static IMatcher<StateEntity> O7SdkAgeGateManager
	{
		get
		{
			if (_matcherO7SdkAgeGateManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(76);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkAgeGateManager = matcher;
			}
			return _matcherO7SdkAgeGateManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkAgeGateSelected
	{
		get
		{
			if (_matcherO7SdkAgeGateSelected == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(77);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkAgeGateSelected = matcher;
			}
			return _matcherO7SdkAgeGateSelected;
		}
	}

	public static IMatcher<StateEntity> O7SdkAgeGateShow
	{
		get
		{
			if (_matcherO7SdkAgeGateShow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(78);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkAgeGateShow = matcher;
			}
			return _matcherO7SdkAgeGateShow;
		}
	}

	public static IMatcher<StateEntity> O7SdkAutoNewsRequest
	{
		get
		{
			if (_matcherO7SdkAutoNewsRequest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(79);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkAutoNewsRequest = matcher;
			}
			return _matcherO7SdkAutoNewsRequest;
		}
	}

	public static IMatcher<StateEntity> O7SdkBalancingData
	{
		get
		{
			if (_matcherO7SdkBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(80);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkBalancingData = matcher;
			}
			return _matcherO7SdkBalancingData;
		}
	}

	public static IMatcher<StateEntity> O7SdkBannerAdHide
	{
		get
		{
			if (_matcherO7SdkBannerAdHide == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(81);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkBannerAdHide = matcher;
			}
			return _matcherO7SdkBannerAdHide;
		}
	}

	public static IMatcher<StateEntity> O7SdkBannerAdManager
	{
		get
		{
			if (_matcherO7SdkBannerAdManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(82);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkBannerAdManager = matcher;
			}
			return _matcherO7SdkBannerAdManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkBannerAdShow
	{
		get
		{
			if (_matcherO7SdkBannerAdShow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(83);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkBannerAdShow = matcher;
			}
			return _matcherO7SdkBannerAdShow;
		}
	}

	public static IMatcher<StateEntity> O7SdkCall
	{
		get
		{
			if (_matcherO7SdkCall == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(84);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkCall = matcher;
			}
			return _matcherO7SdkCall;
		}
	}

	public static IMatcher<StateEntity> O7Sdk
	{
		get
		{
			if (_matcherO7Sdk == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(85);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7Sdk = matcher;
			}
			return _matcherO7Sdk;
		}
	}

	public static IMatcher<StateEntity> O7SdkConfigItems
	{
		get
		{
			if (_matcherO7SdkConfigItems == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(170);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkConfigItems = matcher;
			}
			return _matcherO7SdkConfigItems;
		}
	}

	public static IMatcher<StateEntity> O7SdkCountryCodes
	{
		get
		{
			if (_matcherO7SdkCountryCodes == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(86);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkCountryCodes = matcher;
			}
			return _matcherO7SdkCountryCodes;
		}
	}

	public static IMatcher<StateEntity> O7SdkCrossPromo
	{
		get
		{
			if (_matcherO7SdkCrossPromo == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(87);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkCrossPromo = matcher;
			}
			return _matcherO7SdkCrossPromo;
		}
	}

	public static IMatcher<StateEntity> O7SdkCrossPromoManager
	{
		get
		{
			if (_matcherO7SdkCrossPromoManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(88);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkCrossPromoManager = matcher;
			}
			return _matcherO7SdkCrossPromoManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkData
	{
		get
		{
			if (_matcherO7SdkData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(89);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkData = matcher;
			}
			return _matcherO7SdkData;
		}
	}

	public static IMatcher<StateEntity> O7SdkDataManager
	{
		get
		{
			if (_matcherO7SdkDataManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(90);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkDataManager = matcher;
			}
			return _matcherO7SdkDataManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkEventManager
	{
		get
		{
			if (_matcherO7SdkEventManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(91);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkEventManager = matcher;
			}
			return _matcherO7SdkEventManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkExtConfig
	{
		get
		{
			if (_matcherO7SdkExtConfig == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(171);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkExtConfig = matcher;
			}
			return _matcherO7SdkExtConfig;
		}
	}

	public static IMatcher<StateEntity> O7SdkForceAgeGate
	{
		get
		{
			if (_matcherO7SdkForceAgeGate == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(92);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkForceAgeGate = matcher;
			}
			return _matcherO7SdkForceAgeGate;
		}
	}

	public static IMatcher<StateEntity> O7SdkForceCheckProcessed
	{
		get
		{
			if (_matcherO7SdkForceCheckProcessed == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(93);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkForceCheckProcessed = matcher;
			}
			return _matcherO7SdkForceCheckProcessed;
		}
	}

	public static IMatcher<StateEntity> O7SdkForceGdpr
	{
		get
		{
			if (_matcherO7SdkForceGdpr == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(94);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkForceGdpr = matcher;
			}
			return _matcherO7SdkForceGdpr;
		}
	}

	public static IMatcher<StateEntity> O7SdkGateDone
	{
		get
		{
			if (_matcherO7SdkGateDone == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(95);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGateDone = matcher;
			}
			return _matcherO7SdkGateDone;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprAdProviders
	{
		get
		{
			if (_matcherO7SdkGdprAdProviders == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(96);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprAdProviders = matcher;
			}
			return _matcherO7SdkGdprAdProviders;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprAdProvidersFetch
	{
		get
		{
			if (_matcherO7SdkGdprAdProvidersFetch == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(97);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprAdProvidersFetch = matcher;
			}
			return _matcherO7SdkGdprAdProvidersFetch;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprAdProvidersSelectAll
	{
		get
		{
			if (_matcherO7SdkGdprAdProvidersSelectAll == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(98);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprAdProvidersSelectAll = matcher;
			}
			return _matcherO7SdkGdprAdProvidersSelectAll;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprAdProvidersSubmitSelection
	{
		get
		{
			if (_matcherO7SdkGdprAdProvidersSubmitSelection == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(99);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprAdProvidersSubmitSelection = matcher;
			}
			return _matcherO7SdkGdprAdProvidersSubmitSelection;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprAdProvidersUnSelectAll
	{
		get
		{
			if (_matcherO7SdkGdprAdProvidersUnSelectAll == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(100);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprAdProvidersUnSelectAll = matcher;
			}
			return _matcherO7SdkGdprAdProvidersUnSelectAll;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprCheck
	{
		get
		{
			if (_matcherO7SdkGdprCheck == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(101);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprCheck = matcher;
			}
			return _matcherO7SdkGdprCheck;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprConsentAccepted
	{
		get
		{
			if (_matcherO7SdkGdprConsentAccepted == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(102);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprConsentAccepted = matcher;
			}
			return _matcherO7SdkGdprConsentAccepted;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprConsentDeclined
	{
		get
		{
			if (_matcherO7SdkGdprConsentDeclined == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(103);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprConsentDeclined = matcher;
			}
			return _matcherO7SdkGdprConsentDeclined;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprConsentMustShow
	{
		get
		{
			if (_matcherO7SdkGdprConsentMustShow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(104);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprConsentMustShow = matcher;
			}
			return _matcherO7SdkGdprConsentMustShow;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprConsentPrefSelected
	{
		get
		{
			if (_matcherO7SdkGdprConsentPrefSelected == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(105);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprConsentPrefSelected = matcher;
			}
			return _matcherO7SdkGdprConsentPrefSelected;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprConsentRequired
	{
		get
		{
			if (_matcherO7SdkGdprConsentRequired == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(106);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprConsentRequired = matcher;
			}
			return _matcherO7SdkGdprConsentRequired;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprConsentShow
	{
		get
		{
			if (_matcherO7SdkGdprConsentShow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(107);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprConsentShow = matcher;
			}
			return _matcherO7SdkGdprConsentShow;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprDone
	{
		get
		{
			if (_matcherO7SdkGdprDone == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(108);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprDone = matcher;
			}
			return _matcherO7SdkGdprDone;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprEnableAdProvider
	{
		get
		{
			if (_matcherO7SdkGdprEnableAdProvider == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(109);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprEnableAdProvider = matcher;
			}
			return _matcherO7SdkGdprEnableAdProvider;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprManager
	{
		get
		{
			if (_matcherO7SdkGdprManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(110);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprManager = matcher;
			}
			return _matcherO7SdkGdprManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprProvidersSendRequest
	{
		get
		{
			if (_matcherO7SdkGdprProvidersSendRequest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(111);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprProvidersSendRequest = matcher;
			}
			return _matcherO7SdkGdprProvidersSendRequest;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprRequest
	{
		get
		{
			if (_matcherO7SdkGdprRequest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(112);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprRequest = matcher;
			}
			return _matcherO7SdkGdprRequest;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprShowAdProvidersSelection
	{
		get
		{
			if (_matcherO7SdkGdprShowAdProvidersSelection == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(113);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprShowAdProvidersSelection = matcher;
			}
			return _matcherO7SdkGdprShowAdProvidersSelection;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprShowConsentTool
	{
		get
		{
			if (_matcherO7SdkGdprShowConsentTool == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(114);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprShowConsentTool = matcher;
			}
			return _matcherO7SdkGdprShowConsentTool;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprShowConsentToolType
	{
		get
		{
			if (_matcherO7SdkGdprShowConsentToolType == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(115);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprShowConsentToolType = matcher;
			}
			return _matcherO7SdkGdprShowConsentToolType;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprShowDecider
	{
		get
		{
			if (_matcherO7SdkGdprShowDecider == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(116);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprShowDecider = matcher;
			}
			return _matcherO7SdkGdprShowDecider;
		}
	}

	public static IMatcher<StateEntity> O7SdkGdprShowFromSetting
	{
		get
		{
			if (_matcherO7SdkGdprShowFromSetting == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(117);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGdprShowFromSetting = matcher;
			}
			return _matcherO7SdkGdprShowFromSetting;
		}
	}

	public static IMatcher<StateEntity> O7SdkGenderGateSelected
	{
		get
		{
			if (_matcherO7SdkGenderGateSelected == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(118);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGenderGateSelected = matcher;
			}
			return _matcherO7SdkGenderGateSelected;
		}
	}

	public static IMatcher<StateEntity> O7SdkGenderGateShow
	{
		get
		{
			if (_matcherO7SdkGenderGateShow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(119);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGenderGateShow = matcher;
			}
			return _matcherO7SdkGenderGateShow;
		}
	}

	public static IMatcher<StateEntity> O7SdkGridDownloaded
	{
		get
		{
			if (_matcherO7SdkGridDownloaded == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(120);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkGridDownloaded = matcher;
			}
			return _matcherO7SdkGridDownloaded;
		}
	}

	public static IMatcher<StateEntity> O7SdkIapBuyError
	{
		get
		{
			if (_matcherO7SdkIapBuyError == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(121);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkIapBuyError = matcher;
			}
			return _matcherO7SdkIapBuyError;
		}
	}

	public static IMatcher<StateEntity> O7SdkIapBuyFail
	{
		get
		{
			if (_matcherO7SdkIapBuyFail == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(122);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkIapBuyFail = matcher;
			}
			return _matcherO7SdkIapBuyFail;
		}
	}

	public static IMatcher<StateEntity> O7SdkIapBuyRequest
	{
		get
		{
			if (_matcherO7SdkIapBuyRequest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(123);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkIapBuyRequest = matcher;
			}
			return _matcherO7SdkIapBuyRequest;
		}
	}

	public static IMatcher<StateEntity> O7SdkIapBuySuccess
	{
		get
		{
			if (_matcherO7SdkIapBuySuccess == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(124);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkIapBuySuccess = matcher;
			}
			return _matcherO7SdkIapBuySuccess;
		}
	}

	public static IMatcher<StateEntity> O7SdkIapItemPriceData
	{
		get
		{
			if (_matcherO7SdkIapItemPriceData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(125);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkIapItemPriceData = matcher;
			}
			return _matcherO7SdkIapItemPriceData;
		}
	}

	public static IMatcher<StateEntity> O7SdkIapItemsFetchRequest
	{
		get
		{
			if (_matcherO7SdkIapItemsFetchRequest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(126);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkIapItemsFetchRequest = matcher;
			}
			return _matcherO7SdkIapItemsFetchRequest;
		}
	}

	public static IMatcher<StateEntity> O7SdkIapPacks
	{
		get
		{
			if (_matcherO7SdkIapPacks == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(127);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkIapPacks = matcher;
			}
			return _matcherO7SdkIapPacks;
		}
	}

	public static IMatcher<StateEntity> O7SdkInterstitialAd
	{
		get
		{
			if (_matcherO7SdkInterstitialAd == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(128);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkInterstitialAd = matcher;
			}
			return _matcherO7SdkInterstitialAd;
		}
	}

	public static IMatcher<StateEntity> O7SdkInterstitialAdManager
	{
		get
		{
			if (_matcherO7SdkInterstitialAdManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(129);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkInterstitialAdManager = matcher;
			}
			return _matcherO7SdkInterstitialAdManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkInterstitialShown
	{
		get
		{
			if (_matcherO7SdkInterstitialShown == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(130);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkInterstitialShown = matcher;
			}
			return _matcherO7SdkInterstitialShown;
		}
	}

	public static IMatcher<StateEntity> O7SdkManager
	{
		get
		{
			if (_matcherO7SdkManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(131);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkManager = matcher;
			}
			return _matcherO7SdkManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkPaidUser
	{
		get
		{
			if (_matcherO7SdkPaidUser == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(132);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkPaidUser = matcher;
			}
			return _matcherO7SdkPaidUser;
		}
	}

	public static IMatcher<StateEntity> O7SdkPrivacyURL
	{
		get
		{
			if (_matcherO7SdkPrivacyURL == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(133);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkPrivacyURL = matcher;
			}
			return _matcherO7SdkPrivacyURL;
		}
	}

	public static IMatcher<StateEntity> O7SdkPurchaseManager
	{
		get
		{
			if (_matcherO7SdkPurchaseManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(134);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkPurchaseManager = matcher;
			}
			return _matcherO7SdkPurchaseManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkRateMeClosed
	{
		get
		{
			if (_matcherO7SdkRateMeClosed == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(135);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRateMeClosed = matcher;
			}
			return _matcherO7SdkRateMeClosed;
		}
	}

	public static IMatcher<StateEntity> O7SdkRateMe
	{
		get
		{
			if (_matcherO7SdkRateMe == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(136);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRateMe = matcher;
			}
			return _matcherO7SdkRateMe;
		}
	}

	public static IMatcher<StateEntity> O7SdkRateMeDontLike
	{
		get
		{
			if (_matcherO7SdkRateMeDontLike == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(137);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRateMeDontLike = matcher;
			}
			return _matcherO7SdkRateMeDontLike;
		}
	}

	public static IMatcher<StateEntity> O7SdkRateMeLater
	{
		get
		{
			if (_matcherO7SdkRateMeLater == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(138);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRateMeLater = matcher;
			}
			return _matcherO7SdkRateMeLater;
		}
	}

	public static IMatcher<StateEntity> O7SdkRateMeNow
	{
		get
		{
			if (_matcherO7SdkRateMeNow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(139);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRateMeNow = matcher;
			}
			return _matcherO7SdkRateMeNow;
		}
	}

	public static IMatcher<StateEntity> O7SdkReadyToLoadGame
	{
		get
		{
			if (_matcherO7SdkReadyToLoadGame == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(140);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkReadyToLoadGame = matcher;
			}
			return _matcherO7SdkReadyToLoadGame;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardAdFetched
	{
		get
		{
			if (_matcherO7SdkRewardAdFetched == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(141);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardAdFetched = matcher;
			}
			return _matcherO7SdkRewardAdFetched;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardAdsFetchRequested
	{
		get
		{
			if (_matcherO7SdkRewardAdsFetchRequested == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(142);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardAdsFetchRequested = matcher;
			}
			return _matcherO7SdkRewardAdsFetchRequested;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardAdsShow
	{
		get
		{
			if (_matcherO7SdkRewardAdsShow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(143);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardAdsShow = matcher;
			}
			return _matcherO7SdkRewardAdsShow;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardAwarded
	{
		get
		{
			if (_matcherO7SdkRewardAwarded == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(144);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardAwarded = matcher;
			}
			return _matcherO7SdkRewardAwarded;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardedAdManager
	{
		get
		{
			if (_matcherO7SdkRewardedAdManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(145);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardedAdManager = matcher;
			}
			return _matcherO7SdkRewardedAdManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardNotAwarded
	{
		get
		{
			if (_matcherO7SdkRewardNotAwarded == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(146);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardNotAwarded = matcher;
			}
			return _matcherO7SdkRewardNotAwarded;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardOfferType
	{
		get
		{
			if (_matcherO7SdkRewardOfferType == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(147);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardOfferType = matcher;
			}
			return _matcherO7SdkRewardOfferType;
		}
	}

	public static IMatcher<StateEntity> O7SdkRewardTransaction
	{
		get
		{
			if (_matcherO7SdkRewardTransaction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(148);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkRewardTransaction = matcher;
			}
			return _matcherO7SdkRewardTransaction;
		}
	}

	public static IMatcher<StateEntity> O7SdkSelectCountryCode
	{
		get
		{
			if (_matcherO7SdkSelectCountryCode == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(149);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkSelectCountryCode = matcher;
			}
			return _matcherO7SdkSelectCountryCode;
		}
	}

	public static IMatcher<StateEntity> O7SdkSendLogs
	{
		get
		{
			if (_matcherO7SdkSendLogs == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(150);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkSendLogs = matcher;
			}
			return _matcherO7SdkSendLogs;
		}
	}

	public static IMatcher<StateEntity> O7SdkUserStateManager
	{
		get
		{
			if (_matcherO7SdkUserStateManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(151);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkUserStateManager = matcher;
			}
			return _matcherO7SdkUserStateManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkUtilityManager
	{
		get
		{
			if (_matcherO7SdkUtilityManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(152);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkUtilityManager = matcher;
			}
			return _matcherO7SdkUtilityManager;
		}
	}

	public static IMatcher<StateEntity> O7SdkValidIapPacks
	{
		get
		{
			if (_matcherO7SdkValidIapPacks == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(153);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkValidIapPacks = matcher;
			}
			return _matcherO7SdkValidIapPacks;
		}
	}

	public static IMatcher<StateEntity> O7SdkVideoGallery
	{
		get
		{
			if (_matcherO7SdkVideoGallery == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(154);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkVideoGallery = matcher;
			}
			return _matcherO7SdkVideoGallery;
		}
	}

	public static IMatcher<StateEntity> O7SdkVideoGalleryManager
	{
		get
		{
			if (_matcherO7SdkVideoGalleryManager == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(155);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherO7SdkVideoGalleryManager = matcher;
			}
			return _matcherO7SdkVideoGalleryManager;
		}
	}

	public static IMatcher<StateEntity> OpenFreeChest
	{
		get
		{
			if (_matcherOpenFreeChest == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(156);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherOpenFreeChest = matcher;
			}
			return _matcherOpenFreeChest;
		}
	}

	public static IMatcher<StateEntity> OpenPrivacyUrl
	{
		get
		{
			if (_matcherOpenPrivacyUrl == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(157);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherOpenPrivacyUrl = matcher;
			}
			return _matcherOpenPrivacyUrl;
		}
	}

	public static IMatcher<StateEntity> OpenUrl
	{
		get
		{
			if (_matcherOpenUrl == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(158);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherOpenUrl = matcher;
			}
			return _matcherOpenUrl;
		}
	}

	public static IMatcher<StateEntity> OverrideBalancingConfiguration
	{
		get
		{
			if (_matcherOverrideBalancingConfiguration == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(159);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherOverrideBalancingConfiguration = matcher;
			}
			return _matcherOverrideBalancingConfiguration;
		}
	}

	public static IMatcher<StateEntity> PassLevelAction
	{
		get
		{
			if (_matcherPassLevelAction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(160);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPassLevelAction = matcher;
			}
			return _matcherPassLevelAction;
		}
	}

	public static IMatcher<StateEntity> PendingAsyncRequests
	{
		get
		{
			if (_matcherPendingAsyncRequests == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(161);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPendingAsyncRequests = matcher;
			}
			return _matcherPendingAsyncRequests;
		}
	}

	public static IMatcher<StateEntity> PlayerExperienceGranted
	{
		get
		{
			if (_matcherPlayerExperienceGranted == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(162);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayerExperienceGranted = matcher;
			}
			return _matcherPlayerExperienceGranted;
		}
	}

	public static IMatcher<StateEntity> PlayerLevelBalancingData
	{
		get
		{
			if (_matcherPlayerLevelBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(163);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayerLevelBalancingData = matcher;
			}
			return _matcherPlayerLevelBalancingData;
		}
	}

	public static IMatcher<StateEntity> PlayerLevelExperienceAmountData
	{
		get
		{
			if (_matcherPlayerLevelExperienceAmountData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(164);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayerLevelExperienceAmountData = matcher;
			}
			return _matcherPlayerLevelExperienceAmountData;
		}
	}

	public static IMatcher<StateEntity> PlayerLevelListener
	{
		get
		{
			if (_matcherPlayerLevelListener == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(165);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayerLevelListener = matcher;
			}
			return _matcherPlayerLevelListener;
		}
	}

	public static IMatcher<StateEntity> PlayerLevelUp
	{
		get
		{
			if (_matcherPlayerLevelUp == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(166);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayerLevelUp = matcher;
			}
			return _matcherPlayerLevelUp;
		}
	}

	public static IMatcher<StateEntity> PlayerProgressionUpdated
	{
		get
		{
			if (_matcherPlayerProgressionUpdated == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(167);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayerProgressionUpdated = matcher;
			}
			return _matcherPlayerProgressionUpdated;
		}
	}

	public static IMatcher<StateEntity> PlayLoopSoundEffect
	{
		get
		{
			if (_matcherPlayLoopSoundEffect == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(168);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayLoopSoundEffect = matcher;
			}
			return _matcherPlayLoopSoundEffect;
		}
	}

	public static IMatcher<StateEntity> PlayMusic
	{
		get
		{
			if (_matcherPlayMusic == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(169);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlayMusic = matcher;
			}
			return _matcherPlayMusic;
		}
	}

	public static IMatcher<StateEntity> PlaySoundEffect
	{
		get
		{
			if (_matcherPlaySoundEffect == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(172);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPlaySoundEffect = matcher;
			}
			return _matcherPlaySoundEffect;
		}
	}

	public static IMatcher<StateEntity> PreviousScreen
	{
		get
		{
			if (_matcherPreviousScreen == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(173);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPreviousScreen = matcher;
			}
			return _matcherPreviousScreen;
		}
	}

	public static IMatcher<StateEntity> PrivacyAndLegacyBalancingData
	{
		get
		{
			if (_matcherPrivacyAndLegacyBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(174);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPrivacyAndLegacyBalancingData = matcher;
			}
			return _matcherPrivacyAndLegacyBalancingData;
		}
	}

	public static IMatcher<StateEntity> PrivacyAndLegalClickedEventToBeSent
	{
		get
		{
			if (_matcherPrivacyAndLegalClickedEventToBeSent == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(175);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherPrivacyAndLegalClickedEventToBeSent = matcher;
			}
			return _matcherPrivacyAndLegalClickedEventToBeSent;
		}
	}

	public static IMatcher<StateEntity> RemoteUserState
	{
		get
		{
			if (_matcherRemoteUserState == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(176);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherRemoteUserState = matcher;
			}
			return _matcherRemoteUserState;
		}
	}

	public static IMatcher<StateEntity> ReplaceTargetEvent
	{
		get
		{
			if (_matcherReplaceTargetEvent == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(177);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherReplaceTargetEvent = matcher;
			}
			return _matcherReplaceTargetEvent;
		}
	}

	public static IMatcher<StateEntity> ReplaceUserState
	{
		get
		{
			if (_matcherReplaceUserState == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(178);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherReplaceUserState = matcher;
			}
			return _matcherReplaceUserState;
		}
	}

	public static IMatcher<StateEntity> RestoreSettingsBalancingData
	{
		get
		{
			if (_matcherRestoreSettingsBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(179);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherRestoreSettingsBalancingData = matcher;
			}
			return _matcherRestoreSettingsBalancingData;
		}
	}

	public static IMatcher<StateEntity> SavedData
	{
		get
		{
			if (_matcherSavedData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(180);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherSavedData = matcher;
			}
			return _matcherSavedData;
		}
	}

	public static IMatcher<StateEntity> Session
	{
		get
		{
			if (_matcherSession == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(181);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherSession = matcher;
			}
			return _matcherSession;
		}
	}

	public static IMatcher<StateEntity> ShopIAPsBalancingData
	{
		get
		{
			if (_matcherShopIAPsBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(182);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherShopIAPsBalancingData = matcher;
			}
			return _matcherShopIAPsBalancingData;
		}
	}

	public static IMatcher<StateEntity> ShopSoftCurrencyPacksData
	{
		get
		{
			if (_matcherShopSoftCurrencyPacksData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(183);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherShopSoftCurrencyPacksData = matcher;
			}
			return _matcherShopSoftCurrencyPacksData;
		}
	}

	public static IMatcher<StateEntity> ShowHandAciton
	{
		get
		{
			if (_matcherShowHandAciton == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(184);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherShowHandAciton = matcher;
			}
			return _matcherShowHandAciton;
		}
	}

	public static IMatcher<StateEntity> SoftCurrnecyPrice
	{
		get
		{
			if (_matcherSoftCurrnecyPrice == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(185);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherSoftCurrnecyPrice = matcher;
			}
			return _matcherSoftCurrnecyPrice;
		}
	}

	public static IMatcher<StateEntity> StaminaSettingsBalancingData
	{
		get
		{
			if (_matcherStaminaSettingsBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(186);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherStaminaSettingsBalancingData = matcher;
			}
			return _matcherStaminaSettingsBalancingData;
		}
	}

	public static IMatcher<StateEntity> StopLoopSoundEffect
	{
		get
		{
			if (_matcherStopLoopSoundEffect == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(187);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherStopLoopSoundEffect = matcher;
			}
			return _matcherStopLoopSoundEffect;
		}
	}

	public static IMatcher<StateEntity> StopMusic
	{
		get
		{
			if (_matcherStopMusic == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(188);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherStopMusic = matcher;
			}
			return _matcherStopMusic;
		}
	}

	public static IMatcher<StateEntity> StringData
	{
		get
		{
			if (_matcherStringData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(189);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherStringData = matcher;
			}
			return _matcherStringData;
		}
	}

	public static IMatcher<StateEntity> ToLoad
	{
		get
		{
			if (_matcherToLoad == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(190);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherToLoad = matcher;
			}
			return _matcherToLoad;
		}
	}

	public static IMatcher<StateEntity> ToSave
	{
		get
		{
			if (_matcherToSave == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(191);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherToSave = matcher;
			}
			return _matcherToSave;
		}
	}

	public static IMatcher<StateEntity> TransactionBegin
	{
		get
		{
			if (_matcherTransactionBegin == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(192);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionBegin = matcher;
			}
			return _matcherTransactionBegin;
		}
	}

	public static IMatcher<StateEntity> TransactionCallback
	{
		get
		{
			if (_matcherTransactionCallback == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(193);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionCallback = matcher;
			}
			return _matcherTransactionCallback;
		}
	}

	public static IMatcher<StateEntity> TransactionComplete
	{
		get
		{
			if (_matcherTransactionComplete == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(194);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionComplete = matcher;
			}
			return _matcherTransactionComplete;
		}
	}

	public static IMatcher<StateEntity> Transaction
	{
		get
		{
			if (_matcherTransaction == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(195);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransaction = matcher;
			}
			return _matcherTransaction;
		}
	}

	public static IMatcher<StateEntity> TransactionGranted
	{
		get
		{
			if (_matcherTransactionGranted == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(196);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionGranted = matcher;
			}
			return _matcherTransactionGranted;
		}
	}

	public static IMatcher<StateEntity> TransactionImage
	{
		get
		{
			if (_matcherTransactionImage == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(197);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionImage = matcher;
			}
			return _matcherTransactionImage;
		}
	}

	public static IMatcher<StateEntity> TransactionName
	{
		get
		{
			if (_matcherTransactionName == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(198);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionName = matcher;
			}
			return _matcherTransactionName;
		}
	}

	public static IMatcher<StateEntity> TransactionNotValidated
	{
		get
		{
			if (_matcherTransactionNotValidated == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(199);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionNotValidated = matcher;
			}
			return _matcherTransactionNotValidated;
		}
	}

	public static IMatcher<StateEntity> TransactionOrigin
	{
		get
		{
			if (_matcherTransactionOrigin == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(200);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionOrigin = matcher;
			}
			return _matcherTransactionOrigin;
		}
	}

	public static IMatcher<StateEntity> TransactionRequireConfirmation
	{
		get
		{
			if (_matcherTransactionRequireConfirmation == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(201);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionRequireConfirmation = matcher;
			}
			return _matcherTransactionRequireConfirmation;
		}
	}

	public static IMatcher<StateEntity> TransactionValidated
	{
		get
		{
			if (_matcherTransactionValidated == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(202);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransactionValidated = matcher;
			}
			return _matcherTransactionValidated;
		}
	}

	public static IMatcher<StateEntity> TransitionInformation
	{
		get
		{
			if (_matcherTransitionInformation == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(203);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTransitionInformation = matcher;
			}
			return _matcherTransitionInformation;
		}
	}

	public static IMatcher<StateEntity> TutorialActiveStep
	{
		get
		{
			if (_matcherTutorialActiveStep == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(204);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialActiveStep = matcher;
			}
			return _matcherTutorialActiveStep;
		}
	}

	public static IMatcher<StateEntity> Tutorial
	{
		get
		{
			if (_matcherTutorial == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(205);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorial = matcher;
			}
			return _matcherTutorial;
		}
	}

	public static IMatcher<StateEntity> TutorialDisabled
	{
		get
		{
			if (_matcherTutorialDisabled == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(206);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialDisabled = matcher;
			}
			return _matcherTutorialDisabled;
		}
	}

	public static IMatcher<StateEntity> TutorialDisableStep
	{
		get
		{
			if (_matcherTutorialDisableStep == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(207);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialDisableStep = matcher;
			}
			return _matcherTutorialDisableStep;
		}
	}

	public static IMatcher<StateEntity> TutorialEnableStep
	{
		get
		{
			if (_matcherTutorialEnableStep == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(208);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialEnableStep = matcher;
			}
			return _matcherTutorialEnableStep;
		}
	}

	public static IMatcher<StateEntity> TutorialInitialized
	{
		get
		{
			if (_matcherTutorialInitialized == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(209);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialInitialized = matcher;
			}
			return _matcherTutorialInitialized;
		}
	}

	public static IMatcher<StateEntity> TutorialOutgameUnlockLevelsBalancingData
	{
		get
		{
			if (_matcherTutorialOutgameUnlockLevelsBalancingData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(210);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialOutgameUnlockLevelsBalancingData = matcher;
			}
			return _matcherTutorialOutgameUnlockLevelsBalancingData;
		}
	}

	public static IMatcher<StateEntity> TutorialPossibleGroups
	{
		get
		{
			if (_matcherTutorialPossibleGroups == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(211);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialPossibleGroups = matcher;
			}
			return _matcherTutorialPossibleGroups;
		}
	}

	public static IMatcher<StateEntity> TutorialStepDisableTriggered
	{
		get
		{
			if (_matcherTutorialStepDisableTriggered == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(212);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialStepDisableTriggered = matcher;
			}
			return _matcherTutorialStepDisableTriggered;
		}
	}

	public static IMatcher<StateEntity> TutorialStepEnableTriggered
	{
		get
		{
			if (_matcherTutorialStepEnableTriggered == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(213);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialStepEnableTriggered = matcher;
			}
			return _matcherTutorialStepEnableTriggered;
		}
	}

	public static IMatcher<StateEntity> TutorialStepsQueue
	{
		get
		{
			if (_matcherTutorialStepsQueue == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(214);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialStepsQueue = matcher;
			}
			return _matcherTutorialStepsQueue;
		}
	}

	public static IMatcher<StateEntity> TutorialTargetElement
	{
		get
		{
			if (_matcherTutorialTargetElement == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(215);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialTargetElement = matcher;
			}
			return _matcherTutorialTargetElement;
		}
	}

	public static IMatcher<StateEntity> TutorialTriggersList
	{
		get
		{
			if (_matcherTutorialTriggersList == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(216);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherTutorialTriggersList = matcher;
			}
			return _matcherTutorialTriggersList;
		}
	}

	public static IMatcher<StateEntity> UDIDHash
	{
		get
		{
			if (_matcherUDIDHash == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(217);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUDIDHash = matcher;
			}
			return _matcherUDIDHash;
		}
	}

	public static IMatcher<StateEntity> UpgradeCharacter
	{
		get
		{
			if (_matcherUpgradeCharacter == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(218);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUpgradeCharacter = matcher;
			}
			return _matcherUpgradeCharacter;
		}
	}

	public static IMatcher<StateEntity> UpgradeCompanion
	{
		get
		{
			if (_matcherUpgradeCompanion == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(219);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUpgradeCompanion = matcher;
			}
			return _matcherUpgradeCompanion;
		}
	}

	public static IMatcher<StateEntity> UserStateData
	{
		get
		{
			if (_matcherUserStateData == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(220);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUserStateData = matcher;
			}
			return _matcherUserStateData;
		}
	}

	public static IMatcher<StateEntity> UserStateLastUpdateTime
	{
		get
		{
			if (_matcherUserStateLastUpdateTime == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(221);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUserStateLastUpdateTime = matcher;
			}
			return _matcherUserStateLastUpdateTime;
		}
	}

	public static IMatcher<StateEntity> UserStateShow
	{
		get
		{
			if (_matcherUserStateShow == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(222);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUserStateShow = matcher;
			}
			return _matcherUserStateShow;
		}
	}

	public static IMatcher<StateEntity> UserStateSolveConflict
	{
		get
		{
			if (_matcherUserStateSolveConflict == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(223);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUserStateSolveConflict = matcher;
			}
			return _matcherUserStateSolveConflict;
		}
	}

	public static IMatcher<StateEntity> UserStateTryStartNewGame
	{
		get
		{
			if (_matcherUserStateTryStartNewGame == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(224);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherUserStateTryStartNewGame = matcher;
			}
			return _matcherUserStateTryStartNewGame;
		}
	}

	public static IMatcher<StateEntity> WorldSelectUnlockingAnimationStoped
	{
		get
		{
			if (_matcherWorldSelectUnlockingAnimationStoped == null)
			{
				Matcher<StateEntity> matcher = (Matcher<StateEntity>)Matcher<StateEntity>.AllOf(225);
				matcher.componentNames = StateComponentsLookup.componentNames;
				_matcherWorldSelectUnlockingAnimationStoped = matcher;
			}
			return _matcherWorldSelectUnlockingAnimationStoped;
		}
	}

	public static IAllOfMatcher<StateEntity> AllOf(params int[] indices)
	{
		return Matcher<StateEntity>.AllOf(indices);
	}

	public static IAllOfMatcher<StateEntity> AllOf(params IMatcher<StateEntity>[] matchers)
	{
		return Matcher<StateEntity>.AllOf(matchers);
	}

	public static IAnyOfMatcher<StateEntity> AnyOf(params int[] indices)
	{
		return Matcher<StateEntity>.AnyOf(indices);
	}

	public static IAnyOfMatcher<StateEntity> AnyOf(params IMatcher<StateEntity>[] matchers)
	{
		return Matcher<StateEntity>.AnyOf(matchers);
	}
}
