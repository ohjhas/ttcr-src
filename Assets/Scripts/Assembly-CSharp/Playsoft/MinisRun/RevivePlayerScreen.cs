using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class RevivePlayerScreen : BaseScreen
	{
		[Header("Revive Player Screen")]
		[SerializeField]
		private Image timer;

		[SerializeField]
		private TextMeshProUGUI cost;

		[SerializeField]
		private Button payButton;

		[SerializeField]
		private Button freeButton;

		private float timeSeconds;

		private bool isClosed = true;

		private bool isConfirmed;

		private RestoreSettingsBalancingDataComponent balancingData
		{
			get
			{
				return Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			}
		}

		public override ScreenId Id
		{
			get
			{
				return ScreenId.RevivePopup;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				SetTimer();
				SetCost();
				isClosed = false;
				isConfirmed = false;
				payButton.onClick.RemoveAllListeners();
				freeButton.onClick.RemoveAllListeners();
				payButton.onClick.AddListener(ReviveClicked);
				freeButton.onClick.AddListener(ReviveClicked);
				bool flag = PlayerRestoreHelper.IsReviveForVideoAvailable();
				freeButton.gameObject.SetActive(flag);
				payButton.gameObject.SetActive(!flag);
			}
			else
			{
				isClosed = true;
				if (!isConfirmed)
				{
					EndWaiting();
				}
			}
		}

		private void Update()
		{
			if (base.IsClickable)
			{
				timeSeconds -= Time.deltaTime;
				timer.fillAmount = timeSeconds / (float)balancingData.Map[RestoreSettings.ReviveDecisionTime];
				if (timeSeconds <= 0f && !isClosed && !isConfirmed)
				{
					isClosed = true;
					EndWaiting();
					base.uiContext.CloseDialog(this);
				}
			}
		}

		private void ReviveClicked()
		{
			if (base.IsClickable)
			{
				isConfirmed = true;
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isRevivePlayerScreen = true;
				Contexts.sharedInstance.uI.CloseDialog(this);
			}
		}

		private void EndWaiting()
		{
			isConfirmed = true;
			base.uiContext.OpenDialog<FailScreen>();
			SendPlayerLostEvent();
		}

		private void SetTimer()
		{
			timer.fillAmount = 1f;
			timeSeconds = balancingData.Map[RestoreSettings.ReviveDecisionTime];
		}

		private void SetCost()
		{
			int num = balancingData.Map[RestoreSettings.ReviveCost];
			cost.text = num.ToString("N0");
		}

		private void SendPlayerLostEvent()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isPlayerLost = true;
		}
	}
}
