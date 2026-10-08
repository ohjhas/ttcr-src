using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class WorldPauseSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ISystem
	{
		private readonly UIContext uiContext;

		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		private float currentTimeScale;

		private readonly IGroup<GameEntity> pauseListeners;

		public WorldPauseSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
			gameContext = contexts.game;
			pauseListeners = contexts.game.GetGroup(GameMatcher.PauseListener);
		}

		public void Initialize()
		{
			stateContext.CreateEntity().AddMuteSoundSource(false, "ingame");
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasWorld;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Paused.AddedOrRemoved());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				if (entity.isPaused)
				{
					uiContext.OpenDialog<PauseScreen>();
					stateContext.CreateEntity().AddMuteSoundSource(true, "ingame");
					stateContext.CreateEntity().AddChangeTimeScale(0f);
				}
				else
				{
					stateContext.CreateEntity().AddChangeTimeScale(gameContext.worldEntity.worldTimeScale.scale);
					stateContext.CreateEntity().AddMuteSoundSource(false, "ingame");
				}
				GameEntity[] entities2 = pauseListeners.GetEntities();
				foreach (GameEntity gameEntity in entities2)
				{
					gameEntity.pauseListener.listener.OnPause(entity.isPaused, currentTimeScale);
				}
			}
		}
	}
}
