using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class FadeOutObjSystem : ReactiveSystem<GameEntity>
	{
		public FadeOutObjSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isFadeOut;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.FadeOut.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				SpriteRenderer componentInChildren = entity.view.gameObject.GetComponentInChildren<SpriteRenderer>();
				Color endValue = new Color(componentInChildren.color.r, componentInChildren.color.g, componentInChildren.color.b, 0f);
				componentInChildren.DOColor(endValue, SettingsService.SettingsSO.FadeOutDuration);
			}
		}
	}
}
