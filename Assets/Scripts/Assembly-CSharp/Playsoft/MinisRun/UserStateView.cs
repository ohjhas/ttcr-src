using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class UserStateView : MonoBehaviour
	{
		[SerializeField]
		private Image icon;

		[SerializeField]
		private TextMeshProUGUI coinsCountText;

		[SerializeField]
		private TextMeshProUGUI diamondsCountText;

		[SerializeField]
		private TextMeshProUGUI levelText;

		[SerializeField]
		private TextMeshProUGUI levelNumberText;

		public void SetData(UserStateData userStateData)
		{
			icon.sprite = SettingsService.AssetsSettingsSO.GetSprite(GetChapterSprite(userStateData));
			coinsCountText.text = userStateData.Coins.ToString();
			diamondsCountText.text = userStateData.HardCurrency.ToString();
			levelText.text = GetLevelText(userStateData);
			levelNumberText.text = ScriptLocalization.general_level + " " + (userStateData.UnlockedLevel + 1);
		}

		private string GetLevelText(UserStateData userStateData)
		{
			MissionsBalancingData missionsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.missionBalancingData.Map[userStateData.UnlockedLevel];
			ChaptersBalancingData chaptersBalancingData = Contexts.sharedInstance.state.balancingDataEntity.chapterBalancingData.Map[missionsBalancingData.Chapter];
			return chaptersBalancingData.Chaptername;
		}

		private string GetChapterSprite(UserStateData userStateData)
		{
			MissionsBalancingData missionsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.missionBalancingData.Map[userStateData.UnlockedLevel];
			ChaptersBalancingData chaptersBalancingData = Contexts.sharedInstance.state.balancingDataEntity.chapterBalancingData.Map[missionsBalancingData.Chapter];
			return chaptersBalancingData.Assetname;
		}
	}
}
