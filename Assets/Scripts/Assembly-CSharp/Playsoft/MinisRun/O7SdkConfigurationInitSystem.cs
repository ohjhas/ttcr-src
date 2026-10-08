using System;
using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SdkConfigurationInitSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKDataManager manager;

		public O7SdkConfigurationInitSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (!stateContext.o7SdkEntity.hasO7SdkDataManager || stateContext.o7SdkEntity.hasO7SdkConfigItems)
			{
				return;
			}
			manager = stateContext.o7SdkEntity.o7SdkDataManager.Manager;
			string cDNItemsConfiguration = manager.GetCDNItemsConfiguration();
			if (string.IsNullOrEmpty(cDNItemsConfiguration))
			{
				stateContext.o7SdkEntity.ReplaceO7SdkConfigItems(new BalancingDataOverrideConfig());
				return;
			}
			try
			{
				BalancingDataOverrideConfig balancingDataOverrideConfig = new BalancingDataOverrideConfig();
				List<BalancingDataOverrideConfig.CDNConfigItem> configItems = JsonHelper.FromJson<BalancingDataOverrideConfig.CDNConfigItem>(cDNItemsConfiguration);
				balancingDataOverrideConfig.ConfigItems = configItems;
				balancingDataOverrideConfig.DownloadData();
			}
			catch (Exception ex)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					if (!string.IsNullOrEmpty(cDNItemsConfiguration))
					{
						Debug.LogWarning("[O7SDK] CDN items=" + cDNItemsConfiguration);
					}
					Debug.LogWarning("[O7SDK] no CDN items changes: " + ex.Message);
				}
			}
		}
	}
}
