using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionRandomPowerupAbilitySystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly InputContext inputContext;

		public CompanionRandomPowerupAbilitySystem(Contexts contexts)
		{
			gameContext = contexts.game;
			inputContext = contexts.input;
		}

		public void Execute()
		{
			if (CanExecute())
			{
				GameEntity companionEntity = gameContext.companionEntity;
				companionEntity.abilityTimer.TimeLeft -= Time.deltaTime;
				if (companionEntity.abilityTimer.TimeLeft <= 0f)
				{
					companionEntity.ReplaceAbilityTimer(companionEntity.abilityChargingTime.ChargingTime);
					SetupAbility();
				}
			}
		}

		private bool CanExecute()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity != null && playerEntity.hasView && companionEntity != null && companionEntity.hasView && !playerEntity.isDead && companionEntity.isAbilityRandomPowerup && companionEntity.hasAbilityTimer;
		}

		private void SetupAbility()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameObject gameObject = companionEntity.view.gameObject;
			gameObject.GetComponent<Animator>().SetTrigger("Ability");
			float unicornAnimationLength = SettingsService.SettingsSO.UnicornAnimationLength;
			float time = 0f;
			TweenerCore<float, float, FloatOptions> tweener = DOTween.To(() => time, delegate(float curValue)
			{
				time = curValue;
			}, unicornAnimationLength, unicornAnimationLength);
			tweener.OnComplete(delegate
			{
				PlaceToken();
				FinishAnimation();
			});
			tweener.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(tweener);
			});
		}

		private void PlaceToken()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			GameObject gameObject = playerEntity.view.gameObject;
			LevelElement levelElement = SettingsService.AssetsSettingsSO.GetLevelElement(GetAvailableTokenName());
			if (levelElement != null)
			{
				LevelElementData levelElementData = new LevelElementData(levelElement.Data);
				levelElementData.AssetName = levelElement.name;
				levelElementData.Position = gameObject.transform.position + SettingsService.SettingsSO.AnchorToScreen;
				AddParticleEffects(levelElementData.Position + Vector3.up * 0.5f);
				AddParticleEffects(levelElementData.Position + Vector3.left * 0.5f);
				AddParticleEffects(levelElementData.Position + Vector3.right * 0.5f);
				AddParticleEffects(levelElementData.Position + Vector3.down * 0.5f);
				gameContext.CreateLevelElement(levelElementData);
			}
		}

		private void FinishAnimation()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameObject gameObject = companionEntity.view.gameObject;
			gameObject.GetComponent<Animator>().SetTrigger("Float");
			companionEntity.isPlayerAttached = true;
		}

		private void AddParticleEffects(Vector3 position)
		{
			InputEntity inputEntity = inputContext.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.AddCreateParticles(position);
		}

		private void ResetTweenOncompleteIfPlayerDied(Tween tween)
		{
			if (gameContext.playerEntity.isDead)
			{
				tween.Kill();
			}
		}

		private string GetAvailableTokenName()
		{
			List<string> list = new List<string>();
			list.Add("GiantToken");
			list.Add("MagnetCollectible");
			list.Add("SpeedUpToken");
			list.Add("UpgradeScoreToken");
			List<string> list2 = list;
			return (list2.Count >= 1) ? list2[Random.Range(0, list2.Count)] : string.Empty;
		}
	}
}
