using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public sealed class TutorialInformUiSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		private readonly IGroup<GameEntity> listeners;

		public TutorialInformUiSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			listeners = contexts.game.GetGroup(GameMatcher.TutorialsListener);
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.TutorialPlayStep);
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
				InformUi(entity.tutorialPlayStep.Step);
			}
		}

		private void InformUi(TutorialStep tutorialStepAction)
		{
			GameEntity[] entities = listeners.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				gameEntity.tutorialsListener.listener.OnTutorialAction(tutorialStepAction);
			}
		}
	}
}
