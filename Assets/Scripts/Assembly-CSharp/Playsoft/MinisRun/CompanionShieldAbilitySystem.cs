using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionShieldAbilitySystem : IExecuteSystem, ISystem
	{
		private const string CLOUD_SOUND_ID = "companion_ability_cloud_loop";

		private readonly StateContext stateContext;

		private readonly GameContext gameContext;

		private float abilitySwitchAnimTime = 0.3f;

		public CompanionShieldAbilitySystem(Contexts contexts)
		{
			stateContext = contexts.state;
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (!CanExecute())
			{
				return;
			}
			GameEntity companionEntity = gameContext.companionEntity;
			if (companionEntity.abilityChargingProgress.PercentCharged < 1f)
			{
				companionEntity.abilityTimer.TimeLeft -= Time.deltaTime;
				if (companionEntity.abilityTimer.TimeLeft <= 0f)
				{
					companionEntity.ReplaceAbilityChargingProgress(1f, companionEntity.abilityShield.Duration - 1.5f * abilitySwitchAnimTime);
					StartSound();
					SetupAbility();
					EnableInvulnerability();
				}
				return;
			}
			companionEntity.abilityChargingProgress.TimeLeft -= Time.deltaTime;
			if (companionEntity.abilityChargingProgress.TimeLeft <= 0f)
			{
				companionEntity.ReplaceAbilityTimer(companionEntity.abilityChargingTime.ChargingTime);
				companionEntity.ReplaceAbilityChargingProgress(0f, 0f);
				StopSound();
				FinishAnimation();
			}
			UpdatePosition();
		}

		private bool CanExecute()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity != null && playerEntity.hasView && companionEntity != null && companionEntity.hasView && !playerEntity.isDead && companionEntity.hasAbilityShield && companionEntity.hasAbilityChargingTime && companionEntity.hasAbilityTimer && companionEntity.hasAbilityChargingProgress;
		}

		private void SetupAbility()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameObject gameObject = companionEntity.view.gameObject;
			companionEntity.isPlayerAttached = true;
			gameObject.GetComponent<Animator>().SetTrigger("Ability");
			UpdatePosition();
		}

		private void UpdatePosition()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			GameObject gameObject = companionEntity.view.gameObject;
			Transform transform = gameObject.transform.Find("ability");
			GameObject gameObject2 = playerEntity.view.gameObject;
			float x = gameObject2.transform.localScale.x;
			transform.localScale = gameObject2.transform.localScale;
			Vector3 endValue = gameObject2.transform.localPosition + Vector3.up * 4.55f * x;
			Tweener tweener = gameObject.transform.DOLocalMove(endValue, abilitySwitchAnimTime * 0.5f);
			tweener.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(tweener);
			});
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
			}, 0f, abilitySwitchAnimTime * 0.5f);
			tweener.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(tweener);
			});
		}

		private void ResetTweenOncompleteIfPlayerDied(Tween tween)
		{
			if (gameContext.playerEntity.isDead)
			{
				tween.Kill();
			}
		}

		private void EnableInvulnerability()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			float b = ((!playerEntity.hasInvulnerability) ? 0f : playerEntity.invulnerability.TimeLeft);
			float newTimeLeft = Mathf.Max(companionEntity.abilityShield.Duration, b);
			playerEntity.ReplaceInvulnerability(newTimeLeft);
		}

		private void StartSound()
		{
			stateContext.CreateEntity().AddPlayLoopSoundEffect("companion_ability_cloud_loop");
		}

		private void StopSound()
		{
			stateContext.CreateEntity().AddStopLoopSoundEffect("companion_ability_cloud_loop");
		}
	}
}
