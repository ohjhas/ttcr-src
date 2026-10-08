using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ChestRevealScreenInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public ChestRevealScreenInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.ChestRevealScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIRewardCollected;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.isCloseScreen)
				{
					CloseScreen();
				}
				entity.isDestroyed = true;
			}
		}

		private void CloseScreen()
		{
			BaseDialog dialog = uiContext.GetDialog<ChestRevealScreen>();
			uiContext.CloseDialog(dialog);
			uiContext.RefreshScreen();
		}
	}
}
