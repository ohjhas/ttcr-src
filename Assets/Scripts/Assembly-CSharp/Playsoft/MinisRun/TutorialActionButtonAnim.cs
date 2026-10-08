using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TutorialActionButtonAnim : MonoBehaviour
	{
		[Serializable]
		private class PressFrame
		{
			public float pressStart;

			public float pressStop;
		}

		[Serializable]
		private enum ActionButtonType
		{
			Jump = 0,
			Slide = 1
		}

		[SerializeField]
		private Image ArrowImage;

		[SerializeField]
		private GameObject Hand;

		[SerializeField]
		private Animator HandAnimator;

		[Header("Scale")]
		[SerializeField]
		private ActionButtonType buttonType;

		[SerializeField]
		private float finalScale = 1.2f;

		[Header("Pulsate")]
		[SerializeField]
		private float numberOfPulsations;

		[SerializeField]
		private float singlePulsationDuration;

		[Header("Press")]
		[SerializeField]
		private List<PressFrame> pressFrames;

		[SerializeField]
		private bool useAnimLength;

		[SerializeField]
		private Sprite unpressedSprite;

		[SerializeField]
		private Sprite pressedSprite;

		private Transform buttonTrans;

		private int currentPulsation;

		private float timer;

		private Vector3 initialArrowScale;

		private Tweener arrowFadeTween;

		private Tweener actionButtonScaling;

		private Tweener arrowImageScaling;

		private void Awake()
		{
			ArrowImage.material = new Material(ArrowImage.material);
			initialArrowScale = ArrowImage.transform.localScale;
		}

		private void Start()
		{
			IngameHUD ingameHUD = Contexts.sharedInstance.uI.uIEntity.hUD.hud as IngameHUD;
			switch (buttonType)
			{
			case ActionButtonType.Jump:
				buttonTrans = ingameHUD.GetJumpButton();
				break;
			case ActionButtonType.Slide:
				buttonTrans = ingameHUD.GetSlideButton();
				break;
			}
		}

		private void Update()
		{
			if ((float)currentPulsation < numberOfPulsations)
			{
				Pulsate();
			}
			else
			{
				AnimatePress();
			}
		}

		private void OnDisable()
		{
			timer = 0f;
			ArrowImage.color = new Color(1f, 1f, 1f, 0f);
			ArrowImage.material.SetFloat("UnscaledTime", 0f);
			CompleteAllTweens();
		}

		public void ActionPerformed()
		{
			ArrowImage.enabled = false;
			if (actionButtonScaling != null)
			{
				actionButtonScaling.Kill();
			}
			if (arrowImageScaling != null)
			{
				arrowImageScaling.Kill();
			}
			float num = Mathf.InverseLerp(1f, finalScale, Mathf.Abs(ArrowImage.transform.localScale.x));
			actionButtonScaling = buttonTrans.DOScale(1f, num * singlePulsationDuration * 0.5f).SetUpdate(true);
			arrowImageScaling = ArrowImage.transform.DOScale(initialArrowScale, num * singlePulsationDuration * 0.5f).SetUpdate(true);
		}

		public void ResetAnimation()
		{
			currentPulsation = 0;
			timer = 0f;
			ArrowImage.material.SetFloat("UnscaledTime", 0f);
			ArrowImage.material.SetFloat("_Period", singlePulsationDuration);
			CompleteAllTweens();
			ArrowImage.enabled = true;
			ArrowImage.color = new Color(1f, 1f, 1f, 0f);
			arrowFadeTween = ArrowImage.DOFade(0.5f, singlePulsationDuration * 0.5f).SetUpdate(true);
			Hand.SetActive(false);
			ScaleButton();
		}

		private void Pulsate()
		{
			timer += Time.unscaledDeltaTime;
			ArrowImage.material.SetFloat("UnscaledTime", timer);
			if (timer >= singlePulsationDuration)
			{
				timer = 0f;
				currentPulsation++;
				if ((float)currentPulsation >= numberOfPulsations)
				{
					Hand.SetActive(true);
				}
			}
		}

		private void CompleteAllTweens()
		{
			if (arrowFadeTween != null)
			{
				arrowFadeTween.Complete();
			}
			if (actionButtonScaling != null)
			{
				actionButtonScaling.SetLoops(1);
				actionButtonScaling.Complete();
			}
			if (arrowImageScaling != null)
			{
				actionButtonScaling.SetLoops(1);
				arrowImageScaling.Complete();
			}
		}

		private void AnimatePress()
		{
			AnimatorStateInfo currentAnimatorStateInfo = HandAnimator.GetCurrentAnimatorStateInfo(0);
			float normalizedTime = currentAnimatorStateInfo.normalizedTime;
			normalizedTime *= ((!useAnimLength) ? 1f : currentAnimatorStateInfo.length);
			normalizedTime %= 1f;
			bool isPressed = false;
			for (int i = 0; i < pressFrames.Count; i++)
			{
				if (normalizedTime >= pressFrames[i].pressStart && normalizedTime <= pressFrames[i].pressStop)
				{
					isPressed = true;
					break;
				}
			}
			ChangeSpriteIfNeeded(isPressed);
		}

		private void ChangeSpriteIfNeeded(bool isPressed)
		{
			Sprite sprite = ((!isPressed) ? unpressedSprite : pressedSprite);
			if (sprite != ArrowImage.sprite)
			{
				ArrowImage.sprite = sprite;
			}
		}

		private void ScaleButton()
		{
			if (buttonTrans == null)
			{
				Start();
			}
			buttonTrans.localScale = Vector3.one;
			ArrowImage.transform.localScale = initialArrowScale;
			actionButtonScaling = buttonTrans.DOScale(finalScale, singlePulsationDuration * 0.5f).SetUpdate(true).SetLoops(-1, LoopType.Yoyo);
			arrowImageScaling = ArrowImage.transform.DOScale(initialArrowScale * finalScale, singlePulsationDuration * 0.5f).SetUpdate(true).SetLoops(-1, LoopType.Yoyo);
		}
	}
}
