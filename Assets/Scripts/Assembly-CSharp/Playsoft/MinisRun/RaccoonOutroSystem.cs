using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class RaccoonOutroSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly IGroup<GameEntity> levelEndGroup;

		private bool isWaitAnimationStarted;

		private bool isPickUpAnimationStarted;

		private bool isRunAnimationStarted;

		private bool isChestDropInitialized;

		private bool isChestDropAnimationStarted;

		private Animator raccoonAnimator;

		private GameEntity chestEntity;

		public RaccoonOutroSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			levelEndGroup = gameContext.GetGroup(GameMatcher.LevelEnd);
		}

		public void Execute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			GameEntity singleEntity = levelEndGroup.GetSingleEntity();
			if (playerEntity != null && !playerEntity.isDead && singleEntity != null && singleEntity.hasView)
			{
				StartWaitAnimationAndReparentRaccoonTriggerIfPossible();
				StartPickUpAnimationIfPossible();
				StartRunAnimationIfPossible();
			}
		}

		private void StartWaitAnimationAndReparentRaccoonTriggerIfPossible()
		{
			GameEntity singleEntity = levelEndGroup.GetSingleEntity();
			if (!isWaitAnimationStarted)
			{
				isWaitAnimationStarted = true;
				raccoonAnimator = singleEntity.view.gameObject.transform.GetChild(0).GetComponent<Animator>();
				raccoonAnimator.SetTrigger("Wait");
				Transform child = singleEntity.view.gameObject.transform.GetChild(1);
				child.SetParent(singleEntity.view.gameObject.transform.parent);
			}
		}

		private void StartPickUpAnimationIfPossible()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (!isPickUpAnimationStarted && playerEntity.isRaccoonOutro)
			{
				isPickUpAnimationStarted = true;
				raccoonAnimator.SetTrigger("PickUpBag");
			}
		}

		private void StartRunAnimationIfPossible()
		{
			if (raccoonAnimator.GetCurrentAnimatorStateInfo(0).IsName("Running"))
			{
				isRunAnimationStarted = true;
				float num = CalculateSpeed() * SettingsService.GetSetting<SettingsRacoonAnimSO>().RaccoonIntroRaccoonSpeedModifier;
				raccoonAnimator.transform.position += Vector3.right * num;
			}
		}

		private float CalculateSpeed()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			if (worldEntity.world.Speed < SettingsService.SettingsSO.BaseSpeed)
			{
				worldEntity.world.Speed = Mathf.Lerp(worldEntity.world.Speed, SettingsService.SettingsSO.BaseSpeed, Time.deltaTime * SettingsService.SettingsSO.HitRecoverSpeed);
			}
			float num = ((!gameContext.playerEntity.hasAbilityFasterMovement) ? 1f : gameContext.playerEntity.abilityFasterMovement.Bonus);
			float num2 = worldEntity.world.Speed * num;
			return num2 * Time.smoothDeltaTime;
		}
	}
}
