using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UIDestroySystem : ReactiveSystem<UIEntity>, ITearDownSystem, ISystem
	{
		private readonly UIContext uiContext;

		public UIDestroySystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Destroyed.Added());
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.isEnabled)
				{
					entity.Destroy();
				}
			}
		}

		public void TearDown()
		{
			UIEntity[] entities = uiContext.GetEntities();
			UIEntity[] array = entities;
			foreach (UIEntity uIEntity in array)
			{
				if (uIEntity.isEnabled)
				{
					uIEntity.Destroy();
				}
			}
		}
	}
}
