using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingPrivacyAndLegalSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string BALANCING_PATH = "BalancingData/PrivacyAndLegalBalancing";

		public BalancingPrivacyAndLegalSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasPrivacyAndLegacyBalancingData)
			{
				AddPrivacyAndLegalBalancing(balancingDataEntity);
			}
		}

		private void AddPrivacyAndLegalBalancing(StateEntity balancingEntity)
		{
			PrivacyAndLegalBalancing privacyAndLegalBalancing = Resources.Load("BalancingData/PrivacyAndLegalBalancing") as PrivacyAndLegalBalancing;
			if (null == privacyAndLegalBalancing)
			{
				Debug.LogError("Missing Privacy and Legal balancing data");
			}
			else
			{
				balancingEntity.AddPrivacyAndLegacyBalancingData(privacyAndLegalBalancing.DataArray[0]);
			}
		}
	}
}
