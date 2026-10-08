using Entitas;
using UnityEngine;

public sealed class EmitKeyoardInputSystem : IExecuteSystem, ISystem
{
	private readonly InputContext inputContext;

	public EmitKeyoardInputSystem(Contexts contexts)
	{
		inputContext = contexts.input;
	}

	public void Execute()
	{
		if (Input.GetKeyDown(KeyCode.D))
		{
			InputEntity inputEntity = inputContext.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isInputJump = true;
			inputEntity.AddBuffer(SettingsService.SettingsSO.JumpBufferDuration);
		}
		if (Input.GetKeyDown(KeyCode.A))
		{
			InputEntity inputEntity2 = inputContext.CreateEntity();
			inputEntity2.isInput = true;
			inputEntity2.isInputSlide = true;
		}
		if (Input.GetKey(KeyCode.A))
		{
			InputEntity inputEntity3 = inputContext.CreateEntity();
			inputEntity3.isInput = true;
			inputEntity3.isInputSlide = true;
		}
		if (Input.GetKeyUp(KeyCode.A))
		{
			InputEntity inputEntity4 = inputContext.CreateEntity();
			inputEntity4.isInput = true;
			inputEntity4.isInputSlideStop = true;
		}
	}
}
