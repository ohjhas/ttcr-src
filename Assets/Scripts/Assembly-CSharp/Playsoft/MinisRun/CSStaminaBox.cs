using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CSStaminaBox : MonoBehaviour
	{
		[SerializeField]
		private Image Fill;

		[SerializeField]
		private Image Outline;

		public void SetState(float fillAmount, float outlineAmount)
		{
			Fill.fillAmount = fillAmount;
			Outline.rectTransform.anchorMin = new Vector2(fillAmount, 0f);
			Outline.rectTransform.anchorMax = new Vector2(1f + fillAmount, 1f);
			Outline.fillAmount = outlineAmount;
		}
	}
}
