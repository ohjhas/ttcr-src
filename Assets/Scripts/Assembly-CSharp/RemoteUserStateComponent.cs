using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class RemoteUserStateComponent : IComponent
{
	public UserStateData State;

	public int ResponseCode;
}
