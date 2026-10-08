using Entitas;

[State]
public sealed class LevelCompletedComponent : IComponent
{
	public int LevelNumber;

	public int Stars;

	public int Result;

	public int NewStars;
}
