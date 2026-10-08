using System;
using Entitas;

[State]
public sealed class FreeChest : IComponent
{
	public bool IsLocked;

	public DateTime UnlockTime;
}
