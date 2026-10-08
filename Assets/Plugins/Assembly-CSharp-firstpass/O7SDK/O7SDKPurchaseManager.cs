using System;
using UnityEngine;

namespace O7SDK
{
	public class O7SDKPurchaseManager : O7SDKBase
	{
		private class O7SDKPurchaseDelegate : MonoBehaviour
		{
			public event Action<string> PurchaseSucceeded;

			public event Action<string> PurchaseFailed;

			public event Action<string> PurchasesRestored;

			public event Action ConfigurationSuccess;

			public event Action ConfigurationFail;

			private void OnPurchaseSucceeded(string jsonObject)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKPurchaseDelegate.OnPurchaseSucceeded({0})", jsonObject));
				}
				if (this.PurchaseSucceeded != null)
				{
					this.PurchaseSucceeded(jsonObject);
				}
			}

			private void OnPurchaseFailed(string jsonObject)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKPurchaseDelegate.OnPurchaseFailed({0})", jsonObject));
				}
				if (this.PurchaseFailed != null)
				{
					this.PurchaseFailed(jsonObject);
				}
			}

			private void OnPurchaseRestore(string jsonObject)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log(string.Format("[O7SDK] O7SDKPurchaseDelegate.OnPurchaseRestore({0})", jsonObject));
				}
				if (this.PurchasesRestored != null)
				{
					this.PurchasesRestored(jsonObject);
				}
			}

			private void OnConfigurationSuccess()
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("[O7SDK] O7SDKPurchaseDelegate.OnBillingAvailable");
				}
				if (this.ConfigurationSuccess != null)
				{
					this.ConfigurationSuccess();
				}
			}

			private void OnConfigurationFail()
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("[O7SDK] O7SDKPurchaseDelegate.OnConfigurationFailed");
				}
				if (this.ConfigurationFail != null)
				{
					this.ConfigurationFail();
				}
			}
		}

		private const string MARSHALING_GAME_OBJECT = "[O7SDKMarshalingPurchaseManager]";

		#if UNITY_ANDROID
		private readonly AndroidJavaObject o7SdkManager;
		#endif

		private readonly O7SDKPurchaseDelegate purchaseDelegate;

		public event Action<string> OnPurchaseSucceeded;

		public event Action<string> OnPurchaseFailed;

		public event Action<string> OnPurchasesRestored;

		public event Action<string> OnLoadedValidIAPProducts;

		public event Action OnConfigurationSuccess;

		public event Action OnConfigurationFailed;

		public O7SDKPurchaseManager()
		{
			#if UNITY_ANDROID
			o7SdkManager = new AndroidJavaObject("com.outfit7.talkingtomcandyrun.O7SDKPurchaseManager");
			#endif
			purchaseDelegate = O7SDKTypesMarshaling.GetMarshalingComponent<O7SDKPurchaseDelegate>("[O7SDKMarshalingPurchaseManager]");
			purchaseDelegate.PurchaseSucceeded += PurchaseSucceeded;
			purchaseDelegate.PurchaseFailed += PurchaseFailed;
			purchaseDelegate.PurchasesRestored += PurchasesRestored;
			purchaseDelegate.ConfigurationSuccess += ConfigurationSuccess;
			purchaseDelegate.ConfigurationFail += ConfigurationFailed;
		}

		~O7SDKPurchaseManager()
		{
			if (!(purchaseDelegate == null))
			{
				purchaseDelegate.PurchaseSucceeded -= PurchaseSucceeded;
				purchaseDelegate.PurchaseFailed -= PurchaseFailed;
				purchaseDelegate.PurchasesRestored -= PurchasesRestored;
				purchaseDelegate.ConfigurationSuccess -= ConfigurationSuccess;
				purchaseDelegate.ConfigurationFail -= ConfigurationFailed;
			}
		}

		private void PurchaseSucceeded(string jsonObject)
		{
			if (this.OnPurchaseSucceeded != null)
			{
				this.OnPurchaseSucceeded(jsonObject);
			}
		}

		private void PurchaseFailed(string jsonObject)
		{
			if (this.OnPurchaseFailed != null)
			{
				this.OnPurchaseFailed(jsonObject);
			}
		}

		private void PurchasesRestored(string jsonObject)
		{
			if (this.OnPurchasesRestored != null)
			{
				this.OnPurchasesRestored(jsonObject);
			}
		}

		private void LoadedValidIAPProducts(string jsonArray)
		{
			if (this.OnLoadedValidIAPProducts != null)
			{
				this.OnLoadedValidIAPProducts(jsonArray);
			}
		}

		private void ConfigurationSuccess()
		{
			if (this.OnConfigurationSuccess != null)
			{
				this.OnConfigurationSuccess();
			}
		}

		private void ConfigurationFailed()
		{
			if (this.OnConfigurationFailed != null)
			{
				this.OnConfigurationFailed();
			}
		}

		public bool IsPurchasingAvailable()
		{
			#if UNITY_ANDROID
			string str = o7SdkManager.CallStatic<string>("isBillingAvailable", new object[0]);
			#else
			string str = null;
			#endif
			bool flag = O7SDKTypesMarshaling.ToBoolean(str);
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKPurchaseManager.IsPurchasingAvailable() = {0}", flag));
			}
			return flag;
		}

		public void StartPurchasing(string productId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKPurchaseManager.StartPurchasing({0})", productId));
			}
			#if UNITY_ANDROID
			o7SdkManager.CallStatic("buyIAPProduct", productId);
			#endif
		}

		public void ConsumePurchase(string productId)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKPurchaseManager.ConsumePurchase({0})", productId));
			}
			#if UNITY_ANDROID
			o7SdkManager.CallStatic("consumeIAPProduct", productId);
			#endif
		}

		public void RestorePurchases()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7SDKPurchaseManager.RestorePurchases()");
			}
			#if UNITY_ANDROID
			o7SdkManager.CallStatic("showBanner");
			#endif
		}

		public void RequestStoreProductsList()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("[O7SDK] O7SDKPurchaseManager.RequestStoreProductsList()");
			}
		}

		public string GetValidIAPProducts()
		{
			string text = null;
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKPurchaseManager.GetValidIAPProducts() = {0}", text));
			}
			return text;
		}

		public string GetPrice(string productId)
		{
			#if UNITY_ANDROID
			string text = o7SdkManager.CallStatic<string>("getItemPrice", new object[1] { productId });
			#else
			string text = null;
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKPurchaseManager.GetPrice({0}) = {1}", productId, text));
			}
			return text;
		}

		public void CheckBillingAvailable(string listOfAllBillableItems)
		{
			#if UNITY_ANDROID
			o7SdkManager.CallStatic("checkBillingSupported", listOfAllBillableItems);
			#endif
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("[O7SDK] O7SDKPurchaseManager.CheckBillingAvailable() = {0}", listOfAllBillableItems));
			}
		}
	}
}
