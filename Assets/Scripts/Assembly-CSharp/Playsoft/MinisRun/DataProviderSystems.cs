namespace Playsoft.MinisRun
{
	public class DataProviderSystems : Feature
	{
		public DataProviderSystems(Contexts contexts)
			: base("Data Provider Systems")
		{
			Add(new TotalCoinsDataProviderSystem(contexts));
			Add(new TotalGemsDataProviderSystem(contexts));
			Add(new TotalStarsDataProviderSystem(contexts));
			Add(new CharListDataProviderSystem(contexts));
			Add(new CharSingleDataProviderSystem(contexts));
			Add(new CharSelectedDataProviderSystem(contexts));
			Add(new NewCharactersUnlockedDataProviderSystem(contexts));
			Add(new NewCompanionsUnlockedDataProviderSystem(contexts));
			Add(new CompListDataProviderSystem(contexts));
			Add(new CompSingleDataProviderSystem(contexts));
			Add(new CompSelectedDataProviderSystem(contexts));
			Add(new ChestListDataProviderSystem(contexts));
			Add(new ChestRewardDataProvider(contexts));
			Add(new MissionListDataProviderSystem(contexts));
			Add(new ChapterListDataProviderSystem(contexts));
			Add(new LastSelectedMissionDataProviderSystem(contexts));
			Add(new UnlockedMissionDataProviderSystem(contexts));
			Add(new NewWorldUnlockedDataProviderSystem(contexts));
			Add(new ShopIAPsListDataProviderSystem(contexts));
			Add(new ShopSoftCurrencyPacksListDataProviderSystem(contexts));
			Add(new ShopFreeChestDataProviderSystem(contexts));
			Add(new PlayerProgressionStateProviderSystem(contexts));
			Add(new DailyMissionsDataProviderSystem(contexts));
			Add(new FinishSetupResponseSystem(contexts));
		}
	}
}
