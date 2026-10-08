using Entitas;

[State]
public sealed class UserStateSolveConflictComponent : IComponent
{
	public bool KeepLocalData;
}
