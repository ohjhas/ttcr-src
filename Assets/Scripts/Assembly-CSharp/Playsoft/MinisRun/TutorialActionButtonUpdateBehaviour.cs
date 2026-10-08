using System;
using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialActionButtonUpdateBehaviour : MonoBehaviour
	{
		[Serializable]
		private enum ActionButtonType
		{
			Jump = 0,
			Slide = 1
		}

		private const string CLIP_NAME = "clip";

		[SerializeField]
		private ActionButtonType buttonType;

		[SerializeField]
		private GameObject handObject;

		[SerializeField]
		private Animator animator;

		[SerializeField]
		private CanvasGroup canvasGroup;

		[SerializeField]
		private Transform tutorialArrowTransform;

		private bool canUpdate;

		private Transform buttonTransform;

		private Vector2 initialButtonScale;

		public void KillUpdate()
		{
			if (buttonTransform != null)
			{
				buttonTransform.localScale = initialButtonScale;
			}
			canUpdate = false;
			handObject.SetActive(false);
		}

		private void Start()
		{
			IngameHUD ingameHUD = Contexts.sharedInstance.uI.uIEntity.hUD.hud as IngameHUD;
			switch (buttonType)
			{
			case ActionButtonType.Jump:
				buttonTransform = ingameHUD.GetJumpButton();
				break;
			case ActionButtonType.Slide:
				buttonTransform = ingameHUD.GetSlideButton();
				break;
			}
			if (buttonTransform != null)
			{
				initialButtonScale = buttonTransform.localScale;
			}
		}

		private void OnEnable()
		{
			canvasGroup.alpha = 0f;
			canvasGroup.DOFade(1f, 0.5f).SetUpdate(true);
			animator.Play("clip");
			canUpdate = true;
			handObject.SetActive(true);
		}

		private void Update()
		{
			if (canUpdate)
			{
				buttonTransform.localScale = new Vector2(Mathf.Abs(tutorialArrowTransform.localScale.x), Mathf.Abs(tutorialArrowTransform.localScale.y));
			}
		}

		private void OnDisable()
		{
			if (buttonTransform != null)
			{
				buttonTransform.localScale = initialButtonScale;
			}
		}
	}
}
