using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionSetupAbilitySystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext _gameContext;

		private readonly StateContext _stateContext;

		public CompanionSetupAbilitySystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			_gameContext = contexts.game;
			_stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.SetupAbility.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isCompanion;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity companionEntity = _gameContext.companionEntity;
			RemoveExistingAbilities(companionEntity);
			AddCurrentAbility(companionEntity);
			companionEntity.RemoveSetupAbility();
		}

		private void RemoveExistingAbilities(GameEntity companionEntity)
		{
			if (companionEntity.hasAbilityCreateToken)
			{
				companionEntity.RemoveAbilityCreateToken();
			}
			if (companionEntity.hasAbilityTimer)
			{
				companionEntity.RemoveAbilityTimer();
			}
			if (companionEntity.hasAbilityResurrect)
			{
				companionEntity.RemoveAbilityResurrect();
			}
			if (companionEntity.isAbilityDragonBlast)
			{
				companionEntity.isAbilityDragonBlast = false;
			}
			if (companionEntity.hasAbilitySpeedup)
			{
				companionEntity.RemoveAbilitySpeedup();
				GameEntity playerEntity = _gameContext.playerEntity;
				if (playerEntity.hasSpeedUp && Mathf.Approximately(playerEntity.abilityChargingProgress.PercentCharged, 1f))
				{
					playerEntity.speedUp.Duration -= playerEntity.abilityChargingProgress.TimeLeft;
				}
			}
			if (companionEntity.isAbilityRandomPowerup)
			{
				companionEntity.isAbilityRandomPowerup = false;
			}
			if (companionEntity.hasAbilityChargingTime)
			{
				companionEntity.RemoveAbilityChargingTime();
			}
			if (companionEntity.hasAbilityDuration)
			{
				companionEntity.RemoveAbilityDuration();
			}
			if (companionEntity.hasAbilityShield)
			{
				companionEntity.RemoveAbilityShield();
			}
		}

		private void AddCurrentAbility(GameEntity companionEntity)
		{
			int charId = companionEntity.setupAbility.CharId;
			CSCompanionData compData = DataProviderHelper.GetCompData(charId);
			string name = "CompUpgradeData" + charId;
			StateEntity upgradeData = _stateContext.GetEntitiesWithName(name).SingleEntity();
			CompanionsUpgradesBalancingData companionsUpgradesBalancingData = compData.Upgrades.Find((CompanionsUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
			switch (compData.Ability.ABILITYTYPE)
			{
			case AbilityType.CreateStamina:
			case AbilityType.CreateSpeedup:
				companionEntity.AddAbilityTimer(Random.Range(0f, companionsUpgradesBalancingData.Abilitychargetime));
				companionEntity.AddAbilityCreateToken(GetAvailableTokens(compData.Ability.ABILITYTYPE));
				companionEntity.AddAbilityChargingTime(companionsUpgradesBalancingData.Abilitychargetime);
				break;
			case AbilityType.CreatePowerup:
				companionEntity.AddAbilityTimer(Random.Range(0f, companionsUpgradesBalancingData.Abilitychargetime));
				companionEntity.isAbilityRandomPowerup = true;
				companionEntity.AddAbilityChargingTime(companionsUpgradesBalancingData.Abilitychargetime);
				break;
			case AbilityType.DragonBlast:
				companionEntity.AddAbilityTimer(Random.Range(0f, companionsUpgradesBalancingData.Abilitychargetime));
				companionEntity.isAbilityDragonBlast = true;
				companionEntity.AddAbilityChargingTime(companionsUpgradesBalancingData.Abilitychargetime);
				companionEntity.AddAbilityDuration(companionsUpgradesBalancingData.Abilityduration);
				companionEntity.AddAbilityChargingProgress(0f, 0f);
				break;
			case AbilityType.Revive:
				companionEntity.AddAbilityResurrect(companionsUpgradesBalancingData.Abilityparameter);
				companionEntity.AddAbilityChargingProgress(1f, 0f);
				break;
			case AbilityType.Speedup:
				companionEntity.AddAbilityTimer(Random.Range(0f, companionsUpgradesBalancingData.Abilitychargetime));
				companionEntity.AddAbilitySpeedup(companionsUpgradesBalancingData.Abilityparameter);
				companionEntity.AddAbilityChargingTime(companionsUpgradesBalancingData.Abilitychargetime);
				break;
			case AbilityType.Shield:
				companionEntity.AddAbilityTimer(Random.Range(0f, companionsUpgradesBalancingData.Abilitychargetime));
				companionEntity.AddAbilityShield(companionsUpgradesBalancingData.Abilityduration);
				companionEntity.AddAbilityChargingTime(companionsUpgradesBalancingData.Abilitychargetime);
				companionEntity.AddAbilityChargingProgress(0f, 0f);
				break;
			case AbilityType.PoliceCarTom:
			case AbilityType.GiantSpeedup:
				break;
			}
		}

		private List<string> GetAvailableTokens(AbilityType abilitytype)
		{
			switch (abilitytype)
			{
			case AbilityType.CreateStamina:
			{
				List<string> list = new List<string>();
				list.Add("SmallStaminaToken");
				return list;
			}
			case AbilityType.CreateSpeedup:
			{
				List<string> list = new List<string>();
				list.Add("SpeedUpToken");
				return list;
			}
			default:
				return null;
			}
		}
	}
}
