using Entitas;

[State]
public sealed class PlayerProgressionUpdatedComponent : IComponent
{
	public int CurrentLevel;

	public int CurrentExperienceAmount;
}
