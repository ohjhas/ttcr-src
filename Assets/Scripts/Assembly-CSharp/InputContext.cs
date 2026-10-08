using System;
using Entitas;

public sealed class InputContext : Context<InputEntity>
{
	public InputContext()
		: base(21, 0, new ContextInfo("Input", InputComponentsLookup.componentNames, InputComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => new SafeAERC(entity)))
	{
	}
}
