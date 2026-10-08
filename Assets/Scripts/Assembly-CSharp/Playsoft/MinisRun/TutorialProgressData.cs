using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class TutorialProgressData
	{
		[SerializeField]
		private List<string> tGIs = new List<string>();

		[SerializeField]
		private List<TutorialProgress> tP = new List<TutorialProgress>();

		[SerializeField]
		private List<TutorialOutgameLevel> pTLs = new List<TutorialOutgameLevel>();

		public List<string> TutorialGroupIds
		{
			get
			{
				return tGIs;
			}
			set
			{
				tGIs = value;
			}
		}

		public List<TutorialProgress> TutorialProgress
		{
			get
			{
				return tP;
			}
			set
			{
				tP = value;
			}
		}

		public List<TutorialOutgameLevel> PassedTutorialLevels
		{
			get
			{
				return pTLs;
			}
			set
			{
				pTLs = value;
			}
		}

		public void AddGroupProgress(string groupId, TutorialProgress progress)
		{
			if (TutorialGroupIds.Contains(groupId))
			{
				ReplaceGroupProgress(groupId, progress);
			}
			TutorialGroupIds.Add(groupId);
			TutorialProgress.Add(progress);
		}

		public void RemoveGroupProgress(string groupId, TutorialProgress progress)
		{
			int num = TutorialGroupIds.IndexOf(groupId);
			if (num != -1)
			{
				TutorialGroupIds.RemoveAt(num);
				TutorialProgress.RemoveAt(num);
			}
		}

		public void ReplaceGroupProgress(string groupId, TutorialProgress progress)
		{
			if (!TutorialGroupIds.Contains(groupId))
			{
				AddGroupProgress(groupId, progress);
				return;
			}
			int index = TutorialGroupIds.IndexOf(groupId);
			TutorialProgress[index] = progress;
		}

		public TutorialProgress GetGroupProgress(string groupId)
		{
			int num = TutorialGroupIds.IndexOf(groupId);
			if (num == -1)
			{
				return Playsoft.MinisRun.TutorialProgress.Undefined;
			}
			return TutorialProgress[num];
		}

		public void AddPassedTutorialLevel(TutorialOutgameLevel tutorialOutgameLevel)
		{
			if (!PassedTutorialLevels.Contains(tutorialOutgameLevel))
			{
				PassedTutorialLevels.Add(tutorialOutgameLevel);
			}
		}

		public bool IsTutorialLevelPassed(TutorialOutgameLevel tutorialOutgameLevel)
		{
			return PassedTutorialLevels.Contains(tutorialOutgameLevel);
		}
	}
}
