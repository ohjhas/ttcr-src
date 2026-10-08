using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class AbilityCoolDownSystem : IInitializeSystem, IExecuteSystem, ISystem
	{
		private const float BAR_OFFSET = 0.7f;

		private readonly GameContext gameContext;

		private GameEntity barEntity;

		private Transform headTopTransform;

		private SpriteRenderer barRenderer;

		private Vector3 barPosition;

		public AbilityCoolDownSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Initialize()
		{
			barEntity = gameContext.CreateEntity();
			barEntity.AddAsset("AbilityChargingBar");
			barPosition = Vector3.zero;
		}

		public void Execute()
		{
			if (gameContext.playerEntity != null && gameContext.playerEntity.hasView)
			{
				if (null == headTopTransform)
				{
					headTopTransform = gameContext.playerEntity.view.gameObject.transform.Find("model/head_top");
				}
				if (null == barRenderer && barEntity.hasView)
				{
					barRenderer = barEntity.view.gameObject.GetComponentInChildren<SpriteRenderer>();
				}
				SetPosition();
				SetFill();
			}
		}

		private void SetPosition()
		{
			barPosition.x = headTopTransform.position.x;
			barPosition.y = headTopTransform.position.y + 0.7f;
			barEntity.ReplacePosition(barPosition);
		}

		private void SetFill()
		{
			if (null == barRenderer)
			{
				return;
			}
			float num = ((!gameContext.playerEntity.hasAbilityChargingTime) ? 0f : gameContext.playerEntity.abilityChargingTime.ChargingTime);
			if (num < 0.1f)
			{
				barRenderer.enabled = false;
				return;
			}
			AbilityChargingProgress abilityChargingProgress = gameContext.playerEntity.abilityChargingProgress;
			if (abilityChargingProgress.PercentCharged >= 1f)
			{
				barRenderer.enabled = false;
				return;
			}
			barRenderer.enabled = true;
			float percentCharged = abilityChargingProgress.PercentCharged;
			barRenderer.transform.parent.localScale = new Vector3(percentCharged, 1f, 1f);
		}
	}
}
