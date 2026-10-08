using DG.Tweening;
using Entitas;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class RaccoonFirstIntroSystem : IInitializeSystem, IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly IGroup<GameEntity> listeners;

		private readonly SettingsRacoonAnimSO settingsRaccoonAnimSO;

		private GameEntity raccoonEntity;

		private GameEntity candyStoreEntity;

		private bool isRaccoonTime;

		private bool isCharacterTime;

		private bool isRaccoonLookBackTime;

		private bool isRaccoonSpeedUpTime;

		private bool isIntroStarted;

		private bool hasCandyStorePartsOrderChanged;

		private bool hasRaccoonJumped;

		private bool hasRaccoonLookedBack;

		public RaccoonFirstIntroSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			listeners = contexts.game.GetGroup(GameMatcher.RaccoonIntrosListener);
			settingsRaccoonAnimSO = SettingsService.GetSetting<SettingsRacoonAnimSO>();
		}

		private bool CanExecute()
		{
			return GameIntroHelper.IsIntroTime() && gameContext.worldEntity.isGameStarted && raccoonEntity != null && raccoonEntity.hasView && gameContext.playerEntity != null && gameContext.playerEntity.hasView;
		}

		public void Initialize()
		{
			if (GameIntroHelper.IsIntroTime())
			{
				CreateCandyStoreEntity();
				CreateRaccoonEntity();
				UpdatePlayerEntity();
				InformUI();
			}
		}

		private void CreateCandyStoreEntity()
		{
			candyStoreEntity = gameContext.CreateEntity();
			candyStoreEntity.isCandyStore = true;
			candyStoreEntity.isCameraAttached = true;
			Vector2 posBasingOnAspectRatio = GetPosBasingOnAspectRatio(settingsRaccoonAnimSO.RaccoonFirstIntroCandyStoreInitialPos_Tablet, settingsRaccoonAnimSO.RaccoonFirstIntroCandyStoreInitialPos_IPhoneX);
			candyStoreEntity.AddPosition(posBasingOnAspectRatio);
			candyStoreEntity.AddAsset("LevelElements/CandyStore");
		}

		private void CreateRaccoonEntity()
		{
			raccoonEntity = gameContext.CreateEntity();
			raccoonEntity.isRaccoon = true;
			raccoonEntity.AddAsset("Characters/Ingame/Raccoon");
			raccoonEntity.isLoadAssetAsync = true;
			Vector2 posBasingOnAspectRatio = GetPosBasingOnAspectRatio(settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonInitialPos_Tablet, settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonInitialPos_IPhoneX);
			raccoonEntity.AddPosition(posBasingOnAspectRatio);
			raccoonEntity.isGodMode = true;
		}

		private void UpdatePlayerEntity()
		{
			gameContext.playerEntity.AddRaccoonIntro(RaccoonIntroStep.HUDHidden);
			Vector2 posBasingOnAspectRatio = GetPosBasingOnAspectRatio(settingsRaccoonAnimSO.RaccoonFirstIntroCharacterInitialPos_Tablet, settingsRaccoonAnimSO.RaccoonFirstIntroCharacterInitialPos_IPhoneX);
			gameContext.playerEntity.ReplacePosition(posBasingOnAspectRatio);
			gameContext.playerEntity.isGodMode = true;
		}

		private Vector2 GetPosBasingOnAspectRatio(Vector2 tabletPos, Vector2 iPhoneXPos)
		{
			float num = 1.3333334f;
			float num2 = 2.1653333f;
			float num3 = (float)Screen.width / (float)Screen.height;
			float t = (num3 - num) / (num2 - num);
			return Vector2.Lerp(tabletPos, iPhoneXPos, t);
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
			DOTween.Sequence().InsertCallback(settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonRunInDelay, delegate
			{
				isRaccoonTime = true;
				PlayRacconSound();
			}).InsertCallback(settingsRaccoonAnimSO.RaccoonFirstIntroCharacterRunInDelay, delegate
			{
				isCharacterTime = true;
			})
				.InsertCallback(settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonLookBackDelay, delegate
				{
					isRaccoonLookBackTime = true;
				})
				.InsertCallback(settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonSpeedUpDelay, delegate
				{
					isRaccoonSpeedUpTime = true;
				});
		}

		public void Execute()
		{
			if (CanExecute() && gameContext.playerEntity != null && gameContext.playerEntity.hasRaccoonIntro)
			{
				if (!isIntroStarted)
				{
					RunSequence();
					gameContext.playerEntity.isAngryRun = true;
					isIntroStarted = true;
				}
				MoveRaccoonIfNeeded();
				MakeRaccoonJumpIfNeeded();
				MoveCharacterIfNeeded();
				MakeRaccoonLookBackIfNeeded();
				ChangeOrderOfCandyStorePartsIfNeeded();
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

		private void MakeRaccoonJumpIfNeeded()
		{
			if (isRaccoonTime && !hasRaccoonJumped)
			{
				raccoonEntity.view.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 900f));
				raccoonEntity.view.gameObject.GetComponent<Animator>().SetTrigger("Jump");
				hasRaccoonJumped = true;
			}
		}

		private void MakeRaccoonLookBackIfNeeded()
		{
			if (isRaccoonTime && !hasRaccoonLookedBack && isRaccoonLookBackTime)
			{
				raccoonEntity.view.gameObject.GetComponent<Animator>().SetTrigger("LookBack");
				hasRaccoonLookedBack = true;
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

		private void ChangeOrderOfCandyStorePartsIfNeeded()
		{
			if (isCharacterTime && !hasCandyStorePartsOrderChanged)
			{
				candyStoreEntity.view.gameObject.GetComponent<CandyStoreBehaviour>().SetOrderOfParts(new bool[3] { true, false, false });
				hasCandyStorePartsOrderChanged = true;
			}
		}

		private void PlayRacconSound()
		{
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("racoon_happy_jump");
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("racoon_run_begin");
		}

		private void MoveRaccoon()
		{
			float num = ((!isRaccoonSpeedUpTime) ? settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonSpeedModifierSlow : settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonSpeedModifierFast);
			float num2 = gameContext.worldEntity.world.Speed * Time.smoothDeltaTime * num;
			raccoonEntity.view.gameObject.transform.position += Vector3.right * num2;
		}

		private void MoveCharacter()
		{
			float num = gameContext.worldEntity.world.Speed * Time.smoothDeltaTime;
			gameContext.playerEntity.view.gameObject.transform.position += Vector3.right * num;
		}

		private bool HasRaccoonReachedFinishPosition()
		{
			return raccoonEntity.view.gameObject.transform.position.x >= settingsRaccoonAnimSO.RaccoonFirstIntroRaccoonCameraOffsetFinal.x;
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
			raccoonEntity.isGodMode = false;
		}
	}
}
