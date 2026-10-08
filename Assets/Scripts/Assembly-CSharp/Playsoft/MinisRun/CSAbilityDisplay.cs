using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CSAbilityDisplay : MonoBehaviour
	{
		public struct AbilityInfo
		{
			public int Level;

			public float Parameter;

			public float Duration;

			public float ChargingTime;
		}

		[SerializeField]
		private GameObject abilityExistsPart;

		[SerializeField]
		private GameObject noAbilityPart;

		[SerializeField]
		private Image iconImage;

		[SerializeField]
		private TextMeshProUGUI titleText;

		[SerializeField]
		private TextMeshProUGUI descriptionText;

		public void SetData(AbilitiesBalancingData balancingData, AbilityInfo info)
		{
			if (balancingData.ABILITYTYPE.Equals(AbilityType.None))
			{
				abilityExistsPart.gameObject.SetActive(false);
				noAbilityPart.gameObject.SetActive(true);
				return;
			}
			abilityExistsPart.SetActive(true);
			noAbilityPart.SetActive(false);
			SetText(balancingData, info);
			SetIcon(balancingData.Assetname);
		}

		private void SetText(AbilitiesBalancingData balancingData, AbilityInfo info)
		{
			titleText.text = string.Format("{0} {1} {2}", ScriptLocalization.general_level, info.Level, balancingData.Name);
			object[] abilityParams = GetAbilityParams(balancingData, info.Parameter, info.Duration, info.ChargingTime);
			descriptionText.text = ((abilityParams != null) ? string.Format(balancingData.Description, abilityParams) : balancingData.Description);
		}

		private void SetIcon(string assetName)
		{
			Sprite sprite = SettingsService.AssetsSettingsSO.GetSprite(assetName);
			iconImage.sprite = sprite;
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
