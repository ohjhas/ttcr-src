using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialDoubleJumpSystem : IExecuteSystem, ISystem
	{
		private GameContext _gameContext;

		private readonly UIContext _uiContext;

		private bool _isGameWaitingForSingleJump;

		private bool _isGameWaitingForSingleOrDoubleJump;

		private bool _shouldWaitUntilShowingRepeatedTutorial;

		private bool _isGameWaitingForMandatorySingleJump;

		private float lastDistance;

		private readonly float _repeatedTutorialOffset;

		private Sequence closeDialogSequence;

		public TutorialDoubleJumpSystem(Contexts contexts)
		{
			_gameContext = contexts.game;
			_uiContext = contexts.uI;
			_repeatedTutorialOffset = 3f;
			ResetToDefault();
		}

		public void Execute()
		{
			if (!CanExecute())
			{
				return;
			}
			GameEntity playerEntity = _gameContext.playerEntity;
			Vector3 position = playerEntity.view.gameObject.transform.position;
			playerEntity.tutorialPlayStep.StopTime = !playerEntity.hasJumping || playerEntity.jumping.Quantity == 0;
			_shouldWaitUntilShowingRepeatedTutorial = _shouldWaitUntilShowingRepeatedTutorial || !playerEntity.tutorialPlayStep.StopTime;
			if (!_isGameWaitingForSingleJump && !_isGameWaitingForSingleOrDoubleJump && !_shouldWaitUntilShowingRepeatedTutorial && !_isGameWaitingForMandatorySingleJump)
			{
				if (playerEntity.hasJumping && playerEntity.jumping.Quantity >= 2)
				{
					playerEntity.RemoveTutorialPlayStep();
					UnpauseGame();
					return;
				}
				OpenTutorialScreen();
				if (!playerEntity.hasJumping || (playerEntity.hasJumping && playerEntity.jumping.Quantity == 0))
				{
					_isGameWaitingForSingleOrDoubleJump = true;
				}
				else if (playerEntity.hasJumping && playerEntity.jumping.Quantity == 1)
				{
					_isGameWaitingForSingleJump = true;
				}
			}
			else if (_isGameWaitingForSingleOrDoubleJump && playerEntity.hasJumping && playerEntity.jumping.Quantity > 0)
			{
				_isGameWaitingForSingleOrDoubleJump = false;
				CloseTutorialScreen();
				if (playerEntity.jumping.Quantity == 1)
				{
					playerEntity.tutorialPlayStep.StopTime = false;
					_shouldWaitUntilShowingRepeatedTutorial = true;
				}
				else if (playerEntity.jumping.Quantity >= 2)
				{
					SetActionPerformed();
					playerEntity.RemoveTutorialPlayStep();
				}
			}
			else if (_isGameWaitingForSingleJump && playerEntity.hasJumping && playerEntity.jumping.Quantity >= 2)
			{
				_isGameWaitingForSingleJump = false;
				CloseTutorialScreen();
				SetActionPerformed();
				playerEntity.RemoveTutorialPlayStep();
				ResetToDefault();
			}
			else if (_shouldWaitUntilShowingRepeatedTutorial)
			{
				if (playerEntity.hasJumping && playerEntity.jumping.Quantity >= 2)
				{
					_shouldWaitUntilShowingRepeatedTutorial = false;
					SetActionPerformed();
					playerEntity.RemoveTutorialPlayStep();
					ResetToDefault();
					CloseTutorialScreen();
				}
				else if (position.x <= playerEntity.tutorialPlayStep.EndPosX + _repeatedTutorialOffset)
				{
					lastDistance = _gameContext.worldEntity.world.Distance;
				}
				else
				{
					_shouldWaitUntilShowingRepeatedTutorial = false;
					_isGameWaitingForMandatorySingleJump = true;
					_gameContext.worldEntity.world.Distance = lastDistance;
					_gameContext.worldEntity.ReplaceWorldTimeScale(0f);
					OpenTutorialScreen();
				}
			}
			else if (_isGameWaitingForMandatorySingleJump && playerEntity.hasJumping && playerEntity.jumping.Quantity >= 2)
			{
				_isGameWaitingForMandatorySingleJump = false;
				CloseTutorialScreen();
				ResetToDefault();
				SetActionPerformed();
				playerEntity.RemoveTutorialPlayStep();
			}
		}

		private bool CanExecute()
		{
			return HasPlayerDoubleTutorialStepToPerform() && IsPlayerVisibleAndAlive() && HasPlayerReachedEndPos();
		}

		private bool HasPlayerReachedEndPos()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity.view.gameObject.transform.position.x >= playerEntity.tutorialPlayStep.EndPosX;
		}

		private bool IsPlayerVisibleAndAlive()
		{
			return _gameContext.playerEntity.hasView && !_gameContext.playerEntity.isDead;
		}

		private bool HasPlayerDoubleTutorialStepToPerform()
		{
			return _gameContext.playerEntity.hasTutorialPlayStep && _gameContext.playerEntity.tutorialPlayStep.Step == TutorialStep.DoubleJump;
		}

		private void OpenTutorialScreen()
		{
			if (closeDialogSequence != null)
			{
				closeDialogSequence.Kill();
			}
			if (!_uiContext.GetDialog<TutorialScreen>().gameObject.activeInHierarchy)
			{
				((TutorialScreen)_uiContext.OpenDialog<TutorialScreen>(false)).ShowOverlay();
			}
		}

		private void SetActionPerformed()
		{
			TutorialScreen dialog = _uiContext.GetDialog<TutorialScreen>();
			dialog.ActionPerformed();
		}

		private void CloseTutorialScreen()
		{
			UnpauseGame();
			if (closeDialogSequence != null)
			{
				closeDialogSequence.Kill();
			}
			closeDialogSequence = DOTween.Sequence().InsertCallback(0.5f, delegate
			{
				_uiContext.CloseDialog(_uiContext.GetDialog<TutorialScreen>());
				closeDialogSequence = null;
			});
		}

		private void UnpauseGame()
		{
			_gameContext.worldEntity.ReplaceWorldTimeScale(1f);
		}

		private void ResetToDefault()
		{
			_isGameWaitingForSingleJump = false;
			_isGameWaitingForSingleOrDoubleJump = false;
			_shouldWaitUntilShowingRepeatedTutorial = false;
			_isGameWaitingForMandatorySingleJump = false;
		}
	}
}
