using UnityEngine;
using UnityEngine.UI;

namespace SRF.UI
{
	[RequireComponent(typeof(CanvasScaler))]
	[AddComponentMenu("SRF/UI/Retina Scaler")]
	public class SRRetinaScaler : SRMonoBehaviour
	{
		[SerializeField]
		private int _thresholdDpi = 250;

		[SerializeField]
		private bool _disablePixelPerfect;

		public int ThresholdDpi
		{
			get
			{
				return _thresholdDpi;
			}
		}

		private void Start()
		{
			float dpi = Screen.dpi;
			if (!(dpi <= 0f) && dpi > (float)ThresholdDpi)
			{
				CanvasScaler component = GetComponent<CanvasScaler>();
				component.uiScaleMode = CanvasScaler.ScaleMode.ConstantPixelSize;
				component.scaleFactor = dpi / (float)ThresholdDpi;
				if (_disablePixelPerfect)
				{
					GetComponent<Canvas>().pixelPerfect = false;
				}
			}
		}
	}
}
