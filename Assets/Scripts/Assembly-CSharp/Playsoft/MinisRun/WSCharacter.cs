using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSCharacter
	{
		private const float ENTRIES_VISIBLE_ON_THE_SCREEN = 3f;

		[SerializeField]
		private float unlockAnimDelay;

		[SerializeField]
		private float unlockLevelAnimTime;

		[SerializeField]
		private float characterSize;

		[SerializeField]
		private AnimationCurve unlockLevelAnimYCurve;

		private RectTransform runningCharacter;

		private Animator runningCharacterAnimator;

		private CharacterAnimatorController characterAnimatorController;

		private Transform parent;

		private Action createdCallback;

		private float unlockLevelTimer;

		private float lastAnimFactor;

		private int lastRightId;

		private bool hasReachedTarget;

		private int assignedId;

		private float charPosId;

		private float currentViewPos;

		private int charIndex;

		private bool isCharacterRequested;

		private SettingsWorldSelectSO worldSelectSettings;

		public bool IsCharacterReady
		{
			get
			{
				return runningCharacter != null;
			}
		}

		public void CreateCharacter(Transform parent, int selectedCharacter, Action callback)
		{
			this.parent = parent;
			createdCallback = callback;
			if (!(runningCharacter != null) && !isCharacterRequested)
			{
				if (worldSelectSettings == null)
				{
					worldSelectSettings = SettingsService.GetSetting<SettingsWorldSelectSO>();
				}
				charIndex = selectedCharacter;
				Contexts.sharedInstance.uI.GetCharacterUIAssetAsync(charIndex, SetCharacter);
				isCharacterRequested = true;
			}
		}

		public void UpdateCharacter(List<WSLevelEntry> levelEntries, int currentId)
		{
			if (IsReachedTarget())
			{
				AsignCharToEntry(levelEntries, currentId);
			}
			else
			{
				UpdatePosition(levelEntries);
			}
		}

		public void SetTargetPosition(List<WSLevelEntry> levelEntries, int currentId)
		{
			hasReachedTarget = false;
			charPosId = (float)currentId - Mathf.Clamp((float)currentId - currentViewPos, -3f, 3f);
			assignedId = currentId;
			currentViewPos = charPosId;
		}

		public void AsignCharToEntry(List<WSLevelEntry> levelEntries, int currentId)
		{
			if (runningCharacter != null)
			{
				runningCharacter.position = levelEntries[currentId].CharacterPosition.position;
				runningCharacter.SetAsLastSibling();
				hasReachedTarget = true;
				assignedId = currentId;
				charPosId = assignedId;
				currentViewPos = charPosId;
				ChangeAnimation(CharAnimationType.Idle);
			}
		}

		public bool IsReachedTarget()
		{
			return hasReachedTarget;
		}

		public float AnimateCharacter(List<WSLevelEntry> levelEntries, int currentlySelectedId)
		{
			if (runningCharacter != null)
			{
				unlockLevelTimer += Time.deltaTime;
				float num = 0f;
				if (unlockLevelTimer >= 0f)
				{
					int leftPosId = Mathf.Max(0, currentlySelectedId - 1);
					num = Mathf.Clamp01(unlockLevelTimer / unlockLevelAnimTime);
					UpdateCharRotation(true);
					Vector3 currentPos = LerpPositionBetweenEntries(levelEntries, leftPosId, currentlySelectedId, num);
					UpdateCharPos(currentPos);
					ChangeAnimation(CharAnimationType.Running);
				}
				return num;
			}
			return 0f;
		}

		public void InitTimer(bool shouldWait)
		{
			unlockLevelTimer -= ((!shouldWait) ? 0f : unlockAnimDelay);
		}

		public Vector2 GetPosition()
		{
			return (!(runningCharacter != null)) ? Vector2.zero : runningCharacter.anchoredPosition;
		}

		public float GetRotation()
		{
			return (!(runningCharacter != null)) ? 1f : runningCharacter.localScale.x;
		}

		private void SetCharacter(RectTransform charRect)
		{
			runningCharacter = UnityEngine.Object.Instantiate(charRect);
			runningCharacter.SetParent(parent);
			runningCharacter.localScale = Vector3.one;
			runningCharacter.pivot = new Vector2(0.5f, 0f);
			RectTransform rectTransform = runningCharacter;
			Vector2 vector = new Vector2(0.5f, 0.5f);
			runningCharacter.anchorMax = vector;
			rectTransform.anchorMin = vector;
			runningCharacter.sizeDelta = new Vector2(characterSize, characterSize);
			runningCharacterAnimator = runningCharacter.GetComponent<Animator>();
			characterAnimatorController = runningCharacter.GetComponent<CharacterAnimatorController>();
			if (characterAnimatorController != null)
			{
				characterAnimatorController.PrefetchAnimationAsync(charIndex, CharAnimationType.Running);
			}
			Image[] componentsInChildren = runningCharacter.GetComponentsInChildren<Image>();
			Image[] array = componentsInChildren;
			foreach (Image image in array)
			{
				image.raycastTarget = false;
			}
			createdCallback();
		}

		private void UpdatePosition(List<WSLevelEntry> levelEntries)
		{
			if (runningCharacter != null)
			{
				float num = Mathf.Sign((float)assignedId - charPosId);
				currentViewPos += worldSelectSettings.CharacterSpeed * num * Time.deltaTime;
				SetupCharacterPosition(levelEntries, Mathf.Clamp(currentViewPos, 0f, levelEntries.Count - 1));
				charPosId = ((!(num > 0f)) ? Mathf.CeilToInt(currentViewPos) : Mathf.FloorToInt(currentViewPos));
				ChangeAnimation(CharAnimationType.Running);
				if (Mathf.Approximately(assignedId, charPosId))
				{
					AsignCharToEntry(levelEntries, assignedId);
					UpdateCharRotation(true);
				}
			}
		}

		private void SetupCharacterPosition(List<WSLevelEntry> levelEntries, float viewPos)
		{
			if (!(runningCharacter != null))
			{
				return;
			}
			int num = Mathf.FloorToInt(viewPos);
			int num2 = Mathf.CeilToInt(viewPos);
			float num3 = viewPos - (float)num;
			CheckIfEntriesChanged(num2, num3);
			if (num != num2)
			{
				if (!Mathf.Approximately(lastAnimFactor, num3))
				{
					UpdateCharRotation(ShouldCharHaveRightDirection(num3));
				}
				Vector3 currentPos = LerpPositionBetweenEntries(levelEntries, num, num2, num3);
				UpdateCharPos(currentPos);
			}
			lastAnimFactor = num3;
			lastRightId = num2;
		}

		private void CheckIfEntriesChanged(int rightId, float animFactor)
		{
			if (rightId != lastRightId)
			{
				lastAnimFactor = animFactor;
			}
		}

		private void UpdateCharRotation(bool rightDirection)
		{
			if (!(null == runningCharacter))
			{
				float num = Mathf.Abs(runningCharacter.localScale.x) * (float)(rightDirection ? 1 : (-1));
				if (!Mathf.Approximately(runningCharacter.localScale.x, num))
				{
					runningCharacter.localScale = new Vector3(num, runningCharacter.localScale.y, runningCharacter.localScale.z);
				}
			}
		}

		private void UpdateCharPos(Vector3 currentPos)
		{
			if (!(null == runningCharacter) && runningCharacter.position != currentPos)
			{
				runningCharacter.position = currentPos;
			}
		}

		private Vector3 LerpPositionBetweenEntries(List<WSLevelEntry> levelEntries, int leftPosId, int rightPosId, float animFactor)
		{
			Vector3 position = levelEntries[leftPosId].CharacterPosition.position;
			Vector3 position2 = levelEntries[rightPosId].CharacterPosition.position;
			return new Vector3(Mathf.Lerp(position.x, position2.x, animFactor), Mathf.Lerp(position.y, position2.y, unlockLevelAnimYCurve.Evaluate(animFactor)), position2.z);
		}

		private bool ShouldCharHaveRightDirection(float animFactor)
		{
			return lastAnimFactor <= animFactor;
		}

		private void ChangeAnimation(CharAnimationType animType)
		{
			if (characterAnimatorController != null)
			{
				if (characterAnimatorController.GetCurrentAnimation() != animType)
				{
					characterAnimatorController.ForceAnimation(charIndex, animType);
				}
			}
			else if (runningCharacterAnimator != null)
			{
				switch (animType)
				{
				case CharAnimationType.Idle:
					runningCharacterAnimator.Play("UIIdleAnim");
					break;
				case CharAnimationType.Running:
					runningCharacterAnimator.Play("UIRunAnim");
					break;
				}
			}
		}
	}
}
