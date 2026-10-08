using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class HorizontalShineEffect : MonoBehaviour
	{
		[SerializeField]
		private Image _image;

		[SerializeField]
		private Material _normalMaterial;

		[SerializeField]
		private Material _shineMaterial;

		private float _time;

		public void Show(float duration)
		{
			StartCoroutine(ShowCoroutine(duration));
		}

		private IEnumerator ShowCoroutine(float duration)
		{
			_image.material = _shineMaterial;
			_time = 0f;
			float amplutide = _image.material.GetFloat("_Amplitude");
			while (_time < duration)
			{
				_time += Time.unscaledDeltaTime;
				_image.material.SetFloat("UnscaledTime", _time / duration * amplutide);
				yield return null;
			}
			_image.material = _normalMaterial;
		}
	}
}
