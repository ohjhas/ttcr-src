using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class WSChapterGate : MonoBehaviour
	{
		public RectTransform GateRT;

		[SerializeField]
		private TextMeshProUGUI Title;

		[SerializeField]
		private TextMeshProUGUI Label;

		[SerializeField]
		private CanvasGroup ContainerCanvasGroup;

		[SerializeField]
		private CanvasParticleEmitter UnlockParticles;

		[SerializeField]
		private GameObject NotEnoughStarsButton;

		[SerializeField]
		private float fadeAnimDuration;

		private int requiredStars;

		private int collectedStars;

		private Action notEnoughStarsCallback;

		public void SetCollectedStars(int _collectedStars)
		{
			collectedStars = _collectedStars;
			Label.text = Mathf.Min(requiredStars, collectedStars) + "/" + requiredStars;
		}

		public void SetData(string name, int _requiredStars)
		{
			UnlockParticles.enabled = false;
			Title.text = name;
			requiredStars = _requiredStars;
			Label.text = Mathf.Min(requiredStars, collectedStars) + "/" + requiredStars;
			if (requiredStars == 0)
			{
				Title.enableWordWrapping = true;
				Title.rectTransform.anchorMin = new Vector2(0.52f, 0.4f);
				Title.rectTransform.anchorMax = new Vector2(0.85f, 0.6f);
			}
		}

		public void SetState(bool newChapterUnlocked, Action _notEnoughStarsCallback)
		{
			notEnoughStarsCallback = _notEnoughStarsCallback;
			if (newChapterUnlocked)
			{
				ContainerCanvasGroup.alpha = 1f;
				UnlockParticles.enabled = true;
				UnlockParticles.RestartEmitter();
				ContainerCanvasGroup.DOFade(0f, fadeAnimDuration);
				NotEnoughStarsButton.SetActive(false);
			}
			else
			{
				ContainerCanvasGroup.alpha = ((collectedStars < requiredStars) ? 1 : 0);
				NotEnoughStarsButton.SetActive(collectedStars < requiredStars);
			}
		}

		public void OnNotEnoughStarsButtonPressed()
		{
			if (notEnoughStarsCallback != null)
			{
				notEnoughStarsCallback();
			}
		}
	}
}
