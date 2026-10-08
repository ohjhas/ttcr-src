using System.Collections.Generic;
using Entitas;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TutorialReplaceTargetEventSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private Button target;

		private Button.ButtonClickedEvent originalClickEvent;

		private TutorialAction[] actionsToReplace;

		public TutorialReplaceTargetEventSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ReplaceTargetEvent.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasTutorialDisableStep)
				{
					if (target != null)
					{
						target.onClick = originalClickEvent;
					}
				}
				else if (stateContext.tutorialEntity.hasTutorialTargetElement)
				{
					target = stateContext.tutorialEntity.tutorialTargetElement.Target.GetComponent<Button>();
					if (target != null)
					{
						actionsToReplace = entity.replaceTargetEvent.EventActions;
						originalClickEvent = target.onClick;
						target.onClick = new Button.ButtonClickedEvent();
						target.onClick.AddListener(ExecuteReplaceActions);
					}
				}
				entity.isDestroyed = true;
			}
		}

		private void ExecuteReplaceActions()
		{
			TutorialAction[] array = actionsToReplace;
			foreach (TutorialAction tutorialAction in array)
			{
				if (!(tutorialAction == null))
				{
					tutorialAction.Execute();
				}
			}
		}
	}
}
