using Entitas;

public sealed class GameMatcher
{
	private static IMatcher<GameEntity> _matcherAbilityChangedListener;

	private static IMatcher<GameEntity> _matcherAbilityChargingProgress;

	private static IMatcher<GameEntity> _matcherAbilityChargingTime;

	private static IMatcher<GameEntity> _matcherAbilityCreateToken;

	private static IMatcher<GameEntity> _matcherAbilityDragonBlast;

	private static IMatcher<GameEntity> _matcherAbilityDuration;

	private static IMatcher<GameEntity> _matcherAbilityFasterMovement;

	private static IMatcher<GameEntity> _matcherAbilityGiantSpeedup;

	private static IMatcher<GameEntity> _matcherAbilityMagnetized;

	private static IMatcher<GameEntity> _matcherAbilityRandomPowerup;

	private static IMatcher<GameEntity> _matcherAbilityResurrect;

	private static IMatcher<GameEntity> _matcherAbilityRun;

	private static IMatcher<GameEntity> _matcherAbilityScoreBonus;

	private static IMatcher<GameEntity> _matcherAbilityShield;

	private static IMatcher<GameEntity> _matcherAbilitySpeedup;

	private static IMatcher<GameEntity> _matcherAbilityStateChangedListener;

	private static IMatcher<GameEntity> _matcherAbilityTimer;

	private static IMatcher<GameEntity> _matcherAbilityUpgradeScore;

	private static IMatcher<GameEntity> _matcherAngryRun;

	private static IMatcher<GameEntity> _matcherAsset;

	private static IMatcher<GameEntity> _matcherAsyncRequest;

	private static IMatcher<GameEntity> _matcherCameraAttached;

	private static IMatcher<GameEntity> _matcherCameraStop;

	private static IMatcher<GameEntity> _matcherCandyStore;

	private static IMatcher<GameEntity> _matcherChangeCameraSizeTrigger;

	private static IMatcher<GameEntity> _matcherChangeScaleTime;

	private static IMatcher<GameEntity> _matcherChangeWorldSkin;

	private static IMatcher<GameEntity> _matcherChangeWorldSpeed;

	private static IMatcher<GameEntity> _matcherCloud;

	private static IMatcher<GameEntity> _matcherCoins;

	private static IMatcher<GameEntity> _matcherCoinsListener;

	private static IMatcher<GameEntity> _matcherCollected;

	private static IMatcher<GameEntity> _matcherCollectibleCoin;

	private static IMatcher<GameEntity> _matcherCollectible;

	private static IMatcher<GameEntity> _matcherCollectibleData;

	private static IMatcher<GameEntity> _matcherCollectibleGiantToken;

	private static IMatcher<GameEntity> _matcherCollectibleMagnet;

	private static IMatcher<GameEntity> _matcherCollectibleScore;

	private static IMatcher<GameEntity> _matcherCollectibleScoreMultiplier;

	private static IMatcher<GameEntity> _matcherCollectibleSpeedUp;

	private static IMatcher<GameEntity> _matcherCollectibleStamina;

	private static IMatcher<GameEntity> _matcherCollectibleUpgradeScore;

	private static IMatcher<GameEntity> _matcherCollectSound;

	private static IMatcher<GameEntity> _matcherCompanion;

	private static IMatcher<GameEntity> _matcherDailyMissionCollect;

	private static IMatcher<GameEntity> _matcherDailyMissionDestroyObstacle;

	private static IMatcher<GameEntity> _matcherDailyMissionPlayMissions;

	private static IMatcher<GameEntity> _matcherDead;

	private static IMatcher<GameEntity> _matcherDestroyed;

	private static IMatcher<GameEntity> _matcherDestroyZoned;

	private static IMatcher<GameEntity> _matcherDisableColliders;

	private static IMatcher<GameEntity> _matcherDropping;

	private static IMatcher<GameEntity> _matcherFadeOut;

	private static IMatcher<GameEntity> _matcherFloating;

	private static IMatcher<GameEntity> _matcherFlyAway;

	private static IMatcher<GameEntity> _matcherGainedScore;

	private static IMatcher<GameEntity> _matcherGameStarted;

	private static IMatcher<GameEntity> _matcherGodMode;

	private static IMatcher<GameEntity> _matcherGrounded;

	private static IMatcher<GameEntity> _matcherImprovedScoreToken;

	private static IMatcher<GameEntity> _matcherIngameBg;

	private static IMatcher<GameEntity> _matcherInitialTimeScale;

	private static IMatcher<GameEntity> _matcherInitialTrigger;

	private static IMatcher<GameEntity> _matcherInvulnerability;

	private static IMatcher<GameEntity> _matcherJumping;

	private static IMatcher<GameEntity> _matcherKillZone;

	private static IMatcher<GameEntity> _matcherLevelEnd;

	private static IMatcher<GameEntity> _matcherLevelFinished;

	private static IMatcher<GameEntity> _matcherLevelFinishedListener;

	private static IMatcher<GameEntity> _matcherLevelSettings;

	private static IMatcher<GameEntity> _matcherLoadAssetAsync;

	private static IMatcher<GameEntity> _matcherMagnetized;

	private static IMatcher<GameEntity> _matcherMissionChestGranted;

	private static IMatcher<GameEntity> _matcherMissionCollect;

	private static IMatcher<GameEntity> _matcherMissionCounter;

	private static IMatcher<GameEntity> _matcherMissionData;

	private static IMatcher<GameEntity> _matcherMissionResult;

	private static IMatcher<GameEntity> _matcherMissionSprite;

	private static IMatcher<GameEntity> _matcherMissionSpriteFillAmount;

	private static IMatcher<GameEntity> _matcherMissionThresholds;

	private static IMatcher<GameEntity> _matcherMissionType;

	private static IMatcher<GameEntity> _matcherMissionUpdateListener;

	private static IMatcher<GameEntity> _matcherMovable;

	private static IMatcher<GameEntity> _matcherMovableData;

	private static IMatcher<GameEntity> _matcherNewWorldSkin;

	private static IMatcher<GameEntity> _matcherObstacle;

	private static IMatcher<GameEntity> _matcherObstacleHitListener;

	private static IMatcher<GameEntity> _matcherOffsetPosition;

	private static IMatcher<GameEntity> _matcherOnClouds;

	private static IMatcher<GameEntity> _matcherPaused;

	private static IMatcher<GameEntity> _matcherPauseListener;

	private static IMatcher<GameEntity> _matcherPlatform;

	private static IMatcher<GameEntity> _matcherPlayerAttached;

	private static IMatcher<GameEntity> _matcherPlayerBlinking;

	private static IMatcher<GameEntity> _matcherPlayer;

	private static IMatcher<GameEntity> _matcherPlayerLandListener;

	private static IMatcher<GameEntity> _matcherPlayerRelay;

	private static IMatcher<GameEntity> _matcherPlayerResurrect;

	private static IMatcher<GameEntity> _matcherPlayerRevive;

	private static IMatcher<GameEntity> _matcherPlayGameFailMusic;

	private static IMatcher<GameEntity> _matcherPlayGameMusic;

	private static IMatcher<GameEntity> _matcherPlayGameWinMusic;

	private static IMatcher<GameEntity> _matcherPlaySoundsListener;

	private static IMatcher<GameEntity> _matcherPoolable;

	private static IMatcher<GameEntity> _matcherPoolContainer;

	private static IMatcher<GameEntity> _matcherPoolDespawned;

	private static IMatcher<GameEntity> _matcherPoolSpawned;

	private static IMatcher<GameEntity> _matcherPosition;

	private static IMatcher<GameEntity> _matcherRaccoon;

	private static IMatcher<GameEntity> _matcherRaccoonDropAnimation;

	private static IMatcher<GameEntity> _matcherRaccoonIntro;

	private static IMatcher<GameEntity> _matcherRaccoonIntrosListener;

	private static IMatcher<GameEntity> _matcherRaccoonOutro;

	private static IMatcher<GameEntity> _matcherReplayLevel;

	private static IMatcher<GameEntity> _matcherResetPoolable;

	private static IMatcher<GameEntity> _matcherRestableArea;

	private static IMatcher<GameEntity> _matcherResting;

	private static IMatcher<GameEntity> _matcherResurectionsDone;

	private static IMatcher<GameEntity> _matcherReturnToHomeLevel;

	private static IMatcher<GameEntity> _matcherRevivesDone;

	private static IMatcher<GameEntity> _matcherRunDuration;

	private static IMatcher<GameEntity> _matcherScore;

	private static IMatcher<GameEntity> _matcherScoreListener;

	private static IMatcher<GameEntity> _matcherScoreMultiplier;

	private static IMatcher<GameEntity> _matcherScoreToken;

	private static IMatcher<GameEntity> _matcherSelectedCharacter;

	private static IMatcher<GameEntity> _matcherSetupAbility;

	private static IMatcher<GameEntity> _matcherShaking;

	private static IMatcher<GameEntity> _matcherSizeIncreased;

	private static IMatcher<GameEntity> _matcherSliding;

	private static IMatcher<GameEntity> _matcherSparkle;

	private static IMatcher<GameEntity> _matcherSpecialRun;

	private static IMatcher<GameEntity> _matcherSpeedData;

	private static IMatcher<GameEntity> _matcherSpeedUp;

	private static IMatcher<GameEntity> _matcherStamina;

	private static IMatcher<GameEntity> _matcherStaminasListener;

	private static IMatcher<GameEntity> _matcherSwingingData;

	private static IMatcher<GameEntity> _matcherTutorialPerformed;

	private static IMatcher<GameEntity> _matcherTutorialPlayStep;

	private static IMatcher<GameEntity> _matcherTutorialsListener;

	private static IMatcher<GameEntity> _matcherTutorialTrigger;

	private static IMatcher<GameEntity> _matcherUnderTheScreen;

	private static IMatcher<GameEntity> _matcherUpgradeScore;

	private static IMatcher<GameEntity> _matcherUpgradeSingleScore;

	private static IMatcher<GameEntity> _matcherView;

	private static IMatcher<GameEntity> _matcherWaitToStart;

	private static IMatcher<GameEntity> _matcherWorld;

	private static IMatcher<GameEntity> _matcherWorldTimeScale;

	public static IMatcher<GameEntity> AbilityChangedListener
	{
		get
		{
			if (_matcherAbilityChangedListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(default(int));
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityChangedListener = matcher;
			}
			return _matcherAbilityChangedListener;
		}
	}

	public static IMatcher<GameEntity> AbilityChargingProgress
	{
		get
		{
			if (_matcherAbilityChargingProgress == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(1);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityChargingProgress = matcher;
			}
			return _matcherAbilityChargingProgress;
		}
	}

	public static IMatcher<GameEntity> AbilityChargingTime
	{
		get
		{
			if (_matcherAbilityChargingTime == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(2);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityChargingTime = matcher;
			}
			return _matcherAbilityChargingTime;
		}
	}

	public static IMatcher<GameEntity> AbilityCreateToken
	{
		get
		{
			if (_matcherAbilityCreateToken == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(3);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityCreateToken = matcher;
			}
			return _matcherAbilityCreateToken;
		}
	}

	public static IMatcher<GameEntity> AbilityDragonBlast
	{
		get
		{
			if (_matcherAbilityDragonBlast == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(4);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityDragonBlast = matcher;
			}
			return _matcherAbilityDragonBlast;
		}
	}

	public static IMatcher<GameEntity> AbilityDuration
	{
		get
		{
			if (_matcherAbilityDuration == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(5);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityDuration = matcher;
			}
			return _matcherAbilityDuration;
		}
	}

	public static IMatcher<GameEntity> AbilityFasterMovement
	{
		get
		{
			if (_matcherAbilityFasterMovement == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(6);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityFasterMovement = matcher;
			}
			return _matcherAbilityFasterMovement;
		}
	}

	public static IMatcher<GameEntity> AbilityGiantSpeedup
	{
		get
		{
			if (_matcherAbilityGiantSpeedup == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(7);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityGiantSpeedup = matcher;
			}
			return _matcherAbilityGiantSpeedup;
		}
	}

	public static IMatcher<GameEntity> AbilityMagnetized
	{
		get
		{
			if (_matcherAbilityMagnetized == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(8);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityMagnetized = matcher;
			}
			return _matcherAbilityMagnetized;
		}
	}

	public static IMatcher<GameEntity> AbilityRandomPowerup
	{
		get
		{
			if (_matcherAbilityRandomPowerup == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(9);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityRandomPowerup = matcher;
			}
			return _matcherAbilityRandomPowerup;
		}
	}

	public static IMatcher<GameEntity> AbilityResurrect
	{
		get
		{
			if (_matcherAbilityResurrect == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(10);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityResurrect = matcher;
			}
			return _matcherAbilityResurrect;
		}
	}

	public static IMatcher<GameEntity> AbilityRun
	{
		get
		{
			if (_matcherAbilityRun == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(11);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityRun = matcher;
			}
			return _matcherAbilityRun;
		}
	}

	public static IMatcher<GameEntity> AbilityScoreBonus
	{
		get
		{
			if (_matcherAbilityScoreBonus == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(12);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityScoreBonus = matcher;
			}
			return _matcherAbilityScoreBonus;
		}
	}

	public static IMatcher<GameEntity> AbilityShield
	{
		get
		{
			if (_matcherAbilityShield == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(13);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityShield = matcher;
			}
			return _matcherAbilityShield;
		}
	}

	public static IMatcher<GameEntity> AbilitySpeedup
	{
		get
		{
			if (_matcherAbilitySpeedup == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(14);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilitySpeedup = matcher;
			}
			return _matcherAbilitySpeedup;
		}
	}

	public static IMatcher<GameEntity> AbilityStateChangedListener
	{
		get
		{
			if (_matcherAbilityStateChangedListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(15);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityStateChangedListener = matcher;
			}
			return _matcherAbilityStateChangedListener;
		}
	}

	public static IMatcher<GameEntity> AbilityTimer
	{
		get
		{
			if (_matcherAbilityTimer == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(16);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityTimer = matcher;
			}
			return _matcherAbilityTimer;
		}
	}

	public static IMatcher<GameEntity> AbilityUpgradeScore
	{
		get
		{
			if (_matcherAbilityUpgradeScore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(17);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAbilityUpgradeScore = matcher;
			}
			return _matcherAbilityUpgradeScore;
		}
	}

	public static IMatcher<GameEntity> AngryRun
	{
		get
		{
			if (_matcherAngryRun == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(18);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAngryRun = matcher;
			}
			return _matcherAngryRun;
		}
	}

	public static IMatcher<GameEntity> Asset
	{
		get
		{
			if (_matcherAsset == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(19);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAsset = matcher;
			}
			return _matcherAsset;
		}
	}

	public static IMatcher<GameEntity> AsyncRequest
	{
		get
		{
			if (_matcherAsyncRequest == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(20);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherAsyncRequest = matcher;
			}
			return _matcherAsyncRequest;
		}
	}

	public static IMatcher<GameEntity> CameraAttached
	{
		get
		{
			if (_matcherCameraAttached == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(21);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCameraAttached = matcher;
			}
			return _matcherCameraAttached;
		}
	}

	public static IMatcher<GameEntity> CameraStop
	{
		get
		{
			if (_matcherCameraStop == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(22);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCameraStop = matcher;
			}
			return _matcherCameraStop;
		}
	}

	public static IMatcher<GameEntity> CandyStore
	{
		get
		{
			if (_matcherCandyStore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(23);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCandyStore = matcher;
			}
			return _matcherCandyStore;
		}
	}

	public static IMatcher<GameEntity> ChangeCameraSizeTrigger
	{
		get
		{
			if (_matcherChangeCameraSizeTrigger == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(24);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherChangeCameraSizeTrigger = matcher;
			}
			return _matcherChangeCameraSizeTrigger;
		}
	}

	public static IMatcher<GameEntity> ChangeScaleTime
	{
		get
		{
			if (_matcherChangeScaleTime == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(25);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherChangeScaleTime = matcher;
			}
			return _matcherChangeScaleTime;
		}
	}

	public static IMatcher<GameEntity> ChangeWorldSkin
	{
		get
		{
			if (_matcherChangeWorldSkin == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(26);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherChangeWorldSkin = matcher;
			}
			return _matcherChangeWorldSkin;
		}
	}

	public static IMatcher<GameEntity> ChangeWorldSpeed
	{
		get
		{
			if (_matcherChangeWorldSpeed == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(27);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherChangeWorldSpeed = matcher;
			}
			return _matcherChangeWorldSpeed;
		}
	}

	public static IMatcher<GameEntity> Cloud
	{
		get
		{
			if (_matcherCloud == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(28);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCloud = matcher;
			}
			return _matcherCloud;
		}
	}

	public static IMatcher<GameEntity> Coins
	{
		get
		{
			if (_matcherCoins == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(29);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCoins = matcher;
			}
			return _matcherCoins;
		}
	}

	public static IMatcher<GameEntity> CoinsListener
	{
		get
		{
			if (_matcherCoinsListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(30);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCoinsListener = matcher;
			}
			return _matcherCoinsListener;
		}
	}

	public static IMatcher<GameEntity> Collected
	{
		get
		{
			if (_matcherCollected == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(31);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollected = matcher;
			}
			return _matcherCollected;
		}
	}

	public static IMatcher<GameEntity> CollectibleCoin
	{
		get
		{
			if (_matcherCollectibleCoin == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(32);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleCoin = matcher;
			}
			return _matcherCollectibleCoin;
		}
	}

	public static IMatcher<GameEntity> Collectible
	{
		get
		{
			if (_matcherCollectible == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(33);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectible = matcher;
			}
			return _matcherCollectible;
		}
	}

	public static IMatcher<GameEntity> CollectibleData
	{
		get
		{
			if (_matcherCollectibleData == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(34);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleData = matcher;
			}
			return _matcherCollectibleData;
		}
	}

	public static IMatcher<GameEntity> CollectibleGiantToken
	{
		get
		{
			if (_matcherCollectibleGiantToken == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(35);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleGiantToken = matcher;
			}
			return _matcherCollectibleGiantToken;
		}
	}

	public static IMatcher<GameEntity> CollectibleMagnet
	{
		get
		{
			if (_matcherCollectibleMagnet == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(36);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleMagnet = matcher;
			}
			return _matcherCollectibleMagnet;
		}
	}

	public static IMatcher<GameEntity> CollectibleScore
	{
		get
		{
			if (_matcherCollectibleScore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(37);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleScore = matcher;
			}
			return _matcherCollectibleScore;
		}
	}

	public static IMatcher<GameEntity> CollectibleScoreMultiplier
	{
		get
		{
			if (_matcherCollectibleScoreMultiplier == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(38);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleScoreMultiplier = matcher;
			}
			return _matcherCollectibleScoreMultiplier;
		}
	}

	public static IMatcher<GameEntity> CollectibleSpeedUp
	{
		get
		{
			if (_matcherCollectibleSpeedUp == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(39);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleSpeedUp = matcher;
			}
			return _matcherCollectibleSpeedUp;
		}
	}

	public static IMatcher<GameEntity> CollectibleStamina
	{
		get
		{
			if (_matcherCollectibleStamina == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(40);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleStamina = matcher;
			}
			return _matcherCollectibleStamina;
		}
	}

	public static IMatcher<GameEntity> CollectibleUpgradeScore
	{
		get
		{
			if (_matcherCollectibleUpgradeScore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(41);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectibleUpgradeScore = matcher;
			}
			return _matcherCollectibleUpgradeScore;
		}
	}

	public static IMatcher<GameEntity> CollectSound
	{
		get
		{
			if (_matcherCollectSound == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(42);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCollectSound = matcher;
			}
			return _matcherCollectSound;
		}
	}

	public static IMatcher<GameEntity> Companion
	{
		get
		{
			if (_matcherCompanion == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(43);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherCompanion = matcher;
			}
			return _matcherCompanion;
		}
	}

	public static IMatcher<GameEntity> DailyMissionCollect
	{
		get
		{
			if (_matcherDailyMissionCollect == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(44);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDailyMissionCollect = matcher;
			}
			return _matcherDailyMissionCollect;
		}
	}

	public static IMatcher<GameEntity> DailyMissionDestroyObstacle
	{
		get
		{
			if (_matcherDailyMissionDestroyObstacle == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(45);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDailyMissionDestroyObstacle = matcher;
			}
			return _matcherDailyMissionDestroyObstacle;
		}
	}

	public static IMatcher<GameEntity> DailyMissionPlayMissions
	{
		get
		{
			if (_matcherDailyMissionPlayMissions == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(46);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDailyMissionPlayMissions = matcher;
			}
			return _matcherDailyMissionPlayMissions;
		}
	}

	public static IMatcher<GameEntity> Dead
	{
		get
		{
			if (_matcherDead == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(47);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDead = matcher;
			}
			return _matcherDead;
		}
	}

	public static IMatcher<GameEntity> Destroyed
	{
		get
		{
			if (_matcherDestroyed == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(48);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDestroyed = matcher;
			}
			return _matcherDestroyed;
		}
	}

	public static IMatcher<GameEntity> DestroyZoned
	{
		get
		{
			if (_matcherDestroyZoned == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(49);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDestroyZoned = matcher;
			}
			return _matcherDestroyZoned;
		}
	}

	public static IMatcher<GameEntity> DisableColliders
	{
		get
		{
			if (_matcherDisableColliders == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(50);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDisableColliders = matcher;
			}
			return _matcherDisableColliders;
		}
	}

	public static IMatcher<GameEntity> Dropping
	{
		get
		{
			if (_matcherDropping == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(51);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherDropping = matcher;
			}
			return _matcherDropping;
		}
	}

	public static IMatcher<GameEntity> FadeOut
	{
		get
		{
			if (_matcherFadeOut == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(52);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherFadeOut = matcher;
			}
			return _matcherFadeOut;
		}
	}

	public static IMatcher<GameEntity> Floating
	{
		get
		{
			if (_matcherFloating == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(53);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherFloating = matcher;
			}
			return _matcherFloating;
		}
	}

	public static IMatcher<GameEntity> FlyAway
	{
		get
		{
			if (_matcherFlyAway == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(54);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherFlyAway = matcher;
			}
			return _matcherFlyAway;
		}
	}

	public static IMatcher<GameEntity> GainedScore
	{
		get
		{
			if (_matcherGainedScore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(55);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherGainedScore = matcher;
			}
			return _matcherGainedScore;
		}
	}

	public static IMatcher<GameEntity> GameStarted
	{
		get
		{
			if (_matcherGameStarted == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(56);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherGameStarted = matcher;
			}
			return _matcherGameStarted;
		}
	}

	public static IMatcher<GameEntity> GodMode
	{
		get
		{
			if (_matcherGodMode == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(57);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherGodMode = matcher;
			}
			return _matcherGodMode;
		}
	}

	public static IMatcher<GameEntity> Grounded
	{
		get
		{
			if (_matcherGrounded == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(58);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherGrounded = matcher;
			}
			return _matcherGrounded;
		}
	}

	public static IMatcher<GameEntity> ImprovedScoreToken
	{
		get
		{
			if (_matcherImprovedScoreToken == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(59);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherImprovedScoreToken = matcher;
			}
			return _matcherImprovedScoreToken;
		}
	}

	public static IMatcher<GameEntity> IngameBg
	{
		get
		{
			if (_matcherIngameBg == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(60);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherIngameBg = matcher;
			}
			return _matcherIngameBg;
		}
	}

	public static IMatcher<GameEntity> InitialTimeScale
	{
		get
		{
			if (_matcherInitialTimeScale == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(61);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherInitialTimeScale = matcher;
			}
			return _matcherInitialTimeScale;
		}
	}

	public static IMatcher<GameEntity> InitialTrigger
	{
		get
		{
			if (_matcherInitialTrigger == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(62);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherInitialTrigger = matcher;
			}
			return _matcherInitialTrigger;
		}
	}

	public static IMatcher<GameEntity> Invulnerability
	{
		get
		{
			if (_matcherInvulnerability == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(63);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherInvulnerability = matcher;
			}
			return _matcherInvulnerability;
		}
	}

	public static IMatcher<GameEntity> Jumping
	{
		get
		{
			if (_matcherJumping == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(64);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherJumping = matcher;
			}
			return _matcherJumping;
		}
	}

	public static IMatcher<GameEntity> KillZone
	{
		get
		{
			if (_matcherKillZone == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(65);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherKillZone = matcher;
			}
			return _matcherKillZone;
		}
	}

	public static IMatcher<GameEntity> LevelEnd
	{
		get
		{
			if (_matcherLevelEnd == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(66);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherLevelEnd = matcher;
			}
			return _matcherLevelEnd;
		}
	}

	public static IMatcher<GameEntity> LevelFinished
	{
		get
		{
			if (_matcherLevelFinished == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(67);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherLevelFinished = matcher;
			}
			return _matcherLevelFinished;
		}
	}

	public static IMatcher<GameEntity> LevelFinishedListener
	{
		get
		{
			if (_matcherLevelFinishedListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(68);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherLevelFinishedListener = matcher;
			}
			return _matcherLevelFinishedListener;
		}
	}

	public static IMatcher<GameEntity> LevelSettings
	{
		get
		{
			if (_matcherLevelSettings == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(69);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherLevelSettings = matcher;
			}
			return _matcherLevelSettings;
		}
	}

	public static IMatcher<GameEntity> LoadAssetAsync
	{
		get
		{
			if (_matcherLoadAssetAsync == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(70);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherLoadAssetAsync = matcher;
			}
			return _matcherLoadAssetAsync;
		}
	}

	public static IMatcher<GameEntity> Magnetized
	{
		get
		{
			if (_matcherMagnetized == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(71);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMagnetized = matcher;
			}
			return _matcherMagnetized;
		}
	}

	public static IMatcher<GameEntity> MissionChestGranted
	{
		get
		{
			if (_matcherMissionChestGranted == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(72);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionChestGranted = matcher;
			}
			return _matcherMissionChestGranted;
		}
	}

	public static IMatcher<GameEntity> MissionCollect
	{
		get
		{
			if (_matcherMissionCollect == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(73);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionCollect = matcher;
			}
			return _matcherMissionCollect;
		}
	}

	public static IMatcher<GameEntity> MissionCounter
	{
		get
		{
			if (_matcherMissionCounter == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(74);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionCounter = matcher;
			}
			return _matcherMissionCounter;
		}
	}

	public static IMatcher<GameEntity> MissionData
	{
		get
		{
			if (_matcherMissionData == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(75);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionData = matcher;
			}
			return _matcherMissionData;
		}
	}

	public static IMatcher<GameEntity> MissionResult
	{
		get
		{
			if (_matcherMissionResult == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(76);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionResult = matcher;
			}
			return _matcherMissionResult;
		}
	}

	public static IMatcher<GameEntity> MissionSprite
	{
		get
		{
			if (_matcherMissionSprite == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(77);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionSprite = matcher;
			}
			return _matcherMissionSprite;
		}
	}

	public static IMatcher<GameEntity> MissionSpriteFillAmount
	{
		get
		{
			if (_matcherMissionSpriteFillAmount == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(78);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionSpriteFillAmount = matcher;
			}
			return _matcherMissionSpriteFillAmount;
		}
	}

	public static IMatcher<GameEntity> MissionThresholds
	{
		get
		{
			if (_matcherMissionThresholds == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(79);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionThresholds = matcher;
			}
			return _matcherMissionThresholds;
		}
	}

	public static IMatcher<GameEntity> MissionType
	{
		get
		{
			if (_matcherMissionType == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(80);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionType = matcher;
			}
			return _matcherMissionType;
		}
	}

	public static IMatcher<GameEntity> MissionUpdateListener
	{
		get
		{
			if (_matcherMissionUpdateListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(81);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMissionUpdateListener = matcher;
			}
			return _matcherMissionUpdateListener;
		}
	}

	public static IMatcher<GameEntity> Movable
	{
		get
		{
			if (_matcherMovable == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(82);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMovable = matcher;
			}
			return _matcherMovable;
		}
	}

	public static IMatcher<GameEntity> MovableData
	{
		get
		{
			if (_matcherMovableData == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(83);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherMovableData = matcher;
			}
			return _matcherMovableData;
		}
	}

	public static IMatcher<GameEntity> NewWorldSkin
	{
		get
		{
			if (_matcherNewWorldSkin == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(84);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherNewWorldSkin = matcher;
			}
			return _matcherNewWorldSkin;
		}
	}

	public static IMatcher<GameEntity> Obstacle
	{
		get
		{
			if (_matcherObstacle == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(85);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherObstacle = matcher;
			}
			return _matcherObstacle;
		}
	}

	public static IMatcher<GameEntity> ObstacleHitListener
	{
		get
		{
			if (_matcherObstacleHitListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(86);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherObstacleHitListener = matcher;
			}
			return _matcherObstacleHitListener;
		}
	}

	public static IMatcher<GameEntity> OffsetPosition
	{
		get
		{
			if (_matcherOffsetPosition == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(87);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherOffsetPosition = matcher;
			}
			return _matcherOffsetPosition;
		}
	}

	public static IMatcher<GameEntity> OnClouds
	{
		get
		{
			if (_matcherOnClouds == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(88);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherOnClouds = matcher;
			}
			return _matcherOnClouds;
		}
	}

	public static IMatcher<GameEntity> Paused
	{
		get
		{
			if (_matcherPaused == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(89);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPaused = matcher;
			}
			return _matcherPaused;
		}
	}

	public static IMatcher<GameEntity> PauseListener
	{
		get
		{
			if (_matcherPauseListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(90);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPauseListener = matcher;
			}
			return _matcherPauseListener;
		}
	}

	public static IMatcher<GameEntity> Platform
	{
		get
		{
			if (_matcherPlatform == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(91);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlatform = matcher;
			}
			return _matcherPlatform;
		}
	}

	public static IMatcher<GameEntity> PlayerAttached
	{
		get
		{
			if (_matcherPlayerAttached == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(92);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayerAttached = matcher;
			}
			return _matcherPlayerAttached;
		}
	}

	public static IMatcher<GameEntity> PlayerBlinking
	{
		get
		{
			if (_matcherPlayerBlinking == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(93);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayerBlinking = matcher;
			}
			return _matcherPlayerBlinking;
		}
	}

	public static IMatcher<GameEntity> Player
	{
		get
		{
			if (_matcherPlayer == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(94);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayer = matcher;
			}
			return _matcherPlayer;
		}
	}

	public static IMatcher<GameEntity> PlayerLandListener
	{
		get
		{
			if (_matcherPlayerLandListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(95);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayerLandListener = matcher;
			}
			return _matcherPlayerLandListener;
		}
	}

	public static IMatcher<GameEntity> PlayerRelay
	{
		get
		{
			if (_matcherPlayerRelay == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(96);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayerRelay = matcher;
			}
			return _matcherPlayerRelay;
		}
	}

	public static IMatcher<GameEntity> PlayerResurrect
	{
		get
		{
			if (_matcherPlayerResurrect == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(97);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayerResurrect = matcher;
			}
			return _matcherPlayerResurrect;
		}
	}

	public static IMatcher<GameEntity> PlayerRevive
	{
		get
		{
			if (_matcherPlayerRevive == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(98);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayerRevive = matcher;
			}
			return _matcherPlayerRevive;
		}
	}

	public static IMatcher<GameEntity> PlayGameFailMusic
	{
		get
		{
			if (_matcherPlayGameFailMusic == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(99);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayGameFailMusic = matcher;
			}
			return _matcherPlayGameFailMusic;
		}
	}

	public static IMatcher<GameEntity> PlayGameMusic
	{
		get
		{
			if (_matcherPlayGameMusic == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(100);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayGameMusic = matcher;
			}
			return _matcherPlayGameMusic;
		}
	}

	public static IMatcher<GameEntity> PlayGameWinMusic
	{
		get
		{
			if (_matcherPlayGameWinMusic == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(101);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlayGameWinMusic = matcher;
			}
			return _matcherPlayGameWinMusic;
		}
	}

	public static IMatcher<GameEntity> PlaySoundsListener
	{
		get
		{
			if (_matcherPlaySoundsListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(102);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPlaySoundsListener = matcher;
			}
			return _matcherPlaySoundsListener;
		}
	}

	public static IMatcher<GameEntity> Poolable
	{
		get
		{
			if (_matcherPoolable == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(103);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPoolable = matcher;
			}
			return _matcherPoolable;
		}
	}

	public static IMatcher<GameEntity> PoolContainer
	{
		get
		{
			if (_matcherPoolContainer == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(104);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPoolContainer = matcher;
			}
			return _matcherPoolContainer;
		}
	}

	public static IMatcher<GameEntity> PoolDespawned
	{
		get
		{
			if (_matcherPoolDespawned == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(105);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPoolDespawned = matcher;
			}
			return _matcherPoolDespawned;
		}
	}

	public static IMatcher<GameEntity> PoolSpawned
	{
		get
		{
			if (_matcherPoolSpawned == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(106);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPoolSpawned = matcher;
			}
			return _matcherPoolSpawned;
		}
	}

	public static IMatcher<GameEntity> Position
	{
		get
		{
			if (_matcherPosition == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(107);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherPosition = matcher;
			}
			return _matcherPosition;
		}
	}

	public static IMatcher<GameEntity> Raccoon
	{
		get
		{
			if (_matcherRaccoon == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(108);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRaccoon = matcher;
			}
			return _matcherRaccoon;
		}
	}

	public static IMatcher<GameEntity> RaccoonDropAnimation
	{
		get
		{
			if (_matcherRaccoonDropAnimation == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(109);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRaccoonDropAnimation = matcher;
			}
			return _matcherRaccoonDropAnimation;
		}
	}

	public static IMatcher<GameEntity> RaccoonIntro
	{
		get
		{
			if (_matcherRaccoonIntro == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(110);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRaccoonIntro = matcher;
			}
			return _matcherRaccoonIntro;
		}
	}

	public static IMatcher<GameEntity> RaccoonIntrosListener
	{
		get
		{
			if (_matcherRaccoonIntrosListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(111);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRaccoonIntrosListener = matcher;
			}
			return _matcherRaccoonIntrosListener;
		}
	}

	public static IMatcher<GameEntity> RaccoonOutro
	{
		get
		{
			if (_matcherRaccoonOutro == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(112);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRaccoonOutro = matcher;
			}
			return _matcherRaccoonOutro;
		}
	}

	public static IMatcher<GameEntity> ReplayLevel
	{
		get
		{
			if (_matcherReplayLevel == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(113);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherReplayLevel = matcher;
			}
			return _matcherReplayLevel;
		}
	}

	public static IMatcher<GameEntity> ResetPoolable
	{
		get
		{
			if (_matcherResetPoolable == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(114);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherResetPoolable = matcher;
			}
			return _matcherResetPoolable;
		}
	}

	public static IMatcher<GameEntity> RestableArea
	{
		get
		{
			if (_matcherRestableArea == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(115);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRestableArea = matcher;
			}
			return _matcherRestableArea;
		}
	}

	public static IMatcher<GameEntity> Resting
	{
		get
		{
			if (_matcherResting == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(116);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherResting = matcher;
			}
			return _matcherResting;
		}
	}

	public static IMatcher<GameEntity> ResurectionsDone
	{
		get
		{
			if (_matcherResurectionsDone == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(117);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherResurectionsDone = matcher;
			}
			return _matcherResurectionsDone;
		}
	}

	public static IMatcher<GameEntity> ReturnToHomeLevel
	{
		get
		{
			if (_matcherReturnToHomeLevel == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(118);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherReturnToHomeLevel = matcher;
			}
			return _matcherReturnToHomeLevel;
		}
	}

	public static IMatcher<GameEntity> RevivesDone
	{
		get
		{
			if (_matcherRevivesDone == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(119);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRevivesDone = matcher;
			}
			return _matcherRevivesDone;
		}
	}

	public static IMatcher<GameEntity> RunDuration
	{
		get
		{
			if (_matcherRunDuration == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(120);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherRunDuration = matcher;
			}
			return _matcherRunDuration;
		}
	}

	public static IMatcher<GameEntity> Score
	{
		get
		{
			if (_matcherScore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(121);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherScore = matcher;
			}
			return _matcherScore;
		}
	}

	public static IMatcher<GameEntity> ScoreListener
	{
		get
		{
			if (_matcherScoreListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(122);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherScoreListener = matcher;
			}
			return _matcherScoreListener;
		}
	}

	public static IMatcher<GameEntity> ScoreMultiplier
	{
		get
		{
			if (_matcherScoreMultiplier == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(123);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherScoreMultiplier = matcher;
			}
			return _matcherScoreMultiplier;
		}
	}

	public static IMatcher<GameEntity> ScoreToken
	{
		get
		{
			if (_matcherScoreToken == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(124);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherScoreToken = matcher;
			}
			return _matcherScoreToken;
		}
	}

	public static IMatcher<GameEntity> SelectedCharacter
	{
		get
		{
			if (_matcherSelectedCharacter == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(125);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSelectedCharacter = matcher;
			}
			return _matcherSelectedCharacter;
		}
	}

	public static IMatcher<GameEntity> SetupAbility
	{
		get
		{
			if (_matcherSetupAbility == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(126);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSetupAbility = matcher;
			}
			return _matcherSetupAbility;
		}
	}

	public static IMatcher<GameEntity> Shaking
	{
		get
		{
			if (_matcherShaking == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(127);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherShaking = matcher;
			}
			return _matcherShaking;
		}
	}

	public static IMatcher<GameEntity> SizeIncreased
	{
		get
		{
			if (_matcherSizeIncreased == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(128);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSizeIncreased = matcher;
			}
			return _matcherSizeIncreased;
		}
	}

	public static IMatcher<GameEntity> Sliding
	{
		get
		{
			if (_matcherSliding == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(129);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSliding = matcher;
			}
			return _matcherSliding;
		}
	}

	public static IMatcher<GameEntity> Sparkle
	{
		get
		{
			if (_matcherSparkle == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(130);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSparkle = matcher;
			}
			return _matcherSparkle;
		}
	}

	public static IMatcher<GameEntity> SpecialRun
	{
		get
		{
			if (_matcherSpecialRun == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(131);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSpecialRun = matcher;
			}
			return _matcherSpecialRun;
		}
	}

	public static IMatcher<GameEntity> SpeedData
	{
		get
		{
			if (_matcherSpeedData == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(132);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSpeedData = matcher;
			}
			return _matcherSpeedData;
		}
	}

	public static IMatcher<GameEntity> SpeedUp
	{
		get
		{
			if (_matcherSpeedUp == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(133);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSpeedUp = matcher;
			}
			return _matcherSpeedUp;
		}
	}

	public static IMatcher<GameEntity> Stamina
	{
		get
		{
			if (_matcherStamina == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(134);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherStamina = matcher;
			}
			return _matcherStamina;
		}
	}

	public static IMatcher<GameEntity> StaminasListener
	{
		get
		{
			if (_matcherStaminasListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(135);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherStaminasListener = matcher;
			}
			return _matcherStaminasListener;
		}
	}

	public static IMatcher<GameEntity> SwingingData
	{
		get
		{
			if (_matcherSwingingData == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(136);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherSwingingData = matcher;
			}
			return _matcherSwingingData;
		}
	}

	public static IMatcher<GameEntity> TutorialPerformed
	{
		get
		{
			if (_matcherTutorialPerformed == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(137);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherTutorialPerformed = matcher;
			}
			return _matcherTutorialPerformed;
		}
	}

	public static IMatcher<GameEntity> TutorialPlayStep
	{
		get
		{
			if (_matcherTutorialPlayStep == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(138);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherTutorialPlayStep = matcher;
			}
			return _matcherTutorialPlayStep;
		}
	}

	public static IMatcher<GameEntity> TutorialsListener
	{
		get
		{
			if (_matcherTutorialsListener == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(139);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherTutorialsListener = matcher;
			}
			return _matcherTutorialsListener;
		}
	}

	public static IMatcher<GameEntity> TutorialTrigger
	{
		get
		{
			if (_matcherTutorialTrigger == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(140);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherTutorialTrigger = matcher;
			}
			return _matcherTutorialTrigger;
		}
	}

	public static IMatcher<GameEntity> UnderTheScreen
	{
		get
		{
			if (_matcherUnderTheScreen == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(141);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherUnderTheScreen = matcher;
			}
			return _matcherUnderTheScreen;
		}
	}

	public static IMatcher<GameEntity> UpgradeScore
	{
		get
		{
			if (_matcherUpgradeScore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(142);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherUpgradeScore = matcher;
			}
			return _matcherUpgradeScore;
		}
	}

	public static IMatcher<GameEntity> UpgradeSingleScore
	{
		get
		{
			if (_matcherUpgradeSingleScore == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(143);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherUpgradeSingleScore = matcher;
			}
			return _matcherUpgradeSingleScore;
		}
	}

	public static IMatcher<GameEntity> View
	{
		get
		{
			if (_matcherView == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(144);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherView = matcher;
			}
			return _matcherView;
		}
	}

	public static IMatcher<GameEntity> WaitToStart
	{
		get
		{
			if (_matcherWaitToStart == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(145);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherWaitToStart = matcher;
			}
			return _matcherWaitToStart;
		}
	}

	public static IMatcher<GameEntity> World
	{
		get
		{
			if (_matcherWorld == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(146);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherWorld = matcher;
			}
			return _matcherWorld;
		}
	}

	public static IMatcher<GameEntity> WorldTimeScale
	{
		get
		{
			if (_matcherWorldTimeScale == null)
			{
				Matcher<GameEntity> matcher = (Matcher<GameEntity>)Matcher<GameEntity>.AllOf(147);
				matcher.componentNames = GameComponentsLookup.componentNames;
				_matcherWorldTimeScale = matcher;
			}
			return _matcherWorldTimeScale;
		}
	}

	public static IAllOfMatcher<GameEntity> AllOf(params int[] indices)
	{
		return Matcher<GameEntity>.AllOf(indices);
	}

	public static IAllOfMatcher<GameEntity> AllOf(params IMatcher<GameEntity>[] matchers)
	{
		return Matcher<GameEntity>.AllOf(matchers);
	}

	public static IAnyOfMatcher<GameEntity> AnyOf(params int[] indices)
	{
		return Matcher<GameEntity>.AnyOf(indices);
	}

	public static IAnyOfMatcher<GameEntity> AnyOf(params IMatcher<GameEntity>[] matchers)
	{
		return Matcher<GameEntity>.AnyOf(matchers);
	}
}
