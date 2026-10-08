using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SdkConfigurationUpdateSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public O7SdkConfigurationUpdateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkConfigItems.Added(), StateMatcher.BalancingDataOverrideConfig.Added(), StateMatcher.O7SdkExtConfig.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return stateContext.balancingDataEntity != null && stateContext.balancingDataEntity.hasBalancingDataOverrideConfig;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			bool flag = false;
			if (stateContext.o7SdkEntity != null && stateContext.o7SdkEntity.hasO7SdkConfigItems)
			{
				flag = true;
				stateContext.balancingDataEntity.balancingDataOverrideConfig.Data.CombineConfigurations(stateContext.o7SdkEntity.o7SdkConfigItems.Data);
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("CDN Items: Configurations combined");
				}
			}
			if (stateContext.o7SdkEntity != null && stateContext.o7SdkEntity.hasO7SdkExtConfig)
			{
				flag = true;
				stateContext.balancingDataEntity.balancingDataOverrideConfig.Data.CombineConfigurations(stateContext.o7SdkEntity.o7SdkExtConfig.Data);
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("EXT: Configurations combined");
				}
			}
			if (flag)
			{
				stateContext.balancingDataEntity.balancingDataOverrideConfig.Data.OverrideBalancingData();
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("Balancing Data Override");
				}
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isOverrideBalancingConfiguration = true;
			}
		}
	}
}
