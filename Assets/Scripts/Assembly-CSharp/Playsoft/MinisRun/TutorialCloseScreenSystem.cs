using System.Collections.Generic;
using DG.Tweening;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialCloseScreenSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		public TutorialCloseScreenSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.CloseScreenAction.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity e in entities)
			{
				if (e.closeScreenAction.delay > 0f)
				{
					DOVirtual.DelayedCall(e.closeScreenAction.delay, delegate
					{
						CloseScreen(e.closeScreenAction.screenId);
					});
				}
				else
				{
					CloseScreen(e.closeScreenAction.screenId);
				}
				e.isDestroyed = true;
			}
		}

		private void CloseScreen(ScreenId screenId)
		{
			uiContext.CloseDialog(screenId);
		}
	}
}
