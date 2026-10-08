using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class GiantPowerupSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		public GiantPowerupSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (CanExecute())
			{
				float num = CalculateCurrentSize();
				SetPlayerSize(num);
				float collidersFactor = CalculateCollidersFactor(num);
				AdjustPlayerColliders(collidersFactor);
				CalculateRemainingTime();
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasSizeIncreased && !playerEntity.isDead;
		}

		private float CalculateCurrentSize()
		{
			float sizeIncreaseDuration = SettingsService.GetSetting<SettingsPowerupsSO>().SizeIncreaseDuration;
			float duration = gameContext.playerEntity.sizeIncreased.Duration;
			float num = sizeIncreaseDuration - duration;
			float num2 = num / sizeIncreaseDuration;
			float num3 = SettingsService.GetSetting<SettingsPowerupsSO>().SizeIncreaseCurve.Evaluate(num2);
			Transform transform = gameContext.playerEntity.view.gameObject.transform;
			return (!(num2 > 0.5f)) ? Mathf.Max(num3, transform.localScale.x) : num3;
		}

		private void SetPlayerSize(float currentSize)
		{
			Transform transform = gameContext.playerEntity.view.gameObject.transform;
			transform.localScale = Vector3.one * currentSize;
		}

		private float CalculateCollidersFactor(float currentSize)
		{
			float num = SettingsService.GetSetting<SettingsPowerupsSO>().SizeIncreaseCurve.Evaluate(0.5f);
			return (currentSize - 1f) / (num - 1f);
		}

		private void AdjustPlayerColliders(float collidersFactor)
		{
			BoxCollider2D[] components = gameContext.playerEntity.view.gameObject.GetComponents<BoxCollider2D>();
			BoxCollider2D[] array = components;
			foreach (BoxCollider2D boxCollider2D in array)
			{
				if (boxCollider2D.isTrigger)
				{
					boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, Mathf.Lerp(1.7f, 1.6f, collidersFactor));
					boxCollider2D.size = new Vector2(boxCollider2D.size.x, Mathf.Lerp(2.8f, 2.9f, collidersFactor));
				}
				else
				{
					boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, Mathf.Lerp(0.45f, 0.3f, collidersFactor));
				}
			}
		}

		private void CalculateRemainingTime()
		{
			gameContext.playerEntity.sizeIncreased.Duration -= Time.deltaTime;
			if (gameContext.playerEntity.sizeIncreased.Duration <= 0f)
			{
				gameContext.playerEntity.RemoveSizeIncreased();
				SetPlayerSize(1f);
			}
		}
	}
}
