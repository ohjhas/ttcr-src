using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionCreateTokenAbilitySystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly InputContext inputContext;

		public CompanionCreateTokenAbilitySystem(Contexts contexts)
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
			return playerEntity != null && playerEntity.hasView && companionEntity != null && companionEntity.hasView && !playerEntity.isDead && companionEntity.hasAbilityCreateToken && companionEntity.hasAbilityTimer;
		}

		private void SetupAbility()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			GameObject gameObject = companionEntity.view.gameObject;
			GameObject gameObject2 = playerEntity.view.gameObject;
			companionEntity.isPlayerAttached = false;
			gameObject.GetComponent<Animator>().SetTrigger("Ability");
			Vector3 endValue = gameObject2.transform.localPosition + SettingsService.SettingsSO.AnchorToScreen;
			Tweener tweener = gameObject.transform.DOLocalMove(endValue, 0.7f);
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
			LevelElement levelElement = SettingsService.AssetsSettingsSO.GetLevelElement(GetAvailableTokenName());
			if (levelElement != null)
			{
				LevelElementData levelElementData = new LevelElementData(levelElement.Data);
				levelElementData.AssetName = levelElement.name;
				Vector3 position = gameContext.companionEntity.view.gameObject.transform.position;
				levelElementData.Position = position;
				AddParticleEffects(levelElementData.Position + Vector3.up * 0.5f);
				gameContext.CreateLevelElement(levelElementData);
			}
		}

		private void FinishAnimation()
		{
			GameEntity companion = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			GameObject companionGO = companion.view.gameObject;
			GameObject playerGO = playerEntity.view.gameObject;
			companionGO.GetComponent<Animator>().SetTrigger("Float");
			companion.isPlayerAttached = true;
			TweenerCore<float, float, FloatOptions> tweener = DOTween.To(delegate
			{
				Vector3 a = playerGO.transform.localPosition + companion.offsetPosition.Pos;
				return Vector3.Distance(a, companionGO.transform.localPosition);
			}, delegate(float value)
			{
				Vector3 a = playerGO.transform.localPosition + companion.offsetPosition.Pos;
				companionGO.transform.localPosition = Vector3.Lerp(a, companionGO.transform.localPosition, value);
			}, 0f, 1f);
			tweener.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(tweener);
			});
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
			GameEntity companionEntity = gameContext.companionEntity;
			List<string> availableTokenNames = companionEntity.abilityCreateToken.AvailableTokenNames;
			return (availableTokenNames.Count >= 1) ? availableTokenNames[Random.Range(0, availableTokenNames.Count)] : string.Empty;
		}
	}
}
