using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialInitializeSystem : IInitializeSystem, ISaveDataLoadedListener, ISystem
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		private readonly List<TutorialTrigger> tutorialTriggers = new List<TutorialTrigger>();

		public TutorialInitializeSystem(Contexts contexts)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			if (stateContext.isTutorial)
			{
				return;
			}
			uiContext.CreateEntity().AddSaveDataLoadedListener(this);
			List<TutorialBaseStep> allTutorialSteps = SettingsService.TutorialOutgameSteps.AllTutorialSteps;
			Dictionary<string, List<TutorialBaseStep>> dictionary = new Dictionary<string, List<TutorialBaseStep>>();
			foreach (TutorialBaseStep item in allTutorialSteps)
			{
				if (null == item)
				{
					continue;
				}
				AddTriggersToList(item.EnableTrigger, item);
				AddTriggersToList(item.DisableTrigger, item);
				if (!string.IsNullOrEmpty(item.GroupId))
				{
					if (dictionary.ContainsKey(item.GroupId))
					{
						dictionary[item.GroupId].Add(item);
						continue;
					}
					dictionary.Add(item.GroupId, new List<TutorialBaseStep> { item });
				}
			}
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTutorial = true;
			stateEntity.AddTutorialPossibleGroups(dictionary);
			stateEntity.AddTutorialTriggersList(tutorialTriggers);
			stateEntity.AddTutorialStepsQueue(new Queue<TutorialBaseStep>());
		}

		public void OnDataLoaded()
		{
			string value = stateContext.GetEntitiesWithName("TutorialProgress").SingleEntity().stringData.Value;
			Dictionary<string, List<TutorialBaseStep>> stepsGroups = stateContext.tutorialEntity.tutorialPossibleGroups.StepsGroups;
			if (!string.IsNullOrEmpty(value))
			{
				TutorialProgressData tutorialProgressData = JsonUtility.FromJson<TutorialProgressData>(value);
				foreach (string tutorialGroupId in tutorialProgressData.TutorialGroupIds)
				{
					if (!stepsGroups.ContainsKey(tutorialGroupId))
					{
						continue;
					}
					switch (tutorialProgressData.GetGroupProgress(tutorialGroupId))
					{
					case TutorialProgress.Finished:
						foreach (TutorialBaseStep item in stepsGroups[tutorialGroupId])
						{
							if (item.TrackState && item.Repeating == GroupRepeating.IfNotFinished)
							{
								stepsGroups.Remove(tutorialGroupId);
								DisableTriggersForGroup(tutorialGroupId);
								break;
							}
						}
						break;
					case TutorialProgress.Started:
						foreach (TutorialBaseStep item2 in stepsGroups[tutorialGroupId])
						{
							if (item2.TrackState && item2.Repeating == GroupRepeating.IfNotStarted)
							{
								stepsGroups.Remove(tutorialGroupId);
								DisableTriggersForGroup(tutorialGroupId);
								break;
							}
						}
						break;
					}
				}
			}
			stateContext.tutorialEntity.isTutorialInitialized = true;
		}

		private void AddTriggersToList(TutorialTrigger trigger, TutorialBaseStep step)
		{
			if (null == trigger)
			{
				return;
			}
			tutorialTriggers.Add(trigger);
			trigger.Step = step;
			trigger.IsEnabled = true;
			TutorialTriggersGroup tutorialTriggersGroup = trigger as TutorialTriggersGroup;
			if (!(tutorialTriggersGroup != null))
			{
				return;
			}
			foreach (TutorialTrigger trigger2 in tutorialTriggersGroup.Triggers)
			{
				AddTriggersToList(trigger2, step);
			}
		}

		private void DisableTriggersForGroup(string groupId)
		{
			foreach (TutorialTrigger trigger in stateContext.tutorialEntity.tutorialTriggersList.Triggers)
			{
				if (trigger.Step.GroupId == groupId)
				{
					trigger.IsEnabled = false;
				}
			}
		}
	}
}
