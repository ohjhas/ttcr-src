using System;
using System.Collections;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ShakeEffect : MonoBehaviour
	{
		[SerializeField]
		private bool useRealTime;

		[SerializeField]
		private int loopsAmount;

		[SerializeField]
		private float duration;

		[SerializeField]
		private float offset;

		[SerializeField]
		private float maxAngle;

		[SerializeField]
		private int vibrationsAmount;

		[SerializeField]
		private bool isStrengthAscending;

		public void Play()
		{
			if (base.gameObject.activeInHierarchy)
			{
				StartCoroutine(Shake());
			}
		}

		public void Stop(bool shouldResetRotation = false)
		{
			StopAllCoroutines();
			if (shouldResetRotation)
			{
				base.transform.rotation = Quaternion.Euler(Vector3.zero);
			}
		}

		private IEnumerator Shake()
		{
			if (loopsAmount == -1)
			{
				while (true)
				{
					yield return StartCoroutine(VibratingShake());
				}
			}
			for (int i = 0; i < loopsAmount; i++)
			{
				yield return StartCoroutine(VibratingShake());
			}
		}

		private IEnumerator VibratingShake()
		{
			for (int i = 0; i < vibrationsAmount; i++)
			{
				float vibrationStrength = ((!isStrengthAscending) ? (vibrationsAmount - i) : (i + 1));
				vibrationStrength /= (float)vibrationsAmount;
				yield return StartCoroutine(SingleShake(vibrationStrength));
			}
			if (useRealTime)
			{
				yield return new WaitForSecondsRealtime(offset);
			}
			else
			{
				yield return new WaitForSeconds(offset);
			}
		}

		private IEnumerator SingleShake(float shakeStrength)
		{
			float timer = 0f;
			while (timer < duration)
			{
				timer += ((!useRealTime) ? Time.deltaTime : Time.unscaledDeltaTime);
				float x = Mathf.Min(timer / duration, 1f) * 2f * (float)Math.PI;
				Vector3 rotation = maxAngle * shakeStrength * Mathf.Sin(x) * Vector3.forward;
				base.transform.rotation = Quaternion.Euler(rotation);
				yield return null;
			}
		}
	}
}
