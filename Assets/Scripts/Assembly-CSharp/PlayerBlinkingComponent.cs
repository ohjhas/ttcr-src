using Entitas;

[Game]
public sealed class PlayerBlinkingComponent : IComponent
{
	public float Duration;

	public float TimeLeft;

	public int NumberOfCycles;
}
