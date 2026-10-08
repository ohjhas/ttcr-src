using Entitas;

[State]
public sealed class PlayerLevelListenerComponent : IComponent
{
	public IPlayerLevelListener listener;
}
