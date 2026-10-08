using Entitas;

[Game]
public sealed class TutorialPlayStepComponent : IComponent
{
	public TutorialStep Step;

	public bool StopTime;

	public float StartPosX;

	public float EndPosX;
}
