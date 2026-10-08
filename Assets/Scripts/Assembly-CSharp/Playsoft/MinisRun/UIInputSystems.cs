namespace Playsoft.MinisRun
{
	public class UIInputSystems : Feature
	{
		public UIInputSystems(Contexts contexts)
			: base("Input Handlers Systems")
		{
			Add(new WorldSelectInputSystem(contexts));
			Add(new CharacterSelectInputSystem(contexts));
			Add(new ShopScreenInputSystem(contexts));
			Add(new ResultScreenInputSystem(contexts));
			Add(new ChestRevealScreenInputSystem(contexts));
			Add(new RelayPlayerInputSystem(contexts));
			Add(new RevivePlayerInputSystem(contexts));
			Add(new ResurectPlayerInputSystem(contexts));
			Add(new HomeScreenInputSystem(contexts));
			Add(new CompanionSelectInputSystem(contexts));
			Add(new ClaimDailyMissionRewardInputSystem(contexts));
			Add(new ClaimRewardInputSystem(contexts));
			Add(new CheckPlayerProgressInputSystem(contexts));
			Add(new SettingsScreenInputSystem(contexts));
			Add(new SettingsFooterInputSystem(contexts));
			Add(new FlyingElementEffectSystem(contexts));
			Add(new TransactionValidatedSystem(contexts));
			Add(new TransactionNotValidatedSystem(contexts));
			Add(new DisplayExplanationSystem(contexts));
			Add(new DisplayMissionDetailsSystem(contexts));
			Add(new DisplayTransactionConfirmationSystem(contexts));
			Add(new DisplayAbilityDescriptionSystem(contexts));
			Add(new DisplayChestShopSystem(contexts));
			Add(new DisplayCurrencyUpdateAnimSystem(contexts));
			Add(new DisplayDailyMissionSystem(contexts));
			Add(new GiveDailyMissionsDailyRewardSystem(contexts));
			Add(new BuyRefillCharacterSystem(contexts));
		}
	}
}
