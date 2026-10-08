using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialSkipDoubleJumpSystem : ReactiveSystem<InputEntity>
	{
		private GameContext _gameContext;

		public TutorialSkipDoubleJumpSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			_gameContext = contexts.game;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.InputJumpPerformed);
		}

		protected override bool Filter(InputEntity entity)
		{
			return _gameContext.playerEntity.hasTutorialPlayStep && _gameContext.playerEntity.tutorialPlayStep.Step == TutorialStep.DoubleJump;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			Vector3 position = playerEntity.view.gameObject.transform.position;
			if (position.x >= playerEntity.tutorialPlayStep.StartPosX && position.x <= playerEntity.tutorialPlayStep.EndPosX && playerEntity.hasJumping && playerEntity.jumping.Quantity > 1)
			{
				playerEntity.RemoveTutorialPlayStep();
			}
		}
	}
}
