using UnityEngine;

namespace O7SDK
{
	public class O7SDKEventManager : O7SDKBase
	{
		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SDKManagerAndroid;
		#endif

		public O7SDKEventManager()
		{
			#if UNITY_ANDROID
			o7SDKManagerAndroid = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKHandler");
			#endif
		}

		public void AddEventInAppPurchase(string productId, string productName, string currency, int quantity, int value)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventInAppPurchase({0}, {1}, {2}, {3}, {4})", productId, productName, currency, quantity, value));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addInAppPurchaseEvent", productId, productName, currency, quantity, value);
			#endif
		}

		public void AddEventAchievementComplete(string achievementId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventAchievementComplete({0})", achievementId));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addAchievementCompleteEvent", achievementId);
			#endif
		}

		public void AddEventSelectContent(string contentId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventSelectContent({0})", contentId));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addSelectContentEvent", contentId);
			#endif
		}

		public void AddEventTutorialBegin(string stepId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventTutorialBegin({0})", stepId));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addTutorialBeginEvent", stepId);
			#endif
		}

		public void AddEventTutorialComplete(string stepId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventTutorialComplete({0})", stepId));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addTutorialCompleteEvent", stepId);
			#endif
		}

		public void AddEventLevelUp(string levelId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventLevelUp({0})", levelId));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addLevelUpEvent", levelId);
			#endif
		}

		public void AddEventLevelComplete(string levelId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventLevelComplete({0})", levelId));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addLevelCompleteEvent", levelId);
			#endif
		}

		public void AddEventEarnVirtualCurrency(string currencyId, string gameplayId, int currencyChange, int currencyBalance)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventEarnVirtualCurrency({0}, {1}, {2}, {3})", currencyId, gameplayId, currencyChange, currencyBalance));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addEarnCurrencyEvent", currencyId, gameplayId, currencyChange, currencyBalance);
			#endif
		}

		public void AddEventSpendVirtualCurrency(string currencyId, string gameplayId, int currencyChange, int currencyBalance)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventSpendVirtualCurrency({0}, {1}, {2}, {3})", currencyId, gameplayId, currencyChange, currencyBalance));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addSpendCurrencyEvent", currencyId, gameplayId, currencyChange, currencyBalance);
			#endif
		}

		public void AddEvent(string eid, string gid, string p1 = null, string p2 = null, double? p3 = null, double? p4 = null, string p5 = null, double? res = null, string jsonData = null, bool isOnDemand = false)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEvent({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", eid, gid, p1, p2, p3, p4, p5, res, jsonData, isOnDemand));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addEvent", eid, gid, p1, p2, p3.ToStringOrNull(), p4.ToStringOrNull(), p5, res.ToStringOrNull(), jsonData, isOnDemand);
			#endif
		}

		public void AddEventAndSend(string eid, string gid, string p1 = null, string p2 = null, double? p3 = null, double? p4 = null, string p5 = null, double? res = null, string jsonData = null, bool isOnDemand = false)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKEventManager.AddEventAndSend({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9})", eid, gid, p1, p2, p3, p4, p5, res, jsonData, isOnDemand));
			}
			#if UNITY_ANDROID
			o7SDKManagerAndroid.CallStatic("addEventAndSend", eid, gid, p1, p2, p3.ToStringOrNull(), p4.ToStringOrNull(), p5, res.ToStringOrNull(), jsonData, isOnDemand);
			#endif
		}
	}
}
