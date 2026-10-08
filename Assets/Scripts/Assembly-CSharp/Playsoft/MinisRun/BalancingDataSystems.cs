namespace Playsoft.MinisRun
{
	public class BalancingDataSystems : Feature
	{
		public BalancingDataSystems(Contexts contexts)
			: base("Balancing Data Systems")
		{
			Add(new BalancingDataEconomySystem(contexts));
			Add(new BalancingDataMissionSystem(contexts));
			Add(new BalancingDataChapterSystem(contexts));
			Add(new BalancingDataCharactersSystem(contexts));
			Add(new BalancingDataChestsSystem(contexts));
			Add(new BalancingDataAbilitiesSystem(contexts));
			Add(new BalancingDataMissionChestSystem(contexts));
			Add(new BalancingDataCompanionsSystem(contexts));
			Add(new BalancingDataShopIAPsSystem(contexts));
			Add(new BalancingDataShopSoftCurrencyPacksSystem(contexts));
			Add(new BalancingDataDailyMissionsSystem(contexts));
			Add(new BalancingDataPlayerLevelSystem(contexts));
			Add(new BalancingDataPlayerLevelExperienceAmountSystem(contexts));
			Add(new BalancingDataTutorialOutgameUnlockLevelsSystem(contexts));
			Add(new BalancingPrivacyAndLegalSystem(contexts));
			Add(new BalancingDataO7SdkSystem(contexts));
			Add(new BalancingDataRestoreSettingsSystem(contexts));
			Add(new BalancingDataStaminaSettingsSystem(contexts));
			Add(new BalancingDataOverrideConfigSystem(contexts));
		}
	}
}
