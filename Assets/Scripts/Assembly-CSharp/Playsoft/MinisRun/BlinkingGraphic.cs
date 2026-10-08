using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class BlinkingGraphic : MonoBehaviour
	{
		[SerializeField]
		private Graphic Graphic;

		[SerializeField]
		private float animDuration;

		[SerializeField]
		private Color minColor;

		[SerializeField]
		private Color maxColor;

		[SerializeField]
		private AnimationCurve animationCurve;

		private float timer;

		private void OnEnable()
		{
			timer = 0f;
		}

		private void Update()
		{
			timer += Time.deltaTime;
			if (timer >= animDuration)
			{
				timer -= animDuration;
			}
			float time = timer / animDuration;
			time = animationCurve.Evaluate(time);
			Graphic.color = Color.Lerp(minColor, maxColor, time);
		}
	}
}
