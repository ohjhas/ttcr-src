using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class NotLinearProgressBar
	{
		public float firstThresholdFill = 1f / 3f;

		public float secondThresholdFill = 2f / 3f;

		public float thirdThresholdFill = 1f;

		public float GetFillAmount(float result, float firstThresholdValue, float secondThresholdValue, float thirdThresholdValue)
		{
			float num = 0f;
			if (result <= firstThresholdValue)
			{
				return result / firstThresholdValue * firstThresholdFill;
			}
			if (result <= secondThresholdValue)
			{
				result -= firstThresholdValue;
				secondThresholdValue -= firstThresholdValue;
				float num2 = secondThresholdFill - firstThresholdFill;
				return firstThresholdFill + result / secondThresholdValue * num2;
			}
			result -= secondThresholdValue;
			thirdThresholdValue -= secondThresholdValue;
			float num3 = thirdThresholdFill - secondThresholdFill;
			return secondThresholdFill + result / thirdThresholdValue * num3;
		}

		public float GetThreshold(int index)
		{
			float result;
			switch (index)
			{
			case 0:
				result = firstThresholdFill;
				break;
			case 1:
				result = secondThresholdFill;
				break;
			default:
				result = thirdThresholdFill;
				break;
			}
			return result;
		}
	}
}
