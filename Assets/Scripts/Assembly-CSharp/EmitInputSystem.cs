using Entitas;
using UnityEngine;

public sealed class EmitInputSystem : IExecuteSystem, ICleanupSystem, ISystem
{
	private readonly IGroup<InputEntity> inputs;

	public EmitInputSystem(Contexts contexts)
	{
		inputs = contexts.input.GetGroup(InputMatcher.Input);
	}

	public void Execute()
	{
		InputEntity[] entities = inputs.GetEntities();
		foreach (InputEntity inputEntity in entities)
		{
			if (inputEntity.hasBuffer)
			{
				inputEntity.buffer.Duration -= Time.deltaTime;
				if (inputEntity.buffer.Duration < 0f)
				{
					inputEntity.RemoveBuffer();
				}
			}
		}
	}

	public void Cleanup()
	{
		InputEntity[] entities = inputs.GetEntities();
		foreach (InputEntity inputEntity in entities)
		{
			if (!inputEntity.hasBuffer)
			{
				inputEntity.Destroy();
			}
		}
	}
}
