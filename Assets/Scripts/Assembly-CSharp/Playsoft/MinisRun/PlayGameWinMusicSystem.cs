using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public sealed class PlayGameWinMusicSystem : ReactiveSystem<GameEntity>
	{
		private readonly StateContext stateContext;

		public PlayGameWinMusicSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.LevelFinished.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return true;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			stateContext.CreateEntity().AddPlayMusic("chapter1_win");
			stateContext.CreateEntity().AddMuteSoundSource(true, "ingame");
		}
	}
}
