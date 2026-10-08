using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public class SetupBackgroundSystem : IInitializeSystem, ISystem
{
	private const float cameraDefaultSize = 6f;

	private readonly GameContext gameContext;

	public SetupBackgroundSystem(Contexts contexts)
	{
		gameContext = contexts.game;
	}

	public void Initialize()
	{
		MissionsBalancingData missionData = gameContext.worldEntity.missionData.MissionData;
		ResourceRequest landBgsRequest = Resources.LoadAsync<LandSkinSettingsSO>("SO/Lands/" + missionData.Background);
		GameEntity gameEntity = gameContext.CreateEntity();
		gameEntity.AddAsyncRequest(landBgsRequest, delegate
		{
			LandSkinSettingsSO landSkinSettingsSO = (LandSkinSettingsSO)landBgsRequest.asset;
			SetupBgLayers(landSkinSettingsSO.BackLayer, landSkinSettingsSO.MiddleLayer, landSkinSettingsSO.FrontLayer);
		});
	}

	private void SetupBgLayers(Sprite mainLayer, Sprite middleLayer, Sprite frontLayer)
	{
		bgParallaxBehaviour componentInChildren = Camera.main.transform.parent.GetComponentInChildren<bgParallaxBehaviour>();
		componentInChildren.SetSprites(mainLayer, middleLayer, frontLayer);
		GameEntity gameEntity = gameContext.CreateEntity();
		gameEntity.AddIngameBg(componentInChildren);
		int camerasize = gameContext.worldEntity.missionData.MissionData.Camerasize;
		Vector2 cameraPos = new Vector2((float)camerasize - 6f, (float)camerasize - 6f);
		gameEntity.ingameBg.Bg.cameraPos = cameraPos;
	}
}
