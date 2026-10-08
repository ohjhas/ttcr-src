using Entitas;
using Playsoft.MinisRun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RelayPlayerScreen : BaseScreen
{
	[Header("Relay Player Screen")]
	[SerializeField]
	private Image characterIcon;

	[SerializeField]
	private Image timer;

	[SerializeField]
	private TextMeshProUGUI cost;

	private float timeSeconds;

	private bool isClosed = true;

	private RestoreSettingsBalancingDataComponent balancingData
	{
		get
		{
			return Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
		}
	}

	public override void SetVisible(bool visible)
	{
		base.SetVisible(visible);
		if (visible)
		{
			SetTimer();
			SetChar();
			SetCost();
			isClosed = false;
		}
	}

	private void Update()
	{
		if (base.IsClickable)
		{
			timeSeconds -= Time.deltaTime;
			timer.fillAmount = timeSeconds / (float)balancingData.Map[RestoreSettings.RelayDecisionTime];
			if (timeSeconds <= 0f && !isClosed)
			{
				isClosed = true;
				base.uiContext.CloseDialog(this);
				base.uiContext.OpenDialog<FailScreen>();
				SendPlayerLostEvent();
			}
		}
	}

	public void OnRelayAccepted()
	{
		if (base.IsClickable)
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isRelayPlayerScreen = true;
			base.uiContext.CloseDialog(this);
		}
	}

	private void SetTimer()
	{
		timer.fillAmount = 1f;
		timeSeconds = balancingData.Map[RestoreSettings.RelayDecisionTime];
	}

	private void SetChar()
	{
		int amount = Contexts.sharedInstance.state.GetEntitiesWithName("SecondCharSelectData").SingleEntity().integer.Amount;
		string path = "Characters/Ingame/" + SettingsService.SettingsSO.CharSettings[amount].IngameAssetName;
		GameObject gameObject = Resources.Load<GameObject>(path);
		characterIcon.sprite = gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
	}

	private void SetCost()
	{
		int num = balancingData.Map[RestoreSettings.RelayCost];
		cost.text = num.ToString("N0");
	}

	private void SendPlayerLostEvent()
	{
		InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
		inputEntity.isInput = true;
		inputEntity.isPlayerLost = true;
	}
}
