using Entitas;

[State]
public sealed class MuteSoundSourceComponent : IComponent
{
	public bool Mute;

	public string SourceId;
}
