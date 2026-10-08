using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialSaveProgressSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialSaveProgressSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TutorialEnableStep.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			string value = stateContext.GetEntitiesWithName("TutorialProgress").SingleEntity().stringData.Value;
			TutorialProgressData currentData = JsonUtility.FromJson<TutorialProgressData>(value) ?? new TutorialProgressData();
			foreach (StateEntity entity in entities)
			{
				if (entity.hasTutorialEnableStep)
				{
					StoreProgress(currentData, entity.tutorialEnableStep.Step, TutorialProgress.Started);
				}
				else if (entity.hasTutorialDisableStep)
				{
					StoreProgress(currentData, entity.tutorialDisableStep.Step, TutorialProgress.Finished);
				}
			}
		}

		private void StoreProgress(TutorialProgressData currentData, TutorialBaseStep step, TutorialProgress progress)
		{
			if (step.TrackState)
			{
				currentData.ReplaceGroupProgress(step.GroupId, progress);
				string newValue = JsonUtility.ToJson(currentData);
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isSavedData = true;
				stateEntity.AddName("TutorialProgressSend");
				stateEntity.AddStringData(newValue);
			}
		}
	}
}
