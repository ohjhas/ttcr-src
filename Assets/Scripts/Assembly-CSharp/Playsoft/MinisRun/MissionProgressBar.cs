using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class MissionProgressBar : MonoBehaviour, IMissionUpdateListener, ILevelFinishedListener
	{
		[SerializeField]
		private RectTransform barHolder;

		[SerializeField]
		private RectTransform bar;

		[SerializeField]
		private CanvasGroup canvasGroup;

		[SerializeField]
		private MissionProgressBarStar starPrefab;

		[SerializeField]
		private RectTransform starsHolder;

		[SerializeField]
		private Image fillImage;

		[SerializeField]
		private Image goalIconImage;

		[SerializeField]
		private AspectRatioScaleSwitch scaleSwitch;

		[SerializeField]
		private ShakeEffect shakeEffect;

		[SerializeField]
		private Image chest;

		[SerializeField]
		private CanvasParticleEmitter chestParticles;

		[Space]
		[Tooltip("Min visible value - to show always some progress")]
		private float minFillAmount;

		private float minFirstFillAmount;

		[SerializeField]
		private NotLinearProgressBar progressBarFactor;

		private readonly MissionProgressBarStar[] stars = new MissionProgressBarStar[3];

		private readonly int[] missionGoals = new int[3];

		private int lastGoalValue;

		private Tweener iconTween;

		private bool isStaminaSwitchIntoChestAnimationStarted;

		private bool isFirstFillShown;

		public Image GoalIconImage
		{
			get
			{
				return goalIconImage;
			}
		}

		private IEnumerator Start()
		{
			if (!Contexts.sharedInstance.game.hasWorld)
			{
				yield return null;
			}
			Contexts.sharedInstance.game.CreateEntity().AddMissionUpdateListener(this);
			Contexts.sharedInstance.game.CreateEntity().AddLevelFinishedListener(this);
			if (Contexts.sharedInstance.game.worldEntity.hasMissionThresholds)
			{
				SetMinimumFillAmount(Contexts.sharedInstance.game.worldEntity);
				CreateStars();
				SetGoalImage(Contexts.sharedInstance.game.worldEntity);
			}
			else
			{
				base.gameObject.SetActive(false);
			}
			SetChest();
		}

		private void CreateStars()
		{
			GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
			float width = starsHolder.rect.width;
			missionGoals[0] = worldEntity.missionThresholds.FirstStar;
			missionGoals[1] = worldEntity.missionThresholds.SecondStar;
			missionGoals[2] = worldEntity.missionThresholds.ThirdStar;
			for (int num = 2; num >= 0; num--)
			{
				stars[num] = Object.Instantiate(starPrefab, starsHolder);
				stars[num].transform.localScale = Vector3.one;
				float x = width * progressBarFactor.GetThreshold(num);
				stars[num].Init(missionGoals[num]);
				((RectTransform)stars[num].transform).anchoredPosition = new Vector3(x, 0f, 0f);
				scaleSwitch.AddNewElement(new AspectRatioScaleSwitch.ElementToScale
				{
					Target = stars[num].transform,
					NormalScale = 1f,
					LowerScale = 0.75f
				});
			}
			progressBarFactor.firstThresholdFill -= minFillAmount;
			progressBarFactor.secondThresholdFill -= minFillAmount;
			progressBarFactor.thirdThresholdFill -= minFillAmount;
			UpdateFillImage(0, missionGoals[0], missionGoals[1], missionGoals[2]);
		}

		private void SetMinimumFillAmount(GameEntity world)
		{
			minFillAmount = ((!world.hasMissionSpriteFillAmount) ? 0f : world.missionSpriteFillAmount.FillAmount);
			minFirstFillAmount = ((!world.hasMissionSpriteFillAmount) ? 0f : world.missionSpriteFillAmount.FirstFillAmount);
		}

		private void SetGoalImage(GameEntity world)
		{
			if (world.hasMissionSprite)
			{
				goalIconImage.gameObject.SetActive(true);
				goalIconImage.sprite = world.missionSprite.Sprite;
				goalIconImage.transform.localScale = Vector3.one * world.missionSprite.Size;
			}
			else
			{
				goalIconImage.gameObject.SetActive(false);
			}
		}

		private void SetChest()
		{
			Sprite chestSprite = ChestHelper.GetChestSprite();
			if (chestSprite != null)
			{
				chest.sprite = chestSprite;
			}
			chest.transform.localScale = Vector2.zero;
		}

		private void UpdateFillImage(int value, int firstThreshold, int secondThreshold, int thirdThreshold)
		{
			if (!isFirstFillShown && value > 0)
			{
				isFirstFillShown = true;
				minFillAmount += minFirstFillAmount;
			}
			float fillAmount = progressBarFactor.GetFillAmount(value, firstThreshold, secondThreshold, thirdThreshold);
			fillAmount = minFillAmount + fillAmount;
			DOTween.To(delegate(float x)
			{
				fillImage.fillAmount = x;
			}, fillImage.fillAmount, fillAmount, 0.3f);
		}

		public void Hide()
		{
			canvasGroup.alpha = 0f;
		}

		public void Show()
		{
			canvasGroup.alpha = 1f;
			bar.localScale = new Vector2(0f, 1f);
			bar.DOScale(new Vector2(1f, 1f), 0.5f).SetEase(Ease.OutBack);
		}

		public void ShowWithoutAnimation()
		{
			canvasGroup.alpha = 1f;
		}

		public void ShowWithAnimation()
		{
			barHolder.gameObject.SetActive(true);
			barHolder.localScale = Vector2.zero;
			barHolder.position = new Vector2(0f, 52f);
			barHolder.anchoredPosition3D = new Vector3(barHolder.anchoredPosition3D.x, barHolder.anchoredPosition3D.y, 0f);
			barHolder.DOScale(1.5f, 1f);
			barHolder.DOScale(1.4f, 0.5f).SetDelay(1f).SetLoops(8, LoopType.Yoyo)
				.SetEase(Ease.Linear);
			barHolder.DOAnchorPos3D(Vector3.zero, 0.7f).SetDelay(5f);
			barHolder.DOScale(Vector2.one, 0.7f).SetDelay(5f);
		}

		public void OnMissionUpdate(int value, int firstThreshold, int secondThreshold, int thirdThreshold)
		{
			if (value == lastGoalValue)
			{
				return;
			}
			lastGoalValue = value;
			UpdateFillImage(value, firstThreshold, secondThreshold, thirdThreshold);
			for (int i = 0; i < 3; i++)
			{
				if (value >= missionGoals[i])
				{
					stars[i].SetReached();
				}
				else
				{
					stars[i].SetNotReached();
				}
			}
		}

		public void OnLevelFinished()
		{
		}

		private void SwitchMissionProgressBarIntoChestIfPossible()
		{
			if (isStaminaSwitchIntoChestAnimationStarted)
			{
				return;
			}
			LevelTutorialSettings levelTutorialSettings = Contexts.sharedInstance.game.worldEntity.levelSettings.levelTutorialSettings;
			if (levelTutorialSettings == null)
			{
				return;
			}
			bool flag = IsStaminaLevel();
			bool forceChestReward = levelTutorialSettings.ForceChestReward;
			bool flag2 = lastGoalValue > missionGoals[2];
			bool flag3 = HasLevelFinished();
			if ((!flag && (!forceChestReward || flag2)) || flag3)
			{
				int chestIdIfAvailable = ChestHelper.GetChestIdIfAvailable(flag2, levelTutorialSettings.ForceChestReward);
				if (chestIdIfAvailable != -1)
				{
					isStaminaSwitchIntoChestAnimationStarted = true;
					SwitchMissionProgressBarIntoChest();
				}
			}
		}

		private bool IsStaminaLevel()
		{
			GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
			return worldEntity != null && worldEntity.hasMissionType && worldEntity.missionType.Type == MissionType.Have_Stamina;
		}

		private bool HasLevelFinished()
		{
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			return playerEntity.hasLevelFinished;
		}

		private void SwitchMissionProgressBarIntoChest()
		{
			shakeEffect.Play();
			SettingsResultScreenSO setting = SettingsService.GetSetting<SettingsResultScreenSO>();
			DOTween.Sequence().Append(base.transform.DOScale(0f, setting.MissionProgressBarOnThreeStarsAnimationScaleDownDuration).SetEase(setting.MissionProgressBarOnThreeStarsAnimationScaleDownAnimationCurve).OnComplete(delegate
			{
				shakeEffect.Stop();
			})).InsertCallback(setting.MissionProgressBarOnThreeStarsAnimationParticlesDelay, chestParticles.RestartEmitter)
				.Insert(setting.MissionProgressBarOnThreeStarsAnimationScaleUpDelay, chest.transform.DOScale(1f, setting.MissionProgressBarOnThreeStarsAnimationScaleUpDuration).SetEase(setting.MissionProgressBarOnThreeStarsAnimationScaleUpAnimationCurve));
		}
	}
}
