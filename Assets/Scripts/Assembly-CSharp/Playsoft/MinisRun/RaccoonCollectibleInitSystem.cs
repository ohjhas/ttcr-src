using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class RaccoonCollectibleInitSystem : ReactiveSystem<GameEntity>
	{
		private static IGroup<GameEntity> _raccooonGroup;

		private static Color _transparent;

		public RaccoonCollectibleInitSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			_transparent = new Color(1f, 1f, 1f, 0f);
			_raccooonGroup = contexts.game.GetGroup(GameMatcher.Raccoon);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasView && entity.isCollectible;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.View.Added());
		}

		protected override void Execute(List<GameEntity> justSpawnedCollectibles)
		{
			GameEntity singleEntity = _raccooonGroup.GetSingleEntity();
			Color color = ((singleEntity == null) ? Color.white : _transparent);
			SetCollectiblesColor(justSpawnedCollectibles, color);
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
