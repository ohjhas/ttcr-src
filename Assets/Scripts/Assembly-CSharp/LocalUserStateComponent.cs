using Entitas;
using Entitas.CodeGeneration.Attributes;
using Playsoft.MinisRun;

[State]
[Unique]
public sealed class LocalUserStateComponent : IComponent
{
	public UserStateData StateData;
}
