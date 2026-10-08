using Entitas;

[State]
public sealed class CurrentTimeListenerComponent : IComponent
{
	public ICurrentTimeListener listener;
}
