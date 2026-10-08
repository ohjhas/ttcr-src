using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerSlideSafetyCheckSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly InputContext inputContext;

		private readonly IGroup<InputEntity> inputSlideGroup;

		public PlayerSlideSafetyCheckSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			inputContext = contexts.input;
			inputSlideGroup = contexts.input.GetGroup(InputMatcher.InputSlide);
		}

		public void Execute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			GameEntity worldEntity = gameContext.worldEntity;
			if (playerEntity != null && !playerEntity.isDead && playerEntity.hasView && worldEntity != null && !worldEntity.isPaused && !IsPlayingSlideTutorial())
			{
				GameObject gameObject = playerEntity.view.gameObject;
				Animator component = gameObject.GetComponent<Animator>();
				bool flag = component.GetBool("Slide");
				bool flag2 = inputSlideGroup.count > 0;
				if (flag && !flag2)
				{
					StopSlidingIfNeeded();
				}
			}
		}

		private void StopSlidingIfNeeded()
		{
			Debug.LogWarning("Slide safety check stoped sliding");
			InputEntity inputEntity = inputContext.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isInputSlideStop = true;
			inputEntity.AddBuffer(0f);
		}

		private bool IsPlayingSlideTutorial()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity.hasTutorialPlayStep && playerEntity.tutorialPlayStep.Step == TutorialStep.Slide;
		}
	}
}
