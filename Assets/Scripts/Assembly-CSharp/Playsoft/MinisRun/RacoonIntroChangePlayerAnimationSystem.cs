using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class RacoonIntroChangePlayerAnimationSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		private readonly SettingsRacoonAnimSO settingsRaccoonAnim;

		private readonly SettingsVisualFeedbackSO visualSettings;

		private int collectedCandies;

		private bool wavingStarted;

		private bool sequenceDone;

		private float wavingStartTime;

		public RacoonIntroChangePlayerAnimationSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			settingsRaccoonAnim = SettingsService.GetSetting<SettingsRacoonAnimSO>();
			visualSettings = SettingsService.GetSetting<SettingsVisualFeedbackSO>();
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Collected.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasCollectibleScore && gameContext.playerEntity.hasView;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			if (!sequenceDone)
			{
				collectedCandies++;
				if (gameContext.playerEntity.isAngryRun)
				{
					gameContext.playerEntity.isAngryRun = false;
				}
				if (!wavingStarted && settingsRaccoonAnim.RaccoonFirstIntroPlayerWaveAnimCandiesToCollect == collectedCandies)
				{
					wavingStartTime = Time.realtimeSinceStartup;
					wavingStarted = true;
					Animator component = gameContext.playerEntity.view.gameObject.GetComponent<Animator>();
					component.SetFloat("RunAnim", 1f);
				}
				if (wavingStarted && Time.realtimeSinceStartup - wavingStartTime >= visualSettings.RunWithWaveDuration)
				{
					sequenceDone = true;
					Animator component2 = gameContext.playerEntity.view.gameObject.GetComponent<Animator>();
					component2.SetFloat("RunAnim", 0f);
				}
			}
		}
	}
}
