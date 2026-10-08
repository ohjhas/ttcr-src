using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSChapterGates : BaseCollectionView<WSChapterGate, WSChapterData>
	{
		public void SetupEntries(List<WSChapterData> dataList, WSChapterBackgrounds chapterBackgrounds)
		{
			for (int i = 1; i < Entries.Count; i++)
			{
				if (i < dataList.Count)
				{
					Entries[i].gameObject.SetActive(true);
					InitializeEntry(i, dataList[i]);
					SetupPosition(i, chapterBackgrounds.GetChapterOffset(i - 1), chapterBackgrounds.GetChapterSize(i - 1));
				}
				else
				{
					Entries[i].gameObject.SetActive(false);
				}
			}
		}

		public void SetupEntriesState(int gatesCount, int collectedStars, int currentChapterId, bool newChapterUnlocked, Action notEnoughStarsCallback)
		{
			for (int i = 1; i < gatesCount; i++)
			{
				Entries[i].SetCollectedStars(collectedStars);
				Entries[i].SetState(i == currentChapterId && newChapterUnlocked, notEnoughStarsCallback);
			}
		}

		protected override void InitializeEntry(int index, WSChapterData data)
		{
			Entries[index].SetData(data.Name, data.RequiredStars);
		}

		private void SetupPosition(int index, float offset, float chapterSize)
		{
			offset = ((index != 0) ? offset : 0f) + chapterSize;
			Entries[index].GateRT.anchoredPosition = new Vector2(offset, 0f);
			Entries[index].GateRT.sizeDelta = new Vector2(Entries[index].GateRT.sizeDelta.x, 0f);
			Entries[index].transform.SetAsLastSibling();
		}
	}
}
