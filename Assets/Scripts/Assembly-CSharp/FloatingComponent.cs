using Entitas;

[Game]
public sealed class FloatingComponent : IComponent
{
	public float Amplitude;

	public float Speed;

	public float Offset;

	public bool HasLocalTimer;

	public float CurrentTime;
}
