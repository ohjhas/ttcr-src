using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayerAbilityStateSystem : ReactiveSystem<GameEntity>
	{
		private readonly IGroup<GameEntity> abilityStateChangedListenersGroup;

		public PlayerAbilityStateSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			abilityStateChangedListenersGroup = contexts.game.GetGroup(GameMatcher.AbilityStateChangedListener);
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.AbilityChargingProgress);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				GameEntity[] entities2 = abilityStateChangedListenersGroup.GetEntities();
				foreach (GameEntity gameEntity in entities2)
				{
					gameEntity.abilityStateChangedListener.listener.OnAbilityStateChanged(entity.abilityChargingProgress.PercentCharged);
				}
			}
		}
	}
}
