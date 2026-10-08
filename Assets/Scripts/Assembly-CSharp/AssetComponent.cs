using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
public sealed class AssetComponent : IComponent
{
	[EntityIndex]
	public string name;
}
