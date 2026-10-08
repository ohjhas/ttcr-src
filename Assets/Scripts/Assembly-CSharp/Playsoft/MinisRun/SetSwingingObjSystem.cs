using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class SetSwingingObjSystem : ReactiveSystem<GameEntity>
	{
		public SetSwingingObjSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.View.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasSwingingData;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				GameObject gameObject = entity.view.gameObject;
				BoxCollider2D componentInChildren = gameObject.GetComponentInChildren<BoxCollider2D>();
				Vector3 vector = new Vector3(componentInChildren.size.x * (entity.swingingData.Anchor.x - 0.5f), componentInChildren.size.y * (entity.swingingData.Anchor.y - 0.5f));
				gameObject.transform.position = entity.position.Value + vector;
				entity.ReplacePosition(gameObject.transform.position);
				componentInChildren.transform.localPosition = -vector;
			}
		}
	}
}
