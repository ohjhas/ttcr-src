using System.Text;

namespace Playsoft.MinisRun
{
	public static class NumbersHelper
	{
		public static string GetNumberSplitedBySpaces(long number)
		{
			string text = number + string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < text.Length; i++)
			{
				if (i != 0 && (text.Length - i) % 3 == 0)
				{
					stringBuilder.Append(" ");
				}
				stringBuilder.Append(text[i]);
			}
			return stringBuilder.ToString();
		}
	}
}
