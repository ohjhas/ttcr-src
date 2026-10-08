using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DailyMissionCurrencyRewardLogic : DailyMissionRewardLogic
	{
		[SerializeField]
		private RectTransform _origin;

		public override void GetReward(DailyMissionData data)
		{
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIClaimDailyMissionReward(data.MissionData.DAILYMISSIONGROUP, _origin);
		}
	}
}
