using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialLevelUnlockedEnablingSwitch : MonoBehaviour, ITutorialLevelPassedAvailibilityListener
	{
		[Tooltip("When this level unlocked elements will be unlocked")]
		[SerializeField]
		private TutorialOutgameLevel tutorialLevel;

		[SerializeField]
		private GameObject[] elementsToSwitch;

		private void Start()
		{
			Contexts.sharedInstance.uI.CreateEntity().AddTutorialLevelPassedAvailability(this);
		}

		private void SwitchState(bool enable)
		{
			if (elementsToSwitch == null || elementsToSwitch.Length <= 0)
			{
				return;
			}
			GameObject[] array = elementsToSwitch;
			foreach (GameObject gameObject in array)
			{
				if (!(null == gameObject))
				{
					gameObject.SetActive(enable);
				}
			}
		}

		public void LevelUnlocked(List<TutorialOutgameLevel> unlockedLevels)
		{
			SwitchState(unlockedLevels.Contains(tutorialLevel));
		}
	}
}
