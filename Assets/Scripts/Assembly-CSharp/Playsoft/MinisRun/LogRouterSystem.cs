using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class LogRouterSystem : IInitializeSystem, ITearDownSystem, ISystem
	{
		private readonly StateContext stateContext;

		public LogRouterSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			Application.logMessageReceived += OnLog;
		}

		public void TearDown()
		{
			Application.logMessageReceived -= OnLog;
		}

		private void OnLog(string condition, string stackTrace, LogType type)
		{
			switch (type)
			{
			case LogType.Assert:
				LogAssertion(condition, stackTrace);
				break;
			case LogType.Error:
				LogError(condition, stackTrace);
				break;
			case LogType.Exception:
				LogException(condition, stackTrace);
				break;
			case LogType.Warning:
				LogWarning(condition, stackTrace);
				break;
			case LogType.Log:
				LogMessage(condition, stackTrace);
				break;
			}
		}

		private void LogException(string condition, string stackTrace)
		{
			if (stateContext.o7SdkEntity != null && stateContext.o7SdkEntity.hasO7SdkUtilityManager && stateContext.o7SdkEntity.o7SdkUtilityManager.Manager != null)
			{
				O7SDKUtilityManager manager = stateContext.o7SdkEntity.o7SdkUtilityManager.Manager;
				manager.SaveAndSendStackTrace(condition, stackTrace);
			}
		}

		private void LogAssertion(string condition, string stackTrace)
		{
		}

		private void LogError(string condition, string stackTrace)
		{
		}

		private void LogWarning(string condition, string stackTrace)
		{
		}

		private void LogMessage(string condition, string stackTrace)
		{
		}
	}
}
