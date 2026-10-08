using System;
using UnityEngine;

namespace O7SDK.O7ADSDK
{
	public class O7ADRewardAdManager : O7SDKBase
	{
		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingRewardsManager]";

		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		private readonly O7ADHandler rewardHandler;

		public event Action OnRewardAdLoadSuccess;

		public event Action<string> OnRewardAdLoadFail;

		public event Action OnRewardAdShowSuccess;

		public event Action OnRewardAdShowFail;

		public event Action OnRewardAdClosed;

		public event Action<string> OnRewardRecieved;

		public O7ADRewardAdManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKHandler");
			#endif
			rewardHandler = O7SDKTypesMarshaling.GetMarshalingComponent<O7ADHandler>("[O7SDKMarshalingRewardsManager]");
			rewardHandler.OnAdLoadSuccess += OnAdLoadSuccess;
			rewardHandler.OnAdLoadFail += OnAdLoadFail;
			rewardHandler.OnAdShowSuccess += OnAdShowSuccess;
			rewardHandler.OnAdShowFail += OnAdShowFail;
			rewardHandler.OnAdClosed += OnAdClosed;
		}

		~O7ADRewardAdManager()
		{
			if (!(rewardHandler == null))
			{
				rewardHandler.OnAdLoadSuccess -= OnAdLoadSuccess;
				rewardHandler.OnAdLoadFail -= OnAdLoadFail;
				rewardHandler.OnAdShowSuccess -= OnAdShowSuccess;
				rewardHandler.OnAdShowFail -= OnAdShowFail;
				rewardHandler.OnAdClosed -= OnAdClosed;
			}
		}

		public void ShowRewardsAd()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADRewardAdManager.ShowRewardsAd()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("showRewards");
			#endif
		}

		public void FetchRewardsAd()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7ADRewardAdManager.FetchRewardsAd()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("fetchRewards");
			#endif
		}

		public bool IsVideoLoaded()
		{
			#if UNITY_ANDROID
			bool flag = O7SDKTypesMarshaling.ToBoolean(o7SDKManagerAndroid.CallStatic<string>("isRewardVideoLoaded", new object[0]));
			#else
			bool flag = false;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7ADRewardAdManager.isVideoLoaded() = {0}", flag));
			}
			return flag;
		}

		private void OnAdShowSuccess()
		{
			if (this.OnRewardAdShowSuccess != null)
			{
				this.OnRewardAdShowSuccess();
			}
		}

		private void OnAdShowFail()
		{
			if (this.OnRewardAdShowFail != null)
			{
				this.OnRewardAdShowFail();
			}
		}

		private void OnAdLoadSuccess()
		{
			if (this.OnRewardAdLoadSuccess != null)
			{
				this.OnRewardAdLoadSuccess();
			}
		}

		private void OnAdLoadFail()
		{
			if (this.OnRewardAdLoadFail != null)
			{
				this.OnRewardAdLoadFail(string.Empty);
			}
		}

		private void OnAdClosed(bool isRewarded)
		{
			if (isRewarded && this.OnRewardRecieved != null)
			{
				this.OnRewardRecieved(string.Empty);
			}
			if (this.OnRewardAdClosed != null)
			{
				this.OnRewardAdClosed();
			}
		}
	}
}
