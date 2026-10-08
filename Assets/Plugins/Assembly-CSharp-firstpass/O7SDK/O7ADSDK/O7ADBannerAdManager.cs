using System;
using UnityEngine;

namespace O7SDK.O7ADSDK
{
	public class O7ADBannerAdManager : O7SDKBase
	{
		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingBannerManager]";

		public Action OnBannerLoadSuccess;

		public Action<string> OnBannerLoadFail;

		public Action OnBannerShowSuccess;

		public Action OnBannerShowFail;

		public Action OnBannerClosed;

		#if UNITY_ANDROID
		private AndroidJavaObject o7SDKManagerAndroid;
		#endif

		private readonly O7ADHandler bannerHandler;

		public O7ADBannerAdManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKHandler");
			#endif
			bannerHandler = O7SDKTypesMarshaling.GetMarshalingComponent<O7ADHandler>("[O7SDKMarshalingBannerManager]");
			bannerHandler.OnAdLoadSuccess += OnAdLoadSuccess;
			bannerHandler.OnAdLoadFail += OnAdLoadFail;
			bannerHandler.OnAdShowSuccess += OnAdShowSuccess;
			bannerHandler.OnAdShowFail += OnAdShowFail;
			bannerHandler.OnAdClosed += OnAdClosed;
		}

		~O7ADBannerAdManager()
		{
			if (!(bannerHandler == null))
			{
				bannerHandler.OnAdLoadSuccess -= OnAdLoadSuccess;
				bannerHandler.OnAdLoadFail -= OnAdLoadFail;
				bannerHandler.OnAdShowSuccess -= OnAdShowSuccess;
				bannerHandler.OnAdShowFail -= OnAdShowFail;
				bannerHandler.OnAdClosed -= OnAdClosed;
			}
		}

		public void LoadBannerAd()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADBannerAdManager.LoadBannerAd()");
			}
		}

		public void ShowBanner()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADBannerAdManager.ShowBanner()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("showBanner");
			#endif
		}

		public void SetBannerBottomPosition(bool isBottomPositioned)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7ADBannerAdManager.SetBannerBottomPosition({0})", isBottomPositioned));
			}
		}

		public void HideBanner()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADBannerAdManager.HideBanner()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("hideBanner");
			#endif
		}

		public void PauseBanner()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADBannerAdManager.PauseBanner()");
			}
		}

		public void ResumeBanner()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADBannerAdManager.ResumeBanner()");
			}
		}

		private void OnAdShowSuccess()
		{
			if (OnBannerShowSuccess != null)
			{
				OnBannerShowSuccess();
			}
		}

		private void OnAdShowFail()
		{
			if (OnBannerShowFail != null)
			{
				OnBannerShowFail();
			}
		}

		private void OnAdLoadSuccess()
		{
			if (OnBannerLoadSuccess != null)
			{
				OnBannerLoadSuccess();
			}
		}

		private void OnAdLoadFail()
		{
			if (OnBannerLoadFail != null)
			{
				OnBannerLoadFail(null);
			}
		}

		private void OnAdClosed(bool isRewarded)
		{
			if (OnBannerClosed != null)
			{
				OnBannerClosed();
			}
		}
	}
}
