using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class AgeGateYear : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI yearText;

		[SerializeField]
		private RectTransform textHolder;

		[SerializeField]
		private Transform upArrow;

		[SerializeField]
		private Transform downArrow;

		[SerializeField]
		private AnimationCurve scaleCurve;

		[SerializeField]
		private AnimationCurve yScaleCurve;

		private int year;

		public int Year
		{
			get
			{
				return year;
			}
			set
			{
				year = value;
				yearText.text = ((year != -1) ? year.ToString() : "----");
			}
		}

		public float Offset { get; set; }

		private void Update()
		{
			float num = Mathf.InverseLerp(upArrow.transform.position.y, downArrow.transform.position.y, base.transform.position.y);
			if (num > 0.5f)
			{
				textHolder.anchorMin = new Vector2(0.5f, 1f);
				textHolder.anchorMax = new Vector2(0.5f, 1f);
				textHolder.pivot = new Vector2(0.5f, 1f);
			}
			else
			{
				textHolder.anchorMin = new Vector2(0.5f, 0f);
				textHolder.anchorMax = new Vector2(0.5f, 0f);
				textHolder.pivot = new Vector2(0.5f, 0f);
			}
			textHolder.anchoredPosition = new Vector2(0f, (0.5f - num) * 45f);
			num = ((!(num > 0.5f)) ? num : (0.5f - (num - 0.5f)));
			float x = scaleCurve.Evaluate(num * 2f);
			textHolder.localScale = new Vector3(x, yScaleCurve.Evaluate(num * 2f), 1f);
		}
	}
}
