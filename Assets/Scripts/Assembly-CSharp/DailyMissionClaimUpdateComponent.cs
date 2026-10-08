using Entitas;

[State]
public sealed class DailyMissionClaimUpdateComponent : IComponent
{
	public DailyMissionGroup Group;

	public bool IsClaimed;
}
