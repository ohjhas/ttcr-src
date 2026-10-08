using UnityEngine;

namespace Playsoft.MinisRun
{
	public static class AspectRatioHelper
	{
		private const float LOW_ASPECT_RATIO = 1.333333f;

		public static bool IsLowAspectRatio()
		{
			return (float)Screen.width / (float)Screen.height < 1.433333f;
		}
	}
}
