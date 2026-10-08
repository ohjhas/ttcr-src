using System.Collections.Generic;
using DG.Tweening;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialGoBackSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		public TutorialGoBackSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.GoBackAction.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.goBackAction.ExpectedScreenId == ScreenId.Undefined || IsCurrentScreenExpected(entity.goBackAction.ExpectedScreenId))
				{
					if (entity.goBackAction.Delay > 0f)
					{
						DOVirtual.DelayedCall(entity.goBackAction.Delay, GoBack);
					}
					else
					{
						GoBack();
					}
				}
				entity.isDestroyed = true;
			}
		}

		private void GoBack()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isGoBack = true;
		}

		private bool IsCurrentScreenExpected(ScreenId id)
		{
			BaseDialog baseDialog = uiContext.FindCurrentScreen();
			return baseDialog != null && baseDialog.Id == id;
		}
	}
}
