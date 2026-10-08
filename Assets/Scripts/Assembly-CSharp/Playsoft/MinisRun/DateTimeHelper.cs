using System;
using System.Globalization;

namespace Playsoft.MinisRun
{
	public static class DateTimeHelper
	{
		public const string DATETIME_FORMAT = "yyyy-MM-ddTHH:mm:ssZ";

		public static DateTime ConvertToDateTime(string value)
		{
			return DateTime.SpecifyKind(DateTime.ParseExact(value, "yyyy-MM-ddTHH:mm:ssZ", CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal), DateTimeKind.Utc);
		}

		public static string ConvertToString(DateTime value)
		{
			return value.ToString("yyyy-MM-ddTHH:mm:ssZ");
		}

		public static DateTime UnixTimestampToDateTime(long unixTime)
		{
			DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			long num = unixTime * 10000000;
			return new DateTime(dateTime.Ticks + num, DateTimeKind.Utc);
		}

		public static long DateTimeToUnixTimestamp(DateTime dateTime)
		{
			DateTime dateTime2 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			long ticks = (dateTime.ToUniversalTime() - dateTime2).Ticks;
			return ticks / 10000000;
		}
	}
}
