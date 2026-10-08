using O7SDK;
using Playsoft.MinisRun;

public sealed class O7SDKSystem : Feature
{
	public O7SDKSystem(Contexts contexts)
		: base("O7SDK Systems")
	{
		Add(new O7SdkManagerInitializeSystem(contexts));
		Add(new O7SdkDataSystem(contexts));
		Add(new O7SdkConfigurationInitSystem(contexts));
		Add(new O7SDKExtendedConfigInitSystem(contexts));
		Add(new O7SdkConfigurationUpdateSystem(contexts));
		Add(new O7SDKUserStateInitSystem(contexts));
		Add(new O7SDKUserStateSendInitialStateSystem(contexts));
		Add(new LogRestoreProgressSystem(contexts));
		Add(new O7SDKUserStateConflictSolverSystem(contexts));
		Add(new O7SDKUserStateSendUpdateSystem(contexts));
		Add(new O7SdkRewardValidationSystem(contexts));
		Add(new O7SdkRewardAdCloseSystem(contexts));
		Add(new NewSceneInterstitialSystem(contexts));
		Add(new ScreenChangedInterstitialSystem(contexts));
		Add(new NewSceneBannerSystem(contexts));
		Add(new ScreenChangedBannerSystem(contexts));
		Add(new O7SdkBannerAdSystem(contexts));
		Add(new O7SdkInterstitialAdSystem(contexts));
		Add(new O7SdkAutoNewsShowSystem(contexts));
		Add(new O7SdkCrossPromoSystem(contexts));
		Add(new O7SdkRewardAdSystem(contexts));
		Add(new O7SdkVideoGallerySystem(contexts));
		Add(new O7SdkUIModifierSystem(contexts));
		Add(new O7SdkPrivacyExplanationShowSystem(contexts));
		Add(new O7SdkForceCheckSystem(contexts));
		Add(new O7SdkAgeGateDeciderSystem(contexts));
		Add(new O7SdkAgeGateSystem(contexts));
		Add(new O7SdkGenderGateSystem(contexts));
		Add(new O7SdkGdprAdsDataProviderSystem(contexts));
		Add(new O7SdkGdprShowDeciderSystem(contexts));
		Add(new O7SdkGdprOptOutDeciderSystem(contexts));
		Add(new O7SdkGdprSystem(contexts));
		Add(new O7SdkGdprShowSystem(contexts));
		Add(new O7SdkForceShowSystem(contexts));
		Add(new O7SdkGdprShowDoneSystem(contexts));
		Add(new O7SdkFinishForceGdprSystem(contexts));
		Add(new O7SdkPurchaseSystem(contexts));
		Add(new O7SdkIapInitializeSystem(contexts));
		Add(new O7SdkPurchaseValidationSystem(contexts));
		Add(new PaidUserUpdateSystem(contexts));
		Add(new O7SdkRateMePopupShowSystem(contexts));
		Add(new O7SdkRateMeSystem(contexts));
		Add(new O7SdkAdTrackingSystem(contexts));
		Add(new O7SdkAdTrackingEnableSystem(contexts));
		Add(new O7SdkCountryCodeBroadcastSystem(contexts));
		Add(new O7SdkCountryCodeSystem(contexts));
		Add(new O7SdkPrivacyPolicyShowSystem(contexts));
		Add(new LogGDPRSystems(contexts));
		Add(new O7SdkEventsSystem(contexts));
		Add(new O7SdkSendLogsSystem(contexts));
		Add(new O7SdkDestroySystem(contexts));
	}
}
