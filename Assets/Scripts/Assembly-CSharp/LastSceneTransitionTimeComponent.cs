using System;
using Entitas;

[State]
public sealed class LastSceneTransitionTimeComponent : IComponent
{
	public DateTime TransitionTime;
}
