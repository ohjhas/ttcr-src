using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class WSLevelEntry : MonoBehaviour
	{
		public enum State
		{
			Unlocked = 0,
			Locked = 1,
			LatestUnlocked = 2
		}

		public Transform CharacterPosition;

		public RectTransform EntryRT;

		[SerializeField]
		private List<GameObject> unlockedElements;

		[SerializeField]
		private List<GameObject> lockedElements;

		[SerializeField]
		private List<GameObject> latestUnlockedElements;

		[SerializeField]
		private Button EntryButton;

		[SerializeField]
		private CanvasParticleEmitter UnlockParticles;

		[SerializeField]
		private WSMissionNumber MissionNumber;

		[SerializeField]
		private WSStarsController StarsController;

		[SerializeField]
		private WSChestIcon ChestIcon;

		[SerializeField]
		private List<HighlightWithPulsating> PulsatingEffects;

		[SerializeField]
		private Material PulsatingMaterial;

		private Sprite currentIcon;

		private int entryId = -1;

		private Action<int> pressCallback;

		public bool IsHiglighted { get; private set; }

		public int EntryId
		{
			get
			{
				return entryId;
			}
		}

		public void SetData(int _entryId, WSLevelData levelData, Action<int> _pressCallback)
		{
			entryId = _entryId;
			pressCallback = _pressCallback;
			SetOtherElements(State.Unlocked);
			MissionNumber.SetData(entryId + 1);
			StarsController.SetData(levelData.Stars);
		}

		public void OnMissionPressed()
		{
			if (pressCallback != null)
			{
				pressCallback(entryId);
			}
		}

		public void RefreshState(int selectedEntryId, int unlockedLevelId)
		{
			bool flag = entryId == unlockedLevelId;
			bool flag2 = entryId <= unlockedLevelId;
			bool flag3 = entryId == selectedEntryId;
			StopPulsating();
			if (flag)
			{
				SetLatestUnlocked(flag3);
			}
			else if (flag2)
			{
				SetUnlocked(flag3);
			}
			else
			{
				SetLocked();
			}
		}

		public void MakeTemporaryInactive()
		{
			MissionNumber.SetSelection(false);
			StarsController.SetState(false);
			EntryButton.interactable = false;
			ChestIcon.SetState(false);
			SetOtherElements(State.Locked);
			EntryRT.localScale = Vector3.one * 0.75f;
		}

		public void SetLocked()
		{
			MissionNumber.SetSelection(false);
			StarsController.SetState(false);
			EntryButton.interactable = false;
			ChestIcon.SetState(true);
			SetOtherElements(State.Locked);
			EntryRT.localScale = Vector3.one * 0.75f;
		}

		public void UnlockNewLevel()
		{
			MissionNumber.SetSelection(true);
			StarsController.SetState(true);
			StarsController.SetSelection(true, State.LatestUnlocked);
			EntryButton.interactable = true;
			ChestIcon.SetState(true);
			SetOtherElements(State.LatestUnlocked);
			EntryRT.localScale = Vector3.one * 1f;
			ShowParticles();
		}

		public void SetLatestUnlocked(bool isSelected)
		{
			MissionNumber.SetSelection(isSelected);
			StarsController.SetState(true);
			StarsController.SetSelection(isSelected, State.LatestUnlocked);
			EntryButton.interactable = true;
			ChestIcon.SetState(true);
			SetOtherElements(State.LatestUnlocked);
			EntryRT.localScale = ((!isSelected) ? (Vector3.one * 0.75f) : Vector3.one);
			StartPulsating();
		}

		public void SetPosition(RectTransform parent)
		{
			EntryRT.SetParent(parent);
			EntryRT.localScale = Vector3.one;
			EntryRT.anchorMin = Vector2.zero;
			EntryRT.anchorMax = Vector2.one;
			EntryRT.anchoredPosition3D = Vector3.zero;
			EntryRT.sizeDelta = Vector2.zero;
		}

		public void SetupChestReward(WSLevelData levelData, List<ChestData> chestsData)
		{
			ChestData? chestData = null;
			if (chestsData.Exists((ChestData ch) => ch.Id == levelData.ChestRewardId))
			{
				chestData = chestsData.Find((ChestData ch) => ch.Id == levelData.ChestRewardId);
			}
			ChestIcon.Setup(!levelData.ChestCollected, chestData);
		}

		private void StartPulsating()
		{
			if (!TutorialHelpers.IsTutorialActive())
			{
				PulsatingEffects.ForEach(delegate(HighlightWithPulsating x)
				{
					x.transform.localScale = 0.9f * Vector3.one;
					x.Init(1f, 1.1f, 1f, PulsatingMaterial);
				});
				IsHiglighted = true;
			}
		}

		private void StopPulsating()
		{
			if (!TutorialHelpers.IsTutorialActive())
			{
				PulsatingEffects.ForEach(delegate(HighlightWithPulsating x)
				{
					x.transform.localScale = 0.9f * Vector3.one;
					x.Disable();
				});
				IsHiglighted = false;
			}
		}

		private void SetUnlocked(bool isSelected)
		{
			MissionNumber.SetSelection(isSelected);
			StarsController.SetState(true);
			StarsController.SetSelection(isSelected, State.Unlocked);
			EntryButton.interactable = true;
			ChestIcon.SetState(true);
			SetOtherElements(State.Unlocked);
			EntryRT.localScale = ((!isSelected) ? (Vector3.one * 0.75f) : Vector3.one);
		}

		private void SetOtherElements(State state)
		{
			foreach (GameObject lockedElement in lockedElements)
			{
				lockedElement.SetActive(state == State.Locked);
			}
			foreach (GameObject unlockedElement in unlockedElements)
			{
				unlockedElement.SetActive(state == State.Unlocked);
			}
			foreach (GameObject latestUnlockedElement in latestUnlockedElements)
			{
				latestUnlockedElement.SetActive(state == State.LatestUnlocked);
			}
		}

		private void ShowParticles()
		{
			UnlockParticles.enabled = true;
			UnlockParticles.RestartEmitter();
		}
	}
}
