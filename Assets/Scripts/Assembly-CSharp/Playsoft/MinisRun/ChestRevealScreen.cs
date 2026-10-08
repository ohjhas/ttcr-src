using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ChestRevealScreen : BaseScreen
	{
		private static readonly Vector3 ChestIconInitialPos = new Vector3(0f, 0f, 0f);

		private static readonly Vector3 ChestIconInitialScale = new Vector3(-1.4f, 1.4f, 1.4f);

		private static readonly Vector3 ChestIconFinalPos = new Vector3(-410f, -100f, 0f);

		private static readonly Vector3 ChestIconFinalScale = new Vector3(-1f, 1f, 1f);

		[Header("Chest Reveal Screen")]
		[SerializeField]
		private GameObject RewardsContainer;

		[SerializeField]
		private TextMeshProUGUI RewardsLeftLabel;

		[SerializeField]
		private RectTransform ChestRectTransform;

		[SerializeField]
		private Image ChestIcon;

		[SerializeField]
		private Image[] GlowStripes;

		[SerializeField]
		private GameObject RewardsCounter;

		[SerializeField]
		private CanvasParticleEmitter ChestParticles;

		[SerializeField]
		private CanvasParticleEmitter Particles;

		[SerializeField]
		private CanvasGroup ParticlesCanvasGroup;

		[SerializeField]
		private GameObject chestShadow;

		[SerializeField]
		private GameObject EpicChestLight;

		[SerializeField]
		private GameObject LegendaryChestLight;

		[SerializeField]
		private GameObject OtherChestLight;

		[SerializeField]
		private Button TapButton;

		[SerializeField]
		private Image Flash;

		[SerializeField]
		private CRNewReward NewReward;

		[SerializeField]
		private CRCharReward CharReward;

		[SerializeField]
		private CRCurrencyReward CurrencyReward;

		[SerializeField]
		private CRCompReward CompReward;

		[SerializeField]
		private GameObject TutorialHand;

		private int ownedCoins;

		private int ownedGems;

		private ChestData chestData;

		private List<GeneratedReward> rewardsData;

		private List<CSCharacterData> charsData;

		private List<CSCompanionData> companionsData;

		private int currentRewardId;

		private bool isAnimationPlayed;

		private Tweener openingTween;

		private Tweener punchTween;

		private Tweener glowTweener;

		private Tweener unlockTween;

		private Tweener flashTweener;

		private bool isTutorialChest;

		private bool isScreenDisplayed;

		private bool particlesStarted;

		private Sequence _handSequence;

		private SettingsChestRevealSO animationSettings;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.ChestRevealScreen;
			}
		}

		private void OnEnable()
		{
			ChestRectTransform.localPosition = ChestIconInitialPos;
			ChestRectTransform.localScale = ChestIconInitialScale;
			chestShadow.SetActive(false);
			OtherChestLight.SetActive(false);
			EpicChestLight.SetActive(false);
			LegendaryChestLight.SetActive(false);
			RewardsContainer.SetActive(false);
			RewardsCounter.SetActive(false);
			for (int i = 0; i < GlowStripes.Length; i++)
			{
				GlowStripes[i].color = new Color(GlowStripes[i].color.r, GlowStripes[i].color.g, GlowStripes[i].color.b, 0f);
			}
		}

		private void OnDisable()
		{
			if (openingTween != null)
			{
				openingTween.Complete(true);
			}
			if (punchTween != null)
			{
				punchTween.Complete();
			}
			CharReward.DestroyCharCard();
			CompReward.DestroyCompCard();
		}

		private void OnDestroy()
		{
			TapButton.onClick.RemoveAllListeners();
			if (openingTween != null)
			{
				openingTween.Kill();
			}
			if (punchTween != null)
			{
				punchTween.Kill();
			}
		}

		protected override void Setup()
		{
			base.Setup();
			animationSettings = SettingsService.GetSetting<SettingsChestRevealSO>();
		}

		public override void SetData(UIEntity parameters)
		{
			if (isScreenDisplayed)
			{
				return;
			}
			isScreenDisplayed = true;
			base.SetData(parameters);
			DisactivateHand();
			CheckIfTutorialWasPlayed();
			if (parameters != null)
			{
				TapButton.onClick.RemoveAllListeners();
				TapButton.onClick.AddListener(OnTapButton);
				ownedCoins = parameters.uITotalCoinsResponse.Amount;
				ownedGems = parameters.uITotalGemsResponse.Amount;
				charsData = parameters.uICharListResponse.CharDataList;
				companionsData = parameters.uICompanionListResponse.CompDataList;
				if (parameters.hasUIChestRewardResponse)
				{
					rewardsData = parameters.uIChestRewardResponse.Rewards;
					chestData = parameters.uIChestRewardResponse.Chest;
					ChestIcon.sprite = SettingsService.AssetsSettingsSO.GetSprite(chestData.AssetName);
					isAnimationPlayed = false;
					RefreshScreen(0);
				}
				else
				{
					BaseDialog dialog = base.uiContext.GetDialog<ChestRevealScreen>();
					base.uiContext.CloseDialog(dialog);
				}
			}
		}

		private void CheckIfTutorialWasPlayed()
		{
			isTutorialChest = Contexts.sharedInstance.game != null && Contexts.sharedInstance.game.worldEntity != null && Contexts.sharedInstance.game.worldEntity.hasLevelSettings && Contexts.sharedInstance.game.worldEntity.levelSettings.levelTutorialSettings.ForceChestReward;
		}

		private void ActivateHandIfInTutorial()
		{
			if (isTutorialChest)
			{
				_handSequence = DOTween.Sequence().InsertCallback(3f, delegate
				{
					TutorialHand.SetActive(true);
				}).SetUpdate(true);
			}
		}

		private void DisactivateHand()
		{
			if (_handSequence != null)
			{
				_handSequence.Kill();
			}
			TutorialHand.SetActive(false);
		}

		private void OnTapButton()
		{
			if (!base.IsClickable)
			{
				return;
			}
			DisactivateHand();
			if (!isAnimationPlayed)
			{
				if (openingTween != null)
				{
					openingTween.Complete(true);
					if (isTutorialChest)
					{
						openingTween.Kill();
						AnimateChest();
					}
				}
				else
				{
					isAnimationPlayed = true;
					OnTapButton();
				}
				return;
			}
			if (IsRewardContainChar(rewardsData[currentRewardId]) && !CharReward.IsAnimationFinished())
			{
				CharReward.ForceFinishAnimation();
				ChestParticles.StopEmitter();
				ActivateHandIfInTutorial();
				return;
			}
			if (IsRewardContainCompanion(rewardsData[currentRewardId]) && !CompReward.IsAnimationFinished())
			{
				CompReward.ForceFinishAnimation();
				ChestParticles.StopEmitter();
				ActivateHandIfInTutorial();
				return;
			}
			if (IsRewardContainCurrency(rewardsData[currentRewardId]) && !CurrencyReward.IsAnimationFinished())
			{
				CurrencyReward.ForceFinishAnimation();
				ChestParticles.StopEmitter();
				ActivateHandIfInTutorial();
				return;
			}
			int num = currentRewardId + 1;
			if (num >= rewardsData.Count)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isChestRevealScreen = true;
				uIEntity.AddUIRewardCollected(rewardsData[currentRewardId]);
				uIEntity.isCloseScreen = true;
				ChestParticles.StopEmitter();
				Particles.StopEmitter();
				particlesStarted = false;
				isScreenDisplayed = false;
			}
			else
			{
				RefreshScreen(currentRewardId + 1);
			}
		}

		private void FadeScreen()
		{
			if (openingTween != null)
			{
				openingTween.Kill();
			}
			openingTween = ChestRectTransform.transform.DOShakeRotation(animationSettings.SingleChestShakeDuration, animationSettings.ChestShakeStrength, animationSettings.ChestShakeVibrato, animationSettings.ChestShakeRandomness).SetDelay(animationSettings.DelayBetweenChestShakes).SetLoops((!isTutorialChest) ? animationSettings.ChestShakeLoops : (-1))
				.OnComplete(AnimateChest)
				.SetUpdate(true);
		}

		private void AnimateChest()
		{
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("chest_open");
			ChestIcon.sprite = SettingsService.AssetsSettingsSO.GetSprite(chestData.AssetName + "_open");
			if (chestData.AssetName == "epicChest")
			{
				EpicChestLight.SetActive(true);
			}
			else if (chestData.AssetName == "legendaryChest")
			{
				LegendaryChestLight.SetActive(true);
			}
			else
			{
				OtherChestLight.SetActive(true);
			}
			openingTween = ChestRectTransform.DOAnchorPos(ChestIconFinalPos, animationSettings.ChestOpenDuration);
			Tweener t = ChestRectTransform.DOScale(ChestIconFinalScale, animationSettings.ChestOpenDuration);
			DOTween.Sequence().Append(openingTween).Join(t)
				.OnComplete(delegate
				{
					isAnimationPlayed = true;
					RefreshScreen(currentRewardId);
					chestShadow.SetActive(true);
				})
				.SetUpdate(true);
		}

		private void RefreshScreen(int _currentRewardId)
		{
			ActivateHandIfInTutorial();
			currentRewardId = _currentRewardId;
			if (!isAnimationPlayed)
			{
				FadeScreen();
			}
			else
			{
				OpenNextReward();
			}
		}

		private void ShakeChest()
		{
			if (punchTween != null)
			{
				punchTween.Complete();
			}
			punchTween = ChestRectTransform.transform.DOShakeRotation(0.2f, animationSettings.ChestShakeStrength, animationSettings.ChestShakeVibrato, animationSettings.ChestShakeRandomness).SetUpdate(true);
		}

		private void OpenNextReward()
		{
			CharReward.DestroyCharCard();
			CompReward.DestroyCompCard();
			RewardsContainer.SetActive(true);
			RewardsCounter.SetActive(true);
			ShowGlowStripes();
			ChestParticles.RestartEmitter();
			StartParticlesIfNeeded();
			ShakeChest();
			RewardsLeftLabel.text = (rewardsData.Count - 1 - currentRewardId).ToString();
			CheckCharPartsReward(rewardsData[currentRewardId]);
			CheckCompPartsReward(rewardsData[currentRewardId]);
			CheckCurrencyReward(rewardsData[currentRewardId]);
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("chest_item_appear");
			if (rewardsData.Count - 1 - currentRewardId <= 0)
			{
				OtherChestLight.SetActive(false);
				EpicChestLight.SetActive(false);
				LegendaryChestLight.SetActive(false);
			}
		}

		private void StartParticlesIfNeeded()
		{
			if (!particlesStarted && !(ParticlesCanvasGroup == null) && !(Particles == null))
			{
				particlesStarted = true;
				ParticlesCanvasGroup.alpha = 0f;
				ParticlesCanvasGroup.DOFade(1f, 5f).SetUpdate(true);
				Particles.StartEmitter();
			}
		}

		private void ShowGlowStripes()
		{
			for (int i = 0; i < GlowStripes.Length; i++)
			{
				if (GlowStripes[i].color.a < 0.75f && glowTweener == null)
				{
					glowTweener = GlowStripes[i].DOFade(0.75f, 1f).OnComplete(delegate
					{
						glowTweener = null;
					}).SetUpdate(true);
				}
			}
		}

		private bool IsRewardContainChar(GeneratedReward reward)
		{
			return reward.Type == ItemType.Character;
		}

		private void CheckCharPartsReward(GeneratedReward reward)
		{
			if (IsRewardContainChar(reward))
			{
				CharReward.gameObject.SetActive(true);
				CSCharacterData cSCharacterData = charsData.Find((CSCharacterData ch) => ch.Name.Equals(reward.OptionalName));
				CharReward.SetData(reward, cSCharacterData, ChestRectTransform);
				MakeFlash();
				NewReward.SetData(reward, cSCharacterData);
			}
			else
			{
				CharReward.gameObject.SetActive(false);
			}
		}

		private bool IsRewardContainCompanion(GeneratedReward reward)
		{
			return reward.Type == ItemType.Companion;
		}

		private void CheckCompPartsReward(GeneratedReward reward)
		{
			if (IsRewardContainCompanion(reward))
			{
				CompReward.gameObject.SetActive(true);
				CSCompanionData cSCompanionData = companionsData.Find((CSCompanionData comp) => comp.Name.Equals(reward.OptionalName));
				CompReward.SetData(reward, cSCompanionData, ChestRectTransform);
				MakeFlash();
				NewReward.SetData(reward, null, cSCompanionData);
			}
			else
			{
				CompReward.gameObject.SetActive(false);
			}
		}

		private bool IsRewardContainCurrency(GeneratedReward reward)
		{
			return reward.Type == ItemType.Currency;
		}

		private void CheckCurrencyReward(GeneratedReward reward)
		{
			if (IsRewardContainCurrency(reward))
			{
				CurrencyReward.gameObject.SetActive(true);
				int ownedAmount = ((rewardsData[currentRewardId].Index != ProjectConstants.SOFT_CURRENCY_ID) ? ownedGems : ownedCoins);
				CurrencyReward.SetData(reward, ownedAmount, ChestRectTransform);
				MakeFlash();
				NewReward.SetData(reward);
			}
			else
			{
				CurrencyReward.gameObject.SetActive(false);
			}
		}

		private void MakeFlash()
		{
			Flash.color = new Color(Flash.color.r, Flash.color.g, Flash.color.b, 0f);
			if (flashTweener != null)
			{
				flashTweener.Kill();
			}
			flashTweener = Flash.DOFade(animationSettings.FlashTransparency, animationSettings.FlashDuration * 0.5f).OnComplete(delegate
			{
				flashTweener = Flash.DOFade(0f, animationSettings.FlashDuration * 0.5f).SetUpdate(true);
			}).SetUpdate(true);
		}
	}
}
