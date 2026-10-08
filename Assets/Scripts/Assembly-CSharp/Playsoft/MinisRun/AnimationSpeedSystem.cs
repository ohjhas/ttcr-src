using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class AnimationSpeedSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		public AnimationSpeedSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (gameContext.playerEntity.hasView)
			{
				Animator component = gameContext.playerEntity.view.gameObject.GetComponent<Animator>();
				if (!gameContext.playerEntity.isDead)
				{
					float num = ((!gameContext.playerEntity.hasAbilityFasterMovement) ? 1f : gameContext.playerEntity.abilityFasterMovement.Bonus);
					float num2 = gameContext.worldEntity.world.Speed * num;
					component.speed = num2 / SettingsService.SettingsSO.BaseSpeed;
				}
				else if (component != null)
				{
					component.speed = 1f;
				}
			}
		}
	}
}
