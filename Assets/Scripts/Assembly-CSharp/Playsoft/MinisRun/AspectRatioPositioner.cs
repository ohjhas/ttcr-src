using UnityEngine;

namespace Playsoft.MinisRun
{
	[ExecuteInEditMode]
	public class AspectRatioPositioner : MonoBehaviour
	{
		[SerializeField]
		private RectTransform RT;

		[SerializeField]
		private Vector2 widerRatioPosition;

		[SerializeField]
		private Vector2 thinnerRatioPosition;

		[SerializeField]
		private float aspectRatioThreshold;

		private float lastAspectRatio;

		private void Start()
		{
			Update();
		}

		private void Update()
		{
			float num = (float)Screen.width / (float)Screen.height;
			if (!Mathf.Approximately(num, lastAspectRatio))
			{
				if (num >= aspectRatioThreshold)
				{
					RT.anchoredPosition = widerRatioPosition;
				}
				else
				{
					RT.anchoredPosition = thinnerRatioPosition;
				}
				lastAspectRatio = num;
			}
		}
	}
}
