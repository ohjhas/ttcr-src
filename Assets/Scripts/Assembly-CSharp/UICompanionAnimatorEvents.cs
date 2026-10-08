using System;
using UnityEngine;

public class UICompanionAnimatorEvents : StateMachineBehaviour
{
	public static Action OnStateFinished;

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (OnStateFinished != null)
		{
			OnStateFinished();
		}
	}
}
