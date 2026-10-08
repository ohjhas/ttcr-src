using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionDragonBlastAbilitySystem : IExecuteSystem, ISystem
	{
		private const string DRAGON_SOUND_ID = "companion_ability_dragon";

		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		private readonly IGroup<GameEntity> obstacles;

		public CompanionDragonBlastAbilitySystem(Contexts contexts)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
			obstacles = gameContext.GetGroup(GameMatcher.Obstacle);
		}

		public void Execute()
		{
			if (CanExecute())
			{
				GameEntity companionEntity = gameContext.companionEntity;
				if (companionEntity.abilityChargingProgress.PercentCharged < 1f)
				{
					companionEntity.abilityTimer.TimeLeft -= Time.deltaTime;
					if (companionEntity.abilityTimer.TimeLeft <= 0f)
					{
						companionEntity.ReplaceAbilityChargingProgress(1f, companionEntity.abilityDuration.Duration);
						SetupAbility();
						StartParticles();
						StartSound();
					}
					return;
				}
				companionEntity.abilityChargingProgress.TimeLeft -= Time.deltaTime;
				if (companionEntity.abilityChargingProgress.TimeLeft <= 0f || gameContext.playerEntity.isDead || gameContext.playerEntity.hasLevelFinished)
				{
					companionEntity.ReplaceAbilityTimer(companionEntity.abilityChargingTime.ChargingTime);
					companionEntity.ReplaceAbilityChargingProgress(0f, 0f);
					FinishAnimation();
					StopParticles();
					StopSound();
				}
				DetroyObstacles();
			}
			else if (AbilityIsPlaying())
			{
				GameEntity companionEntity2 = gameContext.companionEntity;
				companionEntity2.ReplaceAbilityTimer(companionEntity2.abilityChargingTime.ChargingTime);
				companionEntity2.ReplaceAbilityChargingProgress(0f, 0f);
				StopParticles();
			}
		}

		private bool AbilityIsPlaying()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity != null && playerEntity.hasView && playerEntity.isDead && companionEntity != null && companionEntity.hasView && companionEntity.isAbilityDragonBlast && companionEntity.abilityChargingProgress.TimeLeft > 0f;
		}

		private bool CanExecute()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity != null && playerEntity.hasView && companionEntity != null && companionEntity.hasView && !playerEntity.isDead && companionEntity.isAbilityDragonBlast && companionEntity.hasAbilityChargingProgress && companionEntity.hasAbilityTimer;
		}

		private void SetupAbility()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			GameEntity playerEntity = gameContext.playerEntity;
			GameObject gameObject = companionEntity.view.gameObject;
			GameObject gameObject2 = playerEntity.view.gameObject;
			companionEntity.isPlayerAttached = false;
			gameObject.GetComponent<Animator>().SetTrigger("Ability");
			Vector3 vector = new Vector3(SettingsService.SettingsSO.AnchorToScreen.x * 0.5f, SettingsService.SettingsSO.AnchorToScreen.y, SettingsService.SettingsSO.AnchorToScreen.z);
			Vector3 endValue = gameObject2.transform.localPosition + vector;
			Tweener tweener = gameObject.transform.DOLocalMove(endValue, 0.7f);
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
			}, 0f, 1f);
			tweener.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(tweener);
			});
		}

		private void ResetTweenOncompleteIfPlayerDied(Tween tween)
		{
			if (gameContext.playerEntity.isDead || gameContext.playerEntity.hasLevelFinished)
			{
				if (gameContext.playerEntity.hasLevelFinished)
				{
					gameContext.companionEntity.isPlayerAttached = true;
				}
				tween.Kill();
				StopParticles();
				StopSound();
			}
		}

		private void DetroyObstacles()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			Transform transform = companionEntity.view.gameObject.transform;
			GameEntity[] entities = obstacles.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && !gameEntity.isFlyAway)
				{
					Transform component = gameEntity.view.gameObject.GetComponent<Transform>();
					if (component.position.x >= transform.position.x && component.position.x <= transform.position.x + SettingsService.SettingsSO.DragonBlastRange)
					{
						gameEntity.isFlyAway = true;
					}
				}
			}
		}

		private void StartParticles()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			ParticleSystem[] componentsInChildren = companionEntity.view.gameObject.GetComponentsInChildren<ParticleSystem>(true);
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem particleSystem in array)
			{
				ParticleSystem.MainModule main = particleSystem.main;
				main.loop = true;
				main.simulationSpeed = 2f;
				CreateParticlesTween(main);
				particleSystem.Play();
			}
		}

		private void CreateParticlesTween(ParticleSystem.MainModule mainModule)
		{
			Tweener particleTween = DOTween.To(() => mainModule.simulationSpeed, delegate(float x)
			{
				mainModule.simulationSpeed = x;
			}, 1f, 0.5f).SetDelay(0.3f);
			particleTween.OnUpdate(delegate
			{
				ResetTweenOncompleteIfPlayerDied(particleTween);
			});
		}

		private void StopParticles()
		{
			GameEntity companionEntity = gameContext.companionEntity;
			ParticleSystem[] componentsInChildren = companionEntity.view.gameObject.GetComponentsInChildren<ParticleSystem>(true);
			ParticleSystem[] array = componentsInChildren;
			foreach (ParticleSystem particleSystem in array)
			{
				ParticleSystem.MainModule main = particleSystem.main;
				main.simulationSpeed = 2f;
				particleSystem.Stop();
			}
		}

		private void StartSound()
		{
			stateContext.CreateEntity().AddPlayLoopSoundEffect("companion_ability_dragon");
		}

		private void StopSound()
		{
			stateContext.CreateEntity().AddStopLoopSoundEffect("companion_ability_dragon");
		}
	}
}
