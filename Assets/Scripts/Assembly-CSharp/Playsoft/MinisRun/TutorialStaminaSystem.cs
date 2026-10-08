using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class TutorialStaminaSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		private readonly UIContext uiContext;

		public TutorialStaminaSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			uiContext = contexts.uI;
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
			TutorialSettingsSO setting = SettingsService.GetSetting<TutorialSettingsSO>();
			foreach (GameEntity entity in entities)
			{
				switch (entity.tutorialPlayStep.Step)
				{
				case TutorialStep.ShowStaminaDrainText:
					OpenTutorialScreen(setting, setting.staminaPotionTutorialDuration);
					break;
				case TutorialStep.ShowStaminaPotionText:
					OpenTutorialScreen(setting, setting.staminaDrainTutorialDuration);
					break;
				}
			}
		}

		private void OpenTutorialScreen(TutorialSettingsSO settings, float tutorialDuration)
		{
			if (!uiContext.GetDialog<TutorialScreen>().gameObject.activeInHierarchy)
			{
				uiContext.OpenDialog<TutorialScreen>(false);
				gameContext.worldEntity.ReplaceChangeScaleTime(settings.staminaSlowDownFactor, settings.staminaSlowDownDuration, tutorialDuration - settings.staminaSlowDownDuration, 0f);
				Tween waitTween = DOVirtual.DelayedCall(tutorialDuration, delegate
				{
					uiContext.CloseDialog(uiContext.GetDialog<TutorialScreen>());
				});
				waitTween.OnUpdate(delegate
				{
					KillTweenIfNeeded(waitTween);
				});
			}
		}

		private void KillTweenIfNeeded(Tween tween)
		{
			if (gameContext.playerEntity.isDead || gameContext.playerEntity.hasLevelFinished || gameContext.worldEntity.isDestroyed || uiContext.GetDialog<FailScreen>().gameObject.activeInHierarchy)
			{
				Debug.LogError("[TutorialStaminaSystem] Tween updating time scale killed");
				tween.Kill();
			}
		}
	}
}
