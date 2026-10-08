using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CharSelectScreen : BaseDialog, ISaveDataLoadedListener
	{
		[Header("Char Select Screen")]
		[SerializeField]
		private ScrollSnapRect scrollSnapRect;

		[SerializeField]
		private List<CSCharEntry> charEntries;

		[SerializeField]
		private HorizontalShineEffect _ribbonShineEffect;

		[SerializeField]
		private BounceEffect _ribbonBounceEffect;

		[SerializeField]
		private TextMeshProUGUI charName;

		[SerializeField]
		private Button infoButton;

		[SerializeField]
		private Button upgradeButton;

		[SerializeField]
		private Button selectButton;

		[SerializeField]
		private GameObject selectedLabel;

		[SerializeField]
		private Button unlockCharButton;

		[SerializeField]
		private GameObject unlockedPart;

		[SerializeField]
		private TextMeshProUGUI availableFromLevelLabel;

		[SerializeField]
		private float minScaleOther;

		[SerializeField]
		private CSPartsBar partsBar;

		private int selectedCharacterId;

		private int playerLevel;

		private List<CSCharacterData> charDataList;

		private List<int> newUnlockedCharacters = new List<int>();

		private List<int> newCharactersToNotifyAsSeen = new List<int>();

		public int CurrentCharId { get; private set; }

		public int SelectedCharOrder { get; private set; }

		public override ScreenId Id
		{
			get
			{
				return ScreenId.CharSelectScreen;
			}
		}

		private void Start()
		{
			base.uiContext.CreateEntity().AddSaveDataLoadedListener(this);
			OnDataLoaded();
			scrollSnapRect.Init();
		}

		private void Update()
		{
			if (base.IsClickable && charDataList != null && charDataList.Count > 0)
			{
				UpdateSelection(scrollSnapRect.GetNearestPage());
				scrollSnapRect.ScaleOthers(1f, minScaleOther);
			}
		}

		private void SelectCharacter()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isCharacterSelectScreen = true;
				int order = (SelectedCharOrder + 1) % charDataList.Count;
				int charEntryIdByOrder = GetCharEntryIdByOrder(order);
				uIEntity.AddUICharacterSelected(CurrentCharId, charEntryIdByOrder);
				NotifyCurrentCharacterSeen();
			}
		}

		private void DisplayInfo()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isCharacterSelectScreen = true;
				uIEntity.AddUIShowCharacterInfo(GetCharEntryIdByOrder(SelectedCharOrder));
				NotifyCurrentCharacterSeen();
			}
		}

		private void DisplayNotEnoughParts()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIOpenShop(ShopGroupType.Chests);
				NotifyCurrentCharacterSeen();
			}
		}

		public void OnDataLoaded()
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.AddSetup(this);
			uIEntity.isUITotalCoinsRequest = true;
			uIEntity.isUICharListRequest = true;
			uIEntity.isUICharSelectedRequest = true;
			uIEntity.isUIPlayerProgressionStateRequest = true;
			uIEntity.isUINewCharactersUnlockedRequest = true;
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			if (parameters == null)
			{
				return;
			}
			ManagePlayerProgressionUpdateScreen(parameters.hasUICharSingleResponse);
			CurrencyAndExperienceHelper.CloseScreen();
			if (parameters.hasUICharSelectedResponse)
			{
				selectedCharacterId = parameters.uICharSelectedResponse.FirstCharId;
			}
			if (parameters.hasUICharListResponse)
			{
				SetCharDataList(parameters.uICharListResponse.CharDataList);
				SelectedCharOrder = GetCharEntryOrderById(selectedCharacterId);
				scrollSnapRect.Init();
				int page = ((!parameters.hasUICharSelectedResponse) ? GetCharEntryOrderById(CurrentCharId) : SelectedCharOrder);
				scrollSnapRect.SetPage(page);
				RefreshOverlay();
			}
			if (parameters.hasUIPlayerProgressionStateResponse)
			{
				playerLevel = parameters.uIPlayerProgressionStateResponse.PlayerLevel;
			}
			if (parameters.hasUICharSingleResponse)
			{
				CSCharacterData charData = parameters.uICharSingleResponse.CharData;
				CSCharacterData oldCharData = charDataList[SelectedCharOrder];
				UpdateCharView(charData);
				RefreshOverlayOnUpgrade(oldCharData, charData);
			}
			if (parameters.hasUINewCharactersUnlockedResponse)
			{
				List<int> unlockedIds = parameters.uINewCharactersUnlockedResponse.Unlocks.UnlockedIds;
				if (unlockedIds != null)
				{
					newUnlockedCharacters = unlockedIds;
				}
			}
			unlockCharButton.onClick.RemoveListener(DisplayNotEnoughParts);
			unlockCharButton.onClick.AddListener(DisplayNotEnoughParts);
			selectButton.onClick.RemoveListener(SelectCharacter);
			selectButton.onClick.AddListener(SelectCharacter);
			infoButton.onClick.RemoveListener(DisplayInfo);
			infoButton.onClick.AddListener(DisplayInfo);
			upgradeButton.onClick.RemoveListener(DisplayInfo);
			upgradeButton.onClick.AddListener(DisplayInfo);
			RefreshOverlay();
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				scrollSnapRect.Init();
			}
		}

		private void RefreshOverlay()
		{
			CurrentCharId = GetCharEntryIdByOrder(SelectedCharOrder);
			NotifyAllCharactersFromListAsSeenExceptCurrent();
			SetCurrentCharacterAsToNotifyAsSeen();
			NotifyNewCharacterSeen(CurrentCharId);
			for (int i = 0; i < charEntries.Count; i++)
			{
				int charEntryIdByOrder = GetCharEntryIdByOrder(i);
				bool isNew = newCharactersToNotifyAsSeen.Contains(charEntryIdByOrder) || newUnlockedCharacters.Contains(charEntryIdByOrder);
				charEntries[i].SetState(SelectedCharOrder == i, isNew);
			}
			CSCharacterData cSCharacterData = charDataList[SelectedCharOrder];
			charName.text = cSCharacterData.Name.ToUpper() + " <color=#E65205FF>" + (ScriptLocalization.general_level + " " + cSCharacterData.Level).ToUpper() + "</color>";
			if (cSCharacterData.Unlocked)
			{
				unlockCharButton.gameObject.SetActive(false);
				availableFromLevelLabel.gameObject.SetActive(false);
				unlockedPart.SetActive(true);
				RefreshUnlockedPart();
			}
			else
			{
				unlockedPart.SetActive(false);
				if (playerLevel >= cSCharacterData.MinLevel)
				{
					unlockCharButton.gameObject.SetActive(TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.Shop));
					availableFromLevelLabel.gameObject.SetActive(false);
				}
				else
				{
					unlockCharButton.gameObject.SetActive(false);
					availableFromLevelLabel.gameObject.SetActive(true);
					availableFromLevelLabel.text = string.Format(ScriptLocalization.available_from_level, cSCharacterData.MinLevel);
				}
			}
			UpdateCharEntriesButtonListeners();
		}

		private void NotifyAllCharactersFromListAsSeenExceptCurrent()
		{
			newCharactersToNotifyAsSeen.Where((int x) => x != CurrentCharId).ToList().ForEach(NotifyNewCharacterSeen);
			newCharactersToNotifyAsSeen.RemoveAll((int x) => x != CurrentCharId);
		}

		private void SetCurrentCharacterAsToNotifyAsSeen()
		{
			if (newUnlockedCharacters.Contains(CurrentCharId))
			{
				newUnlockedCharacters.Remove(CurrentCharId);
				newCharactersToNotifyAsSeen.Add(CurrentCharId);
			}
		}

		private void NotifyCurrentCharacterSeen()
		{
			NotifyNewCharacterSeen(CurrentCharId);
			newCharactersToNotifyAsSeen.Remove(CurrentCharId);
			newUnlockedCharacters.Remove(CurrentCharId);
			charEntries[CurrentCharId].SetState(true, false);
		}

		private void NotifyNewCharacterSeen(int charId)
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isCharacterSelectScreen = true;
			uIEntity.AddUINewUnlockedCharacterDisplayed(charId);
		}

		private void RefreshUnlockedPart()
		{
			CSCharacterData cSCharacterData = charDataList[SelectedCharOrder];
			int ownedParts = cSCharacterData.OwnedParts;
			int nextUpgradeCostParts = cSCharacterData.GetNextUpgradeCostParts();
			int charEntryIdByOrder = GetCharEntryIdByOrder(SelectedCharOrder);
			bool flag = cSCharacterData.IsNextUpgradeAvailable() && ownedParts >= nextUpgradeCostParts;
			selectButton.gameObject.SetActive(charEntryIdByOrder != selectedCharacterId);
			selectedLabel.SetActive(charEntryIdByOrder == selectedCharacterId);
			partsBar.SetData(cSCharacterData.IsNextUpgradeAvailable(), ownedParts, nextUpgradeCostParts);
			upgradeButton.gameObject.SetActive(flag);
			infoButton.gameObject.SetActive(!flag);
		}

		private void RefreshOverlayOnUpgrade(CSCharacterData oldCharData, CSCharacterData newCharData)
		{
			int ownedParts = newCharData.OwnedParts;
			int nextUpgradeCostParts = newCharData.GetNextUpgradeCostParts();
			bool flag = ownedParts >= nextUpgradeCostParts;
			partsBar.SetData(flag, ownedParts, nextUpgradeCostParts);
			unlockedPart.SetActive(flag);
			unlockCharButton.gameObject.SetActive(!flag && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.Shop));
		}

		private void SetCharDataList(List<CSCharacterData> dataList)
		{
			charDataList = dataList;
			for (int i = 0; i < charEntries.Count; i++)
			{
				charEntries[i].SetData(charDataList[i].OwnedParts, charDataList[i].GetNextUpgradeCostParts(), charDataList[i].IsNextUpgradeAvailable(), charDataList[i].Unlocked);
			}
		}

		private void UpdateSelection(int closest)
		{
			if (SelectedCharOrder != closest)
			{
				SelectedCharOrder = closest;
				RefreshOverlay();
			}
		}

		private void UpdateCharView(CSCharacterData newCharData)
		{
			charDataList[SelectedCharOrder] = newCharData;
			charEntries[SelectedCharOrder].SetData(newCharData.OwnedParts, newCharData.GetNextUpgradeCostParts(), newCharData.IsNextUpgradeAvailable(), newCharData.Unlocked);
		}

		private void SortElements()
		{
			SortCharacterDataList();
			SortCharEntries();
			SortCharEntriesInHierarchy();
		}

		private void SortCharacterDataList()
		{
			charDataList = (from x in charDataList
				orderby x.Unlocked descending, x.MinLevel, x.Id
				select x).ToList();
		}

		private void SortCharEntries()
		{
			List<CSCharEntry> list = new List<CSCharEntry>();
			for (int i = 0; i < charDataList.Count; i++)
			{
				CSCharEntry charEntryById = GetCharEntryById(charDataList[i].Id);
				list.Add(charEntryById);
			}
			charEntries = list;
		}

		private void SortCharEntriesInHierarchy()
		{
			foreach (CSCharEntry charEntry in charEntries)
			{
				charEntry.transform.SetAsLastSibling();
			}
		}

		private CSCharEntry GetCharEntryById(int id)
		{
			foreach (CSCharEntry charEntry in charEntries)
			{
				if (charEntry.Id == id)
				{
					return charEntry;
				}
			}
			return null;
		}

		public int GetCharEntryOrderById(int id)
		{
			for (int i = 0; i < charEntries.Count; i++)
			{
				if (charEntries[i].Id == id)
				{
					return i;
				}
			}
			return -1;
		}

		public int GetCharEntryIdByOrder(int order)
		{
			int index = Math.Min(Math.Max(0, order), charEntries.Count - 1);
			return charEntries[index].Id;
		}

		private void UpdateCharEntriesButtonListeners()
		{
			CSCharacterData cSCharacterData = charDataList[SelectedCharOrder];
			CSCharEntry cSCharEntry = charEntries[SelectedCharOrder];
			for (int i = 0; i < charEntries.Count; i++)
			{
				CSCharEntry charEntry = charEntries[i];
				if (charEntry == cSCharEntry)
				{
					if (cSCharacterData.Unlocked)
					{
						charEntry.ChangeButtonListener(DisplayInfo);
					}
					else
					{
						charEntry.ChangeButtonListener(null);
					}
				}
				else
				{
					charEntry.ChangeButtonListener(delegate
					{
						scrollSnapRect.LerpToPage(GetCharEntryOrderById(charEntry.Id));
					});
				}
			}
		}

		private void ManagePlayerProgressionUpdateScreen(bool isCharUpgradeAnimationToBePlayed)
		{
			SettingsSO settingsSO = SettingsService.SettingsSO;
			if (isCharUpgradeAnimationToBePlayed)
			{
				DOVirtual.DelayedCall(settingsSO.LevelProgressAfterUpgradeDelay, delegate
				{
					base.uiContext.CheckPlayerProgress();
				});
			}
			else
			{
				base.uiContext.CheckPlayerProgress();
			}
		}
	}
}
