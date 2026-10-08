using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class CSPartsBar : MonoBehaviour
	{
		[SerializeField]
		private Image barFillImage;

		[SerializeField]
		private Sprite inProgressBarSprite;

		[SerializeField]
		private Sprite fullProgressBarSprite;

		[SerializeField]
		private GameObject inPorgressArrow;

		[SerializeField]
		private GameObject fullArrow;

		[SerializeField]
		private TextMeshProUGUI counterText;

		public void SetData(bool isNextUpgradeAvailable, int currentParts, int maxParts)
		{
			base.gameObject.SetActive(true);
			if (!isNextUpgradeAvailable)
			{
				base.gameObject.SetActive(false);
				return;
			}
			bool flag = currentParts >= maxParts;
			SetFillImage(currentParts, maxParts, flag);
			inPorgressArrow.SetActive(!flag);
			fullArrow.SetActive(flag);
			counterText.text = string.Format("{0}/{1}", currentParts, maxParts);
		}

		private void SetFillImage(int currentParts, int maxParts, bool isFull)
		{
			barFillImage.sprite = ((!isFull) ? inProgressBarSprite : fullProgressBarSprite);
			barFillImage.fillAmount = (float)currentParts / (float)maxParts;
		}
	}
}
