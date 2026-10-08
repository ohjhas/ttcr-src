using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public sealed class TutorialEndSlideSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		private readonly IGroup<InputEntity> inputSlideGroup;

		private readonly IGroup<InputEntity> inputSlideEndGroup;

		public TutorialEndSlideSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			inputSlideGroup = contexts.input.GetGroup(InputMatcher.InputSlide);
			inputSlideEndGroup = contexts.input.GetGroup(InputMatcher.InputSlideStop);
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.TutorialPlayStep.Added());
		}

		protected override bool Filter(GameEntity e)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			return e.hasTutorialPlayStep && playerEntity.hasView && !playerEntity.isDead;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				TutorialStep step = entity.tutorialPlayStep.Step;
				if (step == TutorialStep.SlideEnd)
				{
					CheckIfSlideShouldEnd();
				}
			}
		}

		private void CheckIfSlideShouldEnd()
		{
			if ((inputSlideGroup.GetEntities().Length == 0 || inputSlideEndGroup.GetEntities().Length > 0) && gameContext.playerEntity.isSliding)
			{
				InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.isInputSlideStop = true;
				inputEntity.AddBuffer(0f);
			}
		}
	}
}
