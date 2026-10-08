using System;
using Entitas;

[State]
public sealed class TransactionCallback : IComponent
{
	public Action Callback;
}
