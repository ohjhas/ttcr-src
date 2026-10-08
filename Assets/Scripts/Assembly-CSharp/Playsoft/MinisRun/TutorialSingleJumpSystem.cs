using DG.Tweening;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialSingleJumpSystem : IExecuteSystem, ISystem
	{
		private GameContext _gameContext;

		private readonly IGroup<GameEntity> _listeners;

		private readonly IGroup<GameEntity> _tutorialPerformedGroup;

		private readonly UIContext _uiContext;

		private bool _isTriggered;

		private float _currentTimeScale;

		public TutorialSingleJumpSystem(Contexts contexts)
		{
			_gameContext = contexts.game;
			_listeners = contexts.game.GetGroup(GameMatcher.TutorialsListener);
			_tutorialPerformedGroup = contexts.game.GetGroup(GameMatcher.TutorialPerformed);
			_uiContext = contexts.uI;
			_isTriggered = false;
		}

		public void Execute()
		{
			if (ShouldShowButton())
			{
				InformUI(TutorialStep.ShowJumpButton);
			}
			else if (ShouldShowTutorial())
			{
				GameEntity playerEntity = _gameContext.playerEntity;
				if (playerEntity.view.gameObject.transform.position.x < playerEntity.tutorialPlayStep.EndPosX)
				{
					return;
				}
				if (!_isTriggered)
				{
					if (!playerEntity.hasJumping || playerEntity.jumping.Quantity < 1)
					{
						((TutorialScreen)_uiContext.OpenDialog<TutorialScreen>(false)).ShowOverlay();
						_isTriggered = true;
					}
					else
					{
						playerEntity.RemoveTutorialPlayStep();
						_isTriggered = false;
					}
				}
				else if (_isTriggered && playerEntity.hasJumping && playerEntity.jumping.Quantity > 0)
				{
					UnpauseGame();
					TutorialScreen tutorialScreen = _uiContext.GetDialog<TutorialScreen>();
					tutorialScreen.ActionPerformed();
					DOTween.Sequence().InsertCallback(0.5f, delegate
					{
						_uiContext.CloseDialog(tutorialScreen);
					});
					playerEntity.RemoveTutorialPlayStep();
				}
			}
			else
			{
				_isTriggered = false;
			}
		}

		private bool ShouldShowButton()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity.hasView && !playerEntity.isDead && playerEntity.hasTutorialPlayStep && playerEntity.tutorialPlayStep.Step == TutorialStep.ShowJumpButton;
		}

		private bool ShouldShowTutorial()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			if (!playerEntity.hasView || playerEntity.isDead || !playerEntity.hasTutorialPlayStep || playerEntity.tutorialPlayStep.Step != TutorialStep.SingleJump)
			{
				return false;
			}
			GameEntity[] entities = _tutorialPerformedGroup.GetEntities();
			GameEntity[] array = entities;
			foreach (GameEntity gameEntity in array)
			{
				if (gameEntity.tutorialPerformed.Step == TutorialStep.SingleJump)
				{
					return false;
				}
			}
			return true;
		}

		private void UnpauseGame()
		{
			_isTriggered = false;
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
