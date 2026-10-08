using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

namespace Playsoft.MinisRun
{
	public class InitialStartSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		private InitialStartScene initialStartScene;

		public InitialStartSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.InitialStartSceneDisplayed.Added(), StateMatcher.O7SdkAgeGateShow.Added(), StateMatcher.O7SdkGenderGateShow.Added(), StateMatcher.UserStateShow.Added(), StateMatcher.O7SdkGateDone.Added(), StateMatcher.UserStateTryStartNewGame.Added(), StateMatcher.O7SdkGdprShowAdProvidersSelection.Added(), StateMatcher.O7SdkGdprShowConsentTool.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			if (!GameIntroHelper.IsIntroTime())
			{
				return;
			}
			foreach (StateEntity entity in entities)
			{
				if (entity.hasInitialStartSceneDisplayed)
				{
					initialStartScene = entity.initialStartSceneDisplayed.initialStartScene;
					stateContext.CreateEntity().isO7SdkAgeGateDecider = true;
					initialStartScene.IntroController.OnIntroDone += UnloadInitialScene;
				}
				if (initialStartScene == null)
				{
					break;
				}
				if (entity.isO7SdkAgeGateShow)
				{
					initialStartScene.DisplayAgeGate();
					entity.isDestroyed = true;
				}
				if (entity.isO7SdkGenderGateShow)
				{
					initialStartScene.DisplayGenderGate();
				}
				if (entity.isUserStateShow)
				{
					StateEntity o7SdkEntity = stateContext.o7SdkEntity;
					if (stateContext.o7SdkEntity.hasRemoteUserState && stateContext.localUserState != null && (o7SdkEntity.remoteUserState.ResponseCode == 409 || o7SdkEntity.remoteUserState.ResponseCode == 406))
					{
						initialStartScene.DisplayRestoreScreen();
					}
					else
					{
						stateContext.CreateEntity().AddO7SdkGateDone(true);
					}
				}
				if (entity.isUserStateTryStartNewGame)
				{
					initialStartScene.DisplayRestoreConfirmPopup();
					entity.isDestroyed = true;
				}
				if (entity.hasO7SdkGateDone)
				{
					if (entity.o7SdkGateDone.newGameStarted)
					{
						initialStartScene.DisplayIntro();
					}
					else
					{
						uiContext.CreateEntity().AddChangeScene(ProjectConstants.HOME_SCENE_NAME, false);
					}
				}
				if (entity.isO7SdkGdprShowConsentTool)
				{
					initialStartScene.DisplayGdprSelectionScreen();
				}
				if (entity.isO7SdkGdprShowAdProvidersSelection)
				{
					initialStartScene.DisplayGdprProviderSelectionScreen();
				}
			}
		}

		private void UnloadInitialScene()
		{
			SceneManager.UnloadSceneAsync(ProjectConstants.INITIAL_START_SCENE_NAME);
		}
	}
}
