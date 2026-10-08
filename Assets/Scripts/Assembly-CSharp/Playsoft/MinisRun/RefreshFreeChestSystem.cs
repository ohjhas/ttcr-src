using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshFreeChestSystem : ReactiveSystem<StateEntity>
	{
		public RefreshFreeChestSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.FreeChest.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasFreeChest;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				FreeChestData chestData = new FreeChestData
				{
					IsLocked = entity.freeChest.IsLocked,
					UnlockTime = entity.freeChest.UnlockTime
				};
				IGroup<UIEntity> obj = Contexts.sharedInstance.uI.GetGroup(UIMatcher.FreeChestStateListener);
				UIEntity[] entities2 = obj.GetEntities();
				foreach (UIEntity uIEntity in entities2)
				{
					uIEntity.freeChestStateListener.listener.FreeChestStateChanged(chestData);
				}
			}
		}
	}
}
