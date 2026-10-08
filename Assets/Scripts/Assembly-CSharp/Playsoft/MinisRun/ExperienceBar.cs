using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ExperienceBar : MonoBehaviour
	{
		[SerializeField]
		private RectTransform _star;

		[SerializeField]
		private Image _bar;

		[SerializeField]
		private TextMeshProUGUI _text;

		[SerializeField]
		private float _barFillingDuration;

		public RectTransform Star
		{
			get
			{
				return _star;
			}
		}

		public void SetBarAndText(int currentExperienceRequired, int nextExperienceRequired, int lastExperienceVisible, int lastLevelVisible)
		{
			_text.text = lastLevelVisible.ToString();
			_bar.fillAmount = Mathf.InverseLerp(currentExperienceRequired, nextExperienceRequired, lastExperienceVisible);
		}

		public IEnumerator UpdateBar(int currentLevelNumber, int currentExpAmount, Dictionary<int, PlayerLevelData> playerLevelsData, int lastLevelVisible, float flyingElementsDuration)
		{
			yield return new WaitForSeconds(flyingElementsDuration);
			for (int i = lastLevelVisible; i <= currentLevelNumber; i++)
			{
				_text.text = i.ToString();
				if (i != lastLevelVisible)
				{
					_bar.fillAmount = 0f;
				}
				int currentRequired = playerLevelsData[i].RequiredExperience;
				int nextRequired = playerLevelsData[i + 1].RequiredExperience;
				int endValue = ((i != currentLevelNumber) ? nextRequired : currentExpAmount);
				DOTween.To(endValue: Mathf.InverseLerp(currentRequired, nextRequired, endValue), setter: delegate(float x)
				{
					_bar.fillAmount = x;
				}, startValue: _bar.fillAmount, duration: _barFillingDuration);
				yield return new WaitForSeconds(_barFillingDuration);
			}
		}
	}
}
