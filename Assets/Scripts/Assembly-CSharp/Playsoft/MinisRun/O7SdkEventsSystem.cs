namespace Playsoft.MinisRun
{
	public class O7SdkEventsSystem : Feature
	{
		public O7SdkEventsSystem(Contexts contexts)
			: base("O7Sdk EventsSystem")
		{
			Add(new O7SdkEventInAppPurchaseSystem(contexts));
			Add(new O7SdkEventAchievementCompleteSystem(contexts));
			Add(new O7SdkEventSelectContentSystem(contexts));
			Add(new O7SdkEventTutorialBeginSystem(contexts));
			Add(new O7SdkEventTutorialCompleteSystem(contexts));
			Add(new O7SdkEventLevelUpSystem(contexts));
			Add(new O7SdkEventLevelCompleteSystem(contexts));
			Add(new O7SdkEventEarnVirtualCurrencySystem(contexts));
			Add(new O7SdkEventSpendVirtualCurrencySystems(contexts));
			Add(new O7SdkEventCustomInstantSendSystem(contexts));
			Add(new O7SdkEventCustomSystem(contexts));
		}
	}
}
