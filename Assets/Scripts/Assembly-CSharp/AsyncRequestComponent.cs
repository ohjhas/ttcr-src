using System;
using Entitas;
using UnityEngine;

[Game]
[UI]
public sealed class AsyncRequestComponent : IComponent
{
	public ResourceRequest Request;

	public Action callback;
}
