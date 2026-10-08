using System;
using UnityEngine;

namespace O7SDK
{
	public class O7SDKCrossPromoManager : O7SDKBase
	{
		private class O7CrossPromoHandler : MonoBehaviour
		{
			public Action<bool> onManualNewsReadyAction;

			public Action<bool> onAutoNewsReadyAction;

			public Action onAutoNewsClosedAction;

			public Action onManualNewsClosedAction;

			private void ManualNewsReady(string isReady)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKCrossPromoDelegate.Outfit7ManualNewsReady({0})", isReady));
				}
				if (onManualNewsReadyAction != null)
				{
					onManualNewsReadyAction(O7SDKTypesMarshaling.ToBoolean(isReady));
				}
			}

			private void AutoNewsReady(string isReady)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKCrossPromoDelegate.Outfit7AutoNewsReady({0})", isReady));
				}
				if (onAutoNewsReadyAction != null)
				{
					onAutoNewsReadyAction(O7SDKTypesMarshaling.ToBoolean(isReady));
				}
			}

			private void AutoNewsClosed()
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("[O7SDK] O7SDKCrossPromoDelegate.Outfit7AutoNewsClosed()");
				}
				if (onAutoNewsClosedAction != null)
				{
					onAutoNewsClosedAction();
				}
			}

			private void ManualNewsClosed()
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("[O7SDK] O7SDKCrossPromoDelegate.Outfit7ManualNewsClosed()");
				}
				if (onManualNewsClosedAction != null)
				{
					onManualNewsClosedAction();
				}
			}
		}

		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingCrossPromoManager]";

		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		private readonly O7CrossPromoHandler crossPromoHandler;

		public event Action<bool> OnManualNewsReady;

		public event Action<bool> OnAutoNewsReady;

		public event Action OnAutoNewsClosed;

		public event Action OnManualNewsClosed;

		public O7SDKCrossPromoManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKHandler");
			#endif
			crossPromoHandler = O7SDKTypesMarshaling.GetMarshalingComponent<O7CrossPromoHandler>("[O7SDKMarshalingCrossPromoManager]");
			O7CrossPromoHandler o7CrossPromoHandler = crossPromoHandler;
			o7CrossPromoHandler.onAutoNewsClosedAction = (Action)Delegate.Combine(o7CrossPromoHandler.onAutoNewsClosedAction, new Action(AutoNewsClosed));
			O7CrossPromoHandler o7CrossPromoHandler2 = crossPromoHandler;
			o7CrossPromoHandler2.onManualNewsClosedAction = (Action)Delegate.Combine(o7CrossPromoHandler2.onManualNewsClosedAction, new Action(ManualNewsClosed));
			O7CrossPromoHandler o7CrossPromoHandler3 = crossPromoHandler;
			o7CrossPromoHandler3.onAutoNewsReadyAction = (Action<bool>)Delegate.Combine(o7CrossPromoHandler3.onAutoNewsReadyAction, new Action<bool>(AutoNewsReady));
			O7CrossPromoHandler o7CrossPromoHandler4 = crossPromoHandler;
			o7CrossPromoHandler4.onManualNewsReadyAction = (Action<bool>)Delegate.Combine(o7CrossPromoHandler4.onManualNewsReadyAction, new Action<bool>(ManualNewsReady));
		}

		~O7SDKCrossPromoManager()
		{
			if (!(crossPromoHandler == null))
			{
				O7CrossPromoHandler o7CrossPromoHandler = crossPromoHandler;
				o7CrossPromoHandler.onAutoNewsClosedAction = (Action)Delegate.Remove(o7CrossPromoHandler.onAutoNewsClosedAction, new Action(AutoNewsClosed));
				O7CrossPromoHandler o7CrossPromoHandler2 = crossPromoHandler;
				o7CrossPromoHandler2.onManualNewsClosedAction = (Action)Delegate.Remove(o7CrossPromoHandler2.onManualNewsClosedAction, new Action(ManualNewsClosed));
				O7CrossPromoHandler o7CrossPromoHandler3 = crossPromoHandler;
				o7CrossPromoHandler3.onAutoNewsReadyAction = (Action<bool>)Delegate.Remove(o7CrossPromoHandler3.onAutoNewsReadyAction, new Action<bool>(AutoNewsReady));
				O7CrossPromoHandler o7CrossPromoHandler4 = crossPromoHandler;
				o7CrossPromoHandler4.onManualNewsReadyAction = (Action<bool>)Delegate.Remove(o7CrossPromoHandler4.onManualNewsReadyAction, new Action<bool>(ManualNewsReady));
			}
		}

		private void ManualNewsReady(bool isReady)
		{
			if (this.OnManualNewsReady != null)
			{
				this.OnManualNewsReady(isReady);
			}
		}

		private void AutoNewsReady(bool isReady)
		{
			if (this.OnAutoNewsReady != null)
			{
				this.OnAutoNewsReady(isReady);
			}
		}

		private void ManualNewsClosed()
		{
			if (this.OnManualNewsClosed != null)
			{
				this.OnManualNewsClosed();
			}
		}

		private void AutoNewsClosed()
		{
			if (this.OnAutoNewsClosed != null)
			{
				this.OnAutoNewsClosed();
			}
		}

		public string GetManaulNewsButtonPath()
		{
			#if UNITY_ANDROID
			string text = o7SDKManagerAndroid.CallStatic<string>("getManualNewsButtonPath", new object[0]);
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKCrossPromoManager.GetManaulNewsButtonPath() = {0}", text));
			}
			return text;
		}

		public void ShowAutoNews()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7SDKCrossPromoManager.ShowAutoNews()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("showAutoNews");
			#endif
		}

		public void ShowManualNews()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7SDKCrossPromoManager.ShowManualNews()");
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("showManualNews");
			#endif
		}

		public bool IsManualNewsReady()
		{
			#if UNITY_ANDROID
			bool flag = O7SDKTypesMarshaling.ToBoolean(o7SDKManagerAndroid.CallStatic<string>("isManualNewsReady", new object[0]));
			#else
			bool flag = false;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKCrossPromoManager.IsManualNewsReady() = {0}", flag));
			}
			return flag;
		}

		public bool IsAutoNewsReady()
		{
			#if UNITY_ANDROID
			bool flag = O7SDKTypesMarshaling.ToBoolean(o7SDKManagerAndroid.CallStatic<string>("isAutoNewsReady", new object[0]));
			#else
			bool flag = false;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKCrossPromoManager.IsAutoNewsReady() = {0}", flag));
			}
			return flag;
		}
	}
}
