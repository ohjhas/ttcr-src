using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WinScreenStarsAndBar
	{
		[SerializeField]
		private ShakeEffect progressBarShakeEffect;

		[SerializeField]
		private CanvasParticleEmitter confetiParticle;

		[SerializeField]
		private Image missionGoalProgressImage;

		[SerializeField]
		private Transform starsHolder;

		[SerializeField]
		private List<Animator> starSlotsAnimators;

		[SerializeField]
		private List<WinScreenProgressBarStarController> progressBarStars;

		[SerializeField]
		private Image goalIcon;

		[SerializeField]
		private NotLinearProgressBar progressBarFactor;

		private readonly bool[] isStarAnimationStarted = new bool[3];

		private MissionsBalancingData missionData;

		private WinScreen winScreen;

		public void Init(WinScreen screen, bool barDisplayed, bool chestWon, bool threeStars)
		{
			winScreen = screen;
			SetGoalCounter(barDisplayed);
			SettingsResultScreenSO setting = SettingsService.GetSetting<SettingsResultScreenSO>();
			screen.StartCoroutine(UpdateProgressBarAndStars(setting, barDisplayed));
			if (chestWon || threeStars)
			{
				DOVirtual.DelayedCall(0.5f + setting.CounterAnimTime + 0.5f, ShakeAndScaleDownTheProgressbar);
			}
		}

		private void SetGoalCounter(bool barVisible)
		{
			missionData = Contexts.sharedInstance.state.balancingDataEntity.missionBalancingData.Map[SettingsService.SettingsSO.WorldIndex];
			if (!barVisible)
			{
				progressBarShakeEffect.gameObject.SetActive(false);
				return;
			}
			goalIcon.sprite = SettingsService.AssetsSettingsSO.GetMissionIcon(missionData.MISSIONTYPE).Sprite;
			progressBarStars[0].Init(missionData.Star1);
			progressBarStars[1].Init(missionData.Star2);
			progressBarStars[2].Init(missionData.Star3);
		}

		private IEnumerator UpdateProgressBarAndStars(SettingsResultScreenSO settingsResultScreenSo, bool barVisible)
		{
			yield return new WaitForSecondsRealtime((!barVisible) ? 0f : 0.5f);
			int result = GetCollectedItemsCount();
			float timer = 0f;
			AnimationCurve animCurve = settingsResultScreenSo.CounterAnimCurve;
			float animDuration = settingsResultScreenSo.CounterAnimTime;
			if (barVisible)
			{
				Contexts.sharedInstance.state.CreateEntity().AddPlayLoopSoundEffect("win_bar_counting");
			}
			while (timer < animDuration)
			{
				timer += Time.unscaledDeltaTime;
				int currentValue = (int)((float)result * animCurve.Evaluate(timer / animDuration));
				missionGoalProgressImage.fillAmount = progressBarFactor.GetFillAmount(currentValue, missionData.Star1, missionData.Star2, missionData.Star3);
				ShowStarsAnimation(currentValue);
				yield return null;
			}
			if (result >= missionData.Star3)
			{
				confetiParticle.StartEmitter();
			}
			LevelTutorialSettings levelSettings = Contexts.sharedInstance.game.worldEntity.levelSettings.levelTutorialSettings;
			if (levelSettings.ResultSettings.GoalBar)
			{
				Contexts.sharedInstance.state.CreateEntity().AddStopLoopSoundEffect("win_bar_counting");
			}
		}

		private void ShowStarsAnimation(int currentValue)
		{
			if (!isStarAnimationStarted[0] && currentValue >= missionData.Star1)
			{
				isStarAnimationStarted[0] = true;
				ShowStarAnimation(0);
			}
			if (!isStarAnimationStarted[1] && currentValue >= missionData.Star2)
			{
				isStarAnimationStarted[1] = true;
				ShowStarAnimation(1);
			}
			if (!isStarAnimationStarted[2] && currentValue >= missionData.Star3)
			{
				isStarAnimationStarted[2] = true;
				ShowStarAnimation(2);
			}
		}

		private void ShowStarAnimation(int index)
		{
			progressBarStars[index].SetReached();
			DOVirtual.DelayedCall(0.35f, delegate
			{
				starSlotsAnimators[index].Play("StarFillUp");
			});
		}

		private int GetCollectedItemsCount()
		{
			if (Contexts.sharedInstance.state.balancingDataEntity == null)
			{
				return 0;
			}
			GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			if (missionData.MISSIONTYPE == MissionType.Have_Stamina)
			{
				return (worldEntity.hasMissionCounter && worldEntity.missionCounter.Value >= 0 && !playerEntity.isUnderTheScreen) ? worldEntity.missionCounter.Value : 0;
			}
			return (!worldEntity.hasMissionCounter || worldEntity.missionCounter.Value > missionData.Star3) ? missionData.Star3 : worldEntity.missionCounter.Value;
		}

		private void ShakeAndScaleDownTheProgressbar()
		{
			progressBarShakeEffect.Play();
			SettingsResultScreenSO setting = SettingsService.GetSetting<SettingsResultScreenSO>();
			progressBarShakeEffect.transform.DOScale(0f, setting.MissionProgressBarOnThreeStarsAnimationScaleDownDuration).SetEase(setting.MissionProgressBarOnThreeStarsAnimationScaleDownAnimationCurve).SetUpdate(true)
				.OnComplete(delegate
				{
					progressBarShakeEffect.Stop();
				});
		}
	}
}
