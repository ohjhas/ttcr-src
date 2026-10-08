using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

public class PlayerInitSystem : IInitializeSystem, ISystem
{
	private readonly GameContext gameContext;

	private readonly StateContext stateContext;

	public PlayerInitSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		SettingsSO settingsSO = SettingsService.SettingsSO;
		HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("FirstCharSelectData");
		int num = 0;
		if (entitiesWithName.Count > 0)
		{
			StateEntity stateEntity = entitiesWithName.SingleEntity();
			num = stateEntity.integer.Amount;
		}
		GameEntity gameEntity = gameContext.CreateEntity();
		gameEntity.isPlayer = true;
		gameEntity.AddAsset("Characters/Ingame/" + settingsSO.CharSettings[num].IngameAssetName);
		gameEntity.isLoadAssetAsync = true;
		gameEntity.AddPosition(settingsSO.PlayerCameraOffset);
		gameEntity.AddSetupAbility(num);
		entitiesWithName = stateContext.GetEntitiesWithName("CharUpgradeData" + num);
		float newValue = settingsSO.StartingStamina;
		if (entitiesWithName.Count > 0)
		{
			CSCharacterData charData = DataProviderHelper.GetCharData(num);
			string name = "CharUpgradeData" + num;
			StateEntity upgradeData = stateContext.GetEntitiesWithName(name).SingleEntity();
			newValue = (settingsSO.StartingStamina = charData.Upgrades.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount).Stamina);
		}
		gameEntity.AddStamina(newValue);
		gameEntity.isCameraAttached = true;
	}
}
