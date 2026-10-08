using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public sealed class IngameBgComponent : IComponent
{
	public bgParallaxBehaviour Bg;
}
