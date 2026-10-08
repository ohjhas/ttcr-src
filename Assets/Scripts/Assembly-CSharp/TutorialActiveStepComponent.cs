using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class TutorialActiveStepComponent : IComponent
{
	public TutorialBaseStep Step;
}
