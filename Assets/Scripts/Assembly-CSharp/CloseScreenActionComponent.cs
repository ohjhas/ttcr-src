using Entitas;

[State]
public sealed class CloseScreenActionComponent : IComponent
{
	public ScreenId screenId;

	public float delay;
}
