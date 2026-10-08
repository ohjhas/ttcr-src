using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataOverrideConfigSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string CharactersUpgradesOverrideConfig = "CharactersUpgradesOverrideConfig";

		public const string CompanionsUpgradesOverrideConfig = "CompanionsUpgradesOverrideConfig";

		public const string ChestContentOverrideConfig = "ChestContentOverrideConfig";

		public const string DailyMissionsOverrideConfig = "DailyMissionsOverrideConfig";

		public const string ExtConfigurationOverrideConfig = "ExtConfigurationOverrideConfig";

		public BalancingDataOverrideConfigSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasBalancingDataOverrideConfig)
			{
				AddBalancingDataOverride(balancingDataEntity);
				balancingDataEntity.balancingDataOverrideConfig.Data.OverrideBalancingData();
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.OverrideBalancingConfiguration.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				SaveBalancingOverride();
				entity.isDestroyed = true;
			}
		}

		private void AddBalancingDataOverride(StateEntity balancingEntity)
		{
			BalancingDataOverrideConfig newData = LoadBalancingOverride();
			balancingEntity.AddBalancingDataOverrideConfig(newData);
		}

		private BalancingDataOverrideConfig LoadBalancingOverride()
		{
			BalancingDataOverrideConfig balancingDataOverrideConfig = new BalancingDataOverrideConfig();
			if (PlayerPrefs.HasKey("CharactersUpgradesOverrideConfig"))
			{
				string jsonObject = PlayerPrefs.GetString("CharactersUpgradesOverrideConfig");
				balancingDataOverrideConfig.CharactersUpgrades = JsonHelper.FromJsonWrapped<CharactersUpgradesOverrideConfig>(jsonObject);
			}
			if (PlayerPrefs.HasKey("CompanionsUpgradesOverrideConfig"))
			{
				string jsonObject2 = PlayerPrefs.GetString("CompanionsUpgradesOverrideConfig");
				balancingDataOverrideConfig.CompanionsUpgrades = JsonHelper.FromJsonWrapped<CompanionsUpgradesOverrideConfig>(jsonObject2);
			}
			if (PlayerPrefs.HasKey("ChestContentOverrideConfig"))
			{
				string jsonObject3 = PlayerPrefs.GetString("ChestContentOverrideConfig");
				balancingDataOverrideConfig.ChestContent = JsonHelper.FromJsonWrapped<ChestContentOverrideConfig>(jsonObject3);
			}
			if (PlayerPrefs.HasKey("DailyMissionsOverrideConfig"))
			{
				string jsonObject4 = PlayerPrefs.GetString("DailyMissionsOverrideConfig");
				balancingDataOverrideConfig.DailyMissions = JsonHelper.FromJsonWrapped<DailyMissionsOverrideConfig>(jsonObject4);
			}
			if (PlayerPrefs.HasKey("ExtConfigurationOverrideConfig"))
			{
				string json = PlayerPrefs.GetString("ExtConfigurationOverrideConfig");
				balancingDataOverrideConfig.ExtConfig = JsonUtility.FromJson<BalancingDataOverrideConfig.ExtConfigFile>(json);
			}
			return balancingDataOverrideConfig;
		}

		private void SaveBalancingOverride()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			BalancingDataOverrideConfig data = balancingDataEntity.balancingDataOverrideConfig.Data;
			if (data.CharactersUpgrades != null)
			{
				string value = JsonHelper.ToJson(data.CharactersUpgrades);
				PlayerPrefs.SetString("CharactersUpgradesOverrideConfig", value);
			}
			if (data.CompanionsUpgrades != null)
			{
				string value2 = JsonHelper.ToJson(data.CompanionsUpgrades);
				PlayerPrefs.SetString("CompanionsUpgradesOverrideConfig", value2);
			}
			if (data.ChestContent != null)
			{
				string value3 = JsonHelper.ToJson(data.ChestContent);
				PlayerPrefs.SetString("ChestContentOverrideConfig", value3);
			}
			if (data.DailyMissions != null)
			{
				string value4 = JsonHelper.ToJson(data.DailyMissions);
				PlayerPrefs.SetString("DailyMissionsOverrideConfig", value4);
			}
			if (data.ExtConfig != null)
			{
				string value5 = JsonUtility.ToJson(data.ExtConfig);
				PlayerPrefs.SetString("ExtConfigurationOverrideConfig", value5);
			}
			PlayerPrefs.Save();
		}
	}
}
