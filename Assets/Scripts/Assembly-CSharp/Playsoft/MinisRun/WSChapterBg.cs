using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WSChapterBg : MonoBehaviour
	{
		[SerializeField]
		private RectTransform chapterRT;

		[SerializeField]
		private List<RectTransform> Slots;

		public float Size
		{
			get
			{
				return chapterRT.sizeDelta.x;
			}
		}

		public float Offset { get; private set; }

		private void Awake()
		{
			base.name = base.name.Replace("(Clone)", string.Empty);
			ResourceRequest loadBgRequest = Resources.LoadAsync<RectTransform>("UI/Components/Chapters/" + base.name + "Bg");
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.AddAsyncRequest(loadBgRequest, delegate
			{
				RectTransform original = (RectTransform)loadBgRequest.asset;
				RectTransform rectTransform = Object.Instantiate(original, chapterRT);
				rectTransform.SetAsFirstSibling();
			});
		}

		public int GetSlotsCount()
		{
			return Slots.Count;
		}

		public RectTransform GetLevelEntrySlot(int index)
		{
			return Slots[index];
		}

		public void SetOffset(float _offest)
		{
			Offset = _offest;
			chapterRT.anchoredPosition = new Vector2(Offset, 0f);
		}
	}
}
