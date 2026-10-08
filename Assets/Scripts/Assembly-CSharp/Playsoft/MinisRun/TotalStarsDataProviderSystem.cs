using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TotalStarsDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public TotalStarsDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUITotalStarsRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			int totalStarsAmount = GetTotalStarsAmount();
			foreach (UIEntity entity in entities)
			{
				entity.AddUITotalStarsResponse(totalStarsAmount);
			}
		}

		private int GetTotalStarsAmount()
		{
			int num = 0;
			foreach (KeyValuePair<int, MissionsBalancingData> item in stateContext.balancingDataEntity.missionBalancingData.Map)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("MissionData" + item.Key).SingleEntity();
				num += stateEntity.integer.Amount;
			}
			return num;
		}
	}
}
