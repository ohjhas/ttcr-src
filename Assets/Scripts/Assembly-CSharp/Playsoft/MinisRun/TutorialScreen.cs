using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialScreen : BaseScreen
	{
		[Header("Tutorial Screen")]
		[SerializeField]
		private CanvasGroup ScreenOverlay;

		[SerializeField]
		private GameObject CollectTutorial;

		[SerializeField]
		private GameObject SingleJumpTutorial;

		[SerializeField]
		private GameObject SlideTutorial;

		[SerializeField]
		private GameObject DoubleJumpTutorial;

		[SerializeField]
		private GameObject StaminaDrainTutorial;

		[SerializeField]
		private GameObject StaminaPotionTutorial;

		[SerializeField]
		private GameObject StaminaHitTutorial;

		[SerializeField]
		private TutorialActionButtonUpdateBehaviour currentButtonAnim;

		private Transform canvasObject;

		private Tweener fadeTweener;

		protected override void Awake()
		{
			rectTransform = base.transform as RectTransform;
			canvasObject = GetComponentInParent<Canvas>().transform;
			ScreenOverlay.alpha = 0f;
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
				if (playerEntity.hasTutorialPlayStep)
				{
					CollectTutorial.SetActive(playerEntity.tutorialPlayStep.Step == TutorialStep.ShowCollectText);
					if (CollectTutorial.activeInHierarchy)
					{
						currentButtonAnim = CollectTutorial.GetComponent<TutorialActionButtonUpdateBehaviour>();
					}
					SingleJumpTutorial.SetActive(playerEntity.tutorialPlayStep.Step == TutorialStep.SingleJump);
					if (SingleJumpTutorial.activeInHierarchy)
					{
						currentButtonAnim = SingleJumpTutorial.GetComponentInChildren<TutorialActionButtonUpdateBehaviour>();
					}
					SlideTutorial.SetActive(playerEntity.tutorialPlayStep.Step == TutorialStep.Slide);
					if (SlideTutorial.activeInHierarchy)
					{
						currentButtonAnim = SlideTutorial.GetComponent<TutorialActionButtonUpdateBehaviour>();
					}
					DoubleJumpTutorial.SetActive(playerEntity.tutorialPlayStep.Step == TutorialStep.DoubleJump);
					if (DoubleJumpTutorial.activeInHierarchy)
					{
						currentButtonAnim = DoubleJumpTutorial.GetComponent<TutorialActionButtonUpdateBehaviour>();
					}
					StaminaDrainTutorial.SetActive(playerEntity.tutorialPlayStep.Step == TutorialStep.ShowStaminaDrainText);
					if (StaminaDrainTutorial.activeInHierarchy)
					{
						currentButtonAnim = StaminaDrainTutorial.GetComponent<TutorialActionButtonUpdateBehaviour>();
					}
					StaminaPotionTutorial.SetActive(playerEntity.tutorialPlayStep.Step == TutorialStep.ShowStaminaPotionText);
					if (StaminaPotionTutorial.activeInHierarchy)
					{
						currentButtonAnim = StaminaPotionTutorial.GetComponent<TutorialActionButtonUpdateBehaviour>();
					}
					StaminaHitTutorial.SetActive(playerEntity.tutorialPlayStep.Step.Equals(TutorialStep.HitObstacle));
					if (StaminaHitTutorial.activeInHierarchy)
					{
						currentButtonAnim = StaminaHitTutorial.GetComponent<TutorialActionButtonUpdateBehaviour>();
					}
				}
				else
				{
					Debug.LogError("TutorialScreen shown when no tutorial is played");
				}
			}
			else
			{
				HideOverlay();
			}
		}

		public void ActionPerformed()
		{
			if (currentButtonAnim != null)
			{
				currentButtonAnim.KillUpdate();
			}
		}

		public void ShowOverlay()
		{
			if (fadeTweener != null && fadeTweener.IsPlaying())
			{
				fadeTweener.Kill();
			}
			ScreenOverlay.transform.SetParent(canvasObject);
			ScreenOverlay.transform.SetAsFirstSibling();
			ScreenOverlay.gameObject.SetActive(true);
			RectTransform component = ScreenOverlay.GetComponent<RectTransform>();
			component.anchoredPosition = Vector2.zero;
			component.sizeDelta = Vector2.zero;
			fadeTweener = ScreenOverlay.DOFade(1f, (1f - ScreenOverlay.alpha) * 0.3f).SetUpdate(UpdateType.Normal, true);
		}

		public void HideOverlay()
		{
			if (ScreenOverlay.gameObject.activeInHierarchy)
			{
				if (fadeTweener != null && fadeTweener.IsPlaying())
				{
					fadeTweener.Kill();
				}
				fadeTweener = ScreenOverlay.DOFade(0f, ScreenOverlay.alpha * 0.3f).OnComplete(delegate
				{
					ScreenOverlay.gameObject.SetActive(false);
				});
			}
		}
	}
}
