using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayMegaLandSoundSystem : ReactiveSystem<InputEntity>
	{
		private const string LAND_SOUND_ID = "character_mega_land";

		private readonly StateContext stateContext;

		private readonly GameContext gameContext;

		public PlayMegaLandSoundSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			stateContext = contexts.state;
			gameContext = contexts.game;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Landing);
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isLanding && gameContext.playerEntity.hasSizeIncreased;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				stateContext.CreateEntity().AddPlaySoundEffect("character_mega_land");
			}
		}
	}
}
