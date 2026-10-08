using System;
using Entitas;

[State]
public sealed class UserStateLastUpdateTimeComponent : IComponent
{
	public DateTime Time;
}
