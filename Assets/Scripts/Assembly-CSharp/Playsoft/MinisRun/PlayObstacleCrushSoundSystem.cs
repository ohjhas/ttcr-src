using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayObstacleCrushSoundSystem : ReactiveSystem<GameEntity>
	{
		private const string CRUSH_SOUND_ID = "obstacle_crush";

		private readonly StateContext stateContext;

		public PlayObstacleCrushSoundSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.FlyAway.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return true;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				stateContext.CreateEntity().AddPlaySoundEffect("obstacle_crush");
			}
		}
	}
}
