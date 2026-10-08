using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class IngameHUD : BaseDialog, ITutorialListener, IRaccoonIntroListener, ILevelFinishedListener
	{
		[Serializable]
		private class ShowHudSettings
		{
			public float initialDelay;

			public float hideInterstitialDelay;

			public float showProgressBarDelay;

			public float showStaminaBarDelay;

			public float showHudElemenetsDelay;

			public float showActionButtonsDelay;
		}

		private Button _pauseButton;

		[SerializeField]
		private TextMeshProUGUI _pauseCounter;

		[SerializeField]
		private TextMeshProUGUI _pauseCounterShadow;

		[SerializeField]
		private float _pauseCounterOffsetY;

		[SerializeField]
		private ActionButton _jumpActionButton;

		[SerializeField]
		private ActionButton _slideActionButton;

		[SerializeField]
		private Transform _staminaContener;

		[SerializeField]
		private CanvasGroup _staminaCoverCG;

		[SerializeField]
		private CanvasGroup _coinsCG;

		[SerializeField]
		private CanvasGroup _pauseCG;

		[SerializeField]
		private CanvasGroup _abilityCG;

		[SerializeField]
		private SlideButton _slide;

		[SerializeField]
		private Animator _staminaCover;

		[SerializeField]
		private StaminaBarBehaviour staminaBar;

		[SerializeField]
		private MissionGoalInterstitial _missionInterstitial;

		[SerializeField]
		private TutorialScreen _tutorialScreen;

		[SerializeField]
		private ShowHudSettings _showHudSettings;

		private LevelTutorialSettings levelTutorialSettings;

		private float _currentTimescale;

		private bool isLevelFinished;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.IngameHUD;
			}
		}

		protected override void Setup()
		{
			_pauseButton = GetUIComponent<Button>("IngameHUD/Pause/Button");
			_pauseButton.onClick.AddListener(OnPausePressed);
			_pauseCounterShadow.gameObject.SetActive(false);
			Contexts.sharedInstance.game.CreateEntity().AddTutorialsListener(this);
			Contexts.sharedInstance.game.CreateEntity().AddRaccoonIntrosListener(this);
			Contexts.sharedInstance.game.CreateEntity().AddLevelFinishedListener(this);
		}

		private void OnPausePressed()
		{
			if (!isLevelFinished)
			{
				GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
				if (!worldEntity.isPaused)
				{
					worldEntity.isPaused = true;
				}
			}
		}

		public void ResumeGame()
		{
			PositionNumbers();
			StartCoroutine(Countdown());
		}

		private void PositionNumbers()
		{
			GameObject gameObject = Contexts.sharedInstance.game.playerEntity.view.gameObject;
			Vector3 vector = Camera.main.WorldToViewportPoint(gameObject.transform.position);
			Vector2 anchoredPosition = new Vector2((vector.x - 0.5f) * rectTransform.rect.size.x, (vector.y - 0.5f) * rectTransform.rect.size.y);
			anchoredPosition.y += _pauseCounterOffsetY;
			_pauseCounterShadow.rectTransform.anchoredPosition = anchoredPosition;
		}

		private IEnumerator Countdown()
		{
			_pauseCounterShadow.gameObject.SetActive(true);
			TextMeshProUGUI pauseCounterShadow = _pauseCounterShadow;
			string text = "3";
			_pauseCounter.text = text;
			pauseCounterShadow.text = text;
			yield return new WaitForSecondsRealtime(0.5f);
			TextMeshProUGUI pauseCounterShadow2 = _pauseCounterShadow;
			text = "2";
			_pauseCounter.text = text;
			pauseCounterShadow2.text = text;
			yield return new WaitForSecondsRealtime(0.5f);
			TextMeshProUGUI pauseCounterShadow3 = _pauseCounterShadow;
			text = "1";
			_pauseCounter.text = text;
			pauseCounterShadow3.text = text;
			yield return new WaitForSecondsRealtime(0.5f);
			_pauseCounterShadow.gameObject.SetActive(false);
			GameEntity world = Contexts.sharedInstance.game.worldEntity;
			if (!_slide.isPressed)
			{
				_slide.ForceStopSlide();
			}
			world.isPaused = false;
		}

		public void OnTutorialAction(TutorialStep tutorialStepAction)
		{
			switch (tutorialStepAction)
			{
			case TutorialStep.ShowJumpButton:
				_jumpActionButton.Show();
				break;
			case TutorialStep.ShowSlideButton:
				_slideActionButton.Show();
				break;
			case TutorialStep.ShowStaminaBar:
				ShowStaminaBar();
				break;
			case TutorialStep.StaminaPotion:
				EnlargeStaminaBar();
				break;
			case TutorialStep.StaminaStopPulsate:
				StopStaminaBarPulsation();
				break;
			case TutorialStep.HitObstacle:
				EnlargeStaminaBar();
				ShowStaminaHitText();
				break;
			case TutorialStep.ShowStaminaBarFinished:
			case TutorialStep.HitObstacleFinished:
			case TutorialStep.StaminaPotionFinished:
				SetStaminaBarBackToNormal();
				break;
			case (TutorialStep)7:
			case (TutorialStep)8:
			case TutorialStep.SlideEnd:
			case TutorialStep.ShowStaminaDrainText:
			case TutorialStep.ShowStaminaPotionText:
				break;
			}
		}

		public void OnRaccoonIntro()
		{
			levelTutorialSettings = Contexts.sharedInstance.game.worldEntity.levelSettings.levelTutorialSettings;
			RaccoonIntroComponent raccoonIntro = Contexts.sharedInstance.game.playerEntity.raccoonIntro;
			if (raccoonIntro != null && raccoonIntro.Step == RaccoonIntroStep.HUDHidden)
			{
				HideHud();
				StartCoroutine(ShowHudSequence());
			}
		}

		public void OnLevelFinished()
		{
			isLevelFinished = true;
		}

		public void UpdateStaminaUsingUnscaledTime()
		{
			staminaBar.UseUnscaledTime = true;
		}

		public void UpdateStaminaUsingScaledTime()
		{
			staminaBar.UseUnscaledTime = false;
		}

		public Transform GetJumpButton()
		{
			return _jumpActionButton.transform.GetChild(0);
		}

		public Transform GetSlideButton()
		{
			return _slideActionButton.transform.GetChild(0);
		}

		public void HideButKeepInteractive()
		{
			_jumpActionButton.Hide();
			_slideActionButton.Hide();
			HideElement(_staminaCoverCG, true);
			HideElement(_coinsCG, true);
			HideElement(_pauseCG, true);
			HideElement(_abilityCG);
			_missionInterstitial.missionProgressBar.Hide();
		}

		private void HideHud()
		{
			_jumpActionButton.Hide();
			_slideActionButton.Hide();
			HideElement(_staminaCoverCG);
			HideElement(_coinsCG);
			HideElement(_pauseCG);
			HideElement(_abilityCG);
		}

		private IEnumerator ShowHudSequence()
		{
			if (levelTutorialSettings.HudSettings.MissionGoal)
			{
				if (!levelTutorialSettings.LongerMissionGoalPopup)
				{
					yield return new WaitForSeconds(_showHudSettings.initialDelay);
					_missionInterstitial.Show();
					yield return new WaitForSeconds(_showHudSettings.hideInterstitialDelay);
					_missionInterstitial.Hide();
				}
				else
				{
					TutorialSettingsSO tutorialSettings = SettingsService.GetSetting<TutorialSettingsSO>();
					yield return new WaitForSeconds(tutorialSettings.goalInitialDelay);
					_missionInterstitial.Show();
					_missionInterstitial.Pulsate();
					GameEntity world = Contexts.sharedInstance.game.worldEntity;
					world.ReplaceChangeScaleTime(tutorialSettings.goalSlowDownFactor, tutorialSettings.goalSlowDownDuration, tutorialSettings.hideInterstitialDelay - tutorialSettings.goalSlowDownDuration, 0f);
					yield return new WaitForSeconds(tutorialSettings.hideInterstitialDelay);
					_missionInterstitial.Hide();
				}
			}
			if (levelTutorialSettings.HudSettings.StaminaBar)
			{
				yield return new WaitForSeconds(_showHudSettings.showStaminaBarDelay);
				_staminaContener.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack).From();
				ShowElement(_staminaCoverCG, false);
			}
			if (levelTutorialSettings.HudSettings.CoinsCounter || levelTutorialSettings.HudSettings.PauseButton || levelTutorialSettings.CharacterAbilityEnabeld)
			{
				yield return new WaitForSeconds(_showHudSettings.showHudElemenetsDelay);
				if (levelTutorialSettings.HudSettings.CoinsCounter)
				{
					ShowElement(_coinsCG);
				}
				if (levelTutorialSettings.HudSettings.PauseButton)
				{
					ShowElement(_pauseCG, true, 0.2f);
				}
				if (levelTutorialSettings.CharacterAbilityEnabeld)
				{
					ShowElement(_abilityCG, true, 0.2f);
				}
			}
			if (levelTutorialSettings.HudSettings.JumpButton || levelTutorialSettings.HudSettings.SlideButton)
			{
				yield return new WaitForSeconds(_showHudSettings.showActionButtonsDelay);
				if (levelTutorialSettings.HudSettings.JumpButton)
				{
					_jumpActionButton.Show();
				}
				if (levelTutorialSettings.HudSettings.SlideButton)
				{
					_slideActionButton.Show(0.1f);
				}
			}
		}

		private void HideElement(CanvasGroup cg, bool isInteractive = false)
		{
			cg.alpha = 0f;
			cg.blocksRaycasts = isInteractive;
		}

		private void ShowElement(CanvasGroup cg, bool animate = true, float delay = 0f)
		{
			cg.alpha = 1f;
			cg.blocksRaycasts = true;
			if (animate)
			{
				cg.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack).From()
					.SetDelay(delay);
			}
		}

		private void ShowStaminaHitText()
		{
			_tutorialScreen.ShowOverlay();
			base.uiContext.OpenDialog<TutorialScreen>(false);
			DOVirtual.DelayedCall(SettingsService.GetSetting<TutorialSettingsSO>().HitTutorialDuration, delegate
			{
				base.uiContext.CloseDialog(base.uiContext.GetDialog<TutorialScreen>());
			});
		}

		private void ShowStaminaBar()
		{
			ShowElement(_staminaCoverCG, false);
			_staminaCover.SetTrigger("Show");
		}

		private void SetStaminaBarBackToNormal()
		{
			_staminaCover.SetTrigger("BackToNormal");
		}

		private void EnlargeStaminaBar()
		{
			_staminaCover.SetTrigger("Enlarge");
		}

		private void StopStaminaBarPulsation()
		{
			_staminaCover.SetTrigger("StopPulsate");
		}
	}
}
