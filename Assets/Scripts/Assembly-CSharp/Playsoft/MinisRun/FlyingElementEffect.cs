using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class FlyingElementEffect : MonoBehaviour
	{
		private float uiEffecDelay = 0.1f;

		public GameObject ElementPrefab { get; set; }

		public RectTransform StartPoint { get; set; }

		public RectTransform EndPoint { get; set; }

		public int ElementsAmount { get; set; }

		public float EffectDuration { get; set; }

		public Action Callback { get; set; }

		public void StartEffect()
		{
			if (!base.gameObject.activeInHierarchy)
			{
				Debug.LogWarning("[FlyingEffect] Object is disabled", this);
			}
			else
			{
				StartCoroutine(EffectCoroutine());
			}
		}

		private IEnumerator EffectCoroutine()
		{
			if (null == ElementPrefab)
			{
				Debug.LogWarning("[FlyingEffect] Prefab was not defined", this);
				yield break;
			}
			if (null == StartPoint)
			{
				Debug.LogWarning("[FlyingEffect] Start point was not defined", this);
				yield break;
			}
			if (null == EndPoint)
			{
				Debug.LogWarning("[FlyingEffect] End point was not defined", this);
				yield break;
			}
			ElementsAmount = Mathf.Clamp(ElementsAmount, 1, 10);
			yield return new WaitForSecondsRealtime(0.3f);
			for (int i = 0; i < ElementsAmount; i++)
			{
				ExecutionEffect(StartPoint.transform.position, EndPoint);
				yield return new WaitForSecondsRealtime(uiEffecDelay);
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		private void ExecutionEffect(Vector3 startPositoin, RectTransform target)
		{
			GameObject uiEffecInstance = UnityEngine.Object.Instantiate(ElementPrefab);
			Vector3 endValue = target.TransformPoint(target.rect.center);
			uiEffecInstance.transform.position = startPositoin;
			uiEffecInstance.transform.SetParent(target);
			uiEffecInstance.transform.localScale = Vector3.one;
			uiEffecInstance.transform.SetAsFirstSibling();
			uiEffecInstance.transform.DOScale(0.5f, EffectDuration).From();
			uiEffecInstance.transform.DOMove(endValue, EffectDuration).OnComplete(delegate
			{
				if (Callback != null)
				{
					Callback();
				}
				UnityEngine.Object.Destroy(uiEffecInstance.gameObject);
			}).SetUpdate(true);
		}
	}
}
