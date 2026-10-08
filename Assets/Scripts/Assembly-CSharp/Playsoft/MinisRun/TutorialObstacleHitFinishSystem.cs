using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialObstacleHitFinishSystem : ReactiveSystem<GameEntity>
	{
		private GameContext gameContext;

		private UIContext uiContext;

		private TutorialSettingsSO settings;

		public TutorialObstacleHitFinishSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			uiContext = contexts.uI;
			settings = SettingsService.GetSetting<TutorialSettingsSO>();
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer && entity.hasTutorialPlayStep && entity.tutorialPlayStep.Step.Equals(TutorialStep.HitObstacle);
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.TutorialPlayStep.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			IngameHUD ingameHUD = uiContext.uIEntity.hUD.hud as IngameHUD;
			ingameHUD.StartCoroutine(MakeStaminaHitTutorial(ingameHUD));
		}

		private IEnumerator MakeStaminaHitTutorial(IngameHUD hud)
		{
			float prevTimeScale = gameContext.worldEntity.worldTimeScale.scale;
			yield return 0;
			gameContext.worldEntity.ReplaceWorldTimeScale(0f);
			float timer = settings.HitTutorialDuration * 0.5f;
			while (timer > 0f)
			{
				yield return 0;
				if (!gameContext.worldEntity.isPaused)
				{
					timer -= Time.unscaledDeltaTime;
				}
			}
			hud.UpdateStaminaUsingUnscaledTime();
			float timer2 = settings.HitTutorialDuration * 0.5f;
			while (timer2 > 0f)
			{
				yield return 0;
				if (!gameContext.worldEntity.isPaused)
				{
					timer2 -= Time.unscaledDeltaTime;
				}
			}
			Vector3 playerPos = gameContext.playerEntity.view.gameObject.transform.position;
			gameContext.playerEntity.ReplaceTutorialPlayStep(TutorialStep.HitObstacleFinished, false, playerPos.x, playerPos.x);
			hud.UpdateStaminaUsingScaledTime();
			gameContext.worldEntity.ReplaceWorldTimeScale(prevTimeScale);
		}
	}
}
