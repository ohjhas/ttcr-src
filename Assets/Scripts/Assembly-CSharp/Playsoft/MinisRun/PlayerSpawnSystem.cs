using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerSpawnSystem : ReactiveSystem<GameEntity>
	{
		public PlayerSpawnSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.View.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return !entity.isPoolable && entity.isPlayer;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				if (entity.hasInitialTrigger)
				{
					Animator component = entity.view.gameObject.GetComponent<Animator>();
					component.SetTrigger(entity.initialTrigger.TriggerName);
				}
			}
		}
	}
}
