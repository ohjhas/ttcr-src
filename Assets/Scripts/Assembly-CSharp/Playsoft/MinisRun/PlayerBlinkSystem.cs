using System;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerBlinkSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		public PlayerBlinkSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (!playerEntity.isDead && playerEntity.hasPlayerBlinking && playerEntity.hasView)
			{
				SpriteRenderer componentInChildren = playerEntity.view.gameObject.GetComponentInChildren<SpriteRenderer>();
				float colorFactor = CalculateColorFactor(playerEntity);
				UpdateColor(componentInChildren, colorFactor);
				UpdateTimer(playerEntity);
			}
		}

		private void UpdateColor(SpriteRenderer playerSprite, float colorFactor)
		{
			Color white = Color.white;
			Color b = new Color(0.75f, 0.75f, 0.75f);
			playerSprite.color = Color.Lerp(white, b, colorFactor);
		}

		private float CalculateColorFactor(GameEntity playerEntity)
		{
			float num = playerEntity.playerBlinking.Duration - playerEntity.playerBlinking.TimeLeft;
			num /= playerEntity.playerBlinking.Duration;
			num *= (float)Math.PI * 2f;
			float f = -(float)Math.PI / 2f + num * (float)playerEntity.playerBlinking.NumberOfCycles;
			float num2 = 0.5f * Mathf.Sin(f) + 0.5f;
			return (!(playerEntity.playerBlinking.TimeLeft < 0f)) ? num2 : 0f;
		}

		private void UpdateTimer(GameEntity playerEntity)
		{
			if (playerEntity.playerBlinking.TimeLeft < 0f)
			{
				playerEntity.RemovePlayerBlinking();
				return;
			}
			float newTimeLeft = playerEntity.playerBlinking.TimeLeft - Time.smoothDeltaTime;
			playerEntity.ReplacePlayerBlinking(playerEntity.playerBlinking.Duration, newTimeLeft, playerEntity.playerBlinking.NumberOfCycles);
		}
	}
}
