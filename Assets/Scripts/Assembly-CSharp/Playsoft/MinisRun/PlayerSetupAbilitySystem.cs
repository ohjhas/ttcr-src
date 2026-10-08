using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public sealed class PlayerSetupAbilitySystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext _gameContext;

		private readonly StateContext _stateContext;

		private readonly IGroup<GameEntity> _abilityChangedListenersGroup;

		public PlayerSetupAbilitySystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			_gameContext = contexts.game;
			_stateContext = contexts.state;
			_abilityChangedListenersGroup = contexts.game.GetGroup(GameMatcher.AbilityChangedListener);
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.SetupAbility.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			RemoveExistingAbilities(playerEntity);
			AddCurrentAbility(playerEntity);
			_gameContext.playerEntity.RemoveSetupAbility();
		}

		private void RemoveExistingAbilities(GameEntity playerEntity)
		{
			if (playerEntity.hasAbilityScoreBonus)
			{
				playerEntity.RemoveAbilityScoreBonus();
			}
			if (playerEntity.hasAbilityUpgradeScore)
			{
				playerEntity.RemoveAbilityUpgradeScore();
			}
			if (playerEntity.hasAbilityFasterMovement)
			{
				playerEntity.RemoveAbilityFasterMovement();
			}
			if (playerEntity.isAbilityMagnetized)
			{
				playerEntity.isAbilityMagnetized = false;
				if (playerEntity.hasMagnetized && Mathf.Approximately(playerEntity.abilityChargingProgress.PercentCharged, 1f))
				{
					playerEntity.magnetized.Duration -= playerEntity.abilityChargingProgress.TimeLeft;
				}
			}
			if (playerEntity.hasAbilityResurrect)
			{
				playerEntity.RemoveAbilityResurrect();
			}
			if (playerEntity.hasAbilitySpeedup)
			{
				playerEntity.RemoveAbilitySpeedup();
				if (playerEntity.hasSpeedUp && Mathf.Approximately(playerEntity.abilityChargingProgress.PercentCharged, 1f))
				{
					playerEntity.speedUp.Duration -= playerEntity.abilityChargingProgress.TimeLeft;
				}
			}
			if (playerEntity.hasAbilityChargingTime)
			{
				playerEntity.RemoveAbilityChargingTime();
			}
			if (playerEntity.hasAbilityDuration)
			{
				playerEntity.RemoveAbilityDuration();
			}
		}

		private void AddCurrentAbility(GameEntity playerEntity)
		{
			if (_gameContext.worldEntity.levelSettings.levelTutorialSettings.CharacterAbilityEnabeld)
			{
				int charId = playerEntity.setupAbility.CharId;
				CSCharacterData charData = DataProviderHelper.GetCharData(charId);
				string name = "CharUpgradeData" + charId;
				StateEntity upgradeData = _stateContext.GetEntitiesWithName(name).SingleEntity();
				CharactersUpgradesBalancingData charactersUpgradesBalancingData = charData.Upgrades.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
				AbilityType aBILITYTYPE = charData.Ability.ABILITYTYPE;
				AbilitiesBalancingData abilitiesBalancingData = _stateContext.balancingDataEntity.abilitiesBalancingData.Map[aBILITYTYPE];
				int num = 1;
				switch (aBILITYTYPE)
				{
				case AbilityType.ScoreBonus:
					playerEntity.AddAbilityScoreBonus(Mathf.RoundToInt(charactersUpgradesBalancingData.Abilityparameter));
					break;
				case AbilityType.UpgradeScore:
					playerEntity.AddAbilityUpgradeScore(charactersUpgradesBalancingData.Abilityparameter);
					playerEntity.AddAbilityDuration(charactersUpgradesBalancingData.Abilityduration);
					playerEntity.AddAbilityChargingTime(charactersUpgradesBalancingData.Abilitychargetime);
					num = 0;
					break;
				case AbilityType.FasterMovement:
					playerEntity.AddAbilityFasterMovement(charactersUpgradesBalancingData.Abilityparameter);
					break;
				case AbilityType.Magnet:
					playerEntity.isAbilityMagnetized = true;
					playerEntity.AddAbilityChargingTime(charactersUpgradesBalancingData.Abilitychargetime);
					num = 0;
					break;
				case AbilityType.Speedup:
				case AbilityType.PoliceCarTom:
					playerEntity.AddAbilitySpeedup(charactersUpgradesBalancingData.Abilityparameter);
					playerEntity.AddAbilityChargingTime(charactersUpgradesBalancingData.Abilitychargetime);
					num = 0;
					break;
				case AbilityType.Revive:
					playerEntity.AddAbilityResurrect(charactersUpgradesBalancingData.Abilityparameter);
					break;
				case AbilityType.GiantSpeedup:
					playerEntity.AddAbilityGiantSpeedup(charactersUpgradesBalancingData.Abilityparameter);
					playerEntity.AddAbilityChargingTime(charactersUpgradesBalancingData.Abilitychargetime);
					num = 0;
					break;
				}
				playerEntity.ReplaceAbilityChargingProgress(num, 0f);
				SendSingalToAbilityChangedListeners(abilitiesBalancingData.Assetname);
			}
		}

		private void SendSingalToAbilityChangedListeners(string assetName)
		{
			GameEntity[] entities = _abilityChangedListenersGroup.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				gameEntity.abilityChangedListener.listener.OnAbilityChanged(assetName);
			}
		}
	}
}
