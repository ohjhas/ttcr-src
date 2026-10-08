using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSChapterBackgrounds : BaseCollectionView<WSChapterBg, WSChapterData>
	{
		protected override void InitializeEntry(int index, WSChapterData data)
		{
		}

		public override void CreateEntriesIfNeeded(Transform parent, int count)
		{
			ChapterSettingsSO[] chapterSettings = SettingsService.SettingsSO.ChapterSettings;
			while (Entries.Count < count)
			{
				WSChapterBg original = Resources.Load<WSChapterBg>("UI/Components/Chapters/" + chapterSettings[Entries.Count].ChapterPrefabName);
				WSChapterBg wSChapterBg = UnityEngine.Object.Instantiate(original);
				RectTransform component = wSChapterBg.GetComponent<RectTransform>();
				component.SetParent(parent);
				component.localScale = Vector3.one;
				component.anchoredPosition3D = Vector3.zero;
				component.sizeDelta = Vector2.zero;
				Entries.Add(wSChapterBg);
			}
		}

		public void SetupEntriesPosition(int chaptersCount)
		{
			float num = 0f;
			for (int i = 0; i < chaptersCount; i++)
			{
				Entries[i].SetOffset(num);
				num += Entries[i].Size;
			}
		}

		public float GetChapterOffset(int index)
		{
			return (index >= 0) ? Entries[index].Offset : 0f;
		}

		public float GetChapterSize(int index)
		{
			return (index >= 0) ? Entries[index].Size : 0f;
		}

		public RectTransform GetLevelEntrySlot(int chapterId, int index)
		{
			for (int i = 0; i < chapterId; i++)
			{
				index -= Entries[i].GetSlotsCount();
			}
			return Entries[chapterId].GetLevelEntrySlot(index);
		}
	}
}
