using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class PlayerSlideStopSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext _gameContext;

		private readonly IGroup<GameEntity> _tutorialPerformedGroup;

		private readonly IGroup<InputEntity> _inputSlideGroup;

		public PlayerSlideStopSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			_gameContext = contexts.game;
			_tutorialPerformedGroup = contexts.game.GetGroup(GameMatcher.TutorialPerformed);
			_inputSlideGroup = contexts.input.GetGroup(InputMatcher.InputSlide);
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Input);
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isInputSlideStop || entity.isInputJump;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			if (!IsPlayingSlideTutorial() && !IsUserTryingToJumpDuringSlide(entities) && playerEntity.hasView)
			{
				GameObject gameObject = playerEntity.view.gameObject;
				playerEntity.isSliding = false;
				Animator component = gameObject.GetComponent<Animator>();
				BoxCollider2D component2 = gameObject.GetComponent<BoxCollider2D>();
				component.SetBool("Slide", false);
				Vector2 colliderNormal = SettingsService.SettingsSO.ColliderNormal;
				component2.offset = new Vector2(component2.offset.x, colliderNormal.x);
				component2.size = new Vector2(component2.size.x, colliderNormal.y);
			}
		}

		private bool IsPlayingSlideTutorial()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity.hasTutorialPlayStep && playerEntity.tutorialPlayStep.Step == TutorialStep.Slide && !HasSlideTutorialBeenPerformed();
		}

		private bool HasSlideTutorialBeenPerformed()
		{
			GameEntity[] entities = _tutorialPerformedGroup.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.tutorialPerformed.Step == TutorialStep.Slide)
				{
					return true;
				}
			}
			return false;
		}

		private bool IsUserTryingToJumpDuringSlide(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				if (entity.isInputJump)
				{
					return _inputSlideGroup.count > 0;
				}
			}
			return false;
		}
	}
}
