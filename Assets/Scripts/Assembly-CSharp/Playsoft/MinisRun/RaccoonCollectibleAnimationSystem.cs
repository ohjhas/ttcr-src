using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class RaccoonCollectibleAnimationSystem : ReactiveSystem<GameEntity>
	{
		private const string DROP_SOUND_ID = "new_item_appear";

		private readonly SettingsRacoonAnimSO _settingsSO;

		public RaccoonCollectibleAnimationSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			_settingsSO = SettingsService.GetSetting<SettingsRacoonAnimSO>();
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.RaccoonDropAnimation.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasView && entity.isRaccoonDropAnimation;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				Transform transform = entity.view.gameObject.transform;
				transform.GetComponent<SpriteRenderer>().color = Color.white;
				DOTween.Sequence().Append(transform.DOMoveX(transform.position.x + _settingsSO.RaccoonIntroRaccoonDropDistanceX, _settingsSO.RaccoonIntroRaccoonDropDuration).From()).Join(transform.DOMoveY(transform.position.y + _settingsSO.RaccoonIntroRaccoonDropDistanceY, _settingsSO.RaccoonIntroRaccoonDropDuration).SetEase(_settingsSO.RaccoonIntroRaccoonDropCurve));
				Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("new_item_appear");
			}
		}
	}
}
