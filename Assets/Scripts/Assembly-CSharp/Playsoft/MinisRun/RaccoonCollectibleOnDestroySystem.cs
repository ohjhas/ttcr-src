using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class RaccoonCollectibleOnDestroySystem : ReactiveSystem<GameEntity>
	{
		private static IGroup<GameEntity> _allCollectiblesOnScene;

		public RaccoonCollectibleOnDestroySystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			_allCollectiblesOnScene = contexts.game.GetGroup(GameMatcher.Collectible);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isRaccoon;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Destroyed);
		}

		protected override void Execute(List<GameEntity> entities)
		{
			SetCollectiblesColor(_allCollectiblesOnScene.GetEntities().ToList(), Color.white);
		}

		private void SetCollectiblesColor(List<GameEntity> entities, Color color)
		{
			foreach (GameEntity entity in entities)
			{
				entity.view.gameObject.GetComponent<SpriteRenderer>().color = color;
			}
		}
	}
}
