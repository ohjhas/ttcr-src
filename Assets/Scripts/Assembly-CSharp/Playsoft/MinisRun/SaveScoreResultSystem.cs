using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class SaveScoreResultSystem : ReactiveSystem<GameEntity>
	{
		public SaveScoreResultSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
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
			SaveHighScore();
		}

		private void SaveHighScore()
		{
			GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
			int newAmount = (worldEntity.hasScore ? worldEntity.score.Value : 0);
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("HighScoreSend");
			stateEntity.AddInteger(newAmount);
		}
	}
}
