using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerJumpStopSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext gameContext;

		public PlayerJumpStopSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Input);
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isLanding;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (playerEntity.hasJumping)
			{
				playerEntity.RemoveJumping();
				playerEntity.view.gameObject.GetComponent<Animator>().SetBool("Jump", false);
			}
		}
	}
}
