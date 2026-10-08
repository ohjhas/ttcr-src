using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class FinishSetupResponseSystem : ReactiveSystem<UIEntity>
	{
		public FinishSetupResponseSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasSetup;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.setup.dialog != null)
				{
					entity.setup.dialog.SetData(entity);
				}
				entity.isDestroyed = true;
			}
		}
	}
}
