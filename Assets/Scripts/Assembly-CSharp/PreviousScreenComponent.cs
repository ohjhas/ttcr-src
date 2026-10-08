using Entitas;
using Entitas.CodeGeneration.Attributes;

[State]
[Unique]
public sealed class PreviousScreenComponent : IComponent
{
	public ScreenId ScreenId;
}
