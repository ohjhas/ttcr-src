using System.Collections;
using System.Collections.Generic;
using System.IO;
using O7SDK;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class HomeScreen : BaseDialog, ISaveDataLoadedListener, IO7SdkVideoGalleryListener, IO7SdkManualNewsButtonUpdateListener
	{
		[SerializeField]
		private HSMovingChar firstChar;

		[SerializeField]
		private HSMovingChar secondChar;

		[SerializeField]
		private HSMovingChar thirdChar;

		[SerializeField]
		private HSCompanionBehaviour SelectedCompParent;

		[SerializeField]
		private DailyMissionView DailyMissionView;

		[SerializeField]
		private Notification[] notifications;

		[SerializeField]
		private Image gridButton;

		[SerializeField]
		private Button videosGalleryButton;

		[SerializeField]
		private CanvasGroup HUDOverlay;

		private Sprite gridButtonSprite;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.HomeScreen;
			}
		}

		private void Start()
		{
			gridButtonSprite = gridButton.sprite;
			UpdateVideoGalleryButton(false);
			UpdateGridButton(false, string.Empty);
			base.uiContext.CreateEntity().AddSaveDataLoadedListener(this);
			base.uiContext.CreateEntity().AddO7SdkDataListener(this);
			base.uiContext.ReplaceO7SdkManualNewsListener(this);
			OnDataLoaded();
		}

		private void OnSoftCurrencyButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.AddUIOpenShop(ShopGroupType.SoftCurrency);
			}
		}

		private void OnHardCurrencyButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.AddUIOpenShop(ShopGroupType.HardCurrency);
			}
		}

		public void OnChestButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.AddUIOpenShop(ShopGroupType.Chests);
			}
		}

		public void OnCharButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.isUIOpenCharSelection = true;
			}
		}

		public void OnCompButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.isUIOpenCompSelection = true;
			}
		}

		public void OnGoButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.isUIOpenPvE = true;
			}
		}

		public void OnSettingsButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isHomeScreen = true;
				uIEntity.isUIOpenSettings = true;
			}
		}

		public void OnDailyMissionsButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.isUIOpenDailyMissions = true;
			}
		}

		public void OnDataLoaded()
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			Notification[] array = notifications;
			foreach (Notification notification in array)
			{
				if (!(null == notification))
				{
					notification.SetData(parameters);
				}
			}
			base.uiContext.CheckPlayerProgress();
			if (parameters == null)
			{
				return;
			}
			if (parameters.hasUICurrentDailyMissionsResponse)
			{
				DailyMissionView.UpdateView(parameters.uICurrentDailyMissionsResponse.DailyMissionsCollection);
			}
			CurrencyAndExperienceHelper.OpenScreenForHomeScreen(OnSoftCurrencyButton, OnHardCurrencyButton);
			int selectedCharacterId = 0;
			if (parameters.hasUICharSelectedResponse)
			{
				selectedCharacterId = parameters.uICharSelectedResponse.FirstCharId;
			}
			int selectedCompId = -1;
			if (parameters.hasUICompSelectedResponse)
			{
				selectedCompId = ((!parameters.uICompSelectedResponse.IsSelected) ? (-1) : parameters.uICompSelectedResponse.SelectedCompId);
			}
			CreateSelectedCompanion(parameters.uICompanionListResponse.CompDataList, selectedCompId);
			List<int> unlockedCharIndexListStartingFromTheFirstSelected = GetUnlockedCharIndexListStartingFromTheFirstSelected(parameters.uICharListResponse.CharDataList, selectedCharacterId);
			firstChar.Init(OnCharButton, unlockedCharIndexListStartingFromTheFirstSelected[0]);
			secondChar.gameObject.SetActive(false);
			thirdChar.gameObject.SetActive(false);
			if (unlockedCharIndexListStartingFromTheFirstSelected.Count >= 2)
			{
				secondChar.Init(OnCharButton, unlockedCharIndexListStartingFromTheFirstSelected[1]);
				if (unlockedCharIndexListStartingFromTheFirstSelected.Count >= 3)
				{
					thirdChar.Init(OnCharButton, unlockedCharIndexListStartingFromTheFirstSelected[2]);
				}
			}
		}

		public void HideButKeepInteractive()
		{
			HUDOverlay.alpha = 0f;
			HUDOverlay.blocksRaycasts = true;
		}

		private void CreateSelectedCompanion(List<CSCompanionData> compDataList, int selectedCompId)
		{
			SelectedCompParent.DestroyOldComp();
			if (selectedCompId >= 0 && selectedCompId < compDataList.Count)
			{
				CSCompanionData cSCompanionData = compDataList[selectedCompId];
				if (cSCompanionData.Unlocked)
				{
					SelectedCompParent.CreateCompanionAsync(OnCompButton, selectedCompId);
				}
			}
		}

		private List<int> GetUnlockedCharIndexListStartingFromTheFirstSelected(List<CSCharacterData> charDataList, int selectedCharacterId)
		{
			List<int> list = new List<int>();
			list.Add(selectedCharacterId);
			for (int i = selectedCharacterId + 1; i < charDataList.Count; i++)
			{
				if (charDataList[i].Unlocked)
				{
					list.Add(i);
				}
			}
			for (int j = 0; j < selectedCharacterId; j++)
			{
				if (charDataList[j].Unlocked)
				{
					list.Add(j);
				}
			}
			return list;
		}

		public void OnGridButton()
		{
			if (base.IsClickable)
			{
				StateContext state = Contexts.sharedInstance.state;
				StateEntity stateEntity = state.CreateEntity();
				stateEntity.isO7SdkCall = true;
				stateEntity.AddO7SdkCrossPromo(false, true);
			}
		}

		private IEnumerator SetGridButtonImage(string pathToImage)
		{
			if (File.Exists(pathToImage))
			{
				byte[] data = File.ReadAllBytes(pathToImage);
				Texture2D texture2D = new Texture2D(2, 2, TextureFormat.ARGB32, false);
				texture2D.LoadImage(data);
				Sprite sprite = Sprite.Create(texture2D, new Rect(0f, 0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100f);
				gridButton.sprite = sprite;
			}
			else
			{
				LoadDefaultGridButton();
			}
			yield return null;
		}

		private void LoadDefaultGridButton()
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log("Loading Default grid image");
			}
			gridButton.sprite = gridButtonSprite;
		}

		public void UpdateGridButton(bool isReady, string pathToManualNewsButton)
		{
			Debug.Log(string.Format("UpdateGridButton {0} at {1}", isReady, pathToManualNewsButton));
			if (isReady && pathToManualNewsButton != null && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.EndTutorial))
			{
				gridButton.gameObject.SetActive(true);
				if (string.Empty == pathToManualNewsButton)
				{
					if (O7SDKBase.O7SDK_LOG)
					{
						Debug.Log("Grid Image is empty");
					}
					LoadDefaultGridButton();
				}
				else
				{
					StartCoroutine(SetGridButtonImage(pathToManualNewsButton));
				}
			}
			else
			{
				gridButton.gameObject.SetActive(false);
			}
		}

		public void OnVideosGalleryShow()
		{
			StateContext state = Contexts.sharedInstance.state;
			StateEntity stateEntity = state.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.isO7SdkVideoGallery = true;
		}

		public void UpdateVideoGalleryButton(bool canShow)
		{
			videosGalleryButton.gameObject.SetActive(canShow && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.EndTutorial));
		}
	}
}
