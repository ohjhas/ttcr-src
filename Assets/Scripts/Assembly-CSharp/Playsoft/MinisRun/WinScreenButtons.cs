using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WinScreenButtons
	{
		[SerializeField]
		private Button replayButton;

		[SerializeField]
		private Button nextButton;

		private WinScreen winScreen;

		public void Init(WinScreen screen, bool chestWon, int numberOfStars)
		{
			winScreen = screen;
			AddButtonsListeners();
			replayButton.transform.parent.transform.localScale = Vector3.zero;
			nextButton.transform.parent.transform.localScale = Vector3.zero;
			if (!chestWon)
			{
				winScreen.StartCoroutine(ShowButtons(1f + (float)numberOfStars * 0.3f));
			}
			else
			{
				winScreen.StartCoroutine(ShowButtonsAfterChestReveal());
			}
		}

		private IEnumerator ShowButtonsAfterChestReveal()
		{
			yield return new WaitUntil(IsChestRevealVisible);
			yield return new WaitUntil(IsChestRevealDisappeared);
			yield return winScreen.StartCoroutine(ShowButtons(0.5f));
		}

		private IEnumerator ShowButtons(float delay = 0f)
		{
			yield return new WaitForSecondsRealtime(delay);
			GameEntity world = Contexts.sharedInstance.game.worldEntity;
			LevelTutorialSettings levelSettings = world.levelSettings.levelTutorialSettings;
			replayButton.gameObject.SetActive(levelSettings.ResultSettings.RestartButton);
			nextButton.gameObject.SetActive(true);
			replayButton.transform.parent.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
			nextButton.transform.parent.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack).SetUpdate(true);
		}

		private void AddButtonsListeners()
		{
			replayButton.onClick.RemoveAllListeners();
			replayButton.onClick.AddListener(ReplayButtonPressed);
			nextButton.onClick.RemoveAllListeners();
			nextButton.onClick.AddListener(NextButtonPressed);
		}

		private void NextButtonPressed()
		{
			Contexts.sharedInstance.game.worldEntity.isDead = true;
			Contexts.sharedInstance.state.CreateEntity().AddPassLevelAction(TutorialOutgameLevel.PlayAgain);
		}

		private void ReplayButtonPressed()
		{
			Contexts.sharedInstance.game.worldEntity.isDead = true;
			Contexts.sharedInstance.game.worldEntity.isReplayLevel = true;
		}

		private bool IsChestRevealVisible()
		{
			return Contexts.sharedInstance.uI != null && Contexts.sharedInstance.uI.uIEntity != null && Contexts.sharedInstance.uI.uIEntity != null && Contexts.sharedInstance.uI.FindCurrentScreen() is ChestRevealScreen;
		}

		private bool IsChestRevealDisappeared()
		{
			return Contexts.sharedInstance.uI != null && Contexts.sharedInstance.uI.uIEntity != null && Contexts.sharedInstance.uI.uIEntity != null && Contexts.sharedInstance.uI.FindCurrentScreen() is WinScreen;
		}
	}
}
