using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialTriggerSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext gameContext;

		public TutorialTriggerSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasCollision && entity.collision.to != null && entity.collision.from != null;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Collision);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			TutorialSettingsSO setting = SettingsService.GetSetting<TutorialSettingsSO>();
			foreach (InputEntity entity in entities)
			{
				GameEntity gameEntity = entity.collision.from as GameEntity;
				GameEntity gameEntity2 = entity.collision.to as GameEntity;
				if (gameEntity.isPlayer && gameEntity2.hasTutorialTrigger && (!gameEntity.hasTutorialPlayStep || gameEntity.tutorialPlayStep.Step != gameEntity2.tutorialTrigger.Step))
				{
					float num = gameContext.playerEntity.view.gameObject.transform.position.x + setting.TriggerOffset;
					float newEndPosX = num + setting.TriggerLength;
					gameContext.playerEntity.ReplaceTutorialPlayStep(gameEntity2.tutorialTrigger.Step, gameEntity2.tutorialTrigger.StopTime, num, newEndPosX);
				}
			}
		}
	}
}
