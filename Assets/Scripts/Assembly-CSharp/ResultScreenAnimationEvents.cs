using System;
using UnityEngine;

public class ResultScreenAnimationEvents : StateMachineBehaviour
{
	public static Action OnAnimationFinished;

	public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if (OnAnimationFinished != null)
		{
			OnAnimationFinished();
		}
	}
}
