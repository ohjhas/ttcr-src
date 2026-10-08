using Entitas;
using O7SDK;

[State]
public sealed class O7SdkGenderGateSelectedComponent : IComponent
{
	public O7SDKGateManager.Gender gender;
}
