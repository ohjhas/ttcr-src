using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DailyMissionView : MonoBehaviour
	{
		[SerializeField]
		private GameObject MissionsContainer;

		[SerializeField]
		private GameObject ChestClaimedMark;

		[SerializeField]
		private List<GameObject> MissionEntryFills;

		[SerializeField]
		private ShakeEffect ChestShakeEffect;

		public void UpdateView(DailyMissionsCollection collection)
		{
			SetUpShakeEffect(collection);
			SetUpCheckmark(collection);
			SetUpBar(collection);
		}

		private void SetUpShakeEffect(DailyMissionsCollection collection)
		{
			ChestShakeEffect.Stop(true);
			if (!collection.IsChestClaimed && collection.DailyMissions.ToList().All((DailyMissionData x) => x.CurrentProgress >= x.MissionData.Dailymissiongoal))
			{
				ChestShakeEffect.Play();
			}
		}

		private void SetUpCheckmark(DailyMissionsCollection collection)
		{
			ChestClaimedMark.SetActive(collection.IsChestClaimed);
		}

		private void SetUpBar(DailyMissionsCollection collection)
		{
			MissionsContainer.SetActive(!collection.IsChestClaimed);
			for (int i = 0; i < MissionEntryFills.Count; i++)
			{
				if (i < collection.DailyMissions.Length)
				{
					bool active = collection.DailyMissions[i].CurrentProgress >= collection.DailyMissions[i].MissionData.Dailymissiongoal;
					MissionEntryFills[i].SetActive(active);
				}
				else
				{
					MissionEntryFills[i].transform.parent.gameObject.SetActive(false);
				}
			}
		}
	}
}
