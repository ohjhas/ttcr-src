using System;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerRunAnimSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private float timer;

		private SettingsVisualFeedbackSO visualSettings;

		public PlayerRunAnimSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			visualSettings = SettingsService.GetSetting<SettingsVisualFeedbackSO>();
			System.Random random = new System.Random(Mathf.RoundToInt(Time.realtimeSinceStartup));
			timer = (float)random.NextDouble() * visualSettings.RunWithWaveAnimInterval;
		}

		public void Execute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (playerEntity.isDead || !playerEntity.hasView || playerEntity.isSpecialRun)
			{
				return;
			}
			Animator component = playerEntity.view.gameObject.GetComponent<Animator>();
			if (!component.GetBool("Jump") && !component.GetBool("Slide"))
			{
				timer += Time.smoothDeltaTime;
				if (timer >= visualSettings.RunWithWaveAnimInterval + visualSettings.RunWithWaveDuration)
				{
					component.SetFloat("RunAnim", 0f);
					timer = 0f;
				}
				else if (timer >= visualSettings.RunWithWaveAnimInterval && !Mathf.Approximately(component.GetFloat("RunAnim"), 1f))
				{
					component.SetFloat("RunAnim", 1f);
				}
			}
			else
			{
				component.SetFloat("RunAnim", 0f);
			}
		}
	}
}
