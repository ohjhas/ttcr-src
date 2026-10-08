using System.Collections.Generic;
using DG.Tweening;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ShakingObjSystem : ReactiveSystem<GameEntity>
	{
		public ShakingObjSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.View.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasShaking;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				entity.view.gameObject.transform.DOShakeRotation(entity.shaking.Interval, entity.shaking.Strength).SetLoops(-1);
			}
		}
	}
}
