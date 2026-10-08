using System;
using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class GameEntity : Entity, IAsyncRequest, ICoinsListener, IDestroyed, IRaccoonIntrosListener, IStaminasListener, ITutorialsListener
{
	private static readonly AbilityDragonBlastComponent abilityDragonBlastComponent = new AbilityDragonBlastComponent();

	private static readonly AbilityMagnetizedComponent abilityMagnetizedComponent = new AbilityMagnetizedComponent();

	private static readonly AbilityRandomPowerupComponent abilityRandomPowerupComponent = new AbilityRandomPowerupComponent();

	private static readonly AbilityRunComponent abilityRunComponent = new AbilityRunComponent();

	private static readonly AngryRunComponent angryRunComponent = new AngryRunComponent();

	private static readonly CameraAttachedComponent cameraAttachedComponent = new CameraAttachedComponent();

	private static readonly CameraStopComponent cameraStopComponent = new CameraStopComponent();

	private static readonly CandyStoreComponent candyStoreComponent = new CandyStoreComponent();

	private static readonly CloudComponent cloudComponent = new CloudComponent();

	private static readonly CollectedComponent collectedComponent = new CollectedComponent();

	private static readonly CollectibleComponent collectibleComponent = new CollectibleComponent();

	private static readonly CollectibleGiantTokenComponent collectibleGiantTokenComponent = new CollectibleGiantTokenComponent();

	private static readonly CollectibleMagnetComponent collectibleMagnetComponent = new CollectibleMagnetComponent();

	private static readonly CollectibleScoreMultiplierComponent collectibleScoreMultiplierComponent = new CollectibleScoreMultiplierComponent();

	private static readonly CollectibleSpeedUpComponent collectibleSpeedUpComponent = new CollectibleSpeedUpComponent();

	private static readonly CollectibleUpgradeScoreComponent collectibleUpgradeScoreComponent = new CollectibleUpgradeScoreComponent();

	private static readonly CompanionComponent companionComponent = new CompanionComponent();

	private static readonly DeadComponent deadComponent = new DeadComponent();

	private static readonly DestroyedComponent destroyedComponent = new DestroyedComponent();

	private static readonly DestroyZonedComponent destroyZonedComponent = new DestroyZonedComponent();

	private static readonly DisableCollidersComponent disableCollidersComponent = new DisableCollidersComponent();

	private static readonly FadeOutComponent fadeOutComponent = new FadeOutComponent();

	private static readonly FlyAwayComponent flyAwayComponent = new FlyAwayComponent();

	private static readonly GameStartedComponent gameStartedComponent = new GameStartedComponent();

	private static readonly GodModeComponent godModeComponent = new GodModeComponent();

	private static readonly ImprovedScoreTokenComponent improvedScoreTokenComponent = new ImprovedScoreTokenComponent();

	private static readonly KillZoneComponent killZoneComponent = new KillZoneComponent();

	private static readonly LoadAssetAsyncComponent loadAssetAsyncComponent = new LoadAssetAsyncComponent();

	private static readonly MissionChestGrantedComponent missionChestGrantedComponent = new MissionChestGrantedComponent();

	private static readonly MovableComponent movableComponent = new MovableComponent();

	private static readonly ObstacleComponent obstacleComponent = new ObstacleComponent();

	private static readonly PausedComponent pausedComponent = new PausedComponent();

	private static readonly PlatformComponent platformComponent = new PlatformComponent();

	private static readonly PlayerAttachedComponent playerAttachedComponent = new PlayerAttachedComponent();

	private static readonly PlayerComponent playerComponent = new PlayerComponent();

	private static readonly PlayerRelayComponent playerRelayComponent = new PlayerRelayComponent();

	private static readonly PlayerReviveComponent playerReviveComponent = new PlayerReviveComponent();

	private static readonly PlayGameFailMusicComponent playGameFailMusicComponent = new PlayGameFailMusicComponent();

	private static readonly PlayGameMusicComponent playGameMusicComponent = new PlayGameMusicComponent();

	private static readonly PlayGameWinMusicComponent playGameWinMusicComponent = new PlayGameWinMusicComponent();

	private static readonly PoolableComponent poolableComponent = new PoolableComponent();

	private static readonly Raccoon raccoonComponent = new Raccoon();

	private static readonly RaccoonDropAnimationComponent raccoonDropAnimationComponent = new RaccoonDropAnimationComponent();

	private static readonly RaccoonOutroComponent raccoonOutroComponent = new RaccoonOutroComponent();

	private static readonly ReplayLevelComponent replayLevelComponent = new ReplayLevelComponent();

	private static readonly ResetPoolableComponent resetPoolableComponent = new ResetPoolableComponent();

	private static readonly ReturnToHomeLevelComponent returnToHomeLevelComponent = new ReturnToHomeLevelComponent();

	private static readonly ScoreTokenComponent scoreTokenComponent = new ScoreTokenComponent();

	private static readonly SlidingComponent slidingComponent = new SlidingComponent();

	private static readonly SpecialRunComponent specialRunComponent = new SpecialRunComponent();

	private static readonly UnderTheScreenComponent underTheScreenComponent = new UnderTheScreenComponent();

	public AbilityChangedListenerComponent abilityChangedListener
	{
		get
		{
			return (AbilityChangedListenerComponent)GetComponent(0);
		}
	}

	public bool hasAbilityChangedListener
	{
		get
		{
			return HasComponent(0);
		}
	}

	public AbilityChargingProgress abilityChargingProgress
	{
		get
		{
			return (AbilityChargingProgress)GetComponent(1);
		}
	}

	public bool hasAbilityChargingProgress
	{
		get
		{
			return HasComponent(1);
		}
	}

	public AbilityChargingTimeComponent abilityChargingTime
	{
		get
		{
			return (AbilityChargingTimeComponent)GetComponent(2);
		}
	}

	public bool hasAbilityChargingTime
	{
		get
		{
			return HasComponent(2);
		}
	}

	public AbilityCreateTokenComponent abilityCreateToken
	{
		get
		{
			return (AbilityCreateTokenComponent)GetComponent(3);
		}
	}

	public bool hasAbilityCreateToken
	{
		get
		{
			return HasComponent(3);
		}
	}

	public bool isAbilityDragonBlast
	{
		get
		{
			return HasComponent(4);
		}
		set
		{
			if (value != isAbilityDragonBlast)
			{
				if (value)
				{
					AddComponent(4, abilityDragonBlastComponent);
				}
				else
				{
					RemoveComponent(4);
				}
			}
		}
	}

	public AbilityDurationComponent abilityDuration
	{
		get
		{
			return (AbilityDurationComponent)GetComponent(5);
		}
	}

	public bool hasAbilityDuration
	{
		get
		{
			return HasComponent(5);
		}
	}

	public AbilityFasterMovementComponent abilityFasterMovement
	{
		get
		{
			return (AbilityFasterMovementComponent)GetComponent(6);
		}
	}

	public bool hasAbilityFasterMovement
	{
		get
		{
			return HasComponent(6);
		}
	}

	public AbilityGiantSpeedupComponent abilityGiantSpeedup
	{
		get
		{
			return (AbilityGiantSpeedupComponent)GetComponent(7);
		}
	}

	public bool hasAbilityGiantSpeedup
	{
		get
		{
			return HasComponent(7);
		}
	}

	public bool isAbilityMagnetized
	{
		get
		{
			return HasComponent(8);
		}
		set
		{
			if (value != isAbilityMagnetized)
			{
				if (value)
				{
					AddComponent(8, abilityMagnetizedComponent);
				}
				else
				{
					RemoveComponent(8);
				}
			}
		}
	}

	public bool isAbilityRandomPowerup
	{
		get
		{
			return HasComponent(9);
		}
		set
		{
			if (value != isAbilityRandomPowerup)
			{
				if (value)
				{
					AddComponent(9, abilityRandomPowerupComponent);
				}
				else
				{
					RemoveComponent(9);
				}
			}
		}
	}

	public AbilityResurrectComponent abilityResurrect
	{
		get
		{
			return (AbilityResurrectComponent)GetComponent(10);
		}
	}

	public bool hasAbilityResurrect
	{
		get
		{
			return HasComponent(10);
		}
	}

	public bool isAbilityRun
	{
		get
		{
			return HasComponent(11);
		}
		set
		{
			if (value != isAbilityRun)
			{
				if (value)
				{
					AddComponent(11, abilityRunComponent);
				}
				else
				{
					RemoveComponent(11);
				}
			}
		}
	}

	public AbilityScoreBonusComponent abilityScoreBonus
	{
		get
		{
			return (AbilityScoreBonusComponent)GetComponent(12);
		}
	}

	public bool hasAbilityScoreBonus
	{
		get
		{
			return HasComponent(12);
		}
	}

	public AbilityShieldComponent abilityShield
	{
		get
		{
			return (AbilityShieldComponent)GetComponent(13);
		}
	}

	public bool hasAbilityShield
	{
		get
		{
			return HasComponent(13);
		}
	}

	public AbilitySpeedupComponent abilitySpeedup
	{
		get
		{
			return (AbilitySpeedupComponent)GetComponent(14);
		}
	}

	public bool hasAbilitySpeedup
	{
		get
		{
			return HasComponent(14);
		}
	}

	public AbilityStateChangedListenerComponent abilityStateChangedListener
	{
		get
		{
			return (AbilityStateChangedListenerComponent)GetComponent(15);
		}
	}

	public bool hasAbilityStateChangedListener
	{
		get
		{
			return HasComponent(15);
		}
	}

	public AbilityTimerComponent abilityTimer
	{
		get
		{
			return (AbilityTimerComponent)GetComponent(16);
		}
	}

	public bool hasAbilityTimer
	{
		get
		{
			return HasComponent(16);
		}
	}

	public AbilityUpgradeScoreComponent abilityUpgradeScore
	{
		get
		{
			return (AbilityUpgradeScoreComponent)GetComponent(17);
		}
	}

	public bool hasAbilityUpgradeScore
	{
		get
		{
			return HasComponent(17);
		}
	}

	public bool isAngryRun
	{
		get
		{
			return HasComponent(18);
		}
		set
		{
			if (value != isAngryRun)
			{
				if (value)
				{
					AddComponent(18, angryRunComponent);
				}
				else
				{
					RemoveComponent(18);
				}
			}
		}
	}

	public AssetComponent asset
	{
		get
		{
			return (AssetComponent)GetComponent(19);
		}
	}

	public bool hasAsset
	{
		get
		{
			return HasComponent(19);
		}
	}

	public AsyncRequestComponent asyncRequest
	{
		get
		{
			return (AsyncRequestComponent)GetComponent(20);
		}
	}

	public bool hasAsyncRequest
	{
		get
		{
			return HasComponent(20);
		}
	}

	public bool isCameraAttached
	{
		get
		{
			return HasComponent(21);
		}
		set
		{
			if (value != isCameraAttached)
			{
				if (value)
				{
					AddComponent(21, cameraAttachedComponent);
				}
				else
				{
					RemoveComponent(21);
				}
			}
		}
	}

	public bool isCameraStop
	{
		get
		{
			return HasComponent(22);
		}
		set
		{
			if (value != isCameraStop)
			{
				if (value)
				{
					AddComponent(22, cameraStopComponent);
				}
				else
				{
					RemoveComponent(22);
				}
			}
		}
	}

	public bool isCandyStore
	{
		get
		{
			return HasComponent(23);
		}
		set
		{
			if (value != isCandyStore)
			{
				if (value)
				{
					AddComponent(23, candyStoreComponent);
				}
				else
				{
					RemoveComponent(23);
				}
			}
		}
	}

	public ChangeCameraSizeTriggerComponent changeCameraSizeTrigger
	{
		get
		{
			return (ChangeCameraSizeTriggerComponent)GetComponent(24);
		}
	}

	public bool hasChangeCameraSizeTrigger
	{
		get
		{
			return HasComponent(24);
		}
	}

	public ChangeScaleTimeComponent changeScaleTime
	{
		get
		{
			return (ChangeScaleTimeComponent)GetComponent(25);
		}
	}

	public bool hasChangeScaleTime
	{
		get
		{
			return HasComponent(25);
		}
	}

	public ChangeWorldSkinComponent changeWorldSkin
	{
		get
		{
			return (ChangeWorldSkinComponent)GetComponent(26);
		}
	}

	public bool hasChangeWorldSkin
	{
		get
		{
			return HasComponent(26);
		}
	}

	public ChangeWorldSpeedComponent changeWorldSpeed
	{
		get
		{
			return (ChangeWorldSpeedComponent)GetComponent(27);
		}
	}

	public bool hasChangeWorldSpeed
	{
		get
		{
			return HasComponent(27);
		}
	}

	public bool isCloud
	{
		get
		{
			return HasComponent(28);
		}
		set
		{
			if (value != isCloud)
			{
				if (value)
				{
					AddComponent(28, cloudComponent);
				}
				else
				{
					RemoveComponent(28);
				}
			}
		}
	}

	public CoinsComponent coins
	{
		get
		{
			return (CoinsComponent)GetComponent(29);
		}
	}

	public bool hasCoins
	{
		get
		{
			return HasComponent(29);
		}
	}

	public CoinsListenerComponent coinsListener
	{
		get
		{
			return (CoinsListenerComponent)GetComponent(30);
		}
	}

	public bool hasCoinsListener
	{
		get
		{
			return HasComponent(30);
		}
	}

	public bool isCollected
	{
		get
		{
			return HasComponent(31);
		}
		set
		{
			if (value != isCollected)
			{
				if (value)
				{
					AddComponent(31, collectedComponent);
				}
				else
				{
					RemoveComponent(31);
				}
			}
		}
	}

	public CollectibleCoinComponent collectibleCoin
	{
		get
		{
			return (CollectibleCoinComponent)GetComponent(32);
		}
	}

	public bool hasCollectibleCoin
	{
		get
		{
			return HasComponent(32);
		}
	}

	public bool isCollectible
	{
		get
		{
			return HasComponent(33);
		}
		set
		{
			if (value != isCollectible)
			{
				if (value)
				{
					AddComponent(33, collectibleComponent);
				}
				else
				{
					RemoveComponent(33);
				}
			}
		}
	}

	public CollectibleDataComponent collectibleData
	{
		get
		{
			return (CollectibleDataComponent)GetComponent(34);
		}
	}

	public bool hasCollectibleData
	{
		get
		{
			return HasComponent(34);
		}
	}

	public bool isCollectibleGiantToken
	{
		get
		{
			return HasComponent(35);
		}
		set
		{
			if (value != isCollectibleGiantToken)
			{
				if (value)
				{
					AddComponent(35, collectibleGiantTokenComponent);
				}
				else
				{
					RemoveComponent(35);
				}
			}
		}
	}

	public bool isCollectibleMagnet
	{
		get
		{
			return HasComponent(36);
		}
		set
		{
			if (value != isCollectibleMagnet)
			{
				if (value)
				{
					AddComponent(36, collectibleMagnetComponent);
				}
				else
				{
					RemoveComponent(36);
				}
			}
		}
	}

	public CollectibleScoreComponent collectibleScore
	{
		get
		{
			return (CollectibleScoreComponent)GetComponent(37);
		}
	}

	public bool hasCollectibleScore
	{
		get
		{
			return HasComponent(37);
		}
	}

	public bool isCollectibleScoreMultiplier
	{
		get
		{
			return HasComponent(38);
		}
		set
		{
			if (value != isCollectibleScoreMultiplier)
			{
				if (value)
				{
					AddComponent(38, collectibleScoreMultiplierComponent);
				}
				else
				{
					RemoveComponent(38);
				}
			}
		}
	}

	public bool isCollectibleSpeedUp
	{
		get
		{
			return HasComponent(39);
		}
		set
		{
			if (value != isCollectibleSpeedUp)
			{
				if (value)
				{
					AddComponent(39, collectibleSpeedUpComponent);
				}
				else
				{
					RemoveComponent(39);
				}
			}
		}
	}

	public CollectibleStaminaComponent collectibleStamina
	{
		get
		{
			return (CollectibleStaminaComponent)GetComponent(40);
		}
	}

	public bool hasCollectibleStamina
	{
		get
		{
			return HasComponent(40);
		}
	}

	public bool isCollectibleUpgradeScore
	{
		get
		{
			return HasComponent(41);
		}
		set
		{
			if (value != isCollectibleUpgradeScore)
			{
				if (value)
				{
					AddComponent(41, collectibleUpgradeScoreComponent);
				}
				else
				{
					RemoveComponent(41);
				}
			}
		}
	}

	public CollectSoundComponent collectSound
	{
		get
		{
			return (CollectSoundComponent)GetComponent(42);
		}
	}

	public bool hasCollectSound
	{
		get
		{
			return HasComponent(42);
		}
	}

	public bool isCompanion
	{
		get
		{
			return HasComponent(43);
		}
		set
		{
			if (value != isCompanion)
			{
				if (value)
				{
					AddComponent(43, companionComponent);
				}
				else
				{
					RemoveComponent(43);
				}
			}
		}
	}

	public DailyMissionCollectComponent dailyMissionCollect
	{
		get
		{
			return (DailyMissionCollectComponent)GetComponent(44);
		}
	}

	public bool hasDailyMissionCollect
	{
		get
		{
			return HasComponent(44);
		}
	}

	public DailyMissionDestroyObstacleComponent dailyMissionDestroyObstacle
	{
		get
		{
			return (DailyMissionDestroyObstacleComponent)GetComponent(45);
		}
	}

	public bool hasDailyMissionDestroyObstacle
	{
		get
		{
			return HasComponent(45);
		}
	}

	public DailyMissionPlayMissionsComponent dailyMissionPlayMissions
	{
		get
		{
			return (DailyMissionPlayMissionsComponent)GetComponent(46);
		}
	}

	public bool hasDailyMissionPlayMissions
	{
		get
		{
			return HasComponent(46);
		}
	}

	public bool isDead
	{
		get
		{
			return HasComponent(47);
		}
		set
		{
			if (value != isDead)
			{
				if (value)
				{
					AddComponent(47, deadComponent);
				}
				else
				{
					RemoveComponent(47);
				}
			}
		}
	}

	public bool isDestroyed
	{
		get
		{
			return HasComponent(48);
		}
		set
		{
			if (value != isDestroyed)
			{
				if (value)
				{
					AddComponent(48, destroyedComponent);
				}
				else
				{
					RemoveComponent(48);
				}
			}
		}
	}

	public bool isDestroyZoned
	{
		get
		{
			return HasComponent(49);
		}
		set
		{
			if (value != isDestroyZoned)
			{
				if (value)
				{
					AddComponent(49, destroyZonedComponent);
				}
				else
				{
					RemoveComponent(49);
				}
			}
		}
	}

	public bool isDisableColliders
	{
		get
		{
			return HasComponent(50);
		}
		set
		{
			if (value != isDisableColliders)
			{
				if (value)
				{
					AddComponent(50, disableCollidersComponent);
				}
				else
				{
					RemoveComponent(50);
				}
			}
		}
	}

	public DroppingComponent dropping
	{
		get
		{
			return (DroppingComponent)GetComponent(51);
		}
	}

	public bool hasDropping
	{
		get
		{
			return HasComponent(51);
		}
	}

	public bool isFadeOut
	{
		get
		{
			return HasComponent(52);
		}
		set
		{
			if (value != isFadeOut)
			{
				if (value)
				{
					AddComponent(52, fadeOutComponent);
				}
				else
				{
					RemoveComponent(52);
				}
			}
		}
	}

	public FloatingComponent floating
	{
		get
		{
			return (FloatingComponent)GetComponent(53);
		}
	}

	public bool hasFloating
	{
		get
		{
			return HasComponent(53);
		}
	}

	public bool isFlyAway
	{
		get
		{
			return HasComponent(54);
		}
		set
		{
			if (value != isFlyAway)
			{
				if (value)
				{
					AddComponent(54, flyAwayComponent);
				}
				else
				{
					RemoveComponent(54);
				}
			}
		}
	}

	public GainedScoreComponent gainedScore
	{
		get
		{
			return (GainedScoreComponent)GetComponent(55);
		}
	}

	public bool hasGainedScore
	{
		get
		{
			return HasComponent(55);
		}
	}

	public bool isGameStarted
	{
		get
		{
			return HasComponent(56);
		}
		set
		{
			if (value != isGameStarted)
			{
				if (value)
				{
					AddComponent(56, gameStartedComponent);
				}
				else
				{
					RemoveComponent(56);
				}
			}
		}
	}

	public bool isGodMode
	{
		get
		{
			return HasComponent(57);
		}
		set
		{
			if (value != isGodMode)
			{
				if (value)
				{
					AddComponent(57, godModeComponent);
				}
				else
				{
					RemoveComponent(57);
				}
			}
		}
	}

	public GroundedComponent grounded
	{
		get
		{
			return (GroundedComponent)GetComponent(58);
		}
	}

	public bool hasGrounded
	{
		get
		{
			return HasComponent(58);
		}
	}

	public bool isImprovedScoreToken
	{
		get
		{
			return HasComponent(59);
		}
		set
		{
			if (value != isImprovedScoreToken)
			{
				if (value)
				{
					AddComponent(59, improvedScoreTokenComponent);
				}
				else
				{
					RemoveComponent(59);
				}
			}
		}
	}

	public IngameBgComponent ingameBg
	{
		get
		{
			return (IngameBgComponent)GetComponent(60);
		}
	}

	public bool hasIngameBg
	{
		get
		{
			return HasComponent(60);
		}
	}

	public InitialTimeScaleComponent initialTimeScale
	{
		get
		{
			return (InitialTimeScaleComponent)GetComponent(61);
		}
	}

	public bool hasInitialTimeScale
	{
		get
		{
			return HasComponent(61);
		}
	}

	public InitialTriggerComponent initialTrigger
	{
		get
		{
			return (InitialTriggerComponent)GetComponent(62);
		}
	}

	public bool hasInitialTrigger
	{
		get
		{
			return HasComponent(62);
		}
	}

	public InvulnerabilityComponent invulnerability
	{
		get
		{
			return (InvulnerabilityComponent)GetComponent(63);
		}
	}

	public bool hasInvulnerability
	{
		get
		{
			return HasComponent(63);
		}
	}

	public JumpingComponent jumping
	{
		get
		{
			return (JumpingComponent)GetComponent(64);
		}
	}

	public bool hasJumping
	{
		get
		{
			return HasComponent(64);
		}
	}

	public bool isKillZone
	{
		get
		{
			return HasComponent(65);
		}
		set
		{
			if (value != isKillZone)
			{
				if (value)
				{
					AddComponent(65, killZoneComponent);
				}
				else
				{
					RemoveComponent(65);
				}
			}
		}
	}

	public LevelEndComponent levelEnd
	{
		get
		{
			return (LevelEndComponent)GetComponent(66);
		}
	}

	public bool hasLevelEnd
	{
		get
		{
			return HasComponent(66);
		}
	}

	public LevelFinishedComponent levelFinished
	{
		get
		{
			return (LevelFinishedComponent)GetComponent(67);
		}
	}

	public bool hasLevelFinished
	{
		get
		{
			return HasComponent(67);
		}
	}

	public LevelFinishedListenerComponent levelFinishedListener
	{
		get
		{
			return (LevelFinishedListenerComponent)GetComponent(68);
		}
	}

	public bool hasLevelFinishedListener
	{
		get
		{
			return HasComponent(68);
		}
	}

	public LevelSettingsComponent levelSettings
	{
		get
		{
			return (LevelSettingsComponent)GetComponent(69);
		}
	}

	public bool hasLevelSettings
	{
		get
		{
			return HasComponent(69);
		}
	}

	public bool isLoadAssetAsync
	{
		get
		{
			return HasComponent(70);
		}
		set
		{
			if (value != isLoadAssetAsync)
			{
				if (value)
				{
					AddComponent(70, loadAssetAsyncComponent);
				}
				else
				{
					RemoveComponent(70);
				}
			}
		}
	}

	public MagnetizedComponent magnetized
	{
		get
		{
			return (MagnetizedComponent)GetComponent(71);
		}
	}

	public bool hasMagnetized
	{
		get
		{
			return HasComponent(71);
		}
	}

	public bool isMissionChestGranted
	{
		get
		{
			return HasComponent(72);
		}
		set
		{
			if (value != isMissionChestGranted)
			{
				if (value)
				{
					AddComponent(72, missionChestGrantedComponent);
				}
				else
				{
					RemoveComponent(72);
				}
			}
		}
	}

	public MissionCollectComponent missionCollect
	{
		get
		{
			return (MissionCollectComponent)GetComponent(73);
		}
	}

	public bool hasMissionCollect
	{
		get
		{
			return HasComponent(73);
		}
	}

	public MissionCounterComponent missionCounter
	{
		get
		{
			return (MissionCounterComponent)GetComponent(74);
		}
	}

	public bool hasMissionCounter
	{
		get
		{
			return HasComponent(74);
		}
	}

	public MissionDataComponent missionData
	{
		get
		{
			return (MissionDataComponent)GetComponent(75);
		}
	}

	public bool hasMissionData
	{
		get
		{
			return HasComponent(75);
		}
	}

	public MissionResultComponent missionResult
	{
		get
		{
			return (MissionResultComponent)GetComponent(76);
		}
	}

	public bool hasMissionResult
	{
		get
		{
			return HasComponent(76);
		}
	}

	public MissionSpriteComponent missionSprite
	{
		get
		{
			return (MissionSpriteComponent)GetComponent(77);
		}
	}

	public bool hasMissionSprite
	{
		get
		{
			return HasComponent(77);
		}
	}

	public MissionSpriteFillAmountComponent missionSpriteFillAmount
	{
		get
		{
			return (MissionSpriteFillAmountComponent)GetComponent(78);
		}
	}

	public bool hasMissionSpriteFillAmount
	{
		get
		{
			return HasComponent(78);
		}
	}

	public MissionThresholdsComponent missionThresholds
	{
		get
		{
			return (MissionThresholdsComponent)GetComponent(79);
		}
	}

	public bool hasMissionThresholds
	{
		get
		{
			return HasComponent(79);
		}
	}

	public MissionTypeComponent missionType
	{
		get
		{
			return (MissionTypeComponent)GetComponent(80);
		}
	}

	public bool hasMissionType
	{
		get
		{
			return HasComponent(80);
		}
	}

	public MissionUpdateListenerComponent missionUpdateListener
	{
		get
		{
			return (MissionUpdateListenerComponent)GetComponent(81);
		}
	}

	public bool hasMissionUpdateListener
	{
		get
		{
			return HasComponent(81);
		}
	}

	public bool isMovable
	{
		get
		{
			return HasComponent(82);
		}
		set
		{
			if (value != isMovable)
			{
				if (value)
				{
					AddComponent(82, movableComponent);
				}
				else
				{
					RemoveComponent(82);
				}
			}
		}
	}

	public MovableDataComponent movableData
	{
		get
		{
			return (MovableDataComponent)GetComponent(83);
		}
	}

	public bool hasMovableData
	{
		get
		{
			return HasComponent(83);
		}
	}

	public NewWorldSkinComponent newWorldSkin
	{
		get
		{
			return (NewWorldSkinComponent)GetComponent(84);
		}
	}

	public bool hasNewWorldSkin
	{
		get
		{
			return HasComponent(84);
		}
	}

	public bool isObstacle
	{
		get
		{
			return HasComponent(85);
		}
		set
		{
			if (value != isObstacle)
			{
				if (value)
				{
					AddComponent(85, obstacleComponent);
				}
				else
				{
					RemoveComponent(85);
				}
			}
		}
	}

	public ObstacleHitListenerComponent obstacleHitListener
	{
		get
		{
			return (ObstacleHitListenerComponent)GetComponent(86);
		}
	}

	public bool hasObstacleHitListener
	{
		get
		{
			return HasComponent(86);
		}
	}

	public OffsetPositionComponent offsetPosition
	{
		get
		{
			return (OffsetPositionComponent)GetComponent(87);
		}
	}

	public bool hasOffsetPosition
	{
		get
		{
			return HasComponent(87);
		}
	}

	public OnCloudsComponent onClouds
	{
		get
		{
			return (OnCloudsComponent)GetComponent(88);
		}
	}

	public bool hasOnClouds
	{
		get
		{
			return HasComponent(88);
		}
	}

	public bool isPaused
	{
		get
		{
			return HasComponent(89);
		}
		set
		{
			if (value != isPaused)
			{
				if (value)
				{
					AddComponent(89, pausedComponent);
				}
				else
				{
					RemoveComponent(89);
				}
			}
		}
	}

	public PauseListenerComponent pauseListener
	{
		get
		{
			return (PauseListenerComponent)GetComponent(90);
		}
	}

	public bool hasPauseListener
	{
		get
		{
			return HasComponent(90);
		}
	}

	public bool isPlatform
	{
		get
		{
			return HasComponent(91);
		}
		set
		{
			if (value != isPlatform)
			{
				if (value)
				{
					AddComponent(91, platformComponent);
				}
				else
				{
					RemoveComponent(91);
				}
			}
		}
	}

	public bool isPlayerAttached
	{
		get
		{
			return HasComponent(92);
		}
		set
		{
			if (value != isPlayerAttached)
			{
				if (value)
				{
					AddComponent(92, playerAttachedComponent);
				}
				else
				{
					RemoveComponent(92);
				}
			}
		}
	}

	public PlayerBlinkingComponent playerBlinking
	{
		get
		{
			return (PlayerBlinkingComponent)GetComponent(93);
		}
	}

	public bool hasPlayerBlinking
	{
		get
		{
			return HasComponent(93);
		}
	}

	public bool isPlayer
	{
		get
		{
			return HasComponent(94);
		}
		set
		{
			if (value != isPlayer)
			{
				if (value)
				{
					AddComponent(94, playerComponent);
				}
				else
				{
					RemoveComponent(94);
				}
			}
		}
	}

	public PlayerLandListenerComponent playerLandListener
	{
		get
		{
			return (PlayerLandListenerComponent)GetComponent(95);
		}
	}

	public bool hasPlayerLandListener
	{
		get
		{
			return HasComponent(95);
		}
	}

	public bool isPlayerRelay
	{
		get
		{
			return HasComponent(96);
		}
		set
		{
			if (value != isPlayerRelay)
			{
				if (value)
				{
					AddComponent(96, playerRelayComponent);
				}
				else
				{
					RemoveComponent(96);
				}
			}
		}
	}

	public PlayerResurrectComponent playerResurrect
	{
		get
		{
			return (PlayerResurrectComponent)GetComponent(97);
		}
	}

	public bool hasPlayerResurrect
	{
		get
		{
			return HasComponent(97);
		}
	}

	public bool isPlayerRevive
	{
		get
		{
			return HasComponent(98);
		}
		set
		{
			if (value != isPlayerRevive)
			{
				if (value)
				{
					AddComponent(98, playerReviveComponent);
				}
				else
				{
					RemoveComponent(98);
				}
			}
		}
	}

	public bool isPlayGameFailMusic
	{
		get
		{
			return HasComponent(99);
		}
		set
		{
			if (value != isPlayGameFailMusic)
			{
				if (value)
				{
					AddComponent(99, playGameFailMusicComponent);
				}
				else
				{
					RemoveComponent(99);
				}
			}
		}
	}

	public bool isPlayGameMusic
	{
		get
		{
			return HasComponent(100);
		}
		set
		{
			if (value != isPlayGameMusic)
			{
				if (value)
				{
					AddComponent(100, playGameMusicComponent);
				}
				else
				{
					RemoveComponent(100);
				}
			}
		}
	}

	public bool isPlayGameWinMusic
	{
		get
		{
			return HasComponent(101);
		}
		set
		{
			if (value != isPlayGameWinMusic)
			{
				if (value)
				{
					AddComponent(101, playGameWinMusicComponent);
				}
				else
				{
					RemoveComponent(101);
				}
			}
		}
	}

	public PlaySoundsListenerComponent playSoundsListener
	{
		get
		{
			return (PlaySoundsListenerComponent)GetComponent(102);
		}
	}

	public bool hasPlaySoundsListener
	{
		get
		{
			return HasComponent(102);
		}
	}

	public bool isPoolable
	{
		get
		{
			return HasComponent(103);
		}
		set
		{
			if (value != isPoolable)
			{
				if (value)
				{
					AddComponent(103, poolableComponent);
				}
				else
				{
					RemoveComponent(103);
				}
			}
		}
	}

	public PoolContainerComponent poolContainer
	{
		get
		{
			return (PoolContainerComponent)GetComponent(104);
		}
	}

	public bool hasPoolContainer
	{
		get
		{
			return HasComponent(104);
		}
	}

	public PoolDespawnedComponent poolDespawned
	{
		get
		{
			return (PoolDespawnedComponent)GetComponent(105);
		}
	}

	public bool hasPoolDespawned
	{
		get
		{
			return HasComponent(105);
		}
	}

	public PoolSpawnedComponent poolSpawned
	{
		get
		{
			return (PoolSpawnedComponent)GetComponent(106);
		}
	}

	public bool hasPoolSpawned
	{
		get
		{
			return HasComponent(106);
		}
	}

	public PositionComponent position
	{
		get
		{
			return (PositionComponent)GetComponent(107);
		}
	}

	public bool hasPosition
	{
		get
		{
			return HasComponent(107);
		}
	}

	public bool isRaccoon
	{
		get
		{
			return HasComponent(108);
		}
		set
		{
			if (value != isRaccoon)
			{
				if (value)
				{
					AddComponent(108, raccoonComponent);
				}
				else
				{
					RemoveComponent(108);
				}
			}
		}
	}

	public bool isRaccoonDropAnimation
	{
		get
		{
			return HasComponent(109);
		}
		set
		{
			if (value != isRaccoonDropAnimation)
			{
				if (value)
				{
					AddComponent(109, raccoonDropAnimationComponent);
				}
				else
				{
					RemoveComponent(109);
				}
			}
		}
	}

	public RaccoonIntroComponent raccoonIntro
	{
		get
		{
			return (RaccoonIntroComponent)GetComponent(110);
		}
	}

	public bool hasRaccoonIntro
	{
		get
		{
			return HasComponent(110);
		}
	}

	public RaccoonIntrosListenerComponent raccoonIntrosListener
	{
		get
		{
			return (RaccoonIntrosListenerComponent)GetComponent(111);
		}
	}

	public bool hasRaccoonIntrosListener
	{
		get
		{
			return HasComponent(111);
		}
	}

	public bool isRaccoonOutro
	{
		get
		{
			return HasComponent(112);
		}
		set
		{
			if (value != isRaccoonOutro)
			{
				if (value)
				{
					AddComponent(112, raccoonOutroComponent);
				}
				else
				{
					RemoveComponent(112);
				}
			}
		}
	}

	public bool isReplayLevel
	{
		get
		{
			return HasComponent(113);
		}
		set
		{
			if (value != isReplayLevel)
			{
				if (value)
				{
					AddComponent(113, replayLevelComponent);
				}
				else
				{
					RemoveComponent(113);
				}
			}
		}
	}

	public bool isResetPoolable
	{
		get
		{
			return HasComponent(114);
		}
		set
		{
			if (value != isResetPoolable)
			{
				if (value)
				{
					AddComponent(114, resetPoolableComponent);
				}
				else
				{
					RemoveComponent(114);
				}
			}
		}
	}

	public RestableAreaComponent restableArea
	{
		get
		{
			return (RestableAreaComponent)GetComponent(115);
		}
	}

	public bool hasRestableArea
	{
		get
		{
			return HasComponent(115);
		}
	}

	public RestingComponent resting
	{
		get
		{
			return (RestingComponent)GetComponent(116);
		}
	}

	public bool hasResting
	{
		get
		{
			return HasComponent(116);
		}
	}

	public ResurectionsDoneComponent resurectionsDone
	{
		get
		{
			return (ResurectionsDoneComponent)GetComponent(117);
		}
	}

	public bool hasResurectionsDone
	{
		get
		{
			return HasComponent(117);
		}
	}

	public bool isReturnToHomeLevel
	{
		get
		{
			return HasComponent(118);
		}
		set
		{
			if (value != isReturnToHomeLevel)
			{
				if (value)
				{
					AddComponent(118, returnToHomeLevelComponent);
				}
				else
				{
					RemoveComponent(118);
				}
			}
		}
	}

	public RevivesDoneComponent revivesDone
	{
		get
		{
			return (RevivesDoneComponent)GetComponent(119);
		}
	}

	public bool hasRevivesDone
	{
		get
		{
			return HasComponent(119);
		}
	}

	public RunDurationComponent runDuration
	{
		get
		{
			return (RunDurationComponent)GetComponent(120);
		}
	}

	public bool hasRunDuration
	{
		get
		{
			return HasComponent(120);
		}
	}

	public ScoreComponent score
	{
		get
		{
			return (ScoreComponent)GetComponent(121);
		}
	}

	public bool hasScore
	{
		get
		{
			return HasComponent(121);
		}
	}

	public ScoreListenerComponent scoreListener
	{
		get
		{
			return (ScoreListenerComponent)GetComponent(122);
		}
	}

	public bool hasScoreListener
	{
		get
		{
			return HasComponent(122);
		}
	}

	public ScoreMultiplierComponent scoreMultiplier
	{
		get
		{
			return (ScoreMultiplierComponent)GetComponent(123);
		}
	}

	public bool hasScoreMultiplier
	{
		get
		{
			return HasComponent(123);
		}
	}

	public bool isScoreToken
	{
		get
		{
			return HasComponent(124);
		}
		set
		{
			if (value != isScoreToken)
			{
				if (value)
				{
					AddComponent(124, scoreTokenComponent);
				}
				else
				{
					RemoveComponent(124);
				}
			}
		}
	}

	public SelectedCharacterComponent selectedCharacter
	{
		get
		{
			return (SelectedCharacterComponent)GetComponent(125);
		}
	}

	public bool hasSelectedCharacter
	{
		get
		{
			return HasComponent(125);
		}
	}

	public SetupAbility setupAbility
	{
		get
		{
			return (SetupAbility)GetComponent(126);
		}
	}

	public bool hasSetupAbility
	{
		get
		{
			return HasComponent(126);
		}
	}

	public ShakingComponent shaking
	{
		get
		{
			return (ShakingComponent)GetComponent(127);
		}
	}

	public bool hasShaking
	{
		get
		{
			return HasComponent(127);
		}
	}

	public SizeIncreasedComponent sizeIncreased
	{
		get
		{
			return (SizeIncreasedComponent)GetComponent(128);
		}
	}

	public bool hasSizeIncreased
	{
		get
		{
			return HasComponent(128);
		}
	}

	public bool isSliding
	{
		get
		{
			return HasComponent(129);
		}
		set
		{
			if (value != isSliding)
			{
				if (value)
				{
					AddComponent(129, slidingComponent);
				}
				else
				{
					RemoveComponent(129);
				}
			}
		}
	}

	public SparkleComponent sparkle
	{
		get
		{
			return (SparkleComponent)GetComponent(130);
		}
	}

	public bool hasSparkle
	{
		get
		{
			return HasComponent(130);
		}
	}

	public bool isSpecialRun
	{
		get
		{
			return HasComponent(131);
		}
		set
		{
			if (value != isSpecialRun)
			{
				if (value)
				{
					AddComponent(131, specialRunComponent);
				}
				else
				{
					RemoveComponent(131);
				}
			}
		}
	}

	public SpeedDataComponent speedData
	{
		get
		{
			return (SpeedDataComponent)GetComponent(132);
		}
	}

	public bool hasSpeedData
	{
		get
		{
			return HasComponent(132);
		}
	}

	public SpeedUpComponent speedUp
	{
		get
		{
			return (SpeedUpComponent)GetComponent(133);
		}
	}

	public bool hasSpeedUp
	{
		get
		{
			return HasComponent(133);
		}
	}

	public StaminaComponent stamina
	{
		get
		{
			return (StaminaComponent)GetComponent(134);
		}
	}

	public bool hasStamina
	{
		get
		{
			return HasComponent(134);
		}
	}

	public StaminasListenerComponent staminasListener
	{
		get
		{
			return (StaminasListenerComponent)GetComponent(135);
		}
	}

	public bool hasStaminasListener
	{
		get
		{
			return HasComponent(135);
		}
	}

	public SwingingDataComponent swingingData
	{
		get
		{
			return (SwingingDataComponent)GetComponent(136);
		}
	}

	public bool hasSwingingData
	{
		get
		{
			return HasComponent(136);
		}
	}

	public TutorialPerformedComponent tutorialPerformed
	{
		get
		{
			return (TutorialPerformedComponent)GetComponent(137);
		}
	}

	public bool hasTutorialPerformed
	{
		get
		{
			return HasComponent(137);
		}
	}

	public TutorialPlayStepComponent tutorialPlayStep
	{
		get
		{
			return (TutorialPlayStepComponent)GetComponent(138);
		}
	}

	public bool hasTutorialPlayStep
	{
		get
		{
			return HasComponent(138);
		}
	}

	public TutorialsListenerComponent tutorialsListener
	{
		get
		{
			return (TutorialsListenerComponent)GetComponent(139);
		}
	}

	public bool hasTutorialsListener
	{
		get
		{
			return HasComponent(139);
		}
	}

	public TutorialTriggerComponent tutorialTrigger
	{
		get
		{
			return (TutorialTriggerComponent)GetComponent(140);
		}
	}

	public bool hasTutorialTrigger
	{
		get
		{
			return HasComponent(140);
		}
	}

	public bool isUnderTheScreen
	{
		get
		{
			return HasComponent(141);
		}
		set
		{
			if (value != isUnderTheScreen)
			{
				if (value)
				{
					AddComponent(141, underTheScreenComponent);
				}
				else
				{
					RemoveComponent(141);
				}
			}
		}
	}

	public UpgradeScoreComponent upgradeScore
	{
		get
		{
			return (UpgradeScoreComponent)GetComponent(142);
		}
	}

	public bool hasUpgradeScore
	{
		get
		{
			return HasComponent(142);
		}
	}

	public UpgradeSingleScoreComponent upgradeSingleScore
	{
		get
		{
			return (UpgradeSingleScoreComponent)GetComponent(143);
		}
	}

	public bool hasUpgradeSingleScore
	{
		get
		{
			return HasComponent(143);
		}
	}

	public ViewComponent view
	{
		get
		{
			return (ViewComponent)GetComponent(144);
		}
	}

	public bool hasView
	{
		get
		{
			return HasComponent(144);
		}
	}

	public WaitToStartComponent waitToStart
	{
		get
		{
			return (WaitToStartComponent)GetComponent(145);
		}
	}

	public bool hasWaitToStart
	{
		get
		{
			return HasComponent(145);
		}
	}

	public WorldComponent world
	{
		get
		{
			return (WorldComponent)GetComponent(146);
		}
	}

	public bool hasWorld
	{
		get
		{
			return HasComponent(146);
		}
	}

	public WorldTimeScaleComponent worldTimeScale
	{
		get
		{
			return (WorldTimeScaleComponent)GetComponent(147);
		}
	}

	public bool hasWorldTimeScale
	{
		get
		{
			return HasComponent(147);
		}
	}

	public void AddAbilityChangedListener(IAbilityChangedListener newListener)
	{
		int index = 0;
		AbilityChangedListenerComponent abilityChangedListenerComponent = CreateComponent<AbilityChangedListenerComponent>(index);
		abilityChangedListenerComponent.listener = newListener;
		AddComponent(index, abilityChangedListenerComponent);
	}

	public void ReplaceAbilityChangedListener(IAbilityChangedListener newListener)
	{
		int index = 0;
		AbilityChangedListenerComponent abilityChangedListenerComponent = CreateComponent<AbilityChangedListenerComponent>(index);
		abilityChangedListenerComponent.listener = newListener;
		ReplaceComponent(index, abilityChangedListenerComponent);
	}

	public void RemoveAbilityChangedListener()
	{
		RemoveComponent(0);
	}

	public void AddAbilityChargingProgress(float newPercentCharged, float newTimeLeft)
	{
		int index = 1;
		AbilityChargingProgress abilityChargingProgress = CreateComponent<AbilityChargingProgress>(index);
		abilityChargingProgress.PercentCharged = newPercentCharged;
		abilityChargingProgress.TimeLeft = newTimeLeft;
		AddComponent(index, abilityChargingProgress);
	}

	public void ReplaceAbilityChargingProgress(float newPercentCharged, float newTimeLeft)
	{
		int index = 1;
		AbilityChargingProgress abilityChargingProgress = CreateComponent<AbilityChargingProgress>(index);
		abilityChargingProgress.PercentCharged = newPercentCharged;
		abilityChargingProgress.TimeLeft = newTimeLeft;
		ReplaceComponent(index, abilityChargingProgress);
	}

	public void RemoveAbilityChargingProgress()
	{
		RemoveComponent(1);
	}

	public void AddAbilityChargingTime(float newChargingTime)
	{
		int index = 2;
		AbilityChargingTimeComponent abilityChargingTimeComponent = CreateComponent<AbilityChargingTimeComponent>(index);
		abilityChargingTimeComponent.ChargingTime = newChargingTime;
		AddComponent(index, abilityChargingTimeComponent);
	}

	public void ReplaceAbilityChargingTime(float newChargingTime)
	{
		int index = 2;
		AbilityChargingTimeComponent abilityChargingTimeComponent = CreateComponent<AbilityChargingTimeComponent>(index);
		abilityChargingTimeComponent.ChargingTime = newChargingTime;
		ReplaceComponent(index, abilityChargingTimeComponent);
	}

	public void RemoveAbilityChargingTime()
	{
		RemoveComponent(2);
	}

	public void AddAbilityCreateToken(List<string> newAvailableTokenNames)
	{
		int index = 3;
		AbilityCreateTokenComponent abilityCreateTokenComponent = CreateComponent<AbilityCreateTokenComponent>(index);
		abilityCreateTokenComponent.AvailableTokenNames = newAvailableTokenNames;
		AddComponent(index, abilityCreateTokenComponent);
	}

	public void ReplaceAbilityCreateToken(List<string> newAvailableTokenNames)
	{
		int index = 3;
		AbilityCreateTokenComponent abilityCreateTokenComponent = CreateComponent<AbilityCreateTokenComponent>(index);
		abilityCreateTokenComponent.AvailableTokenNames = newAvailableTokenNames;
		ReplaceComponent(index, abilityCreateTokenComponent);
	}

	public void RemoveAbilityCreateToken()
	{
		RemoveComponent(3);
	}

	public void AddAbilityDuration(float newDuration)
	{
		int index = 5;
		AbilityDurationComponent abilityDurationComponent = CreateComponent<AbilityDurationComponent>(index);
		abilityDurationComponent.Duration = newDuration;
		AddComponent(index, abilityDurationComponent);
	}

	public void ReplaceAbilityDuration(float newDuration)
	{
		int index = 5;
		AbilityDurationComponent abilityDurationComponent = CreateComponent<AbilityDurationComponent>(index);
		abilityDurationComponent.Duration = newDuration;
		ReplaceComponent(index, abilityDurationComponent);
	}

	public void RemoveAbilityDuration()
	{
		RemoveComponent(5);
	}

	public void AddAbilityFasterMovement(float newBonus)
	{
		int index = 6;
		AbilityFasterMovementComponent abilityFasterMovementComponent = CreateComponent<AbilityFasterMovementComponent>(index);
		abilityFasterMovementComponent.Bonus = newBonus;
		AddComponent(index, abilityFasterMovementComponent);
	}

	public void ReplaceAbilityFasterMovement(float newBonus)
	{
		int index = 6;
		AbilityFasterMovementComponent abilityFasterMovementComponent = CreateComponent<AbilityFasterMovementComponent>(index);
		abilityFasterMovementComponent.Bonus = newBonus;
		ReplaceComponent(index, abilityFasterMovementComponent);
	}

	public void RemoveAbilityFasterMovement()
	{
		RemoveComponent(6);
	}

	public void AddAbilityGiantSpeedup(float newFactor)
	{
		int index = 7;
		AbilityGiantSpeedupComponent abilityGiantSpeedupComponent = CreateComponent<AbilityGiantSpeedupComponent>(index);
		abilityGiantSpeedupComponent.Factor = newFactor;
		AddComponent(index, abilityGiantSpeedupComponent);
	}

	public void ReplaceAbilityGiantSpeedup(float newFactor)
	{
		int index = 7;
		AbilityGiantSpeedupComponent abilityGiantSpeedupComponent = CreateComponent<AbilityGiantSpeedupComponent>(index);
		abilityGiantSpeedupComponent.Factor = newFactor;
		ReplaceComponent(index, abilityGiantSpeedupComponent);
	}

	public void RemoveAbilityGiantSpeedup()
	{
		RemoveComponent(7);
	}

	public void AddAbilityResurrect(float newStaminaFactor)
	{
		int index = 10;
		AbilityResurrectComponent abilityResurrectComponent = CreateComponent<AbilityResurrectComponent>(index);
		abilityResurrectComponent.StaminaFactor = newStaminaFactor;
		AddComponent(index, abilityResurrectComponent);
	}

	public void ReplaceAbilityResurrect(float newStaminaFactor)
	{
		int index = 10;
		AbilityResurrectComponent abilityResurrectComponent = CreateComponent<AbilityResurrectComponent>(index);
		abilityResurrectComponent.StaminaFactor = newStaminaFactor;
		ReplaceComponent(index, abilityResurrectComponent);
	}

	public void RemoveAbilityResurrect()
	{
		RemoveComponent(10);
	}

	public void AddAbilityScoreBonus(int newAmount)
	{
		int index = 12;
		AbilityScoreBonusComponent abilityScoreBonusComponent = CreateComponent<AbilityScoreBonusComponent>(index);
		abilityScoreBonusComponent.Amount = newAmount;
		AddComponent(index, abilityScoreBonusComponent);
	}

	public void ReplaceAbilityScoreBonus(int newAmount)
	{
		int index = 12;
		AbilityScoreBonusComponent abilityScoreBonusComponent = CreateComponent<AbilityScoreBonusComponent>(index);
		abilityScoreBonusComponent.Amount = newAmount;
		ReplaceComponent(index, abilityScoreBonusComponent);
	}

	public void RemoveAbilityScoreBonus()
	{
		RemoveComponent(12);
	}

	public void AddAbilityShield(float newDuration)
	{
		int index = 13;
		AbilityShieldComponent abilityShieldComponent = CreateComponent<AbilityShieldComponent>(index);
		abilityShieldComponent.Duration = newDuration;
		AddComponent(index, abilityShieldComponent);
	}

	public void ReplaceAbilityShield(float newDuration)
	{
		int index = 13;
		AbilityShieldComponent abilityShieldComponent = CreateComponent<AbilityShieldComponent>(index);
		abilityShieldComponent.Duration = newDuration;
		ReplaceComponent(index, abilityShieldComponent);
	}

	public void RemoveAbilityShield()
	{
		RemoveComponent(13);
	}

	public void AddAbilitySpeedup(float newMultiplier)
	{
		int index = 14;
		AbilitySpeedupComponent abilitySpeedupComponent = CreateComponent<AbilitySpeedupComponent>(index);
		abilitySpeedupComponent.Multiplier = newMultiplier;
		AddComponent(index, abilitySpeedupComponent);
	}

	public void ReplaceAbilitySpeedup(float newMultiplier)
	{
		int index = 14;
		AbilitySpeedupComponent abilitySpeedupComponent = CreateComponent<AbilitySpeedupComponent>(index);
		abilitySpeedupComponent.Multiplier = newMultiplier;
		ReplaceComponent(index, abilitySpeedupComponent);
	}

	public void RemoveAbilitySpeedup()
	{
		RemoveComponent(14);
	}

	public void AddAbilityStateChangedListener(IAbilityStateChangedListener newListener)
	{
		int index = 15;
		AbilityStateChangedListenerComponent abilityStateChangedListenerComponent = CreateComponent<AbilityStateChangedListenerComponent>(index);
		abilityStateChangedListenerComponent.listener = newListener;
		AddComponent(index, abilityStateChangedListenerComponent);
	}

	public void ReplaceAbilityStateChangedListener(IAbilityStateChangedListener newListener)
	{
		int index = 15;
		AbilityStateChangedListenerComponent abilityStateChangedListenerComponent = CreateComponent<AbilityStateChangedListenerComponent>(index);
		abilityStateChangedListenerComponent.listener = newListener;
		ReplaceComponent(index, abilityStateChangedListenerComponent);
	}

	public void RemoveAbilityStateChangedListener()
	{
		RemoveComponent(15);
	}

	public void AddAbilityTimer(float newTimeLeft)
	{
		int index = 16;
		AbilityTimerComponent abilityTimerComponent = CreateComponent<AbilityTimerComponent>(index);
		abilityTimerComponent.TimeLeft = newTimeLeft;
		AddComponent(index, abilityTimerComponent);
	}

	public void ReplaceAbilityTimer(float newTimeLeft)
	{
		int index = 16;
		AbilityTimerComponent abilityTimerComponent = CreateComponent<AbilityTimerComponent>(index);
		abilityTimerComponent.TimeLeft = newTimeLeft;
		ReplaceComponent(index, abilityTimerComponent);
	}

	public void RemoveAbilityTimer()
	{
		RemoveComponent(16);
	}

	public void AddAbilityUpgradeScore(float newDistance)
	{
		int index = 17;
		AbilityUpgradeScoreComponent abilityUpgradeScoreComponent = CreateComponent<AbilityUpgradeScoreComponent>(index);
		abilityUpgradeScoreComponent.Distance = newDistance;
		AddComponent(index, abilityUpgradeScoreComponent);
	}

	public void ReplaceAbilityUpgradeScore(float newDistance)
	{
		int index = 17;
		AbilityUpgradeScoreComponent abilityUpgradeScoreComponent = CreateComponent<AbilityUpgradeScoreComponent>(index);
		abilityUpgradeScoreComponent.Distance = newDistance;
		ReplaceComponent(index, abilityUpgradeScoreComponent);
	}

	public void RemoveAbilityUpgradeScore()
	{
		RemoveComponent(17);
	}

	public void AddAsset(string newName)
	{
		int index = 19;
		AssetComponent assetComponent = CreateComponent<AssetComponent>(index);
		assetComponent.name = newName;
		AddComponent(index, assetComponent);
	}

	public void ReplaceAsset(string newName)
	{
		int index = 19;
		AssetComponent assetComponent = CreateComponent<AssetComponent>(index);
		assetComponent.name = newName;
		ReplaceComponent(index, assetComponent);
	}

	public void RemoveAsset()
	{
		RemoveComponent(19);
	}

	public void AddAsyncRequest(ResourceRequest newRequest, Action newCallback)
	{
		int index = 20;
		AsyncRequestComponent asyncRequestComponent = CreateComponent<AsyncRequestComponent>(index);
		asyncRequestComponent.Request = newRequest;
		asyncRequestComponent.callback = newCallback;
		AddComponent(index, asyncRequestComponent);
	}

	public void ReplaceAsyncRequest(ResourceRequest newRequest, Action newCallback)
	{
		int index = 20;
		AsyncRequestComponent asyncRequestComponent = CreateComponent<AsyncRequestComponent>(index);
		asyncRequestComponent.Request = newRequest;
		asyncRequestComponent.callback = newCallback;
		ReplaceComponent(index, asyncRequestComponent);
	}

	public void RemoveAsyncRequest()
	{
		RemoveComponent(20);
	}

	public void AddChangeCameraSizeTrigger(float newSize)
	{
		int index = 24;
		ChangeCameraSizeTriggerComponent changeCameraSizeTriggerComponent = CreateComponent<ChangeCameraSizeTriggerComponent>(index);
		changeCameraSizeTriggerComponent.Size = newSize;
		AddComponent(index, changeCameraSizeTriggerComponent);
	}

	public void ReplaceChangeCameraSizeTrigger(float newSize)
	{
		int index = 24;
		ChangeCameraSizeTriggerComponent changeCameraSizeTriggerComponent = CreateComponent<ChangeCameraSizeTriggerComponent>(index);
		changeCameraSizeTriggerComponent.Size = newSize;
		ReplaceComponent(index, changeCameraSizeTriggerComponent);
	}

	public void RemoveChangeCameraSizeTrigger()
	{
		RemoveComponent(24);
	}

	public void AddChangeScaleTime(float newFinalTimeScale, float newTransitionDuration, float newSlowDownDuration, float newTimer)
	{
		int index = 25;
		ChangeScaleTimeComponent changeScaleTimeComponent = CreateComponent<ChangeScaleTimeComponent>(index);
		changeScaleTimeComponent.finalTimeScale = newFinalTimeScale;
		changeScaleTimeComponent.transitionDuration = newTransitionDuration;
		changeScaleTimeComponent.slowDownDuration = newSlowDownDuration;
		changeScaleTimeComponent.timer = newTimer;
		AddComponent(index, changeScaleTimeComponent);
	}

	public void ReplaceChangeScaleTime(float newFinalTimeScale, float newTransitionDuration, float newSlowDownDuration, float newTimer)
	{
		int index = 25;
		ChangeScaleTimeComponent changeScaleTimeComponent = CreateComponent<ChangeScaleTimeComponent>(index);
		changeScaleTimeComponent.finalTimeScale = newFinalTimeScale;
		changeScaleTimeComponent.transitionDuration = newTransitionDuration;
		changeScaleTimeComponent.slowDownDuration = newSlowDownDuration;
		changeScaleTimeComponent.timer = newTimer;
		ReplaceComponent(index, changeScaleTimeComponent);
	}

	public void RemoveChangeScaleTime()
	{
		RemoveComponent(25);
	}

	public void AddChangeWorldSkin(string newWorldSuffix)
	{
		int index = 26;
		ChangeWorldSkinComponent changeWorldSkinComponent = CreateComponent<ChangeWorldSkinComponent>(index);
		changeWorldSkinComponent.WorldSuffix = newWorldSuffix;
		AddComponent(index, changeWorldSkinComponent);
	}

	public void ReplaceChangeWorldSkin(string newWorldSuffix)
	{
		int index = 26;
		ChangeWorldSkinComponent changeWorldSkinComponent = CreateComponent<ChangeWorldSkinComponent>(index);
		changeWorldSkinComponent.WorldSuffix = newWorldSuffix;
		ReplaceComponent(index, changeWorldSkinComponent);
	}

	public void RemoveChangeWorldSkin()
	{
		RemoveComponent(26);
	}

	public void AddChangeWorldSpeed(float newSpeed)
	{
		int index = 27;
		ChangeWorldSpeedComponent changeWorldSpeedComponent = CreateComponent<ChangeWorldSpeedComponent>(index);
		changeWorldSpeedComponent.Speed = newSpeed;
		AddComponent(index, changeWorldSpeedComponent);
	}

	public void ReplaceChangeWorldSpeed(float newSpeed)
	{
		int index = 27;
		ChangeWorldSpeedComponent changeWorldSpeedComponent = CreateComponent<ChangeWorldSpeedComponent>(index);
		changeWorldSpeedComponent.Speed = newSpeed;
		ReplaceComponent(index, changeWorldSpeedComponent);
	}

	public void RemoveChangeWorldSpeed()
	{
		RemoveComponent(27);
	}

	public void AddCoins(int newValue)
	{
		int index = 29;
		CoinsComponent coinsComponent = CreateComponent<CoinsComponent>(index);
		coinsComponent.Value = newValue;
		AddComponent(index, coinsComponent);
	}

	public void ReplaceCoins(int newValue)
	{
		int index = 29;
		CoinsComponent coinsComponent = CreateComponent<CoinsComponent>(index);
		coinsComponent.Value = newValue;
		ReplaceComponent(index, coinsComponent);
	}

	public void RemoveCoins()
	{
		RemoveComponent(29);
	}

	public void AddCoinsListener(ICoinListener newListener)
	{
		int index = 30;
		CoinsListenerComponent coinsListenerComponent = CreateComponent<CoinsListenerComponent>(index);
		coinsListenerComponent.listener = newListener;
		AddComponent(index, coinsListenerComponent);
	}

	public void ReplaceCoinsListener(ICoinListener newListener)
	{
		int index = 30;
		CoinsListenerComponent coinsListenerComponent = CreateComponent<CoinsListenerComponent>(index);
		coinsListenerComponent.listener = newListener;
		ReplaceComponent(index, coinsListenerComponent);
	}

	public void RemoveCoinsListener()
	{
		RemoveComponent(30);
	}

	public void AddCollectibleCoin(int newValue)
	{
		int index = 32;
		CollectibleCoinComponent collectibleCoinComponent = CreateComponent<CollectibleCoinComponent>(index);
		collectibleCoinComponent.Value = newValue;
		AddComponent(index, collectibleCoinComponent);
	}

	public void ReplaceCollectibleCoin(int newValue)
	{
		int index = 32;
		CollectibleCoinComponent collectibleCoinComponent = CreateComponent<CollectibleCoinComponent>(index);
		collectibleCoinComponent.Value = newValue;
		ReplaceComponent(index, collectibleCoinComponent);
	}

	public void RemoveCollectibleCoin()
	{
		RemoveComponent(32);
	}

	public void AddCollectibleData(CollectibleType newType)
	{
		int index = 34;
		CollectibleDataComponent collectibleDataComponent = CreateComponent<CollectibleDataComponent>(index);
		collectibleDataComponent.Type = newType;
		AddComponent(index, collectibleDataComponent);
	}

	public void ReplaceCollectibleData(CollectibleType newType)
	{
		int index = 34;
		CollectibleDataComponent collectibleDataComponent = CreateComponent<CollectibleDataComponent>(index);
		collectibleDataComponent.Type = newType;
		ReplaceComponent(index, collectibleDataComponent);
	}

	public void RemoveCollectibleData()
	{
		RemoveComponent(34);
	}

	public void AddCollectibleScore(int newValue)
	{
		int index = 37;
		CollectibleScoreComponent collectibleScoreComponent = CreateComponent<CollectibleScoreComponent>(index);
		collectibleScoreComponent.Value = newValue;
		AddComponent(index, collectibleScoreComponent);
	}

	public void ReplaceCollectibleScore(int newValue)
	{
		int index = 37;
		CollectibleScoreComponent collectibleScoreComponent = CreateComponent<CollectibleScoreComponent>(index);
		collectibleScoreComponent.Value = newValue;
		ReplaceComponent(index, collectibleScoreComponent);
	}

	public void RemoveCollectibleScore()
	{
		RemoveComponent(37);
	}

	public void AddCollectibleStamina(float newValue)
	{
		int index = 40;
		CollectibleStaminaComponent collectibleStaminaComponent = CreateComponent<CollectibleStaminaComponent>(index);
		collectibleStaminaComponent.Value = newValue;
		AddComponent(index, collectibleStaminaComponent);
	}

	public void ReplaceCollectibleStamina(float newValue)
	{
		int index = 40;
		CollectibleStaminaComponent collectibleStaminaComponent = CreateComponent<CollectibleStaminaComponent>(index);
		collectibleStaminaComponent.Value = newValue;
		ReplaceComponent(index, collectibleStaminaComponent);
	}

	public void RemoveCollectibleStamina()
	{
		RemoveComponent(40);
	}

	public void AddCollectSound(string newSoundId)
	{
		int index = 42;
		CollectSoundComponent collectSoundComponent = CreateComponent<CollectSoundComponent>(index);
		collectSoundComponent.SoundId = newSoundId;
		AddComponent(index, collectSoundComponent);
	}

	public void ReplaceCollectSound(string newSoundId)
	{
		int index = 42;
		CollectSoundComponent collectSoundComponent = CreateComponent<CollectSoundComponent>(index);
		collectSoundComponent.SoundId = newSoundId;
		ReplaceComponent(index, collectSoundComponent);
	}

	public void RemoveCollectSound()
	{
		RemoveComponent(42);
	}

	public void AddDailyMissionCollect(Dictionary<CollectibleType, KeyValuePair<DailyMissionGroup, int>> newMissionDict)
	{
		int index = 44;
		DailyMissionCollectComponent dailyMissionCollectComponent = CreateComponent<DailyMissionCollectComponent>(index);
		dailyMissionCollectComponent.MissionDict = newMissionDict;
		AddComponent(index, dailyMissionCollectComponent);
	}

	public void ReplaceDailyMissionCollect(Dictionary<CollectibleType, KeyValuePair<DailyMissionGroup, int>> newMissionDict)
	{
		int index = 44;
		DailyMissionCollectComponent dailyMissionCollectComponent = CreateComponent<DailyMissionCollectComponent>(index);
		dailyMissionCollectComponent.MissionDict = newMissionDict;
		ReplaceComponent(index, dailyMissionCollectComponent);
	}

	public void RemoveDailyMissionCollect()
	{
		RemoveComponent(44);
	}

	public void AddDailyMissionDestroyObstacle(DailyMissionGroup newGroup, int newValue)
	{
		int index = 45;
		DailyMissionDestroyObstacleComponent dailyMissionDestroyObstacleComponent = CreateComponent<DailyMissionDestroyObstacleComponent>(index);
		dailyMissionDestroyObstacleComponent.Group = newGroup;
		dailyMissionDestroyObstacleComponent.Value = newValue;
		AddComponent(index, dailyMissionDestroyObstacleComponent);
	}

	public void ReplaceDailyMissionDestroyObstacle(DailyMissionGroup newGroup, int newValue)
	{
		int index = 45;
		DailyMissionDestroyObstacleComponent dailyMissionDestroyObstacleComponent = CreateComponent<DailyMissionDestroyObstacleComponent>(index);
		dailyMissionDestroyObstacleComponent.Group = newGroup;
		dailyMissionDestroyObstacleComponent.Value = newValue;
		ReplaceComponent(index, dailyMissionDestroyObstacleComponent);
	}

	public void RemoveDailyMissionDestroyObstacle()
	{
		RemoveComponent(45);
	}

	public void AddDailyMissionPlayMissions(DailyMissionGroup newGroup)
	{
		int index = 46;
		DailyMissionPlayMissionsComponent dailyMissionPlayMissionsComponent = CreateComponent<DailyMissionPlayMissionsComponent>(index);
		dailyMissionPlayMissionsComponent.Group = newGroup;
		AddComponent(index, dailyMissionPlayMissionsComponent);
	}

	public void ReplaceDailyMissionPlayMissions(DailyMissionGroup newGroup)
	{
		int index = 46;
		DailyMissionPlayMissionsComponent dailyMissionPlayMissionsComponent = CreateComponent<DailyMissionPlayMissionsComponent>(index);
		dailyMissionPlayMissionsComponent.Group = newGroup;
		ReplaceComponent(index, dailyMissionPlayMissionsComponent);
	}

	public void RemoveDailyMissionPlayMissions()
	{
		RemoveComponent(46);
	}

	public void AddDropping(bool newActivated, float newDelay)
	{
		int index = 51;
		DroppingComponent droppingComponent = CreateComponent<DroppingComponent>(index);
		droppingComponent.Activated = newActivated;
		droppingComponent.Delay = newDelay;
		AddComponent(index, droppingComponent);
	}

	public void ReplaceDropping(bool newActivated, float newDelay)
	{
		int index = 51;
		DroppingComponent droppingComponent = CreateComponent<DroppingComponent>(index);
		droppingComponent.Activated = newActivated;
		droppingComponent.Delay = newDelay;
		ReplaceComponent(index, droppingComponent);
	}

	public void RemoveDropping()
	{
		RemoveComponent(51);
	}

	public void AddFloating(float newAmplitude, float newSpeed, float newOffset, bool newHasLocalTimer, float newCurrentTime)
	{
		int index = 53;
		FloatingComponent floatingComponent = CreateComponent<FloatingComponent>(index);
		floatingComponent.Amplitude = newAmplitude;
		floatingComponent.Speed = newSpeed;
		floatingComponent.Offset = newOffset;
		floatingComponent.HasLocalTimer = newHasLocalTimer;
		floatingComponent.CurrentTime = newCurrentTime;
		AddComponent(index, floatingComponent);
	}

	public void ReplaceFloating(float newAmplitude, float newSpeed, float newOffset, bool newHasLocalTimer, float newCurrentTime)
	{
		int index = 53;
		FloatingComponent floatingComponent = CreateComponent<FloatingComponent>(index);
		floatingComponent.Amplitude = newAmplitude;
		floatingComponent.Speed = newSpeed;
		floatingComponent.Offset = newOffset;
		floatingComponent.HasLocalTimer = newHasLocalTimer;
		floatingComponent.CurrentTime = newCurrentTime;
		ReplaceComponent(index, floatingComponent);
	}

	public void RemoveFloating()
	{
		RemoveComponent(53);
	}

	public void AddGainedScore(int newScore)
	{
		int index = 55;
		GainedScoreComponent gainedScoreComponent = CreateComponent<GainedScoreComponent>(index);
		gainedScoreComponent.Score = newScore;
		AddComponent(index, gainedScoreComponent);
	}

	public void ReplaceGainedScore(int newScore)
	{
		int index = 55;
		GainedScoreComponent gainedScoreComponent = CreateComponent<GainedScoreComponent>(index);
		gainedScoreComponent.Score = newScore;
		ReplaceComponent(index, gainedScoreComponent);
	}

	public void RemoveGainedScore()
	{
		RemoveComponent(55);
	}

	public void AddGrounded(IEntity newGround)
	{
		int index = 58;
		GroundedComponent groundedComponent = CreateComponent<GroundedComponent>(index);
		groundedComponent.ground = newGround;
		AddComponent(index, groundedComponent);
	}

	public void ReplaceGrounded(IEntity newGround)
	{
		int index = 58;
		GroundedComponent groundedComponent = CreateComponent<GroundedComponent>(index);
		groundedComponent.ground = newGround;
		ReplaceComponent(index, groundedComponent);
	}

	public void RemoveGrounded()
	{
		RemoveComponent(58);
	}

	public void AddIngameBg(bgParallaxBehaviour newBg)
	{
		int index = 60;
		IngameBgComponent ingameBgComponent = CreateComponent<IngameBgComponent>(index);
		ingameBgComponent.Bg = newBg;
		AddComponent(index, ingameBgComponent);
	}

	public void ReplaceIngameBg(bgParallaxBehaviour newBg)
	{
		int index = 60;
		IngameBgComponent ingameBgComponent = CreateComponent<IngameBgComponent>(index);
		ingameBgComponent.Bg = newBg;
		ReplaceComponent(index, ingameBgComponent);
	}

	public void RemoveIngameBg()
	{
		RemoveComponent(60);
	}

	public void AddInitialTimeScale(float newInitialScaleTime)
	{
		int index = 61;
		InitialTimeScaleComponent initialTimeScaleComponent = CreateComponent<InitialTimeScaleComponent>(index);
		initialTimeScaleComponent.initialScaleTime = newInitialScaleTime;
		AddComponent(index, initialTimeScaleComponent);
	}

	public void ReplaceInitialTimeScale(float newInitialScaleTime)
	{
		int index = 61;
		InitialTimeScaleComponent initialTimeScaleComponent = CreateComponent<InitialTimeScaleComponent>(index);
		initialTimeScaleComponent.initialScaleTime = newInitialScaleTime;
		ReplaceComponent(index, initialTimeScaleComponent);
	}

	public void RemoveInitialTimeScale()
	{
		RemoveComponent(61);
	}

	public void AddInitialTrigger(string newTriggerName)
	{
		int index = 62;
		InitialTriggerComponent initialTriggerComponent = CreateComponent<InitialTriggerComponent>(index);
		initialTriggerComponent.TriggerName = newTriggerName;
		AddComponent(index, initialTriggerComponent);
	}

	public void ReplaceInitialTrigger(string newTriggerName)
	{
		int index = 62;
		InitialTriggerComponent initialTriggerComponent = CreateComponent<InitialTriggerComponent>(index);
		initialTriggerComponent.TriggerName = newTriggerName;
		ReplaceComponent(index, initialTriggerComponent);
	}

	public void RemoveInitialTrigger()
	{
		RemoveComponent(62);
	}

	public void AddInvulnerability(float newTimeLeft)
	{
		int index = 63;
		InvulnerabilityComponent invulnerabilityComponent = CreateComponent<InvulnerabilityComponent>(index);
		invulnerabilityComponent.TimeLeft = newTimeLeft;
		AddComponent(index, invulnerabilityComponent);
	}

	public void ReplaceInvulnerability(float newTimeLeft)
	{
		int index = 63;
		InvulnerabilityComponent invulnerabilityComponent = CreateComponent<InvulnerabilityComponent>(index);
		invulnerabilityComponent.TimeLeft = newTimeLeft;
		ReplaceComponent(index, invulnerabilityComponent);
	}

	public void RemoveInvulnerability()
	{
		RemoveComponent(63);
	}

	public void AddJumping(int newQuantity)
	{
		int index = 64;
		JumpingComponent jumpingComponent = CreateComponent<JumpingComponent>(index);
		jumpingComponent.Quantity = newQuantity;
		AddComponent(index, jumpingComponent);
	}

	public void ReplaceJumping(int newQuantity)
	{
		int index = 64;
		JumpingComponent jumpingComponent = CreateComponent<JumpingComponent>(index);
		jumpingComponent.Quantity = newQuantity;
		ReplaceComponent(index, jumpingComponent);
	}

	public void RemoveJumping()
	{
		RemoveComponent(64);
	}

	public void AddLevelEnd(float newDistanceToEnd)
	{
		int index = 66;
		LevelEndComponent levelEndComponent = CreateComponent<LevelEndComponent>(index);
		levelEndComponent.DistanceToEnd = newDistanceToEnd;
		AddComponent(index, levelEndComponent);
	}

	public void ReplaceLevelEnd(float newDistanceToEnd)
	{
		int index = 66;
		LevelEndComponent levelEndComponent = CreateComponent<LevelEndComponent>(index);
		levelEndComponent.DistanceToEnd = newDistanceToEnd;
		ReplaceComponent(index, levelEndComponent);
	}

	public void RemoveLevelEnd()
	{
		RemoveComponent(66);
	}

	public void AddLevelFinished(float newDistanceToEnd)
	{
		int index = 67;
		LevelFinishedComponent levelFinishedComponent = CreateComponent<LevelFinishedComponent>(index);
		levelFinishedComponent.DistanceToEnd = newDistanceToEnd;
		AddComponent(index, levelFinishedComponent);
	}

	public void ReplaceLevelFinished(float newDistanceToEnd)
	{
		int index = 67;
		LevelFinishedComponent levelFinishedComponent = CreateComponent<LevelFinishedComponent>(index);
		levelFinishedComponent.DistanceToEnd = newDistanceToEnd;
		ReplaceComponent(index, levelFinishedComponent);
	}

	public void RemoveLevelFinished()
	{
		RemoveComponent(67);
	}

	public void AddLevelFinishedListener(ILevelFinishedListener newListener)
	{
		int index = 68;
		LevelFinishedListenerComponent levelFinishedListenerComponent = CreateComponent<LevelFinishedListenerComponent>(index);
		levelFinishedListenerComponent.listener = newListener;
		AddComponent(index, levelFinishedListenerComponent);
	}

	public void ReplaceLevelFinishedListener(ILevelFinishedListener newListener)
	{
		int index = 68;
		LevelFinishedListenerComponent levelFinishedListenerComponent = CreateComponent<LevelFinishedListenerComponent>(index);
		levelFinishedListenerComponent.listener = newListener;
		ReplaceComponent(index, levelFinishedListenerComponent);
	}

	public void RemoveLevelFinishedListener()
	{
		RemoveComponent(68);
	}

	public void AddLevelSettings(LevelTutorialSettings newLevelTutorialSettings)
	{
		int index = 69;
		LevelSettingsComponent levelSettingsComponent = CreateComponent<LevelSettingsComponent>(index);
		levelSettingsComponent.levelTutorialSettings = newLevelTutorialSettings;
		AddComponent(index, levelSettingsComponent);
	}

	public void ReplaceLevelSettings(LevelTutorialSettings newLevelTutorialSettings)
	{
		int index = 69;
		LevelSettingsComponent levelSettingsComponent = CreateComponent<LevelSettingsComponent>(index);
		levelSettingsComponent.levelTutorialSettings = newLevelTutorialSettings;
		ReplaceComponent(index, levelSettingsComponent);
	}

	public void RemoveLevelSettings()
	{
		RemoveComponent(69);
	}

	public void AddMagnetized(float newDuration)
	{
		int index = 71;
		MagnetizedComponent magnetizedComponent = CreateComponent<MagnetizedComponent>(index);
		magnetizedComponent.Duration = newDuration;
		AddComponent(index, magnetizedComponent);
	}

	public void ReplaceMagnetized(float newDuration)
	{
		int index = 71;
		MagnetizedComponent magnetizedComponent = CreateComponent<MagnetizedComponent>(index);
		magnetizedComponent.Duration = newDuration;
		ReplaceComponent(index, magnetizedComponent);
	}

	public void RemoveMagnetized()
	{
		RemoveComponent(71);
	}

	public void AddMissionCollect(CollectibleType newTokenType)
	{
		int index = 73;
		MissionCollectComponent missionCollectComponent = CreateComponent<MissionCollectComponent>(index);
		missionCollectComponent.TokenType = newTokenType;
		AddComponent(index, missionCollectComponent);
	}

	public void ReplaceMissionCollect(CollectibleType newTokenType)
	{
		int index = 73;
		MissionCollectComponent missionCollectComponent = CreateComponent<MissionCollectComponent>(index);
		missionCollectComponent.TokenType = newTokenType;
		ReplaceComponent(index, missionCollectComponent);
	}

	public void RemoveMissionCollect()
	{
		RemoveComponent(73);
	}

	public void AddMissionCounter(int newValue)
	{
		int index = 74;
		MissionCounterComponent missionCounterComponent = CreateComponent<MissionCounterComponent>(index);
		missionCounterComponent.Value = newValue;
		AddComponent(index, missionCounterComponent);
	}

	public void ReplaceMissionCounter(int newValue)
	{
		int index = 74;
		MissionCounterComponent missionCounterComponent = CreateComponent<MissionCounterComponent>(index);
		missionCounterComponent.Value = newValue;
		ReplaceComponent(index, missionCounterComponent);
	}

	public void RemoveMissionCounter()
	{
		RemoveComponent(74);
	}

	public void AddMissionData(MissionsBalancingData newMissionData)
	{
		int index = 75;
		MissionDataComponent missionDataComponent = CreateComponent<MissionDataComponent>(index);
		missionDataComponent.MissionData = newMissionData;
		AddComponent(index, missionDataComponent);
	}

	public void ReplaceMissionData(MissionsBalancingData newMissionData)
	{
		int index = 75;
		MissionDataComponent missionDataComponent = CreateComponent<MissionDataComponent>(index);
		missionDataComponent.MissionData = newMissionData;
		ReplaceComponent(index, missionDataComponent);
	}

	public void RemoveMissionData()
	{
		RemoveComponent(75);
	}

	public void AddMissionResult(int newResultValue, int newStars)
	{
		int index = 76;
		MissionResultComponent missionResultComponent = CreateComponent<MissionResultComponent>(index);
		missionResultComponent.ResultValue = newResultValue;
		missionResultComponent.Stars = newStars;
		AddComponent(index, missionResultComponent);
	}

	public void ReplaceMissionResult(int newResultValue, int newStars)
	{
		int index = 76;
		MissionResultComponent missionResultComponent = CreateComponent<MissionResultComponent>(index);
		missionResultComponent.ResultValue = newResultValue;
		missionResultComponent.Stars = newStars;
		ReplaceComponent(index, missionResultComponent);
	}

	public void RemoveMissionResult()
	{
		RemoveComponent(76);
	}

	public void AddMissionSprite(float newSize, Sprite newSprite)
	{
		int index = 77;
		MissionSpriteComponent missionSpriteComponent = CreateComponent<MissionSpriteComponent>(index);
		missionSpriteComponent.Size = newSize;
		missionSpriteComponent.Sprite = newSprite;
		AddComponent(index, missionSpriteComponent);
	}

	public void ReplaceMissionSprite(float newSize, Sprite newSprite)
	{
		int index = 77;
		MissionSpriteComponent missionSpriteComponent = CreateComponent<MissionSpriteComponent>(index);
		missionSpriteComponent.Size = newSize;
		missionSpriteComponent.Sprite = newSprite;
		ReplaceComponent(index, missionSpriteComponent);
	}

	public void RemoveMissionSprite()
	{
		RemoveComponent(77);
	}

	public void AddMissionSpriteFillAmount(float newFillAmount, float newFirstFillAmount)
	{
		int index = 78;
		MissionSpriteFillAmountComponent missionSpriteFillAmountComponent = CreateComponent<MissionSpriteFillAmountComponent>(index);
		missionSpriteFillAmountComponent.FillAmount = newFillAmount;
		missionSpriteFillAmountComponent.FirstFillAmount = newFirstFillAmount;
		AddComponent(index, missionSpriteFillAmountComponent);
	}

	public void ReplaceMissionSpriteFillAmount(float newFillAmount, float newFirstFillAmount)
	{
		int index = 78;
		MissionSpriteFillAmountComponent missionSpriteFillAmountComponent = CreateComponent<MissionSpriteFillAmountComponent>(index);
		missionSpriteFillAmountComponent.FillAmount = newFillAmount;
		missionSpriteFillAmountComponent.FirstFillAmount = newFirstFillAmount;
		ReplaceComponent(index, missionSpriteFillAmountComponent);
	}

	public void RemoveMissionSpriteFillAmount()
	{
		RemoveComponent(78);
	}

	public void AddMissionThresholds(int newFirstStar, int newSecondStar, int newThirdStar)
	{
		int index = 79;
		MissionThresholdsComponent missionThresholdsComponent = CreateComponent<MissionThresholdsComponent>(index);
		missionThresholdsComponent.FirstStar = newFirstStar;
		missionThresholdsComponent.SecondStar = newSecondStar;
		missionThresholdsComponent.ThirdStar = newThirdStar;
		AddComponent(index, missionThresholdsComponent);
	}

	public void ReplaceMissionThresholds(int newFirstStar, int newSecondStar, int newThirdStar)
	{
		int index = 79;
		MissionThresholdsComponent missionThresholdsComponent = CreateComponent<MissionThresholdsComponent>(index);
		missionThresholdsComponent.FirstStar = newFirstStar;
		missionThresholdsComponent.SecondStar = newSecondStar;
		missionThresholdsComponent.ThirdStar = newThirdStar;
		ReplaceComponent(index, missionThresholdsComponent);
	}

	public void RemoveMissionThresholds()
	{
		RemoveComponent(79);
	}

	public void AddMissionType(MissionType newType)
	{
		int index = 80;
		MissionTypeComponent missionTypeComponent = CreateComponent<MissionTypeComponent>(index);
		missionTypeComponent.Type = newType;
		AddComponent(index, missionTypeComponent);
	}

	public void ReplaceMissionType(MissionType newType)
	{
		int index = 80;
		MissionTypeComponent missionTypeComponent = CreateComponent<MissionTypeComponent>(index);
		missionTypeComponent.Type = newType;
		ReplaceComponent(index, missionTypeComponent);
	}

	public void RemoveMissionType()
	{
		RemoveComponent(80);
	}

	public void AddMissionUpdateListener(IMissionUpdateListener newListener)
	{
		int index = 81;
		MissionUpdateListenerComponent missionUpdateListenerComponent = CreateComponent<MissionUpdateListenerComponent>(index);
		missionUpdateListenerComponent.listener = newListener;
		AddComponent(index, missionUpdateListenerComponent);
	}

	public void ReplaceMissionUpdateListener(IMissionUpdateListener newListener)
	{
		int index = 81;
		MissionUpdateListenerComponent missionUpdateListenerComponent = CreateComponent<MissionUpdateListenerComponent>(index);
		missionUpdateListenerComponent.listener = newListener;
		ReplaceComponent(index, missionUpdateListenerComponent);
	}

	public void RemoveMissionUpdateListener()
	{
		RemoveComponent(81);
	}

	public void AddMovableData(float newStartThreshold, float newEndThreshold, Vector2 newMoveVector)
	{
		int index = 83;
		MovableDataComponent movableDataComponent = CreateComponent<MovableDataComponent>(index);
		movableDataComponent.StartThreshold = newStartThreshold;
		movableDataComponent.EndThreshold = newEndThreshold;
		movableDataComponent.MoveVector = newMoveVector;
		AddComponent(index, movableDataComponent);
	}

	public void ReplaceMovableData(float newStartThreshold, float newEndThreshold, Vector2 newMoveVector)
	{
		int index = 83;
		MovableDataComponent movableDataComponent = CreateComponent<MovableDataComponent>(index);
		movableDataComponent.StartThreshold = newStartThreshold;
		movableDataComponent.EndThreshold = newEndThreshold;
		movableDataComponent.MoveVector = newMoveVector;
		ReplaceComponent(index, movableDataComponent);
	}

	public void RemoveMovableData()
	{
		RemoveComponent(83);
	}

	public void AddNewWorldSkin(string newWorldSuffix, Sprite newBgSprite)
	{
		int index = 84;
		NewWorldSkinComponent newWorldSkinComponent = CreateComponent<NewWorldSkinComponent>(index);
		newWorldSkinComponent.WorldSuffix = newWorldSuffix;
		newWorldSkinComponent.BgSprite = newBgSprite;
		AddComponent(index, newWorldSkinComponent);
	}

	public void ReplaceNewWorldSkin(string newWorldSuffix, Sprite newBgSprite)
	{
		int index = 84;
		NewWorldSkinComponent newWorldSkinComponent = CreateComponent<NewWorldSkinComponent>(index);
		newWorldSkinComponent.WorldSuffix = newWorldSuffix;
		newWorldSkinComponent.BgSprite = newBgSprite;
		ReplaceComponent(index, newWorldSkinComponent);
	}

	public void RemoveNewWorldSkin()
	{
		RemoveComponent(84);
	}

	public void AddObstacleHitListener(IObstacleHitListener newListener)
	{
		int index = 86;
		ObstacleHitListenerComponent obstacleHitListenerComponent = CreateComponent<ObstacleHitListenerComponent>(index);
		obstacleHitListenerComponent.listener = newListener;
		AddComponent(index, obstacleHitListenerComponent);
	}

	public void ReplaceObstacleHitListener(IObstacleHitListener newListener)
	{
		int index = 86;
		ObstacleHitListenerComponent obstacleHitListenerComponent = CreateComponent<ObstacleHitListenerComponent>(index);
		obstacleHitListenerComponent.listener = newListener;
		ReplaceComponent(index, obstacleHitListenerComponent);
	}

	public void RemoveObstacleHitListener()
	{
		RemoveComponent(86);
	}

	public void AddOffsetPosition(Vector3 newPos)
	{
		int index = 87;
		OffsetPositionComponent offsetPositionComponent = CreateComponent<OffsetPositionComponent>(index);
		offsetPositionComponent.Pos = newPos;
		AddComponent(index, offsetPositionComponent);
	}

	public void ReplaceOffsetPosition(Vector3 newPos)
	{
		int index = 87;
		OffsetPositionComponent offsetPositionComponent = CreateComponent<OffsetPositionComponent>(index);
		offsetPositionComponent.Pos = newPos;
		ReplaceComponent(index, offsetPositionComponent);
	}

	public void RemoveOffsetPosition()
	{
		RemoveComponent(87);
	}

	public void AddOnClouds(float newDuration)
	{
		int index = 88;
		OnCloudsComponent onCloudsComponent = CreateComponent<OnCloudsComponent>(index);
		onCloudsComponent.Duration = newDuration;
		AddComponent(index, onCloudsComponent);
	}

	public void ReplaceOnClouds(float newDuration)
	{
		int index = 88;
		OnCloudsComponent onCloudsComponent = CreateComponent<OnCloudsComponent>(index);
		onCloudsComponent.Duration = newDuration;
		ReplaceComponent(index, onCloudsComponent);
	}

	public void RemoveOnClouds()
	{
		RemoveComponent(88);
	}

	public void AddPauseListener(IPauseListener newListener)
	{
		int index = 90;
		PauseListenerComponent pauseListenerComponent = CreateComponent<PauseListenerComponent>(index);
		pauseListenerComponent.listener = newListener;
		AddComponent(index, pauseListenerComponent);
	}

	public void ReplacePauseListener(IPauseListener newListener)
	{
		int index = 90;
		PauseListenerComponent pauseListenerComponent = CreateComponent<PauseListenerComponent>(index);
		pauseListenerComponent.listener = newListener;
		ReplaceComponent(index, pauseListenerComponent);
	}

	public void RemovePauseListener()
	{
		RemoveComponent(90);
	}

	public void AddPlayerBlinking(float newDuration, float newTimeLeft, int newNumberOfCycles)
	{
		int index = 93;
		PlayerBlinkingComponent playerBlinkingComponent = CreateComponent<PlayerBlinkingComponent>(index);
		playerBlinkingComponent.Duration = newDuration;
		playerBlinkingComponent.TimeLeft = newTimeLeft;
		playerBlinkingComponent.NumberOfCycles = newNumberOfCycles;
		AddComponent(index, playerBlinkingComponent);
	}

	public void ReplacePlayerBlinking(float newDuration, float newTimeLeft, int newNumberOfCycles)
	{
		int index = 93;
		PlayerBlinkingComponent playerBlinkingComponent = CreateComponent<PlayerBlinkingComponent>(index);
		playerBlinkingComponent.Duration = newDuration;
		playerBlinkingComponent.TimeLeft = newTimeLeft;
		playerBlinkingComponent.NumberOfCycles = newNumberOfCycles;
		ReplaceComponent(index, playerBlinkingComponent);
	}

	public void RemovePlayerBlinking()
	{
		RemoveComponent(93);
	}

	public void AddPlayerLandListener(IPlayerLandListener newListener)
	{
		int index = 95;
		PlayerLandListenerComponent playerLandListenerComponent = CreateComponent<PlayerLandListenerComponent>(index);
		playerLandListenerComponent.listener = newListener;
		AddComponent(index, playerLandListenerComponent);
	}

	public void ReplacePlayerLandListener(IPlayerLandListener newListener)
	{
		int index = 95;
		PlayerLandListenerComponent playerLandListenerComponent = CreateComponent<PlayerLandListenerComponent>(index);
		playerLandListenerComponent.listener = newListener;
		ReplaceComponent(index, playerLandListenerComponent);
	}

	public void RemovePlayerLandListener()
	{
		RemoveComponent(95);
	}

	public void AddPlayerResurrect(float newDelay)
	{
		int index = 97;
		PlayerResurrectComponent playerResurrectComponent = CreateComponent<PlayerResurrectComponent>(index);
		playerResurrectComponent.Delay = newDelay;
		AddComponent(index, playerResurrectComponent);
	}

	public void ReplacePlayerResurrect(float newDelay)
	{
		int index = 97;
		PlayerResurrectComponent playerResurrectComponent = CreateComponent<PlayerResurrectComponent>(index);
		playerResurrectComponent.Delay = newDelay;
		ReplaceComponent(index, playerResurrectComponent);
	}

	public void RemovePlayerResurrect()
	{
		RemoveComponent(97);
	}

	public void AddPlaySoundsListener(IPlaySoundListener newListener)
	{
		int index = 102;
		PlaySoundsListenerComponent playSoundsListenerComponent = CreateComponent<PlaySoundsListenerComponent>(index);
		playSoundsListenerComponent.listener = newListener;
		AddComponent(index, playSoundsListenerComponent);
	}

	public void ReplacePlaySoundsListener(IPlaySoundListener newListener)
	{
		int index = 102;
		PlaySoundsListenerComponent playSoundsListenerComponent = CreateComponent<PlaySoundsListenerComponent>(index);
		playSoundsListenerComponent.listener = newListener;
		ReplaceComponent(index, playSoundsListenerComponent);
	}

	public void RemovePlaySoundsListener()
	{
		RemoveComponent(102);
	}

	public void AddPoolContainer(Dictionary<string, Stack<GameObject>> newPools)
	{
		int index = 104;
		PoolContainerComponent poolContainerComponent = CreateComponent<PoolContainerComponent>(index);
		poolContainerComponent.Pools = newPools;
		AddComponent(index, poolContainerComponent);
	}

	public void ReplacePoolContainer(Dictionary<string, Stack<GameObject>> newPools)
	{
		int index = 104;
		PoolContainerComponent poolContainerComponent = CreateComponent<PoolContainerComponent>(index);
		poolContainerComponent.Pools = newPools;
		ReplaceComponent(index, poolContainerComponent);
	}

	public void RemovePoolContainer()
	{
		RemoveComponent(104);
	}

	public void AddPoolDespawned(IPoolDespawnedListener newListener)
	{
		int index = 105;
		PoolDespawnedComponent poolDespawnedComponent = CreateComponent<PoolDespawnedComponent>(index);
		poolDespawnedComponent.listener = newListener;
		AddComponent(index, poolDespawnedComponent);
	}

	public void ReplacePoolDespawned(IPoolDespawnedListener newListener)
	{
		int index = 105;
		PoolDespawnedComponent poolDespawnedComponent = CreateComponent<PoolDespawnedComponent>(index);
		poolDespawnedComponent.listener = newListener;
		ReplaceComponent(index, poolDespawnedComponent);
	}

	public void RemovePoolDespawned()
	{
		RemoveComponent(105);
	}

	public void AddPoolSpawned(IPoolSpawnedListener newListener)
	{
		int index = 106;
		PoolSpawnedComponent poolSpawnedComponent = CreateComponent<PoolSpawnedComponent>(index);
		poolSpawnedComponent.listener = newListener;
		AddComponent(index, poolSpawnedComponent);
	}

	public void ReplacePoolSpawned(IPoolSpawnedListener newListener)
	{
		int index = 106;
		PoolSpawnedComponent poolSpawnedComponent = CreateComponent<PoolSpawnedComponent>(index);
		poolSpawnedComponent.listener = newListener;
		ReplaceComponent(index, poolSpawnedComponent);
	}

	public void RemovePoolSpawned()
	{
		RemoveComponent(106);
	}

	public void AddPosition(Vector3 newValue)
	{
		int index = 107;
		PositionComponent positionComponent = CreateComponent<PositionComponent>(index);
		positionComponent.Value = newValue;
		AddComponent(index, positionComponent);
	}

	public void ReplacePosition(Vector3 newValue)
	{
		int index = 107;
		PositionComponent positionComponent = CreateComponent<PositionComponent>(index);
		positionComponent.Value = newValue;
		ReplaceComponent(index, positionComponent);
	}

	public void RemovePosition()
	{
		RemoveComponent(107);
	}

	public void AddRaccoonIntro(RaccoonIntroStep newStep)
	{
		int index = 110;
		RaccoonIntroComponent raccoonIntroComponent = CreateComponent<RaccoonIntroComponent>(index);
		raccoonIntroComponent.Step = newStep;
		AddComponent(index, raccoonIntroComponent);
	}

	public void ReplaceRaccoonIntro(RaccoonIntroStep newStep)
	{
		int index = 110;
		RaccoonIntroComponent raccoonIntroComponent = CreateComponent<RaccoonIntroComponent>(index);
		raccoonIntroComponent.Step = newStep;
		ReplaceComponent(index, raccoonIntroComponent);
	}

	public void RemoveRaccoonIntro()
	{
		RemoveComponent(110);
	}

	public void AddRaccoonIntrosListener(IRaccoonIntroListener newListener)
	{
		int index = 111;
		RaccoonIntrosListenerComponent raccoonIntrosListenerComponent = CreateComponent<RaccoonIntrosListenerComponent>(index);
		raccoonIntrosListenerComponent.listener = newListener;
		AddComponent(index, raccoonIntrosListenerComponent);
	}

	public void ReplaceRaccoonIntrosListener(IRaccoonIntroListener newListener)
	{
		int index = 111;
		RaccoonIntrosListenerComponent raccoonIntrosListenerComponent = CreateComponent<RaccoonIntrosListenerComponent>(index);
		raccoonIntrosListenerComponent.listener = newListener;
		ReplaceComponent(index, raccoonIntrosListenerComponent);
	}

	public void RemoveRaccoonIntrosListener()
	{
		RemoveComponent(111);
	}

	public void AddRestableArea(Vector2 newMoveVector, bool newMovingFinished)
	{
		int index = 115;
		RestableAreaComponent restableAreaComponent = CreateComponent<RestableAreaComponent>(index);
		restableAreaComponent.MoveVector = newMoveVector;
		restableAreaComponent.MovingFinished = newMovingFinished;
		AddComponent(index, restableAreaComponent);
	}

	public void ReplaceRestableArea(Vector2 newMoveVector, bool newMovingFinished)
	{
		int index = 115;
		RestableAreaComponent restableAreaComponent = CreateComponent<RestableAreaComponent>(index);
		restableAreaComponent.MoveVector = newMoveVector;
		restableAreaComponent.MovingFinished = newMovingFinished;
		ReplaceComponent(index, restableAreaComponent);
	}

	public void RemoveRestableArea()
	{
		RemoveComponent(115);
	}

	public void AddResting(IEntity newRestableArea)
	{
		int index = 116;
		RestingComponent restingComponent = CreateComponent<RestingComponent>(index);
		restingComponent.restableArea = newRestableArea;
		AddComponent(index, restingComponent);
	}

	public void ReplaceResting(IEntity newRestableArea)
	{
		int index = 116;
		RestingComponent restingComponent = CreateComponent<RestingComponent>(index);
		restingComponent.restableArea = newRestableArea;
		ReplaceComponent(index, restingComponent);
	}

	public void RemoveResting()
	{
		RemoveComponent(116);
	}

	public void AddResurectionsDone(int newAmount)
	{
		int index = 117;
		ResurectionsDoneComponent resurectionsDoneComponent = CreateComponent<ResurectionsDoneComponent>(index);
		resurectionsDoneComponent.Amount = newAmount;
		AddComponent(index, resurectionsDoneComponent);
	}

	public void ReplaceResurectionsDone(int newAmount)
	{
		int index = 117;
		ResurectionsDoneComponent resurectionsDoneComponent = CreateComponent<ResurectionsDoneComponent>(index);
		resurectionsDoneComponent.Amount = newAmount;
		ReplaceComponent(index, resurectionsDoneComponent);
	}

	public void RemoveResurectionsDone()
	{
		RemoveComponent(117);
	}

	public void AddRevivesDone(int newAmount)
	{
		int index = 119;
		RevivesDoneComponent revivesDoneComponent = CreateComponent<RevivesDoneComponent>(index);
		revivesDoneComponent.Amount = newAmount;
		AddComponent(index, revivesDoneComponent);
	}

	public void ReplaceRevivesDone(int newAmount)
	{
		int index = 119;
		RevivesDoneComponent revivesDoneComponent = CreateComponent<RevivesDoneComponent>(index);
		revivesDoneComponent.Amount = newAmount;
		ReplaceComponent(index, revivesDoneComponent);
	}

	public void RemoveRevivesDone()
	{
		RemoveComponent(119);
	}

	public void AddRunDuration(float newDuration_MS)
	{
		int index = 120;
		RunDurationComponent runDurationComponent = CreateComponent<RunDurationComponent>(index);
		runDurationComponent.Duration_MS = newDuration_MS;
		AddComponent(index, runDurationComponent);
	}

	public void ReplaceRunDuration(float newDuration_MS)
	{
		int index = 120;
		RunDurationComponent runDurationComponent = CreateComponent<RunDurationComponent>(index);
		runDurationComponent.Duration_MS = newDuration_MS;
		ReplaceComponent(index, runDurationComponent);
	}

	public void RemoveRunDuration()
	{
		RemoveComponent(120);
	}

	public void AddScore(int newValue)
	{
		int index = 121;
		ScoreComponent scoreComponent = CreateComponent<ScoreComponent>(index);
		scoreComponent.Value = newValue;
		AddComponent(index, scoreComponent);
	}

	public void ReplaceScore(int newValue)
	{
		int index = 121;
		ScoreComponent scoreComponent = CreateComponent<ScoreComponent>(index);
		scoreComponent.Value = newValue;
		ReplaceComponent(index, scoreComponent);
	}

	public void RemoveScore()
	{
		RemoveComponent(121);
	}

	public void AddScoreListener(IScoreListener newListener)
	{
		int index = 122;
		ScoreListenerComponent scoreListenerComponent = CreateComponent<ScoreListenerComponent>(index);
		scoreListenerComponent.listener = newListener;
		AddComponent(index, scoreListenerComponent);
	}

	public void ReplaceScoreListener(IScoreListener newListener)
	{
		int index = 122;
		ScoreListenerComponent scoreListenerComponent = CreateComponent<ScoreListenerComponent>(index);
		scoreListenerComponent.listener = newListener;
		ReplaceComponent(index, scoreListenerComponent);
	}

	public void RemoveScoreListener()
	{
		RemoveComponent(122);
	}

	public void AddScoreMultiplier(float newDuration, float newFactor)
	{
		int index = 123;
		ScoreMultiplierComponent scoreMultiplierComponent = CreateComponent<ScoreMultiplierComponent>(index);
		scoreMultiplierComponent.Duration = newDuration;
		scoreMultiplierComponent.Factor = newFactor;
		AddComponent(index, scoreMultiplierComponent);
	}

	public void ReplaceScoreMultiplier(float newDuration, float newFactor)
	{
		int index = 123;
		ScoreMultiplierComponent scoreMultiplierComponent = CreateComponent<ScoreMultiplierComponent>(index);
		scoreMultiplierComponent.Duration = newDuration;
		scoreMultiplierComponent.Factor = newFactor;
		ReplaceComponent(index, scoreMultiplierComponent);
	}

	public void RemoveScoreMultiplier()
	{
		RemoveComponent(123);
	}

	public void AddSelectedCharacter(int newCharId)
	{
		int index = 125;
		SelectedCharacterComponent selectedCharacterComponent = CreateComponent<SelectedCharacterComponent>(index);
		selectedCharacterComponent.charId = newCharId;
		AddComponent(index, selectedCharacterComponent);
	}

	public void ReplaceSelectedCharacter(int newCharId)
	{
		int index = 125;
		SelectedCharacterComponent selectedCharacterComponent = CreateComponent<SelectedCharacterComponent>(index);
		selectedCharacterComponent.charId = newCharId;
		ReplaceComponent(index, selectedCharacterComponent);
	}

	public void RemoveSelectedCharacter()
	{
		RemoveComponent(125);
	}

	public void AddSetupAbility(int newCharId)
	{
		int index = 126;
		SetupAbility setupAbility = CreateComponent<SetupAbility>(index);
		setupAbility.CharId = newCharId;
		AddComponent(index, setupAbility);
	}

	public void ReplaceSetupAbility(int newCharId)
	{
		int index = 126;
		SetupAbility setupAbility = CreateComponent<SetupAbility>(index);
		setupAbility.CharId = newCharId;
		ReplaceComponent(index, setupAbility);
	}

	public void RemoveSetupAbility()
	{
		RemoveComponent(126);
	}

	public void AddShaking(float newInterval, float newStrength)
	{
		int index = 127;
		ShakingComponent shakingComponent = CreateComponent<ShakingComponent>(index);
		shakingComponent.Interval = newInterval;
		shakingComponent.Strength = newStrength;
		AddComponent(index, shakingComponent);
	}

	public void ReplaceShaking(float newInterval, float newStrength)
	{
		int index = 127;
		ShakingComponent shakingComponent = CreateComponent<ShakingComponent>(index);
		shakingComponent.Interval = newInterval;
		shakingComponent.Strength = newStrength;
		ReplaceComponent(index, shakingComponent);
	}

	public void RemoveShaking()
	{
		RemoveComponent(127);
	}

	public void AddSizeIncreased(float newDuration)
	{
		int index = 128;
		SizeIncreasedComponent sizeIncreasedComponent = CreateComponent<SizeIncreasedComponent>(index);
		sizeIncreasedComponent.Duration = newDuration;
		AddComponent(index, sizeIncreasedComponent);
	}

	public void ReplaceSizeIncreased(float newDuration)
	{
		int index = 128;
		SizeIncreasedComponent sizeIncreasedComponent = CreateComponent<SizeIncreasedComponent>(index);
		sizeIncreasedComponent.Duration = newDuration;
		ReplaceComponent(index, sizeIncreasedComponent);
	}

	public void RemoveSizeIncreased()
	{
		RemoveComponent(128);
	}

	public void AddSparkle(string newAssetName)
	{
		int index = 130;
		SparkleComponent sparkleComponent = CreateComponent<SparkleComponent>(index);
		sparkleComponent.AssetName = newAssetName;
		AddComponent(index, sparkleComponent);
	}

	public void ReplaceSparkle(string newAssetName)
	{
		int index = 130;
		SparkleComponent sparkleComponent = CreateComponent<SparkleComponent>(index);
		sparkleComponent.AssetName = newAssetName;
		ReplaceComponent(index, sparkleComponent);
	}

	public void RemoveSparkle()
	{
		RemoveComponent(130);
	}

	public void AddSpeedData(float newSpeed)
	{
		int index = 132;
		SpeedDataComponent speedDataComponent = CreateComponent<SpeedDataComponent>(index);
		speedDataComponent.Speed = newSpeed;
		AddComponent(index, speedDataComponent);
	}

	public void ReplaceSpeedData(float newSpeed)
	{
		int index = 132;
		SpeedDataComponent speedDataComponent = CreateComponent<SpeedDataComponent>(index);
		speedDataComponent.Speed = newSpeed;
		ReplaceComponent(index, speedDataComponent);
	}

	public void RemoveSpeedData()
	{
		RemoveComponent(132);
	}

	public void AddSpeedUp(float newDuration, float newFactor)
	{
		int index = 133;
		SpeedUpComponent speedUpComponent = CreateComponent<SpeedUpComponent>(index);
		speedUpComponent.Duration = newDuration;
		speedUpComponent.Factor = newFactor;
		AddComponent(index, speedUpComponent);
	}

	public void ReplaceSpeedUp(float newDuration, float newFactor)
	{
		int index = 133;
		SpeedUpComponent speedUpComponent = CreateComponent<SpeedUpComponent>(index);
		speedUpComponent.Duration = newDuration;
		speedUpComponent.Factor = newFactor;
		ReplaceComponent(index, speedUpComponent);
	}

	public void RemoveSpeedUp()
	{
		RemoveComponent(133);
	}

	public void AddStamina(float newValue)
	{
		int index = 134;
		StaminaComponent staminaComponent = CreateComponent<StaminaComponent>(index);
		staminaComponent.Value = newValue;
		AddComponent(index, staminaComponent);
	}

	public void ReplaceStamina(float newValue)
	{
		int index = 134;
		StaminaComponent staminaComponent = CreateComponent<StaminaComponent>(index);
		staminaComponent.Value = newValue;
		ReplaceComponent(index, staminaComponent);
	}

	public void RemoveStamina()
	{
		RemoveComponent(134);
	}

	public void AddStaminasListener(IStaminaListener newListener)
	{
		int index = 135;
		StaminasListenerComponent staminasListenerComponent = CreateComponent<StaminasListenerComponent>(index);
		staminasListenerComponent.listener = newListener;
		AddComponent(index, staminasListenerComponent);
	}

	public void ReplaceStaminasListener(IStaminaListener newListener)
	{
		int index = 135;
		StaminasListenerComponent staminasListenerComponent = CreateComponent<StaminasListenerComponent>(index);
		staminasListenerComponent.listener = newListener;
		ReplaceComponent(index, staminasListenerComponent);
	}

	public void RemoveStaminasListener()
	{
		RemoveComponent(135);
	}

	public void AddSwingingData(Vector2 newAnchor, Vector2 newStartEndRotation, float newStartThreshold, float newEndThreshold, float newAverageSpeed)
	{
		int index = 136;
		SwingingDataComponent swingingDataComponent = CreateComponent<SwingingDataComponent>(index);
		swingingDataComponent.Anchor = newAnchor;
		swingingDataComponent.StartEndRotation = newStartEndRotation;
		swingingDataComponent.StartThreshold = newStartThreshold;
		swingingDataComponent.EndThreshold = newEndThreshold;
		swingingDataComponent.AverageSpeed = newAverageSpeed;
		AddComponent(index, swingingDataComponent);
	}

	public void ReplaceSwingingData(Vector2 newAnchor, Vector2 newStartEndRotation, float newStartThreshold, float newEndThreshold, float newAverageSpeed)
	{
		int index = 136;
		SwingingDataComponent swingingDataComponent = CreateComponent<SwingingDataComponent>(index);
		swingingDataComponent.Anchor = newAnchor;
		swingingDataComponent.StartEndRotation = newStartEndRotation;
		swingingDataComponent.StartThreshold = newStartThreshold;
		swingingDataComponent.EndThreshold = newEndThreshold;
		swingingDataComponent.AverageSpeed = newAverageSpeed;
		ReplaceComponent(index, swingingDataComponent);
	}

	public void RemoveSwingingData()
	{
		RemoveComponent(136);
	}

	public void AddTutorialPerformed(TutorialStep newStep)
	{
		int index = 137;
		TutorialPerformedComponent tutorialPerformedComponent = CreateComponent<TutorialPerformedComponent>(index);
		tutorialPerformedComponent.Step = newStep;
		AddComponent(index, tutorialPerformedComponent);
	}

	public void ReplaceTutorialPerformed(TutorialStep newStep)
	{
		int index = 137;
		TutorialPerformedComponent tutorialPerformedComponent = CreateComponent<TutorialPerformedComponent>(index);
		tutorialPerformedComponent.Step = newStep;
		ReplaceComponent(index, tutorialPerformedComponent);
	}

	public void RemoveTutorialPerformed()
	{
		RemoveComponent(137);
	}

	public void AddTutorialPlayStep(TutorialStep newStep, bool newStopTime, float newStartPosX, float newEndPosX)
	{
		int index = 138;
		TutorialPlayStepComponent tutorialPlayStepComponent = CreateComponent<TutorialPlayStepComponent>(index);
		tutorialPlayStepComponent.Step = newStep;
		tutorialPlayStepComponent.StopTime = newStopTime;
		tutorialPlayStepComponent.StartPosX = newStartPosX;
		tutorialPlayStepComponent.EndPosX = newEndPosX;
		AddComponent(index, tutorialPlayStepComponent);
	}

	public void ReplaceTutorialPlayStep(TutorialStep newStep, bool newStopTime, float newStartPosX, float newEndPosX)
	{
		int index = 138;
		TutorialPlayStepComponent tutorialPlayStepComponent = CreateComponent<TutorialPlayStepComponent>(index);
		tutorialPlayStepComponent.Step = newStep;
		tutorialPlayStepComponent.StopTime = newStopTime;
		tutorialPlayStepComponent.StartPosX = newStartPosX;
		tutorialPlayStepComponent.EndPosX = newEndPosX;
		ReplaceComponent(index, tutorialPlayStepComponent);
	}

	public void RemoveTutorialPlayStep()
	{
		RemoveComponent(138);
	}

	public void AddTutorialsListener(ITutorialListener newListener)
	{
		int index = 139;
		TutorialsListenerComponent tutorialsListenerComponent = CreateComponent<TutorialsListenerComponent>(index);
		tutorialsListenerComponent.listener = newListener;
		AddComponent(index, tutorialsListenerComponent);
	}

	public void ReplaceTutorialsListener(ITutorialListener newListener)
	{
		int index = 139;
		TutorialsListenerComponent tutorialsListenerComponent = CreateComponent<TutorialsListenerComponent>(index);
		tutorialsListenerComponent.listener = newListener;
		ReplaceComponent(index, tutorialsListenerComponent);
	}

	public void RemoveTutorialsListener()
	{
		RemoveComponent(139);
	}

	public void AddTutorialTrigger(TutorialStep newStep, bool newStopTime)
	{
		int index = 140;
		TutorialTriggerComponent tutorialTriggerComponent = CreateComponent<TutorialTriggerComponent>(index);
		tutorialTriggerComponent.Step = newStep;
		tutorialTriggerComponent.StopTime = newStopTime;
		AddComponent(index, tutorialTriggerComponent);
	}

	public void ReplaceTutorialTrigger(TutorialStep newStep, bool newStopTime)
	{
		int index = 140;
		TutorialTriggerComponent tutorialTriggerComponent = CreateComponent<TutorialTriggerComponent>(index);
		tutorialTriggerComponent.Step = newStep;
		tutorialTriggerComponent.StopTime = newStopTime;
		ReplaceComponent(index, tutorialTriggerComponent);
	}

	public void RemoveTutorialTrigger()
	{
		RemoveComponent(140);
	}

	public void AddUpgradeScore(float newDuration, float newDistance)
	{
		int index = 142;
		UpgradeScoreComponent upgradeScoreComponent = CreateComponent<UpgradeScoreComponent>(index);
		upgradeScoreComponent.Duration = newDuration;
		upgradeScoreComponent.Distance = newDistance;
		AddComponent(index, upgradeScoreComponent);
	}

	public void ReplaceUpgradeScore(float newDuration, float newDistance)
	{
		int index = 142;
		UpgradeScoreComponent upgradeScoreComponent = CreateComponent<UpgradeScoreComponent>(index);
		upgradeScoreComponent.Duration = newDuration;
		upgradeScoreComponent.Distance = newDistance;
		ReplaceComponent(index, upgradeScoreComponent);
	}

	public void RemoveUpgradeScore()
	{
		RemoveComponent(142);
	}

	public void AddUpgradeSingleScore(float newDuration, float newDistance)
	{
		int index = 143;
		UpgradeSingleScoreComponent upgradeSingleScoreComponent = CreateComponent<UpgradeSingleScoreComponent>(index);
		upgradeSingleScoreComponent.Duration = newDuration;
		upgradeSingleScoreComponent.Distance = newDistance;
		AddComponent(index, upgradeSingleScoreComponent);
	}

	public void ReplaceUpgradeSingleScore(float newDuration, float newDistance)
	{
		int index = 143;
		UpgradeSingleScoreComponent upgradeSingleScoreComponent = CreateComponent<UpgradeSingleScoreComponent>(index);
		upgradeSingleScoreComponent.Duration = newDuration;
		upgradeSingleScoreComponent.Distance = newDistance;
		ReplaceComponent(index, upgradeSingleScoreComponent);
	}

	public void RemoveUpgradeSingleScore()
	{
		RemoveComponent(143);
	}

	public void AddView(GameObject newGameObject)
	{
		int index = 144;
		ViewComponent viewComponent = CreateComponent<ViewComponent>(index);
		viewComponent.gameObject = newGameObject;
		AddComponent(index, viewComponent);
	}

	public void ReplaceView(GameObject newGameObject)
	{
		int index = 144;
		ViewComponent viewComponent = CreateComponent<ViewComponent>(index);
		viewComponent.gameObject = newGameObject;
		ReplaceComponent(index, viewComponent);
	}

	public void RemoveView()
	{
		RemoveComponent(144);
	}

	public void AddWaitToStart(float newDuration)
	{
		int index = 145;
		WaitToStartComponent waitToStartComponent = CreateComponent<WaitToStartComponent>(index);
		waitToStartComponent.Duration = newDuration;
		AddComponent(index, waitToStartComponent);
	}

	public void ReplaceWaitToStart(float newDuration)
	{
		int index = 145;
		WaitToStartComponent waitToStartComponent = CreateComponent<WaitToStartComponent>(index);
		waitToStartComponent.Duration = newDuration;
		ReplaceComponent(index, waitToStartComponent);
	}

	public void RemoveWaitToStart()
	{
		RemoveComponent(145);
	}

	public void AddWorld(float newDistance, float newSpeed, List<LevelElementData> newLevelElements)
	{
		int index = 146;
		WorldComponent worldComponent = CreateComponent<WorldComponent>(index);
		worldComponent.Distance = newDistance;
		worldComponent.Speed = newSpeed;
		worldComponent.levelElements = newLevelElements;
		AddComponent(index, worldComponent);
	}

	public void ReplaceWorld(float newDistance, float newSpeed, List<LevelElementData> newLevelElements)
	{
		int index = 146;
		WorldComponent worldComponent = CreateComponent<WorldComponent>(index);
		worldComponent.Distance = newDistance;
		worldComponent.Speed = newSpeed;
		worldComponent.levelElements = newLevelElements;
		ReplaceComponent(index, worldComponent);
	}

	public void RemoveWorld()
	{
		RemoveComponent(146);
	}

	public void AddWorldTimeScale(float newScale)
	{
		int index = 147;
		WorldTimeScaleComponent worldTimeScaleComponent = CreateComponent<WorldTimeScaleComponent>(index);
		worldTimeScaleComponent.scale = newScale;
		AddComponent(index, worldTimeScaleComponent);
	}

	public void ReplaceWorldTimeScale(float newScale)
	{
		int index = 147;
		WorldTimeScaleComponent worldTimeScaleComponent = CreateComponent<WorldTimeScaleComponent>(index);
		worldTimeScaleComponent.scale = newScale;
		ReplaceComponent(index, worldTimeScaleComponent);
	}

	public void RemoveWorldTimeScale()
	{
		RemoveComponent(147);
	}
}
