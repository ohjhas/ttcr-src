using System;
using UnityEngine;

namespace O7SDK
{
	public class O7SDKRestoreManager : O7SDKBase
	{
		public enum ForceOverwriteMode
		{
			None = 0,
			New = 1,
			Master = 2,
			Restored = 3
		}

		[Serializable]
		public struct RestoreRequestSuccessedResponse
		{
			public int httpCode;

			public string responseJson;

			public RestoreRequestSuccessedResponseData<T> GetResponseData<T>()
			{
				return (!string.IsNullOrEmpty(responseJson)) ? JsonUtility.FromJson<RestoreRequestSuccessedResponseData<T>>(responseJson) : default(RestoreRequestSuccessedResponseData<T>);
			}

			public T GetRemoteState<T>()
			{
				return (!string.IsNullOrEmpty(responseJson)) ? JsonUtility.FromJson<RestoreRequestSuccessedResponseData<T>>(responseJson).u : default(T);
			}
		}

		[Serializable]
		public struct RestoreRequestSuccessedResponseData<T>
		{
			public int responseCode;

			public long lastResponseTs;

			public T u;
		}

		[Serializable]
		public struct RestoreRequestFailedResponse
		{
			public int httpCode;

			public string error;
		}

		private class O7SDKRestoreHandler : MonoBehaviour
		{
			public event Action<string> GotRestoreRequestResponse;

			public event Action<string> RestoreRequestFailed;

			private void OnGotRestoreRequestResponse(string jsonObject)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKRestoreDelegate.GotRestoreRequestResponse({0})", jsonObject));
				}
				if (this.GotRestoreRequestResponse != null)
				{
					this.GotRestoreRequestResponse(jsonObject);
				}
			}

			private void OnRestoreRequestFailed(string jsonObject)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKRestoreDelegate.RestoreRequestFailed({0})", jsonObject));
				}
				if (this.RestoreRequestFailed != null)
				{
					this.RestoreRequestFailed(jsonObject);
				}
			}
		}

		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingRestoreManager]";

		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		private readonly O7SDKRestoreHandler restoreHandler;

		public event Action<RestoreRequestSuccessedResponse> OnRestoreRequestSuccessed;

		public event Action<RestoreRequestFailedResponse> OnRestoreRequestFailed;

		public O7SDKRestoreManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKUserStateManager");
			#endif
			restoreHandler = O7SDKTypesMarshaling.GetMarshalingComponent<O7SDKRestoreHandler>("[O7SDKMarshalingRestoreManager]");
			restoreHandler.GotRestoreRequestResponse += RestoreRequestSuccessed;
			restoreHandler.RestoreRequestFailed += RestoreRequestFailed;
		}

		~O7SDKRestoreManager()
		{
			if (!(restoreHandler == null))
			{
				restoreHandler.GotRestoreRequestResponse -= RestoreRequestSuccessed;
				restoreHandler.RestoreRequestFailed -= RestoreRequestFailed;
			}
		}

		private void RestoreRequestSuccessed(string jsonObject)
		{
			if (this.OnRestoreRequestSuccessed != null)
			{
				RestoreRequestSuccessedResponse obj = JsonUtility.FromJson<RestoreRequestSuccessedResponse>(jsonObject);
				this.OnRestoreRequestSuccessed(obj);
			}
		}

		private void RestoreRequestFailed(string jsonObject)
		{
			if (this.OnRestoreRequestFailed != null)
			{
				RestoreRequestFailedResponse obj = JsonUtility.FromJson<RestoreRequestFailedResponse>(jsonObject);
				this.OnRestoreRequestFailed(obj);
			}
		}

		public void RestoreStateForApp(ForceOverwriteMode forceOverwriteMode, string currentState)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKRestoreManager.RestoreStateForApp({0}, {1})", forceOverwriteMode, currentState));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("restoreStateForApp", ForceOverwriteModeToString(forceOverwriteMode), currentState);
			#endif
		}

		public string ForceOverwriteModeToString(ForceOverwriteMode forceOverwriteMode)
		{
			switch (forceOverwriteMode)
			{
			case ForceOverwriteMode.New:
				return "NEW";
			case ForceOverwriteMode.Master:
				return "MASTER";
			case ForceOverwriteMode.Restored:
				return "RESTORED";
			default:
				return "NONE";
			}
		}
	}
}
