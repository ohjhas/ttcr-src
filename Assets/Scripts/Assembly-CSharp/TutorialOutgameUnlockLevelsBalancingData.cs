using System;
using UnityEngine;

[Serializable]
public class TutorialOutgameUnlockLevelsBalancingData
{
	[SerializeField]
	private TutorialOutgameLevel tutorialoutgamelevel;

	[SerializeField]
	private int passedleveltrigger;

	public TutorialOutgameLevel TUTORIALOUTGAMELEVEL
	{
		get
		{
			return tutorialoutgamelevel;
		}
		set
		{
			tutorialoutgamelevel = value;
		}
	}

	public int Passedleveltrigger
	{
		get
		{
			return passedleveltrigger;
		}
		set
		{
			passedleveltrigger = value;
		}
	}
}
