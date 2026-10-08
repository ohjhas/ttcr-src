using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogMissionEndSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		public LogMissionEndSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Dead);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasWorld;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			StateEntity stateEntity2 = stateContext.GetEntitiesWithName("Coins").SingleEntity();
			int amount = stateEntity2.integer.Amount;
			int value = gameContext.worldEntity.coins.Value;
			StateEntity stateEntity3 = stateContext.GetEntitiesWithName("LastScene").SingleEntity();
			string newP = ((!(stateEntity3.stringData.Value == ProjectConstants.INGAME_SCENE_NAME)) ? "new" : "replay");
			stateEntity.AddEventCustom("run", "gc", "coins", null, value, amount, newP, null, null, false);
		}
	}
}
