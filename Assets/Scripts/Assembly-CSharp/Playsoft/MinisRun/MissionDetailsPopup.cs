using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class MissionDetailsPopup : BasePopupScreen
	{
		[Header("MissionDetailsPoup")]
		[Space(5f)]
		[SerializeField]
		private Button goButton;

		[SerializeField]
		private MissionDetailsRibbon ribbonPart;

		[SerializeField]
		private MissionDetailsGoalText goalTextPart;

		[SerializeField]
		private MissionDetailsProgressBar progressPart;

		[SerializeField]
		private MissionDetailsGoalIcon goalIconPart;

		[SerializeField]
		private MissionDetailsReward rewardPart;

		private int levelId;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.MissionDetailsPopup;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (parameters.hasUIDisplayMissionDetails)
			{
				levelId = parameters.uIDisplayMissionDetails.LevelId;
				WSLevelData levelData = parameters.uIMissionListResponse.LevelDataList[levelId];
				ribbonPart.SetUp(levelId + 1);
				goalTextPart.SetUp(levelData.MissionType);
				rewardPart.SetUp(levelData, parameters.uIChestListResponse.ChestDataList);
				progressPart.SetUp(levelData.ItemsToCollect, levelData.ResultValue, !rewardPart.IsActive());
				goalIconPart.SetUp(levelData.MissionType);
				goButton.onClick.RemoveAllListeners();
				goButton.onClick.AddListener(GoButtonClicked);
			}
		}

		private void GoButtonClicked()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.AddUIPlayMission(levelId);
			}
		}
	}
}
