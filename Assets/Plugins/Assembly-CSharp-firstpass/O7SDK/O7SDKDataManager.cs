using UnityEngine;

namespace O7SDK
{
	public class O7SDKDataManager : O7SDKBase
	{
		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		#if UNITY_ANDROID
		private readonly AndroidJavaObject countryCodeManager;
		#endif

		public O7SDKDataManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKDataManager");
			#endif
			#if UNITY_ANDROID
			countryCodeManager = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKCountryCodeManager");
			#endif
		}

		public string GetOnDemandEventsList()
		{
			#if UNITY_ANDROID
			string text = o7SDKManagerAndroid.CallStatic<string>("getOnDemandEventsList", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetOnDemandEventsList() = {0}", text));
			}
			return text;
		}

		public string GetAdConfigurationParameters()
		{
			#if UNITY_ANDROID
			string text = o7SDKManagerAndroid.CallStatic<string>("getAdConfiguration", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetAdConfigurationParameters() = {0}", text));
			}
			return text;
		}

		public string GetAppConfiguration()
		{
			#if UNITY_ANDROID
			string text = o7SDKManagerAndroid.CallStatic<string>("getAppConfiguration", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetAppConfiguration() = {0}", text));
			}
			return text;
		}

		public string GetUpdateBanner()
		{
			#if UNITY_ANDROID
			string text = o7SDKManagerAndroid.CallStatic<string>("getUpdateBanner", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetUpdateBanner() = {0}", text));
			}
			return text;
		}

		public string GetCDNItemsConfiguration()
		{
			#if UNITY_ANDROID
			string text = o7SDKManagerAndroid.CallStatic<string>("getCDNItemsConfiguration", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetCDNItemsConfiguration() = {0}", text));
			}
			return text;
		}

		public string GetUDIDHash()
		{
			string text = "test";
			#if UNITY_ANDROID
			text = o7SDKManagerAndroid.CallStatic<string>("getUDIDHash", new object[0]);
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetUDIDHash() = {0}", text));
			}
			return text;
		}

		public string GetIAPuPacks()
		{
			#if UNITY_ANDROID
			string text = o7SDKManagerAndroid.CallStatic<string>("getIapuPacks", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetIAPuPacks() = {0}", text));
			}
			return text;
		}

		public string GetValidCountryCodes()
		{
			#if UNITY_ANDROID
			string text = countryCodeManager.CallStatic<string>("getValidCountryCodes", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetValidCountryCodes() = {0}", text));
			}
			return text;
		}

		public bool MustShowEPrivacy()
		{
			bool flag = false;
			#if UNITY_ANDROID
			string str = o7SDKManagerAndroid.CallStatic<string>("mustShowEPrivacy", new object[0]);
			#else
			string str = null;
			#endif
			flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.MustShowEPrivacy()"));
			}
			return flag;
		}

		public bool MustShowPrivo()
		{
			bool flag = false;
			#if UNITY_ANDROID
			string str = o7SDKManagerAndroid.CallStatic<string>("mustShowPrivo", new object[0]);
			#else
			string str = null;
			#endif
			flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.MustShowPrivo()"));
			}
			return true;
		}

		public string GetPrivacyURL(bool forceDefault)
		{
			string text = "https://outfit7.com/privacy/en";
			#if UNITY_ANDROID
			text = o7SDKManagerAndroid.CallStatic<string>("getPrivacyURL", new object[1] { forceDefault });
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKDataManager.GetPrivacyURL({0})", text));
			}
			return text;
		}
	}
}
