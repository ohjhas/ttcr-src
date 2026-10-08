using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WorldSelectScreen : BaseDialog, ISaveDataLoadedListener
	{
		private const int ENTRIES_IN_CHAPTER = 15;

		[SerializeField]
		private WSScrollView ScrollView = new WSScrollView();

		[SerializeField]
		private WSCharacter Character = new WSCharacter();

		[SerializeField]
		private WSCompanion Companion = new WSCompanion();

		[SerializeField]
		private WSRaccoon Raccoon = new WSRaccoon();

		[SerializeField]
		private WSLevelEntriesCollection EntriesCollection = new WSLevelEntriesCollection();

		[SerializeField]
		private WSChapterBackgrounds ChapterBackgrounds = new WSChapterBackgrounds();

		[SerializeField]
		private WSChapterGates ChapterGates = new WSChapterGates();

		[SerializeField]
		private DailyMissionView DailyMissionButton;

		[SerializeField]
		private CanvasGroup HUDOverlay;

		[SerializeField]
		private Notification[] notifications;

		private int unlockedLevelId;

		private int currentlySelectedId;

		private int totalStarsCollected;

		private bool newLevelUnlockAnimation;

		private bool wasNotifiedAboutMissionUnlocking;

		private bool wasUnlockingNewLevelAnimationStoped;

		private bool stopUnlockingNewLevelAnimation;

		private List<WSLevelData> levelDataList = new List<WSLevelData>();

		private List<WSChapterData> chapterDataList = new List<WSChapterData>();

		private List<ChestData> chestDataList = new List<ChestData>();

		private DailyMissionsCollection dailyMissionsCollection;

		private Coroutine openPopupCoroutine;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.SagaMap;
			}
		}

		private void Start()
		{
			base.uiContext.CreateEntity().AddSaveDataLoadedListener(this);
			OnDataLoaded();
		}

		private void LateUpdate()
		{
			if ((stopUnlockingNewLevelAnimation || newLevelUnlockAnimation) && !wasUnlockingNewLevelAnimationStoped && CanAnimateUnlockingLevel(false))
			{
				int currentId = ((!stopUnlockingNewLevelAnimation) ? currentlySelectedId : (currentlySelectedId + 1));
				Raccoon.UpdateRaccoon(EntriesCollection.GetEntries(), currentId, UnlockEntry);
			}
			if (!newLevelUnlockAnimation && !wasUnlockingNewLevelAnimationStoped)
			{
				ScrollView.UpdateScrollView();
				Character.UpdateCharacter(EntriesCollection.GetEntries(), currentlySelectedId);
			}
			else
			{
				float num = 0f;
				if (CanAnimateUnlockingLevel(true))
				{
					num = Character.AnimateCharacter(EntriesCollection.GetEntries(), currentlySelectedId);
					if (num > 0f)
					{
						stopUnlockingNewLevelAnimation = false;
						Contexts.sharedInstance.state.sessionEntity.isWorldSelectUnlockingAnimationStoped = false;
					}
				}
				ScrollView.CenterOnSelectedWorld((float)Mathf.Max(0, currentlySelectedId - 1) + num);
				ScrollView.ClampScrollView();
				FinishCharacterAnimation(num);
			}
			Companion.SetPosition(Character.GetPosition(), Character.GetRotation());
			UpdateCulling();
		}

		private void UpdateCulling()
		{
			if (ChapterBackgrounds == null || ScrollView == null)
			{
				return;
			}
			List<WSChapterBg> entries = ChapterBackgrounds.GetEntries();
			float num = ScrollView.GetPositionIndex() / 15f;
			int num2 = (int)num;
			int num3 = SelectClosestScreen(num, num2);
			for (int i = 0; i < entries.Count; i++)
			{
				WSChapterBg wSChapterBg = entries[i];
				if (i == num2 || i == num3)
				{
					if (!wSChapterBg.gameObject.activeInHierarchy)
					{
						wSChapterBg.gameObject.SetActive(true);
					}
				}
				else if (wSChapterBg.gameObject.activeInHierarchy)
				{
					wSChapterBg.gameObject.SetActive(false);
				}
			}
		}

		private int SelectClosestScreen(float currentPos, int index)
		{
			float num = currentPos - (float)index;
			if (num > 0.5f)
			{
				return index + 1;
			}
			return index - 1;
		}

		public void OnSelectPressed()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.AddUIPlayMission(currentlySelectedId);
			}
		}

		public void OnCharButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.isUIOpenCharSelection = true;
			}
		}

		public void OnCompButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.isUIOpenCompSelection = true;
			}
		}

		public void OnChestButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.AddUIOpenShop(ShopGroupType.Chests);
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

		public void OnSoftCurrencyButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.AddUIOpenShop(ShopGroupType.SoftCurrency);
			}
		}

		public void OnHardCurrencyButton()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.AddUIOpenShop(ShopGroupType.HardCurrency);
			}
		}

		private void OnMissionEntry(int entryId)
		{
			if (base.IsClickable)
			{
				OpenMissionDetailsPopup(entryId);
			}
		}

		public void OnNotEnoughStarsButtonPressed()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIDisplayExplanation(ExplanationType.Chapter);
			}
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
			if (parameters != null)
			{
				RefreshUI(parameters);
			}
			CurrencyAndExperienceHelper.OpenScreenForWorldSelectScreen(OnSoftCurrencyButton, OnHardCurrencyButton);
		}

		public override RectTransform GetLastFocusElement()
		{
			return base.transform as RectTransform;
		}

		public void OnDataLoaded()
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isRefreshScreen = true;
		}

		public void HideButKeepInteractive()
		{
			HUDOverlay.alpha = 0f;
			HUDOverlay.blocksRaycasts = true;
		}

		private void FinishCharacterAnimation(float animFactor)
		{
			if (animFactor >= 1f)
			{
				newLevelUnlockAnimation = false;
				Contexts.sharedInstance.state.sessionEntity.isWorldSelectUnlockingAnimationStoped = false;
				wasUnlockingNewLevelAnimationStoped = false;
				ScrollView.SetScrollViewState(true);
				Character.AsignCharToEntry(EntriesCollection.GetEntries(), currentlySelectedId);
				ScrollView.CenterOnSelectedWorld(currentlySelectedId);
				ScrollView.ClampScrollView();
			}
		}

		private void UnlockEntry()
		{
			int num = ((!stopUnlockingNewLevelAnimation) ? currentlySelectedId : (currentlySelectedId + 1));
			EntriesCollection.UnlockEntry(num);
			EntriesCollection.RefreshEntryState(num, num, num);
			NotifyAboutMissionUnlocking();
		}

		private void OpenMissionDetailsPopup(int entryId)
		{
			Contexts.sharedInstance.state.CreateEntity().AddPassLevelAction(TutorialOutgameLevel.LevelEntry);
			if (entryId != currentlySelectedId)
			{
				EntriesCollection.RefreshEntryState(entryId, entryId, unlockedLevelId);
				EntriesCollection.RefreshEntryState(currentlySelectedId, entryId, unlockedLevelId);
				currentlySelectedId = entryId;
				Character.SetTargetPosition(EntriesCollection.GetEntries(), currentlySelectedId);
				ScrollView.SetScrollTarget(currentlySelectedId);
			}
			if (openPopupCoroutine != null)
			{
				StopCoroutine(openPopupCoroutine);
			}
			openPopupCoroutine = StartCoroutine(OpenMissionDetailsCoroutine(entryId, true));
		}

		private IEnumerator OpenMissionDetailsCoroutine(int entryId, bool isTriggeredByButton)
		{
			yield return null;
			yield return new WaitUntil(Character.IsReachedTarget);
			SettingsWorldSelectSO so = SettingsService.GetSetting<SettingsWorldSelectSO>();
			yield return new WaitForSecondsRealtime(so.ConfirmationPopupDelay);
			if (!isTriggeredByButton)
			{
				yield return new WaitForSecondsRealtime(so.ConfirmationPopupAdditionalDelay);
			}
			base.IsClickable = true;
			UIEntity entity = base.uiContext.CreateEntity();
			entity.isUIAction = true;
			entity.AddUIDisplayMissionDetails(entryId);
			openPopupCoroutine = null;
		}

		private IEnumerator GoDirectlyToLevelCoroutine(int entryId)
		{
			yield return null;
			yield return new WaitUntil(Character.IsReachedTarget);
			SettingsWorldSelectSO so = SettingsService.GetSetting<SettingsWorldSelectSO>();
			yield return new WaitForSecondsRealtime(so.ConfirmationPopupDelay + so.ConfirmationPopupAdditionalDelay);
			base.IsClickable = true;
			UIEntity action = base.uiContext.CreateEntity();
			action.isWorldSelectScreen = true;
			action.AddUIPlayMission(entryId);
		}

		private void RefreshUI(UIEntity data)
		{
			LoadDataFromEntity(data);
			if (RateMePopupShowPossible())
			{
				ShowRateMePopUp();
			}
			SetupChapterBackgrounds();
			SetupLevelEntries();
			SetupChapterGates();
			UpdateDailyMissionsButton();
			int characterPosId = Mathf.Max(0, currentlySelectedId + ((newLevelUnlockAnimation || wasUnlockingNewLevelAnimationStoped) ? (-1) : 0));
			SetupScrollView(characterPosId);
			SetupCompanion((!data.uICompSelectedResponse.IsSelected) ? (-1) : data.uICompSelectedResponse.SelectedCompId);
			if (newLevelUnlockAnimation)
			{
				SetupRaccoon(characterPosId);
			}
			SetupCharacter(characterPosId, data.uICharSelectedResponse.FirstCharId);
			StartCoroutine(SetRunning());
		}

		private bool RateMePopupShowPossible()
		{
			StateContext state = Contexts.sharedInstance.state;
			O7SdkBalancingDataComponent o7SdkBalancingData = state.balancingDataEntity.o7SdkBalancingData;
			StateEntity stateEntity = state.GetEntitiesWithName("RateMeShown").SingleEntity();
			StateEntity stateEntity2 = state.GetEntitiesWithName("RateMeDontLike").SingleEntity();
			int num = 0;
			if (stateEntity.integer.Amount == 0)
			{
				num = o7SdkBalancingData.Data.Ratemefirsttime;
			}
			else if (stateEntity.integer.Amount == 1)
			{
				num = o7SdkBalancingData.Data.Ratemesecondtime;
			}
			if (unlockedLevelId == num && !stateEntity2.boolean.Value && state.o7SdkEntity != null)
			{
				return true;
			}
			return false;
		}

		private void ShowRateMePopUp()
		{
			if (!TutorialHelpers.IsTutorialActive())
			{
				StateContext state = Contexts.sharedInstance.state;
				StateEntity stateEntity = state.CreateEntity();
				stateEntity.isO7SdkRateMe = true;
			}
		}

		private bool CanAnimateUnlockingLevel(bool checkTutorial)
		{
			IGroup<StateEntity> obj = Contexts.sharedInstance.state.GetGroup(StateMatcher.PlayerLevelUp);
			CurrencyAndExperienceScreen dialog = base.uiContext.GetDialog<CurrencyAndExperienceScreen>();
			bool flag = checkTutorial && TutorialHelpers.IsTutorialActive();
			return (dialog == null || !dialog.IsPlayingExperienceAnimations) && obj.GetEntities().Length == 0 && Contexts.sharedInstance.state.activeScreenEntity.activeScreen.ScreenId.Equals(ScreenId.SagaMap) && !flag;
		}

		private IEnumerator SetRunning()
		{
			yield return null;
			yield return new WaitUntil(() => Character.IsCharacterReady);
			bool tutorialActive = TutorialHelpers.IsTutorialActive() || RateMePopupShowPossible();
			if (!newLevelUnlockAnimation)
			{
				if (wasUnlockingNewLevelAnimationStoped && !tutorialActive)
				{
					base.IsClickable = false;
					Character.SetTargetPosition(EntriesCollection.GetEntries(), currentlySelectedId);
					openPopupCoroutine = StartCoroutine(OpenMissionDetailsCoroutine(unlockedLevelId, false));
				}
				yield break;
			}
			newLevelUnlockAnimation = !tutorialActive;
			currentlySelectedId -= (tutorialActive ? 1 : 0);
			stopUnlockingNewLevelAnimation = tutorialActive;
			if (!tutorialActive)
			{
				base.IsClickable = false;
				Character.SetTargetPosition(EntriesCollection.GetEntries(), currentlySelectedId);
				if (openPopupCoroutine != null)
				{
					StopCoroutine(openPopupCoroutine);
				}
				if (SettingsService.SettingsSO.WorldIndex == 0)
				{
					openPopupCoroutine = StartCoroutine(GoDirectlyToLevelCoroutine(unlockedLevelId));
				}
				else
				{
					openPopupCoroutine = StartCoroutine(OpenMissionDetailsCoroutine(unlockedLevelId, false));
				}
			}
		}

		private void LoadDataFromEntity(UIEntity data)
		{
			levelDataList = data.uIMissionListResponse.LevelDataList;
			chapterDataList = data.uIChapterListResponse.ChapterDataList;
			chestDataList = data.uIChestListResponse.ChestDataList;
			totalStarsCollected = data.uITotalStarsResponse.Amount;
			unlockedLevelId = Mathf.Min(data.uIUnlockedMissionResponse.Index, levelDataList.Count - 1);
			currentlySelectedId = Mathf.Min(data.uILastSelectedMissionResponse.Index, unlockedLevelId);
			newLevelUnlockAnimation = data.uINewMissionUnlockedResponse.Value && data.uIUnlockedMissionResponse.Index < levelDataList.Count;
			wasNotifiedAboutMissionUnlocking = false;
			dailyMissionsCollection = data.uICurrentDailyMissionsResponse.DailyMissionsCollection;
			wasUnlockingNewLevelAnimationStoped = Contexts.sharedInstance.state.sessionEntity.isWorldSelectUnlockingAnimationStoped;
		}

		private void NotifyAboutMissionUnlocking()
		{
			if (!wasNotifiedAboutMissionUnlocking)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isWorldSelectScreen = true;
				uIEntity.isUIUnlockNewMission = true;
				wasNotifiedAboutMissionUnlocking = true;
				Contexts.sharedInstance.state.sessionEntity.isWorldSelectUnlockingAnimationStoped = true;
			}
		}

		private void SetupChapterBackgrounds()
		{
			ChapterBackgrounds.CreateEntriesIfNeeded(ScrollView.GetViewContent(), chapterDataList.Count);
			ChapterBackgrounds.SetupEntries(chapterDataList);
			ChapterBackgrounds.SetupEntriesPosition(chapterDataList.Count);
		}

		private void SetupChapterGates()
		{
			ChapterGates.CreateEntriesIfNeeded(ScrollView.GetViewContent(), chapterDataList.Count);
			ChapterGates.SetupEntries(chapterDataList, ChapterBackgrounds);
			bool newChapterUnlocked = newLevelUnlockAnimation && unlockedLevelId > 0 && levelDataList[unlockedLevelId].Chapter > levelDataList[unlockedLevelId - 1].Chapter;
			ChapterGates.SetupEntriesState(chapterDataList.Count, totalStarsCollected, levelDataList[unlockedLevelId].Chapter, newChapterUnlocked, OnNotEnoughStarsButtonPressed);
		}

		private void SetupLevelEntries()
		{
			EntriesCollection.SetupEntryPressCallback(OnMissionEntry);
			EntriesCollection.CreateEntriesIfNeeded(ScrollView.GetViewContent(), levelDataList.Count);
			EntriesCollection.SetupEntries(levelDataList, chestDataList, ChapterBackgrounds, unlockedLevelId);
			EntriesCollection.SetupEntriesState(levelDataList.Count, unlockedLevelId, currentlySelectedId, newLevelUnlockAnimation);
			ScrollView.SetEntries(EntriesCollection);
		}

		private void SetupScrollView(int characterPosId)
		{
			ScrollView.CenterOnSelectedWorld(characterPosId);
			ScrollView.SetScrollViewState(!newLevelUnlockAnimation);
			ScrollView.ClampScrollView();
		}

		private void SetupCharacter(int characterPosId, int selectedCharacter)
		{
			Character.CreateCharacter(base.transform, selectedCharacter, delegate
			{
				Character.InitTimer(!wasUnlockingNewLevelAnimationStoped);
				Character.AsignCharToEntry(EntriesCollection.GetEntries(), characterPosId);
				Companion.SetPosition(Character.GetPosition(), Character.GetRotation());
			});
		}

		private void SetupCompanion(int selectedComp)
		{
			if (selectedComp >= 0)
			{
				Companion.CreateCompanion(base.transform, selectedComp);
			}
		}

		private void SetupRaccoon(int characterPosId)
		{
			Raccoon.CreateRaccoon(base.transform);
			Raccoon.InitTimer();
			Raccoon.AsignRaccoonToEntry(EntriesCollection.GetEntries(), characterPosId);
		}

		private void UpdateDailyMissionsButton()
		{
			DailyMissionButton.UpdateView(dailyMissionsCollection);
		}
	}
}
