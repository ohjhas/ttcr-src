using System.Linq;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class DailyReward : MonoBehaviour
	{
		[SerializeField]
		private GameObject ComeBackTomorrowGroup;

		[SerializeField]
		private GameObject RewardGroup;

		[SerializeField]
		private Image Icon;

		[SerializeField]
		private Image BarFilling;

		[SerializeField]
		private TextMeshProUGUI BarText;

		public void SetData(UIEntity parameters, bool shouldAnimateBar)
		{
			DailyMissionsCollection dailyMissionsCollection = parameters.uICurrentDailyMissionsResponse.DailyMissionsCollection;
			int dailyMissionChestId = Contexts.sharedInstance.state.balancingDataEntity.initConfigData.Map[EnumSettings.DailyMissionChestId];
			SetIcon(parameters.uIChestListResponse.ChestDataList.Find((ChestData ch) => ch.Id.Equals(dailyMissionChestId)).AssetName);
			SetBarFilling(dailyMissionsCollection, shouldAnimateBar);
			SetBarText(dailyMissionsCollection);
			SetElementsVisibility(dailyMissionsCollection.IsChestClaimed);
		}

		private void SetIcon(string assetName)
		{
			Sprite sprite = SettingsService.AssetsSettingsSO.GetSprite(assetName);
			if (!(sprite == null))
			{
				Icon.sprite = sprite;
			}
		}

		private void SetBarFilling(DailyMissionsCollection dailyMissionsCollection, bool shouldAnimateBar)
		{
			float endValue = dailyMissionsCollection.DailyMissions.Where((DailyMissionData x) => x.IsClaimed).Sum((DailyMissionData x) => 1f / (float)dailyMissionsCollection.DailyMissions.Length);
			float duration = ((!shouldAnimateBar) ? 0f : 0.5f);
			DOTween.To(() => BarFilling.fillAmount, delegate(float x)
			{
				BarFilling.fillAmount = x;
			}, endValue, duration);
		}

		private void SetBarText(DailyMissionsCollection dailyMissionsCollection)
		{
			int num = dailyMissionsCollection.DailyMissions.Where((DailyMissionData x) => x.IsClaimed).Sum((DailyMissionData x) => 1);
			BarText.text = num + "/" + dailyMissionsCollection.DailyMissions.Length;
		}

		private void SetElementsVisibility(bool isChestClaimed)
		{
			ComeBackTomorrowGroup.SetActive(isChestClaimed);
			RewardGroup.SetActive(!isChestClaimed);
		}
	}
}
