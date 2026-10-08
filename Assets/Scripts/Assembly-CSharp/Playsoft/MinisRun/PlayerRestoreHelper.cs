using O7SDK.O7ADSDK;

namespace Playsoft.MinisRun
{
	public static class PlayerRestoreHelper
	{
		public static bool IsReviveAvailable()
		{
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			int num = restoreSettingsBalancingData.Map[RestoreSettings.MaxRevivesPerPlay];
			int num2 = (playerEntity.hasRevivesDone ? playerEntity.revivesDone.Amount : 0);
			return num > num2;
		}

		public static bool IsReviveForVideoAvailable()
		{
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			int num = restoreSettingsBalancingData.Map[RestoreSettings.RevivesForVideo];
			int num2 = (playerEntity.hasRevivesDone ? playerEntity.revivesDone.Amount : 0);
			StateEntity o7SdkEntity = Contexts.sharedInstance.state.o7SdkEntity;
			O7ADRewardAdManager manager = o7SdkEntity.o7SdkRewardedAdManager.Manager;
			return num > num2 && o7SdkEntity != null && manager.IsVideoLoaded() && o7SdkEntity.isO7SdkRewardAdFetched;
		}

		public static bool IsResurectAvailable()
		{
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			int num = restoreSettingsBalancingData.Map[RestoreSettings.MaxResurectsPerPlay];
			int num2 = (playerEntity.hasResurectionsDone ? playerEntity.resurectionsDone.Amount : 0);
			return num > num2;
		}

		public static bool IsResurectForVideoAvailable()
		{
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			int num = restoreSettingsBalancingData.Map[RestoreSettings.ResurectsForVideoPerPlay];
			int num2 = (playerEntity.hasResurectionsDone ? playerEntity.resurectionsDone.Amount : 0);
			StateEntity o7SdkEntity = Contexts.sharedInstance.state.o7SdkEntity;
			O7ADRewardAdManager manager = o7SdkEntity.o7SdkRewardedAdManager.Manager;
			return num > num2 && o7SdkEntity != null && manager.IsVideoLoaded() && o7SdkEntity.isO7SdkRewardAdFetched;
		}
	}
}
