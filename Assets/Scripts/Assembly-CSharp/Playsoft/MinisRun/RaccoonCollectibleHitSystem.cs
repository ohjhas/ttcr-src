using System.Collections.Generic;
using DG.Tweening;
using Entitas;

namespace Playsoft.MinisRun
{
	public sealed class RaccoonCollectibleHitSystem : ReactiveSystem<InputEntity>, IInitializeSystem, ISystem
	{
		private readonly SettingsRacoonAnimSO settingsRaccoonAnimSO;

		private bool canDropTokens;

		public RaccoonCollectibleHitSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			settingsRaccoonAnimSO = SettingsService.GetSetting<SettingsRacoonAnimSO>();
		}

		public void Initialize()
		{
			canDropTokens = false;
			if (GameIntroHelper.IsIntroTime())
			{
				float raccoonFirstIntroRaccoonDropDelay = settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonDropDelay;
				DOVirtual.DelayedCall(raccoonFirstIntroRaccoonDropDelay, delegate
				{
					canDropTokens = true;
				});
			}
			else
			{
				canDropTokens = true;
			}
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasCollision && entity.collision.to != null && ((GameEntity)entity.collision.to).isCollectible && entity.collision.from != null && ((GameEntity)entity.collision.from).isRaccoon;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Collision);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				GameEntity gameEntity = (GameEntity)entity.collision.to;
				if (canDropTokens)
				{
					if (!gameEntity.isRaccoonDropAnimation)
					{
						gameEntity.isRaccoonDropAnimation = true;
					}
				}
				else
				{
					gameEntity.isDestroyed = true;
				}
			}
		}
	}
}
