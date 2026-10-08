using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ChestRewardDataProvider : ReactiveSystem<UIEntity>
	{
		public ChestRewardDataProvider(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIChestRewardRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				entity.AddUIChestRewardResponse(entity.uIChestRewardRequest.Chest, entity.uIChestRewardRequest.Rewards);
			}
		}
	}
}
