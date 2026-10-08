using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ResetPoolableViewSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public ResetPoolableViewSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isResetPoolable && entity.hasView;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.ResetPoolable.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				EnableColliders(entity);
				ResetRigidbody(entity);
				ResetSpriteRenderer(entity);
				entity.isResetPoolable = false;
			}
		}

		private void EnableColliders(GameEntity entity)
		{
			Collider2D[] componentsInChildren = entity.view.gameObject.GetComponentsInChildren<Collider2D>();
			if (componentsInChildren != null)
			{
				for (int i = 0; i < componentsInChildren.Length; i++)
				{
					componentsInChildren[i].enabled = true;
				}
			}
		}

		private void ResetRigidbody(GameEntity entity)
		{
			Rigidbody2D componentInChildren = entity.view.gameObject.GetComponentInChildren<Rigidbody2D>();
			if (componentInChildren != null)
			{
				componentInChildren.transform.localRotation = Quaternion.Euler(Vector3.zero);
				componentInChildren.isKinematic = true;
			}
		}

		private void ResetSpriteRenderer(GameEntity entity)
		{
			SettingsVisualFeedbackSO setting = SettingsService.GetSetting<SettingsVisualFeedbackSO>();
			SpriteRenderer componentInChildren = entity.view.gameObject.GetComponentInChildren<SpriteRenderer>();
			if (componentInChildren != null)
			{
				DOTween.Kill(componentInChildren);
				Color color = new Color(componentInChildren.color.r, componentInChildren.color.g, componentInChildren.color.b, 1f);
				componentInChildren.color = color;
				if (entity.isScoreToken)
				{
					componentInChildren.material = ((!gameContext.playerEntity.hasScoreMultiplier) ? setting.ScoreDefaultMat : setting.ScoreBlinkMat);
				}
			}
		}
	}
}
