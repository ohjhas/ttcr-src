using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CSStaminaDisplay : MexicanWaveBarBehaviour
	{
		[SerializeField]
		private List<CSStaminaBox> StaminaBoxes;

		[SerializeField]
		private float countingTime;

		private bool isAnimated;

		private float oldValue;

		private float newValue;

		private float currentValue;

		private float timer;

		private int staminaIncrease;

		private bool enoughParts;

		private SettingsSO settingsSO;

		private new void Awake()
		{
			base.Awake();
			settingsSO = SettingsService.SettingsSO;
		}

		private void Update()
		{
			if (isAnimated)
			{
				timer += Time.deltaTime;
				float t = timer / countingTime;
				currentValue = Mathf.Lerp(oldValue, newValue, t);
				int num = Mathf.RoundToInt(currentValue);
				SetData(num, staminaIncrease, enoughParts, isAnimated);
				if (num == Mathf.RoundToInt(newValue))
				{
					isAnimated = false;
				}
			}
		}

		public void SetData(int charStamina, int upgradeStaminaIncrease, bool enoughPartsToUpgrade, bool _isAnimated = false)
		{
			StaminaSettingsBalancingDataComponent staminaSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.staminaSettingsBalancingData;
			float num = staminaSettingsBalancingData.Map[StaminaSettings.MaxStamina];
			float num2 = num / (float)StaminaBoxes.Count;
			float num3 = charStamina;
			float num4 = upgradeStaminaIncrease;
			for (int i = 0; i < StaminaBoxes.Count; i++)
			{
				if (num3 >= num2)
				{
					StaminaBoxes[i].SetState(1f, 0f);
					num3 -= num2;
					continue;
				}
				float num5 = num3 / num2;
				float outlineAmount = 0f;
				if (enoughPartsToUpgrade)
				{
					if (num4 > num2 - num3)
					{
						outlineAmount = 1f - num5;
						num4 -= num2 - num3;
					}
					else
					{
						outlineAmount = num4 / num2;
						num4 = 0f;
					}
				}
				StaminaBoxes[i].SetState(num5, outlineAmount);
				num3 = 0f;
			}
			isAnimated = _isAnimated;
		}

		public void Animate(int oldCharStamina, int newCharStamina, int upgradeStaminaIncrease, bool enoughPartsToUpgrade, float duration, float offset, float firstBarPunchStrength, float lastBarPunchStrength)
		{
			isAnimated = true;
			timer = 0f;
			oldValue = (currentValue = oldCharStamina);
			newValue = newCharStamina;
			staminaIncrease = upgradeStaminaIncrease;
			enoughParts = enoughPartsToUpgrade;
			List<GameObject> bars = StaminaBoxes.Select((CSStaminaBox box) => box.gameObject).ToList();
			PunchBarsIfPossible(bars, 0f, duration, offset, firstBarPunchStrength, lastBarPunchStrength);
		}
	}
}
