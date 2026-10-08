using Playsoft.MinisRun;

public sealed class StateSystems : Feature
{
	public StateSystems(Contexts contexts)
		: base("Game State Systems")
	{
		Add(new SessionInitializeSystem(contexts));
		Add(new FirstLaunchCheckSystem(contexts));
		Add(new InitSavedDataSystem(contexts));
		Add(new LogInitSceneSystems(contexts));
		Add(new InitialStartSystem(contexts));
		Add(new ChangeTimeScaleSystem(contexts));
		Add(new TutorialManagerSystems(contexts));
		Add(new RealTimeSystem(contexts));
		Add(new RefreshCurrentTimeSystem(contexts));
		Add(new OpenUrlSystem(contexts));
		Add(new PlaySoundEffectSystem(contexts));
		Add(new PlayLoopSoundEffectSystem(contexts));
		Add(new PlayMusicSystem(contexts));
		Add(new MuteSoundEffectsSystem(contexts));
		Add(new MuteSoundsSourceSystem(contexts));
		Add(new MuteMusicSystem(contexts));
		Add(new StopMusicSystem(contexts));
		Add(new MultitouchEnableSystem(contexts));
		Add(new PlayerProgressionSystems(contexts));
		Add(new TransactionSystems(contexts));
		Add(new FreeChestInitializeSystem(contexts));
		Add(new FreeChestResetSystem(contexts));
		Add(new FreeChestSetInitialTimeSystem(contexts));
		Add(new FreeChestUnlockSystem(contexts));
		Add(new ToggleSoundsSystem(contexts));
		Add(new ToggleMusicSystem(contexts));
		Add(new SaveScoreResultSystem(contexts));
		Add(new SaveMissionResultSystem(contexts));
		Add(new UpdateUnlockedLevelSystem(contexts));
		Add(new CharacterUnlockSystem(contexts));
		Add(new CharacterUpgradeSystem(contexts));
		Add(new CompanionUnlockSystem(contexts));
		Add(new CompanionUpgradeSystem(contexts));
		Add(new UpdateMissionChestAvailabilitySystem(contexts));
		Add(new DailyMissionOutgameSystems(contexts));
		Add(new LastSceneSaveDataSystem(contexts));
		Add(new UnlockedLevelSaveDataSystem(contexts));
		Add(new SelectedLevelSaveDataSystem(contexts));
		Add(new HighScoreSaveDataSystem(contexts));
		Add(new MissionResultValueSaveDataSystem(contexts));
		Add(new MissionSaveDataSystem(contexts));
		Add(new MissionResultValueSaveDataSystem(contexts));
		Add(new MissionChestAvailabilitySaveDataSystem(contexts));
		Add(new NewLevelUnlockedSaveDataSystem(contexts));
		Add(new PlayerLevelNumberSaveDataSystem(contexts));
		Add(new PlayerLevelExperienceSaveDataSystem(contexts));
		Add(new PlayerProgressionUpdateSaveDataSystem(contexts));
		Add(new PlayerProgressionExpPoolSaveDataSystem(contexts));
		Add(new TutorialOutgameSaveDataSystem(contexts));
		Add(new NewCharactersUnlockedSaveDataSystem(contexts));
		Add(new NewCompanionUnlockedSavedDataSystem(contexts));
		Add(new CharactersUpgradeSaveDataSystem(contexts));
		Add(new CharactersPartsSaveDataSystem(contexts));
		Add(new CharactersUnlockSaveDataSystem(contexts));
		Add(new CharacterFirstSelectedSaveDataSystem(contexts));
		Add(new CharacterSecondSelectedSaveDataSystem(contexts));
		Add(new CompanionUpgradeSaveDataSystem(contexts));
		Add(new CompanionPartsSaveDataSystem(contexts));
		Add(new CompanionsUnlockSaveDataSystem(contexts));
		Add(new CompanionSelectedSaveDataSystem(contexts));
		Add(new MissionChestAvailabilitySaveDataSystem(contexts));
		Add(new RateMeAnsweredSaveDataSystem(contexts));
		Add(new RateMeSaveDataSystem(contexts));
		Add(new AdTrackingSaveDataSystem(contexts));
		Add(new CountryCodeSaveDataSystem(contexts));
		Add(new PaidUserSaveDataSystem(contexts));
		Add(new TutorialStaminaHitSaveData(contexts));
		Add(new FreeChestSaveDataSystem(contexts));
		Add(new FreeChestInitialSaveDataSystem(contexts));
		Add(new DailyMissionsSaveDataSystem(contexts));
		Add(new CharacterEnergySystems(contexts));
		Add(new SaveDataToPlayerPrefsSystem(contexts));
		Add(new LoadDataFromPlayerPrefsSystem(contexts));
		Add(new InitializeLocalUserStateSystem(contexts));
		Add(new UpdateLocalUserStateSystem(contexts));
		Add(new ReplaceLocalUserStateSystem(contexts));
		Add(new PrivacyAndLegalClickedEventSendSystem(contexts));
		Add(new EulaClickedEventSendSystem(contexts));
		Add(new DestroyWorldSystem(contexts));
	}
}
