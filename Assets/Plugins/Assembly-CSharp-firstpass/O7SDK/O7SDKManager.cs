using System;
using UnityEngine;
namespace O7SDK
{
	public class O7SDKManager : O7SDKBase
	{
		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingManager]";

#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
#endif

		public string O7SdkVersion
		{
			get
			{
#if UNITY_ANDROID
				string text = o7SDKManagerAndroid.CallStatic<string>("getVersion", new object[0]);
#else
				string text = "0.0.0-stub";
#endif
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKManager.O7SDKVersion = {0}", text));
				}
				return text;
			}
		}

		public bool IsGridDownloaded
		{
			get
			{
				bool flag = true;
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKManager.isGridDownloaded = {0}", flag));
				}
				return flag;
			}
		}

		public bool IsReadyToLoadGame
		{
			get
			{
				bool flag = true;
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKManager.isReadyToLoadGame = {0}", flag));
				}
				return flag;
			}
		}

		public string O7AdSdkVersion
		{
			get
			{
				string text = null;
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKManager.O7AdSdkVersion = {0}", text));
				}
				return text;
			}
		}

		public string AppVersion
		{
			get
			{
				string version = Application.version;
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKManager.AppVersion = {0}", version));
				}
				return version;
			}
		}

		public event Action<bool> OnGridIsDownloaded;
		public event Action OnReadyToLoadGame;

		public O7SDKManager()
		{
#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKHandler");
#endif
			GridIsDownloaded(true);
			ReadyToLoadGame();
		}

		~O7SDKManager()
		{
		}

		private void GridIsDownloaded(bool isGridDownloaded)
		{
			if (this.OnGridIsDownloaded != null)
			{
				this.OnGridIsDownloaded(isGridDownloaded);
			}
		}

		private void ReadyToLoadGame()
		{
			if (this.OnReadyToLoadGame != null)
			{
				this.OnReadyToLoadGame();
			}
		}
	}
}
