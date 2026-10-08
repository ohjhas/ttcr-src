using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogGenderGateQuestionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private InitialStartScene initialStartScene;

		public LogGenderGateQuestionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.InitialStartSceneDisplayed.Added(), StateMatcher.O7SdkGenderGateShow.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasInitialStartSceneDisplayed)
				{
					initialStartScene = entity.initialStartSceneDisplayed.initialStartScene;
				}
				if (initialStartScene == null)
				{
					break;
				}
				if (entity.isO7SdkGenderGateShow)
				{
					LogGenderGateQuestion();
				}
			}
		}

		private void LogGenderGateQuestion()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("gender-question", "app-features", null, null, null, null, null, null, null, false);
		}
	}
}
