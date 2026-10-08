using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Tutorial steps")]
	public class TutorialSteps : ScriptableObject
	{
		[SerializeField]
		private List<TutorialBaseStep> tutorialSteps;

		public List<TutorialBaseStep> AllTutorialSteps
		{
			get
			{
				return tutorialSteps;
			}
		}
	}
}
