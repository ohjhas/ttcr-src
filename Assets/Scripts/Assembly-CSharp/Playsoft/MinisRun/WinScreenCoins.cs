using System;
using System.Collections;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WinScreenCoins
	{
		[SerializeField]
		private ResultCounter coinsCounter;

		public void Init(WinScreen screen, bool barDisplayed)
		{
			screen.StartCoroutine(UpdateCoinsCounter((!barDisplayed) ? 0.3f : 1f));
		}

		private IEnumerator UpdateCoinsCounter(float delay = 0f)
		{
			GameEntity world = Contexts.sharedInstance.game.worldEntity;
			LevelTutorialSettings levelSettings = world.levelSettings.levelTutorialSettings;
			if (!levelSettings.ResultSettings.Coins)
			{
				coinsCounter.gameObject.SetActive(false);
				yield break;
			}
			SettingsResultScreenSO settingsResultScreenSo = SettingsService.GetSetting<SettingsResultScreenSO>();
			yield return new WaitForSecondsRealtime(delay);
			AnimationCurve animCurve = settingsResultScreenSo.CounterAnimCurve;
			float animDuration = settingsResultScreenSo.CounterAnimTime;
			int totalCoins = (world.hasCoins ? world.coins.Value : 0);
			coinsCounter.Show(totalCoins, animCurve, animDuration, AnimateCurrencyUpdateIfNeeded);
		}

		private void AnimateCurrencyUpdateIfNeeded()
		{
			int value = Contexts.sharedInstance.game.worldEntity.coins.Value;
			if (value > 0)
			{
				CurrencyAndExperienceHelper.OpenScreenForResultScreen(value, coinsCounter.GetComponent<RectTransform>());
			}
		}
	}
}
