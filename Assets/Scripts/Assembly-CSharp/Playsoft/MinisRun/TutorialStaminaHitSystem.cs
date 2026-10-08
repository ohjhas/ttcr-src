using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialStaminaHitSystem : ReactiveSystem<InputEntity>
	{
		private GameContext gameContext;

		private StateContext stateContext;

		private TutorialSettingsSO settings;

		public TutorialStaminaHitSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
			settings = SettingsService.GetSetting<TutorialSettingsSO>();
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
			if (!ShouldShow())
			{
				return;
			}
			foreach (InputEntity entity in entities)
			{
				GameEntity gameEntity = entity.collision.from as GameEntity;
				GameEntity gameEntity2 = entity.collision.to as GameEntity;
				if (gameEntity.isPlayer && gameEntity2.isObstacle && !gameEntity.hasInvulnerability && (!gameEntity.hasTutorialPlayStep || gameEntity.tutorialPlayStep.Step != TutorialStep.HitObstacle))
				{
					Vector3 position = gameContext.playerEntity.view.gameObject.transform.position;
					gameContext.playerEntity.ReplaceTutorialPlayStep(TutorialStep.HitObstacle, false, position.x, position.x);
					MarkTutorialAsPlayed();
				}
			}
		}

		private bool ShouldShow()
		{
			return gameContext.worldEntity.levelSettings.levelTutorialSettings.ShowStaminaHitTutorialIfHappens && !TutorialWasPlayed();
		}

		private bool TutorialWasPlayed()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("TutorialStaminaHitData").SingleEntity();
			return stateEntity != null && stateEntity.integer.Amount >= settings.HitTutorialPossibleDisplayesCount;
		}

		private void MarkTutorialAsPlayed()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("TutorialStaminaHitData").SingleEntity();
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddName("TutorialStaminaHitDataSend");
			stateEntity2.AddInteger(stateEntity.integer.Amount + 1);
		}
	}
}
