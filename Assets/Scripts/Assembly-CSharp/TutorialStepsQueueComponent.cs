using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class TutorialStepsQueueComponent : IComponent
{
	public Queue<TutorialBaseStep> stepsQueue;
}
