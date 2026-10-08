using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSScrollView
	{
		private const float MAX_CONTENT_POS = 0f;

		[SerializeField]
		private ScrollRect ScrollRect;

		[SerializeField]
		private RectTransform Content;

		private WSLevelEntriesCollection entriesCollection;

		private bool hasReachedTarget = true;

		private float targetId;

		private float currentPosId;

		private float minContentPos;

		private SettingsWorldSelectSO worldSelectSettings;

		public void SetEntries(WSLevelEntriesCollection _entriesCollection)
		{
			entriesCollection = _entriesCollection;
			minContentPos = 0f - GetPositionEntry(entriesCollection.GetEntries().Count - 1);
		}

		public void UpdateScrollView()
		{
			if (!IsReachedTarget())
			{
				UpdatePosition();
			}
			ClampScrollView();
		}

		public void ClampScrollView()
		{
			if (entriesCollection != null)
			{
				float num = Mathf.Clamp(Content.anchoredPosition.x, minContentPos, 0f);
				bool flag = Content.anchoredPosition.x != num;
				Content.anchoredPosition = new Vector2(num, 0f);
				if (flag)
				{
					ScrollRect.velocity = Vector2.zero;
				}
			}
		}

		public void CenterOnSelectedWorld(float focusIndex)
		{
			float a = 0f - GetPositionEntry(Mathf.FloorToInt(focusIndex));
			float b = 0f - GetPositionEntry(Mathf.CeilToInt(focusIndex));
			float x = Mathf.Lerp(a, b, focusIndex - (float)Mathf.FloorToInt(focusIndex));
			Content.anchoredPosition = new Vector2(x, 0f);
		}

		public void SetScrollViewState(bool enabled)
		{
			ScrollRect.enabled = enabled;
		}

		public void UpdatePosition()
		{
			if (ScrollRect.velocity.magnitude > 0f)
			{
				hasReachedTarget = true;
				return;
			}
			GetWorldSelectSettingsIfNeeded();
			if (targetId > currentPosId)
			{
				currentPosId += worldSelectSettings.ScrollSpeed * Time.deltaTime;
				if (currentPosId >= targetId)
				{
					currentPosId = targetId;
					hasReachedTarget = true;
				}
			}
			else
			{
				currentPosId -= worldSelectSettings.ScrollSpeed * Time.deltaTime;
				if (currentPosId <= targetId)
				{
					currentPosId = targetId;
					hasReachedTarget = true;
				}
			}
			CenterOnSelectedWorld(currentPosId);
		}

		private void GetWorldSelectSettingsIfNeeded()
		{
			if (worldSelectSettings == null)
			{
				worldSelectSettings = SettingsService.GetSetting<SettingsWorldSelectSO>();
			}
		}

		public void SetScrollTarget(float currentlySelectedId)
		{
			targetId = currentlySelectedId;
			currentPosId = GetPositionIndex();
			ScrollRect.velocity = Vector2.zero;
			hasReachedTarget = false;
		}

		public bool IsReachedTarget()
		{
			return hasReachedTarget;
		}

		public RectTransform GetViewContent()
		{
			return Content;
		}

		public float GetPositionIndex()
		{
			if (entriesCollection == null)
			{
				return 0f;
			}
			List<WSLevelEntry> entries = entriesCollection.GetEntries();
			float num = Mathf.Abs(Content.anchoredPosition.x);
			for (int i = 1; i < entries.Count; i++)
			{
				float positionEntry = GetPositionEntry(i);
				if (positionEntry >= num)
				{
					float positionEntry2 = GetPositionEntry(i - 1);
					return (float)i - Mathf.Clamp01((positionEntry - num) / (positionEntry - positionEntry2));
				}
			}
			return 0f;
		}

		private float GetPositionEntry(int index)
		{
			List<WSLevelEntry> entries = entriesCollection.GetEntries();
			RectTransform entryRT = entries[index].EntryRT;
			RectTransform component = entryRT.parent.parent.parent.GetComponent<RectTransform>();
			RectTransform component2 = entryRT.parent.GetComponent<RectTransform>();
			return component.anchoredPosition.x + component2.anchoredPosition.x - Content.rect.size.x * 0.5f;
		}
	}
}
