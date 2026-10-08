using System;
using UnityEngine;

namespace O7SDK
{
	public class O7ADVideoGalleryAdManager : O7SDKBase
	{
		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingVideoGalleryManager]";

		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		public event Action OnVideoGalleryLoaded;

		public event Action OnVideoGalleryClosed;

		public O7ADVideoGalleryAdManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKHandler");
			#endif
		}

		~O7ADVideoGalleryAdManager()
		{
		}

		private void VideoGalleryLoaded()
		{
			if (this.OnVideoGalleryLoaded != null)
			{
				this.OnVideoGalleryLoaded();
			}
		}

		private void VideoGalleryClosed()
		{
			if (this.OnVideoGalleryClosed != null)
			{
				this.OnVideoGalleryClosed();
			}
		}

		public bool IsVideoGalleryAvailable()
		{
			#if UNITY_ANDROID
			bool flag = O7SDKTypesMarshaling.ToBoolean(o7SDKManagerAndroid.CallStatic<string>("isVideoGalleryReady", new object[0]));
			#else
			bool flag = false;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7ADVideoGalleryManager.IsVideoGalleryAvailable() = {0}", flag));
			}
			return flag;
		}

		public void ShowVideoGallery(string sourceScreenName)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7ADVideoGalleryManager.ShowVideoGallery({0})", sourceScreenName));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("openVideoGallery");
			#endif
		}
	}
}
