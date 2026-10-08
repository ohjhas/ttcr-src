using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public abstract class CRRewardView : MonoBehaviour
	{
		[Header("Base")]
		[SerializeField]
		protected CanvasGroup CounterCanvasGroup;

		[SerializeField]
		protected TextMeshProUGUI ReceivedLabel;

		[SerializeField]
		protected TextMeshProUGUI CounterLabel;

		[SerializeField]
		protected Transform CounterTransform;

		[Header("Animation")]
		[SerializeField]
		protected float flyingTime;

		[SerializeField]
		protected float countingTime;

		[SerializeField]
		protected float jumpStrength;

		[SerializeField]
		protected float punchStrength;

		protected Transform iconTransform;

		protected int targetAmount;

		protected int initialAmount;

		protected float currentAmount;

		private int prevAmount;

		private float timer;

		private float animFactor;

		private Vector3 initialIconPos;

		private int punchThreshold;

		private float punchDuration = 0.1f;

		private Tweener punchCounterTween;

		protected virtual float maxIconScale
		{
			get
			{
				return 0f;
			}
		}

		protected void Update()
		{
			if (currentAmount < (float)targetAmount)
			{
				timer += Time.unscaledDeltaTime;
				if (iconTransform.localPosition.sqrMagnitude > 0.01f)
				{
					animFactor = Mathf.Clamp01(timer / flyingTime);
					SetupIconPosAndScale(animFactor);
					return;
				}
				animFactor = Mathf.Clamp01((timer - flyingTime) / countingTime);
				SetCanvasColor(animFactor);
				SetCounterValue(animFactor);
				PunchSliderIfNeeded();
			}
		}

		public bool IsAnimationFinished()
		{
			return currentAmount >= (float)targetAmount;
		}

		public void ForceFinishAnimation()
		{
			timer = flyingTime + countingTime;
			SetupIconPosAndScale(1f);
			SetCanvasColor(1f);
			SetCounterValue(1f);
		}

		protected void SetupInitialIconPosAndScale(RectTransform chestPos)
		{
			iconTransform.position = chestPos.position;
			iconTransform.localScale = Vector3.zero;
			initialIconPos = iconTransform.localPosition;
		}

		protected void SetupFieldsAndOverlay(GeneratedReward reward, int ownedAmount)
		{
			timer = 0f;
			ownedAmount -= reward.Amount;
			currentAmount = (initialAmount = (prevAmount = ownedAmount));
			targetAmount = ownedAmount + reward.Amount;
			punchThreshold = Mathf.Max(1, Mathf.RoundToInt((float)reward.Amount / (countingTime / punchDuration)));
			ReceivedLabel.text = "+" + NumbersHelper.GetNumberSplitedBySpaces(reward.Amount);
			CounterCanvasGroup.alpha = 0f;
		}

		protected abstract void SetCounterValue(float animFactor);

		private void SetCanvasColor(float animFactor)
		{
			float alpha = Mathf.Clamp01(animFactor * 5f);
			CounterCanvasGroup.alpha = alpha;
		}

		private void SetupIconPosAndScale(float animFactor)
		{
			float num = Mathf.Abs(initialIconPos.y * jumpStrength);
			num *= Mathf.Sin((1f - animFactor) * (float)Math.PI) * animFactor;
			iconTransform.localPosition = Vector3.Lerp(initialIconPos, Vector3.zero, animFactor) + Vector3.up * num;
			iconTransform.localScale = Vector3.one * Mathf.Lerp(0f, maxIconScale, animFactor);
		}

		private void PunchSliderIfNeeded()
		{
			if ((int)currentAmount - prevAmount >= punchThreshold)
			{
				if (punchCounterTween != null)
				{
					punchCounterTween.Complete();
				}
				punchCounterTween = CounterTransform.DOPunchScale(Vector3.one * punchStrength, punchDuration, 1).SetUpdate(true);
				prevAmount = (int)currentAmount;
			}
		}
	}
}
