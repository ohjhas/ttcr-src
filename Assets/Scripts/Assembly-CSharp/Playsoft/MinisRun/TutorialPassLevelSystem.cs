using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialPassLevelSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialPassLevelSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.PassLevelAction.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("TutorialProgress").SingleEntity();
				string value = stateEntity.stringData.Value;
				TutorialProgressData tutorialProgressData = JsonUtility.FromJson<TutorialProgressData>(value) ?? new TutorialProgressData();
				tutorialProgressData.AddPassedTutorialLevel(entity.passLevelAction.TutorialLevel);
				string newValue = JsonUtility.ToJson(tutorialProgressData);
				stateEntity.ReplaceStringData(newValue);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
