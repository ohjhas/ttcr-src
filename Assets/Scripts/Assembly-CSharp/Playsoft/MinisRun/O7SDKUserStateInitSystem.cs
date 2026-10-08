using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SDKUserStateInitSystem : IInitializeSystem, ITearDownSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKRestoreManager manager;

		public O7SDKUserStateInitSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (stateContext.o7SdkEntity.hasO7SdkUserStateManager)
			{
				manager = stateContext.o7SdkEntity.o7SdkUserStateManager.Manager;
				manager.OnRestoreRequestFailed += OnRestoreRequestFailed;
				manager.OnRestoreRequestSuccessed += OnRestoreRequestSuccessed;
			}
		}

		public void TearDown()
		{
			if (stateContext.o7SdkEntity.hasO7SdkUserStateManager)
			{
				manager = stateContext.o7SdkEntity.o7SdkUserStateManager.Manager;
				manager.OnRestoreRequestFailed -= OnRestoreRequestFailed;
				manager.OnRestoreRequestSuccessed -= OnRestoreRequestSuccessed;
			}
		}

		private void OnRestoreRequestSuccessed(O7SDKRestoreManager.RestoreRequestSuccessedResponse successResponse)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("O7SDKRestoreManager.OnRestoreRequestSuccessed({0}, {1})", successResponse.httpCode, successResponse.responseJson));
			}
			O7SDKRestoreManager.RestoreRequestSuccessedResponseData<UserStateData> responseData = successResponse.GetResponseData<UserStateData>();
			stateContext.o7SdkEntity.ReplaceRemoteUserState(responseData.u, responseData.responseCode);
		}

		private void OnRestoreRequestFailed(O7SDKRestoreManager.RestoreRequestFailedResponse failedResponse)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.LogWarning(string.Format("O7SDKRestoreManager.OnRestoreRequestFailed({0}, {1})", failedResponse.httpCode, failedResponse.error));
			}
		}
	}
}
