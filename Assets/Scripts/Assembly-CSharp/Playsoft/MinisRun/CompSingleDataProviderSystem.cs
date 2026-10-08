using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CompSingleDataProviderSystem : ReactiveSystem<UIEntity>
	{
		public CompSingleDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUICompSingleRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				CSCompanionData compData = DataProviderHelper.GetCompData(entity.uICompSingleRequest.CompId);
				entity.AddUICompSingleResponse(compData);
			}
		}
	}
}
