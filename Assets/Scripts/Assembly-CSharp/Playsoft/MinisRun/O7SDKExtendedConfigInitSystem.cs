using System;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SDKExtendedConfigInitSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKDataManager manager;

		public O7SDKExtendedConfigInitSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (!stateContext.o7SdkEntity.hasO7SdkDataManager || stateContext.o7SdkEntity.hasO7SdkExtConfig)
			{
				return;
			}
			manager = stateContext.o7SdkEntity.o7SdkDataManager.Manager;
			string appConfiguration = manager.GetAppConfiguration();
			if (string.IsNullOrEmpty(appConfiguration))
			{
				stateContext.o7SdkEntity.ReplaceO7SdkExtConfig(new BalancingDataOverrideConfig());
				return;
			}
			try
			{
				BalancingDataOverrideConfig balancingDataOverrideConfig = new BalancingDataOverrideConfig();
				BalancingDataOverrideConfig.ExtConfigFile extConfig = JsonUtility.FromJson<BalancingDataOverrideConfig.ExtConfigFile>(appConfiguration);
				balancingDataOverrideConfig.ExtConfig = extConfig;
				stateContext.o7SdkEntity.ReplaceO7SdkExtConfig(balancingDataOverrideConfig);
			}
			catch (Exception ex)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					if (!string.IsNullOrEmpty(appConfiguration))
					{
						Debug.LogWarning("[O7SDK] ext config=" + appConfiguration);
					}
					Debug.LogWarning("[O7SDK] no ext configuration changes: " + ex.Message);
				}
			}
		}
	}
}
