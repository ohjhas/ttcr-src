using System;

public static class GameComponentsLookup
{
	public const int AbilityChangedListener = 0;

	public const int AbilityChargingProgress = 1;

	public const int AbilityChargingTime = 2;

	public const int AbilityCreateToken = 3;

	public const int AbilityDragonBlast = 4;

	public const int AbilityDuration = 5;

	public const int AbilityFasterMovement = 6;

	public const int AbilityGiantSpeedup = 7;

	public const int AbilityMagnetized = 8;

	public const int AbilityRandomPowerup = 9;

	public const int AbilityResurrect = 10;

	public const int AbilityRun = 11;

	public const int AbilityScoreBonus = 12;

	public const int AbilityShield = 13;

	public const int AbilitySpeedup = 14;

	public const int AbilityStateChangedListener = 15;

	public const int AbilityTimer = 16;

	public const int AbilityUpgradeScore = 17;

	public const int AngryRun = 18;

	public const int Asset = 19;

	public const int AsyncRequest = 20;

	public const int CameraAttached = 21;

	public const int CameraStop = 22;

	public const int CandyStore = 23;

	public const int ChangeCameraSizeTrigger = 24;

	public const int ChangeScaleTime = 25;

	public const int ChangeWorldSkin = 26;

	public const int ChangeWorldSpeed = 27;

	public const int Cloud = 28;

	public const int Coins = 29;

	public const int CoinsListener = 30;

	public const int Collected = 31;

	public const int CollectibleCoin = 32;

	public const int Collectible = 33;

	public const int CollectibleData = 34;

	public const int CollectibleGiantToken = 35;

	public const int CollectibleMagnet = 36;

	public const int CollectibleScore = 37;

	public const int CollectibleScoreMultiplier = 38;

	public const int CollectibleSpeedUp = 39;

	public const int CollectibleStamina = 40;

	public const int CollectibleUpgradeScore = 41;

	public const int CollectSound = 42;

	public const int Companion = 43;

	public const int DailyMissionCollect = 44;

	public const int DailyMissionDestroyObstacle = 45;

	public const int DailyMissionPlayMissions = 46;

	public const int Dead = 47;

	public const int Destroyed = 48;

	public const int DestroyZoned = 49;

	public const int DisableColliders = 50;

	public const int Dropping = 51;

	public const int FadeOut = 52;

	public const int Floating = 53;

	public const int FlyAway = 54;

	public const int GainedScore = 55;

	public const int GameStarted = 56;

	public const int GodMode = 57;

	public const int Grounded = 58;

	public const int ImprovedScoreToken = 59;

	public const int IngameBg = 60;

	public const int InitialTimeScale = 61;

	public const int InitialTrigger = 62;

	public const int Invulnerability = 63;

	public const int Jumping = 64;

	public const int KillZone = 65;

	public const int LevelEnd = 66;

	public const int LevelFinished = 67;

	public const int LevelFinishedListener = 68;

	public const int LevelSettings = 69;

	public const int LoadAssetAsync = 70;

	public const int Magnetized = 71;

	public const int MissionChestGranted = 72;

	public const int MissionCollect = 73;

	public const int MissionCounter = 74;

	public const int MissionData = 75;

	public const int MissionResult = 76;

	public const int MissionSprite = 77;

	public const int MissionSpriteFillAmount = 78;

	public const int MissionThresholds = 79;

	public const int MissionType = 80;

	public const int MissionUpdateListener = 81;

	public const int Movable = 82;

	public const int MovableData = 83;

	public const int NewWorldSkin = 84;

	public const int Obstacle = 85;

	public const int ObstacleHitListener = 86;

	public const int OffsetPosition = 87;

	public const int OnClouds = 88;

	public const int Paused = 89;

	public const int PauseListener = 90;

	public const int Platform = 91;

	public const int PlayerAttached = 92;

	public const int PlayerBlinking = 93;

	public const int Player = 94;

	public const int PlayerLandListener = 95;

	public const int PlayerRelay = 96;

	public const int PlayerResurrect = 97;

	public const int PlayerRevive = 98;

	public const int PlayGameFailMusic = 99;

	public const int PlayGameMusic = 100;

	public const int PlayGameWinMusic = 101;

	public const int PlaySoundsListener = 102;

	public const int Poolable = 103;

	public const int PoolContainer = 104;

	public const int PoolDespawned = 105;

	public const int PoolSpawned = 106;

	public const int Position = 107;

	public const int Raccoon = 108;

	public const int RaccoonDropAnimation = 109;

	public const int RaccoonIntro = 110;

	public const int RaccoonIntrosListener = 111;

	public const int RaccoonOutro = 112;

	public const int ReplayLevel = 113;

	public const int ResetPoolable = 114;

	public const int RestableArea = 115;

	public const int Resting = 116;

	public const int ResurectionsDone = 117;

	public const int ReturnToHomeLevel = 118;

	public const int RevivesDone = 119;

	public const int RunDuration = 120;

	public const int Score = 121;

	public const int ScoreListener = 122;

	public const int ScoreMultiplier = 123;

	public const int ScoreToken = 124;

	public const int SelectedCharacter = 125;

	public const int SetupAbility = 126;

	public const int Shaking = 127;

	public const int SizeIncreased = 128;

	public const int Sliding = 129;

	public const int Sparkle = 130;

	public const int SpecialRun = 131;

	public const int SpeedData = 132;

	public const int SpeedUp = 133;

	public const int Stamina = 134;

	public const int StaminasListener = 135;

	public const int SwingingData = 136;

	public const int TutorialPerformed = 137;

	public const int TutorialPlayStep = 138;

	public const int TutorialsListener = 139;

	public const int TutorialTrigger = 140;

	public const int UnderTheScreen = 141;

	public const int UpgradeScore = 142;

	public const int UpgradeSingleScore = 143;

	public const int View = 144;

	public const int WaitToStart = 145;

	public const int World = 146;

	public const int WorldTimeScale = 147;

	public const int TotalComponents = 148;

	public static readonly string[] componentNames = new string[148]
	{
		"AbilityChangedListener", "AbilityChargingProgress", "AbilityChargingTime", "AbilityCreateToken", "AbilityDragonBlast", "AbilityDuration", "AbilityFasterMovement", "AbilityGiantSpeedup", "AbilityMagnetized", "AbilityRandomPowerup",
		"AbilityResurrect", "AbilityRun", "AbilityScoreBonus", "AbilityShield", "AbilitySpeedup", "AbilityStateChangedListener", "AbilityTimer", "AbilityUpgradeScore", "AngryRun", "Asset",
		"AsyncRequest", "CameraAttached", "CameraStop", "CandyStore", "ChangeCameraSizeTrigger", "ChangeScaleTime", "ChangeWorldSkin", "ChangeWorldSpeed", "Cloud", "Coins",
		"CoinsListener", "Collected", "CollectibleCoin", "Collectible", "CollectibleData", "CollectibleGiantToken", "CollectibleMagnet", "CollectibleScore", "CollectibleScoreMultiplier", "CollectibleSpeedUp",
		"CollectibleStamina", "CollectibleUpgradeScore", "CollectSound", "Companion", "DailyMissionCollect", "DailyMissionDestroyObstacle", "DailyMissionPlayMissions", "Dead", "Destroyed", "DestroyZoned",
		"DisableColliders", "Dropping", "FadeOut", "Floating", "FlyAway", "GainedScore", "GameStarted", "GodMode", "Grounded", "ImprovedScoreToken",
		"IngameBg", "InitialTimeScale", "InitialTrigger", "Invulnerability", "Jumping", "KillZone", "LevelEnd", "LevelFinished", "LevelFinishedListener", "LevelSettings",
		"LoadAssetAsync", "Magnetized", "MissionChestGranted", "MissionCollect", "MissionCounter", "MissionData", "MissionResult", "MissionSprite", "MissionSpriteFillAmount", "MissionThresholds",
		"MissionType", "MissionUpdateListener", "Movable", "MovableData", "NewWorldSkin", "Obstacle", "ObstacleHitListener", "OffsetPosition", "OnClouds", "Paused",
		"PauseListener", "Platform", "PlayerAttached", "PlayerBlinking", "Player", "PlayerLandListener", "PlayerRelay", "PlayerResurrect", "PlayerRevive", "PlayGameFailMusic",
		"PlayGameMusic", "PlayGameWinMusic", "PlaySoundsListener", "Poolable", "PoolContainer", "PoolDespawned", "PoolSpawned", "Position", "Raccoon", "RaccoonDropAnimation",
		"RaccoonIntro", "RaccoonIntrosListener", "RaccoonOutro", "ReplayLevel", "ResetPoolable", "RestableArea", "Resting", "ResurectionsDone", "ReturnToHomeLevel", "RevivesDone",
		"RunDuration", "Score", "ScoreListener", "ScoreMultiplier", "ScoreToken", "SelectedCharacter", "SetupAbility", "Shaking", "SizeIncreased", "Sliding",
		"Sparkle", "SpecialRun", "SpeedData", "SpeedUp", "Stamina", "StaminasListener", "SwingingData", "TutorialPerformed", "TutorialPlayStep", "TutorialsListener",
		"TutorialTrigger", "UnderTheScreen", "UpgradeScore", "UpgradeSingleScore", "View", "WaitToStart", "World", "WorldTimeScale"
	};

	public static readonly Type[] componentTypes = new Type[148]
	{
		typeof(AbilityChangedListenerComponent),
		typeof(AbilityChargingProgress),
		typeof(AbilityChargingTimeComponent),
		typeof(AbilityCreateTokenComponent),
		typeof(AbilityDragonBlastComponent),
		typeof(AbilityDurationComponent),
		typeof(AbilityFasterMovementComponent),
		typeof(AbilityGiantSpeedupComponent),
		typeof(AbilityMagnetizedComponent),
		typeof(AbilityRandomPowerupComponent),
		typeof(AbilityResurrectComponent),
		typeof(AbilityRunComponent),
		typeof(AbilityScoreBonusComponent),
		typeof(AbilityShieldComponent),
		typeof(AbilitySpeedupComponent),
		typeof(AbilityStateChangedListenerComponent),
		typeof(AbilityTimerComponent),
		typeof(AbilityUpgradeScoreComponent),
		typeof(AngryRunComponent),
		typeof(AssetComponent),
		typeof(AsyncRequestComponent),
		typeof(CameraAttachedComponent),
		typeof(CameraStopComponent),
		typeof(CandyStoreComponent),
		typeof(ChangeCameraSizeTriggerComponent),
		typeof(ChangeScaleTimeComponent),
		typeof(ChangeWorldSkinComponent),
		typeof(ChangeWorldSpeedComponent),
		typeof(CloudComponent),
		typeof(CoinsComponent),
		typeof(CoinsListenerComponent),
		typeof(CollectedComponent),
		typeof(CollectibleCoinComponent),
		typeof(CollectibleComponent),
		typeof(CollectibleDataComponent),
		typeof(CollectibleGiantTokenComponent),
		typeof(CollectibleMagnetComponent),
		typeof(CollectibleScoreComponent),
		typeof(CollectibleScoreMultiplierComponent),
		typeof(CollectibleSpeedUpComponent),
		typeof(CollectibleStaminaComponent),
		typeof(CollectibleUpgradeScoreComponent),
		typeof(CollectSoundComponent),
		typeof(CompanionComponent),
		typeof(DailyMissionCollectComponent),
		typeof(DailyMissionDestroyObstacleComponent),
		typeof(DailyMissionPlayMissionsComponent),
		typeof(DeadComponent),
		typeof(DestroyedComponent),
		typeof(DestroyZonedComponent),
		typeof(DisableCollidersComponent),
		typeof(DroppingComponent),
		typeof(FadeOutComponent),
		typeof(FloatingComponent),
		typeof(FlyAwayComponent),
		typeof(GainedScoreComponent),
		typeof(GameStartedComponent),
		typeof(GodModeComponent),
		typeof(GroundedComponent),
		typeof(ImprovedScoreTokenComponent),
		typeof(IngameBgComponent),
		typeof(InitialTimeScaleComponent),
		typeof(InitialTriggerComponent),
		typeof(InvulnerabilityComponent),
		typeof(JumpingComponent),
		typeof(KillZoneComponent),
		typeof(LevelEndComponent),
		typeof(LevelFinishedComponent),
		typeof(LevelFinishedListenerComponent),
		typeof(LevelSettingsComponent),
		typeof(LoadAssetAsyncComponent),
		typeof(MagnetizedComponent),
		typeof(MissionChestGrantedComponent),
		typeof(MissionCollectComponent),
		typeof(MissionCounterComponent),
		typeof(MissionDataComponent),
		typeof(MissionResultComponent),
		typeof(MissionSpriteComponent),
		typeof(MissionSpriteFillAmountComponent),
		typeof(MissionThresholdsComponent),
		typeof(MissionTypeComponent),
		typeof(MissionUpdateListenerComponent),
		typeof(MovableComponent),
		typeof(MovableDataComponent),
		typeof(NewWorldSkinComponent),
		typeof(ObstacleComponent),
		typeof(ObstacleHitListenerComponent),
		typeof(OffsetPositionComponent),
		typeof(OnCloudsComponent),
		typeof(PausedComponent),
		typeof(PauseListenerComponent),
		typeof(PlatformComponent),
		typeof(PlayerAttachedComponent),
		typeof(PlayerBlinkingComponent),
		typeof(PlayerComponent),
		typeof(PlayerLandListenerComponent),
		typeof(PlayerRelayComponent),
		typeof(PlayerResurrectComponent),
		typeof(PlayerReviveComponent),
		typeof(PlayGameFailMusicComponent),
		typeof(PlayGameMusicComponent),
		typeof(PlayGameWinMusicComponent),
		typeof(PlaySoundsListenerComponent),
		typeof(PoolableComponent),
		typeof(PoolContainerComponent),
		typeof(PoolDespawnedComponent),
		typeof(PoolSpawnedComponent),
		typeof(PositionComponent),
		typeof(Raccoon),
		typeof(RaccoonDropAnimationComponent),
		typeof(RaccoonIntroComponent),
		typeof(RaccoonIntrosListenerComponent),
		typeof(RaccoonOutroComponent),
		typeof(ReplayLevelComponent),
		typeof(ResetPoolableComponent),
		typeof(RestableAreaComponent),
		typeof(RestingComponent),
		typeof(ResurectionsDoneComponent),
		typeof(ReturnToHomeLevelComponent),
		typeof(RevivesDoneComponent),
		typeof(RunDurationComponent),
		typeof(ScoreComponent),
		typeof(ScoreListenerComponent),
		typeof(ScoreMultiplierComponent),
		typeof(ScoreTokenComponent),
		typeof(SelectedCharacterComponent),
		typeof(SetupAbility),
		typeof(ShakingComponent),
		typeof(SizeIncreasedComponent),
		typeof(SlidingComponent),
		typeof(SparkleComponent),
		typeof(SpecialRunComponent),
		typeof(SpeedDataComponent),
		typeof(SpeedUpComponent),
		typeof(StaminaComponent),
		typeof(StaminasListenerComponent),
		typeof(SwingingDataComponent),
		typeof(TutorialPerformedComponent),
		typeof(TutorialPlayStepComponent),
		typeof(TutorialsListenerComponent),
		typeof(TutorialTriggerComponent),
		typeof(UnderTheScreenComponent),
		typeof(UpgradeScoreComponent),
		typeof(UpgradeSingleScoreComponent),
		typeof(ViewComponent),
		typeof(WaitToStartComponent),
		typeof(WorldComponent),
		typeof(WorldTimeScaleComponent)
	};
}
