using Entitas;

[Game]
public sealed class AbilityChargingProgress : IComponent
{
	public float PercentCharged;

	public float TimeLeft;
}
