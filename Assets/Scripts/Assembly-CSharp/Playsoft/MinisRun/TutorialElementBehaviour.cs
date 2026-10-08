using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[RequireComponent(typeof(HighlightWithPulsating))]
	public abstract class TutorialElementBehaviour : MonoBehaviour
	{
		[Serializable]
		public class HighlightSettings
		{
			public float animSpeed = 1f;

			public float minScale = 1f;

			public float maxScale = 1.05f;

			public float period = 1f;

			public Material higlighMaterial;

			public bool shouldHiglight = true;

			public bool shouldPulsate = true;
		}

		[SerializeField]
		protected GameObject tutorialHandPrefab;

		[SerializeField]
		protected HighlightSettings highlightSettings;

		private bool isHiglighted;

		protected HighlightWithPulsating HighlightEffect { get; private set; }

		protected GameObject TutorialHand { get; private set; }

		protected virtual void Awake()
		{
			HighlightEffect = GetComponent<HighlightWithPulsating>();
			HighlightEffect.Disable();
		}

		protected virtual void Update()
		{
			if (ShouldHighlight())
			{
				if (!isHiglighted)
				{
					isHiglighted = true;
					EnableHiglightElement();
				}
			}
			else if (isHiglighted)
			{
				isHiglighted = false;
				DisableHiglightElement();
			}
		}

		protected virtual bool ShouldHighlight()
		{
			return false;
		}

		protected void EnableHiglightElement()
		{
			HighlightEffect.Init(highlightSettings.minScale, highlightSettings.maxScale, highlightSettings.animSpeed, highlightSettings.higlighMaterial, highlightSettings.period, highlightSettings.shouldHiglight, highlightSettings.shouldPulsate);
		}

		protected void DisableHiglightElement()
		{
			HighlightEffect.Disable();
		}

		protected void ShowHand(RectTransform target, float angle)
		{
			if (!(null == tutorialHandPrefab) && !(null == target))
			{
				TutorialHand = UnityEngine.Object.Instantiate(tutorialHandPrefab);
				TutorialHand.transform.SetParent(base.transform.parent);
				TutorialHand.transform.SetSiblingIndex(base.transform.GetSiblingIndex() + 1);
				TutorialHand.transform.localScale = Vector3.one;
				TutorialHand.transform.localPosition = Vector3.zero;
				TutorialHand.transform.position = target.TransformPoint(target.rect.center);
				TutorialHand.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
			}
		}

		protected void HideHand()
		{
			if (!(null == TutorialHand))
			{
				UnityEngine.Object.Destroy(TutorialHand);
			}
		}
	}
}
