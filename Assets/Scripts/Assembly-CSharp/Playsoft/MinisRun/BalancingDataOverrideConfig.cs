using System;
using System.Collections;
using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataOverrideConfig
	{
		[Serializable]
		public class CDNConfigItem
		{
			[SerializeField]
			private string t;

			[SerializeField]
			private string u;

			[SerializeField]
			private List<string> iIs;

			[SerializeField]
			private List<string> uIs;

			public string Key
			{
				get
				{
					return t;
				}
				set
				{
					t = value;
				}
			}

			public string Value
			{
				get
				{
					return u;
				}
				set
				{
					u = value;
				}
			}
		}

		[Serializable]
		public class ExtConfigFile
		{
			[SerializeField]
			private int uSSR;

			[SerializeField]
			private ExtConfigurationData gE;

			public int USSR_IGNORED_SO_FAR
			{
				get
				{
					return uSSR;
				}
				set
				{
					uSSR = value;
				}
			}

			public ExtConfigurationData GridExtension
			{
				get
				{
					return gE;
				}
				set
				{
					gE = value;
				}
			}
		}

		public List<CDNConfigItem> ConfigItems = new List<CDNConfigItem>();

		public ExtConfigFile ExtConfig;

		public List<CharactersUpgradesOverrideConfig> CharactersUpgrades;

		public List<CompanionsUpgradesOverrideConfig> CompanionsUpgrades;

		public List<ChestContentOverrideConfig> ChestContent;

		public List<DailyMissionsOverrideConfig> DailyMissions;

		public void OverrideBalancingData()
		{
			StateEntity balancingDataEntity = Contexts.sharedInstance.state.balancingDataEntity;
			UpdateCharactersUpgradeBalancingData(balancingDataEntity);
			UpdateCompanionsUpgradeBalancingData(balancingDataEntity);
			UpdateChestsContentBalancingData(balancingDataEntity);
			UpdateDailyMissionsBalancingData(balancingDataEntity);
			UpdateInitConfigBalancingData(balancingDataEntity);
			UpdateStaminaSettingsBalancingData(balancingDataEntity);
			UpdateRestoreSettingsBalancingData(balancingDataEntity);
		}

		public void CombineConfigurations(BalancingDataOverrideConfig newConfiguration)
		{
			if (newConfiguration.CharactersUpgrades != null)
			{
				CharactersUpgrades = newConfiguration.CharactersUpgrades;
			}
			if (newConfiguration.CompanionsUpgrades != null)
			{
				CompanionsUpgrades = newConfiguration.CompanionsUpgrades;
			}
			if (newConfiguration.ChestContent != null)
			{
				ChestContent = newConfiguration.ChestContent;
			}
			if (newConfiguration.DailyMissions != null)
			{
				DailyMissions = newConfiguration.DailyMissions;
			}
			if (newConfiguration.ExtConfig != null)
			{
				ExtConfig = newConfiguration.ExtConfig;
			}
		}

		public void DownloadData()
		{
			Coroutiner.StartAsyncOperation(DownloadDataFromCdnItems);
		}

		private IEnumerator DownloadDataFromCdnItems()
		{
			for (int i = 0; i < ConfigItems.Count; i++)
			{
				WWW wwwrequest = new WWW(ConfigItems[i].Value);
				yield return wwwrequest;
				if (string.IsNullOrEmpty(wwwrequest.error))
				{
					string text = wwwrequest.text;
					GetDataFromJson(text, ConfigItems[i].Key);
					if (O7SDKBase.O7SDK_LOG)
					{
						Debug.Log("CDN item key=" + ConfigItems[i].Key + " json=" + text);
					}
				}
				else if (O7SDKBase.O7SDK_LOG)
				{
					Debug.Log("CDN item download error: " + wwwrequest.error);
				}
			}
			Contexts.sharedInstance.state.o7SdkEntity.ReplaceO7SdkConfigItems(this);
		}

		private void GetDataFromJson(string json, string key)
		{
			try
			{
				if (key.Equals("CANDY_RUN_CHARACTER_UPGRADES"))
				{
					CharactersUpgrades = JsonHelper.FromJson<CharactersUpgradesOverrideConfig>(json);
				}
				else if (key.Equals("CANDY_RUN_COMPANION_UPGRADES"))
				{
					CompanionsUpgrades = JsonHelper.FromJson<CompanionsUpgradesOverrideConfig>(json);
				}
				else if (key.Equals("CANDY_RUN_CHEST_LOOT"))
				{
					ChestContent = JsonHelper.FromJson<ChestContentOverrideConfig>(json);
				}
				else if (key.Equals("CANDY_RUN_DAILY_MISSIONS"))
				{
					DailyMissions = JsonHelper.FromJson<DailyMissionsOverrideConfig>(json);
				}
			}
			catch (Exception ex)
			{
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.LogWarning("Cannot parse CDN item key=" + key + " json=" + json + " exception=" + ex.Message);
				}
			}
		}

		private void UpdateCharactersUpgradeBalancingData(StateEntity balancingEntity)
		{
			if (CharactersUpgrades == null || CharactersUpgrades.Count <= 0)
			{
				return;
			}
			Dictionary<int, List<CharactersUpgradesBalancingData>> map = balancingEntity.characterUpgradesBalancingData.Map;
			map.Clear();
			for (int i = 0; i < CharactersUpgrades.Count; i++)
			{
				if (!map.ContainsKey(CharactersUpgrades[i].Id))
				{
					map.Add(CharactersUpgrades[i].Id, new List<CharactersUpgradesBalancingData>());
				}
				map[CharactersUpgrades[i].Id].Add(CharactersUpgrades[i].ConvertToBalancingData());
			}
		}

		private void UpdateCompanionsUpgradeBalancingData(StateEntity balancingEntity)
		{
			if (CompanionsUpgrades == null || CompanionsUpgrades.Count <= 0)
			{
				return;
			}
			Dictionary<int, List<CompanionsUpgradesBalancingData>> map = balancingEntity.companionsUpgradesBalancingData.Map;
			map.Clear();
			for (int i = 0; i < CompanionsUpgrades.Count; i++)
			{
				if (!map.ContainsKey(CompanionsUpgrades[i].Id))
				{
					map.Add(CompanionsUpgrades[i].Id, new List<CompanionsUpgradesBalancingData>());
				}
				map[CompanionsUpgrades[i].Id].Add(CompanionsUpgrades[i].ConvertToBalancingData());
			}
		}

		private void UpdateChestsContentBalancingData(StateEntity balancingEntity)
		{
			if (ChestContent == null || ChestContent.Count <= 0)
			{
				return;
			}
			Dictionary<int, ChestData> map = balancingEntity.gatchaBalancingData.Map;
			foreach (KeyValuePair<int, ChestData> item in map)
			{
				item.Value.RewardsData.Clear();
			}
			for (int i = 0; i < ChestContent.Count; i++)
			{
				if (map.ContainsKey(ChestContent[i].Chestid))
				{
					map[ChestContent[i].Chestid].RewardsData.Add(ChestContent[i].ConvertToChestRewardData());
				}
			}
		}

		private void UpdateDailyMissionsBalancingData(StateEntity balancingEntity)
		{
			if (DailyMissions == null || DailyMissions.Count <= 0)
			{
				return;
			}
			Dictionary<int, DailyMissionsBalancingData> map = balancingEntity.dailyMissionsBalancingData.Map;
			map.Clear();
			for (int i = 0; i < DailyMissions.Count; i++)
			{
				if (!map.ContainsKey(DailyMissions[i].Id))
				{
					map.Add(DailyMissions[i].Id, DailyMissions[i].ConvertToBalancingData());
				}
			}
		}

		private void UpdateStaminaSettingsBalancingData(StateEntity balancingEntity)
		{
			if (ExtConfig != null && ExtConfig.GridExtension != null && ExtConfig.GridExtension.StaminaSettings != null && balancingEntity.hasStaminaSettingsBalancingData)
			{
				balancingEntity.staminaSettingsBalancingData.Map[StaminaSettings.MaxStamina] = ExtConfig.GridExtension.StaminaSettings.MaxStamina;
				balancingEntity.staminaSettingsBalancingData.Map[StaminaSettings.StaminaDecreasingFactor] = ExtConfig.GridExtension.StaminaSettings.StaminaDecreasingFactor;
				balancingEntity.staminaSettingsBalancingData.Map[StaminaSettings.StaminaLostPerHit] = ExtConfig.GridExtension.StaminaSettings.StaminaLostPerHit;
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.LogWarning("UpdateStaminaSettingsBalancingData");
				}
			}
		}

		private void UpdateRestoreSettingsBalancingData(StateEntity balancingEntity)
		{
			if (ExtConfig != null && ExtConfig.GridExtension != null && ExtConfig.GridExtension.RestoreSettings != null && balancingEntity.hasRestoreSettingsBalancingData)
			{
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.MaxResurectsPerPlay] = ExtConfig.GridExtension.RestoreSettings.MaxResurectsPerPlay;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.MaxRevivesPerPlay] = ExtConfig.GridExtension.RestoreSettings.MaxRevivesPerPlay;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.RelayCost] = ExtConfig.GridExtension.RestoreSettings.RelayCost;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.RelayDecisionTime] = ExtConfig.GridExtension.RestoreSettings.RelayDecisionTime;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.ResurectCost] = ExtConfig.GridExtension.RestoreSettings.ResurectCost;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.ResurectDecisionTime] = ExtConfig.GridExtension.RestoreSettings.ResurectDecisionTime;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.ResurectsForVideoPerPlay] = ExtConfig.GridExtension.RestoreSettings.ResurectsForVideoPerPlay;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.ResurectStaminaAmount] = ExtConfig.GridExtension.RestoreSettings.ResurectStaminaAmount;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.ReviveCost] = ExtConfig.GridExtension.RestoreSettings.ReviveCost;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.ReviveDecisionTime] = ExtConfig.GridExtension.RestoreSettings.ReviveDecisionTime;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.ReviveMinStaminaAmount] = ExtConfig.GridExtension.RestoreSettings.ReviveMinStaminaAmount;
				balancingEntity.restoreSettingsBalancingData.Map[RestoreSettings.RevivesForVideo] = ExtConfig.GridExtension.RestoreSettings.RevivesForVideo;
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.LogWarning("UpdateRestoreSettingsBalancingData");
				}
			}
		}

		private void UpdateInitConfigBalancingData(StateEntity balancingEntity)
		{
			if (ExtConfig != null && ExtConfig.GridExtension != null && balancingEntity.hasInitConfigData)
			{
				if (ExtConfig.GridExtension.InitialCoins >= 0)
				{
					balancingEntity.initConfigData.Map[EnumSettings.InitCoins] = ExtConfig.GridExtension.InitialCoins;
				}
				if (ExtConfig.GridExtension.InitialDiamonds >= 0)
				{
					balancingEntity.initConfigData.Map[EnumSettings.InitHardCurrency] = ExtConfig.GridExtension.InitialDiamonds;
				}
				if (ExtConfig.GridExtension.DailyMissionChestId >= 0)
				{
					balancingEntity.initConfigData.Map[EnumSettings.DailyMissionChestId] = ExtConfig.GridExtension.DailyMissionChestId;
				}
				if (ExtConfig.GridExtension.ResetDailyMissions_H >= 0)
				{
					balancingEntity.initConfigData.Map[EnumSettings.ResetDailyMissionsHour_24H] = ExtConfig.GridExtension.ResetDailyMissions_H;
				}
				UpdateUserStateIfNeeded(ExtConfig.GridExtension.InitialCoins, ExtConfig.GridExtension.InitialDiamonds);
				if (O7SDKBase.O7SDK_LOG)
				{
					Debug.LogWarning("UpdateInitConfigBalancingData");
				}
			}
		}

		private void UpdateUserStateIfNeeded(int newCoins, int newDiamonds)
		{
			if (newCoins >= 0 && newDiamonds >= 0 && !PlayerPrefs.HasKey("Coins") && !PlayerPrefs.HasKey("HardCurrency"))
			{
				StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("Coins").SingleEntity();
				stateEntity.integer.Amount = newCoins;
				stateEntity.isToSave = true;
				StateEntity stateEntity2 = Contexts.sharedInstance.state.GetEntitiesWithName("HardCurrency").SingleEntity();
				stateEntity2.integer.Amount = newDiamonds;
				stateEntity2.isToSave = true;
			}
		}
	}
}
