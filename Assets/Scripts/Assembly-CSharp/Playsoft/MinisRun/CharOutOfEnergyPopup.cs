using System;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CharOutOfEnergyPopup : BaseScreen
	{
		[SerializeField]
		private TextMeshProUGUI description;

		[SerializeField]
		private TextMeshProUGUI refillPrice;

		[SerializeField]
		private Button refillButton;

		private int charIndex;

		private Action callback;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.OutOfEnergyPopup;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (parameters != null && parameters.hasUICharSingleResponse && parameters.hasUITotalGemsResponse && parameters.hasUIActionFinishedCallback)
			{
				int refillCost = parameters.uICharSingleResponse.CharData.RefillCost;
				charIndex = parameters.uICharSingleResponse.CharData.Id;
				callback = parameters.uIActionFinishedCallback.Callback;
				refillPrice.text = refillCost.ToString();
				refillButton.gameObject.SetActive(true);
				description.text = ScriptLocalization.refill_popup_description_part_1 + ScriptLocalization.refill_popup_description_part_2;
			}
		}

		public void OnRefillPressed()
		{
			base.uiContext.CloseDialog(this);
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIRefillChar(charIndex);
			uIEntity.AddUIActionFinishedCallback(callback);
		}
	}
}
