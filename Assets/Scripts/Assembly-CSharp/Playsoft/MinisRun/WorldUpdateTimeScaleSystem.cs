using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class WorldUpdateTimeScaleSystem : ReactiveSystem<GameEntity>
	{
		private readonly StateContext stateContext;

		public WorldUpdateTimeScaleSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.WorldTimeScale);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasWorldTimeScale && !entity.isPaused;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				stateContext.CreateEntity().AddChangeTimeScale(entity.worldTimeScale.scale);
			}
		}
	}
}
