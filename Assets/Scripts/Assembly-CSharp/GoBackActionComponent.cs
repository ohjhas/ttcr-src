using Entitas;

[State]
public sealed class GoBackActionComponent : IComponent
{
	public ScreenId ExpectedScreenId;

	public float Delay;
}
