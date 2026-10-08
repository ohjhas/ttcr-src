using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class AbilityPopup : BaseScreen
	{
		[Header("Ability Popup")]
		[SerializeField]
		private TextMeshProUGUI Title;

		[SerializeField]
		private TextMeshProUGUI Description;

		[SerializeField]
		private Image Icon;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.AbilityPopup;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			if (parameters != null)
			{
				if (parameters.hasUICharSingleResponse)
				{
					CSCharacterData charData = parameters.uICharSingleResponse.CharData;
					CharactersUpgradesBalancingData charactersUpgradesBalancingData = charData.Upgrades[charData.Level - 1];
					RefreshUI(charData.Level, charData.Ability, charactersUpgradesBalancingData.Abilityparameter, charactersUpgradesBalancingData.Abilityduration, charactersUpgradesBalancingData.Abilitychargetime);
				}
				else if (parameters.hasUICompSingleResponse)
				{
					CSCompanionData compData = parameters.uICompSingleResponse.CompData;
					CompanionsUpgradesBalancingData companionsUpgradesBalancingData = compData.Upgrades[compData.Level - 1];
					RefreshUI(compData.Level, compData.Ability, companionsUpgradesBalancingData.Abilityparameter, companionsUpgradesBalancingData.Abilityduration, companionsUpgradesBalancingData.Abilitychargetime);
				}
			}
		}

		private void RefreshUI(int level, AbilitiesBalancingData ability, float abilityParameter, float abilityDuration, float abilityChargingTime)
		{
			Title.text = ScriptLocalization.general_level + " " + level + " " + ability.Name;
			object[] abilityParams = GetAbilityParams(ability, abilityParameter, abilityDuration, abilityChargingTime);
			Description.text = ((abilityParams != null) ? string.Format(ability.Description, abilityParams) : ability.Description);
			Icon.sprite = SettingsService.AssetsSettingsSO.GetSprite(ability.Assetname);
		}

		private object[] GetAbilityParams(AbilitiesBalancingData ability, float abilityParameter, float abilityDuration, float abilityChargingTime)
		{
			switch (ability.ABILITYTYPE)
			{
			case AbilityType.FasterMovement:
			{
				int num = Mathf.RoundToInt((abilityParameter - 1f) * 100f);
				string text = "<color=orange>" + num + "%</color>";
				return new string[1] { text };
			}
			case AbilityType.UpgradeScore:
			case AbilityType.Magnet:
			case AbilityType.Speedup:
			case AbilityType.CreateStamina:
			case AbilityType.CreatePowerup:
			case AbilityType.CreateSpeedup:
			case AbilityType.PoliceCarTom:
			case AbilityType.GiantSpeedup:
			case AbilityType.Shield:
			{
				string text = "<color=orange>" + abilityChargingTime.ToString("N1") + "</color>";
				return new string[1] { text };
			}
			case AbilityType.Revive:
			{
				int num = Mathf.RoundToInt(abilityParameter * 100f);
				string text = "<color=orange>" + num + "%</color>";
				return new string[1] { text };
			}
			case AbilityType.DragonBlast:
			{
				string text = "<color=orange>" + abilityDuration.ToString("N1") + "</color>";
				string text2 = "<color=orange>" + abilityChargingTime.ToString("N1") + "</color>";
				return new string[2] { text, text2 };
			}
			default:
				return null;
			}
		}
	}
}
