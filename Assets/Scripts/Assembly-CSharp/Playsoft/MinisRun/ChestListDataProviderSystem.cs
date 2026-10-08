using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ChestListDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public ChestListDataProviderSystem(Contexts contexts)
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
			return entity.isUIChestListRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			List<ChestData> newChestDataList = CreateChestDataList();
			foreach (UIEntity entity in entities)
			{
				entity.AddUIChestListResponse(newChestDataList);
			}
		}

		private List<ChestData> CreateChestDataList()
		{
			List<ChestData> list = new List<ChestData>();
			Dictionary<int, ChestData>.Enumerator enumerator = stateContext.balancingDataEntity.gatchaBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				list.Add(enumerator.Current.Value);
			}
			return list;
		}
	}
}
