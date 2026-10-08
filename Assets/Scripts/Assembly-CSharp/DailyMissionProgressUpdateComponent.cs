using Entitas;

[State]
public sealed class DailyMissionProgressUpdateComponent : IComponent
{
	public DailyMissionGroup Group;

	public int MissionProgress;
}
