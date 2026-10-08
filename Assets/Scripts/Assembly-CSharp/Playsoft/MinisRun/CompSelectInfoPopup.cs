using I2.Loc;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompSelectInfoPopup : BasePopupScreen
	{
		[Header("Info popup")]
		[SerializeField]
		private RectTransform companionrHolder;

		[SerializeField]
		private TextMeshProUGUI companionNameText;

		[SerializeField]
		private CSAbilityDisplay abilityDisplay;

		[SerializeField]
		private CSPartsBar partsBar;

		[SerializeField]
		private CSUpgradePart upgradePart;

		private bool isSameCompanion;

		private int compId = -1;

		private RectTransform companionObject;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.CompInfoPopup;
			}
		}

		public override RectTransform GetLastFocusElement()
		{
			return upgradePart.transform as RectTransform;
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			if (parameters != null && parameters.hasUICompSingleResponse)
			{
				CSCompanionData compData = parameters.uICompSingleResponse.CompData;
				isSameCompanion = compData.Id == compId;
				compId = compData.Id;
				int ownedParts = compData.OwnedParts;
				int nextUpgradeCostParts = compData.GetNextUpgradeCostParts();
				bool enoughParts = ownedParts >= nextUpgradeCostParts;
				CompanionsUpgradesBalancingData companionsUpgradesBalancingData = compData.Upgrades[compData.Level - 1];
				partsBar.SetData(compData.IsNextUpgradeAvailable(), ownedParts, nextUpgradeCostParts);
				abilityDisplay.SetData(compData.Ability, new CSAbilityDisplay.AbilityInfo
				{
					Level = compData.Level,
					Parameter = companionsUpgradesBalancingData.Abilityparameter,
					Duration = companionsUpgradesBalancingData.Abilityduration,
					ChargingTime = companionsUpgradesBalancingData.Abilitychargetime
				});
				upgradePart.SetData(UpgradeCompanionClicked, GetPartsClicked, compData.IsNextUpgradeAvailable(), enoughParts, compData.GetNextUpgradeCostSC());
				companionNameText.text = compData.Name.ToUpper() + " <color=#E65205FF>" + (ScriptLocalization.general_level + " " + compData.Level).ToUpper() + "</color>";
				CreateCompanion(compId);
			}
		}

		private void CreateCompanion(int companionId)
		{
			if (!isSameCompanion)
			{
				if (companionObject != null)
				{
					Object.Destroy(companionObject.gameObject);
				}
				RectTransform companionUIAsset = base.uiContext.GetCompanionUIAsset(companionId);
				companionObject = Object.Instantiate(companionUIAsset);
				companionObject.transform.SetParent(companionrHolder);
				companionObject.transform.localScale = Vector3.one;
				companionObject.transform.localPosition = Vector3.zero;
				RectTransform obj = companionObject;
				Vector2 zero = Vector2.zero;
				companionObject.offsetMin = zero;
				obj.offsetMax = zero;
			}
		}

		private void UpgradeCompanionClicked()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isCompanionSelectScreen = true;
				uIEntity.AddUICompanionUpgraded(compId);
			}
		}

		private void GetPartsClicked()
		{
			if (base.IsClickable)
			{
				base.uiContext.CloseDialog(this);
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIOpenShop(ShopGroupType.Chests);
			}
		}
	}
}
