using System;
using System.Collections.Generic;
using Entitas;
using O7SDK;
using O7SDK.O7ADSDK;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class StateEntity : Entity, IDestroyed, IO7SdkCall
{
	private static readonly BalancingDataComponent balancingDataComponent = new BalancingDataComponent();

	private static readonly BoughtPlayerRevive boughtPlayerReviveComponent = new BoughtPlayerRevive();

	private static readonly DestroyedComponent destroyedComponent = new DestroyedComponent();

	private static readonly EulaClickedEventToBeSentComponent eulaClickedEventToBeSentComponent = new EulaClickedEventToBeSentComponent();

	private static readonly FreePriceComponent freePriceComponent = new FreePriceComponent();

	private static readonly GameFirstLaunch gameFirstLaunchComponent = new GameFirstLaunch();

	private static readonly InitLocalUserStateComponent initLocalUserStateComponent = new InitLocalUserStateComponent();

	private static readonly MoveTargetToTopActionComponent moveTargetToTopActionComponent = new MoveTargetToTopActionComponent();

	private static readonly O7SdkAdTrackingComponent o7SdkAdTrackingComponent = new O7SdkAdTrackingComponent();

	private static readonly O7SdkAgeGateDeciderComponent o7SdkAgeGateDeciderComponent = new O7SdkAgeGateDeciderComponent();

	private static readonly O7SdkAgeGateShowComponent o7SdkAgeGateShowComponent = new O7SdkAgeGateShowComponent();

	private static readonly O7SdkAutoNewsRequestComponent o7SdkAutoNewsRequestComponent = new O7SdkAutoNewsRequestComponent();

	private static readonly O7SdkBannerAdHideComponent o7SdkBannerAdHideComponent = new O7SdkBannerAdHideComponent();

	private static readonly O7SdkBannerAdShowComponent o7SdkBannerAdShowComponent = new O7SdkBannerAdShowComponent();

	private static readonly O7SdkCallComponent o7SdkCallComponent = new O7SdkCallComponent();

	private static readonly O7SdkComponent o7SdkComponent = new O7SdkComponent();

	private static readonly O7SdkForceAgeGateComponent o7SdkForceAgeGateComponent = new O7SdkForceAgeGateComponent();

	private static readonly O7SdkForceCheckProcessedComponent o7SdkForceCheckProcessedComponent = new O7SdkForceCheckProcessedComponent();

	private static readonly O7SdkForceGdprComponent o7SdkForceGdprComponent = new O7SdkForceGdprComponent();

	private static readonly O7SdkGdprAdProvidersSelectAllComponent o7SdkGdprAdProvidersSelectAllComponent = new O7SdkGdprAdProvidersSelectAllComponent();

	private static readonly O7SdkGdprAdProvidersSubmitSelectionComponent o7SdkGdprAdProvidersSubmitSelectionComponent = new O7SdkGdprAdProvidersSubmitSelectionComponent();

	private static readonly O7SdkGdprAdProvidersUnSelectAllComponent o7SdkGdprAdProvidersUnSelectAllComponent = new O7SdkGdprAdProvidersUnSelectAllComponent();

	private static readonly O7SdkGdprCheckComponent o7SdkGdprCheckComponent = new O7SdkGdprCheckComponent();

	private static readonly O7SdkGdprConsentAcceptedComponent o7SdkGdprConsentAcceptedComponent = new O7SdkGdprConsentAcceptedComponent();

	private static readonly O7SdkGdprConsentDeclinedComponent o7SdkGdprConsentDeclinedComponent = new O7SdkGdprConsentDeclinedComponent();

	private static readonly O7SdkGdprConsentMustShowComponent o7SdkGdprConsentMustShowComponent = new O7SdkGdprConsentMustShowComponent();

	private static readonly O7SdkGdprConsentPrefSelectedComponent o7SdkGdprConsentPrefSelectedComponent = new O7SdkGdprConsentPrefSelectedComponent();

	private static readonly O7SdkGdprConsentRequiredComponent o7SdkGdprConsentRequiredComponent = new O7SdkGdprConsentRequiredComponent();

	private static readonly O7SdkGdprConsentShowComponent o7SdkGdprConsentShowComponent = new O7SdkGdprConsentShowComponent();

	private static readonly O7SdkGdprDoneComponent o7SdkGdprDoneComponent = new O7SdkGdprDoneComponent();

	private static readonly O7SdkGdprProvidersSendRequestComponent o7SdkGdprProvidersSendRequestComponent = new O7SdkGdprProvidersSendRequestComponent();

	private static readonly O7SdkGdprRequestComponent o7SdkGdprRequestComponent = new O7SdkGdprRequestComponent();

	private static readonly O7SdkGdprShowAdProvidersSelectionComponent o7SdkGdprShowAdProvidersSelectionComponent = new O7SdkGdprShowAdProvidersSelectionComponent();

	private static readonly O7SdkGdprShowConsentToolComponent o7SdkGdprShowConsentToolComponent = new O7SdkGdprShowConsentToolComponent();

	private static readonly O7SdkGdprShowDeciderComponent o7SdkGdprShowDeciderComponent = new O7SdkGdprShowDeciderComponent();

	private static readonly O7SdkGdprShowFromSettingComponent o7SdkGdprShowFromSettingComponent = new O7SdkGdprShowFromSettingComponent();

	private static readonly O7SdkGenderGateShowComponent o7SdkGenderGateShowComponent = new O7SdkGenderGateShowComponent();

	private static readonly O7SdkGridDownloadedComponent o7SdkGridDownloadedComponent = new O7SdkGridDownloadedComponent();

	private static readonly O7SdkIapItemsFetchRequestComponent o7SdkIapItemsFetchRequestComponent = new O7SdkIapItemsFetchRequestComponent();

	private static readonly O7SdkInterstitialShownComponent o7SdkInterstitialShownComponent = new O7SdkInterstitialShownComponent();

	private static readonly O7SdkPaidUserComponent o7SdkPaidUserComponent = new O7SdkPaidUserComponent();

	private static readonly O7SdkRateMeClosedComponent o7SdkRateMeClosedComponent = new O7SdkRateMeClosedComponent();

	private static readonly O7SdkRateMeComponent o7SdkRateMeComponent = new O7SdkRateMeComponent();

	private static readonly O7SdkRateMeDontLikeComponent o7SdkRateMeDontLikeComponent = new O7SdkRateMeDontLikeComponent();

	private static readonly O7SdkRateMeLaterComponent o7SdkRateMeLaterComponent = new O7SdkRateMeLaterComponent();

	private static readonly O7SdkRateMeNowComponent o7SdkRateMeNowComponent = new O7SdkRateMeNowComponent();

	private static readonly O7SdkReadyToLoadGame o7SdkReadyToLoadGameComponent = new O7SdkReadyToLoadGame();

	private static readonly O7SdkRewardAdFetchedComponent o7SdkRewardAdFetchedComponent = new O7SdkRewardAdFetchedComponent();

	private static readonly O7SdkRewardAdsFetchRequestedComponent o7SdkRewardAdsFetchRequestedComponent = new O7SdkRewardAdsFetchRequestedComponent();

	private static readonly O7SdkRewardAdsShowComponent o7SdkRewardAdsShowComponent = new O7SdkRewardAdsShowComponent();

	private static readonly O7SdkRewardNotAwardedComponent o7SdkRewardNotAwardedComponent = new O7SdkRewardNotAwardedComponent();

	private static readonly O7SdkRewardTransactionComponent o7SdkRewardTransactionComponent = new O7SdkRewardTransactionComponent();

	private static readonly O7SdkVideoGalleryComponent o7SdkVideoGalleryComponent = new O7SdkVideoGalleryComponent();

	private static readonly OpenFreeChestComponent openFreeChestComponent = new OpenFreeChestComponent();

	private static readonly OpenPrivacyUrlComponent openPrivacyUrlComponent = new OpenPrivacyUrlComponent();

	private static readonly OverrideBalancingConfigurationComponent overrideBalancingConfigurationComponent = new OverrideBalancingConfigurationComponent();

	private static readonly PrivacyAndLegalClickedEventToBeSentComponent privacyAndLegalClickedEventToBeSentComponent = new PrivacyAndLegalClickedEventToBeSentComponent();

	private static readonly SavedDataComponent savedDataComponent = new SavedDataComponent();

	private static readonly SessionComponent sessionComponent = new SessionComponent();

	private static readonly ToLoadComponent toLoadComponent = new ToLoadComponent();

	private static readonly ToSaveComponent toSaveComponent = new ToSaveComponent();

	private static readonly TransactionBeginComponent transactionBeginComponent = new TransactionBeginComponent();

	private static readonly TransactionCompleteComponent transactionCompleteComponent = new TransactionCompleteComponent();

	private static readonly TransactionComponent transactionComponent = new TransactionComponent();

	private static readonly TransactionGrantedComponent transactionGrantedComponent = new TransactionGrantedComponent();

	private static readonly TransactionNotValidatedComponent transactionNotValidatedComponent = new TransactionNotValidatedComponent();

	private static readonly TransactionRequireConfirmationComponent transactionRequireConfirmationComponent = new TransactionRequireConfirmationComponent();

	private static readonly TransactionValidatedComponent transactionValidatedComponent = new TransactionValidatedComponent();

	private static readonly TutorialComponent tutorialComponent = new TutorialComponent();

	private static readonly TutorialDisabledComponent tutorialDisabledComponent = new TutorialDisabledComponent();

	private static readonly TutorialInitializedComponent tutorialInitializedComponent = new TutorialInitializedComponent();

	private static readonly UserStateDataComponent userStateDataComponent = new UserStateDataComponent();

	private static readonly UserStateShowComponent userStateShowComponent = new UserStateShowComponent();

	private static readonly UserStateTryStartNewGame userStateTryStartNewGameComponent = new UserStateTryStartNewGame();

	private static readonly WorldSelectUnlockingAnimationStopedComponent worldSelectUnlockingAnimationStopedComponent = new WorldSelectUnlockingAnimationStopedComponent();

	public AbilitiesBalancingDataComponent abilitiesBalancingData
	{
		get
		{
			return (AbilitiesBalancingDataComponent)GetComponent(0);
		}
	}

	public bool hasAbilitiesBalancingData
	{
		get
		{
			return HasComponent(0);
		}
	}

	public ActionDelayComponent actionDelay
	{
		get
		{
			return (ActionDelayComponent)GetComponent(1);
		}
	}

	public bool hasActionDelay
	{
		get
		{
			return HasComponent(1);
		}
	}

	public ActiveScreenComponent activeScreen
	{
		get
		{
			return (ActiveScreenComponent)GetComponent(2);
		}
	}

	public bool hasActiveScreen
	{
		get
		{
			return HasComponent(2);
		}
	}

	public bool isBalancingData
	{
		get
		{
			return HasComponent(3);
		}
		set
		{
			if (value != isBalancingData)
			{
				if (value)
				{
					AddComponent(3, balancingDataComponent);
				}
				else
				{
					RemoveComponent(3);
				}
			}
		}
	}

	public BalancingDataOverrideConfigComponent balancingDataOverrideConfig
	{
		get
		{
			return (BalancingDataOverrideConfigComponent)GetComponent(4);
		}
	}

	public bool hasBalancingDataOverrideConfig
	{
		get
		{
			return HasComponent(4);
		}
	}

	public BooleanComponent boolean
	{
		get
		{
			return (BooleanComponent)GetComponent(5);
		}
	}

	public bool hasBoolean
	{
		get
		{
			return HasComponent(5);
		}
	}

	public BoughtCharacterUpgradeComponent boughtCharacterUpgrade
	{
		get
		{
			return (BoughtCharacterUpgradeComponent)GetComponent(6);
		}
	}

	public bool hasBoughtCharacterUpgrade
	{
		get
		{
			return HasComponent(6);
		}
	}

	public BoughtChestComponent boughtChest
	{
		get
		{
			return (BoughtChestComponent)GetComponent(7);
		}
	}

	public bool hasBoughtChest
	{
		get
		{
			return HasComponent(7);
		}
	}

	public BoughtChestPreparedComponent boughtChestPrepared
	{
		get
		{
			return (BoughtChestPreparedComponent)GetComponent(8);
		}
	}

	public bool hasBoughtChestPrepared
	{
		get
		{
			return HasComponent(8);
		}
	}

	public BoughtCompanionUpgradeComponent boughtCompanionUpgrade
	{
		get
		{
			return (BoughtCompanionUpgradeComponent)GetComponent(9);
		}
	}

	public bool hasBoughtCompanionUpgrade
	{
		get
		{
			return HasComponent(9);
		}
	}

	public BoughtEnergyRefill boughtEnergyRefill
	{
		get
		{
			return (BoughtEnergyRefill)GetComponent(10);
		}
	}

	public bool hasBoughtEnergyRefill
	{
		get
		{
			return HasComponent(10);
		}
	}

	public BoughtHardCurrencyComponent boughtHardCurrency
	{
		get
		{
			return (BoughtHardCurrencyComponent)GetComponent(11);
		}
	}

	public bool hasBoughtHardCurrency
	{
		get
		{
			return HasComponent(11);
		}
	}

	public BoughtPlayerResurect boughtPlayerResurect
	{
		get
		{
			return (BoughtPlayerResurect)GetComponent(12);
		}
	}

	public bool hasBoughtPlayerResurect
	{
		get
		{
			return HasComponent(12);
		}
	}

	public bool isBoughtPlayerRevive
	{
		get
		{
			return HasComponent(13);
		}
		set
		{
			if (value != isBoughtPlayerRevive)
			{
				if (value)
				{
					AddComponent(13, boughtPlayerReviveComponent);
				}
				else
				{
					RemoveComponent(13);
				}
			}
		}
	}

	public BoughtSoftCurrencyComponent boughtSoftCurrency
	{
		get
		{
			return (BoughtSoftCurrencyComponent)GetComponent(14);
		}
	}

	public bool hasBoughtSoftCurrency
	{
		get
		{
			return HasComponent(14);
		}
	}

	public ChangeTimeScaleComponent changeTimeScale
	{
		get
		{
			return (ChangeTimeScaleComponent)GetComponent(15);
		}
	}

	public bool hasChangeTimeScale
	{
		get
		{
			return HasComponent(15);
		}
	}

	public ChapterBalancingDataComponent chapterBalancingData
	{
		get
		{
			return (ChapterBalancingDataComponent)GetComponent(16);
		}
	}

	public bool hasChapterBalancingData
	{
		get
		{
			return HasComponent(16);
		}
	}

	public CharacterPartsPriceComponent characterPartsPrice
	{
		get
		{
			return (CharacterPartsPriceComponent)GetComponent(17);
		}
	}

	public bool hasCharacterPartsPrice
	{
		get
		{
			return HasComponent(17);
		}
	}

	public CharacterStaticBalancingDataComponent characterStaticBalancingData
	{
		get
		{
			return (CharacterStaticBalancingDataComponent)GetComponent(18);
		}
	}

	public bool hasCharacterStaticBalancingData
	{
		get
		{
			return HasComponent(18);
		}
	}

	public CharacterUpgradesBalancingDataComponent characterUpgradesBalancingData
	{
		get
		{
			return (CharacterUpgradesBalancingDataComponent)GetComponent(19);
		}
	}

	public bool hasCharacterUpgradesBalancingData
	{
		get
		{
			return HasComponent(19);
		}
	}

	public CloseScreenActionComponent closeScreenAction
	{
		get
		{
			return (CloseScreenActionComponent)GetComponent(20);
		}
	}

	public bool hasCloseScreenAction
	{
		get
		{
			return HasComponent(20);
		}
	}

	public CompanionPartsPriceComponent companionPartsPrice
	{
		get
		{
			return (CompanionPartsPriceComponent)GetComponent(21);
		}
	}

	public bool hasCompanionPartsPrice
	{
		get
		{
			return HasComponent(21);
		}
	}

	public CompanionsStaticBalancingDataComponent companionsStaticBalancingData
	{
		get
		{
			return (CompanionsStaticBalancingDataComponent)GetComponent(22);
		}
	}

	public bool hasCompanionsStaticBalancingData
	{
		get
		{
			return HasComponent(22);
		}
	}

	public CompanionsUpgradesBalancingDataComponent companionsUpgradesBalancingData
	{
		get
		{
			return (CompanionsUpgradesBalancingDataComponent)GetComponent(23);
		}
	}

	public bool hasCompanionsUpgradesBalancingData
	{
		get
		{
			return HasComponent(23);
		}
	}

	public CurrentRealTimeComponent currentRealTime
	{
		get
		{
			return (CurrentRealTimeComponent)GetComponent(24);
		}
	}

	public bool hasCurrentRealTime
	{
		get
		{
			return HasComponent(24);
		}
	}

	public CurrentTimeListenerComponent currentTimeListener
	{
		get
		{
			return (CurrentTimeListenerComponent)GetComponent(25);
		}
	}

	public bool hasCurrentTimeListener
	{
		get
		{
			return HasComponent(25);
		}
	}

	public DailyMissionChestStateUpdateComponent dailyMissionChestStateUpdate
	{
		get
		{
			return (DailyMissionChestStateUpdateComponent)GetComponent(26);
		}
	}

	public bool hasDailyMissionChestStateUpdate
	{
		get
		{
			return HasComponent(26);
		}
	}

	public DailyMissionClaimUpdateComponent dailyMissionClaimUpdate
	{
		get
		{
			return (DailyMissionClaimUpdateComponent)GetComponent(27);
		}
	}

	public bool hasDailyMissionClaimUpdate
	{
		get
		{
			return HasComponent(27);
		}
	}

	public DailyMissionProgressUpdateComponent dailyMissionProgressUpdate
	{
		get
		{
			return (DailyMissionProgressUpdateComponent)GetComponent(28);
		}
	}

	public bool hasDailyMissionProgressUpdate
	{
		get
		{
			return HasComponent(28);
		}
	}

	public DailyMissionsBalancingDataComponent dailyMissionsBalancingData
	{
		get
		{
			return (DailyMissionsBalancingDataComponent)GetComponent(29);
		}
	}

	public bool hasDailyMissionsBalancingData
	{
		get
		{
			return HasComponent(29);
		}
	}

	public DailyMissionsCollectionComponent dailyMissionsCollection
	{
		get
		{
			return (DailyMissionsCollectionComponent)GetComponent(30);
		}
	}

	public bool hasDailyMissionsCollection
	{
		get
		{
			return HasComponent(30);
		}
	}

	public DateTimeComponent dateTime
	{
		get
		{
			return (DateTimeComponent)GetComponent(31);
		}
	}

	public bool hasDateTime
	{
		get
		{
			return HasComponent(31);
		}
	}

	public bool isDestroyed
	{
		get
		{
			return HasComponent(32);
		}
		set
		{
			if (value != isDestroyed)
			{
				if (value)
				{
					AddComponent(32, destroyedComponent);
				}
				else
				{
					RemoveComponent(32);
				}
			}
		}
	}

	public DisplayLockLayerActionComponent displayLockLayerAction
	{
		get
		{
			return (DisplayLockLayerActionComponent)GetComponent(33);
		}
	}

	public bool hasDisplayLockLayerAction
	{
		get
		{
			return HasComponent(33);
		}
	}

	public bool isEulaClickedEventToBeSent
	{
		get
		{
			return HasComponent(34);
		}
		set
		{
			if (value != isEulaClickedEventToBeSent)
			{
				if (value)
				{
					AddComponent(34, eulaClickedEventToBeSentComponent);
				}
				else
				{
					RemoveComponent(34);
				}
			}
		}
	}

	public EventAchievementCompleteComponent eventAchievementComplete
	{
		get
		{
			return (EventAchievementCompleteComponent)GetComponent(35);
		}
	}

	public bool hasEventAchievementComplete
	{
		get
		{
			return HasComponent(35);
		}
	}

	public EventCustomComponent eventCustom
	{
		get
		{
			return (EventCustomComponent)GetComponent(36);
		}
	}

	public bool hasEventCustom
	{
		get
		{
			return HasComponent(36);
		}
	}

	public EventCustomSendComponent eventCustomSend
	{
		get
		{
			return (EventCustomSendComponent)GetComponent(37);
		}
	}

	public bool hasEventCustomSend
	{
		get
		{
			return HasComponent(37);
		}
	}

	public EventEarnVirtualCurrencyComponent eventEarnVirtualCurrency
	{
		get
		{
			return (EventEarnVirtualCurrencyComponent)GetComponent(38);
		}
	}

	public bool hasEventEarnVirtualCurrency
	{
		get
		{
			return HasComponent(38);
		}
	}

	public EventInAppPurchaseComponent eventInAppPurchase
	{
		get
		{
			return (EventInAppPurchaseComponent)GetComponent(39);
		}
	}

	public bool hasEventInAppPurchase
	{
		get
		{
			return HasComponent(39);
		}
	}

	public EventLevelCompleteComponent eventLevelComplete
	{
		get
		{
			return (EventLevelCompleteComponent)GetComponent(40);
		}
	}

	public bool hasEventLevelComplete
	{
		get
		{
			return HasComponent(40);
		}
	}

	public EventLevelUpComponent eventLevelUp
	{
		get
		{
			return (EventLevelUpComponent)GetComponent(41);
		}
	}

	public bool hasEventLevelUp
	{
		get
		{
			return HasComponent(41);
		}
	}

	public EventSelectContentComponent eventSelectContent
	{
		get
		{
			return (EventSelectContentComponent)GetComponent(42);
		}
	}

	public bool hasEventSelectContent
	{
		get
		{
			return HasComponent(42);
		}
	}

	public EventSpendVirtualCurrencyComponent eventSpendVirtualCurrency
	{
		get
		{
			return (EventSpendVirtualCurrencyComponent)GetComponent(43);
		}
	}

	public bool hasEventSpendVirtualCurrency
	{
		get
		{
			return HasComponent(43);
		}
	}

	public EventTutorialBeginComponent eventTutorialBegin
	{
		get
		{
			return (EventTutorialBeginComponent)GetComponent(44);
		}
	}

	public bool hasEventTutorialBegin
	{
		get
		{
			return HasComponent(44);
		}
	}

	public EventTutorialCompleteComponent eventTutorialComplete
	{
		get
		{
			return (EventTutorialCompleteComponent)GetComponent(45);
		}
	}

	public bool hasEventTutorialComplete
	{
		get
		{
			return HasComponent(45);
		}
	}

	public FindTargetActionComponent findTargetAction
	{
		get
		{
			return (FindTargetActionComponent)GetComponent(46);
		}
	}

	public bool hasFindTargetAction
	{
		get
		{
			return HasComponent(46);
		}
	}

	public FreeChest freeChest
	{
		get
		{
			return (FreeChest)GetComponent(47);
		}
	}

	public bool hasFreeChest
	{
		get
		{
			return HasComponent(47);
		}
	}

	public bool isFreePrice
	{
		get
		{
			return HasComponent(48);
		}
		set
		{
			if (value != isFreePrice)
			{
				if (value)
				{
					AddComponent(48, freePriceComponent);
				}
				else
				{
					RemoveComponent(48);
				}
			}
		}
	}

	public bool isGameFirstLaunch
	{
		get
		{
			return HasComponent(49);
		}
		set
		{
			if (value != isGameFirstLaunch)
			{
				if (value)
				{
					AddComponent(49, gameFirstLaunchComponent);
				}
				else
				{
					RemoveComponent(49);
				}
			}
		}
	}

	public GatchaBalancingDataComponent gatchaBalancingData
	{
		get
		{
			return (GatchaBalancingDataComponent)GetComponent(50);
		}
	}

	public bool hasGatchaBalancingData
	{
		get
		{
			return HasComponent(50);
		}
	}

	public GoBackActionComponent goBackAction
	{
		get
		{
			return (GoBackActionComponent)GetComponent(51);
		}
	}

	public bool hasGoBackAction
	{
		get
		{
			return HasComponent(51);
		}
	}

	public GrantPlayerExperienceComponent grantPlayerExperience
	{
		get
		{
			return (GrantPlayerExperienceComponent)GetComponent(52);
		}
	}

	public bool hasGrantPlayerExperience
	{
		get
		{
			return HasComponent(52);
		}
	}

	public HardCurrencyPriceComponent hardCurrencyPrice
	{
		get
		{
			return (HardCurrencyPriceComponent)GetComponent(53);
		}
	}

	public bool hasHardCurrencyPrice
	{
		get
		{
			return HasComponent(53);
		}
	}

	public HiglightTargetActionComponent higlightTargetAction
	{
		get
		{
			return (HiglightTargetActionComponent)GetComponent(54);
		}
	}

	public bool hasHiglightTargetAction
	{
		get
		{
			return HasComponent(54);
		}
	}

	public InitConfigDataComponent initConfigData
	{
		get
		{
			return (InitConfigDataComponent)GetComponent(55);
		}
	}

	public bool hasInitConfigData
	{
		get
		{
			return HasComponent(55);
		}
	}

	public InitialStartSceneDisplayed initialStartSceneDisplayed
	{
		get
		{
			return (InitialStartSceneDisplayed)GetComponent(56);
		}
	}

	public bool hasInitialStartSceneDisplayed
	{
		get
		{
			return HasComponent(56);
		}
	}

	public bool isInitLocalUserState
	{
		get
		{
			return HasComponent(57);
		}
		set
		{
			if (value != isInitLocalUserState)
			{
				if (value)
				{
					AddComponent(57, initLocalUserStateComponent);
				}
				else
				{
					RemoveComponent(57);
				}
			}
		}
	}

	public IntegerComponent integer
	{
		get
		{
			return (IntegerComponent)GetComponent(58);
		}
	}

	public bool hasInteger
	{
		get
		{
			return HasComponent(58);
		}
	}

	public LabelComponent label
	{
		get
		{
			return (LabelComponent)GetComponent(59);
		}
	}

	public bool hasLabel
	{
		get
		{
			return HasComponent(59);
		}
	}

	public LastLoggedScreenComponent lastLoggedScreen
	{
		get
		{
			return (LastLoggedScreenComponent)GetComponent(60);
		}
	}

	public bool hasLastLoggedScreen
	{
		get
		{
			return HasComponent(60);
		}
	}

	public LastSceneFramesCountComponent lastSceneFramesCount
	{
		get
		{
			return (LastSceneFramesCountComponent)GetComponent(61);
		}
	}

	public bool hasLastSceneFramesCount
	{
		get
		{
			return HasComponent(61);
		}
	}

	public LastSceneTransitionTimeComponent lastSceneTransitionTime
	{
		get
		{
			return (LastSceneTransitionTimeComponent)GetComponent(62);
		}
	}

	public bool hasLastSceneTransitionTime
	{
		get
		{
			return HasComponent(62);
		}
	}

	public LevelCompletedComponent levelCompleted
	{
		get
		{
			return (LevelCompletedComponent)GetComponent(63);
		}
	}

	public bool hasLevelCompleted
	{
		get
		{
			return HasComponent(63);
		}
	}

	public LocalUserStateComponent localUserState
	{
		get
		{
			return (LocalUserStateComponent)GetComponent(64);
		}
	}

	public bool hasLocalUserState
	{
		get
		{
			return HasComponent(64);
		}
	}

	public LogO7SdkIapBuyRequestComponent logO7SdkIapBuyRequest
	{
		get
		{
			return (LogO7SdkIapBuyRequestComponent)GetComponent(65);
		}
	}

	public bool hasLogO7SdkIapBuyRequest
	{
		get
		{
			return HasComponent(65);
		}
	}

	public MissionBalancingDataComponent missionBalancingData
	{
		get
		{
			return (MissionBalancingDataComponent)GetComponent(66);
		}
	}

	public bool hasMissionBalancingData
	{
		get
		{
			return HasComponent(66);
		}
	}

	public MissionChestsBalancingDataComponent missionChestsBalancingData
	{
		get
		{
			return (MissionChestsBalancingDataComponent)GetComponent(67);
		}
	}

	public bool hasMissionChestsBalancingData
	{
		get
		{
			return HasComponent(67);
		}
	}

	public bool isMoveTargetToTopAction
	{
		get
		{
			return HasComponent(68);
		}
		set
		{
			if (value != isMoveTargetToTopAction)
			{
				if (value)
				{
					AddComponent(68, moveTargetToTopActionComponent);
				}
				else
				{
					RemoveComponent(68);
				}
			}
		}
	}

	public MuteMusicComponent muteMusic
	{
		get
		{
			return (MuteMusicComponent)GetComponent(69);
		}
	}

	public bool hasMuteMusic
	{
		get
		{
			return HasComponent(69);
		}
	}

	public MuteSoundComponent muteSound
	{
		get
		{
			return (MuteSoundComponent)GetComponent(70);
		}
	}

	public bool hasMuteSound
	{
		get
		{
			return HasComponent(70);
		}
	}

	public MuteSoundSourceComponent muteSoundSource
	{
		get
		{
			return (MuteSoundSourceComponent)GetComponent(71);
		}
	}

	public bool hasMuteSoundSource
	{
		get
		{
			return HasComponent(71);
		}
	}

	public NameComponent name
	{
		get
		{
			return (NameComponent)GetComponent(72);
		}
	}

	public bool hasName
	{
		get
		{
			return HasComponent(72);
		}
	}

	public NewLevelUnlockedComponent newLevelUnlocked
	{
		get
		{
			return (NewLevelUnlockedComponent)GetComponent(73);
		}
	}

	public bool hasNewLevelUnlocked
	{
		get
		{
			return HasComponent(73);
		}
	}

	public bool isO7SdkAdTracking
	{
		get
		{
			return HasComponent(74);
		}
		set
		{
			if (value != isO7SdkAdTracking)
			{
				if (value)
				{
					AddComponent(74, o7SdkAdTrackingComponent);
				}
				else
				{
					RemoveComponent(74);
				}
			}
		}
	}

	public bool isO7SdkAgeGateDecider
	{
		get
		{
			return HasComponent(75);
		}
		set
		{
			if (value != isO7SdkAgeGateDecider)
			{
				if (value)
				{
					AddComponent(75, o7SdkAgeGateDeciderComponent);
				}
				else
				{
					RemoveComponent(75);
				}
			}
		}
	}

	public O7SdkAgeGateManagerComponent o7SdkAgeGateManager
	{
		get
		{
			return (O7SdkAgeGateManagerComponent)GetComponent(76);
		}
	}

	public bool hasO7SdkAgeGateManager
	{
		get
		{
			return HasComponent(76);
		}
	}

	public O7SdkAgeGateSelectedComponent o7SdkAgeGateSelected
	{
		get
		{
			return (O7SdkAgeGateSelectedComponent)GetComponent(77);
		}
	}

	public bool hasO7SdkAgeGateSelected
	{
		get
		{
			return HasComponent(77);
		}
	}

	public bool isO7SdkAgeGateShow
	{
		get
		{
			return HasComponent(78);
		}
		set
		{
			if (value != isO7SdkAgeGateShow)
			{
				if (value)
				{
					AddComponent(78, o7SdkAgeGateShowComponent);
				}
				else
				{
					RemoveComponent(78);
				}
			}
		}
	}

	public bool isO7SdkAutoNewsRequest
	{
		get
		{
			return HasComponent(79);
		}
		set
		{
			if (value != isO7SdkAutoNewsRequest)
			{
				if (value)
				{
					AddComponent(79, o7SdkAutoNewsRequestComponent);
				}
				else
				{
					RemoveComponent(79);
				}
			}
		}
	}

	public O7SdkBalancingDataComponent o7SdkBalancingData
	{
		get
		{
			return (O7SdkBalancingDataComponent)GetComponent(80);
		}
	}

	public bool hasO7SdkBalancingData
	{
		get
		{
			return HasComponent(80);
		}
	}

	public bool isO7SdkBannerAdHide
	{
		get
		{
			return HasComponent(81);
		}
		set
		{
			if (value != isO7SdkBannerAdHide)
			{
				if (value)
				{
					AddComponent(81, o7SdkBannerAdHideComponent);
				}
				else
				{
					RemoveComponent(81);
				}
			}
		}
	}

	public O7SdkBannerAdManagerComponent o7SdkBannerAdManager
	{
		get
		{
			return (O7SdkBannerAdManagerComponent)GetComponent(82);
		}
	}

	public bool hasO7SdkBannerAdManager
	{
		get
		{
			return HasComponent(82);
		}
	}

	public bool isO7SdkBannerAdShow
	{
		get
		{
			return HasComponent(83);
		}
		set
		{
			if (value != isO7SdkBannerAdShow)
			{
				if (value)
				{
					AddComponent(83, o7SdkBannerAdShowComponent);
				}
				else
				{
					RemoveComponent(83);
				}
			}
		}
	}

	public bool isO7SdkCall
	{
		get
		{
			return HasComponent(84);
		}
		set
		{
			if (value != isO7SdkCall)
			{
				if (value)
				{
					AddComponent(84, o7SdkCallComponent);
				}
				else
				{
					RemoveComponent(84);
				}
			}
		}
	}

	public bool isO7Sdk
	{
		get
		{
			return HasComponent(85);
		}
		set
		{
			if (value != isO7Sdk)
			{
				if (value)
				{
					AddComponent(85, o7SdkComponent);
				}
				else
				{
					RemoveComponent(85);
				}
			}
		}
	}

	public O7SdkConfigItemsComponent o7SdkConfigItems
	{
		get
		{
			return (O7SdkConfigItemsComponent)GetComponent(170);
		}
	}

	public bool hasO7SdkConfigItems
	{
		get
		{
			return HasComponent(170);
		}
	}

	public O7SdkCountryCodes o7SdkCountryCodes
	{
		get
		{
			return (O7SdkCountryCodes)GetComponent(86);
		}
	}

	public bool hasO7SdkCountryCodes
	{
		get
		{
			return HasComponent(86);
		}
	}

	public O7SdkCrossPromoComponent o7SdkCrossPromo
	{
		get
		{
			return (O7SdkCrossPromoComponent)GetComponent(87);
		}
	}

	public bool hasO7SdkCrossPromo
	{
		get
		{
			return HasComponent(87);
		}
	}

	public O7SdkCrossPromoManagerComponent o7SdkCrossPromoManager
	{
		get
		{
			return (O7SdkCrossPromoManagerComponent)GetComponent(88);
		}
	}

	public bool hasO7SdkCrossPromoManager
	{
		get
		{
			return HasComponent(88);
		}
	}

	public O7SdkDataComponent o7SdkData
	{
		get
		{
			return (O7SdkDataComponent)GetComponent(89);
		}
	}

	public bool hasO7SdkData
	{
		get
		{
			return HasComponent(89);
		}
	}

	public O7SdkDataManagerComponent o7SdkDataManager
	{
		get
		{
			return (O7SdkDataManagerComponent)GetComponent(90);
		}
	}

	public bool hasO7SdkDataManager
	{
		get
		{
			return HasComponent(90);
		}
	}

	public O7SdkEventManagerComponent o7SdkEventManager
	{
		get
		{
			return (O7SdkEventManagerComponent)GetComponent(91);
		}
	}

	public bool hasO7SdkEventManager
	{
		get
		{
			return HasComponent(91);
		}
	}

	public O7SdkExtConfigComponent o7SdkExtConfig
	{
		get
		{
			return (O7SdkExtConfigComponent)GetComponent(171);
		}
	}

	public bool hasO7SdkExtConfig
	{
		get
		{
			return HasComponent(171);
		}
	}

	public bool isO7SdkForceAgeGate
	{
		get
		{
			return HasComponent(92);
		}
		set
		{
			if (value != isO7SdkForceAgeGate)
			{
				if (value)
				{
					AddComponent(92, o7SdkForceAgeGateComponent);
				}
				else
				{
					RemoveComponent(92);
				}
			}
		}
	}

	public bool isO7SdkForceCheckProcessed
	{
		get
		{
			return HasComponent(93);
		}
		set
		{
			if (value != isO7SdkForceCheckProcessed)
			{
				if (value)
				{
					AddComponent(93, o7SdkForceCheckProcessedComponent);
				}
				else
				{
					RemoveComponent(93);
				}
			}
		}
	}

	public bool isO7SdkForceGdpr
	{
		get
		{
			return HasComponent(94);
		}
		set
		{
			if (value != isO7SdkForceGdpr)
			{
				if (value)
				{
					AddComponent(94, o7SdkForceGdprComponent);
				}
				else
				{
					RemoveComponent(94);
				}
			}
		}
	}

	public O7SdkGateDoneComponent o7SdkGateDone
	{
		get
		{
			return (O7SdkGateDoneComponent)GetComponent(95);
		}
	}

	public bool hasO7SdkGateDone
	{
		get
		{
			return HasComponent(95);
		}
	}

	public O7SdkGdprAdProvidersComponent o7SdkGdprAdProviders
	{
		get
		{
			return (O7SdkGdprAdProvidersComponent)GetComponent(96);
		}
	}

	public bool hasO7SdkGdprAdProviders
	{
		get
		{
			return HasComponent(96);
		}
	}

	public O7SdkGdprAdProvidersFetchComponent o7SdkGdprAdProvidersFetch
	{
		get
		{
			return (O7SdkGdprAdProvidersFetchComponent)GetComponent(97);
		}
	}

	public bool hasO7SdkGdprAdProvidersFetch
	{
		get
		{
			return HasComponent(97);
		}
	}

	public bool isO7SdkGdprAdProvidersSelectAll
	{
		get
		{
			return HasComponent(98);
		}
		set
		{
			if (value != isO7SdkGdprAdProvidersSelectAll)
			{
				if (value)
				{
					AddComponent(98, o7SdkGdprAdProvidersSelectAllComponent);
				}
				else
				{
					RemoveComponent(98);
				}
			}
		}
	}

	public bool isO7SdkGdprAdProvidersSubmitSelection
	{
		get
		{
			return HasComponent(99);
		}
		set
		{
			if (value != isO7SdkGdprAdProvidersSubmitSelection)
			{
				if (value)
				{
					AddComponent(99, o7SdkGdprAdProvidersSubmitSelectionComponent);
				}
				else
				{
					RemoveComponent(99);
				}
			}
		}
	}

	public bool isO7SdkGdprAdProvidersUnSelectAll
	{
		get
		{
			return HasComponent(100);
		}
		set
		{
			if (value != isO7SdkGdprAdProvidersUnSelectAll)
			{
				if (value)
				{
					AddComponent(100, o7SdkGdprAdProvidersUnSelectAllComponent);
				}
				else
				{
					RemoveComponent(100);
				}
			}
		}
	}

	public bool isO7SdkGdprCheck
	{
		get
		{
			return HasComponent(101);
		}
		set
		{
			if (value != isO7SdkGdprCheck)
			{
				if (value)
				{
					AddComponent(101, o7SdkGdprCheckComponent);
				}
				else
				{
					RemoveComponent(101);
				}
			}
		}
	}

	public bool isO7SdkGdprConsentAccepted
	{
		get
		{
			return HasComponent(102);
		}
		set
		{
			if (value != isO7SdkGdprConsentAccepted)
			{
				if (value)
				{
					AddComponent(102, o7SdkGdprConsentAcceptedComponent);
				}
				else
				{
					RemoveComponent(102);
				}
			}
		}
	}

	public bool isO7SdkGdprConsentDeclined
	{
		get
		{
			return HasComponent(103);
		}
		set
		{
			if (value != isO7SdkGdprConsentDeclined)
			{
				if (value)
				{
					AddComponent(103, o7SdkGdprConsentDeclinedComponent);
				}
				else
				{
					RemoveComponent(103);
				}
			}
		}
	}

	public bool isO7SdkGdprConsentMustShow
	{
		get
		{
			return HasComponent(104);
		}
		set
		{
			if (value != isO7SdkGdprConsentMustShow)
			{
				if (value)
				{
					AddComponent(104, o7SdkGdprConsentMustShowComponent);
				}
				else
				{
					RemoveComponent(104);
				}
			}
		}
	}

	public bool isO7SdkGdprConsentPrefSelected
	{
		get
		{
			return HasComponent(105);
		}
		set
		{
			if (value != isO7SdkGdprConsentPrefSelected)
			{
				if (value)
				{
					AddComponent(105, o7SdkGdprConsentPrefSelectedComponent);
				}
				else
				{
					RemoveComponent(105);
				}
			}
		}
	}

	public bool isO7SdkGdprConsentRequired
	{
		get
		{
			return HasComponent(106);
		}
		set
		{
			if (value != isO7SdkGdprConsentRequired)
			{
				if (value)
				{
					AddComponent(106, o7SdkGdprConsentRequiredComponent);
				}
				else
				{
					RemoveComponent(106);
				}
			}
		}
	}

	public bool isO7SdkGdprConsentShow
	{
		get
		{
			return HasComponent(107);
		}
		set
		{
			if (value != isO7SdkGdprConsentShow)
			{
				if (value)
				{
					AddComponent(107, o7SdkGdprConsentShowComponent);
				}
				else
				{
					RemoveComponent(107);
				}
			}
		}
	}

	public bool isO7SdkGdprDone
	{
		get
		{
			return HasComponent(108);
		}
		set
		{
			if (value != isO7SdkGdprDone)
			{
				if (value)
				{
					AddComponent(108, o7SdkGdprDoneComponent);
				}
				else
				{
					RemoveComponent(108);
				}
			}
		}
	}

	public O7SdkGdprEnableAdProviderComponent o7SdkGdprEnableAdProvider
	{
		get
		{
			return (O7SdkGdprEnableAdProviderComponent)GetComponent(109);
		}
	}

	public bool hasO7SdkGdprEnableAdProvider
	{
		get
		{
			return HasComponent(109);
		}
	}

	public O7SdkGdprManagerComponent o7SdkGdprManager
	{
		get
		{
			return (O7SdkGdprManagerComponent)GetComponent(110);
		}
	}

	public bool hasO7SdkGdprManager
	{
		get
		{
			return HasComponent(110);
		}
	}

	public bool isO7SdkGdprProvidersSendRequest
	{
		get
		{
			return HasComponent(111);
		}
		set
		{
			if (value != isO7SdkGdprProvidersSendRequest)
			{
				if (value)
				{
					AddComponent(111, o7SdkGdprProvidersSendRequestComponent);
				}
				else
				{
					RemoveComponent(111);
				}
			}
		}
	}

	public bool isO7SdkGdprRequest
	{
		get
		{
			return HasComponent(112);
		}
		set
		{
			if (value != isO7SdkGdprRequest)
			{
				if (value)
				{
					AddComponent(112, o7SdkGdprRequestComponent);
				}
				else
				{
					RemoveComponent(112);
				}
			}
		}
	}

	public bool isO7SdkGdprShowAdProvidersSelection
	{
		get
		{
			return HasComponent(113);
		}
		set
		{
			if (value != isO7SdkGdprShowAdProvidersSelection)
			{
				if (value)
				{
					AddComponent(113, o7SdkGdprShowAdProvidersSelectionComponent);
				}
				else
				{
					RemoveComponent(113);
				}
			}
		}
	}

	public bool isO7SdkGdprShowConsentTool
	{
		get
		{
			return HasComponent(114);
		}
		set
		{
			if (value != isO7SdkGdprShowConsentTool)
			{
				if (value)
				{
					AddComponent(114, o7SdkGdprShowConsentToolComponent);
				}
				else
				{
					RemoveComponent(114);
				}
			}
		}
	}

	public O7SdkGdprShowConsentToolTypeComponent o7SdkGdprShowConsentToolType
	{
		get
		{
			return (O7SdkGdprShowConsentToolTypeComponent)GetComponent(115);
		}
	}

	public bool hasO7SdkGdprShowConsentToolType
	{
		get
		{
			return HasComponent(115);
		}
	}

	public bool isO7SdkGdprShowDecider
	{
		get
		{
			return HasComponent(116);
		}
		set
		{
			if (value != isO7SdkGdprShowDecider)
			{
				if (value)
				{
					AddComponent(116, o7SdkGdprShowDeciderComponent);
				}
				else
				{
					RemoveComponent(116);
				}
			}
		}
	}

	public bool isO7SdkGdprShowFromSetting
	{
		get
		{
			return HasComponent(117);
		}
		set
		{
			if (value != isO7SdkGdprShowFromSetting)
			{
				if (value)
				{
					AddComponent(117, o7SdkGdprShowFromSettingComponent);
				}
				else
				{
					RemoveComponent(117);
				}
			}
		}
	}

	public O7SdkGenderGateSelectedComponent o7SdkGenderGateSelected
	{
		get
		{
			return (O7SdkGenderGateSelectedComponent)GetComponent(118);
		}
	}

	public bool hasO7SdkGenderGateSelected
	{
		get
		{
			return HasComponent(118);
		}
	}

	public bool isO7SdkGenderGateShow
	{
		get
		{
			return HasComponent(119);
		}
		set
		{
			if (value != isO7SdkGenderGateShow)
			{
				if (value)
				{
					AddComponent(119, o7SdkGenderGateShowComponent);
				}
				else
				{
					RemoveComponent(119);
				}
			}
		}
	}

	public bool isO7SdkGridDownloaded
	{
		get
		{
			return HasComponent(120);
		}
		set
		{
			if (value != isO7SdkGridDownloaded)
			{
				if (value)
				{
					AddComponent(120, o7SdkGridDownloadedComponent);
				}
				else
				{
					RemoveComponent(120);
				}
			}
		}
	}

	public O7SdkIapBuyErrorComponent o7SdkIapBuyError
	{
		get
		{
			return (O7SdkIapBuyErrorComponent)GetComponent(121);
		}
	}

	public bool hasO7SdkIapBuyError
	{
		get
		{
			return HasComponent(121);
		}
	}

	public O7SdkIapBuyFailComponent o7SdkIapBuyFail
	{
		get
		{
			return (O7SdkIapBuyFailComponent)GetComponent(122);
		}
	}

	public bool hasO7SdkIapBuyFail
	{
		get
		{
			return HasComponent(122);
		}
	}

	public O7SdkIapBuyRequestComponent o7SdkIapBuyRequest
	{
		get
		{
			return (O7SdkIapBuyRequestComponent)GetComponent(123);
		}
	}

	public bool hasO7SdkIapBuyRequest
	{
		get
		{
			return HasComponent(123);
		}
	}

	public O7SdkIapBuySuccessComponent o7SdkIapBuySuccess
	{
		get
		{
			return (O7SdkIapBuySuccessComponent)GetComponent(124);
		}
	}

	public bool hasO7SdkIapBuySuccess
	{
		get
		{
			return HasComponent(124);
		}
	}

	public O7SdkIapItemPriceData o7SdkIapItemPriceData
	{
		get
		{
			return (O7SdkIapItemPriceData)GetComponent(125);
		}
	}

	public bool hasO7SdkIapItemPriceData
	{
		get
		{
			return HasComponent(125);
		}
	}

	public bool isO7SdkIapItemsFetchRequest
	{
		get
		{
			return HasComponent(126);
		}
		set
		{
			if (value != isO7SdkIapItemsFetchRequest)
			{
				if (value)
				{
					AddComponent(126, o7SdkIapItemsFetchRequestComponent);
				}
				else
				{
					RemoveComponent(126);
				}
			}
		}
	}

	public O7SdkIapPacksComponent o7SdkIapPacks
	{
		get
		{
			return (O7SdkIapPacksComponent)GetComponent(127);
		}
	}

	public bool hasO7SdkIapPacks
	{
		get
		{
			return HasComponent(127);
		}
	}

	public O7SdkInterstitialAdComponent o7SdkInterstitialAd
	{
		get
		{
			return (O7SdkInterstitialAdComponent)GetComponent(128);
		}
	}

	public bool hasO7SdkInterstitialAd
	{
		get
		{
			return HasComponent(128);
		}
	}

	public O7SdkInterstitialAdManagerComponent o7SdkInterstitialAdManager
	{
		get
		{
			return (O7SdkInterstitialAdManagerComponent)GetComponent(129);
		}
	}

	public bool hasO7SdkInterstitialAdManager
	{
		get
		{
			return HasComponent(129);
		}
	}

	public bool isO7SdkInterstitialShown
	{
		get
		{
			return HasComponent(130);
		}
		set
		{
			if (value != isO7SdkInterstitialShown)
			{
				if (value)
				{
					AddComponent(130, o7SdkInterstitialShownComponent);
				}
				else
				{
					RemoveComponent(130);
				}
			}
		}
	}

	public O7SdkManagerComponent o7SdkManager
	{
		get
		{
			return (O7SdkManagerComponent)GetComponent(131);
		}
	}

	public bool hasO7SdkManager
	{
		get
		{
			return HasComponent(131);
		}
	}

	public bool isO7SdkPaidUser
	{
		get
		{
			return HasComponent(132);
		}
		set
		{
			if (value != isO7SdkPaidUser)
			{
				if (value)
				{
					AddComponent(132, o7SdkPaidUserComponent);
				}
				else
				{
					RemoveComponent(132);
				}
			}
		}
	}

	public O7SdkPrivacyURLComponent o7SdkPrivacyURL
	{
		get
		{
			return (O7SdkPrivacyURLComponent)GetComponent(133);
		}
	}

	public bool hasO7SdkPrivacyURL
	{
		get
		{
			return HasComponent(133);
		}
	}

	public O7SdkPurchaseManagerComponent o7SdkPurchaseManager
	{
		get
		{
			return (O7SdkPurchaseManagerComponent)GetComponent(134);
		}
	}

	public bool hasO7SdkPurchaseManager
	{
		get
		{
			return HasComponent(134);
		}
	}

	public bool isO7SdkRateMeClosed
	{
		get
		{
			return HasComponent(135);
		}
		set
		{
			if (value != isO7SdkRateMeClosed)
			{
				if (value)
				{
					AddComponent(135, o7SdkRateMeClosedComponent);
				}
				else
				{
					RemoveComponent(135);
				}
			}
		}
	}

	public bool isO7SdkRateMe
	{
		get
		{
			return HasComponent(136);
		}
		set
		{
			if (value != isO7SdkRateMe)
			{
				if (value)
				{
					AddComponent(136, o7SdkRateMeComponent);
				}
				else
				{
					RemoveComponent(136);
				}
			}
		}
	}

	public bool isO7SdkRateMeDontLike
	{
		get
		{
			return HasComponent(137);
		}
		set
		{
			if (value != isO7SdkRateMeDontLike)
			{
				if (value)
				{
					AddComponent(137, o7SdkRateMeDontLikeComponent);
				}
				else
				{
					RemoveComponent(137);
				}
			}
		}
	}

	public bool isO7SdkRateMeLater
	{
		get
		{
			return HasComponent(138);
		}
		set
		{
			if (value != isO7SdkRateMeLater)
			{
				if (value)
				{
					AddComponent(138, o7SdkRateMeLaterComponent);
				}
				else
				{
					RemoveComponent(138);
				}
			}
		}
	}

	public bool isO7SdkRateMeNow
	{
		get
		{
			return HasComponent(139);
		}
		set
		{
			if (value != isO7SdkRateMeNow)
			{
				if (value)
				{
					AddComponent(139, o7SdkRateMeNowComponent);
				}
				else
				{
					RemoveComponent(139);
				}
			}
		}
	}

	public bool isO7SdkReadyToLoadGame
	{
		get
		{
			return HasComponent(140);
		}
		set
		{
			if (value != isO7SdkReadyToLoadGame)
			{
				if (value)
				{
					AddComponent(140, o7SdkReadyToLoadGameComponent);
				}
				else
				{
					RemoveComponent(140);
				}
			}
		}
	}

	public bool isO7SdkRewardAdFetched
	{
		get
		{
			return HasComponent(141);
		}
		set
		{
			if (value != isO7SdkRewardAdFetched)
			{
				if (value)
				{
					AddComponent(141, o7SdkRewardAdFetchedComponent);
				}
				else
				{
					RemoveComponent(141);
				}
			}
		}
	}

	public bool isO7SdkRewardAdsFetchRequested
	{
		get
		{
			return HasComponent(142);
		}
		set
		{
			if (value != isO7SdkRewardAdsFetchRequested)
			{
				if (value)
				{
					AddComponent(142, o7SdkRewardAdsFetchRequestedComponent);
				}
				else
				{
					RemoveComponent(142);
				}
			}
		}
	}

	public bool isO7SdkRewardAdsShow
	{
		get
		{
			return HasComponent(143);
		}
		set
		{
			if (value != isO7SdkRewardAdsShow)
			{
				if (value)
				{
					AddComponent(143, o7SdkRewardAdsShowComponent);
				}
				else
				{
					RemoveComponent(143);
				}
			}
		}
	}

	public O7SdkRewardAwardedComponent o7SdkRewardAwarded
	{
		get
		{
			return (O7SdkRewardAwardedComponent)GetComponent(144);
		}
	}

	public bool hasO7SdkRewardAwarded
	{
		get
		{
			return HasComponent(144);
		}
	}

	public O7SdkRewardedAdManagerComponent o7SdkRewardedAdManager
	{
		get
		{
			return (O7SdkRewardedAdManagerComponent)GetComponent(145);
		}
	}

	public bool hasO7SdkRewardedAdManager
	{
		get
		{
			return HasComponent(145);
		}
	}

	public bool isO7SdkRewardNotAwarded
	{
		get
		{
			return HasComponent(146);
		}
		set
		{
			if (value != isO7SdkRewardNotAwarded)
			{
				if (value)
				{
					AddComponent(146, o7SdkRewardNotAwardedComponent);
				}
				else
				{
					RemoveComponent(146);
				}
			}
		}
	}

	public O7SdkRewardOfferTypeComponent o7SdkRewardOfferType
	{
		get
		{
			return (O7SdkRewardOfferTypeComponent)GetComponent(147);
		}
	}

	public bool hasO7SdkRewardOfferType
	{
		get
		{
			return HasComponent(147);
		}
	}

	public bool isO7SdkRewardTransaction
	{
		get
		{
			return HasComponent(148);
		}
		set
		{
			if (value != isO7SdkRewardTransaction)
			{
				if (value)
				{
					AddComponent(148, o7SdkRewardTransactionComponent);
				}
				else
				{
					RemoveComponent(148);
				}
			}
		}
	}

	public O7SdkSelectCountryCodeComponent o7SdkSelectCountryCode
	{
		get
		{
			return (O7SdkSelectCountryCodeComponent)GetComponent(149);
		}
	}

	public bool hasO7SdkSelectCountryCode
	{
		get
		{
			return HasComponent(149);
		}
	}

	public O7SdkSendLogsComponent o7SdkSendLogs
	{
		get
		{
			return (O7SdkSendLogsComponent)GetComponent(150);
		}
	}

	public bool hasO7SdkSendLogs
	{
		get
		{
			return HasComponent(150);
		}
	}

	public O7SdkUserStateManagerComponent o7SdkUserStateManager
	{
		get
		{
			return (O7SdkUserStateManagerComponent)GetComponent(151);
		}
	}

	public bool hasO7SdkUserStateManager
	{
		get
		{
			return HasComponent(151);
		}
	}

	public O7SdkUtilityManagerComponent o7SdkUtilityManager
	{
		get
		{
			return (O7SdkUtilityManagerComponent)GetComponent(152);
		}
	}

	public bool hasO7SdkUtilityManager
	{
		get
		{
			return HasComponent(152);
		}
	}

	public O7SdkValidIapPacksComponent o7SdkValidIapPacks
	{
		get
		{
			return (O7SdkValidIapPacksComponent)GetComponent(153);
		}
	}

	public bool hasO7SdkValidIapPacks
	{
		get
		{
			return HasComponent(153);
		}
	}

	public bool isO7SdkVideoGallery
	{
		get
		{
			return HasComponent(154);
		}
		set
		{
			if (value != isO7SdkVideoGallery)
			{
				if (value)
				{
					AddComponent(154, o7SdkVideoGalleryComponent);
				}
				else
				{
					RemoveComponent(154);
				}
			}
		}
	}

	public O7SdkVideoGalleryManagerComponent o7SdkVideoGalleryManager
	{
		get
		{
			return (O7SdkVideoGalleryManagerComponent)GetComponent(155);
		}
	}

	public bool hasO7SdkVideoGalleryManager
	{
		get
		{
			return HasComponent(155);
		}
	}

	public bool isOpenFreeChest
	{
		get
		{
			return HasComponent(156);
		}
		set
		{
			if (value != isOpenFreeChest)
			{
				if (value)
				{
					AddComponent(156, openFreeChestComponent);
				}
				else
				{
					RemoveComponent(156);
				}
			}
		}
	}

	public bool isOpenPrivacyUrl
	{
		get
		{
			return HasComponent(157);
		}
		set
		{
			if (value != isOpenPrivacyUrl)
			{
				if (value)
				{
					AddComponent(157, openPrivacyUrlComponent);
				}
				else
				{
					RemoveComponent(157);
				}
			}
		}
	}

	public OpenUrlComponent openUrl
	{
		get
		{
			return (OpenUrlComponent)GetComponent(158);
		}
	}

	public bool hasOpenUrl
	{
		get
		{
			return HasComponent(158);
		}
	}

	public bool isOverrideBalancingConfiguration
	{
		get
		{
			return HasComponent(159);
		}
		set
		{
			if (value != isOverrideBalancingConfiguration)
			{
				if (value)
				{
					AddComponent(159, overrideBalancingConfigurationComponent);
				}
				else
				{
					RemoveComponent(159);
				}
			}
		}
	}

	public PassLevelActionComponent passLevelAction
	{
		get
		{
			return (PassLevelActionComponent)GetComponent(160);
		}
	}

	public bool hasPassLevelAction
	{
		get
		{
			return HasComponent(160);
		}
	}

	public PendingAsyncRequestsComponent pendingAsyncRequests
	{
		get
		{
			return (PendingAsyncRequestsComponent)GetComponent(161);
		}
	}

	public bool hasPendingAsyncRequests
	{
		get
		{
			return HasComponent(161);
		}
	}

	public PlayerExperienceGrantedComponent playerExperienceGranted
	{
		get
		{
			return (PlayerExperienceGrantedComponent)GetComponent(162);
		}
	}

	public bool hasPlayerExperienceGranted
	{
		get
		{
			return HasComponent(162);
		}
	}

	public PlayerLevelBalancingDataComponent playerLevelBalancingData
	{
		get
		{
			return (PlayerLevelBalancingDataComponent)GetComponent(163);
		}
	}

	public bool hasPlayerLevelBalancingData
	{
		get
		{
			return HasComponent(163);
		}
	}

	public PlayerLevelExperienceAmountDataComponent playerLevelExperienceAmountData
	{
		get
		{
			return (PlayerLevelExperienceAmountDataComponent)GetComponent(164);
		}
	}

	public bool hasPlayerLevelExperienceAmountData
	{
		get
		{
			return HasComponent(164);
		}
	}

	public PlayerLevelListenerComponent playerLevelListener
	{
		get
		{
			return (PlayerLevelListenerComponent)GetComponent(165);
		}
	}

	public bool hasPlayerLevelListener
	{
		get
		{
			return HasComponent(165);
		}
	}

	public PlayerLevelUpComponent playerLevelUp
	{
		get
		{
			return (PlayerLevelUpComponent)GetComponent(166);
		}
	}

	public bool hasPlayerLevelUp
	{
		get
		{
			return HasComponent(166);
		}
	}

	public PlayerProgressionUpdatedComponent playerProgressionUpdated
	{
		get
		{
			return (PlayerProgressionUpdatedComponent)GetComponent(167);
		}
	}

	public bool hasPlayerProgressionUpdated
	{
		get
		{
			return HasComponent(167);
		}
	}

	public PlayLoopSoundEffectComponent playLoopSoundEffect
	{
		get
		{
			return (PlayLoopSoundEffectComponent)GetComponent(168);
		}
	}

	public bool hasPlayLoopSoundEffect
	{
		get
		{
			return HasComponent(168);
		}
	}

	public PlayMusicComponent playMusic
	{
		get
		{
			return (PlayMusicComponent)GetComponent(169);
		}
	}

	public bool hasPlayMusic
	{
		get
		{
			return HasComponent(169);
		}
	}

	public PlaySoundEffectComponent playSoundEffect
	{
		get
		{
			return (PlaySoundEffectComponent)GetComponent(172);
		}
	}

	public bool hasPlaySoundEffect
	{
		get
		{
			return HasComponent(172);
		}
	}

	public PreviousScreenComponent previousScreen
	{
		get
		{
			return (PreviousScreenComponent)GetComponent(173);
		}
	}

	public bool hasPreviousScreen
	{
		get
		{
			return HasComponent(173);
		}
	}

	public PrivacyAndLegacyBalancingDataComponent privacyAndLegacyBalancingData
	{
		get
		{
			return (PrivacyAndLegacyBalancingDataComponent)GetComponent(174);
		}
	}

	public bool hasPrivacyAndLegacyBalancingData
	{
		get
		{
			return HasComponent(174);
		}
	}

	public bool isPrivacyAndLegalClickedEventToBeSent
	{
		get
		{
			return HasComponent(175);
		}
		set
		{
			if (value != isPrivacyAndLegalClickedEventToBeSent)
			{
				if (value)
				{
					AddComponent(175, privacyAndLegalClickedEventToBeSentComponent);
				}
				else
				{
					RemoveComponent(175);
				}
			}
		}
	}

	public RemoteUserStateComponent remoteUserState
	{
		get
		{
			return (RemoteUserStateComponent)GetComponent(176);
		}
	}

	public bool hasRemoteUserState
	{
		get
		{
			return HasComponent(176);
		}
	}

	public ReplaceTargetEventComponent replaceTargetEvent
	{
		get
		{
			return (ReplaceTargetEventComponent)GetComponent(177);
		}
	}

	public bool hasReplaceTargetEvent
	{
		get
		{
			return HasComponent(177);
		}
	}

	public ReplaceUserStateComponent replaceUserState
	{
		get
		{
			return (ReplaceUserStateComponent)GetComponent(178);
		}
	}

	public bool hasReplaceUserState
	{
		get
		{
			return HasComponent(178);
		}
	}

	public RestoreSettingsBalancingDataComponent restoreSettingsBalancingData
	{
		get
		{
			return (RestoreSettingsBalancingDataComponent)GetComponent(179);
		}
	}

	public bool hasRestoreSettingsBalancingData
	{
		get
		{
			return HasComponent(179);
		}
	}

	public bool isSavedData
	{
		get
		{
			return HasComponent(180);
		}
		set
		{
			if (value != isSavedData)
			{
				if (value)
				{
					AddComponent(180, savedDataComponent);
				}
				else
				{
					RemoveComponent(180);
				}
			}
		}
	}

	public bool isSession
	{
		get
		{
			return HasComponent(181);
		}
		set
		{
			if (value != isSession)
			{
				if (value)
				{
					AddComponent(181, sessionComponent);
				}
				else
				{
					RemoveComponent(181);
				}
			}
		}
	}

	public ShopIAPsBalancingDataComponent shopIAPsBalancingData
	{
		get
		{
			return (ShopIAPsBalancingDataComponent)GetComponent(182);
		}
	}

	public bool hasShopIAPsBalancingData
	{
		get
		{
			return HasComponent(182);
		}
	}

	public ShopSoftCurrencyPacksDataComponent shopSoftCurrencyPacksData
	{
		get
		{
			return (ShopSoftCurrencyPacksDataComponent)GetComponent(183);
		}
	}

	public bool hasShopSoftCurrencyPacksData
	{
		get
		{
			return HasComponent(183);
		}
	}

	public ShowHandAcitonComponent showHandAciton
	{
		get
		{
			return (ShowHandAcitonComponent)GetComponent(184);
		}
	}

	public bool hasShowHandAciton
	{
		get
		{
			return HasComponent(184);
		}
	}

	public SoftCurrnecyPriceComponent softCurrnecyPrice
	{
		get
		{
			return (SoftCurrnecyPriceComponent)GetComponent(185);
		}
	}

	public bool hasSoftCurrnecyPrice
	{
		get
		{
			return HasComponent(185);
		}
	}

	public StaminaSettingsBalancingDataComponent staminaSettingsBalancingData
	{
		get
		{
			return (StaminaSettingsBalancingDataComponent)GetComponent(186);
		}
	}

	public bool hasStaminaSettingsBalancingData
	{
		get
		{
			return HasComponent(186);
		}
	}

	public StopLoopSoundEffectComponent stopLoopSoundEffect
	{
		get
		{
			return (StopLoopSoundEffectComponent)GetComponent(187);
		}
	}

	public bool hasStopLoopSoundEffect
	{
		get
		{
			return HasComponent(187);
		}
	}

	public StopMusicComponent stopMusic
	{
		get
		{
			return (StopMusicComponent)GetComponent(188);
		}
	}

	public bool hasStopMusic
	{
		get
		{
			return HasComponent(188);
		}
	}

	public StringDataComponent stringData
	{
		get
		{
			return (StringDataComponent)GetComponent(189);
		}
	}

	public bool hasStringData
	{
		get
		{
			return HasComponent(189);
		}
	}

	public bool isToLoad
	{
		get
		{
			return HasComponent(190);
		}
		set
		{
			if (value != isToLoad)
			{
				if (value)
				{
					AddComponent(190, toLoadComponent);
				}
				else
				{
					RemoveComponent(190);
				}
			}
		}
	}

	public bool isToSave
	{
		get
		{
			return HasComponent(191);
		}
		set
		{
			if (value != isToSave)
			{
				if (value)
				{
					AddComponent(191, toSaveComponent);
				}
				else
				{
					RemoveComponent(191);
				}
			}
		}
	}

	public bool isTransactionBegin
	{
		get
		{
			return HasComponent(192);
		}
		set
		{
			if (value != isTransactionBegin)
			{
				if (value)
				{
					AddComponent(192, transactionBeginComponent);
				}
				else
				{
					RemoveComponent(192);
				}
			}
		}
	}

	public TransactionCallback transactionCallback
	{
		get
		{
			return (TransactionCallback)GetComponent(193);
		}
	}

	public bool hasTransactionCallback
	{
		get
		{
			return HasComponent(193);
		}
	}

	public bool isTransactionComplete
	{
		get
		{
			return HasComponent(194);
		}
		set
		{
			if (value != isTransactionComplete)
			{
				if (value)
				{
					AddComponent(194, transactionCompleteComponent);
				}
				else
				{
					RemoveComponent(194);
				}
			}
		}
	}

	public bool isTransaction
	{
		get
		{
			return HasComponent(195);
		}
		set
		{
			if (value != isTransaction)
			{
				if (value)
				{
					AddComponent(195, transactionComponent);
				}
				else
				{
					RemoveComponent(195);
				}
			}
		}
	}

	public bool isTransactionGranted
	{
		get
		{
			return HasComponent(196);
		}
		set
		{
			if (value != isTransactionGranted)
			{
				if (value)
				{
					AddComponent(196, transactionGrantedComponent);
				}
				else
				{
					RemoveComponent(196);
				}
			}
		}
	}

	public TransactionImage transactionImage
	{
		get
		{
			return (TransactionImage)GetComponent(197);
		}
	}

	public bool hasTransactionImage
	{
		get
		{
			return HasComponent(197);
		}
	}

	public TransactionName transactionName
	{
		get
		{
			return (TransactionName)GetComponent(198);
		}
	}

	public bool hasTransactionName
	{
		get
		{
			return HasComponent(198);
		}
	}

	public bool isTransactionNotValidated
	{
		get
		{
			return HasComponent(199);
		}
		set
		{
			if (value != isTransactionNotValidated)
			{
				if (value)
				{
					AddComponent(199, transactionNotValidatedComponent);
				}
				else
				{
					RemoveComponent(199);
				}
			}
		}
	}

	public TransactionOrigin transactionOrigin
	{
		get
		{
			return (TransactionOrigin)GetComponent(200);
		}
	}

	public bool hasTransactionOrigin
	{
		get
		{
			return HasComponent(200);
		}
	}

	public bool isTransactionRequireConfirmation
	{
		get
		{
			return HasComponent(201);
		}
		set
		{
			if (value != isTransactionRequireConfirmation)
			{
				if (value)
				{
					AddComponent(201, transactionRequireConfirmationComponent);
				}
				else
				{
					RemoveComponent(201);
				}
			}
		}
	}

	public bool isTransactionValidated
	{
		get
		{
			return HasComponent(202);
		}
		set
		{
			if (value != isTransactionValidated)
			{
				if (value)
				{
					AddComponent(202, transactionValidatedComponent);
				}
				else
				{
					RemoveComponent(202);
				}
			}
		}
	}

	public TransitionInformationComponent transitionInformation
	{
		get
		{
			return (TransitionInformationComponent)GetComponent(203);
		}
	}

	public bool hasTransitionInformation
	{
		get
		{
			return HasComponent(203);
		}
	}

	public TutorialActiveStepComponent tutorialActiveStep
	{
		get
		{
			return (TutorialActiveStepComponent)GetComponent(204);
		}
	}

	public bool hasTutorialActiveStep
	{
		get
		{
			return HasComponent(204);
		}
	}

	public bool isTutorial
	{
		get
		{
			return HasComponent(205);
		}
		set
		{
			if (value != isTutorial)
			{
				if (value)
				{
					AddComponent(205, tutorialComponent);
				}
				else
				{
					RemoveComponent(205);
				}
			}
		}
	}

	public bool isTutorialDisabled
	{
		get
		{
			return HasComponent(206);
		}
		set
		{
			if (value != isTutorialDisabled)
			{
				if (value)
				{
					AddComponent(206, tutorialDisabledComponent);
				}
				else
				{
					RemoveComponent(206);
				}
			}
		}
	}

	public TutorialDisableStepComponent tutorialDisableStep
	{
		get
		{
			return (TutorialDisableStepComponent)GetComponent(207);
		}
	}

	public bool hasTutorialDisableStep
	{
		get
		{
			return HasComponent(207);
		}
	}

	public TutorialEnableStepComponent tutorialEnableStep
	{
		get
		{
			return (TutorialEnableStepComponent)GetComponent(208);
		}
	}

	public bool hasTutorialEnableStep
	{
		get
		{
			return HasComponent(208);
		}
	}

	public bool isTutorialInitialized
	{
		get
		{
			return HasComponent(209);
		}
		set
		{
			if (value != isTutorialInitialized)
			{
				if (value)
				{
					AddComponent(209, tutorialInitializedComponent);
				}
				else
				{
					RemoveComponent(209);
				}
			}
		}
	}

	public TutorialOutgameUnlockLevelsBalancingDataComponent tutorialOutgameUnlockLevelsBalancingData
	{
		get
		{
			return (TutorialOutgameUnlockLevelsBalancingDataComponent)GetComponent(210);
		}
	}

	public bool hasTutorialOutgameUnlockLevelsBalancingData
	{
		get
		{
			return HasComponent(210);
		}
	}

	public TutorialPossibleGroupsComponent tutorialPossibleGroups
	{
		get
		{
			return (TutorialPossibleGroupsComponent)GetComponent(211);
		}
	}

	public bool hasTutorialPossibleGroups
	{
		get
		{
			return HasComponent(211);
		}
	}

	public TutorialStepDisableTriggeredComponent tutorialStepDisableTriggered
	{
		get
		{
			return (TutorialStepDisableTriggeredComponent)GetComponent(212);
		}
	}

	public bool hasTutorialStepDisableTriggered
	{
		get
		{
			return HasComponent(212);
		}
	}

	public TutorialStepEnableTriggeredComponent tutorialStepEnableTriggered
	{
		get
		{
			return (TutorialStepEnableTriggeredComponent)GetComponent(213);
		}
	}

	public bool hasTutorialStepEnableTriggered
	{
		get
		{
			return HasComponent(213);
		}
	}

	public TutorialStepsQueueComponent tutorialStepsQueue
	{
		get
		{
			return (TutorialStepsQueueComponent)GetComponent(214);
		}
	}

	public bool hasTutorialStepsQueue
	{
		get
		{
			return HasComponent(214);
		}
	}

	public TutorialTargetElement tutorialTargetElement
	{
		get
		{
			return (TutorialTargetElement)GetComponent(215);
		}
	}

	public bool hasTutorialTargetElement
	{
		get
		{
			return HasComponent(215);
		}
	}

	public TutorialTriggersListComponent tutorialTriggersList
	{
		get
		{
			return (TutorialTriggersListComponent)GetComponent(216);
		}
	}

	public bool hasTutorialTriggersList
	{
		get
		{
			return HasComponent(216);
		}
	}

	public UDIDHashComponent uDIDHash
	{
		get
		{
			return (UDIDHashComponent)GetComponent(217);
		}
	}

	public bool hasUDIDHash
	{
		get
		{
			return HasComponent(217);
		}
	}

	public UpgradeCharacterComponent upgradeCharacter
	{
		get
		{
			return (UpgradeCharacterComponent)GetComponent(218);
		}
	}

	public bool hasUpgradeCharacter
	{
		get
		{
			return HasComponent(218);
		}
	}

	public UpgradeCompanionComponent upgradeCompanion
	{
		get
		{
			return (UpgradeCompanionComponent)GetComponent(219);
		}
	}

	public bool hasUpgradeCompanion
	{
		get
		{
			return HasComponent(219);
		}
	}

	public bool isUserStateData
	{
		get
		{
			return HasComponent(220);
		}
		set
		{
			if (value != isUserStateData)
			{
				if (value)
				{
					AddComponent(220, userStateDataComponent);
				}
				else
				{
					RemoveComponent(220);
				}
			}
		}
	}

	public UserStateLastUpdateTimeComponent userStateLastUpdateTime
	{
		get
		{
			return (UserStateLastUpdateTimeComponent)GetComponent(221);
		}
	}

	public bool hasUserStateLastUpdateTime
	{
		get
		{
			return HasComponent(221);
		}
	}

	public bool isUserStateShow
	{
		get
		{
			return HasComponent(222);
		}
		set
		{
			if (value != isUserStateShow)
			{
				if (value)
				{
					AddComponent(222, userStateShowComponent);
				}
				else
				{
					RemoveComponent(222);
				}
			}
		}
	}

	public UserStateSolveConflictComponent userStateSolveConflict
	{
		get
		{
			return (UserStateSolveConflictComponent)GetComponent(223);
		}
	}

	public bool hasUserStateSolveConflict
	{
		get
		{
			return HasComponent(223);
		}
	}

	public bool isUserStateTryStartNewGame
	{
		get
		{
			return HasComponent(224);
		}
		set
		{
			if (value != isUserStateTryStartNewGame)
			{
				if (value)
				{
					AddComponent(224, userStateTryStartNewGameComponent);
				}
				else
				{
					RemoveComponent(224);
				}
			}
		}
	}

	public bool isWorldSelectUnlockingAnimationStoped
	{
		get
		{
			return HasComponent(225);
		}
		set
		{
			if (value != isWorldSelectUnlockingAnimationStoped)
			{
				if (value)
				{
					AddComponent(225, worldSelectUnlockingAnimationStopedComponent);
				}
				else
				{
					RemoveComponent(225);
				}
			}
		}
	}

	public void AddAbilitiesBalancingData(Dictionary<AbilityType, AbilitiesBalancingData> newMap)
	{
		int index = 0;
		AbilitiesBalancingDataComponent abilitiesBalancingDataComponent = CreateComponent<AbilitiesBalancingDataComponent>(index);
		abilitiesBalancingDataComponent.Map = newMap;
		AddComponent(index, abilitiesBalancingDataComponent);
	}

	public void ReplaceAbilitiesBalancingData(Dictionary<AbilityType, AbilitiesBalancingData> newMap)
	{
		int index = 0;
		AbilitiesBalancingDataComponent abilitiesBalancingDataComponent = CreateComponent<AbilitiesBalancingDataComponent>(index);
		abilitiesBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, abilitiesBalancingDataComponent);
	}

	public void RemoveAbilitiesBalancingData()
	{
		RemoveComponent(0);
	}

	public void AddActionDelay(float newDelay)
	{
		int index = 1;
		ActionDelayComponent actionDelayComponent = CreateComponent<ActionDelayComponent>(index);
		actionDelayComponent.Delay = newDelay;
		AddComponent(index, actionDelayComponent);
	}

	public void ReplaceActionDelay(float newDelay)
	{
		int index = 1;
		ActionDelayComponent actionDelayComponent = CreateComponent<ActionDelayComponent>(index);
		actionDelayComponent.Delay = newDelay;
		ReplaceComponent(index, actionDelayComponent);
	}

	public void RemoveActionDelay()
	{
		RemoveComponent(1);
	}

	public void AddActiveScreen(ScreenId newScreenId)
	{
		int index = 2;
		ActiveScreenComponent activeScreenComponent = CreateComponent<ActiveScreenComponent>(index);
		activeScreenComponent.ScreenId = newScreenId;
		AddComponent(index, activeScreenComponent);
	}

	public void ReplaceActiveScreen(ScreenId newScreenId)
	{
		int index = 2;
		ActiveScreenComponent activeScreenComponent = CreateComponent<ActiveScreenComponent>(index);
		activeScreenComponent.ScreenId = newScreenId;
		ReplaceComponent(index, activeScreenComponent);
	}

	public void RemoveActiveScreen()
	{
		RemoveComponent(2);
	}

	public void AddBalancingDataOverrideConfig(BalancingDataOverrideConfig newData)
	{
		int index = 4;
		BalancingDataOverrideConfigComponent balancingDataOverrideConfigComponent = CreateComponent<BalancingDataOverrideConfigComponent>(index);
		balancingDataOverrideConfigComponent.Data = newData;
		AddComponent(index, balancingDataOverrideConfigComponent);
	}

	public void ReplaceBalancingDataOverrideConfig(BalancingDataOverrideConfig newData)
	{
		int index = 4;
		BalancingDataOverrideConfigComponent balancingDataOverrideConfigComponent = CreateComponent<BalancingDataOverrideConfigComponent>(index);
		balancingDataOverrideConfigComponent.Data = newData;
		ReplaceComponent(index, balancingDataOverrideConfigComponent);
	}

	public void RemoveBalancingDataOverrideConfig()
	{
		RemoveComponent(4);
	}

	public void AddBoolean(bool newValue)
	{
		int index = 5;
		BooleanComponent booleanComponent = CreateComponent<BooleanComponent>(index);
		booleanComponent.Value = newValue;
		AddComponent(index, booleanComponent);
	}

	public void ReplaceBoolean(bool newValue)
	{
		int index = 5;
		BooleanComponent booleanComponent = CreateComponent<BooleanComponent>(index);
		booleanComponent.Value = newValue;
		ReplaceComponent(index, booleanComponent);
	}

	public void RemoveBoolean()
	{
		RemoveComponent(5);
	}

	public void AddBoughtCharacterUpgrade(int newCharacterId)
	{
		int index = 6;
		BoughtCharacterUpgradeComponent boughtCharacterUpgradeComponent = CreateComponent<BoughtCharacterUpgradeComponent>(index);
		boughtCharacterUpgradeComponent.CharacterId = newCharacterId;
		AddComponent(index, boughtCharacterUpgradeComponent);
	}

	public void ReplaceBoughtCharacterUpgrade(int newCharacterId)
	{
		int index = 6;
		BoughtCharacterUpgradeComponent boughtCharacterUpgradeComponent = CreateComponent<BoughtCharacterUpgradeComponent>(index);
		boughtCharacterUpgradeComponent.CharacterId = newCharacterId;
		ReplaceComponent(index, boughtCharacterUpgradeComponent);
	}

	public void RemoveBoughtCharacterUpgrade()
	{
		RemoveComponent(6);
	}

	public void AddBoughtChest(ChestData newChest, ChestType newType)
	{
		int index = 7;
		BoughtChestComponent boughtChestComponent = CreateComponent<BoughtChestComponent>(index);
		boughtChestComponent.Chest = newChest;
		boughtChestComponent.Type = newType;
		AddComponent(index, boughtChestComponent);
	}

	public void ReplaceBoughtChest(ChestData newChest, ChestType newType)
	{
		int index = 7;
		BoughtChestComponent boughtChestComponent = CreateComponent<BoughtChestComponent>(index);
		boughtChestComponent.Chest = newChest;
		boughtChestComponent.Type = newType;
		ReplaceComponent(index, boughtChestComponent);
	}

	public void RemoveBoughtChest()
	{
		RemoveComponent(7);
	}

	public void AddBoughtChestPrepared(List<GeneratedReward> newPreparedChestRewards)
	{
		int index = 8;
		BoughtChestPreparedComponent boughtChestPreparedComponent = CreateComponent<BoughtChestPreparedComponent>(index);
		boughtChestPreparedComponent.PreparedChestRewards = newPreparedChestRewards;
		AddComponent(index, boughtChestPreparedComponent);
	}

	public void ReplaceBoughtChestPrepared(List<GeneratedReward> newPreparedChestRewards)
	{
		int index = 8;
		BoughtChestPreparedComponent boughtChestPreparedComponent = CreateComponent<BoughtChestPreparedComponent>(index);
		boughtChestPreparedComponent.PreparedChestRewards = newPreparedChestRewards;
		ReplaceComponent(index, boughtChestPreparedComponent);
	}

	public void RemoveBoughtChestPrepared()
	{
		RemoveComponent(8);
	}

	public void AddBoughtCompanionUpgrade(int newCompanionId)
	{
		int index = 9;
		BoughtCompanionUpgradeComponent boughtCompanionUpgradeComponent = CreateComponent<BoughtCompanionUpgradeComponent>(index);
		boughtCompanionUpgradeComponent.CompanionId = newCompanionId;
		AddComponent(index, boughtCompanionUpgradeComponent);
	}

	public void ReplaceBoughtCompanionUpgrade(int newCompanionId)
	{
		int index = 9;
		BoughtCompanionUpgradeComponent boughtCompanionUpgradeComponent = CreateComponent<BoughtCompanionUpgradeComponent>(index);
		boughtCompanionUpgradeComponent.CompanionId = newCompanionId;
		ReplaceComponent(index, boughtCompanionUpgradeComponent);
	}

	public void RemoveBoughtCompanionUpgrade()
	{
		RemoveComponent(9);
	}

	public void AddBoughtEnergyRefill(int newCharacterId)
	{
		int index = 10;
		BoughtEnergyRefill boughtEnergyRefill = CreateComponent<BoughtEnergyRefill>(index);
		boughtEnergyRefill.CharacterId = newCharacterId;
		AddComponent(index, boughtEnergyRefill);
	}

	public void ReplaceBoughtEnergyRefill(int newCharacterId)
	{
		int index = 10;
		BoughtEnergyRefill boughtEnergyRefill = CreateComponent<BoughtEnergyRefill>(index);
		boughtEnergyRefill.CharacterId = newCharacterId;
		ReplaceComponent(index, boughtEnergyRefill);
	}

	public void RemoveBoughtEnergyRefill()
	{
		RemoveComponent(10);
	}

	public void AddBoughtHardCurrency(int newAmount)
	{
		int index = 11;
		BoughtHardCurrencyComponent boughtHardCurrencyComponent = CreateComponent<BoughtHardCurrencyComponent>(index);
		boughtHardCurrencyComponent.Amount = newAmount;
		AddComponent(index, boughtHardCurrencyComponent);
	}

	public void ReplaceBoughtHardCurrency(int newAmount)
	{
		int index = 11;
		BoughtHardCurrencyComponent boughtHardCurrencyComponent = CreateComponent<BoughtHardCurrencyComponent>(index);
		boughtHardCurrencyComponent.Amount = newAmount;
		ReplaceComponent(index, boughtHardCurrencyComponent);
	}

	public void RemoveBoughtHardCurrency()
	{
		RemoveComponent(11);
	}

	public void AddBoughtPlayerResurect(float newDelay)
	{
		int index = 12;
		BoughtPlayerResurect boughtPlayerResurect = CreateComponent<BoughtPlayerResurect>(index);
		boughtPlayerResurect.Delay = newDelay;
		AddComponent(index, boughtPlayerResurect);
	}

	public void ReplaceBoughtPlayerResurect(float newDelay)
	{
		int index = 12;
		BoughtPlayerResurect boughtPlayerResurect = CreateComponent<BoughtPlayerResurect>(index);
		boughtPlayerResurect.Delay = newDelay;
		ReplaceComponent(index, boughtPlayerResurect);
	}

	public void RemoveBoughtPlayerResurect()
	{
		RemoveComponent(12);
	}

	public void AddBoughtSoftCurrency(int newAmount)
	{
		int index = 14;
		BoughtSoftCurrencyComponent boughtSoftCurrencyComponent = CreateComponent<BoughtSoftCurrencyComponent>(index);
		boughtSoftCurrencyComponent.Amount = newAmount;
		AddComponent(index, boughtSoftCurrencyComponent);
	}

	public void ReplaceBoughtSoftCurrency(int newAmount)
	{
		int index = 14;
		BoughtSoftCurrencyComponent boughtSoftCurrencyComponent = CreateComponent<BoughtSoftCurrencyComponent>(index);
		boughtSoftCurrencyComponent.Amount = newAmount;
		ReplaceComponent(index, boughtSoftCurrencyComponent);
	}

	public void RemoveBoughtSoftCurrency()
	{
		RemoveComponent(14);
	}

	public void AddChangeTimeScale(float newScale)
	{
		int index = 15;
		ChangeTimeScaleComponent changeTimeScaleComponent = CreateComponent<ChangeTimeScaleComponent>(index);
		changeTimeScaleComponent.scale = newScale;
		AddComponent(index, changeTimeScaleComponent);
	}

	public void ReplaceChangeTimeScale(float newScale)
	{
		int index = 15;
		ChangeTimeScaleComponent changeTimeScaleComponent = CreateComponent<ChangeTimeScaleComponent>(index);
		changeTimeScaleComponent.scale = newScale;
		ReplaceComponent(index, changeTimeScaleComponent);
	}

	public void RemoveChangeTimeScale()
	{
		RemoveComponent(15);
	}

	public void AddChapterBalancingData(Dictionary<int, ChaptersBalancingData> newMap)
	{
		int index = 16;
		ChapterBalancingDataComponent chapterBalancingDataComponent = CreateComponent<ChapterBalancingDataComponent>(index);
		chapterBalancingDataComponent.Map = newMap;
		AddComponent(index, chapterBalancingDataComponent);
	}

	public void ReplaceChapterBalancingData(Dictionary<int, ChaptersBalancingData> newMap)
	{
		int index = 16;
		ChapterBalancingDataComponent chapterBalancingDataComponent = CreateComponent<ChapterBalancingDataComponent>(index);
		chapterBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, chapterBalancingDataComponent);
	}

	public void RemoveChapterBalancingData()
	{
		RemoveComponent(16);
	}

	public void AddCharacterPartsPrice(int newCharacterId, int newAmount)
	{
		int index = 17;
		CharacterPartsPriceComponent characterPartsPriceComponent = CreateComponent<CharacterPartsPriceComponent>(index);
		characterPartsPriceComponent.CharacterId = newCharacterId;
		characterPartsPriceComponent.Amount = newAmount;
		AddComponent(index, characterPartsPriceComponent);
	}

	public void ReplaceCharacterPartsPrice(int newCharacterId, int newAmount)
	{
		int index = 17;
		CharacterPartsPriceComponent characterPartsPriceComponent = CreateComponent<CharacterPartsPriceComponent>(index);
		characterPartsPriceComponent.CharacterId = newCharacterId;
		characterPartsPriceComponent.Amount = newAmount;
		ReplaceComponent(index, characterPartsPriceComponent);
	}

	public void RemoveCharacterPartsPrice()
	{
		RemoveComponent(17);
	}

	public void AddCharacterStaticBalancingData(Dictionary<int, CharactersStaticBalancingData> newMap)
	{
		int index = 18;
		CharacterStaticBalancingDataComponent characterStaticBalancingDataComponent = CreateComponent<CharacterStaticBalancingDataComponent>(index);
		characterStaticBalancingDataComponent.Map = newMap;
		AddComponent(index, characterStaticBalancingDataComponent);
	}

	public void ReplaceCharacterStaticBalancingData(Dictionary<int, CharactersStaticBalancingData> newMap)
	{
		int index = 18;
		CharacterStaticBalancingDataComponent characterStaticBalancingDataComponent = CreateComponent<CharacterStaticBalancingDataComponent>(index);
		characterStaticBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, characterStaticBalancingDataComponent);
	}

	public void RemoveCharacterStaticBalancingData()
	{
		RemoveComponent(18);
	}

	public void AddCharacterUpgradesBalancingData(Dictionary<int, List<CharactersUpgradesBalancingData>> newMap)
	{
		int index = 19;
		CharacterUpgradesBalancingDataComponent characterUpgradesBalancingDataComponent = CreateComponent<CharacterUpgradesBalancingDataComponent>(index);
		characterUpgradesBalancingDataComponent.Map = newMap;
		AddComponent(index, characterUpgradesBalancingDataComponent);
	}

	public void ReplaceCharacterUpgradesBalancingData(Dictionary<int, List<CharactersUpgradesBalancingData>> newMap)
	{
		int index = 19;
		CharacterUpgradesBalancingDataComponent characterUpgradesBalancingDataComponent = CreateComponent<CharacterUpgradesBalancingDataComponent>(index);
		characterUpgradesBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, characterUpgradesBalancingDataComponent);
	}

	public void RemoveCharacterUpgradesBalancingData()
	{
		RemoveComponent(19);
	}

	public void AddCloseScreenAction(ScreenId newScreenId, float newDelay)
	{
		int index = 20;
		CloseScreenActionComponent closeScreenActionComponent = CreateComponent<CloseScreenActionComponent>(index);
		closeScreenActionComponent.screenId = newScreenId;
		closeScreenActionComponent.delay = newDelay;
		AddComponent(index, closeScreenActionComponent);
	}

	public void ReplaceCloseScreenAction(ScreenId newScreenId, float newDelay)
	{
		int index = 20;
		CloseScreenActionComponent closeScreenActionComponent = CreateComponent<CloseScreenActionComponent>(index);
		closeScreenActionComponent.screenId = newScreenId;
		closeScreenActionComponent.delay = newDelay;
		ReplaceComponent(index, closeScreenActionComponent);
	}

	public void RemoveCloseScreenAction()
	{
		RemoveComponent(20);
	}

	public void AddCompanionPartsPrice(int newCompanionId, int newAmount)
	{
		int index = 21;
		CompanionPartsPriceComponent companionPartsPriceComponent = CreateComponent<CompanionPartsPriceComponent>(index);
		companionPartsPriceComponent.CompanionId = newCompanionId;
		companionPartsPriceComponent.Amount = newAmount;
		AddComponent(index, companionPartsPriceComponent);
	}

	public void ReplaceCompanionPartsPrice(int newCompanionId, int newAmount)
	{
		int index = 21;
		CompanionPartsPriceComponent companionPartsPriceComponent = CreateComponent<CompanionPartsPriceComponent>(index);
		companionPartsPriceComponent.CompanionId = newCompanionId;
		companionPartsPriceComponent.Amount = newAmount;
		ReplaceComponent(index, companionPartsPriceComponent);
	}

	public void RemoveCompanionPartsPrice()
	{
		RemoveComponent(21);
	}

	public void AddCompanionsStaticBalancingData(Dictionary<int, CompanionsStaticBalancingData> newMap)
	{
		int index = 22;
		CompanionsStaticBalancingDataComponent companionsStaticBalancingDataComponent = CreateComponent<CompanionsStaticBalancingDataComponent>(index);
		companionsStaticBalancingDataComponent.Map = newMap;
		AddComponent(index, companionsStaticBalancingDataComponent);
	}

	public void ReplaceCompanionsStaticBalancingData(Dictionary<int, CompanionsStaticBalancingData> newMap)
	{
		int index = 22;
		CompanionsStaticBalancingDataComponent companionsStaticBalancingDataComponent = CreateComponent<CompanionsStaticBalancingDataComponent>(index);
		companionsStaticBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, companionsStaticBalancingDataComponent);
	}

	public void RemoveCompanionsStaticBalancingData()
	{
		RemoveComponent(22);
	}

	public void AddCompanionsUpgradesBalancingData(Dictionary<int, List<CompanionsUpgradesBalancingData>> newMap)
	{
		int index = 23;
		CompanionsUpgradesBalancingDataComponent companionsUpgradesBalancingDataComponent = CreateComponent<CompanionsUpgradesBalancingDataComponent>(index);
		companionsUpgradesBalancingDataComponent.Map = newMap;
		AddComponent(index, companionsUpgradesBalancingDataComponent);
	}

	public void ReplaceCompanionsUpgradesBalancingData(Dictionary<int, List<CompanionsUpgradesBalancingData>> newMap)
	{
		int index = 23;
		CompanionsUpgradesBalancingDataComponent companionsUpgradesBalancingDataComponent = CreateComponent<CompanionsUpgradesBalancingDataComponent>(index);
		companionsUpgradesBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, companionsUpgradesBalancingDataComponent);
	}

	public void RemoveCompanionsUpgradesBalancingData()
	{
		RemoveComponent(23);
	}

	public void AddCurrentRealTime(DateTime newCurrentTime)
	{
		int index = 24;
		CurrentRealTimeComponent currentRealTimeComponent = CreateComponent<CurrentRealTimeComponent>(index);
		currentRealTimeComponent.CurrentTime = newCurrentTime;
		AddComponent(index, currentRealTimeComponent);
	}

	public void ReplaceCurrentRealTime(DateTime newCurrentTime)
	{
		int index = 24;
		CurrentRealTimeComponent currentRealTimeComponent = CreateComponent<CurrentRealTimeComponent>(index);
		currentRealTimeComponent.CurrentTime = newCurrentTime;
		ReplaceComponent(index, currentRealTimeComponent);
	}

	public void RemoveCurrentRealTime()
	{
		RemoveComponent(24);
	}

	public void AddCurrentTimeListener(ICurrentTimeListener newListener)
	{
		int index = 25;
		CurrentTimeListenerComponent currentTimeListenerComponent = CreateComponent<CurrentTimeListenerComponent>(index);
		currentTimeListenerComponent.listener = newListener;
		AddComponent(index, currentTimeListenerComponent);
	}

	public void ReplaceCurrentTimeListener(ICurrentTimeListener newListener)
	{
		int index = 25;
		CurrentTimeListenerComponent currentTimeListenerComponent = CreateComponent<CurrentTimeListenerComponent>(index);
		currentTimeListenerComponent.listener = newListener;
		ReplaceComponent(index, currentTimeListenerComponent);
	}

	public void RemoveCurrentTimeListener()
	{
		RemoveComponent(25);
	}

	public void AddDailyMissionChestStateUpdate(bool newIsClaimed)
	{
		int index = 26;
		DailyMissionChestStateUpdateComponent dailyMissionChestStateUpdateComponent = CreateComponent<DailyMissionChestStateUpdateComponent>(index);
		dailyMissionChestStateUpdateComponent.IsClaimed = newIsClaimed;
		AddComponent(index, dailyMissionChestStateUpdateComponent);
	}

	public void ReplaceDailyMissionChestStateUpdate(bool newIsClaimed)
	{
		int index = 26;
		DailyMissionChestStateUpdateComponent dailyMissionChestStateUpdateComponent = CreateComponent<DailyMissionChestStateUpdateComponent>(index);
		dailyMissionChestStateUpdateComponent.IsClaimed = newIsClaimed;
		ReplaceComponent(index, dailyMissionChestStateUpdateComponent);
	}

	public void RemoveDailyMissionChestStateUpdate()
	{
		RemoveComponent(26);
	}

	public void AddDailyMissionClaimUpdate(DailyMissionGroup newGroup, bool newIsClaimed)
	{
		int index = 27;
		DailyMissionClaimUpdateComponent dailyMissionClaimUpdateComponent = CreateComponent<DailyMissionClaimUpdateComponent>(index);
		dailyMissionClaimUpdateComponent.Group = newGroup;
		dailyMissionClaimUpdateComponent.IsClaimed = newIsClaimed;
		AddComponent(index, dailyMissionClaimUpdateComponent);
	}

	public void ReplaceDailyMissionClaimUpdate(DailyMissionGroup newGroup, bool newIsClaimed)
	{
		int index = 27;
		DailyMissionClaimUpdateComponent dailyMissionClaimUpdateComponent = CreateComponent<DailyMissionClaimUpdateComponent>(index);
		dailyMissionClaimUpdateComponent.Group = newGroup;
		dailyMissionClaimUpdateComponent.IsClaimed = newIsClaimed;
		ReplaceComponent(index, dailyMissionClaimUpdateComponent);
	}

	public void RemoveDailyMissionClaimUpdate()
	{
		RemoveComponent(27);
	}

	public void AddDailyMissionProgressUpdate(DailyMissionGroup newGroup, int newMissionProgress)
	{
		int index = 28;
		DailyMissionProgressUpdateComponent dailyMissionProgressUpdateComponent = CreateComponent<DailyMissionProgressUpdateComponent>(index);
		dailyMissionProgressUpdateComponent.Group = newGroup;
		dailyMissionProgressUpdateComponent.MissionProgress = newMissionProgress;
		AddComponent(index, dailyMissionProgressUpdateComponent);
	}

	public void ReplaceDailyMissionProgressUpdate(DailyMissionGroup newGroup, int newMissionProgress)
	{
		int index = 28;
		DailyMissionProgressUpdateComponent dailyMissionProgressUpdateComponent = CreateComponent<DailyMissionProgressUpdateComponent>(index);
		dailyMissionProgressUpdateComponent.Group = newGroup;
		dailyMissionProgressUpdateComponent.MissionProgress = newMissionProgress;
		ReplaceComponent(index, dailyMissionProgressUpdateComponent);
	}

	public void RemoveDailyMissionProgressUpdate()
	{
		RemoveComponent(28);
	}

	public void AddDailyMissionsBalancingData(Dictionary<int, DailyMissionsBalancingData> newMap)
	{
		int index = 29;
		DailyMissionsBalancingDataComponent dailyMissionsBalancingDataComponent = CreateComponent<DailyMissionsBalancingDataComponent>(index);
		dailyMissionsBalancingDataComponent.Map = newMap;
		AddComponent(index, dailyMissionsBalancingDataComponent);
	}

	public void ReplaceDailyMissionsBalancingData(Dictionary<int, DailyMissionsBalancingData> newMap)
	{
		int index = 29;
		DailyMissionsBalancingDataComponent dailyMissionsBalancingDataComponent = CreateComponent<DailyMissionsBalancingDataComponent>(index);
		dailyMissionsBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, dailyMissionsBalancingDataComponent);
	}

	public void RemoveDailyMissionsBalancingData()
	{
		RemoveComponent(29);
	}

	public void AddDailyMissionsCollection(DailyMissionsCollection newCollection)
	{
		int index = 30;
		DailyMissionsCollectionComponent dailyMissionsCollectionComponent = CreateComponent<DailyMissionsCollectionComponent>(index);
		dailyMissionsCollectionComponent.Collection = newCollection;
		AddComponent(index, dailyMissionsCollectionComponent);
	}

	public void ReplaceDailyMissionsCollection(DailyMissionsCollection newCollection)
	{
		int index = 30;
		DailyMissionsCollectionComponent dailyMissionsCollectionComponent = CreateComponent<DailyMissionsCollectionComponent>(index);
		dailyMissionsCollectionComponent.Collection = newCollection;
		ReplaceComponent(index, dailyMissionsCollectionComponent);
	}

	public void RemoveDailyMissionsCollection()
	{
		RemoveComponent(30);
	}

	public void AddDateTime(DateTime newValue)
	{
		int index = 31;
		DateTimeComponent dateTimeComponent = CreateComponent<DateTimeComponent>(index);
		dateTimeComponent.Value = newValue;
		AddComponent(index, dateTimeComponent);
	}

	public void ReplaceDateTime(DateTime newValue)
	{
		int index = 31;
		DateTimeComponent dateTimeComponent = CreateComponent<DateTimeComponent>(index);
		dateTimeComponent.Value = newValue;
		ReplaceComponent(index, dateTimeComponent);
	}

	public void RemoveDateTime()
	{
		RemoveComponent(31);
	}

	public void AddDisplayLockLayerAction(bool newLayerTransparent)
	{
		int index = 33;
		DisplayLockLayerActionComponent displayLockLayerActionComponent = CreateComponent<DisplayLockLayerActionComponent>(index);
		displayLockLayerActionComponent.LayerTransparent = newLayerTransparent;
		AddComponent(index, displayLockLayerActionComponent);
	}

	public void ReplaceDisplayLockLayerAction(bool newLayerTransparent)
	{
		int index = 33;
		DisplayLockLayerActionComponent displayLockLayerActionComponent = CreateComponent<DisplayLockLayerActionComponent>(index);
		displayLockLayerActionComponent.LayerTransparent = newLayerTransparent;
		ReplaceComponent(index, displayLockLayerActionComponent);
	}

	public void RemoveDisplayLockLayerAction()
	{
		RemoveComponent(33);
	}

	public void AddEventAchievementComplete(string newAchievementId)
	{
		int index = 35;
		EventAchievementCompleteComponent eventAchievementCompleteComponent = CreateComponent<EventAchievementCompleteComponent>(index);
		eventAchievementCompleteComponent.achievementId = newAchievementId;
		AddComponent(index, eventAchievementCompleteComponent);
	}

	public void ReplaceEventAchievementComplete(string newAchievementId)
	{
		int index = 35;
		EventAchievementCompleteComponent eventAchievementCompleteComponent = CreateComponent<EventAchievementCompleteComponent>(index);
		eventAchievementCompleteComponent.achievementId = newAchievementId;
		ReplaceComponent(index, eventAchievementCompleteComponent);
	}

	public void RemoveEventAchievementComplete()
	{
		RemoveComponent(35);
	}

	public void AddEventCustom(string newEid, string newGid, string newP1, string newP2, double? newP3, double? newP4, string newP5, double? newRes, string newJsonData, bool newIsOnDemand)
	{
		int index = 36;
		EventCustomComponent eventCustomComponent = CreateComponent<EventCustomComponent>(index);
		eventCustomComponent.eid = newEid;
		eventCustomComponent.gid = newGid;
		eventCustomComponent.p1 = newP1;
		eventCustomComponent.p2 = newP2;
		eventCustomComponent.p3 = newP3;
		eventCustomComponent.p4 = newP4;
		eventCustomComponent.p5 = newP5;
		eventCustomComponent.res = newRes;
		eventCustomComponent.jsonData = newJsonData;
		eventCustomComponent.isOnDemand = newIsOnDemand;
		AddComponent(index, eventCustomComponent);
	}

	public void ReplaceEventCustom(string newEid, string newGid, string newP1, string newP2, double? newP3, double? newP4, string newP5, double? newRes, string newJsonData, bool newIsOnDemand)
	{
		int index = 36;
		EventCustomComponent eventCustomComponent = CreateComponent<EventCustomComponent>(index);
		eventCustomComponent.eid = newEid;
		eventCustomComponent.gid = newGid;
		eventCustomComponent.p1 = newP1;
		eventCustomComponent.p2 = newP2;
		eventCustomComponent.p3 = newP3;
		eventCustomComponent.p4 = newP4;
		eventCustomComponent.p5 = newP5;
		eventCustomComponent.res = newRes;
		eventCustomComponent.jsonData = newJsonData;
		eventCustomComponent.isOnDemand = newIsOnDemand;
		ReplaceComponent(index, eventCustomComponent);
	}

	public void RemoveEventCustom()
	{
		RemoveComponent(36);
	}

	public void AddEventCustomSend(EventCustomComponent newComponent)
	{
		int index = 37;
		EventCustomSendComponent eventCustomSendComponent = CreateComponent<EventCustomSendComponent>(index);
		eventCustomSendComponent.component = newComponent;
		AddComponent(index, eventCustomSendComponent);
	}

	public void ReplaceEventCustomSend(EventCustomComponent newComponent)
	{
		int index = 37;
		EventCustomSendComponent eventCustomSendComponent = CreateComponent<EventCustomSendComponent>(index);
		eventCustomSendComponent.component = newComponent;
		ReplaceComponent(index, eventCustomSendComponent);
	}

	public void RemoveEventCustomSend()
	{
		RemoveComponent(37);
	}

	public void AddEventEarnVirtualCurrency(string newCurrencyId, string newGameplayId, int newCurrencyChange, int newCurrencyBalance)
	{
		int index = 38;
		EventEarnVirtualCurrencyComponent eventEarnVirtualCurrencyComponent = CreateComponent<EventEarnVirtualCurrencyComponent>(index);
		eventEarnVirtualCurrencyComponent.currencyId = newCurrencyId;
		eventEarnVirtualCurrencyComponent.gameplayId = newGameplayId;
		eventEarnVirtualCurrencyComponent.currencyChange = newCurrencyChange;
		eventEarnVirtualCurrencyComponent.currencyBalance = newCurrencyBalance;
		AddComponent(index, eventEarnVirtualCurrencyComponent);
	}

	public void ReplaceEventEarnVirtualCurrency(string newCurrencyId, string newGameplayId, int newCurrencyChange, int newCurrencyBalance)
	{
		int index = 38;
		EventEarnVirtualCurrencyComponent eventEarnVirtualCurrencyComponent = CreateComponent<EventEarnVirtualCurrencyComponent>(index);
		eventEarnVirtualCurrencyComponent.currencyId = newCurrencyId;
		eventEarnVirtualCurrencyComponent.gameplayId = newGameplayId;
		eventEarnVirtualCurrencyComponent.currencyChange = newCurrencyChange;
		eventEarnVirtualCurrencyComponent.currencyBalance = newCurrencyBalance;
		ReplaceComponent(index, eventEarnVirtualCurrencyComponent);
	}

	public void RemoveEventEarnVirtualCurrency()
	{
		RemoveComponent(38);
	}

	public void AddEventInAppPurchase(string newProductId, string newProductName, string newCurrency, int newQuantity, int newValue)
	{
		int index = 39;
		EventInAppPurchaseComponent eventInAppPurchaseComponent = CreateComponent<EventInAppPurchaseComponent>(index);
		eventInAppPurchaseComponent.productId = newProductId;
		eventInAppPurchaseComponent.productName = newProductName;
		eventInAppPurchaseComponent.currency = newCurrency;
		eventInAppPurchaseComponent.quantity = newQuantity;
		eventInAppPurchaseComponent.value = newValue;
		AddComponent(index, eventInAppPurchaseComponent);
	}

	public void ReplaceEventInAppPurchase(string newProductId, string newProductName, string newCurrency, int newQuantity, int newValue)
	{
		int index = 39;
		EventInAppPurchaseComponent eventInAppPurchaseComponent = CreateComponent<EventInAppPurchaseComponent>(index);
		eventInAppPurchaseComponent.productId = newProductId;
		eventInAppPurchaseComponent.productName = newProductName;
		eventInAppPurchaseComponent.currency = newCurrency;
		eventInAppPurchaseComponent.quantity = newQuantity;
		eventInAppPurchaseComponent.value = newValue;
		ReplaceComponent(index, eventInAppPurchaseComponent);
	}

	public void RemoveEventInAppPurchase()
	{
		RemoveComponent(39);
	}

	public void AddEventLevelComplete(string newLevelId)
	{
		int index = 40;
		EventLevelCompleteComponent eventLevelCompleteComponent = CreateComponent<EventLevelCompleteComponent>(index);
		eventLevelCompleteComponent.levelId = newLevelId;
		AddComponent(index, eventLevelCompleteComponent);
	}

	public void ReplaceEventLevelComplete(string newLevelId)
	{
		int index = 40;
		EventLevelCompleteComponent eventLevelCompleteComponent = CreateComponent<EventLevelCompleteComponent>(index);
		eventLevelCompleteComponent.levelId = newLevelId;
		ReplaceComponent(index, eventLevelCompleteComponent);
	}

	public void RemoveEventLevelComplete()
	{
		RemoveComponent(40);
	}

	public void AddEventLevelUp(string newLevelId)
	{
		int index = 41;
		EventLevelUpComponent eventLevelUpComponent = CreateComponent<EventLevelUpComponent>(index);
		eventLevelUpComponent.levelId = newLevelId;
		AddComponent(index, eventLevelUpComponent);
	}

	public void ReplaceEventLevelUp(string newLevelId)
	{
		int index = 41;
		EventLevelUpComponent eventLevelUpComponent = CreateComponent<EventLevelUpComponent>(index);
		eventLevelUpComponent.levelId = newLevelId;
		ReplaceComponent(index, eventLevelUpComponent);
	}

	public void RemoveEventLevelUp()
	{
		RemoveComponent(41);
	}

	public void AddEventSelectContent(string newContentId)
	{
		int index = 42;
		EventSelectContentComponent eventSelectContentComponent = CreateComponent<EventSelectContentComponent>(index);
		eventSelectContentComponent.contentId = newContentId;
		AddComponent(index, eventSelectContentComponent);
	}

	public void ReplaceEventSelectContent(string newContentId)
	{
		int index = 42;
		EventSelectContentComponent eventSelectContentComponent = CreateComponent<EventSelectContentComponent>(index);
		eventSelectContentComponent.contentId = newContentId;
		ReplaceComponent(index, eventSelectContentComponent);
	}

	public void RemoveEventSelectContent()
	{
		RemoveComponent(42);
	}

	public void AddEventSpendVirtualCurrency(string newCurrencyId, string newGameplayId, int newCurrencyChange, int newCurrencyBalance)
	{
		int index = 43;
		EventSpendVirtualCurrencyComponent eventSpendVirtualCurrencyComponent = CreateComponent<EventSpendVirtualCurrencyComponent>(index);
		eventSpendVirtualCurrencyComponent.currencyId = newCurrencyId;
		eventSpendVirtualCurrencyComponent.gameplayId = newGameplayId;
		eventSpendVirtualCurrencyComponent.currencyChange = newCurrencyChange;
		eventSpendVirtualCurrencyComponent.currencyBalance = newCurrencyBalance;
		AddComponent(index, eventSpendVirtualCurrencyComponent);
	}

	public void ReplaceEventSpendVirtualCurrency(string newCurrencyId, string newGameplayId, int newCurrencyChange, int newCurrencyBalance)
	{
		int index = 43;
		EventSpendVirtualCurrencyComponent eventSpendVirtualCurrencyComponent = CreateComponent<EventSpendVirtualCurrencyComponent>(index);
		eventSpendVirtualCurrencyComponent.currencyId = newCurrencyId;
		eventSpendVirtualCurrencyComponent.gameplayId = newGameplayId;
		eventSpendVirtualCurrencyComponent.currencyChange = newCurrencyChange;
		eventSpendVirtualCurrencyComponent.currencyBalance = newCurrencyBalance;
		ReplaceComponent(index, eventSpendVirtualCurrencyComponent);
	}

	public void RemoveEventSpendVirtualCurrency()
	{
		RemoveComponent(43);
	}

	public void AddEventTutorialBegin(string newStepId)
	{
		int index = 44;
		EventTutorialBeginComponent eventTutorialBeginComponent = CreateComponent<EventTutorialBeginComponent>(index);
		eventTutorialBeginComponent.stepId = newStepId;
		AddComponent(index, eventTutorialBeginComponent);
	}

	public void ReplaceEventTutorialBegin(string newStepId)
	{
		int index = 44;
		EventTutorialBeginComponent eventTutorialBeginComponent = CreateComponent<EventTutorialBeginComponent>(index);
		eventTutorialBeginComponent.stepId = newStepId;
		ReplaceComponent(index, eventTutorialBeginComponent);
	}

	public void RemoveEventTutorialBegin()
	{
		RemoveComponent(44);
	}

	public void AddEventTutorialComplete(string newStepId)
	{
		int index = 45;
		EventTutorialCompleteComponent eventTutorialCompleteComponent = CreateComponent<EventTutorialCompleteComponent>(index);
		eventTutorialCompleteComponent.stepId = newStepId;
		AddComponent(index, eventTutorialCompleteComponent);
	}

	public void ReplaceEventTutorialComplete(string newStepId)
	{
		int index = 45;
		EventTutorialCompleteComponent eventTutorialCompleteComponent = CreateComponent<EventTutorialCompleteComponent>(index);
		eventTutorialCompleteComponent.stepId = newStepId;
		ReplaceComponent(index, eventTutorialCompleteComponent);
	}

	public void RemoveEventTutorialComplete()
	{
		RemoveComponent(45);
	}

	public void AddFindTargetAction(string newTargetId)
	{
		int index = 46;
		FindTargetActionComponent findTargetActionComponent = CreateComponent<FindTargetActionComponent>(index);
		findTargetActionComponent.TargetId = newTargetId;
		AddComponent(index, findTargetActionComponent);
	}

	public void ReplaceFindTargetAction(string newTargetId)
	{
		int index = 46;
		FindTargetActionComponent findTargetActionComponent = CreateComponent<FindTargetActionComponent>(index);
		findTargetActionComponent.TargetId = newTargetId;
		ReplaceComponent(index, findTargetActionComponent);
	}

	public void RemoveFindTargetAction()
	{
		RemoveComponent(46);
	}

	public void AddFreeChest(bool newIsLocked, DateTime newUnlockTime)
	{
		int index = 47;
		FreeChest freeChest = CreateComponent<FreeChest>(index);
		freeChest.IsLocked = newIsLocked;
		freeChest.UnlockTime = newUnlockTime;
		AddComponent(index, freeChest);
	}

	public void ReplaceFreeChest(bool newIsLocked, DateTime newUnlockTime)
	{
		int index = 47;
		FreeChest freeChest = CreateComponent<FreeChest>(index);
		freeChest.IsLocked = newIsLocked;
		freeChest.UnlockTime = newUnlockTime;
		ReplaceComponent(index, freeChest);
	}

	public void RemoveFreeChest()
	{
		RemoveComponent(47);
	}

	public void AddGatchaBalancingData(Dictionary<int, ChestData> newMap)
	{
		int index = 50;
		GatchaBalancingDataComponent gatchaBalancingDataComponent = CreateComponent<GatchaBalancingDataComponent>(index);
		gatchaBalancingDataComponent.Map = newMap;
		AddComponent(index, gatchaBalancingDataComponent);
	}

	public void ReplaceGatchaBalancingData(Dictionary<int, ChestData> newMap)
	{
		int index = 50;
		GatchaBalancingDataComponent gatchaBalancingDataComponent = CreateComponent<GatchaBalancingDataComponent>(index);
		gatchaBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, gatchaBalancingDataComponent);
	}

	public void RemoveGatchaBalancingData()
	{
		RemoveComponent(50);
	}

	public void AddGoBackAction(ScreenId newExpectedScreenId, float newDelay)
	{
		int index = 51;
		GoBackActionComponent goBackActionComponent = CreateComponent<GoBackActionComponent>(index);
		goBackActionComponent.ExpectedScreenId = newExpectedScreenId;
		goBackActionComponent.Delay = newDelay;
		AddComponent(index, goBackActionComponent);
	}

	public void ReplaceGoBackAction(ScreenId newExpectedScreenId, float newDelay)
	{
		int index = 51;
		GoBackActionComponent goBackActionComponent = CreateComponent<GoBackActionComponent>(index);
		goBackActionComponent.ExpectedScreenId = newExpectedScreenId;
		goBackActionComponent.Delay = newDelay;
		ReplaceComponent(index, goBackActionComponent);
	}

	public void RemoveGoBackAction()
	{
		RemoveComponent(51);
	}

	public void AddGrantPlayerExperience(int newExperienceAmount)
	{
		int index = 52;
		GrantPlayerExperienceComponent grantPlayerExperienceComponent = CreateComponent<GrantPlayerExperienceComponent>(index);
		grantPlayerExperienceComponent.ExperienceAmount = newExperienceAmount;
		AddComponent(index, grantPlayerExperienceComponent);
	}

	public void ReplaceGrantPlayerExperience(int newExperienceAmount)
	{
		int index = 52;
		GrantPlayerExperienceComponent grantPlayerExperienceComponent = CreateComponent<GrantPlayerExperienceComponent>(index);
		grantPlayerExperienceComponent.ExperienceAmount = newExperienceAmount;
		ReplaceComponent(index, grantPlayerExperienceComponent);
	}

	public void RemoveGrantPlayerExperience()
	{
		RemoveComponent(52);
	}

	public void AddHardCurrencyPrice(int newAmount)
	{
		int index = 53;
		HardCurrencyPriceComponent hardCurrencyPriceComponent = CreateComponent<HardCurrencyPriceComponent>(index);
		hardCurrencyPriceComponent.Amount = newAmount;
		AddComponent(index, hardCurrencyPriceComponent);
	}

	public void ReplaceHardCurrencyPrice(int newAmount)
	{
		int index = 53;
		HardCurrencyPriceComponent hardCurrencyPriceComponent = CreateComponent<HardCurrencyPriceComponent>(index);
		hardCurrencyPriceComponent.Amount = newAmount;
		ReplaceComponent(index, hardCurrencyPriceComponent);
	}

	public void RemoveHardCurrencyPrice()
	{
		RemoveComponent(53);
	}

	public void AddHiglightTargetAction(HighlightOptions newOptions)
	{
		int index = 54;
		HiglightTargetActionComponent higlightTargetActionComponent = CreateComponent<HiglightTargetActionComponent>(index);
		higlightTargetActionComponent.Options = newOptions;
		AddComponent(index, higlightTargetActionComponent);
	}

	public void ReplaceHiglightTargetAction(HighlightOptions newOptions)
	{
		int index = 54;
		HiglightTargetActionComponent higlightTargetActionComponent = CreateComponent<HiglightTargetActionComponent>(index);
		higlightTargetActionComponent.Options = newOptions;
		ReplaceComponent(index, higlightTargetActionComponent);
	}

	public void RemoveHiglightTargetAction()
	{
		RemoveComponent(54);
	}

	public void AddInitConfigData(Dictionary<EnumSettings, int> newMap)
	{
		int index = 55;
		InitConfigDataComponent initConfigDataComponent = CreateComponent<InitConfigDataComponent>(index);
		initConfigDataComponent.Map = newMap;
		AddComponent(index, initConfigDataComponent);
	}

	public void ReplaceInitConfigData(Dictionary<EnumSettings, int> newMap)
	{
		int index = 55;
		InitConfigDataComponent initConfigDataComponent = CreateComponent<InitConfigDataComponent>(index);
		initConfigDataComponent.Map = newMap;
		ReplaceComponent(index, initConfigDataComponent);
	}

	public void RemoveInitConfigData()
	{
		RemoveComponent(55);
	}

	public void AddInitialStartSceneDisplayed(InitialStartScene newInitialStartScene)
	{
		int index = 56;
		InitialStartSceneDisplayed initialStartSceneDisplayed = CreateComponent<InitialStartSceneDisplayed>(index);
		initialStartSceneDisplayed.initialStartScene = newInitialStartScene;
		AddComponent(index, initialStartSceneDisplayed);
	}

	public void ReplaceInitialStartSceneDisplayed(InitialStartScene newInitialStartScene)
	{
		int index = 56;
		InitialStartSceneDisplayed initialStartSceneDisplayed = CreateComponent<InitialStartSceneDisplayed>(index);
		initialStartSceneDisplayed.initialStartScene = newInitialStartScene;
		ReplaceComponent(index, initialStartSceneDisplayed);
	}

	public void RemoveInitialStartSceneDisplayed()
	{
		RemoveComponent(56);
	}

	public void AddInteger(int newAmount)
	{
		int index = 58;
		IntegerComponent integerComponent = CreateComponent<IntegerComponent>(index);
		integerComponent.Amount = newAmount;
		AddComponent(index, integerComponent);
	}

	public void ReplaceInteger(int newAmount)
	{
		int index = 58;
		IntegerComponent integerComponent = CreateComponent<IntegerComponent>(index);
		integerComponent.Amount = newAmount;
		ReplaceComponent(index, integerComponent);
	}

	public void RemoveInteger()
	{
		RemoveComponent(58);
	}

	public void AddLabel(string newLabel)
	{
		int index = 59;
		LabelComponent labelComponent = CreateComponent<LabelComponent>(index);
		labelComponent.Label = newLabel;
		AddComponent(index, labelComponent);
	}

	public void ReplaceLabel(string newLabel)
	{
		int index = 59;
		LabelComponent labelComponent = CreateComponent<LabelComponent>(index);
		labelComponent.Label = newLabel;
		ReplaceComponent(index, labelComponent);
	}

	public void RemoveLabel()
	{
		RemoveComponent(59);
	}

	public void AddLastLoggedScreen(ScreenId newScreenId)
	{
		int index = 60;
		LastLoggedScreenComponent lastLoggedScreenComponent = CreateComponent<LastLoggedScreenComponent>(index);
		lastLoggedScreenComponent.ScreenId = newScreenId;
		AddComponent(index, lastLoggedScreenComponent);
	}

	public void ReplaceLastLoggedScreen(ScreenId newScreenId)
	{
		int index = 60;
		LastLoggedScreenComponent lastLoggedScreenComponent = CreateComponent<LastLoggedScreenComponent>(index);
		lastLoggedScreenComponent.ScreenId = newScreenId;
		ReplaceComponent(index, lastLoggedScreenComponent);
	}

	public void RemoveLastLoggedScreen()
	{
		RemoveComponent(60);
	}

	public void AddLastSceneFramesCount(int newFramesCount)
	{
		int index = 61;
		LastSceneFramesCountComponent lastSceneFramesCountComponent = CreateComponent<LastSceneFramesCountComponent>(index);
		lastSceneFramesCountComponent.FramesCount = newFramesCount;
		AddComponent(index, lastSceneFramesCountComponent);
	}

	public void ReplaceLastSceneFramesCount(int newFramesCount)
	{
		int index = 61;
		LastSceneFramesCountComponent lastSceneFramesCountComponent = CreateComponent<LastSceneFramesCountComponent>(index);
		lastSceneFramesCountComponent.FramesCount = newFramesCount;
		ReplaceComponent(index, lastSceneFramesCountComponent);
	}

	public void RemoveLastSceneFramesCount()
	{
		RemoveComponent(61);
	}

	public void AddLastSceneTransitionTime(DateTime newTransitionTime)
	{
		int index = 62;
		LastSceneTransitionTimeComponent lastSceneTransitionTimeComponent = CreateComponent<LastSceneTransitionTimeComponent>(index);
		lastSceneTransitionTimeComponent.TransitionTime = newTransitionTime;
		AddComponent(index, lastSceneTransitionTimeComponent);
	}

	public void ReplaceLastSceneTransitionTime(DateTime newTransitionTime)
	{
		int index = 62;
		LastSceneTransitionTimeComponent lastSceneTransitionTimeComponent = CreateComponent<LastSceneTransitionTimeComponent>(index);
		lastSceneTransitionTimeComponent.TransitionTime = newTransitionTime;
		ReplaceComponent(index, lastSceneTransitionTimeComponent);
	}

	public void RemoveLastSceneTransitionTime()
	{
		RemoveComponent(62);
	}

	public void AddLevelCompleted(int newLevelNumber, int newStars, int newResult, int newNewStars)
	{
		int index = 63;
		LevelCompletedComponent levelCompletedComponent = CreateComponent<LevelCompletedComponent>(index);
		levelCompletedComponent.LevelNumber = newLevelNumber;
		levelCompletedComponent.Stars = newStars;
		levelCompletedComponent.Result = newResult;
		levelCompletedComponent.NewStars = newNewStars;
		AddComponent(index, levelCompletedComponent);
	}

	public void ReplaceLevelCompleted(int newLevelNumber, int newStars, int newResult, int newNewStars)
	{
		int index = 63;
		LevelCompletedComponent levelCompletedComponent = CreateComponent<LevelCompletedComponent>(index);
		levelCompletedComponent.LevelNumber = newLevelNumber;
		levelCompletedComponent.Stars = newStars;
		levelCompletedComponent.Result = newResult;
		levelCompletedComponent.NewStars = newNewStars;
		ReplaceComponent(index, levelCompletedComponent);
	}

	public void RemoveLevelCompleted()
	{
		RemoveComponent(63);
	}

	public void AddLocalUserState(UserStateData newStateData)
	{
		int index = 64;
		LocalUserStateComponent localUserStateComponent = CreateComponent<LocalUserStateComponent>(index);
		localUserStateComponent.StateData = newStateData;
		AddComponent(index, localUserStateComponent);
	}

	public void ReplaceLocalUserState(UserStateData newStateData)
	{
		int index = 64;
		LocalUserStateComponent localUserStateComponent = CreateComponent<LocalUserStateComponent>(index);
		localUserStateComponent.StateData = newStateData;
		ReplaceComponent(index, localUserStateComponent);
	}

	public void RemoveLocalUserState()
	{
		RemoveComponent(64);
	}

	public void AddLogO7SdkIapBuyRequest(string newProductId)
	{
		int index = 65;
		LogO7SdkIapBuyRequestComponent logO7SdkIapBuyRequestComponent = CreateComponent<LogO7SdkIapBuyRequestComponent>(index);
		logO7SdkIapBuyRequestComponent.productId = newProductId;
		AddComponent(index, logO7SdkIapBuyRequestComponent);
	}

	public void ReplaceLogO7SdkIapBuyRequest(string newProductId)
	{
		int index = 65;
		LogO7SdkIapBuyRequestComponent logO7SdkIapBuyRequestComponent = CreateComponent<LogO7SdkIapBuyRequestComponent>(index);
		logO7SdkIapBuyRequestComponent.productId = newProductId;
		ReplaceComponent(index, logO7SdkIapBuyRequestComponent);
	}

	public void RemoveLogO7SdkIapBuyRequest()
	{
		RemoveComponent(65);
	}

	public void AddMissionBalancingData(Dictionary<int, MissionsBalancingData> newMap)
	{
		int index = 66;
		MissionBalancingDataComponent missionBalancingDataComponent = CreateComponent<MissionBalancingDataComponent>(index);
		missionBalancingDataComponent.Map = newMap;
		AddComponent(index, missionBalancingDataComponent);
	}

	public void ReplaceMissionBalancingData(Dictionary<int, MissionsBalancingData> newMap)
	{
		int index = 66;
		MissionBalancingDataComponent missionBalancingDataComponent = CreateComponent<MissionBalancingDataComponent>(index);
		missionBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, missionBalancingDataComponent);
	}

	public void RemoveMissionBalancingData()
	{
		RemoveComponent(66);
	}

	public void AddMissionChestsBalancingData(Dictionary<int, MissionChestsBalancingData> newMap)
	{
		int index = 67;
		MissionChestsBalancingDataComponent missionChestsBalancingDataComponent = CreateComponent<MissionChestsBalancingDataComponent>(index);
		missionChestsBalancingDataComponent.Map = newMap;
		AddComponent(index, missionChestsBalancingDataComponent);
	}

	public void ReplaceMissionChestsBalancingData(Dictionary<int, MissionChestsBalancingData> newMap)
	{
		int index = 67;
		MissionChestsBalancingDataComponent missionChestsBalancingDataComponent = CreateComponent<MissionChestsBalancingDataComponent>(index);
		missionChestsBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, missionChestsBalancingDataComponent);
	}

	public void RemoveMissionChestsBalancingData()
	{
		RemoveComponent(67);
	}

	public void AddMuteMusic(bool newMute)
	{
		int index = 69;
		MuteMusicComponent muteMusicComponent = CreateComponent<MuteMusicComponent>(index);
		muteMusicComponent.Mute = newMute;
		AddComponent(index, muteMusicComponent);
	}

	public void ReplaceMuteMusic(bool newMute)
	{
		int index = 69;
		MuteMusicComponent muteMusicComponent = CreateComponent<MuteMusicComponent>(index);
		muteMusicComponent.Mute = newMute;
		ReplaceComponent(index, muteMusicComponent);
	}

	public void RemoveMuteMusic()
	{
		RemoveComponent(69);
	}

	public void AddMuteSound(bool newMute)
	{
		int index = 70;
		MuteSoundComponent muteSoundComponent = CreateComponent<MuteSoundComponent>(index);
		muteSoundComponent.Mute = newMute;
		AddComponent(index, muteSoundComponent);
	}

	public void ReplaceMuteSound(bool newMute)
	{
		int index = 70;
		MuteSoundComponent muteSoundComponent = CreateComponent<MuteSoundComponent>(index);
		muteSoundComponent.Mute = newMute;
		ReplaceComponent(index, muteSoundComponent);
	}

	public void RemoveMuteSound()
	{
		RemoveComponent(70);
	}

	public void AddMuteSoundSource(bool newMute, string newSourceId)
	{
		int index = 71;
		MuteSoundSourceComponent muteSoundSourceComponent = CreateComponent<MuteSoundSourceComponent>(index);
		muteSoundSourceComponent.Mute = newMute;
		muteSoundSourceComponent.SourceId = newSourceId;
		AddComponent(index, muteSoundSourceComponent);
	}

	public void ReplaceMuteSoundSource(bool newMute, string newSourceId)
	{
		int index = 71;
		MuteSoundSourceComponent muteSoundSourceComponent = CreateComponent<MuteSoundSourceComponent>(index);
		muteSoundSourceComponent.Mute = newMute;
		muteSoundSourceComponent.SourceId = newSourceId;
		ReplaceComponent(index, muteSoundSourceComponent);
	}

	public void RemoveMuteSoundSource()
	{
		RemoveComponent(71);
	}

	public void AddName(string newName)
	{
		int index = 72;
		NameComponent nameComponent = CreateComponent<NameComponent>(index);
		nameComponent.Name = newName;
		AddComponent(index, nameComponent);
	}

	public void ReplaceName(string newName)
	{
		int index = 72;
		NameComponent nameComponent = CreateComponent<NameComponent>(index);
		nameComponent.Name = newName;
		ReplaceComponent(index, nameComponent);
	}

	public void RemoveName()
	{
		RemoveComponent(72);
	}

	public void AddNewLevelUnlocked(int newLevelNumber)
	{
		int index = 73;
		NewLevelUnlockedComponent newLevelUnlockedComponent = CreateComponent<NewLevelUnlockedComponent>(index);
		newLevelUnlockedComponent.LevelNumber = newLevelNumber;
		AddComponent(index, newLevelUnlockedComponent);
	}

	public void ReplaceNewLevelUnlocked(int newLevelNumber)
	{
		int index = 73;
		NewLevelUnlockedComponent newLevelUnlockedComponent = CreateComponent<NewLevelUnlockedComponent>(index);
		newLevelUnlockedComponent.LevelNumber = newLevelNumber;
		ReplaceComponent(index, newLevelUnlockedComponent);
	}

	public void RemoveNewLevelUnlocked()
	{
		RemoveComponent(73);
	}

	public void AddO7SdkAgeGateManager(O7SDKGateManager newManager)
	{
		int index = 76;
		O7SdkAgeGateManagerComponent o7SdkAgeGateManagerComponent = CreateComponent<O7SdkAgeGateManagerComponent>(index);
		o7SdkAgeGateManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkAgeGateManagerComponent);
	}

	public void ReplaceO7SdkAgeGateManager(O7SDKGateManager newManager)
	{
		int index = 76;
		O7SdkAgeGateManagerComponent o7SdkAgeGateManagerComponent = CreateComponent<O7SdkAgeGateManagerComponent>(index);
		o7SdkAgeGateManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkAgeGateManagerComponent);
	}

	public void RemoveO7SdkAgeGateManager()
	{
		RemoveComponent(76);
	}

	public void AddO7SdkAgeGateSelected(int newYear)
	{
		int index = 77;
		O7SdkAgeGateSelectedComponent o7SdkAgeGateSelectedComponent = CreateComponent<O7SdkAgeGateSelectedComponent>(index);
		o7SdkAgeGateSelectedComponent.year = newYear;
		AddComponent(index, o7SdkAgeGateSelectedComponent);
	}

	public void ReplaceO7SdkAgeGateSelected(int newYear)
	{
		int index = 77;
		O7SdkAgeGateSelectedComponent o7SdkAgeGateSelectedComponent = CreateComponent<O7SdkAgeGateSelectedComponent>(index);
		o7SdkAgeGateSelectedComponent.year = newYear;
		ReplaceComponent(index, o7SdkAgeGateSelectedComponent);
	}

	public void RemoveO7SdkAgeGateSelected()
	{
		RemoveComponent(77);
	}

	public void AddO7SdkBalancingData(O7SdkBalancingData newData)
	{
		int index = 80;
		O7SdkBalancingDataComponent o7SdkBalancingDataComponent = CreateComponent<O7SdkBalancingDataComponent>(index);
		o7SdkBalancingDataComponent.Data = newData;
		AddComponent(index, o7SdkBalancingDataComponent);
	}

	public void ReplaceO7SdkBalancingData(O7SdkBalancingData newData)
	{
		int index = 80;
		O7SdkBalancingDataComponent o7SdkBalancingDataComponent = CreateComponent<O7SdkBalancingDataComponent>(index);
		o7SdkBalancingDataComponent.Data = newData;
		ReplaceComponent(index, o7SdkBalancingDataComponent);
	}

	public void RemoveO7SdkBalancingData()
	{
		RemoveComponent(80);
	}

	public void AddO7SdkBannerAdManager(O7ADBannerAdManager newManager)
	{
		int index = 82;
		O7SdkBannerAdManagerComponent o7SdkBannerAdManagerComponent = CreateComponent<O7SdkBannerAdManagerComponent>(index);
		o7SdkBannerAdManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkBannerAdManagerComponent);
	}

	public void ReplaceO7SdkBannerAdManager(O7ADBannerAdManager newManager)
	{
		int index = 82;
		O7SdkBannerAdManagerComponent o7SdkBannerAdManagerComponent = CreateComponent<O7SdkBannerAdManagerComponent>(index);
		o7SdkBannerAdManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkBannerAdManagerComponent);
	}

	public void RemoveO7SdkBannerAdManager()
	{
		RemoveComponent(82);
	}

	public void AddO7SdkConfigItems(BalancingDataOverrideConfig newData)
	{
		int index = 170;
		O7SdkConfigItemsComponent o7SdkConfigItemsComponent = CreateComponent<O7SdkConfigItemsComponent>(index);
		o7SdkConfigItemsComponent.Data = newData;
		AddComponent(index, o7SdkConfigItemsComponent);
	}

	public void ReplaceO7SdkConfigItems(BalancingDataOverrideConfig newData)
	{
		int index = 170;
		O7SdkConfigItemsComponent o7SdkConfigItemsComponent = CreateComponent<O7SdkConfigItemsComponent>(index);
		o7SdkConfigItemsComponent.Data = newData;
		ReplaceComponent(index, o7SdkConfigItemsComponent);
	}

	public void RemoveO7SdkConfigItems()
	{
		RemoveComponent(170);
	}

	public void AddO7SdkCountryCodes(List<string> newCodes)
	{
		int index = 86;
		O7SdkCountryCodes o7SdkCountryCodes = CreateComponent<O7SdkCountryCodes>(index);
		o7SdkCountryCodes.codes = newCodes;
		AddComponent(index, o7SdkCountryCodes);
	}

	public void ReplaceO7SdkCountryCodes(List<string> newCodes)
	{
		int index = 86;
		O7SdkCountryCodes o7SdkCountryCodes = CreateComponent<O7SdkCountryCodes>(index);
		o7SdkCountryCodes.codes = newCodes;
		ReplaceComponent(index, o7SdkCountryCodes);
	}

	public void RemoveO7SdkCountryCodes()
	{
		RemoveComponent(86);
	}

	public void AddO7SdkCrossPromo(bool newIsAutoNews, bool newIsManualNews)
	{
		int index = 87;
		O7SdkCrossPromoComponent o7SdkCrossPromoComponent = CreateComponent<O7SdkCrossPromoComponent>(index);
		o7SdkCrossPromoComponent.isAutoNews = newIsAutoNews;
		o7SdkCrossPromoComponent.isManualNews = newIsManualNews;
		AddComponent(index, o7SdkCrossPromoComponent);
	}

	public void ReplaceO7SdkCrossPromo(bool newIsAutoNews, bool newIsManualNews)
	{
		int index = 87;
		O7SdkCrossPromoComponent o7SdkCrossPromoComponent = CreateComponent<O7SdkCrossPromoComponent>(index);
		o7SdkCrossPromoComponent.isAutoNews = newIsAutoNews;
		o7SdkCrossPromoComponent.isManualNews = newIsManualNews;
		ReplaceComponent(index, o7SdkCrossPromoComponent);
	}

	public void RemoveO7SdkCrossPromo()
	{
		RemoveComponent(87);
	}

	public void AddO7SdkCrossPromoManager(O7SDKCrossPromoManager newManager)
	{
		int index = 88;
		O7SdkCrossPromoManagerComponent o7SdkCrossPromoManagerComponent = CreateComponent<O7SdkCrossPromoManagerComponent>(index);
		o7SdkCrossPromoManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkCrossPromoManagerComponent);
	}

	public void ReplaceO7SdkCrossPromoManager(O7SDKCrossPromoManager newManager)
	{
		int index = 88;
		O7SdkCrossPromoManagerComponent o7SdkCrossPromoManagerComponent = CreateComponent<O7SdkCrossPromoManagerComponent>(index);
		o7SdkCrossPromoManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkCrossPromoManagerComponent);
	}

	public void RemoveO7SdkCrossPromoManager()
	{
		RemoveComponent(88);
	}

	public void AddO7SdkData(bool newManualNewsReady, string newPathToManualNewsButton)
	{
		int index = 89;
		O7SdkDataComponent o7SdkDataComponent = CreateComponent<O7SdkDataComponent>(index);
		o7SdkDataComponent.manualNewsReady = newManualNewsReady;
		o7SdkDataComponent.pathToManualNewsButton = newPathToManualNewsButton;
		AddComponent(index, o7SdkDataComponent);
	}

	public void ReplaceO7SdkData(bool newManualNewsReady, string newPathToManualNewsButton)
	{
		int index = 89;
		O7SdkDataComponent o7SdkDataComponent = CreateComponent<O7SdkDataComponent>(index);
		o7SdkDataComponent.manualNewsReady = newManualNewsReady;
		o7SdkDataComponent.pathToManualNewsButton = newPathToManualNewsButton;
		ReplaceComponent(index, o7SdkDataComponent);
	}

	public void RemoveO7SdkData()
	{
		RemoveComponent(89);
	}

	public void AddO7SdkDataManager(O7SDKDataManager newManager)
	{
		int index = 90;
		O7SdkDataManagerComponent o7SdkDataManagerComponent = CreateComponent<O7SdkDataManagerComponent>(index);
		o7SdkDataManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkDataManagerComponent);
	}

	public void ReplaceO7SdkDataManager(O7SDKDataManager newManager)
	{
		int index = 90;
		O7SdkDataManagerComponent o7SdkDataManagerComponent = CreateComponent<O7SdkDataManagerComponent>(index);
		o7SdkDataManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkDataManagerComponent);
	}

	public void RemoveO7SdkDataManager()
	{
		RemoveComponent(90);
	}

	public void AddO7SdkEventManager(O7SDKEventManager newManager)
	{
		int index = 91;
		O7SdkEventManagerComponent o7SdkEventManagerComponent = CreateComponent<O7SdkEventManagerComponent>(index);
		o7SdkEventManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkEventManagerComponent);
	}

	public void ReplaceO7SdkEventManager(O7SDKEventManager newManager)
	{
		int index = 91;
		O7SdkEventManagerComponent o7SdkEventManagerComponent = CreateComponent<O7SdkEventManagerComponent>(index);
		o7SdkEventManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkEventManagerComponent);
	}

	public void RemoveO7SdkEventManager()
	{
		RemoveComponent(91);
	}

	public void AddO7SdkExtConfig(BalancingDataOverrideConfig newData)
	{
		int index = 171;
		O7SdkExtConfigComponent o7SdkExtConfigComponent = CreateComponent<O7SdkExtConfigComponent>(index);
		o7SdkExtConfigComponent.Data = newData;
		AddComponent(index, o7SdkExtConfigComponent);
	}

	public void ReplaceO7SdkExtConfig(BalancingDataOverrideConfig newData)
	{
		int index = 171;
		O7SdkExtConfigComponent o7SdkExtConfigComponent = CreateComponent<O7SdkExtConfigComponent>(index);
		o7SdkExtConfigComponent.Data = newData;
		ReplaceComponent(index, o7SdkExtConfigComponent);
	}

	public void RemoveO7SdkExtConfig()
	{
		RemoveComponent(171);
	}

	public void AddO7SdkGateDone(bool newNewGameStarted)
	{
		int index = 95;
		O7SdkGateDoneComponent o7SdkGateDoneComponent = CreateComponent<O7SdkGateDoneComponent>(index);
		o7SdkGateDoneComponent.newGameStarted = newNewGameStarted;
		AddComponent(index, o7SdkGateDoneComponent);
	}

	public void ReplaceO7SdkGateDone(bool newNewGameStarted)
	{
		int index = 95;
		O7SdkGateDoneComponent o7SdkGateDoneComponent = CreateComponent<O7SdkGateDoneComponent>(index);
		o7SdkGateDoneComponent.newGameStarted = newNewGameStarted;
		ReplaceComponent(index, o7SdkGateDoneComponent);
	}

	public void RemoveO7SdkGateDone()
	{
		RemoveComponent(95);
	}

	public void AddO7SdkGdprAdProviders(List<AdProvider> newProviders)
	{
		int index = 96;
		O7SdkGdprAdProvidersComponent o7SdkGdprAdProvidersComponent = CreateComponent<O7SdkGdprAdProvidersComponent>(index);
		o7SdkGdprAdProvidersComponent.providers = newProviders;
		AddComponent(index, o7SdkGdprAdProvidersComponent);
	}

	public void ReplaceO7SdkGdprAdProviders(List<AdProvider> newProviders)
	{
		int index = 96;
		O7SdkGdprAdProvidersComponent o7SdkGdprAdProvidersComponent = CreateComponent<O7SdkGdprAdProvidersComponent>(index);
		o7SdkGdprAdProvidersComponent.providers = newProviders;
		ReplaceComponent(index, o7SdkGdprAdProvidersComponent);
	}

	public void RemoveO7SdkGdprAdProviders()
	{
		RemoveComponent(96);
	}

	public void AddO7SdkGdprAdProvidersFetch(int newMode)
	{
		int index = 97;
		O7SdkGdprAdProvidersFetchComponent o7SdkGdprAdProvidersFetchComponent = CreateComponent<O7SdkGdprAdProvidersFetchComponent>(index);
		o7SdkGdprAdProvidersFetchComponent.mode = newMode;
		AddComponent(index, o7SdkGdprAdProvidersFetchComponent);
	}

	public void ReplaceO7SdkGdprAdProvidersFetch(int newMode)
	{
		int index = 97;
		O7SdkGdprAdProvidersFetchComponent o7SdkGdprAdProvidersFetchComponent = CreateComponent<O7SdkGdprAdProvidersFetchComponent>(index);
		o7SdkGdprAdProvidersFetchComponent.mode = newMode;
		ReplaceComponent(index, o7SdkGdprAdProvidersFetchComponent);
	}

	public void RemoveO7SdkGdprAdProvidersFetch()
	{
		RemoveComponent(97);
	}

	public void AddO7SdkGdprEnableAdProvider(string newProviderId, bool newIsEnabled)
	{
		int index = 109;
		O7SdkGdprEnableAdProviderComponent o7SdkGdprEnableAdProviderComponent = CreateComponent<O7SdkGdprEnableAdProviderComponent>(index);
		o7SdkGdprEnableAdProviderComponent.providerId = newProviderId;
		o7SdkGdprEnableAdProviderComponent.isEnabled = newIsEnabled;
		AddComponent(index, o7SdkGdprEnableAdProviderComponent);
	}

	public void ReplaceO7SdkGdprEnableAdProvider(string newProviderId, bool newIsEnabled)
	{
		int index = 109;
		O7SdkGdprEnableAdProviderComponent o7SdkGdprEnableAdProviderComponent = CreateComponent<O7SdkGdprEnableAdProviderComponent>(index);
		o7SdkGdprEnableAdProviderComponent.providerId = newProviderId;
		o7SdkGdprEnableAdProviderComponent.isEnabled = newIsEnabled;
		ReplaceComponent(index, o7SdkGdprEnableAdProviderComponent);
	}

	public void RemoveO7SdkGdprEnableAdProvider()
	{
		RemoveComponent(109);
	}

	public void AddO7SdkGdprManager(O7SDKGdprManager newManager)
	{
		int index = 110;
		O7SdkGdprManagerComponent o7SdkGdprManagerComponent = CreateComponent<O7SdkGdprManagerComponent>(index);
		o7SdkGdprManagerComponent.manager = newManager;
		AddComponent(index, o7SdkGdprManagerComponent);
	}

	public void ReplaceO7SdkGdprManager(O7SDKGdprManager newManager)
	{
		int index = 110;
		O7SdkGdprManagerComponent o7SdkGdprManagerComponent = CreateComponent<O7SdkGdprManagerComponent>(index);
		o7SdkGdprManagerComponent.manager = newManager;
		ReplaceComponent(index, o7SdkGdprManagerComponent);
	}

	public void RemoveO7SdkGdprManager()
	{
		RemoveComponent(110);
	}

	public void AddO7SdkGdprShowConsentToolType(GDPRShowState newState)
	{
		int index = 115;
		O7SdkGdprShowConsentToolTypeComponent o7SdkGdprShowConsentToolTypeComponent = CreateComponent<O7SdkGdprShowConsentToolTypeComponent>(index);
		o7SdkGdprShowConsentToolTypeComponent.state = newState;
		AddComponent(index, o7SdkGdprShowConsentToolTypeComponent);
	}

	public void ReplaceO7SdkGdprShowConsentToolType(GDPRShowState newState)
	{
		int index = 115;
		O7SdkGdprShowConsentToolTypeComponent o7SdkGdprShowConsentToolTypeComponent = CreateComponent<O7SdkGdprShowConsentToolTypeComponent>(index);
		o7SdkGdprShowConsentToolTypeComponent.state = newState;
		ReplaceComponent(index, o7SdkGdprShowConsentToolTypeComponent);
	}

	public void RemoveO7SdkGdprShowConsentToolType()
	{
		RemoveComponent(115);
	}

	public void AddO7SdkGenderGateSelected(O7SDKGateManager.Gender newGender)
	{
		int index = 118;
		O7SdkGenderGateSelectedComponent o7SdkGenderGateSelectedComponent = CreateComponent<O7SdkGenderGateSelectedComponent>(index);
		o7SdkGenderGateSelectedComponent.gender = newGender;
		AddComponent(index, o7SdkGenderGateSelectedComponent);
	}

	public void ReplaceO7SdkGenderGateSelected(O7SDKGateManager.Gender newGender)
	{
		int index = 118;
		O7SdkGenderGateSelectedComponent o7SdkGenderGateSelectedComponent = CreateComponent<O7SdkGenderGateSelectedComponent>(index);
		o7SdkGenderGateSelectedComponent.gender = newGender;
		ReplaceComponent(index, o7SdkGenderGateSelectedComponent);
	}

	public void RemoveO7SdkGenderGateSelected()
	{
		RemoveComponent(118);
	}

	public void AddO7SdkIapBuyError(string newProductId)
	{
		int index = 121;
		O7SdkIapBuyErrorComponent o7SdkIapBuyErrorComponent = CreateComponent<O7SdkIapBuyErrorComponent>(index);
		o7SdkIapBuyErrorComponent.productId = newProductId;
		AddComponent(index, o7SdkIapBuyErrorComponent);
	}

	public void ReplaceO7SdkIapBuyError(string newProductId)
	{
		int index = 121;
		O7SdkIapBuyErrorComponent o7SdkIapBuyErrorComponent = CreateComponent<O7SdkIapBuyErrorComponent>(index);
		o7SdkIapBuyErrorComponent.productId = newProductId;
		ReplaceComponent(index, o7SdkIapBuyErrorComponent);
	}

	public void RemoveO7SdkIapBuyError()
	{
		RemoveComponent(121);
	}

	public void AddO7SdkIapBuyFail(string newProductId)
	{
		int index = 122;
		O7SdkIapBuyFailComponent o7SdkIapBuyFailComponent = CreateComponent<O7SdkIapBuyFailComponent>(index);
		o7SdkIapBuyFailComponent.productId = newProductId;
		AddComponent(index, o7SdkIapBuyFailComponent);
	}

	public void ReplaceO7SdkIapBuyFail(string newProductId)
	{
		int index = 122;
		O7SdkIapBuyFailComponent o7SdkIapBuyFailComponent = CreateComponent<O7SdkIapBuyFailComponent>(index);
		o7SdkIapBuyFailComponent.productId = newProductId;
		ReplaceComponent(index, o7SdkIapBuyFailComponent);
	}

	public void RemoveO7SdkIapBuyFail()
	{
		RemoveComponent(122);
	}

	public void AddO7SdkIapBuyRequest(string newProductId, RectTransform newTransform)
	{
		int index = 123;
		O7SdkIapBuyRequestComponent o7SdkIapBuyRequestComponent = CreateComponent<O7SdkIapBuyRequestComponent>(index);
		o7SdkIapBuyRequestComponent.productId = newProductId;
		o7SdkIapBuyRequestComponent.transform = newTransform;
		AddComponent(index, o7SdkIapBuyRequestComponent);
	}

	public void ReplaceO7SdkIapBuyRequest(string newProductId, RectTransform newTransform)
	{
		int index = 123;
		O7SdkIapBuyRequestComponent o7SdkIapBuyRequestComponent = CreateComponent<O7SdkIapBuyRequestComponent>(index);
		o7SdkIapBuyRequestComponent.productId = newProductId;
		o7SdkIapBuyRequestComponent.transform = newTransform;
		ReplaceComponent(index, o7SdkIapBuyRequestComponent);
	}

	public void RemoveO7SdkIapBuyRequest()
	{
		RemoveComponent(123);
	}

	public void AddO7SdkIapBuySuccess(string newProductId, RectTransform newTransform)
	{
		int index = 124;
		O7SdkIapBuySuccessComponent o7SdkIapBuySuccessComponent = CreateComponent<O7SdkIapBuySuccessComponent>(index);
		o7SdkIapBuySuccessComponent.productId = newProductId;
		o7SdkIapBuySuccessComponent.transform = newTransform;
		AddComponent(index, o7SdkIapBuySuccessComponent);
	}

	public void ReplaceO7SdkIapBuySuccess(string newProductId, RectTransform newTransform)
	{
		int index = 124;
		O7SdkIapBuySuccessComponent o7SdkIapBuySuccessComponent = CreateComponent<O7SdkIapBuySuccessComponent>(index);
		o7SdkIapBuySuccessComponent.productId = newProductId;
		o7SdkIapBuySuccessComponent.transform = newTransform;
		ReplaceComponent(index, o7SdkIapBuySuccessComponent);
	}

	public void RemoveO7SdkIapBuySuccess()
	{
		RemoveComponent(124);
	}

	public void AddO7SdkIapItemPriceData(Dictionary<string, string> newPricesDictionary)
	{
		int index = 125;
		O7SdkIapItemPriceData o7SdkIapItemPriceData = CreateComponent<O7SdkIapItemPriceData>(index);
		o7SdkIapItemPriceData.pricesDictionary = newPricesDictionary;
		AddComponent(index, o7SdkIapItemPriceData);
	}

	public void ReplaceO7SdkIapItemPriceData(Dictionary<string, string> newPricesDictionary)
	{
		int index = 125;
		O7SdkIapItemPriceData o7SdkIapItemPriceData = CreateComponent<O7SdkIapItemPriceData>(index);
		o7SdkIapItemPriceData.pricesDictionary = newPricesDictionary;
		ReplaceComponent(index, o7SdkIapItemPriceData);
	}

	public void RemoveO7SdkIapItemPriceData()
	{
		RemoveComponent(125);
	}

	public void AddO7SdkIapPacks(List<IapPacks> newIapPacks)
	{
		int index = 127;
		O7SdkIapPacksComponent o7SdkIapPacksComponent = CreateComponent<O7SdkIapPacksComponent>(index);
		o7SdkIapPacksComponent.iapPacks = newIapPacks;
		AddComponent(index, o7SdkIapPacksComponent);
	}

	public void ReplaceO7SdkIapPacks(List<IapPacks> newIapPacks)
	{
		int index = 127;
		O7SdkIapPacksComponent o7SdkIapPacksComponent = CreateComponent<O7SdkIapPacksComponent>(index);
		o7SdkIapPacksComponent.iapPacks = newIapPacks;
		ReplaceComponent(index, o7SdkIapPacksComponent);
	}

	public void RemoveO7SdkIapPacks()
	{
		RemoveComponent(127);
	}

	public void AddO7SdkInterstitialAd(string newScreen1, string newScreen2)
	{
		int index = 128;
		O7SdkInterstitialAdComponent o7SdkInterstitialAdComponent = CreateComponent<O7SdkInterstitialAdComponent>(index);
		o7SdkInterstitialAdComponent.screen1 = newScreen1;
		o7SdkInterstitialAdComponent.screen2 = newScreen2;
		AddComponent(index, o7SdkInterstitialAdComponent);
	}

	public void ReplaceO7SdkInterstitialAd(string newScreen1, string newScreen2)
	{
		int index = 128;
		O7SdkInterstitialAdComponent o7SdkInterstitialAdComponent = CreateComponent<O7SdkInterstitialAdComponent>(index);
		o7SdkInterstitialAdComponent.screen1 = newScreen1;
		o7SdkInterstitialAdComponent.screen2 = newScreen2;
		ReplaceComponent(index, o7SdkInterstitialAdComponent);
	}

	public void RemoveO7SdkInterstitialAd()
	{
		RemoveComponent(128);
	}

	public void AddO7SdkInterstitialAdManager(O7ADInterstitialAdManager newManager)
	{
		int index = 129;
		O7SdkInterstitialAdManagerComponent o7SdkInterstitialAdManagerComponent = CreateComponent<O7SdkInterstitialAdManagerComponent>(index);
		o7SdkInterstitialAdManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkInterstitialAdManagerComponent);
	}

	public void ReplaceO7SdkInterstitialAdManager(O7ADInterstitialAdManager newManager)
	{
		int index = 129;
		O7SdkInterstitialAdManagerComponent o7SdkInterstitialAdManagerComponent = CreateComponent<O7SdkInterstitialAdManagerComponent>(index);
		o7SdkInterstitialAdManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkInterstitialAdManagerComponent);
	}

	public void RemoveO7SdkInterstitialAdManager()
	{
		RemoveComponent(129);
	}

	public void AddO7SdkManager(O7SDKManager newManager)
	{
		int index = 131;
		O7SdkManagerComponent o7SdkManagerComponent = CreateComponent<O7SdkManagerComponent>(index);
		o7SdkManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkManagerComponent);
	}

	public void ReplaceO7SdkManager(O7SDKManager newManager)
	{
		int index = 131;
		O7SdkManagerComponent o7SdkManagerComponent = CreateComponent<O7SdkManagerComponent>(index);
		o7SdkManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkManagerComponent);
	}

	public void RemoveO7SdkManager()
	{
		RemoveComponent(131);
	}

	public void AddO7SdkPrivacyURL(string newUrl)
	{
		int index = 133;
		O7SdkPrivacyURLComponent o7SdkPrivacyURLComponent = CreateComponent<O7SdkPrivacyURLComponent>(index);
		o7SdkPrivacyURLComponent.url = newUrl;
		AddComponent(index, o7SdkPrivacyURLComponent);
	}

	public void ReplaceO7SdkPrivacyURL(string newUrl)
	{
		int index = 133;
		O7SdkPrivacyURLComponent o7SdkPrivacyURLComponent = CreateComponent<O7SdkPrivacyURLComponent>(index);
		o7SdkPrivacyURLComponent.url = newUrl;
		ReplaceComponent(index, o7SdkPrivacyURLComponent);
	}

	public void RemoveO7SdkPrivacyURL()
	{
		RemoveComponent(133);
	}

	public void AddO7SdkPurchaseManager(O7SDKPurchaseManager newManager)
	{
		int index = 134;
		O7SdkPurchaseManagerComponent o7SdkPurchaseManagerComponent = CreateComponent<O7SdkPurchaseManagerComponent>(index);
		o7SdkPurchaseManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkPurchaseManagerComponent);
	}

	public void ReplaceO7SdkPurchaseManager(O7SDKPurchaseManager newManager)
	{
		int index = 134;
		O7SdkPurchaseManagerComponent o7SdkPurchaseManagerComponent = CreateComponent<O7SdkPurchaseManagerComponent>(index);
		o7SdkPurchaseManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkPurchaseManagerComponent);
	}

	public void RemoveO7SdkPurchaseManager()
	{
		RemoveComponent(134);
	}

	public void AddO7SdkRewardAwarded(string newProviderID)
	{
		int index = 144;
		O7SdkRewardAwardedComponent o7SdkRewardAwardedComponent = CreateComponent<O7SdkRewardAwardedComponent>(index);
		o7SdkRewardAwardedComponent.ProviderID = newProviderID;
		AddComponent(index, o7SdkRewardAwardedComponent);
	}

	public void ReplaceO7SdkRewardAwarded(string newProviderID)
	{
		int index = 144;
		O7SdkRewardAwardedComponent o7SdkRewardAwardedComponent = CreateComponent<O7SdkRewardAwardedComponent>(index);
		o7SdkRewardAwardedComponent.ProviderID = newProviderID;
		ReplaceComponent(index, o7SdkRewardAwardedComponent);
	}

	public void RemoveO7SdkRewardAwarded()
	{
		RemoveComponent(144);
	}

	public void AddO7SdkRewardedAdManager(O7ADRewardAdManager newManager)
	{
		int index = 145;
		O7SdkRewardedAdManagerComponent o7SdkRewardedAdManagerComponent = CreateComponent<O7SdkRewardedAdManagerComponent>(index);
		o7SdkRewardedAdManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkRewardedAdManagerComponent);
	}

	public void ReplaceO7SdkRewardedAdManager(O7ADRewardAdManager newManager)
	{
		int index = 145;
		O7SdkRewardedAdManagerComponent o7SdkRewardedAdManagerComponent = CreateComponent<O7SdkRewardedAdManagerComponent>(index);
		o7SdkRewardedAdManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkRewardedAdManagerComponent);
	}

	public void RemoveO7SdkRewardedAdManager()
	{
		RemoveComponent(145);
	}

	public void AddO7SdkRewardOfferType(O7SdkRewardOfferType newOfferType)
	{
		int index = 147;
		O7SdkRewardOfferTypeComponent o7SdkRewardOfferTypeComponent = CreateComponent<O7SdkRewardOfferTypeComponent>(index);
		o7SdkRewardOfferTypeComponent.offerType = newOfferType;
		AddComponent(index, o7SdkRewardOfferTypeComponent);
	}

	public void ReplaceO7SdkRewardOfferType(O7SdkRewardOfferType newOfferType)
	{
		int index = 147;
		O7SdkRewardOfferTypeComponent o7SdkRewardOfferTypeComponent = CreateComponent<O7SdkRewardOfferTypeComponent>(index);
		o7SdkRewardOfferTypeComponent.offerType = newOfferType;
		ReplaceComponent(index, o7SdkRewardOfferTypeComponent);
	}

	public void RemoveO7SdkRewardOfferType()
	{
		RemoveComponent(147);
	}

	public void AddO7SdkSelectCountryCode(string newCountryCode)
	{
		int index = 149;
		O7SdkSelectCountryCodeComponent o7SdkSelectCountryCodeComponent = CreateComponent<O7SdkSelectCountryCodeComponent>(index);
		o7SdkSelectCountryCodeComponent.countryCode = newCountryCode;
		AddComponent(index, o7SdkSelectCountryCodeComponent);
	}

	public void ReplaceO7SdkSelectCountryCode(string newCountryCode)
	{
		int index = 149;
		O7SdkSelectCountryCodeComponent o7SdkSelectCountryCodeComponent = CreateComponent<O7SdkSelectCountryCodeComponent>(index);
		o7SdkSelectCountryCodeComponent.countryCode = newCountryCode;
		ReplaceComponent(index, o7SdkSelectCountryCodeComponent);
	}

	public void RemoveO7SdkSelectCountryCode()
	{
		RemoveComponent(149);
	}

	public void AddO7SdkSendLogs(string newRecipientEmail, string newSubject, string newBody, string[] newAttachmentPaths, int newRequestCode)
	{
		int index = 150;
		O7SdkSendLogsComponent o7SdkSendLogsComponent = CreateComponent<O7SdkSendLogsComponent>(index);
		o7SdkSendLogsComponent.recipientEmail = newRecipientEmail;
		o7SdkSendLogsComponent.subject = newSubject;
		o7SdkSendLogsComponent.body = newBody;
		o7SdkSendLogsComponent.attachmentPaths = newAttachmentPaths;
		o7SdkSendLogsComponent.requestCode = newRequestCode;
		AddComponent(index, o7SdkSendLogsComponent);
	}

	public void ReplaceO7SdkSendLogs(string newRecipientEmail, string newSubject, string newBody, string[] newAttachmentPaths, int newRequestCode)
	{
		int index = 150;
		O7SdkSendLogsComponent o7SdkSendLogsComponent = CreateComponent<O7SdkSendLogsComponent>(index);
		o7SdkSendLogsComponent.recipientEmail = newRecipientEmail;
		o7SdkSendLogsComponent.subject = newSubject;
		o7SdkSendLogsComponent.body = newBody;
		o7SdkSendLogsComponent.attachmentPaths = newAttachmentPaths;
		o7SdkSendLogsComponent.requestCode = newRequestCode;
		ReplaceComponent(index, o7SdkSendLogsComponent);
	}

	public void RemoveO7SdkSendLogs()
	{
		RemoveComponent(150);
	}

	public void AddO7SdkUserStateManager(O7SDKRestoreManager newManager)
	{
		int index = 151;
		O7SdkUserStateManagerComponent o7SdkUserStateManagerComponent = CreateComponent<O7SdkUserStateManagerComponent>(index);
		o7SdkUserStateManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkUserStateManagerComponent);
	}

	public void ReplaceO7SdkUserStateManager(O7SDKRestoreManager newManager)
	{
		int index = 151;
		O7SdkUserStateManagerComponent o7SdkUserStateManagerComponent = CreateComponent<O7SdkUserStateManagerComponent>(index);
		o7SdkUserStateManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkUserStateManagerComponent);
	}

	public void RemoveO7SdkUserStateManager()
	{
		RemoveComponent(151);
	}

	public void AddO7SdkUtilityManager(O7SDKUtilityManager newManager)
	{
		int index = 152;
		O7SdkUtilityManagerComponent o7SdkUtilityManagerComponent = CreateComponent<O7SdkUtilityManagerComponent>(index);
		o7SdkUtilityManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkUtilityManagerComponent);
	}

	public void ReplaceO7SdkUtilityManager(O7SDKUtilityManager newManager)
	{
		int index = 152;
		O7SdkUtilityManagerComponent o7SdkUtilityManagerComponent = CreateComponent<O7SdkUtilityManagerComponent>(index);
		o7SdkUtilityManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkUtilityManagerComponent);
	}

	public void RemoveO7SdkUtilityManager()
	{
		RemoveComponent(152);
	}

	public void AddO7SdkValidIapPacks(List<ValidIapPacks> newIapValidPacks)
	{
		int index = 153;
		O7SdkValidIapPacksComponent o7SdkValidIapPacksComponent = CreateComponent<O7SdkValidIapPacksComponent>(index);
		o7SdkValidIapPacksComponent.iapValidPacks = newIapValidPacks;
		AddComponent(index, o7SdkValidIapPacksComponent);
	}

	public void ReplaceO7SdkValidIapPacks(List<ValidIapPacks> newIapValidPacks)
	{
		int index = 153;
		O7SdkValidIapPacksComponent o7SdkValidIapPacksComponent = CreateComponent<O7SdkValidIapPacksComponent>(index);
		o7SdkValidIapPacksComponent.iapValidPacks = newIapValidPacks;
		ReplaceComponent(index, o7SdkValidIapPacksComponent);
	}

	public void RemoveO7SdkValidIapPacks()
	{
		RemoveComponent(153);
	}

	public void AddO7SdkVideoGalleryManager(O7ADVideoGalleryAdManager newManager)
	{
		int index = 155;
		O7SdkVideoGalleryManagerComponent o7SdkVideoGalleryManagerComponent = CreateComponent<O7SdkVideoGalleryManagerComponent>(index);
		o7SdkVideoGalleryManagerComponent.Manager = newManager;
		AddComponent(index, o7SdkVideoGalleryManagerComponent);
	}

	public void ReplaceO7SdkVideoGalleryManager(O7ADVideoGalleryAdManager newManager)
	{
		int index = 155;
		O7SdkVideoGalleryManagerComponent o7SdkVideoGalleryManagerComponent = CreateComponent<O7SdkVideoGalleryManagerComponent>(index);
		o7SdkVideoGalleryManagerComponent.Manager = newManager;
		ReplaceComponent(index, o7SdkVideoGalleryManagerComponent);
	}

	public void RemoveO7SdkVideoGalleryManager()
	{
		RemoveComponent(155);
	}

	public void AddOpenUrl(string newUrl)
	{
		int index = 158;
		OpenUrlComponent openUrlComponent = CreateComponent<OpenUrlComponent>(index);
		openUrlComponent.Url = newUrl;
		AddComponent(index, openUrlComponent);
	}

	public void ReplaceOpenUrl(string newUrl)
	{
		int index = 158;
		OpenUrlComponent openUrlComponent = CreateComponent<OpenUrlComponent>(index);
		openUrlComponent.Url = newUrl;
		ReplaceComponent(index, openUrlComponent);
	}

	public void RemoveOpenUrl()
	{
		RemoveComponent(158);
	}

	public void AddPassLevelAction(TutorialOutgameLevel newTutorialLevel)
	{
		int index = 160;
		PassLevelActionComponent passLevelActionComponent = CreateComponent<PassLevelActionComponent>(index);
		passLevelActionComponent.TutorialLevel = newTutorialLevel;
		AddComponent(index, passLevelActionComponent);
	}

	public void ReplacePassLevelAction(TutorialOutgameLevel newTutorialLevel)
	{
		int index = 160;
		PassLevelActionComponent passLevelActionComponent = CreateComponent<PassLevelActionComponent>(index);
		passLevelActionComponent.TutorialLevel = newTutorialLevel;
		ReplaceComponent(index, passLevelActionComponent);
	}

	public void RemovePassLevelAction()
	{
		RemoveComponent(160);
	}

	public void AddPendingAsyncRequests(float newProgress)
	{
		int index = 161;
		PendingAsyncRequestsComponent pendingAsyncRequestsComponent = CreateComponent<PendingAsyncRequestsComponent>(index);
		pendingAsyncRequestsComponent.Progress = newProgress;
		AddComponent(index, pendingAsyncRequestsComponent);
	}

	public void ReplacePendingAsyncRequests(float newProgress)
	{
		int index = 161;
		PendingAsyncRequestsComponent pendingAsyncRequestsComponent = CreateComponent<PendingAsyncRequestsComponent>(index);
		pendingAsyncRequestsComponent.Progress = newProgress;
		ReplaceComponent(index, pendingAsyncRequestsComponent);
	}

	public void RemovePendingAsyncRequests()
	{
		RemoveComponent(161);
	}

	public void AddPlayerExperienceGranted(int newCurrentExperienceAmount)
	{
		int index = 162;
		PlayerExperienceGrantedComponent playerExperienceGrantedComponent = CreateComponent<PlayerExperienceGrantedComponent>(index);
		playerExperienceGrantedComponent.CurrentExperienceAmount = newCurrentExperienceAmount;
		AddComponent(index, playerExperienceGrantedComponent);
	}

	public void ReplacePlayerExperienceGranted(int newCurrentExperienceAmount)
	{
		int index = 162;
		PlayerExperienceGrantedComponent playerExperienceGrantedComponent = CreateComponent<PlayerExperienceGrantedComponent>(index);
		playerExperienceGrantedComponent.CurrentExperienceAmount = newCurrentExperienceAmount;
		ReplaceComponent(index, playerExperienceGrantedComponent);
	}

	public void RemovePlayerExperienceGranted()
	{
		RemoveComponent(162);
	}

	public void AddPlayerLevelBalancingData(Dictionary<int, PlayerLevelData> newMap)
	{
		int index = 163;
		PlayerLevelBalancingDataComponent playerLevelBalancingDataComponent = CreateComponent<PlayerLevelBalancingDataComponent>(index);
		playerLevelBalancingDataComponent.Map = newMap;
		AddComponent(index, playerLevelBalancingDataComponent);
	}

	public void ReplacePlayerLevelBalancingData(Dictionary<int, PlayerLevelData> newMap)
	{
		int index = 163;
		PlayerLevelBalancingDataComponent playerLevelBalancingDataComponent = CreateComponent<PlayerLevelBalancingDataComponent>(index);
		playerLevelBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, playerLevelBalancingDataComponent);
	}

	public void RemovePlayerLevelBalancingData()
	{
		RemoveComponent(163);
	}

	public void AddPlayerLevelExperienceAmountData(Dictionary<PlayerExperienceType, List<PlayerLevelExperienceAmountData>> newMap)
	{
		int index = 164;
		PlayerLevelExperienceAmountDataComponent playerLevelExperienceAmountDataComponent = CreateComponent<PlayerLevelExperienceAmountDataComponent>(index);
		playerLevelExperienceAmountDataComponent.Map = newMap;
		AddComponent(index, playerLevelExperienceAmountDataComponent);
	}

	public void ReplacePlayerLevelExperienceAmountData(Dictionary<PlayerExperienceType, List<PlayerLevelExperienceAmountData>> newMap)
	{
		int index = 164;
		PlayerLevelExperienceAmountDataComponent playerLevelExperienceAmountDataComponent = CreateComponent<PlayerLevelExperienceAmountDataComponent>(index);
		playerLevelExperienceAmountDataComponent.Map = newMap;
		ReplaceComponent(index, playerLevelExperienceAmountDataComponent);
	}

	public void RemovePlayerLevelExperienceAmountData()
	{
		RemoveComponent(164);
	}

	public void AddPlayerLevelListener(IPlayerLevelListener newListener)
	{
		int index = 165;
		PlayerLevelListenerComponent playerLevelListenerComponent = CreateComponent<PlayerLevelListenerComponent>(index);
		playerLevelListenerComponent.listener = newListener;
		AddComponent(index, playerLevelListenerComponent);
	}

	public void ReplacePlayerLevelListener(IPlayerLevelListener newListener)
	{
		int index = 165;
		PlayerLevelListenerComponent playerLevelListenerComponent = CreateComponent<PlayerLevelListenerComponent>(index);
		playerLevelListenerComponent.listener = newListener;
		ReplaceComponent(index, playerLevelListenerComponent);
	}

	public void RemovePlayerLevelListener()
	{
		RemoveComponent(165);
	}

	public void AddPlayerLevelUp(int newCurrentLevel)
	{
		int index = 166;
		PlayerLevelUpComponent playerLevelUpComponent = CreateComponent<PlayerLevelUpComponent>(index);
		playerLevelUpComponent.CurrentLevel = newCurrentLevel;
		AddComponent(index, playerLevelUpComponent);
	}

	public void ReplacePlayerLevelUp(int newCurrentLevel)
	{
		int index = 166;
		PlayerLevelUpComponent playerLevelUpComponent = CreateComponent<PlayerLevelUpComponent>(index);
		playerLevelUpComponent.CurrentLevel = newCurrentLevel;
		ReplaceComponent(index, playerLevelUpComponent);
	}

	public void RemovePlayerLevelUp()
	{
		RemoveComponent(166);
	}

	public void AddPlayerProgressionUpdated(int newCurrentLevel, int newCurrentExperienceAmount)
	{
		int index = 167;
		PlayerProgressionUpdatedComponent playerProgressionUpdatedComponent = CreateComponent<PlayerProgressionUpdatedComponent>(index);
		playerProgressionUpdatedComponent.CurrentLevel = newCurrentLevel;
		playerProgressionUpdatedComponent.CurrentExperienceAmount = newCurrentExperienceAmount;
		AddComponent(index, playerProgressionUpdatedComponent);
	}

	public void ReplacePlayerProgressionUpdated(int newCurrentLevel, int newCurrentExperienceAmount)
	{
		int index = 167;
		PlayerProgressionUpdatedComponent playerProgressionUpdatedComponent = CreateComponent<PlayerProgressionUpdatedComponent>(index);
		playerProgressionUpdatedComponent.CurrentLevel = newCurrentLevel;
		playerProgressionUpdatedComponent.CurrentExperienceAmount = newCurrentExperienceAmount;
		ReplaceComponent(index, playerProgressionUpdatedComponent);
	}

	public void RemovePlayerProgressionUpdated()
	{
		RemoveComponent(167);
	}

	public void AddPlayLoopSoundEffect(string newSoundId)
	{
		int index = 168;
		PlayLoopSoundEffectComponent playLoopSoundEffectComponent = CreateComponent<PlayLoopSoundEffectComponent>(index);
		playLoopSoundEffectComponent.SoundId = newSoundId;
		AddComponent(index, playLoopSoundEffectComponent);
	}

	public void ReplacePlayLoopSoundEffect(string newSoundId)
	{
		int index = 168;
		PlayLoopSoundEffectComponent playLoopSoundEffectComponent = CreateComponent<PlayLoopSoundEffectComponent>(index);
		playLoopSoundEffectComponent.SoundId = newSoundId;
		ReplaceComponent(index, playLoopSoundEffectComponent);
	}

	public void RemovePlayLoopSoundEffect()
	{
		RemoveComponent(168);
	}

	public void AddPlayMusic(string newMusicId)
	{
		int index = 169;
		PlayMusicComponent playMusicComponent = CreateComponent<PlayMusicComponent>(index);
		playMusicComponent.MusicId = newMusicId;
		AddComponent(index, playMusicComponent);
	}

	public void ReplacePlayMusic(string newMusicId)
	{
		int index = 169;
		PlayMusicComponent playMusicComponent = CreateComponent<PlayMusicComponent>(index);
		playMusicComponent.MusicId = newMusicId;
		ReplaceComponent(index, playMusicComponent);
	}

	public void RemovePlayMusic()
	{
		RemoveComponent(169);
	}

	public void AddPlaySoundEffect(string newSoundId)
	{
		int index = 172;
		PlaySoundEffectComponent playSoundEffectComponent = CreateComponent<PlaySoundEffectComponent>(index);
		playSoundEffectComponent.SoundId = newSoundId;
		AddComponent(index, playSoundEffectComponent);
	}

	public void ReplacePlaySoundEffect(string newSoundId)
	{
		int index = 172;
		PlaySoundEffectComponent playSoundEffectComponent = CreateComponent<PlaySoundEffectComponent>(index);
		playSoundEffectComponent.SoundId = newSoundId;
		ReplaceComponent(index, playSoundEffectComponent);
	}

	public void RemovePlaySoundEffect()
	{
		RemoveComponent(172);
	}

	public void AddPreviousScreen(ScreenId newScreenId)
	{
		int index = 173;
		PreviousScreenComponent previousScreenComponent = CreateComponent<PreviousScreenComponent>(index);
		previousScreenComponent.ScreenId = newScreenId;
		AddComponent(index, previousScreenComponent);
	}

	public void ReplacePreviousScreen(ScreenId newScreenId)
	{
		int index = 173;
		PreviousScreenComponent previousScreenComponent = CreateComponent<PreviousScreenComponent>(index);
		previousScreenComponent.ScreenId = newScreenId;
		ReplaceComponent(index, previousScreenComponent);
	}

	public void RemovePreviousScreen()
	{
		RemoveComponent(173);
	}

	public void AddPrivacyAndLegacyBalancingData(PrivacyAndLegalBalancingData newData)
	{
		int index = 174;
		PrivacyAndLegacyBalancingDataComponent privacyAndLegacyBalancingDataComponent = CreateComponent<PrivacyAndLegacyBalancingDataComponent>(index);
		privacyAndLegacyBalancingDataComponent.Data = newData;
		AddComponent(index, privacyAndLegacyBalancingDataComponent);
	}

	public void ReplacePrivacyAndLegacyBalancingData(PrivacyAndLegalBalancingData newData)
	{
		int index = 174;
		PrivacyAndLegacyBalancingDataComponent privacyAndLegacyBalancingDataComponent = CreateComponent<PrivacyAndLegacyBalancingDataComponent>(index);
		privacyAndLegacyBalancingDataComponent.Data = newData;
		ReplaceComponent(index, privacyAndLegacyBalancingDataComponent);
	}

	public void RemovePrivacyAndLegacyBalancingData()
	{
		RemoveComponent(174);
	}

	public void AddRemoteUserState(UserStateData newState, int newResponseCode)
	{
		int index = 176;
		RemoteUserStateComponent remoteUserStateComponent = CreateComponent<RemoteUserStateComponent>(index);
		remoteUserStateComponent.State = newState;
		remoteUserStateComponent.ResponseCode = newResponseCode;
		AddComponent(index, remoteUserStateComponent);
	}

	public void ReplaceRemoteUserState(UserStateData newState, int newResponseCode)
	{
		int index = 176;
		RemoteUserStateComponent remoteUserStateComponent = CreateComponent<RemoteUserStateComponent>(index);
		remoteUserStateComponent.State = newState;
		remoteUserStateComponent.ResponseCode = newResponseCode;
		ReplaceComponent(index, remoteUserStateComponent);
	}

	public void RemoveRemoteUserState()
	{
		RemoveComponent(176);
	}

	public void AddReplaceTargetEvent(TutorialAction[] newEventActions)
	{
		int index = 177;
		ReplaceTargetEventComponent replaceTargetEventComponent = CreateComponent<ReplaceTargetEventComponent>(index);
		replaceTargetEventComponent.EventActions = newEventActions;
		AddComponent(index, replaceTargetEventComponent);
	}

	public void ReplaceReplaceTargetEvent(TutorialAction[] newEventActions)
	{
		int index = 177;
		ReplaceTargetEventComponent replaceTargetEventComponent = CreateComponent<ReplaceTargetEventComponent>(index);
		replaceTargetEventComponent.EventActions = newEventActions;
		ReplaceComponent(index, replaceTargetEventComponent);
	}

	public void RemoveReplaceTargetEvent()
	{
		RemoveComponent(177);
	}

	public void AddReplaceUserState(UserStateData newNewState)
	{
		int index = 178;
		ReplaceUserStateComponent replaceUserStateComponent = CreateComponent<ReplaceUserStateComponent>(index);
		replaceUserStateComponent.NewState = newNewState;
		AddComponent(index, replaceUserStateComponent);
	}

	public void ReplaceReplaceUserState(UserStateData newNewState)
	{
		int index = 178;
		ReplaceUserStateComponent replaceUserStateComponent = CreateComponent<ReplaceUserStateComponent>(index);
		replaceUserStateComponent.NewState = newNewState;
		ReplaceComponent(index, replaceUserStateComponent);
	}

	public void RemoveReplaceUserState()
	{
		RemoveComponent(178);
	}

	public void AddRestoreSettingsBalancingData(Dictionary<RestoreSettings, int> newMap)
	{
		int index = 179;
		RestoreSettingsBalancingDataComponent restoreSettingsBalancingDataComponent = CreateComponent<RestoreSettingsBalancingDataComponent>(index);
		restoreSettingsBalancingDataComponent.Map = newMap;
		AddComponent(index, restoreSettingsBalancingDataComponent);
	}

	public void ReplaceRestoreSettingsBalancingData(Dictionary<RestoreSettings, int> newMap)
	{
		int index = 179;
		RestoreSettingsBalancingDataComponent restoreSettingsBalancingDataComponent = CreateComponent<RestoreSettingsBalancingDataComponent>(index);
		restoreSettingsBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, restoreSettingsBalancingDataComponent);
	}

	public void RemoveRestoreSettingsBalancingData()
	{
		RemoveComponent(179);
	}

	public void AddShopIAPsBalancingData(Dictionary<int, ShopIAPsBalancingData> newMap)
	{
		int index = 182;
		ShopIAPsBalancingDataComponent shopIAPsBalancingDataComponent = CreateComponent<ShopIAPsBalancingDataComponent>(index);
		shopIAPsBalancingDataComponent.Map = newMap;
		AddComponent(index, shopIAPsBalancingDataComponent);
	}

	public void ReplaceShopIAPsBalancingData(Dictionary<int, ShopIAPsBalancingData> newMap)
	{
		int index = 182;
		ShopIAPsBalancingDataComponent shopIAPsBalancingDataComponent = CreateComponent<ShopIAPsBalancingDataComponent>(index);
		shopIAPsBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, shopIAPsBalancingDataComponent);
	}

	public void RemoveShopIAPsBalancingData()
	{
		RemoveComponent(182);
	}

	public void AddShopSoftCurrencyPacksData(Dictionary<int, ShopSoftCurrencyPacksBalancingData> newMap)
	{
		int index = 183;
		ShopSoftCurrencyPacksDataComponent shopSoftCurrencyPacksDataComponent = CreateComponent<ShopSoftCurrencyPacksDataComponent>(index);
		shopSoftCurrencyPacksDataComponent.Map = newMap;
		AddComponent(index, shopSoftCurrencyPacksDataComponent);
	}

	public void ReplaceShopSoftCurrencyPacksData(Dictionary<int, ShopSoftCurrencyPacksBalancingData> newMap)
	{
		int index = 183;
		ShopSoftCurrencyPacksDataComponent shopSoftCurrencyPacksDataComponent = CreateComponent<ShopSoftCurrencyPacksDataComponent>(index);
		shopSoftCurrencyPacksDataComponent.Map = newMap;
		ReplaceComponent(index, shopSoftCurrencyPacksDataComponent);
	}

	public void RemoveShopSoftCurrencyPacksData()
	{
		RemoveComponent(183);
	}

	public void AddShowHandAciton(HandOptions newOptions)
	{
		int index = 184;
		ShowHandAcitonComponent showHandAcitonComponent = CreateComponent<ShowHandAcitonComponent>(index);
		showHandAcitonComponent.Options = newOptions;
		AddComponent(index, showHandAcitonComponent);
	}

	public void ReplaceShowHandAciton(HandOptions newOptions)
	{
		int index = 184;
		ShowHandAcitonComponent showHandAcitonComponent = CreateComponent<ShowHandAcitonComponent>(index);
		showHandAcitonComponent.Options = newOptions;
		ReplaceComponent(index, showHandAcitonComponent);
	}

	public void RemoveShowHandAciton()
	{
		RemoveComponent(184);
	}

	public void AddSoftCurrnecyPrice(int newAmount)
	{
		int index = 185;
		SoftCurrnecyPriceComponent softCurrnecyPriceComponent = CreateComponent<SoftCurrnecyPriceComponent>(index);
		softCurrnecyPriceComponent.Amount = newAmount;
		AddComponent(index, softCurrnecyPriceComponent);
	}

	public void ReplaceSoftCurrnecyPrice(int newAmount)
	{
		int index = 185;
		SoftCurrnecyPriceComponent softCurrnecyPriceComponent = CreateComponent<SoftCurrnecyPriceComponent>(index);
		softCurrnecyPriceComponent.Amount = newAmount;
		ReplaceComponent(index, softCurrnecyPriceComponent);
	}

	public void RemoveSoftCurrnecyPrice()
	{
		RemoveComponent(185);
	}

	public void AddStaminaSettingsBalancingData(Dictionary<StaminaSettings, float> newMap)
	{
		int index = 186;
		StaminaSettingsBalancingDataComponent staminaSettingsBalancingDataComponent = CreateComponent<StaminaSettingsBalancingDataComponent>(index);
		staminaSettingsBalancingDataComponent.Map = newMap;
		AddComponent(index, staminaSettingsBalancingDataComponent);
	}

	public void ReplaceStaminaSettingsBalancingData(Dictionary<StaminaSettings, float> newMap)
	{
		int index = 186;
		StaminaSettingsBalancingDataComponent staminaSettingsBalancingDataComponent = CreateComponent<StaminaSettingsBalancingDataComponent>(index);
		staminaSettingsBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, staminaSettingsBalancingDataComponent);
	}

	public void RemoveStaminaSettingsBalancingData()
	{
		RemoveComponent(186);
	}

	public void AddStopLoopSoundEffect(string newSoundId)
	{
		int index = 187;
		StopLoopSoundEffectComponent stopLoopSoundEffectComponent = CreateComponent<StopLoopSoundEffectComponent>(index);
		stopLoopSoundEffectComponent.SoundId = newSoundId;
		AddComponent(index, stopLoopSoundEffectComponent);
	}

	public void ReplaceStopLoopSoundEffect(string newSoundId)
	{
		int index = 187;
		StopLoopSoundEffectComponent stopLoopSoundEffectComponent = CreateComponent<StopLoopSoundEffectComponent>(index);
		stopLoopSoundEffectComponent.SoundId = newSoundId;
		ReplaceComponent(index, stopLoopSoundEffectComponent);
	}

	public void RemoveStopLoopSoundEffect()
	{
		RemoveComponent(187);
	}

	public void AddStopMusic(string newMusicId)
	{
		int index = 188;
		StopMusicComponent stopMusicComponent = CreateComponent<StopMusicComponent>(index);
		stopMusicComponent.MusicId = newMusicId;
		AddComponent(index, stopMusicComponent);
	}

	public void ReplaceStopMusic(string newMusicId)
	{
		int index = 188;
		StopMusicComponent stopMusicComponent = CreateComponent<StopMusicComponent>(index);
		stopMusicComponent.MusicId = newMusicId;
		ReplaceComponent(index, stopMusicComponent);
	}

	public void RemoveStopMusic()
	{
		RemoveComponent(188);
	}

	public void AddStringData(string newValue)
	{
		int index = 189;
		StringDataComponent stringDataComponent = CreateComponent<StringDataComponent>(index);
		stringDataComponent.Value = newValue;
		AddComponent(index, stringDataComponent);
	}

	public void ReplaceStringData(string newValue)
	{
		int index = 189;
		StringDataComponent stringDataComponent = CreateComponent<StringDataComponent>(index);
		stringDataComponent.Value = newValue;
		ReplaceComponent(index, stringDataComponent);
	}

	public void RemoveStringData()
	{
		RemoveComponent(189);
	}

	public void AddTransactionCallback(Action newCallback)
	{
		int index = 193;
		TransactionCallback transactionCallback = CreateComponent<TransactionCallback>(index);
		transactionCallback.Callback = newCallback;
		AddComponent(index, transactionCallback);
	}

	public void ReplaceTransactionCallback(Action newCallback)
	{
		int index = 193;
		TransactionCallback transactionCallback = CreateComponent<TransactionCallback>(index);
		transactionCallback.Callback = newCallback;
		ReplaceComponent(index, transactionCallback);
	}

	public void RemoveTransactionCallback()
	{
		RemoveComponent(193);
	}

	public void AddTransactionImage(string newImageName)
	{
		int index = 197;
		TransactionImage transactionImage = CreateComponent<TransactionImage>(index);
		transactionImage.ImageName = newImageName;
		AddComponent(index, transactionImage);
	}

	public void ReplaceTransactionImage(string newImageName)
	{
		int index = 197;
		TransactionImage transactionImage = CreateComponent<TransactionImage>(index);
		transactionImage.ImageName = newImageName;
		ReplaceComponent(index, transactionImage);
	}

	public void RemoveTransactionImage()
	{
		RemoveComponent(197);
	}

	public void AddTransactionName(string newName, string newContentDescription)
	{
		int index = 198;
		TransactionName transactionName = CreateComponent<TransactionName>(index);
		transactionName.Name = newName;
		transactionName.ContentDescription = newContentDescription;
		AddComponent(index, transactionName);
	}

	public void ReplaceTransactionName(string newName, string newContentDescription)
	{
		int index = 198;
		TransactionName transactionName = CreateComponent<TransactionName>(index);
		transactionName.Name = newName;
		transactionName.ContentDescription = newContentDescription;
		ReplaceComponent(index, transactionName);
	}

	public void RemoveTransactionName()
	{
		RemoveComponent(198);
	}

	public void AddTransactionOrigin(RectTransform newOrigin)
	{
		int index = 200;
		TransactionOrigin transactionOrigin = CreateComponent<TransactionOrigin>(index);
		transactionOrigin.Origin = newOrigin;
		AddComponent(index, transactionOrigin);
	}

	public void ReplaceTransactionOrigin(RectTransform newOrigin)
	{
		int index = 200;
		TransactionOrigin transactionOrigin = CreateComponent<TransactionOrigin>(index);
		transactionOrigin.Origin = newOrigin;
		ReplaceComponent(index, transactionOrigin);
	}

	public void RemoveTransactionOrigin()
	{
		RemoveComponent(200);
	}

	public void AddTransitionInformation(float newTransitionDuration)
	{
		int index = 203;
		TransitionInformationComponent transitionInformationComponent = CreateComponent<TransitionInformationComponent>(index);
		transitionInformationComponent.TransitionDuration = newTransitionDuration;
		AddComponent(index, transitionInformationComponent);
	}

	public void ReplaceTransitionInformation(float newTransitionDuration)
	{
		int index = 203;
		TransitionInformationComponent transitionInformationComponent = CreateComponent<TransitionInformationComponent>(index);
		transitionInformationComponent.TransitionDuration = newTransitionDuration;
		ReplaceComponent(index, transitionInformationComponent);
	}

	public void RemoveTransitionInformation()
	{
		RemoveComponent(203);
	}

	public void AddTutorialActiveStep(TutorialBaseStep newStep)
	{
		int index = 204;
		TutorialActiveStepComponent tutorialActiveStepComponent = CreateComponent<TutorialActiveStepComponent>(index);
		tutorialActiveStepComponent.Step = newStep;
		AddComponent(index, tutorialActiveStepComponent);
	}

	public void ReplaceTutorialActiveStep(TutorialBaseStep newStep)
	{
		int index = 204;
		TutorialActiveStepComponent tutorialActiveStepComponent = CreateComponent<TutorialActiveStepComponent>(index);
		tutorialActiveStepComponent.Step = newStep;
		ReplaceComponent(index, tutorialActiveStepComponent);
	}

	public void RemoveTutorialActiveStep()
	{
		RemoveComponent(204);
	}

	public void AddTutorialDisableStep(TutorialBaseStep newStep)
	{
		int index = 207;
		TutorialDisableStepComponent tutorialDisableStepComponent = CreateComponent<TutorialDisableStepComponent>(index);
		tutorialDisableStepComponent.Step = newStep;
		AddComponent(index, tutorialDisableStepComponent);
	}

	public void ReplaceTutorialDisableStep(TutorialBaseStep newStep)
	{
		int index = 207;
		TutorialDisableStepComponent tutorialDisableStepComponent = CreateComponent<TutorialDisableStepComponent>(index);
		tutorialDisableStepComponent.Step = newStep;
		ReplaceComponent(index, tutorialDisableStepComponent);
	}

	public void RemoveTutorialDisableStep()
	{
		RemoveComponent(207);
	}

	public void AddTutorialEnableStep(TutorialBaseStep newStep)
	{
		int index = 208;
		TutorialEnableStepComponent tutorialEnableStepComponent = CreateComponent<TutorialEnableStepComponent>(index);
		tutorialEnableStepComponent.Step = newStep;
		AddComponent(index, tutorialEnableStepComponent);
	}

	public void ReplaceTutorialEnableStep(TutorialBaseStep newStep)
	{
		int index = 208;
		TutorialEnableStepComponent tutorialEnableStepComponent = CreateComponent<TutorialEnableStepComponent>(index);
		tutorialEnableStepComponent.Step = newStep;
		ReplaceComponent(index, tutorialEnableStepComponent);
	}

	public void RemoveTutorialEnableStep()
	{
		RemoveComponent(208);
	}

	public void AddTutorialOutgameUnlockLevelsBalancingData(Dictionary<TutorialOutgameLevel, TutorialOutgameUnlockLevelData> newMap)
	{
		int index = 210;
		TutorialOutgameUnlockLevelsBalancingDataComponent tutorialOutgameUnlockLevelsBalancingDataComponent = CreateComponent<TutorialOutgameUnlockLevelsBalancingDataComponent>(index);
		tutorialOutgameUnlockLevelsBalancingDataComponent.Map = newMap;
		AddComponent(index, tutorialOutgameUnlockLevelsBalancingDataComponent);
	}

	public void ReplaceTutorialOutgameUnlockLevelsBalancingData(Dictionary<TutorialOutgameLevel, TutorialOutgameUnlockLevelData> newMap)
	{
		int index = 210;
		TutorialOutgameUnlockLevelsBalancingDataComponent tutorialOutgameUnlockLevelsBalancingDataComponent = CreateComponent<TutorialOutgameUnlockLevelsBalancingDataComponent>(index);
		tutorialOutgameUnlockLevelsBalancingDataComponent.Map = newMap;
		ReplaceComponent(index, tutorialOutgameUnlockLevelsBalancingDataComponent);
	}

	public void RemoveTutorialOutgameUnlockLevelsBalancingData()
	{
		RemoveComponent(210);
	}

	public void AddTutorialPossibleGroups(Dictionary<string, List<TutorialBaseStep>> newStepsGroups)
	{
		int index = 211;
		TutorialPossibleGroupsComponent tutorialPossibleGroupsComponent = CreateComponent<TutorialPossibleGroupsComponent>(index);
		tutorialPossibleGroupsComponent.StepsGroups = newStepsGroups;
		AddComponent(index, tutorialPossibleGroupsComponent);
	}

	public void ReplaceTutorialPossibleGroups(Dictionary<string, List<TutorialBaseStep>> newStepsGroups)
	{
		int index = 211;
		TutorialPossibleGroupsComponent tutorialPossibleGroupsComponent = CreateComponent<TutorialPossibleGroupsComponent>(index);
		tutorialPossibleGroupsComponent.StepsGroups = newStepsGroups;
		ReplaceComponent(index, tutorialPossibleGroupsComponent);
	}

	public void RemoveTutorialPossibleGroups()
	{
		RemoveComponent(211);
	}

	public void AddTutorialStepDisableTriggered(TutorialBaseStep newTutorialStep)
	{
		int index = 212;
		TutorialStepDisableTriggeredComponent tutorialStepDisableTriggeredComponent = CreateComponent<TutorialStepDisableTriggeredComponent>(index);
		tutorialStepDisableTriggeredComponent.TutorialStep = newTutorialStep;
		AddComponent(index, tutorialStepDisableTriggeredComponent);
	}

	public void ReplaceTutorialStepDisableTriggered(TutorialBaseStep newTutorialStep)
	{
		int index = 212;
		TutorialStepDisableTriggeredComponent tutorialStepDisableTriggeredComponent = CreateComponent<TutorialStepDisableTriggeredComponent>(index);
		tutorialStepDisableTriggeredComponent.TutorialStep = newTutorialStep;
		ReplaceComponent(index, tutorialStepDisableTriggeredComponent);
	}

	public void RemoveTutorialStepDisableTriggered()
	{
		RemoveComponent(212);
	}

	public void AddTutorialStepEnableTriggered(TutorialBaseStep newTutorialStep)
	{
		int index = 213;
		TutorialStepEnableTriggeredComponent tutorialStepEnableTriggeredComponent = CreateComponent<TutorialStepEnableTriggeredComponent>(index);
		tutorialStepEnableTriggeredComponent.TutorialStep = newTutorialStep;
		AddComponent(index, tutorialStepEnableTriggeredComponent);
	}

	public void ReplaceTutorialStepEnableTriggered(TutorialBaseStep newTutorialStep)
	{
		int index = 213;
		TutorialStepEnableTriggeredComponent tutorialStepEnableTriggeredComponent = CreateComponent<TutorialStepEnableTriggeredComponent>(index);
		tutorialStepEnableTriggeredComponent.TutorialStep = newTutorialStep;
		ReplaceComponent(index, tutorialStepEnableTriggeredComponent);
	}

	public void RemoveTutorialStepEnableTriggered()
	{
		RemoveComponent(213);
	}

	public void AddTutorialStepsQueue(Queue<TutorialBaseStep> newStepsQueue)
	{
		int index = 214;
		TutorialStepsQueueComponent tutorialStepsQueueComponent = CreateComponent<TutorialStepsQueueComponent>(index);
		tutorialStepsQueueComponent.stepsQueue = newStepsQueue;
		AddComponent(index, tutorialStepsQueueComponent);
	}

	public void ReplaceTutorialStepsQueue(Queue<TutorialBaseStep> newStepsQueue)
	{
		int index = 214;
		TutorialStepsQueueComponent tutorialStepsQueueComponent = CreateComponent<TutorialStepsQueueComponent>(index);
		tutorialStepsQueueComponent.stepsQueue = newStepsQueue;
		ReplaceComponent(index, tutorialStepsQueueComponent);
	}

	public void RemoveTutorialStepsQueue()
	{
		RemoveComponent(214);
	}

	public void AddTutorialTargetElement(RectTransform newTarget)
	{
		int index = 215;
		TutorialTargetElement tutorialTargetElement = CreateComponent<TutorialTargetElement>(index);
		tutorialTargetElement.Target = newTarget;
		AddComponent(index, tutorialTargetElement);
	}

	public void ReplaceTutorialTargetElement(RectTransform newTarget)
	{
		int index = 215;
		TutorialTargetElement tutorialTargetElement = CreateComponent<TutorialTargetElement>(index);
		tutorialTargetElement.Target = newTarget;
		ReplaceComponent(index, tutorialTargetElement);
	}

	public void RemoveTutorialTargetElement()
	{
		RemoveComponent(215);
	}

	public void AddTutorialTriggersList(List<TutorialTrigger> newTriggers)
	{
		int index = 216;
		TutorialTriggersListComponent tutorialTriggersListComponent = CreateComponent<TutorialTriggersListComponent>(index);
		tutorialTriggersListComponent.Triggers = newTriggers;
		AddComponent(index, tutorialTriggersListComponent);
	}

	public void ReplaceTutorialTriggersList(List<TutorialTrigger> newTriggers)
	{
		int index = 216;
		TutorialTriggersListComponent tutorialTriggersListComponent = CreateComponent<TutorialTriggersListComponent>(index);
		tutorialTriggersListComponent.Triggers = newTriggers;
		ReplaceComponent(index, tutorialTriggersListComponent);
	}

	public void RemoveTutorialTriggersList()
	{
		RemoveComponent(216);
	}

	public void AddUDIDHash(string newUDID)
	{
		int index = 217;
		UDIDHashComponent uDIDHashComponent = CreateComponent<UDIDHashComponent>(index);
		uDIDHashComponent.UDID = newUDID;
		AddComponent(index, uDIDHashComponent);
	}

	public void ReplaceUDIDHash(string newUDID)
	{
		int index = 217;
		UDIDHashComponent uDIDHashComponent = CreateComponent<UDIDHashComponent>(index);
		uDIDHashComponent.UDID = newUDID;
		ReplaceComponent(index, uDIDHashComponent);
	}

	public void RemoveUDIDHash()
	{
		RemoveComponent(217);
	}

	public void AddUpgradeCharacter(int newCharacterId)
	{
		int index = 218;
		UpgradeCharacterComponent upgradeCharacterComponent = CreateComponent<UpgradeCharacterComponent>(index);
		upgradeCharacterComponent.CharacterId = newCharacterId;
		AddComponent(index, upgradeCharacterComponent);
	}

	public void ReplaceUpgradeCharacter(int newCharacterId)
	{
		int index = 218;
		UpgradeCharacterComponent upgradeCharacterComponent = CreateComponent<UpgradeCharacterComponent>(index);
		upgradeCharacterComponent.CharacterId = newCharacterId;
		ReplaceComponent(index, upgradeCharacterComponent);
	}

	public void RemoveUpgradeCharacter()
	{
		RemoveComponent(218);
	}

	public void AddUpgradeCompanion(int newCompanionId)
	{
		int index = 219;
		UpgradeCompanionComponent upgradeCompanionComponent = CreateComponent<UpgradeCompanionComponent>(index);
		upgradeCompanionComponent.CompanionId = newCompanionId;
		AddComponent(index, upgradeCompanionComponent);
	}

	public void ReplaceUpgradeCompanion(int newCompanionId)
	{
		int index = 219;
		UpgradeCompanionComponent upgradeCompanionComponent = CreateComponent<UpgradeCompanionComponent>(index);
		upgradeCompanionComponent.CompanionId = newCompanionId;
		ReplaceComponent(index, upgradeCompanionComponent);
	}

	public void RemoveUpgradeCompanion()
	{
		RemoveComponent(219);
	}

	public void AddUserStateLastUpdateTime(DateTime newTime)
	{
		int index = 221;
		UserStateLastUpdateTimeComponent userStateLastUpdateTimeComponent = CreateComponent<UserStateLastUpdateTimeComponent>(index);
		userStateLastUpdateTimeComponent.Time = newTime;
		AddComponent(index, userStateLastUpdateTimeComponent);
	}

	public void ReplaceUserStateLastUpdateTime(DateTime newTime)
	{
		int index = 221;
		UserStateLastUpdateTimeComponent userStateLastUpdateTimeComponent = CreateComponent<UserStateLastUpdateTimeComponent>(index);
		userStateLastUpdateTimeComponent.Time = newTime;
		ReplaceComponent(index, userStateLastUpdateTimeComponent);
	}

	public void RemoveUserStateLastUpdateTime()
	{
		RemoveComponent(221);
	}

	public void AddUserStateSolveConflict(bool newKeepLocalData)
	{
		int index = 223;
		UserStateSolveConflictComponent userStateSolveConflictComponent = CreateComponent<UserStateSolveConflictComponent>(index);
		userStateSolveConflictComponent.KeepLocalData = newKeepLocalData;
		AddComponent(index, userStateSolveConflictComponent);
	}

	public void ReplaceUserStateSolveConflict(bool newKeepLocalData)
	{
		int index = 223;
		UserStateSolveConflictComponent userStateSolveConflictComponent = CreateComponent<UserStateSolveConflictComponent>(index);
		userStateSolveConflictComponent.KeepLocalData = newKeepLocalData;
		ReplaceComponent(index, userStateSolveConflictComponent);
	}

	public void RemoveUserStateSolveConflict()
	{
		RemoveComponent(223);
	}
}
