using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogAgeGateQuestionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private InitialStartScene initialStartScene;

		public LogAgeGateQuestionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.InitialStartSceneDisplayed.Added(), StateMatcher.O7SdkAgeGateShow.Added());
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
				if (entity.isO7SdkAgeGateShow)
				{
					LogAgeGateQuestion();
				}
			}
		}

		private void LogAgeGateQuestion()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("sharing-question", "app-features", null, null, null, null, null, null, null, false);
		}
	}
}
