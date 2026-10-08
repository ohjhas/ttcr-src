using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class BoughtChestComponent : IComponent
{
	public ChestData Chest;

	public ChestType Type;
}
