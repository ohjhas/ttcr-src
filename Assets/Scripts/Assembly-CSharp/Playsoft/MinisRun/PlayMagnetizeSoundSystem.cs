using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayMagnetizeSoundSystem : ReactiveSystem<GameEntity>
	{
		private const string MAGNETIZE_SOUND_ID = "power_up_magnet_loop";

		private readonly StateContext stateContext;

		public PlayMagnetizeSoundSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Magnetized.Added(), GameMatcher.Magnetized.Removed());
		}

		protected override bool Filter(GameEntity entity)
		{
			return true;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				if (entity.hasMagnetized)
				{
					stateContext.CreateEntity().AddPlayLoopSoundEffect("power_up_magnet_loop");
				}
				else
				{
					stateContext.CreateEntity().AddStopLoopSoundEffect("power_up_magnet_loop");
				}
			}
		}
	}
}
