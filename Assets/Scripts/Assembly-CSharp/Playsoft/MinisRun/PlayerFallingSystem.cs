using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerFallingSystem : ReactiveSystem<GameEntity>
	{
		public PlayerFallingSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Grounded.AddedOrRemoved());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer && !entity.hasJumping;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				Animator component = entity.view.gameObject.GetComponent<Animator>();
				if (!entity.hasGrounded)
				{
					component.SetBool("Jump", true);
					component.SetFloat("JumpAnim", Mathf.Round(Random.Range(0f, 1f)));
				}
				else if (component.GetBool("Jump"))
				{
					component.SetBool("Jump", false);
				}
			}
		}
	}
}
