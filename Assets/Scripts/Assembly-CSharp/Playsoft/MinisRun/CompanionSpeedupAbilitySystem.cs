using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionSpeedupAbilitySystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		public CompanionSpeedupAbilitySystem(Contexts contexts)
		{
			gameContext = contexts.game;
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
					EnableSpeedup();
					AnimateCompanion();
				}
			}
		}

		private bool CanExecute()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity != null && playerEntity.hasView && companionEntity != null && companionEntity.hasView && !playerEntity.isDead && companionEntity.hasAbilitySpeedup && companionEntity.hasAbilityTimer;
		}

		private void AnimateCompanion()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			GameObject companionGO = companionEntity.view.gameObject;
			GameObject playerGO = playerEntity.view.gameObject;
			companionEntity.isPlayerAttached = false;
			companionGO.GetComponent<Animator>().SetTrigger("Ability");
			TweenerCore<Vector3, Vector3, VectorOptions> tweener = DOTween.To(endValue: new Vector3(SettingsService.SettingsSO.AnchorToScreen.x * 0.33f, SettingsService.SettingsSO.AnchorToScreen.y, SettingsService.SettingsSO.AnchorToScreen.z), getter: () => companionGO.transform.localPosition - playerGO.transform.localPosition, setter: delegate(Vector3 pos)
			{
				companionGO.transform.localPosition = playerGO.transform.localPosition + pos;
			}, duration: 0.7f);
			tweener.OnComplete(FinishAnimation);
			tweener.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(tweener);
			});
		}

		private void FinishAnimation()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameObject companionGO = companionEntity.view.gameObject;
			GameEntity playerEntity = gameContext.playerEntity;
			GameObject playerGO = playerEntity.view.gameObject;
			TweenerCore<Vector3, Vector3, VectorOptions> tweener = DOTween.To(() => companionGO.transform.localPosition - playerGO.transform.localPosition, delegate(Vector3 pos)
			{
				companionGO.transform.localPosition = playerGO.transform.localPosition + pos;
			}, companionEntity.offsetPosition.Pos, 1f);
			tweener.SetDelay(SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration - 0.7f);
			tweener.OnStart(FinishAbilityAnim);
			tweener.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(tweener);
			});
		}

		private void FinishAbilityAnim()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameObject gameObject = companionEntity.view.gameObject;
			gameObject.GetComponent<Animator>().SetTrigger("Float");
			companionEntity.isPlayerAttached = true;
		}

		private void EnableSpeedup()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			float speedUpDuration = SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration;
			float b = ((!playerEntity.hasInvulnerability) ? 0f : playerEntity.invulnerability.TimeLeft);
			float newTimeLeft = Mathf.Max(speedUpDuration, b);
			playerEntity.ReplaceSpeedUp(speedUpDuration, 1f);
			playerEntity.ReplaceInvulnerability(newTimeLeft);
			playerEntity.ReplaceOnClouds(speedUpDuration);
		}

		private void ResetTweenOncompleteIfPlayerDied(Tween tween)
		{
			if (gameContext.playerEntity.isDead)
			{
				tween.Kill();
			}
		}
	}
}
