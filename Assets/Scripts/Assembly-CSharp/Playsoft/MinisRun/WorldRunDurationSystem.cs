using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WorldRunDurationSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		public WorldRunDurationSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (CanExecute())
			{
				gameContext.worldEntity.runDuration.Duration_MS += Time.smoothDeltaTime * 1000f;
			}
		}

		private bool CanExecute()
		{
			return gameContext.worldEntity != null && gameContext.worldEntity.hasRunDuration && !gameContext.worldEntity.isPaused && gameContext.playerEntity != null && !gameContext.playerEntity.isDead;
		}
	}
}
