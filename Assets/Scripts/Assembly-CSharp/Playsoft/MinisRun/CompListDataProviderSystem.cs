using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CompListDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public CompListDataProviderSystem(Contexts contexts)
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
			return entity.isUICompanionListRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			List<CSCompanionData> newCompDataList = CreateCompDataList();
			foreach (UIEntity entity in entities)
			{
				entity.AddUICompanionListResponse(newCompDataList);
			}
		}

		private List<CSCompanionData> CreateCompDataList()
		{
			List<CSCompanionData> list = new List<CSCompanionData>();
			Dictionary<int, CompanionsStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.companionsStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				list.Add(DataProviderHelper.GetCompData(enumerator.Current.Key));
			}
			enumerator.Dispose();
			return list;
		}
	}
}
