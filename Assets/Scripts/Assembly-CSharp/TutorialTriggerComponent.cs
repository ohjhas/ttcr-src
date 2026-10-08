using Entitas;

[Game]
public sealed class TutorialTriggerComponent : IComponent
{
	public TutorialStep Step;

	public bool StopTime;
}
