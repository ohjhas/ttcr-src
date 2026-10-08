using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class ReplaceUserStateComponent : IComponent
{
	public UserStateData NewState;
}
