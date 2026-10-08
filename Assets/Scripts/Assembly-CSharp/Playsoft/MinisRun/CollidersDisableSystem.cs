using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CollidersDisableSystem : ReactiveSystem<GameEntity>
	{
		public CollidersDisableSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isDisableColliders;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.DisableColliders.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				DisableColliders(entity);
				entity.isDisableColliders = false;
			}
		}

		private void DisableColliders(GameEntity entity)
		{
			Collider2D[] componentsInChildren = entity.view.gameObject.GetComponentsInChildren<Collider2D>();
			if (componentsInChildren != null)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = false;
				}
			}
		}
	}
}
