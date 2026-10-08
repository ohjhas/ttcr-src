namespace Playsoft.MinisRun
{
	public class DailyMissionChestRewardLogic : DailyMissionRewardLogic
	{
		protected override void SetDescription(DailyMissionData data)
		{
			_description.text = "<sprite name=" + _iconName + ">";
		}

		public override void GetReward(DailyMissionData data)
		{
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIClaimDailyMissionReward(data.MissionData.DAILYMISSIONGROUP, null);
		}
	}
}
