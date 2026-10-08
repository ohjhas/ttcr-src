using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class PlayerDieSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	private readonly InputContext inputContext;

	private readonly UIContext uiContext;

	private readonly StateContext stateContext;

	private float DistanceToSlideDeath = 4f;

	public PlayerDieSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
		inputContext = contexts.input;
		uiContext = contexts.uI;
		stateContext = contexts.state;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Dead);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isPlayer;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		GameEntity playerEntity = gameContext.playerEntity;
		GameObject gameObject = playerEntity.view.gameObject;
		Animator component = gameObject.GetComponent<Animator>();
		float duration = ((!gameContext.playerEntity.isAbilityRun) ? 1f : 2f);
		PlayFailMusic();
		StopMovementAnimations(component);
		component.SetTrigger("Die");
		component.SetFloat("DeathAnim", Mathf.Round(Random.Range(0f, 1f)));
		CreateDeadFeedbackTrigger();
		float endValue = gameObject.transform.position.x + DistanceToSlideDeath;
		gameObject.transform.DOMoveX(endValue, duration).OnComplete(delegate
		{
			OnDeath();
		});
	}

	private void PlayFailMusic()
	{
		gameContext.CreateEntity().isPlayGameFailMusic = true;
	}

	private void StopMovementAnimations(Animator playerAnimator)
	{
		gameContext.playerEntity.isSliding = false;
		InputEntity inputEntity = inputContext.CreateEntity();
		inputEntity.isInput = true;
		inputEntity.isInputSlideStop = true;
		playerAnimator.SetBool("Slide", false);
		playerAnimator.SetBool("Jump", false);
		gameContext.playerEntity.isAbilityRun = false;
		playerAnimator.SetFloat("SpecialRun", 0f);
	}

	private void CreateDeadFeedbackTrigger()
	{
		InputEntity inputEntity = inputContext.CreateEntity();
		inputEntity.isInput = true;
		inputEntity.isDeadFeedback = true;
	}

	private void OnDeath()
	{
		GameEntity playerEntity = gameContext.playerEntity;
		RemoveVisualEffects(playerEntity);
		bool flag = !playerEntity.isUnderTheScreen && CanRelay() && !playerEntity.isPlayerRelay && SettingsService.SettingsSO.GameplayType == GameplayType.EndlessRun;
		bool flag2 = CanRevive() && playerEntity.isUnderTheScreen && !playerEntity.isPlayerRevive;
		bool flag3 = !playerEntity.isUnderTheScreen && HasReviveAbilityCharged();
		bool flag4 = CanResurect() && playerEntity.stamina.Value <= 0f && !playerEntity.hasPlayerResurrect;
		if (flag3)
		{
			ResurrectForFree(playerEntity);
		}
		else if (flag)
		{
			Animator component = playerEntity.view.gameObject.GetComponent<Animator>();
			Object.DestroyImmediate(component);
			uiContext.OpenDialog<RelayPlayerScreen>();
		}
		else if (flag2)
		{
			uiContext.OpenDialog<RevivePlayerScreen>();
		}
		else if (flag4)
		{
			uiContext.OpenDialog<ResurectPlayerScreen>();
		}
		else
		{
			uiContext.OpenDialog<FailScreen>();
			SendPlayerLostEvent();
		}
	}

	private void ResurrectForFree(GameEntity player)
	{
		StateEntity stateEntity = stateContext.CreateEntity();
		stateEntity.isTransaction = true;
		stateEntity.AddHardCurrencyPrice(0);
		stateEntity.isTransactionBegin = true;
		stateEntity.AddBoughtPlayerResurect(SettingsService.GetSetting<SettingsPowerupsSO>().ReviveAbilityDelay);
	}

	private void RemoveVisualEffects(GameEntity entity)
	{
		if (entity.hasPlayerBlinking)
		{
			entity.RemovePlayerBlinking();
		}
	}

	private bool CanRelay()
	{
		int amount = stateContext.GetEntitiesWithName("SecondCharSelectData").SingleEntity().integer.Amount;
		CharactersStaticBalancingData charactersStaticBalancingData = stateContext.balancingDataEntity.characterStaticBalancingData.Map[amount];
		string name = "CharUnlockData" + amount;
		int num = stateContext.balancingDataEntity.restoreSettingsBalancingData.Map[RestoreSettings.RelayCost];
		StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
		StateEntity stateEntity2 = stateContext.GetEntitiesWithName(name).SingleEntity();
		int num2 = stateEntity.integer.Amount + gameContext.worldEntity.coins.Value;
		return num2 >= num && (stateEntity2.boolean.Value || charactersStaticBalancingData.Unlocked);
	}

	private bool CanRevive()
	{
		int num = stateContext.balancingDataEntity.restoreSettingsBalancingData.Map[RestoreSettings.ReviveCost];
		StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
		return PlayerRestoreHelper.IsReviveAvailable() && (PlayerRestoreHelper.IsReviveForVideoAvailable() || stateEntity.integer.Amount >= num);
	}

	private bool CanResurect()
	{
		int num = stateContext.balancingDataEntity.restoreSettingsBalancingData.Map[RestoreSettings.ResurectCost];
		StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
		return PlayerRestoreHelper.IsResurectAvailable() && (PlayerRestoreHelper.IsResurectForVideoAvailable() || stateEntity.integer.Amount >= num);
	}

	private bool HasReviveAbilityCharged()
	{
		return (gameContext.playerEntity.hasAbilityResurrect && Mathf.Approximately(gameContext.playerEntity.abilityChargingProgress.PercentCharged, 1f)) || (gameContext.companionEntity != null && gameContext.companionEntity.hasAbilityResurrect && Mathf.Approximately(gameContext.companionEntity.abilityChargingProgress.PercentCharged, 1f));
	}

	private void SendPlayerLostEvent()
	{
		InputEntity inputEntity = inputContext.CreateEntity();
		inputEntity.isInput = true;
		inputEntity.isPlayerLost = true;
	}
}
