using System;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CSEnergyDisplay : MonoBehaviour
	{
		[SerializeField]
		private GameObject workingPart;

		[SerializeField]
		private GameObject fullPart;

		[SerializeField]
		private TextMeshProUGUI counterValueShadow;

		[SerializeField]
		private TextMeshProUGUI counterValue;

		[Space]
		[SerializeField]
		private CountDownTimerView timerValue;

		public void SetData(int charId, int currentLifes, int maxLifes, DateTime refillTime)
		{
			workingPart.SetActive(false);
			fullPart.SetActive(false);
			TextMeshProUGUI textMeshProUGUI = counterValueShadow;
			string text = currentLifes + "/" + maxLifes;
			counterValue.text = text;
			textMeshProUGUI.text = text;
			if (currentLifes >= maxLifes)
			{
				fullPart.SetActive(true);
				return;
			}
			workingPart.SetActive(true);
			timerValue.Setup(refillTime, delegate
			{
				CharSelectInfoPopup dialog = Contexts.sharedInstance.uI.GetDialog<CharSelectInfoPopup>();
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				uIEntity.AddSetup(dialog);
				uIEntity.AddUICharSingleRequest(charId);
			});
		}
	}
}
