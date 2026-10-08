using System;
using System.Collections.Generic;
using System.Linq;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CompSelectScreen : BaseDialog, ISaveDataLoadedListener
	{
		[Header("Comp Select Screen")]
		[SerializeField]
		private ScrollSnapRect scrollSnapRect;

		[SerializeField]
		private List<CSCompEntry> compEntries;

		[SerializeField]
		private TextMeshProUGUI compName;

		[SerializeField]
		private Button infoButton;

		[SerializeField]
		private Button upgradeButton;

		[SerializeField]
		private Button selectButton;

		[SerializeField]
		private GameObject selectedLabel;

		[SerializeField]
		private Button unlockCompButton;

		[SerializeField]
		private GameObject unlockedPart;

		[SerializeField]
		private TextMeshProUGUI availableFromLevelLabel;

		[SerializeField]
		private CanvasParticleEmitter upgradeParticles;

		[SerializeField]
		private CSPartsBar partsBar;

		[SerializeField]
		private float minScaleOther;

		private int selectedCompanionId;

		private List<int> newUnlockedCompanions = new List<int>();

		private List<int> newCompanionsToNotifyAsSeen = new List<int>();

		private int playerLevel;

		private List<CSCompanionData> compDataList;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.CompSelectScreen;
			}
		}

		public int CurrentCompId { get; private set; }

		public int CurrentCompanionOrder { get; private set; }

		private void Start()
		{
			base.uiContext.CreateEntity().AddSaveDataLoadedListener(this);
			OnDataLoaded();
			scrollSnapRect.Init();
		}

		private void Update()
		{
			if (base.IsClickable && compDataList != null && compDataList.Count > 0)
			{
				UpdateSelection(scrollSnapRect.GetNearestPage());
				scrollSnapRect.ScaleOthers(1f, minScaleOther);
			}
		}

		public void SelectCompanion()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isCompanionSelectScreen = true;
				uIEntity.AddUICompanionSelected(GetCompEntryIdByOrder(CurrentCompanionOrder));
				NotifyCurrentCompanionSeen();
			}
		}

		private void DisplayInfo()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isCompanionSelectScreen = true;
				uIEntity.AddUIShowCompanionInfo(GetCompEntryIdByOrder(CurrentCompanionOrder));
				NotifyCurrentCompanionSeen();
			}
		}

		private void DisplayNotEnoughParts()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIOpenShop(ShopGroupType.Chests);
				NotifyCurrentCompanionSeen();
			}
		}

		public void OnDataLoaded()
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.AddSetup(this);
			uIEntity.isUITotalCoinsRequest = true;
			uIEntity.isUICompanionListRequest = true;
			uIEntity.isUICompSelectedRequest = true;
			uIEntity.isUIPlayerProgressionStateRequest = true;
			uIEntity.isUINewCompanionUnlockedRequest = true;
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			if (parameters == null)
			{
				return;
			}
			base.uiContext.CheckPlayerProgress();
			CurrencyAndExperienceHelper.CloseScreen();
			if (parameters.hasUICompSelectedResponse)
			{
				selectedCompanionId = parameters.uICompSelectedResponse.SelectedCompId;
			}
			if (parameters.hasUICompanionListResponse)
			{
				SetCompDataList(parameters.uICompanionListResponse.CompDataList);
				CurrentCompanionOrder = GetCompEntryOrderById(selectedCompanionId);
				scrollSnapRect.Init();
				int page = ((!parameters.hasUICompSelectedResponse) ? GetCompEntryOrderById(CurrentCompId) : CurrentCompanionOrder);
				scrollSnapRect.SetPage(page);
				RefreshOverlay();
			}
			if (parameters.hasUIPlayerProgressionStateResponse)
			{
				playerLevel = parameters.uIPlayerProgressionStateResponse.PlayerLevel;
			}
			if (parameters.hasUICompSingleResponse)
			{
				CSCompanionData compData = parameters.uICompSingleResponse.CompData;
				CSCompanionData oldCompData = compDataList[CurrentCompanionOrder];
				UpdateCompView(compData);
				RefreshOverlayOnUpgrade(oldCompData, compData);
			}
			if (parameters.hasUINewCompanionsUnlockedResponse)
			{
				List<int> unlockedIds = parameters.uINewCompanionsUnlockedResponse.Unlocks.UnlockedIds;
				if (unlockedIds != null)
				{
					newUnlockedCompanions = unlockedIds;
				}
			}
			unlockCompButton.onClick.RemoveListener(DisplayNotEnoughParts);
			unlockCompButton.onClick.AddListener(DisplayNotEnoughParts);
			selectButton.onClick.RemoveListener(SelectCompanion);
			selectButton.onClick.AddListener(SelectCompanion);
			infoButton.onClick.RemoveListener(DisplayInfo);
			infoButton.onClick.AddListener(DisplayInfo);
			upgradeButton.onClick.RemoveListener(DisplayInfo);
			upgradeButton.onClick.AddListener(DisplayInfo);
			RefreshOverlay();
		}

		public override RectTransform GetLastFocusElement()
		{
			return upgradeButton.transform as RectTransform;
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
			CurrentCompId = GetCompEntryIdByOrder(CurrentCompanionOrder);
			NotifyAllCompanionsFromListAsSeenExceptCurrent();
			SetCurrentCompanionAsToNotifyAsSeen();
			NotifyNewCompanionSeen(CurrentCompId);
			for (int i = 0; i < compEntries.Count; i++)
			{
				int compEntryIdByOrder = GetCompEntryIdByOrder(i);
				bool isNew = newCompanionsToNotifyAsSeen.Contains(compEntryIdByOrder) || newUnlockedCompanions.Contains(compEntryIdByOrder);
				compEntries[i].SetState(CurrentCompanionOrder == i, isNew);
			}
			CSCompanionData cSCompanionData = compDataList[CurrentCompanionOrder];
			compName.text = cSCompanionData.Name.ToUpper() + " <color=#E65205FF>" + (ScriptLocalization.general_level + " " + cSCompanionData.Level).ToUpper() + "</color>";
			if (cSCompanionData.Unlocked)
			{
				unlockCompButton.gameObject.SetActive(false);
				availableFromLevelLabel.gameObject.SetActive(false);
				unlockedPart.SetActive(true);
				RefreshUnlockedPart();
			}
			else
			{
				unlockedPart.SetActive(false);
				if (playerLevel >= cSCompanionData.MinLevel)
				{
					unlockCompButton.gameObject.SetActive(TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.Shop));
					availableFromLevelLabel.gameObject.SetActive(false);
				}
				else
				{
					unlockCompButton.gameObject.SetActive(false);
					availableFromLevelLabel.gameObject.SetActive(true);
					availableFromLevelLabel.text = string.Format(ScriptLocalization.available_from_level, cSCompanionData.MinLevel);
				}
			}
			UpdateCompEntriesButtonListeners();
		}

		private void NotifyAllCompanionsFromListAsSeenExceptCurrent()
		{
			newCompanionsToNotifyAsSeen.Where((int x) => x != CurrentCompId).ToList().ForEach(NotifyNewCompanionSeen);
			newCompanionsToNotifyAsSeen.RemoveAll((int x) => x != CurrentCompId);
		}

		private void SetCurrentCompanionAsToNotifyAsSeen()
		{
			if (newUnlockedCompanions.Contains(CurrentCompId))
			{
				newUnlockedCompanions.Remove(CurrentCompId);
				newCompanionsToNotifyAsSeen.Add(CurrentCompId);
			}
		}

		private void NotifyCurrentCompanionSeen()
		{
			NotifyNewCompanionSeen(CurrentCompId);
			newCompanionsToNotifyAsSeen.Remove(CurrentCompId);
			newUnlockedCompanions.Remove(CurrentCompId);
			compEntries[CurrentCompId].SetState(true, false);
		}

		private void NotifyNewCompanionSeen(int compId)
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isCompanionSelectScreen = true;
			uIEntity.AddUINewUnlockedCompanionDisplayed(compId);
		}

		private void RefreshUnlockedPart()
		{
			CSCompanionData cSCompanionData = compDataList[CurrentCompanionOrder];
			int ownedParts = cSCompanionData.OwnedParts;
			int nextUpgradeCostParts = cSCompanionData.GetNextUpgradeCostParts();
			int compEntryIdByOrder = GetCompEntryIdByOrder(CurrentCompanionOrder);
			bool flag = cSCompanionData.IsNextUpgradeAvailable() && ownedParts >= nextUpgradeCostParts;
			selectButton.gameObject.SetActive(compEntryIdByOrder != selectedCompanionId);
			selectedLabel.SetActive(compEntryIdByOrder == selectedCompanionId);
			partsBar.SetData(cSCompanionData.IsNextUpgradeAvailable(), ownedParts, nextUpgradeCostParts);
			upgradeButton.gameObject.SetActive(flag);
			infoButton.gameObject.SetActive(!flag);
		}

		private void RefreshOverlayOnUpgrade(CSCompanionData oldCompData, CSCompanionData newCompData)
		{
			int ownedParts = newCompData.OwnedParts;
			int nextUpgradeCostParts = newCompData.GetNextUpgradeCostParts();
			bool flag = ownedParts >= nextUpgradeCostParts;
			compName.text = newCompData.Name.ToUpper() + " <color=#E65205FF>" + (ScriptLocalization.general_level + " " + newCompData.Level).ToUpper() + "</color>";
			partsBar.SetData(flag, ownedParts, nextUpgradeCostParts);
			unlockedPart.SetActive(flag);
			unlockCompButton.gameObject.SetActive(!flag && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.Shop));
			upgradeParticles.RestartEmitter();
		}

		private void SetCompDataList(List<CSCompanionData> dataList)
		{
			compDataList = dataList;
			for (int i = 0; i < compEntries.Count; i++)
			{
				compEntries[i].SetData(compDataList[i].OwnedParts, compDataList[i].GetNextUpgradeCostParts(), compDataList[i].IsNextUpgradeAvailable(), compDataList[i].Unlocked);
			}
		}

		private void UpdateSelection(int closest)
		{
			if (CurrentCompanionOrder != closest)
			{
				CurrentCompanionOrder = closest;
				RefreshOverlay();
			}
		}

		private void UpdateCompView(CSCompanionData newCompData)
		{
			compDataList[CurrentCompanionOrder] = newCompData;
			compEntries[CurrentCompanionOrder].SetData(newCompData.OwnedParts, newCompData.GetNextUpgradeCostParts(), newCompData.IsNextUpgradeAvailable(), newCompData.Unlocked);
		}

		private void SortElements()
		{
			SortCompanionDataList();
			SortCharEntries();
			SortCompEntriesInHierarchy();
		}

		private void SortCompanionDataList()
		{
			compDataList = (from x in compDataList
				orderby x.Unlocked descending, x.MinLevel, x.Id
				select x).ToList();
		}

		private void SortCharEntries()
		{
			List<CSCompEntry> list = new List<CSCompEntry>();
			for (int i = 0; i < compDataList.Count; i++)
			{
				CSCompEntry compEntryById = GetCompEntryById(compDataList[i].Id);
				list.Add(compEntryById);
			}
			compEntries = list;
		}

		private void SortCompEntriesInHierarchy()
		{
			foreach (CSCompEntry compEntry in compEntries)
			{
				compEntry.transform.SetAsLastSibling();
			}
		}

		private CSCompEntry GetCompEntryById(int id)
		{
			foreach (CSCompEntry compEntry in compEntries)
			{
				if (compEntry.Id == id)
				{
					return compEntry;
				}
			}
			return null;
		}

		public int GetCompEntryOrderById(int id)
		{
			id = ((id != -1) ? id : 0);
			for (int i = 0; i < compEntries.Count; i++)
			{
				if (compEntries[i].Id == id)
				{
					return i;
				}
			}
			return -1;
		}

		public int GetCompEntryIdByOrder(int order)
		{
			order = ((order != -1) ? order : 0);
			int index = Math.Min(Math.Max(0, order), compEntries.Count - 1);
			return compEntries[index].Id;
		}

		private void UpdateCompEntriesButtonListeners()
		{
			CSCompanionData cSCompanionData = compDataList[CurrentCompanionOrder];
			CSCompEntry cSCompEntry = compEntries[CurrentCompanionOrder];
			for (int i = 0; i < compEntries.Count; i++)
			{
				CSCompEntry compEntry = compEntries[i];
				if (compEntry == cSCompEntry)
				{
					if (cSCompanionData.Unlocked)
					{
						compEntry.ChangeButtonListener(DisplayInfo);
					}
					else
					{
						compEntry.ChangeButtonListener(null);
					}
				}
				else
				{
					compEntry.ChangeButtonListener(delegate
					{
						scrollSnapRect.LerpToPage(GetCompEntryOrderById(compEntry.Id));
					});
				}
			}
		}
	}
}
