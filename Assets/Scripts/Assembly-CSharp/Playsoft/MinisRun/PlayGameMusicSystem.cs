using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public sealed class PlayGameMusicSystem : ReactiveSystem<GameEntity>
	{
		private readonly StateContext stateContext;

		public PlayGameMusicSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.PlayerRelay.Added(), GameMatcher.PlayerResurrect.Added(), GameMatcher.PlayerRevive.Added(), GameMatcher.PlayGameMusic.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return true;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			stateContext.CreateEntity().AddPlayMusic("chapter1");
			stateContext.CreateEntity().AddMuteSoundSource(false, "ingame");
		}
	}
}
