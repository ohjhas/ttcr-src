using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

public class CompanionInitSystem : IInitializeSystem, ISystem
{
	private readonly GameContext gameContext;

	private readonly StateContext stateContext;

	public CompanionInitSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		int compIndex = GetCompIndex();
		bool flag = IsCompanionUnlocked(compIndex);
		bool companionEnabled = gameContext.worldEntity.levelSettings.levelTutorialSettings.CompanionEnabled;
		if (flag && companionEnabled)
		{
			CreateCompanion(compIndex);
		}
	}

	private int GetCompIndex()
	{
		HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("CompSelectData");
		int result = -1;
		if (entitiesWithName.Count > 0)
		{
			StateEntity stateEntity = entitiesWithName.SingleEntity();
			result = stateEntity.integer.Amount;
		}
		return result;
	}

	private bool IsCompanionUnlocked(int compIndex)
	{
		CSCompanionData compData = DataProviderHelper.GetCompData(compIndex);
		return compData != null && compData.Unlocked;
	}

	private void CreateCompanion(int compIndex)
	{
		GameEntity gameEntity = gameContext.CreateEntity();
		gameEntity.isCompanion = true;
		gameEntity.AddAsset("Companions/Ingame/" + SettingsService.SettingsSO.CompanionSettings[compIndex].IngameAssetName);
		gameEntity.isLoadAssetAsync = true;
		gameEntity.AddOffsetPosition(SettingsService.SettingsSO.AnchorToPlayer);
		gameEntity.AddPosition(gameContext.playerEntity.position.Value + SettingsService.SettingsSO.AnchorToPlayer);
		gameEntity.isCameraAttached = true;
		gameEntity.isPlayerAttached = true;
		gameEntity.AddSetupAbility(compIndex);
	}
}
