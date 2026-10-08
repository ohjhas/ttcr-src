using System;
using Entitas;
using Entitas.CodeGeneration.Attributes;

[State]
[Unique]
public sealed class CurrentRealTimeComponent : IComponent
{
	public DateTime CurrentTime;
}
