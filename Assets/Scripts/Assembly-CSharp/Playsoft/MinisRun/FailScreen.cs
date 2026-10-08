using System;
using System.Collections;
using DG.Tweening;
using Entitas;
using TMPro;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class FailScreen : BaseDialog
	{
		[SerializeField]
		private ResultCounter coinsCounter;

		[SerializeField]
		private Transform charParent;

		[SerializeField]
		private Button replayButton;

		[SerializeField]
		private Button acceptButton;

		[SerializeField]
		private TextMeshProUGUI totalCoins;

		[SerializeField]
		private SkewTextExample labelTextEffect;

		[Space]
		[SerializeField]
		private float characterSize;

		private Animator animator;

		private RectTransform runningCharacter;

		private Animator runningCharacterAnimator;

		private LevelTutorialSettings levelSettings;

		private SettingsResultScreenSO settingsResultScreenSo;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.FailScreen;
			}
		}

		private StateContext stateContext
		{
			get
			{
				return Contexts.sharedInstance.state;
			}
		}

		protected override void Setup()
		{
			base.Setup();
			animator = GetComponent<Animator>();
			settingsResultScreenSo = SettingsService.GetSetting<SettingsResultScreenSO>();
			replayButton.onClick.RemoveAllListeners();
			acceptButton.onClick.RemoveAllListeners();
		}

		private void OnEnable()
		{
			ResultScreenAnimationEvents.OnAnimationFinished = OnAnimationFinished;
		}

		private void OnDisable()
		{
			ResultScreenAnimationEvents.OnAnimationFinished = null;
		}

		private void OnOKButtonPressed()
		{
			if (base.IsClickable)
			{
				Contexts.sharedInstance.game.worldEntity.isDead = true;
			}
		}

		private void OnReplayButton()
		{
			if (base.IsClickable)
			{
				if (CharacterHasEnergy())
				{
					ReplayLevel();
				}
				else
				{
					OpenOutOfEnergyPopup();
				}
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				levelSettings = Contexts.sharedInstance.game.worldEntity.levelSettings.levelTutorialSettings;
				animator.SetTrigger("PlayAnim");
				labelTextEffect.CurveText();
				CreateCharacters();
				totalCoins.outlineColor = Color.white;
				totalCoins.outlineWidth = 0.45f;
				UpdateCoinsCounter(AnimateCurrencyUpdateIfNeeded);
				if (!levelSettings.ResultSettings.Coins)
				{
					coinsCounter.gameObject.SetActive(false);
				}
				AddButtonsListeners();
				replayButton.transform.localScale = Vector3.zero;
				acceptButton.transform.localScale = Vector3.zero;
				replayButton.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true)
					.SetDelay(0.7f);
				acceptButton.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true)
					.SetDelay(0.7f);
			}
		}

		private bool CharacterHasEnergy()
		{
			int amount = stateContext.GetEntitiesWithName("FirstCharSelectData").SingleEntity().integer.Amount;
			int amount2 = stateContext.GetEntitiesWithName("CharLifesData" + amount).SingleEntity().integer.Amount;
			return amount2 > 0;
		}

		private void OpenOutOfEnergyPopup()
		{
			int amount = stateContext.GetEntitiesWithName("FirstCharSelectData").SingleEntity().integer.Amount;
			BaseDialog newDialog = base.uiContext.OpenDialog<CharOutOfEnergyPopup>();
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.AddSetup(newDialog);
			uIEntity.AddUICharSingleRequest(amount);
			uIEntity.isUITotalGemsRequest = true;
			uIEntity.AddUIActionFinishedCallback(ReplayLevel);
		}

		private void ReplayLevel()
		{
			Contexts.sharedInstance.game.worldEntity.isDead = true;
			Contexts.sharedInstance.game.worldEntity.isReplayLevel = true;
		}

		private void AnimateCurrencyUpdateIfNeeded()
		{
			int value = Contexts.sharedInstance.game.worldEntity.coins.Value;
			if (value > 0)
			{
				CurrencyAndExperienceHelper.OpenScreenForResultScreen(value, coinsCounter.GetComponent<RectTransform>());
			}
		}

		private void CreateCharacters()
		{
			if (runningCharacter != null)
			{
				UnityEngine.Object.Destroy(runningCharacter.gameObject);
			}
			int amount = Contexts.sharedInstance.state.GetEntitiesWithName("FirstCharSelectData").SingleEntity().integer.Amount;
			RectTransform characterUIAsset = Contexts.sharedInstance.uI.GetCharacterUIAsset(amount);
			runningCharacter = UnityEngine.Object.Instantiate(characterUIAsset);
			runningCharacter.SetParent(charParent);
			runningCharacter.localScale = Vector3.one;
			runningCharacter.pivot = new Vector2(0.5f, 0f);
			runningCharacter.anchorMin = new Vector2(0.5f, 0f);
			runningCharacter.anchorMax = new Vector2(0.5f, 1f);
			runningCharacter.sizeDelta = new Vector2(characterSize, 0f);
			runningCharacter.anchoredPosition3D = Vector3.zero;
			runningCharacterAnimator = runningCharacter.GetComponent<Animator>();
			StartCoroutine(PlayAnimationAfterDelay(amount, 0.4f));
		}

		private IEnumerator PlayAnimationAfterDelay(int charIndex, float delay)
		{
			yield return new WaitForSecondsRealtime(delay);
			CharacterAnimatorController animatorController = runningCharacterAnimator.GetComponent<CharacterAnimatorController>();
			if (animatorController != null)
			{
				animatorController.ForceAnimation(charIndex, CharAnimationType.RunAndDie);
			}
			else
			{
				runningCharacterAnimator.Play("UIRunAndDieAnim");
			}
		}

		private void OnAnimationFinished()
		{
		}

		private void UpdateCoinsCounter(Action callback = null)
		{
			GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
			AnimationCurve counterAnimCurve = settingsResultScreenSo.CounterAnimCurve;
			float counterAnimTime = settingsResultScreenSo.CounterAnimTime;
			int targetValue = (worldEntity.hasCoins ? worldEntity.coins.Value : 0);
			if (!levelSettings.ResultSettings.Coins)
			{
				if (callback != null)
				{
					callback();
				}
			}
			else
			{
				coinsCounter.Show(targetValue, counterAnimCurve, counterAnimTime, callback);
			}
		}

		private void AddButtonsListeners()
		{
			replayButton.onClick.AddListener(OnReplayButton);
			acceptButton.onClick.AddListener(OnOKButtonPressed);
		}
	}
}
