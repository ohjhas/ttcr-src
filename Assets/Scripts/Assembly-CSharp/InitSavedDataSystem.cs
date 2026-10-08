using System.Collections.Generic;
using Entitas;

public class InitSavedDataSystem : IInitializeSystem, ISystem
{
	private readonly StateContext stateContext;

	public const string coinsKey = "Coins";

	public const string gemsKey = "HardCurrency";

	public const string soundsKey = "Sounds";

	public const string musicKey = "Music";

	public InitSavedDataSystem(Contexts contexts)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		LoadInitData();
	}

	private void LoadInitData()
	{
		stateContext.CreateIntegerData("Coins", GetDefaultValue(EnumSettings.InitCoins), true);
		stateContext.CreateIntegerData("HardCurrency", GetDefaultValue(EnumSettings.InitHardCurrency), true);
		stateContext.CreateBoolData("Sounds", true, true);
		stateContext.CreateBoolData("Music", true, true);
	}

	private int GetDefaultValue(EnumSettings settings)
	{
		Dictionary<EnumSettings, int> map = stateContext.balancingDataEntity.initConfigData.Map;
		return map[settings];
	}
}
