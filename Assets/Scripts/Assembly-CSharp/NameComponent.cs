using Entitas;
using Entitas.CodeGeneration.Attributes;

[State]
public sealed class NameComponent : IComponent
{
	[EntityIndex]
	public string Name;
}
