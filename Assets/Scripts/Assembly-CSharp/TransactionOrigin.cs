using Entitas;
using UnityEngine;

[State]
public sealed class TransactionOrigin : IComponent
{
	public RectTransform Origin;
}
