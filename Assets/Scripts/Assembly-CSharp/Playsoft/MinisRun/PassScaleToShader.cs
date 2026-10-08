using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class PassScaleToShader : MonoBehaviour
	{
		[SerializeField]
		private Image Image;

		[SerializeField]
		private Transform ScaleHolder;

		[SerializeField]
		[Range(0f, 1f)]
		private float MaxValue = 0.25f;

		public float MinScale = 1f;

		public float MaxScale = 1.05f;

		public void Init()
		{
			Image = GetComponent<Image>();
			ScaleHolder = GetComponent<Transform>();
			MaxValue = 0.25f;
		}

		private void Update()
		{
			if (Image != null)
			{
				Image.material.SetFloat("UnscaledTime", MaxValue * 0.5f * NormalizeScale(ScaleHolder.localScale.x));
			}
		}

		private float NormalizeScale(float scale)
		{
			if (scale <= MinScale)
			{
				return 0f;
			}
			if (scale >= MaxScale)
			{
				return 1f;
			}
			return (scale - MinScale) / (MaxScale - MinScale);
		}
	}
}
