using Entitas;

[Input]
public sealed class CollisionComponent : IComponent
{
	public IEntity from;

	public IEntity to;
}
