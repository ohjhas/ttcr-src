using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshUDIDDisplaySystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		private IGroup<UIEntity> udidListeners;

		public RefreshUDIDDisplaySystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
			udidListeners = uiContext.GetGroup(UIMatcher.DisplayUDIDListener);
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.DisplayUDID);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
			UIEntity[] entities2 = udidListeners.GetEntities();
			foreach (UIEntity uIEntity in entities2)
			{
				uIEntity.displayUDIDListener.listener.Toggle();
			}
		}
	}
}
