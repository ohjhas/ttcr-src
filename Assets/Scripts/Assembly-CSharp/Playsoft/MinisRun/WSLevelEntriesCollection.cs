using System;
using System.Collections.Generic;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSLevelEntriesCollection : BaseCollectionView<WSLevelEntry, WSLevelData>
	{
		private Action<int> onEntryPressCallback;

		public void SetupEntries(List<WSLevelData> dataList, List<ChestData> chestData, WSChapterBackgrounds ChapterBgs, int unlockedLevelId)
		{
			for (int i = 0; i < Entries.Count; i++)
			{
				if (i < dataList.Count)
				{
					Entries[i].gameObject.SetActive(true);
					InitializeEntry(i, dataList[i]);
					Entries[i].SetPosition(ChapterBgs.GetLevelEntrySlot(dataList[i].Chapter, i));
					Entries[i].SetupChestReward(dataList[i], chestData);
				}
				else
				{
					Entries[i].gameObject.SetActive(false);
				}
			}
		}

		protected override void InitializeEntry(int index, WSLevelData data)
		{
			Entries[index].SetData(index, data, onEntryPressCallback);
		}

		public void SetupEntryPressCallback(Action<int> _onEntryPressCallback)
		{
			onEntryPressCallback = _onEntryPressCallback;
		}

		public void SetupEntriesState(int levelsCount, int unlockedLevelId, int currentlySelectedId, bool newLevelUnlockAnimation)
		{
			for (int i = 0; i < levelsCount; i++)
			{
				if (i == unlockedLevelId)
				{
					if (newLevelUnlockAnimation)
					{
						Entries[i].MakeTemporaryInactive();
					}
					else
					{
						Entries[i].SetLatestUnlocked(currentlySelectedId == i);
					}
				}
				else if (i > unlockedLevelId)
				{
					Entries[i].SetLocked();
				}
				else if (i == unlockedLevelId - 1 && newLevelUnlockAnimation)
				{
					Entries[i].SetLatestUnlocked(true);
				}
				else
				{
					Entries[i].RefreshState(currentlySelectedId, unlockedLevelId);
				}
			}
		}

		public void UnlockEntry(int currentlySelectedId)
		{
			Entries[currentlySelectedId].UnlockNewLevel();
			int num = currentlySelectedId - 1;
			if (num >= 0)
			{
				Entries[num].RefreshState(currentlySelectedId, currentlySelectedId);
			}
		}

		public void RefreshEntryState(int entryId, int selectedEntryId, int unlockedLevelId)
		{
			Entries[entryId].RefreshState(selectedEntryId, unlockedLevelId);
		}
	}
}
