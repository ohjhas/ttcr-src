using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class MissionDetailsProgressBar
	{
		[SerializeField]
		private RectTransform container;

		[SerializeField]
		private TextMeshProUGUI[] texts;

		[SerializeField]
		private GameObject[] fillings;

		[SerializeField]
		private Image progressImage;

		[SerializeField]
		private NotLinearProgressBar progressBarFactor;

		[Space]
		[SerializeField]
		private float normalPositionY;

		[SerializeField]
		private float centeredPositionY;

		public void SetUp(int[] goals, int result, bool shouldBeCentered)
		{
			for (int i = 0; i < goals.Length; i++)
			{
				int num = goals[i];
				texts[i].text = NumbersHelper.GetNumberSplitedBySpaces(num);
				fillings[i].SetActive(result >= num);
			}
			progressImage.fillAmount = progressBarFactor.GetFillAmount(result, goals[0], goals[1], goals[2]);
			SetPosition(shouldBeCentered);
		}

		private void SetPosition(bool shouldBeCentered)
		{
			float y = ((!shouldBeCentered) ? normalPositionY : centeredPositionY);
			container.anchoredPosition = new Vector2(container.anchoredPosition.x, y);
		}
	}
}
