using Playsoft.MinisRun;

public sealed class UISystems : Feature
{
	public UISystems(Contexts contexts)
		: base("UI Systems")
	{
		Add(new UIInitSystem(contexts));
		Add(new UIPreloadAssetsSystem(contexts));
		Add(new DataProviderSystems(contexts));
		Add(new UIInputSystems(contexts));
		Add(new HUDGoBackSystem(contexts));
		Add(new RefreshFreeChestSystem(contexts));
		Add(new RefreshScreenSystem(contexts));
		Add(new RefreshAfterLevelUpSystem(contexts));
		Add(new RefreshCoinsCounterSystem(contexts));
		Add(new RefreshHardCurrencyCounterSystem(contexts));
		Add(new RefreshSoundComponentsSystem(contexts));
		Add(new RefreshMusicComponentsSystem(contexts));
		Add(new RefreshCompanionSelectionSystem(contexts));
		Add(new RefreshCharacterSelectionSystem(contexts));
		Add(new O7SdkAdTrackingRefreshSystem(contexts));
		Add(new RefreshUDIDDisplaySystem(contexts));
		Add(new AsyncLoadSystem(contexts));
		Add(new UILoadingSpinnerSystem(contexts));
		Add(new UIChangeSceneSystem(contexts));
		Add(new UISceneObjectsCleanupSystem(contexts));
		Add(new InitialSceneDeciderSystem(contexts));
		Add(new LogsUISystems(contexts));
		Add(new UIDestroySystem(contexts));
	}
}
