using System.Runtime.InteropServices;
using UnityEngine;

namespace O7SDK
{
	public class O7SDKGdprManager : O7SDKBase
	{
		#if UNITY_ANDROID
		private readonly AndroidJavaObject gdprManagerObject;
		#endif

		public O7SDKGdprManager()
		{
			#if UNITY_ANDROID
			gdprManagerObject = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKGdprConsentManager");
			#endif
		}

		public int MustShowGdprConsent()
		{
			int num = -1;
			#if UNITY_ANDROID
			string str = gdprManagerObject.CallStatic<string>("mustShowGdprConsent", new object[0]);
			#else
			string str = null;
			#endif
			num = O7SDKTypesMarshaling.ToInt(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGdprManager.MustShowGdprConsent() = {0}", num));
			}
			return num;
		}

		public bool IsGdprConsentRequired()
		{
			bool flag = false;
			#if UNITY_ANDROID
			string str = gdprManagerObject.CallStatic<string>("isGdprConsentRequired", new object[0]);
			#else
			string str = null;
			#endif
			flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGdprManager.IsGdprConsentRequired() = {0}", flag));
			}
			return flag;
		}

		public string GetGdprConsentProviders(int mode)
		{
			string empty = string.Empty;
			#if UNITY_ANDROID
			empty = gdprManagerObject.CallStatic<string>("getGdprConsentProviders", new object[1] { mode.ToString() });
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGdprManager.GetGdprConsentProviders({0}) = {1}", mode, empty));
			}
			return empty;
		}

		public void SetGdprConsentProviders(string providers)
		{
			#if UNITY_ANDROID
			gdprManagerObject.CallStatic("setGdprConsentProviders", providers);
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGdprManager.SetGdprConsentProviders() = {0}", providers));
			}
		}

		public bool HaveConsentForProvider(string providerName)
		{
			bool flag = false;
			#if UNITY_ANDROID
			flag = O7SDKTypesMarshaling.ToBoolean(gdprManagerObject.CallStatic<string>("haveConsentForProvider", new object[0]));
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGdprManager.HaveConsentForProvider({0}) = {1}", providerName, flag));
			}
			return flag;
		}

		[DllImport("__Internal")]
		private static extern int O7SDKGateManager_mustShowGdprConsent();

		[DllImport("__Internal")]
		private static extern bool O7SDKGateManager_isGdprConsentRequired();

		[DllImport("__Internal")]
		private static extern string O7SDKGateManager_getGdprConsentProviders(int fromScreen);

		[DllImport("__Internal")]
		private static extern void O7SDKGateManager_setGdprConsentProviders(string providers);
	}
}
