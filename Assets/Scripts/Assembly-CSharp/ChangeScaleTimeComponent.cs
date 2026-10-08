using Entitas;

[Game]
public sealed class ChangeScaleTimeComponent : IComponent
{
	public float finalTimeScale;

	public float transitionDuration;

	public float slowDownDuration;

	public float timer;
}
