using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSRaccoon
	{
		[SerializeField]
		private float animDelay;

		[SerializeField]
		private float runAnimDuration;

		[SerializeField]
		private float characterSize;

		[SerializeField]
		private AnimationCurve runAnimYCurve;

		private RectTransform Raccoon;

		private Animator RaccoonAnimator;

		private Transform parent;

		private float animTimer;

		private float lastAnimFactor;

		private int lastRightId;

		private bool isJumpStarted;

		private Vector3 raccoonPosAtJumpStart;

		private SettingsWorldSelectSO worldSelectSettings;

		public void CreateRaccoon(Transform parent)
		{
			this.parent = parent;
			Remove();
			if (worldSelectSettings == null)
			{
				worldSelectSettings = SettingsService.GetSetting<SettingsWorldSelectSO>();
			}
			Contexts.sharedInstance.uI.GetRacoonUIAssetAsync(SetRaccoon);
		}

		public void Remove()
		{
			if (Raccoon != null)
			{
				UnityEngine.Object.Destroy(Raccoon.gameObject);
			}
		}

		public void UpdateRaccoon(List<WSLevelEntry> levelEntries, int currentId, Action unlockEntryCallback)
		{
			if (Raccoon != null)
			{
				float num = AnimateRaccoon(levelEntries, currentId);
				if (num >= 0.85f)
				{
					unlockEntryCallback();
				}
				if (num >= 1f)
				{
					Remove();
				}
			}
		}

		public void AsignRaccoonToEntry(List<WSLevelEntry> levelEntries, int currentId)
		{
			if (Raccoon != null)
			{
				Raccoon.position = levelEntries[currentId].CharacterPosition.position;
				ChangeAnimation(RaccoonAnimationType.Waiting);
			}
		}

		public void InitTimer()
		{
			animTimer = 0f - animDelay;
		}

		private void SetRaccoon(RectTransform raccoon)
		{
			Raccoon = UnityEngine.Object.Instantiate(raccoon);
			Raccoon.SetParent(parent);
			Raccoon.localScale = Vector3.one;
			Raccoon.pivot = new Vector2(0.5f, 0f);
			RectTransform raccoon2 = Raccoon;
			Vector2 vector = new Vector2(0.5f, 0.5f);
			Raccoon.anchorMax = vector;
			raccoon2.anchorMin = vector;
			Raccoon.sizeDelta = new Vector2(characterSize, characterSize);
			RaccoonAnimator = Raccoon.GetComponent<Animator>();
			Image[] componentsInChildren = Raccoon.GetComponentsInChildren<Image>();
			Image[] array = componentsInChildren;
			foreach (Image image in array)
			{
				image.raycastTarget = false;
			}
		}

		private void UpdateRaccoonPos(Vector3 currentPos)
		{
			if (!(Raccoon == null) && Raccoon.position != currentPos)
			{
				Raccoon.position = currentPos;
			}
		}

		private float AnimateRaccoon(List<WSLevelEntry> levelEntries, int currentlySelectedId)
		{
			animTimer += Time.deltaTime;
			int leftPosId = Mathf.Max(0, currentlySelectedId - 1);
			float num = Mathf.Clamp01(animTimer / runAnimDuration);
			Vector3 currentPos = LerpPositionBetweenEntries(levelEntries, leftPosId, currentlySelectedId, num);
			UpdateRaccoonPos(currentPos);
			if (num >= 0.35f)
			{
				ChangeAnimation(RaccoonAnimationType.Jumping);
			}
			else if (num > 0f)
			{
				ChangeAnimation(RaccoonAnimationType.Running);
			}
			return num;
		}

		private Vector3 LerpPositionBetweenEntries(List<WSLevelEntry> levelEntries, int leftPosId, int rightPosId, float animFactor)
		{
			Vector3 position = levelEntries[leftPosId].CharacterPosition.position;
			Vector3 position2 = levelEntries[rightPosId].CharacterPosition.position;
			return new Vector3(Mathf.Lerp(position.x, position2.x, animFactor), Mathf.Lerp(position.y, position2.y, runAnimYCurve.Evaluate(animFactor)), position2.z);
		}

		private void ChangeAnimation(RaccoonAnimationType animType)
		{
			if (!(RaccoonAnimator == null))
			{
				switch (animType)
				{
				case RaccoonAnimationType.Waiting:
					RaccoonAnimator.Play("UIWait");
					break;
				case RaccoonAnimationType.Running:
					RaccoonAnimator.Play("UIRun");
					break;
				case RaccoonAnimationType.Jumping:
					RaccoonAnimator.Play("UIJump");
					break;
				}
			}
		}
	}
}
