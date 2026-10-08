using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TutorialLayer : BaseUI
	{
		[SerializeField]
		private Image overlayImage;

		public Image OverlayImage
		{
			get
			{
				return overlayImage;
			}
		}
	}
}
