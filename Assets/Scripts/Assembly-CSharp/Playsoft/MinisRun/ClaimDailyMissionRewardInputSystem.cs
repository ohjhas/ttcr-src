using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ClaimDailyMissionRewardInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		private readonly StateContext stateContext;

		public ClaimDailyMissionRewardInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIClaimDailyMissionReward;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			DailyMissionsCollection collection = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection.Collection;
			foreach (UIEntity entity in entities)
			{
				DailyMissionData dailyMissionData = collection.DailyMissions[(int)entity.uIClaimDailyMissionReward.group];
				ClaimReward(dailyMissionData, entity.uIClaimDailyMissionReward.origin);
				SetRewardAsClaimed(dailyMissionData);
				entity.isDestroyed = true;
			}
		}

		private void ClaimReward(DailyMissionData claimedMission, RectTransform origin)
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			RewardData newData = new RewardData
			{
				Type = claimedMission.MissionData.ITEMTYPE,
				Index = claimedMission.MissionData.Itemindex,
				Amount = claimedMission.MissionData.Itemamount
			};
			uIEntity.isUIAction = true;
			uIEntity.AddUIClaimReward(newData, origin, true);
		}

		private void SetRewardAsClaimed(DailyMissionData claimedMission)
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("DailyMissionDataSend");
			stateEntity.AddDailyMissionClaimUpdate(claimedMission.MissionData.DAILYMISSIONGROUP, true);
		}
	}
}
