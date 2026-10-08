using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ScoreMultiplierPowerupSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		public ScoreMultiplierPowerupSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (CanExecute())
			{
				CalculateRemainingTime();
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasScoreMultiplier && !playerEntity.isDead;
		}

		private void CalculateRemainingTime()
		{
			gameContext.playerEntity.scoreMultiplier.Duration -= Time.deltaTime;
			if (gameContext.playerEntity.scoreMultiplier.Duration <= 0f)
			{
				gameContext.playerEntity.RemoveScoreMultiplier();
			}
		}
	}
}
