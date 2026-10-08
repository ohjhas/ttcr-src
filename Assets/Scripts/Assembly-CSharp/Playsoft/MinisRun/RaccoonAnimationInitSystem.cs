using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RaccoonAnimationInitSystem : ReactiveSystem<GameEntity>
	{
		public RaccoonAnimationInitSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isRaccoon && entity.hasView;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.View.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
			}
		}
	}
}
