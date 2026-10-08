using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ScoreBlinkSystem : ReactiveSystem<GameEntity>
	{
		private GameContext gameContext;

		private IGroup<GameEntity> scoreTokens;

		public ScoreBlinkSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			scoreTokens = gameContext.GetGroup(GameMatcher.CollectibleScore);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.ScoreMultiplier.AddedOrRemoved());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			SettingsVisualFeedbackSO setting = SettingsService.GetSetting<SettingsVisualFeedbackSO>();
			foreach (GameEntity entity in entities)
			{
				GameEntity[] entities2 = scoreTokens.GetEntities();
				foreach (GameEntity gameEntity in entities2)
				{
					if (gameEntity.hasView && gameEntity.isScoreToken)
					{
						SpriteRenderer component = gameEntity.view.gameObject.GetComponent<SpriteRenderer>();
						component.material = ((!entity.hasScoreMultiplier) ? setting.ScoreDefaultMat : setting.ScoreBlinkMat);
					}
				}
			}
		}
	}
}
