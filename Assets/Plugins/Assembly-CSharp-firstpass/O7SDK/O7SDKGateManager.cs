using System;
using UnityEngine;

namespace O7SDK
{
	public class O7SDKGateManager : O7SDKBase
	{
		public enum Gender
		{
			Unknown = 0,
			Male = 1,
			Female = 2
		}

		private class O7SDKGateManagerDelegate : MonoBehaviour
		{
			public event Action<bool> O7SDKAgeGateStatusChanged;

			private void O7SDKGateManager_o7SDKAgeGateStatusChanged(string passedStr)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKGateManagerDelegate.O7SDKAgeGateStatusChanged({0})", passedStr));
				}
				if (this.O7SDKAgeGateStatusChanged != null)
				{
					this.O7SDKAgeGateStatusChanged(O7SDKTypesMarshaling.ToBoolean(passedStr));
				}
			}
		}

		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingGateManager]";

		#if UNITY_ANDROID
		private readonly AndroidJavaObject ageGateManagerObject;
		#endif

		#if UNITY_ANDROID
		private readonly AndroidJavaObject countryCodeManagerObject;
		#endif

		private readonly O7SDKGateManagerDelegate gateManagerDelegate;

		public event Action<bool> OnAgeGateStatusChanged;

		public O7SDKGateManager()
		{
			#if UNITY_ANDROID
			ageGateManagerObject = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKAgeGateManager");
			#endif
			#if UNITY_ANDROID
			countryCodeManagerObject = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKCountryCodeManager");
			#endif
			gateManagerDelegate = O7SDKTypesMarshaling.GetMarshalingComponent<O7SDKGateManagerDelegate>("[O7SDKMarshalingGateManager]");
			gateManagerDelegate.O7SDKAgeGateStatusChanged += AgeGateStatusChanged;
		}

		~O7SDKGateManager()
		{
			if (!(gateManagerDelegate == null))
			{
				gateManagerDelegate.O7SDKAgeGateStatusChanged -= AgeGateStatusChanged;
			}
		}

		private void AgeGateStatusChanged(bool isPassed)
		{
			if (this.OnAgeGateStatusChanged != null)
			{
				this.OnAgeGateStatusChanged(isPassed);
			}
		}

		public bool MustShowAgeGate()
		{
			#if UNITY_ANDROID
			string str = ageGateManagerObject.CallStatic<string>("mustShowAgeGate", new object[0]);
			#else
			string str = null;
			#endif
			bool flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.MustShowAgeGate() = {0}", flag));
			}
			return flag;
		}

		public bool MustShowGenderGate()
		{
			#if UNITY_ANDROID
			string str = ageGateManagerObject.CallStatic<string>("mustShowGenderGate", new object[0]);
			#else
			string str = null;
			#endif
			bool flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.MustShowGenderGate() = {0}", flag));
			}
			return flag;
		}

		public void SetUserGender(Gender userGender)
		{
			int num = (int)userGender;
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.SetUserGender({0})", userGender));
			}
			#if UNITY_ANDROID
			ageGateManagerObject.CallStatic("setUserGender", num.ToString());
			#endif
		}

		public void SetUserBirthYear(int usersBirthYear)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.SetUserBirthYear({0})", usersBirthYear));
			}
			#if UNITY_ANDROID
			ageGateManagerObject.CallStatic("setUserBirthYear", usersBirthYear.ToString());
			#endif
		}

		public bool DidUserPassAgeGate()
		{
			#if UNITY_ANDROID
			string str = ageGateManagerObject.CallStatic<string>("isAgeGatePassed", new object[0]);
			#else
			string str = null;
			#endif
			bool flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.DidUserPassAgeGate() = {0}", flag));
			}
			return flag;
		}

		public void SetCountryCodeOverride(string countryCode)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.SetCountryCodeOverride({0})", countryCode));
			}
			#if UNITY_ANDROID
			countryCodeManagerObject.CallStatic("saveCountryCodeAndSendEvent", countryCode);
			#endif
		}

		public string GetCurrentCountryCode()
		{
			#if UNITY_ANDROID
			string text = countryCodeManagerObject.CallStatic<string>("getCountryCode", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.GetCurrentCountryCode() = {0}", text));
			}
			return text;
		}

		public bool MustShowCountryCodeOverrideEnabled()
		{
			#if UNITY_ANDROID
			string str = countryCodeManagerObject.CallStatic<string>("isCountryCodeOverrideEnabled", new object[0]);
			#else
			string str = null;
			#endif
			bool flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKGateManager.MustShowCountryCodeOverrideEnabled() = {0}", flag));
			}
			return flag;
		}
	}
}
