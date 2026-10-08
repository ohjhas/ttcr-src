using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharListDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public CharListDataProviderSystem(Contexts contexts)
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
			return entity.isUICharListRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			List<CSCharacterData> newCharDataList = CreateCharDataList();
			foreach (UIEntity entity in entities)
			{
				entity.AddUICharListResponse(newCharDataList);
			}
		}

		private List<CSCharacterData> CreateCharDataList()
		{
			List<CSCharacterData> list = new List<CSCharacterData>();
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				list.Add(DataProviderHelper.GetCharData(enumerator.Current.Key));
			}
			enumerator.Dispose();
			return list;
		}
	}
}
