using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayGrowUpSoundSystem : ReactiveSystem<GameEntity>
	{
		private const string GROW_SOUND_ID = "power_up_grow";

		private readonly StateContext stateContext;

		public PlayGrowUpSoundSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.SizeIncreased.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return true;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				stateContext.CreateEntity().AddPlaySoundEffect("power_up_grow");
			}
		}
	}
}
