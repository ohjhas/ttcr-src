using Entitas;
using UnityEngine;

public class CompanionFollowSystem : IExecuteSystem, ISystem
{
	private readonly GameContext gameContext;

	private readonly float smoothTime;

	private Vector3 velocity;

	public CompanionFollowSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		smoothTime = SettingsService.SettingsSO.CameraSmoothTimeFollow;
	}

	public void Execute()
	{
		GameEntity companionEntity = gameContext.companionEntity;
		if (companionEntity != null)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (companionEntity.isPlayerAttached && playerEntity.hasView && companionEntity.hasView)
			{
				Vector3 localPosition = playerEntity.view.gameObject.transform.localPosition;
				Transform transform = companionEntity.view.gameObject.transform;
				transform.localPosition = Vector3.SmoothDamp(transform.localPosition, localPosition + companionEntity.offsetPosition.Pos, ref velocity, smoothTime);
			}
		}
	}
}
