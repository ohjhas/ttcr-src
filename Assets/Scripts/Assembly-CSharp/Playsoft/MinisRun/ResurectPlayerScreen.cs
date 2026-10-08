using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ResurectPlayerScreen : BaseScreen
	{
		[Header("Resurect Player Screen")]
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

		private bool isConfirmed = true;

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
				return ScreenId.ResurrectPopup;
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
				payButton.onClick.AddListener(ResurectClicked);
				freeButton.onClick.AddListener(ResurectClicked);
				bool flag = PlayerRestoreHelper.IsResurectForVideoAvailable();
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
				timer.fillAmount = timeSeconds / (float)balancingData.Map[RestoreSettings.ResurectDecisionTime];
				if (timeSeconds <= 0f && !isClosed && !isConfirmed)
				{
					isClosed = true;
					EndWaiting();
					base.uiContext.CloseDialog(this);
				}
			}
		}

		private void ResurectClicked()
		{
			if (base.IsClickable)
			{
				isConfirmed = true;
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isResurectPlayerScreen = true;
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
			timeSeconds = balancingData.Map[RestoreSettings.ResurectDecisionTime];
		}

		private void SetCost()
		{
			int num = balancingData.Map[RestoreSettings.ResurectCost];
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
