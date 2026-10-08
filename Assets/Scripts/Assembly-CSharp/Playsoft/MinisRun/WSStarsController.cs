using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSStarsController
	{
		[SerializeField]
		private RectTransform ContainerRT;

		[SerializeField]
		private List<RectTransform> stars;

		[SerializeField]
		private GameObject FirstStarFill;

		[SerializeField]
		private GameObject SecondStarFill;

		[SerializeField]
		private GameObject ThirdStarFill;

		[SerializeField]
		private GameObject LockIcon;

		[SerializeField]
		private List<RectTransform> PositionsSelected;

		[SerializeField]
		private List<RectTransform> PositionsLatestUnlocked;

		[SerializeField]
		private List<RectTransform> PositionsOtherCases;

		private Sequence sequence;

		private bool shouldSetPosInsteadOfTween = true;

		public void SetData(int numberOfStars)
		{
			FirstStarFill.SetActive(numberOfStars > 0);
			SecondStarFill.SetActive(numberOfStars > 1);
			ThirdStarFill.SetActive(numberOfStars > 2);
		}

		public void SetState(bool isEnabled)
		{
			ContainerRT.gameObject.SetActive(isEnabled);
			LockIcon.SetActive(!isEnabled);
		}

		public void SetSelection(bool isSelected, WSLevelEntry.State state)
		{
			List<RectTransform> list = PositionsOtherCases;
			if (isSelected)
			{
				list = PositionsSelected;
			}
			else if (state == WSLevelEntry.State.LatestUnlocked)
			{
				list = PositionsLatestUnlocked;
			}
			if (shouldSetPosInsteadOfTween)
			{
				shouldSetPosInsteadOfTween = false;
				SetPos(list);
			}
			else
			{
				TweenToPos(list);
			}
		}

		private void TweenToPos(List<RectTransform> position)
		{
			SettingsWorldSelectSO setting = SettingsService.GetSetting<SettingsWorldSelectSO>();
			sequence.Kill();
			sequence = DOTween.Sequence().Append(stars[0].DOAnchorPos(position[0].anchoredPosition, setting.WSStarFlyAnimationDuration).SetEase(setting.WSStarFlyAnimationCurve)).Insert(setting.WSStarsFlyAnimationOffset, stars[1].DOAnchorPos(position[1].anchoredPosition, setting.WSStarFlyAnimationDuration).SetEase(setting.WSStarFlyAnimationCurve))
				.Insert(2f * setting.WSStarsFlyAnimationOffset, stars[2].DOAnchorPos(position[2].anchoredPosition, setting.WSStarFlyAnimationDuration).SetEase(setting.WSStarFlyAnimationCurve));
		}

		private void SetPos(List<RectTransform> position)
		{
			for (int i = 0; i < stars.Count; i++)
			{
				stars[i].anchoredPosition = position[i].GetComponent<RectTransform>().anchoredPosition;
			}
		}
	}
}
