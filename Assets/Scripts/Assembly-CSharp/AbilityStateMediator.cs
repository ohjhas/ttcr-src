using UnityEngine.UI;

public class AbilityStateMediator : BaseUI, IAbilityChangedListener, IAbilityStateChangedListener
{
	private Image _stateImage;

	private Image _fillingImage;

	protected override void Setup()
	{
		_stateImage = GetUIComponent<Image>("StateImage");
		_fillingImage = GetUIComponent<Image>("FillingImage");
		GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
		gameEntity.AddAbilityChangedListener(this);
		gameEntity.AddAbilityStateChangedListener(this);
	}

	public void OnAbilityChanged(string abilityAssetName)
	{
		_stateImage.sprite = SettingsService.AssetsSettingsSO.GetSprite(abilityAssetName);
		_fillingImage.sprite = SettingsService.AssetsSettingsSO.GetSprite(abilityAssetName);
	}

	public void OnAbilityStateChanged(float percentCharged)
	{
		if (base.gameObject.activeInHierarchy)
		{
			_fillingImage.fillAmount = percentCharged;
		}
	}
}
