using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public abstract class DailyMissionRewardLogic : MonoBehaviour
	{
		[SerializeField]
		protected TextMeshProUGUI _description;

		[SerializeField]
		protected string _iconName;

		public void Init(DailyMissionData data)
		{
			SetDescription(data);
			SetElementsVisibility(data);
		}

		public abstract void GetReward(DailyMissionData data);

		protected virtual void SetDescription(DailyMissionData data)
		{
			_description.text = "<size=80%>" + NumbersHelper.GetNumberSplitedBySpaces(data.MissionData.Itemamount) + "</size>";
		}

		private void SetElementsVisibility(DailyMissionData data)
		{
			_description.gameObject.SetActive(!data.IsClaimed);
		}
	}
}
