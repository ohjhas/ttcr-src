using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataO7SdkSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string sdkBalancingPath = "BalancingData/O7SdkBalancing";

		public BalancingDataO7SdkSystem(Contexts context)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasO7SdkBalancingData)
			{
				AddO7SdkData(balancingDataEntity);
			}
		}

		private void AddO7SdkData(StateEntity balancingEntity)
		{
			balancingEntity.AddO7SdkBalancingData(new O7SdkBalancingData());
			O7SdkBalancing o7SdkBalancing = Resources.Load("BalancingData/O7SdkBalancing") as O7SdkBalancing;
			O7SdkBalancingData[] dataArray = o7SdkBalancing.DataArray;
			foreach (O7SdkBalancingData data in dataArray)
			{
				balancingEntity.o7SdkBalancingData.Data = data;
			}
		}
	}
}
