using Entitas;

[State]
public sealed class CharacterPartsPriceComponent : IComponent
{
	public int CharacterId;

	public int Amount;
}
