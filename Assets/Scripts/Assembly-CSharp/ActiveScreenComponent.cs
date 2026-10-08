using Entitas;
using Entitas.CodeGeneration.Attributes;

[State]
[Unique]
public sealed class ActiveScreenComponent : IComponent
{
	public ScreenId ScreenId;
}
