using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DailyMissionsScreen : BaseScreen
	{
		[Space(5f)]
		[Header("Daily Missions")]
		[SerializeField]
		private RectTransform _contentRectTransform;

		[SerializeField]
		private DailyMissionsTimer _timer;

		[SerializeField]
		private List<DailyMissionEntry> _dailyMissionEntries;

		[SerializeField]
		private DailyReward _dailyReward;

		private Tween delayedDailyReward;

		private bool isScreenJustOpened;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.DailyMissionScreen;
			}
		}

		protected override void Setup()
		{
			base.Setup();
			isScreenJustOpened = true;
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			CurrencyAndExperienceHelper.OpenScreenForDailyMissionsScreen();
			_timer.SetData();
			if (parameters.hasUICurrentDailyMissionsResponse)
			{
				DailyMissionsCollection dailyMissionCollection = parameters.uICurrentDailyMissionsResponse.DailyMissionsCollection;
				SetDailyMissionEntries(dailyMissionCollection);
				delayedDailyReward.Kill();
				delayedDailyReward = DOVirtual.DelayedCall(1f, delegate
				{
					GiveDailyRewardIfNeeded(dailyMissionCollection);
				});
			}
			if (parameters.hasUIChestListResponse)
			{
				_dailyReward.SetData(parameters, !isScreenJustOpened);
			}
			isScreenJustOpened = false;
		}

		private void SetDailyMissionEntries(DailyMissionsCollection dailyMissionCollection)
		{
			for (int i = 0; i < _dailyMissionEntries.Count; i++)
			{
				if (!(null == _dailyMissionEntries[i]))
				{
					_dailyMissionEntries[i].gameObject.SetActive(true);
					_dailyMissionEntries[i].SetData(dailyMissionCollection.DailyMissions[i]);
				}
			}
		}

		private void GiveDailyRewardIfNeeded(DailyMissionsCollection dailyMissionCollection)
		{
			bool flag = !dailyMissionCollection.IsChestClaimed;
			for (int i = 0; i < _dailyMissionEntries.Count; i++)
			{
				flag = flag && dailyMissionCollection.DailyMissions[i].IsClaimed;
			}
			if (flag)
			{
				GiveDailyReward();
			}
		}

		private void GiveDailyReward()
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.isUIGiveDailyMissionsDailyReward = true;
		}
	}
}
