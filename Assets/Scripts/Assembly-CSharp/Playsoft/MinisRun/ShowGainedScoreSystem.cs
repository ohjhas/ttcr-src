using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ShowGainedScoreSystem : ReactiveSystem<GameEntity>
	{
		public ShowGainedScoreSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasGainedScore;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.GainedScore.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				TextMeshProUGUI componentInChildren = entity.view.gameObject.GetComponentInChildren<TextMeshProUGUI>();
				InitTextLabel(componentInChildren, entity.gainedScore.Score);
				float animTime = SettingsService.SettingsSO.FadeOutDuration * 0.5f;
				AnimatePath(entity.view.gameObject.transform, animTime);
				AnimateColor(componentInChildren, entity, animTime);
			}
		}

		private void InitTextLabel(TextMeshProUGUI textMesh, int score)
		{
			textMesh.text = "+" + score;
			textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
		}

		private void AnimatePath(Transform trans, float animTime)
		{
			Vector3[] array = new Vector3[2];
			array[0] = trans.position;
			array[1] = array[0] + new Vector3(0f, 5f);
			trans.DOPath(array, animTime, PathType.Linear, PathMode.Sidescroller2D);
		}

		private void AnimateColor(TextMeshProUGUI textMesh, GameEntity entity, float animTime)
		{
			Color endValue = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 0f);
			textMesh.DOColor(endValue, animTime).OnComplete(delegate
			{
				entity.isDestroyed = true;
			});
		}
	}
}
