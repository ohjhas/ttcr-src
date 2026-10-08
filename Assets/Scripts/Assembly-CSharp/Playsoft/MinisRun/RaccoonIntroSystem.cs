using DG.Tweening;
using Entitas;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class RaccoonIntroSystem : IInitializeSystem, IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly IGroup<GameEntity> listeners;

		private readonly SettingsRacoonAnimSO settingsRaccoonAnimSO;

		private GameEntity raccoonEntity;

		private bool isIntroStarted;

		private bool isRaccoonTime;

		private bool isCharacterTime;

		private bool hasRaccoonStartedRunning;

		public RaccoonIntroSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			listeners = contexts.game.GetGroup(GameMatcher.RaccoonIntrosListener);
			settingsRaccoonAnimSO = SettingsService.GetSetting<SettingsRacoonAnimSO>();
		}

		private bool CanExecute()
		{
			return !GameIntroHelper.IsIntroTime() && gameContext.worldEntity.isGameStarted && raccoonEntity != null && raccoonEntity.hasView && gameContext.playerEntity != null && gameContext.playerEntity.hasView;
		}

		public void Initialize()
		{
			if (!GameIntroHelper.IsIntroTime())
			{
				UpdatePlayerEntity();
				CreateRaccoonEntity();
				InformUI();
			}
		}

		private void CreateRaccoonEntity()
		{
			raccoonEntity = gameContext.CreateEntity();
			raccoonEntity.isRaccoon = true;
			raccoonEntity.AddAsset("Characters/Ingame/Raccoon");
			raccoonEntity.isLoadAssetAsync = true;
			raccoonEntity.AddPosition(settingsRaccoonAnimSO.RaccoonIntroCharacterCameraOffsetInitial);
			raccoonEntity.isGodMode = true;
		}

		private void UpdatePlayerEntity()
		{
			gameContext.playerEntity.AddRaccoonIntro(RaccoonIntroStep.HUDHidden);
			gameContext.playerEntity.ReplacePosition(settingsRaccoonAnimSO.RaccoonIntroCharacterCameraOffsetInitial);
			gameContext.playerEntity.isGodMode = true;
		}

		private void InformUI()
		{
			GameEntity[] entities = listeners.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				gameEntity.raccoonIntrosListener.listener.OnRaccoonIntro();
			}
		}

		private void RunSequence()
		{
			DOTween.Sequence().InsertCallback(settingsRaccoonAnimSO.RaccoonIntroRaccoonRunInDelay, delegate
			{
				isRaccoonTime = true;
				PlayRacconSound();
			}).InsertCallback(settingsRaccoonAnimSO.RaccoonIntroCharacterRunInDelay, delegate
			{
				isCharacterTime = true;
			});
		}

		public void Execute()
		{
			if (CanExecute() && gameContext.playerEntity != null && gameContext.playerEntity.hasRaccoonIntro)
			{
				if (!isIntroStarted)
				{
					RunSequence();
					isIntroStarted = true;
				}
				MoveRaccoonIfNeeded();
				MoveCharacterIfNeeded();
			}
		}

		private void MoveRaccoonIfNeeded()
		{
			if (isRaccoonTime)
			{
				MoveRaccoon();
				if (HasRaccoonReachedFinishPosition())
				{
					FinishRaccoonRunInStep();
				}
			}
		}

		private void MoveCharacterIfNeeded()
		{
			if (isCharacterTime)
			{
				MoveCharacter();
				if (HasCharacterReachedFinishPosition())
				{
					FinishCharacterRunInStep();
				}
			}
		}

		private void PlayRacconSound()
		{
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("racoon_run_begin");
		}

		private void MoveRaccoon()
		{
			float num = gameContext.worldEntity.world.Speed * Time.smoothDeltaTime * settingsRaccoonAnimSO.RaccoonIntroRaccoonSpeedModifier;
			raccoonEntity.view.gameObject.transform.position += Vector3.right * num;
			if (!hasRaccoonStartedRunning)
			{
				raccoonEntity.view.gameObject.GetComponent<Animator>().SetTrigger("Run");
				hasRaccoonStartedRunning = true;
			}
		}

		private void MoveCharacter()
		{
			float num = gameContext.worldEntity.world.Speed * Time.smoothDeltaTime;
			gameContext.playerEntity.view.gameObject.transform.position += Vector3.right * num;
		}

		private bool HasRaccoonReachedFinishPosition()
		{
			return raccoonEntity.view.gameObject.transform.position.x >= settingsRaccoonAnimSO.RaccoonIntroRaccoonCameraOffsetFinal.x;
		}

		private bool HasCharacterReachedFinishPosition()
		{
			return gameContext.playerEntity.view.gameObject.transform.position.x >= SettingsService.SettingsSO.PlayerCameraOffset.x;
		}

		private void FinishRaccoonRunInStep()
		{
			isRaccoonTime = false;
			raccoonEntity.view.gameObject.DestroyGameObject();
			raccoonEntity.isDestroyed = true;
		}

		private void FinishCharacterRunInStep()
		{
			isCharacterTime = false;
			Vector3 position = gameContext.playerEntity.view.gameObject.transform.position;
			position = new Vector3(SettingsService.SettingsSO.PlayerCameraOffset.x, position.y, position.z);
			gameContext.playerEntity.ReplacePosition(position);
			gameContext.playerEntity.ReplaceRaccoonIntro(RaccoonIntroStep.HUDRevealed);
			InformUI();
			if (!gameContext.worldEntity.levelSettings.levelTutorialSettings.GodMode)
			{
				gameContext.playerEntity.isGodMode = false;
			}
			if (raccoonEntity != null)
			{
				raccoonEntity.isGodMode = false;
			}
		}
	}
}
