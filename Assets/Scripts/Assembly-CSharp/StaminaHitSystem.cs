using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class StaminaHitSystem : ReactiveSystem<InputEntity>
{
	private readonly StateContext stateContext;

	public StaminaHitSystem(Contexts contexts)
		: base((IContext<InputEntity>)contexts.input)
	{
		stateContext = contexts.state;
	}

	protected override bool Filter(InputEntity entity)
	{
		return entity.hasCollision && entity.collision.to != null && entity.collision.from != null;
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
	{
		return context.CreateCollector(InputMatcher.Collision);
	}

	protected override void Execute(List<InputEntity> entities)
	{
		foreach (InputEntity entity in entities)
		{
			GameEntity gameEntity = entity.collision.from as GameEntity;
			GameEntity gameEntity2 = entity.collision.to as GameEntity;
			if (!gameEntity.isPlayer || !gameEntity2.isObstacle)
			{
				continue;
			}
			if (gameEntity2.isKillZone)
			{
				gameEntity.isDead = true;
			}
			else if (!gameEntity.hasInvulnerability)
			{
				float num = stateContext.balancingDataEntity.staminaSettingsBalancingData.Map[StaminaSettings.StaminaLostPerHit];
				float num2 = ((!gameEntity.isGodMode) ? num : 0f);
				gameEntity.ReplaceStamina(gameEntity.stamina.Value - num2);
				Collider2D component = gameEntity.view.gameObject.GetComponent<Collider2D>();
				Collider2D collider2D = gameEntity2.view.gameObject.GetComponent<Collider2D>();
				if (collider2D == null)
				{
					collider2D = gameEntity2.view.gameObject.GetComponentInChildren<Collider2D>();
				}
				ColliderDistance2D colliderDistance2D = component.Distance(collider2D);
				CreateVisualFeedbackTrigger(gameEntity.view.gameObject.transform.position.y + component.offset.y > colliderDistance2D.pointA.y);
				UpdatePlayerState(gameEntity);
			}
		}
	}

	private void UpdatePlayerState(GameEntity player)
	{
		if (player.stamina.Value <= 0f)
		{
			player.isDead = true;
			return;
		}
		player.AddInvulnerability(SettingsService.SettingsSO.InvulnerabilityTime);
		ChangeWorldSpeed();
		AddPlayerBlinking(player);
	}

	private void CreateVisualFeedbackTrigger(bool touchPointIsLow)
	{
		InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
		inputEntity.isInput = true;
		inputEntity.ReplaceCollisionFeedback(touchPointIsLow);
	}

	private void ChangeWorldSpeed()
	{
		GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
		worldEntity.world.Speed = SettingsService.SettingsSO.HitSpeed;
	}

	private void AddPlayerBlinking(GameEntity player)
	{
		float invulnerabilityTime = SettingsService.SettingsSO.InvulnerabilityTime;
		int numberOfCycles = SettingsService.GetSetting<SettingsVisualFeedbackSO>().NumberOfCycles;
		player.ReplacePlayerBlinking(invulnerabilityTime, invulnerabilityTime, numberOfCycles);
	}
}
