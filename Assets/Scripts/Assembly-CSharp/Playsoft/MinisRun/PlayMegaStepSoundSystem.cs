using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayMegaStepSoundSystem : IExecuteSystem, ISystem
	{
		private const string STEP_SOUND_ID = "character_mega_step";

		private const float TIME_BETWEEN = 0.2f;

		private readonly StateContext stateContext;

		private readonly GameContext gameContext;

		private float elapsedTime;

		public PlayMegaStepSoundSystem(Contexts contexts)
		{
			stateContext = contexts.state;
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (CanExecute())
			{
				elapsedTime += Time.deltaTime;
				if (elapsedTime > 0.2f)
				{
					elapsedTime = 0f;
					stateContext.CreateEntity().AddPlaySoundEffect("character_mega_step");
				}
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasSizeIncreased && !playerEntity.isDead && playerEntity.hasGrounded && (!playerEntity.hasJumping || (playerEntity.hasJumping && playerEntity.jumping.Quantity <= 0));
		}
	}
}
