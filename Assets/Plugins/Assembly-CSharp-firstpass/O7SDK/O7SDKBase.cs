namespace O7SDK
{
	public abstract class O7SDKBase
	{
		public static bool O7SDK_LOG;

		protected O7SDKSettings sdkSettings;

		protected O7SDKBase()
		{
			sdkSettings = O7SDKSettings.GetSettings();
			O7SDK_LOG = sdkSettings.build.logging;
		}
	}
}
