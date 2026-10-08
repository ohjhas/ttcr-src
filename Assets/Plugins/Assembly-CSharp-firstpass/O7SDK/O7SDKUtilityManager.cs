using UnityEngine;

namespace O7SDK
{
	public class O7SDKUtilityManager : O7SDKBase
	{
		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		public O7SDKUtilityManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKUtilityManager");
			#endif
		}

		public void RequestRateThisAppPopup()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7SDKUtilityManager.RequestRateThisAppPopup()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("rateThisApp");
			#endif
		}

		public void DisableAdTracking(bool disable)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKUtilityManager.DisableAdTracking({0})", disable));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("setAdTrackingEnableStatus", disable.ToString());
			#endif
		}

		public bool IsAdTrackingDisabled()
		{
			#if UNITY_ANDROID
			string str = o7SDKManagerAndroid.CallStatic<string>("getAdTrackingEnabledStatus", new object[0]);
			#else
			string str = null;
			#endif
			bool flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKUtilityManager.IsAdTrackingDisabled() = {0}", flag));
			}
			return flag;
		}

		public void SaveAndSendStackTrace(string message, string stacktrace)
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("saveAndSendStackTrace", message, stacktrace);
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKUtilityManager.SaveAndSendStackTrace() = {0} , {1}", message, stacktrace));
			}
		}

		public void SendLogs(string recipientEmail, string subject, string body, string[] attachmentPaths, int requestCode)
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("sendLogs", recipientEmail, subject, body, attachmentPaths, requestCode);
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKUtilityManager.SendLogs() = {0} , {1} , {2} , {3} , {4} ", recipientEmail, subject, body, attachmentPaths, requestCode));
			}
		}

		public void ShowDebugConsole()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("showAdsDebugMenu");
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKUtilityManager.ShowDebugConsole()"));
			}
		}
	}
}
