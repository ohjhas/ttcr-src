using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialSlideSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext _gameContext;

		private readonly UIContext _uiContext;

		private readonly IGroup<GameEntity> _listeners;

		private readonly IGroup<GameEntity> _tutorialPerformedGroup;

		private readonly IGroup<InputEntity> _inputSlideGroup;

		public TutorialSlideSystem(Contexts contexts)
		{
			_gameContext = contexts.game;
			_uiContext = contexts.uI;
			_listeners = contexts.game.GetGroup(GameMatcher.TutorialsListener);
			_tutorialPerformedGroup = contexts.game.GetGroup(GameMatcher.TutorialPerformed);
			_inputSlideGroup = contexts.input.GetGroup(InputMatcher.AnyOf(InputMatcher.InputSlideStart, InputMatcher.InputSlide, InputMatcher.InputSlideStop));
		}

		public void Execute()
		{
			if (ShouldShowButton())
			{
				InformUI(TutorialStep.ShowSlideButton);
			}
			else
			{
				if (!ShouldShowTutorial())
				{
					return;
				}
				GameEntity playerEntity = _gameContext.playerEntity;
				switch (playerEntity.tutorialPlayStep.Step)
				{
				case TutorialStep.Slide:
					if (_inputSlideGroup.GetEntities().Length == 0)
					{
						bool flag = IsPlayerCloseEnough();
						TutorialScreen dialog = _uiContext.GetDialog<TutorialScreen>();
						if (!(dialog != null))
						{
							break;
						}
						if (!dialog.gameObject.activeInHierarchy && flag)
						{
							dialog = (TutorialScreen)_uiContext.OpenDialog<TutorialScreen>(false);
							if (dialog != null)
							{
								dialog.ShowOverlay();
							}
						}
						if (flag)
						{
							PauseGame();
						}
					}
					else
					{
						UnpauseGame();
						CloseTutorialScreen();
					}
					break;
				case TutorialStep.SlideEnd:
					UnpauseGame();
					CloseTutorialScreen();
					playerEntity.RemoveTutorialPlayStep();
					break;
				}
			}
		}

		private bool IsPlayerCloseEnough()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			if (playerEntity != null && playerEntity.hasView && !playerEntity.isDead)
			{
				return playerEntity.view.gameObject.transform.position.x >= playerEntity.tutorialPlayStep.EndPosX;
			}
			return false;
		}

		private bool ShouldShowButton()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity != null && playerEntity.hasView && !playerEntity.isDead && playerEntity.hasTutorialPlayStep && playerEntity.tutorialPlayStep.Step == TutorialStep.ShowSlideButton;
		}

		private bool ShouldShowTutorial()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			if (playerEntity == null || !playerEntity.hasView || playerEntity.isDead || !playerEntity.hasTutorialPlayStep || (playerEntity.tutorialPlayStep.Step != TutorialStep.Slide && playerEntity.tutorialPlayStep.Step != TutorialStep.SlideEnd))
			{
				return false;
			}
			GameEntity[] entities = _tutorialPerformedGroup.GetEntities();
			GameEntity[] array = entities;
			foreach (GameEntity gameEntity in array)
			{
				if (gameEntity.tutorialPerformed.Step == TutorialStep.Slide)
				{
					return false;
				}
			}
			return true;
		}

		private void CloseTutorialScreen()
		{
			TutorialScreen dialog = _uiContext.GetDialog<TutorialScreen>();
			if (dialog != null)
			{
				dialog.ActionPerformed();
				_uiContext.CloseDialog(dialog);
			}
		}

		private void PauseGame()
		{
			if (!(Time.timeScale < 0.01f))
			{
				_gameContext.worldEntity.ReplaceWorldTimeScale(0f);
			}
		}

		private void UnpauseGame()
		{
			_gameContext.worldEntity.ReplaceWorldTimeScale(1f);
		}

		private void InformUI(TutorialStep tutorialStepAction)
		{
			GameEntity[] entities = _listeners.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				gameEntity.tutorialsListener.listener.OnTutorialAction(tutorialStepAction);
			}
		}
	}
}
