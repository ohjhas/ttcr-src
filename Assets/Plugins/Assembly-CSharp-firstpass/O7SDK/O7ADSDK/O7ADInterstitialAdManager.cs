using System;
using UnityEngine;

namespace O7SDK.O7ADSDK
{
	public class O7ADInterstitialAdManager : O7SDKBase
	{
		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingInsterstitialManager]";

		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		private readonly O7ADHandler interstitialHandler;

		public event Action OnInterstitialLoadSuccess;

		public event Action<string> OnInterstitialLoadFail;

		public event Action OnInterstitialShowSuccess;

		public event Action OnInterstitialShowFail;

		public event Action OnInterstitialClosed;

		public O7ADInterstitialAdManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKHandler");
			#endif
			interstitialHandler = O7SDKTypesMarshaling.GetMarshalingComponent<O7ADHandler>("[O7SDKMarshalingInsterstitialManager]");
			interstitialHandler.OnAdLoadSuccess += OnAdLoadSuccess;
			interstitialHandler.OnAdLoadFail += OnAdLoadFail;
			interstitialHandler.OnAdShowSuccess += OnAdShowSuccess;
			interstitialHandler.OnAdShowFail += OnAdShowFail;
			interstitialHandler.OnAdClosed += OnAdClosed;
		}

		~O7ADInterstitialAdManager()
		{
			if (!(interstitialHandler == null))
			{
				interstitialHandler.OnAdLoadSuccess -= OnAdLoadSuccess;
				interstitialHandler.OnAdLoadFail -= OnAdLoadFail;
				interstitialHandler.OnAdShowSuccess -= OnAdShowSuccess;
				interstitialHandler.OnAdShowFail -= OnAdShowFail;
				interstitialHandler.OnAdClosed -= OnAdClosed;
			}
		}

		public void ShowInterstitialAd()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADInterstitialAdManager.ShowInterstitialAd()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("showIntertitials");
			#endif
		}

		public void FetchInterstitialAd()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADInterstitialAdManager.FetchInterstitialAd()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("fetchInterstitials");
			#endif
		}

		public void ShowInterstitialAd(string fromTransition, string toTransition)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7ADInterstitialAdManager.ShowInterstitialAd({0}, {1})", fromTransition, toTransition));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("showInterstitials", fromTransition, toTransition);
			#endif
		}

		private void OnAdShowSuccess()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7AdInterstitialAdManager.OnAdShowSuccess()");
			}
			if (this.OnInterstitialShowSuccess != null)
			{
				this.OnInterstitialShowSuccess();
			}
		}

		private void OnAdShowFail()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7AdInterstitialAdManager.OnAdShowFail()");
			}
			if (this.OnInterstitialShowFail != null)
			{
				this.OnInterstitialShowFail();
			}
		}

		private void OnAdLoadSuccess()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7AdInterstitialAdManager.OnAdLoadSuccess()");
			}
			if (this.OnInterstitialLoadSuccess != null)
			{
				this.OnInterstitialLoadSuccess();
			}
		}

		private void OnAdLoadFail()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7AdInterstitialAdManager.OnAdLoadFail()");
			}
			if (this.OnInterstitialLoadFail != null)
			{
				this.OnInterstitialLoadFail(string.Empty);
			}
		}

		private void OnAdClosed(bool isRewarded)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7AdInterstitialAdManager.OnAdClosed()");
			}
			if (this.OnInterstitialClosed != null)
			{
				this.OnInterstitialClosed();
			}
		}
	}
}
