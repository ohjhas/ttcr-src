using System;
using System.Collections.Generic;
using O7SDK.O7ADSDK;
using UnityEngine;
using UnityEngine.UI;

namespace O7SDK.Examples
{
	public class O7SDKExamples : MonoBehaviour
	{
		[Serializable]
		private class AppState
		{
			public int coins;

			public int xp;

			public void Load()
			{
				coins = PlayerPrefs.GetInt("coins");
				xp = PlayerPrefs.GetInt("xp");
			}

			public void Randomize()
			{
				coins = UnityEngine.Random.Range(0, 100);
				xp = UnityEngine.Random.Range(0, 100);
			}

			public void Save()
			{
				PlayerPrefs.SetInt("coins", coins);
				PlayerPrefs.SetInt("xp", xp);
				PlayerPrefs.Save();
			}
		}

		[Serializable]
		private struct O7SDKDataManagerUI
		{
			public Button GetOnDemandEventsListButton;

			public Button GetAdConfigurationParametersButton;

			public Button GetAppConfigurationButton;

			public Button GetUpdateBannerButton;

			public Button GetCDNItemsConfigurationButton;

			public Button GetUDIDHashButton;

			public Button GetIAPuPacksButton;

			public Button GetValidCountryCodesButton;
		}

		[Serializable]
		private struct O7SDKEventManagerUI
		{
			public Button AddEventInAppPurchaseButton;

			public Button AddEventAchievementCompleteButton;

			public Button AddEventSelectContentButton;

			public Button AddEventTutorialBeginButton;

			public Button AddEventTutorialCompleteButton;

			public Button AddEventLevelUpButton;

			public Button AddEventLevelCompleteButton;

			public Button AddEventEarnVirtualCurrencyButton;

			public Button AddEventSpendVirtualCurrencyButton;

			public Button AddEventTest1Button;

			public Button AddEventTest2Button;
		}

		[Serializable]
		private struct O7ADBannerAdManagerUI
		{
			public Button LoadBannerAdButton;

			public Button AddBannerAdToViewButton;

			public Toggle SetBannerBottomPositionToggle;

			public Button HideBannerButton;

			public Button PauseBannerButton;

			public Button ResumeBannerButton;
		}

		[Serializable]
		private struct O7ADInterstitialAdManagerUI
		{
			public Button LoadInterstitialAdButton;

			public Button ShowInterstitialAdButton;

			public Button ShowTransitionInterstitialAdButton;
		}

		[Serializable]
		private struct O7ADRewardAdManagerUI
		{
			public Button LoadRewardAdButton;

			public Button ShowRewardAdButton;
		}

		[Serializable]
		private struct O7ADVideoGalleryAdManagerUI
		{
			public Button ShowVideoGalleryButton;
		}

		[Serializable]
		private struct O7SDKCrossPromoManagerUI
		{
			public Toggle IsAutoNewsReadyToggle;

			public Toggle IsManualNewsReadyToggle;

			public Button ShowAutoNewsButton;

			public Button ShowManualNewsButton;
		}

		[Serializable]
		private struct O7SDKPurchaseManagerUI
		{
			public Button GetValidIAPProductsButton;

			public Button RequestStoreProductsListButton;

			public Button RestorePurchasesButton;

			public GameObject StartPurchasingButtonPrefab;

			public LayoutGroup StartPurchasingButtonsLayoutGroup;
		}

		[Serializable]
		private struct O7SDKUtilityManagerUI
		{
			public Toggle AdTrackingDisabledToggle;

			public Button RequestRateThisAppPopupButton;
		}

		[Serializable]
		private struct O7SDKGateManagerUI
		{
			public Toggle MustShowAgeGateToggle;

			public Toggle MustShowGenderGateToggle;

			public Button SetUserGenderButton;

			public Button SetUserBirthYearButton;

			public Toggle DidUserPassAgeGateToggle;

			public Button SetCountryCodeOverrideButton;

			public Toggle MustShowCountryCodeOverrideEnabledToggle;
		}

		[Serializable]
		private struct O7SDKRestoreManagerUI
		{
			public Button AppStateLoadButton;

			public Button AppStateSaveButton;

			public Button AppStateRandomizeButton;

			public Button RestoreStateForAppNoneButton;

			public Button RestoreStateForAppNewButton;

			public Button RestoreStateForAppMasterButton;

			public Button RestoreStateForAppRestoredButton;
		}

		[Serializable]
		public class IapPacks
		{
			public string id;

			public long price;

			public List<Item> items;
		}

		[Serializable]
		public class Item
		{
			public string name;

			public long amount;
		}

		private static readonly Queue<string> countryCodeLoop = new Queue<string>();

		private static readonly Queue<O7SDKGateManager.Gender> userGenderLoop = new Queue<O7SDKGateManager.Gender>(new O7SDKGateManager.Gender[2]
		{
			O7SDKGateManager.Gender.Male,
			O7SDKGateManager.Gender.Female
		});

		private static readonly Queue<int> userAgeLoop = new Queue<int>(new int[7] { 3, 5, 7, 13, 18, 25, 35 });

		private readonly List<string> listOfIapsGameSupports = new List<string> { "com.outfit7.talkingtomcandyrun.diamonds.fistful", "com.outfit7.talkingtomcandyrun.diamonds.pouch", "com.outfit7.talkingtomcandyrun.diamonds.bucket" };

		private O7SDKManager sdkManager;

		private O7SDKEventManager eventManager;

		private O7SDKDataManager dataManager;

		private O7ADBannerAdManager adBannerAdManager;

		private O7ADInterstitialAdManager adInterstitialAdManager;

		private O7ADRewardAdManager adRewardAdManager;

		private O7ADVideoGalleryAdManager adVideoGalleryAdManager;

		private O7SDKCrossPromoManager crossPromoManager;

		private O7SDKPurchaseManager purchaseManager;

		private O7SDKUtilityManager utilityManager;

		private O7SDKGateManager gateManager;

		private O7SDKRestoreManager restoreManager;

		private AppState appState;

		[SerializeField]
		private Text logText;

		[SerializeField]
		private O7SDKDataManagerUI dataManagerUI;

		[SerializeField]
		private O7SDKEventManagerUI eventManagerUI;

		[SerializeField]
		private O7ADBannerAdManagerUI adBannerAdManagerUI;

		[SerializeField]
		private O7ADInterstitialAdManagerUI adInterstitialAdManagerUI;

		[SerializeField]
		private O7ADRewardAdManagerUI adRewardAdManagerUI;

		[SerializeField]
		private O7ADVideoGalleryAdManagerUI adVideoGalleryAdManagerUi;

		[SerializeField]
		private O7SDKCrossPromoManagerUI crossPromoManagerUI;

		[SerializeField]
		private O7SDKPurchaseManagerUI purchaseManagerUI;

		[SerializeField]
		private O7SDKUtilityManagerUI utilityManagerUI;

		[SerializeField]
		private O7SDKGateManagerUI gateManagerUI;

		[SerializeField]
		private O7SDKRestoreManagerUI restoreManagerUI;

		private void ToLog(string msg)
		{
			logText.text += string.Format(Environment.NewLine + "{0}: {1}", DateTime.Now.ToString("u"), msg);
		}

		private void Start()
		{
			sdkManager = new O7SDKManager();
			eventManager = new O7SDKEventManager();
			dataManager = new O7SDKDataManager();
			adBannerAdManager = new O7ADBannerAdManager();
			adInterstitialAdManager = new O7ADInterstitialAdManager();
			adRewardAdManager = new O7ADRewardAdManager();
			adVideoGalleryAdManager = new O7ADVideoGalleryAdManager();
			crossPromoManager = new O7SDKCrossPromoManager();
			purchaseManager = new O7SDKPurchaseManager();
			utilityManager = new O7SDKUtilityManager();
			gateManager = new O7SDKGateManager();
			restoreManager = new O7SDKRestoreManager();
			appState = new AppState();
			appState.Load();
			InitializeIAP();
			ToLog(string.Format("O7SDKManager.O7SdkVersion = {0}", sdkManager.O7SdkVersion));
			ToLog(string.Format("O7SDKManager.O7AdSdkVersion = {0}", sdkManager.O7AdSdkVersion));
			sdkManager.OnGridIsDownloaded += delegate(bool isDownloaded)
			{
				ToLog(string.Format("O7SDKManager.OnGridIsDownloaded({0})", isDownloaded));
			};
			sdkManager.OnReadyToLoadGame += delegate
			{
				ToLog("O7SDKManager.OnReadyToLoadGame()");
				purchaseManager.RequestStoreProductsList();
			};
			ToLog(string.Format("O7SDKManager.isReadyToLoadGame = {0}", sdkManager.IsReadyToLoadGame));
			if (sdkManager.IsReadyToLoadGame)
			{
				purchaseManager.RequestStoreProductsList();
			}
			ToLog(string.Format("O7SDKDataManager.GetUDIDHash() = {0}", dataManager.GetUDIDHash()));
			dataManagerUI.GetOnDemandEventsListButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetOnDemandEventsList() = {0}", dataManager.GetOnDemandEventsList()));
			});
			dataManagerUI.GetAdConfigurationParametersButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetAdConfigurationParameters() = {0}", dataManager.GetAdConfigurationParameters()));
			});
			dataManagerUI.GetAppConfigurationButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetAppConfiguration() = {0}", dataManager.GetAppConfiguration()));
			});
			dataManagerUI.GetUpdateBannerButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetUpdateBanner() = {0}", dataManager.GetUpdateBanner()));
			});
			dataManagerUI.GetCDNItemsConfigurationButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetCDNItemsConfiguration() = {0}", dataManager.GetCDNItemsConfiguration()));
			});
			dataManagerUI.GetUDIDHashButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetUDIDHash() = {0}", dataManager.GetUDIDHash()));
			});
			dataManagerUI.GetIAPuPacksButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetIAPuPacks() = {0}", dataManager.GetIAPuPacks()));
			});
			dataManagerUI.GetValidCountryCodesButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKDataManager.GetValidCountryCodes() = {0}", dataManager.GetValidCountryCodes()));
			});
			eventManagerUI.AddEventInAppPurchaseButton.onClick.AddListener(delegate
			{
				eventManager.AddEventInAppPurchase(Application.identifier + ".iap.coins", "Test coins IAP", "eur", 1, 1);
			});
			eventManagerUI.AddEventAchievementCompleteButton.onClick.AddListener(delegate
			{
				eventManager.AddEventAchievementComplete(Application.identifier + ".achievement.level." + UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventSelectContentButton.onClick.AddListener(delegate
			{
				eventManager.AddEventSelectContent(Application.identifier + ".content.id." + UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventTutorialBeginButton.onClick.AddListener(delegate
			{
				eventManager.AddEventTutorialBegin(Application.identifier + ".tutorial.id." + UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventTutorialCompleteButton.onClick.AddListener(delegate
			{
				eventManager.AddEventTutorialComplete(Application.identifier + ".tutorial.id." + UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventLevelUpButton.onClick.AddListener(delegate
			{
				eventManager.AddEventLevelUp(Application.identifier + ".level.id." + UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventLevelCompleteButton.onClick.AddListener(delegate
			{
				eventManager.AddEventLevelComplete(Application.identifier + ".level.id." + UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventEarnVirtualCurrencyButton.onClick.AddListener(delegate
			{
				eventManager.AddEventEarnVirtualCurrency(Application.identifier + ".gold", Application.identifier + ".boost", UnityEngine.Random.Range(1, 100), UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventSpendVirtualCurrencyButton.onClick.AddListener(delegate
			{
				eventManager.AddEventSpendVirtualCurrency(Application.identifier + ".gold", Application.identifier + ".loot", UnityEngine.Random.Range(1, 100), UnityEngine.Random.Range(1, 100));
			});
			eventManagerUI.AddEventTest1Button.onClick.AddListener(delegate
			{
				O7SDKEventManager o7SDKEventManager = eventManager;
				string eid = Application.identifier + ".event.eid1";
				string gid = Application.identifier + ".event.gid1";
				double? res = UnityEngine.Random.Range(1, 100);
				o7SDKEventManager.AddEvent(eid, gid, null, null, null, null, null, res);
			});
			eventManagerUI.AddEventTest2Button.onClick.AddListener(delegate
			{
				eventManager.AddEvent(Application.identifier + ".event.eid2", Application.identifier + ".event.gid2", "p1", "p2", UnityEngine.Random.Range(1, 100), UnityEngine.Random.Range(1, 100), "p5", UnityEngine.Random.Range(1, 100), string.Format("{{\"chapter\":{0},\"level\":{1}}}", UnityEngine.Random.Range(1, 100), UnityEngine.Random.Range(1, 100)));
			});
			O7ADBannerAdManager o7ADBannerAdManager = adBannerAdManager;
			o7ADBannerAdManager.OnBannerLoadSuccess = (Action)Delegate.Combine(o7ADBannerAdManager.OnBannerLoadSuccess, (Action)delegate
			{
				ToLog("O7ADBannerAdManager.BannerDidLoad");
			});
			O7ADBannerAdManager o7ADBannerAdManager2 = adBannerAdManager;
			o7ADBannerAdManager2.OnBannerLoadFail = (Action<string>)Delegate.Combine(o7ADBannerAdManager2.OnBannerLoadFail, (Action<string>)delegate(string error)
			{
				ToLog(string.Format("O7ADBannerAdManager.BannerFailedToLoadWithError({0})", error));
			});
			O7ADBannerAdManager o7ADBannerAdManager3 = adBannerAdManager;
			o7ADBannerAdManager3.OnBannerShowSuccess = (Action)Delegate.Combine(o7ADBannerAdManager3.OnBannerShowSuccess, (Action)delegate
			{
				ToLog("O7ADBannerAdManager.BannerWillAppear");
			});
			O7ADBannerAdManager o7ADBannerAdManager4 = adBannerAdManager;
			o7ADBannerAdManager4.OnBannerClosed = (Action)Delegate.Combine(o7ADBannerAdManager4.OnBannerClosed, (Action)delegate
			{
				ToLog("O7ADBannerAdManager.BannerDidDismissModalView");
			});
			adBannerAdManagerUI.LoadBannerAdButton.onClick.AddListener(delegate
			{
				adBannerAdManager.LoadBannerAd();
			});
			adBannerAdManagerUI.AddBannerAdToViewButton.onClick.AddListener(delegate
			{
				adBannerAdManager.ShowBanner();
			});
			adBannerAdManagerUI.SetBannerBottomPositionToggle.onValueChanged.AddListener(delegate(bool value)
			{
				adBannerAdManager.SetBannerBottomPosition(value);
			});
			adBannerAdManagerUI.HideBannerButton.onClick.AddListener(delegate
			{
				adBannerAdManager.HideBanner();
			});
			adBannerAdManagerUI.PauseBannerButton.onClick.AddListener(delegate
			{
				adBannerAdManager.PauseBanner();
			});
			adBannerAdManagerUI.ResumeBannerButton.onClick.AddListener(delegate
			{
				adBannerAdManager.ResumeBanner();
			});
			adInterstitialAdManager.OnInterstitialLoadSuccess += delegate
			{
				ToLog("O7ADInterstitialAdManager.OnInterstitialLoadSuccess");
			};
			adInterstitialAdManager.OnInterstitialLoadFail += delegate(string error)
			{
				ToLog(string.Format("O7ADInterstitialAdManager.OnInterstitialLoadFail({0})", error));
			};
			adInterstitialAdManager.OnInterstitialShowSuccess += delegate
			{
				ToLog("O7ADInterstitialAdManager.InterstitialShowSuccess");
			};
			adInterstitialAdManager.OnInterstitialShowFail += delegate
			{
				ToLog("O7ADInterstitialAdManager.OnInterstitialShowFail");
			};
			adInterstitialAdManager.OnInterstitialClosed += delegate
			{
				ToLog("O7ADInterstitialAdManager.InterstitialClosed");
			};
			adInterstitialAdManagerUI.LoadInterstitialAdButton.onClick.AddListener(delegate
			{
				adInterstitialAdManager.FetchInterstitialAd();
			});
			adInterstitialAdManagerUI.ShowInterstitialAdButton.onClick.AddListener(delegate
			{
				adInterstitialAdManager.ShowInterstitialAd();
			});
			adInterstitialAdManagerUI.ShowTransitionInterstitialAdButton.onClick.AddListener(delegate
			{
				adInterstitialAdManager.ShowInterstitialAd("*", "HomeState");
			});
			adRewardAdManager.OnRewardAdLoadSuccess += delegate
			{
				ToLog("O7ADRewardAdManager.OnRewardAdLoadSuccess");
			};
			adRewardAdManager.OnRewardAdLoadFail += delegate(string error)
			{
				ToLog(string.Format("O7ADRewardAdManager.OnRewardAdLoadFail({0})", error));
			};
			adRewardAdManager.OnRewardAdShowSuccess += delegate
			{
				ToLog("O7ADRewardAdManager.OnRewardAdShowSuccess");
			};
			adRewardAdManager.OnRewardAdShowFail += delegate
			{
				ToLog("O7ADRewardAdManager.OnRewardAdShowFail");
			};
			adRewardAdManager.OnRewardAdClosed += delegate
			{
				ToLog("O7ADRewardAdManager.OnRewardAdClosed");
			};
			adRewardAdManager.OnRewardRecieved += delegate(string providerId)
			{
				ToLog(string.Format("O7ADRewardAdManager.OnRewardRecieved({0})", providerId));
			};
			adRewardAdManagerUI.LoadRewardAdButton.onClick.AddListener(delegate
			{
				adRewardAdManager.FetchRewardsAd();
			});
			adRewardAdManagerUI.ShowRewardAdButton.onClick.AddListener(delegate
			{
				adRewardAdManager.ShowRewardsAd();
			});
			adVideoGalleryAdManagerUi.ShowVideoGalleryButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7ADVideoGalleryAdManager.IsVideoGalleryAvailable() = {0}", adVideoGalleryAdManager.IsVideoGalleryAvailable()));
				if (adVideoGalleryAdManager.IsVideoGalleryAvailable())
				{
					adVideoGalleryAdManager.ShowVideoGallery("MainScene");
				}
			});
			crossPromoManagerUI.IsAutoNewsReadyToggle.isOn = crossPromoManager.IsAutoNewsReady();
			crossPromoManagerUI.IsManualNewsReadyToggle.isOn = crossPromoManager.IsManualNewsReady();
			crossPromoManager.OnAutoNewsClosed += delegate
			{
				ToLog("O7SDKCrossPromoManager.OnAutoNewsClosed");
			};
			crossPromoManager.OnManualNewsClosed += delegate
			{
				ToLog("O7SDKCrossPromoManager.OnManualNewsClosed");
			};
			crossPromoManager.OnAutoNewsReady += delegate(bool ready)
			{
				ToLog(string.Format("O7SDKCrossPromoManager.OnAutoNewsReady({0})", ready));
				crossPromoManagerUI.IsAutoNewsReadyToggle.isOn = ready;
			};
			crossPromoManager.OnManualNewsReady += delegate(bool ready)
			{
				ToLog(string.Format("O7SDKCrossPromoManager.OnManualNewsReady({0})", ready));
				ToLog(string.Format("O7SDKCrossPromoManager.GetManaulNewsButtonPath() = {0}", crossPromoManager.GetManaulNewsButtonPath()));
				crossPromoManagerUI.IsManualNewsReadyToggle.isOn = ready;
			};
			crossPromoManagerUI.ShowAutoNewsButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKCrossPromoManager.IsAutoNewsReady() = {0}", crossPromoManager.IsAutoNewsReady()));
				if (crossPromoManager.IsAutoNewsReady())
				{
					crossPromoManager.ShowAutoNews();
				}
			});
			crossPromoManagerUI.ShowManualNewsButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKCrossPromoManager.IsManualNewsReady() = {0}", crossPromoManager.IsManualNewsReady()));
				if (crossPromoManager.IsManualNewsReady())
				{
					crossPromoManager.ShowManualNews();
				}
			});
			purchaseManager.OnPurchaseSucceeded += delegate(string jsonObject)
			{
				ToLog(string.Format("O7SDKPurchaseManager.OnPurchaseSucceeded({0})", jsonObject));
			};
			purchaseManager.OnPurchaseFailed += delegate(string jsonObject)
			{
				ToLog(string.Format("O7SDKPurchaseManager.OnPurchaseFailed({0})", jsonObject));
			};
			purchaseManager.OnPurchasesRestored += delegate(string jsonObject)
			{
				ToLog(string.Format("O7SDKPurchaseManager.OnPurchasesRestored({0})", jsonObject));
			};
			purchaseManager.OnLoadedValidIAPProducts += delegate(string jsonArray)
			{
				ToLog(string.Format("O7SDKPurchaseManager.OnLoadedValidIAPProducts({0})", jsonArray));
			};
			purchaseManager.OnConfigurationSuccess += delegate
			{
				ToLog("O7SDKPurchaseManager.OnPurchaseAvailable()");
				InitializeIapProducts(dataManager.GetIAPuPacks());
			};
			purchaseManagerUI.GetValidIAPProductsButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKPurchaseManager.IsPurchasingAvailable() = {0}", purchaseManager.IsPurchasingAvailable()));
				ToLog(string.Format("O7SDKPurchaseManager.GetValidIAPProducts() = {0}", purchaseManager.GetValidIAPProducts()));
			});
			purchaseManagerUI.RestorePurchasesButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKPurchaseManager.IsPurchasingAvailable() = {0}", purchaseManager.IsPurchasingAvailable()));
				if (purchaseManager.IsPurchasingAvailable())
				{
					purchaseManager.RestorePurchases();
				}
			});
			purchaseManagerUI.RequestStoreProductsListButton.onClick.AddListener(delegate
			{
				ToLog(string.Format("O7SDKPurchaseManager.IsPurchasingAvailable() = {0}", purchaseManager.IsPurchasingAvailable()));
				if (purchaseManager.IsPurchasingAvailable())
				{
					purchaseManager.RequestStoreProductsList();
				}
			});
			utilityManagerUI.AdTrackingDisabledToggle.isOn = utilityManager.IsAdTrackingDisabled();
			utilityManagerUI.AdTrackingDisabledToggle.onValueChanged.AddListener(delegate(bool value)
			{
				utilityManager.DisableAdTracking(value);
			});
			utilityManagerUI.RequestRateThisAppPopupButton.onClick.AddListener(delegate
			{
				utilityManager.RequestRateThisAppPopup();
			});
			gateManager.OnAgeGateStatusChanged += delegate(bool isPassed)
			{
				ToLog(string.Format("O7SDKGateManager.OnAgeGateStatusChanged({0})", isPassed));
				gateManagerUI.DidUserPassAgeGateToggle.isOn = isPassed;
			};
			gateManagerUI.MustShowAgeGateToggle.isOn = gateManager.MustShowAgeGate();
			gateManagerUI.MustShowGenderGateToggle.isOn = gateManager.MustShowGenderGate();
			gateManagerUI.SetUserGenderButton.onClick.AddListener(delegate
			{
				O7SDKGateManager.Gender gender = userGenderLoop.Dequeue();
				userGenderLoop.Enqueue(gender);
				ToLog(string.Format("O7SDKGateManager.SetUserGender({0})", gender));
				gateManager.SetUserGender(gender);
				gateManagerUI.MustShowGenderGateToggle.isOn = gateManager.MustShowGenderGate();
			});
			gateManagerUI.SetUserBirthYearButton.onClick.AddListener(delegate
			{
				int num = userAgeLoop.Dequeue();
				userAgeLoop.Enqueue(num);
				int num2 = DateTime.Now.Year - num;
				ToLog(string.Format("O7SDKGateManager.SetUserBirthYear({0})", num2));
				gateManager.SetUserBirthYear(num2);
				gateManagerUI.MustShowAgeGateToggle.isOn = gateManager.MustShowAgeGate();
				gateManagerUI.DidUserPassAgeGateToggle.isOn = gateManager.DidUserPassAgeGate();
			});
			gateManagerUI.DidUserPassAgeGateToggle.isOn = gateManager.DidUserPassAgeGate();
			gateManagerUI.SetCountryCodeOverrideButton.onClick.AddListener(delegate
			{
				if (countryCodeLoop.Count == 0)
				{
					string validCountryCodes = dataManager.GetValidCountryCodes();
					if (!string.IsNullOrEmpty(validCountryCodes))
					{
						foreach (string item in O7SDKTypesMarshaling.ToList<string>(validCountryCodes))
						{
							countryCodeLoop.Enqueue(item);
						}
					}
				}
				if (countryCodeLoop.Count != 0)
				{
					string text = countryCodeLoop.Dequeue();
					countryCodeLoop.Enqueue(text);
					ToLog(string.Format("O7SDKGateManager.SetCountryCodeOverride({0})", text));
					gateManager.SetCountryCodeOverride(text);
				}
			});
			gateManagerUI.MustShowCountryCodeOverrideEnabledToggle.isOn = gateManager.MustShowCountryCodeOverrideEnabled();
			restoreManager.OnRestoreRequestSuccessed += delegate(O7SDKRestoreManager.RestoreRequestSuccessedResponse responce)
			{
				ToLog(string.Format("O7SDKRestoreManager.OnRestoreRequestSuccessed({0}, {1})", responce.httpCode, responce.responseJson));
				appState = responce.GetRemoteState<AppState>() ?? new AppState();
				ToLog(string.Format("AppState = {0}", JsonUtility.ToJson(appState)));
			};
			restoreManager.OnRestoreRequestFailed += delegate(O7SDKRestoreManager.RestoreRequestFailedResponse responce)
			{
				ToLog(string.Format("O7SDKRestoreManager.OnRestoreRequestFailed({0}, {1})", responce.httpCode, responce.error));
			};
			restoreManagerUI.AppStateLoadButton.onClick.AddListener(delegate
			{
				appState.Load();
				ToLog(string.Format("AppState = {0}", JsonUtility.ToJson(appState)));
			});
			restoreManagerUI.AppStateSaveButton.onClick.AddListener(delegate
			{
				appState.Save();
				ToLog(string.Format("AppState = {0}", JsonUtility.ToJson(appState)));
			});
			restoreManagerUI.AppStateRandomizeButton.onClick.AddListener(delegate
			{
				appState.Randomize();
				ToLog(string.Format("AppState = {0}", JsonUtility.ToJson(appState)));
			});
			restoreManagerUI.RestoreStateForAppNoneButton.onClick.AddListener(delegate
			{
				O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode = O7SDKRestoreManager.ForceOverwriteMode.None;
				string text = JsonUtility.ToJson(appState);
				ToLog(string.Format("O7SDKRestoreManager.RestoreStateForApp({0}, {1})", forceOverwriteMode, text));
				restoreManager.RestoreStateForApp(forceOverwriteMode, text);
			});
			restoreManagerUI.RestoreStateForAppNewButton.onClick.AddListener(delegate
			{
				O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode = O7SDKRestoreManager.ForceOverwriteMode.New;
				string text = JsonUtility.ToJson(appState);
				ToLog(string.Format("O7SDKRestoreManager.RestoreStateForApp({0}, {1})", forceOverwriteMode, text));
				restoreManager.RestoreStateForApp(forceOverwriteMode, text);
			});
			restoreManagerUI.RestoreStateForAppMasterButton.onClick.AddListener(delegate
			{
				O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode = O7SDKRestoreManager.ForceOverwriteMode.Master;
				string text = JsonUtility.ToJson(appState);
				ToLog(string.Format("O7SDKRestoreManager.RestoreStateForApp({0}, {1})", forceOverwriteMode, text));
				restoreManager.RestoreStateForApp(forceOverwriteMode, text);
			});
			restoreManagerUI.RestoreStateForAppRestoredButton.onClick.AddListener(delegate
			{
				O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode = O7SDKRestoreManager.ForceOverwriteMode.Restored;
				string text = JsonUtility.ToJson(appState);
				ToLog(string.Format("O7SDKRestoreManager.RestoreStateForApp({0}, {1})", forceOverwriteMode, text));
				restoreManager.RestoreStateForApp(forceOverwriteMode, text);
			});
		}

		private void InitializeIAP()
		{
			string listOfAllBillableItems = string.Join(",", listOfIapsGameSupports.ToArray());
			purchaseManager.CheckBillingAvailable(listOfAllBillableItems);
		}

		private void InitializeIapProducts(string packData)
		{
			foreach (string item in listOfIapsGameSupports)
			{
				List<IapPacks> list = O7SDKTypesMarshaling.ToList<IapPacks>(packData);
				IapPacks iapPacks = list.Find((IapPacks x) => x.id == item);
				string price = purchaseManager.GetPrice(iapPacks.id);
				if (price != null)
				{
					string id = item;
					GameObject gameObject = UnityEngine.Object.Instantiate(purchaseManagerUI.StartPurchasingButtonPrefab, purchaseManagerUI.StartPurchasingButtonsLayoutGroup.transform);
					gameObject.gameObject.SetActive(true);
					gameObject.GetComponent<LayoutElement>().ignoreLayout = false;
					Button componentInChildren = gameObject.GetComponentInChildren<Button>();
					componentInChildren.onClick.AddListener(delegate
					{
						ToLog(string.Format("purchaseManager.IsPurchasingAvailable({0})", id));
						purchaseManager.StartPurchasing(id);
					});
					Text componentInChildren2 = gameObject.GetComponentInChildren<Text>();
					string[] array = item.Split('.');
					string arg = array[array.Length - 1];
					componentInChildren2.text = string.Format("{0}-{1}", arg, price);
				}
			}
		}
	}
}
