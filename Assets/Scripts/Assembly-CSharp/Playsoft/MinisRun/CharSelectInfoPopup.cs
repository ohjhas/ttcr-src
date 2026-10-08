using DG.Tweening;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CharSelectInfoPopup : BasePopupScreen
	{
		[Header("Info popup")]
		[SerializeField]
		private RectTransform characterHolder;

		[SerializeField]
		private CharEntryPositioner charPositioner;

		[SerializeField]
		private TextMeshProUGUI characterNameText;

		[SerializeField]
		private CSStaminaDisplay staminaDisplay;

		[SerializeField]
		private CSEnergyDisplay energyDisplay;

		[SerializeField]
		private CSAbilityDisplay abilityDisplay;

		[SerializeField]
		private CSPartsBar partsBar;

		[SerializeField]
		private CSUpgradePart upgradePart;

		[SerializeField]
		private HorizontalShineEffect _ribbonShineEffect;

		[SerializeField]
		private BounceEffect _abilityBounceEffect;

		[SerializeField]
		private BounceEffect _staminaBounceEffect;

		[SerializeField]
		private GameObject abilityExistsPart;

		[SerializeField]
		private CanvasParticleEmitter particles;

		private bool isSameCharacter;

		private int charId = -1;

		private RectTransform characterObject;

		private Tweener jumpTween;

		private bool isUpgradeAnimationTime;

		private CSCharacterData characterBeforeUpgrade;

		public int CharacterId
		{
			get
			{
				return charId;
			}
			private set
			{
				charId = value;
			}
		}

		public override ScreenId Id
		{
			get
			{
				return ScreenId.CharInfoPopup;
			}
		}

		public override RectTransform GetLastFocusElement()
		{
			return upgradePart.transform as RectTransform;
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (!visible)
			{
				isUpgradeAnimationTime = false;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			if (parameters == null || !parameters.hasUICharSingleResponse)
			{
				return;
			}
			CSCharacterData charData = parameters.uICharSingleResponse.CharData;
			int ownedParts = charData.OwnedParts;
			int nextUpgradeCostParts = charData.GetNextUpgradeCostParts();
			bool flag = ownedParts >= nextUpgradeCostParts;
			int upgradeStaminaIncrease = charData.GetNextUpgradeStaminaValue() - charData.Stamina;
			isSameCharacter = charData.Id == CharacterId;
			CharacterId = charData.Id;
			CharactersUpgradesBalancingData charactersUpgradesBalancingData = charData.Upgrades[charData.Level - 1];
			energyDisplay.SetData(CharacterId, charData.Lifes, charData.MaxLifes, charData.LastRefillTime.AddSeconds(charData.RefillDuration));
			partsBar.SetData(charData.IsNextUpgradeAvailable(), ownedParts, nextUpgradeCostParts);
			upgradePart.SetData(UpgradeCharacterClicked, GetPartsClicked, charData.IsNextUpgradeAvailable(), flag, charData.GetNextUpgradeCostSC());
			if (!isUpgradeAnimationTime)
			{
				staminaDisplay.SetData(charData.Stamina, upgradeStaminaIncrease, flag);
				abilityDisplay.SetData(charData.Ability, new CSAbilityDisplay.AbilityInfo
				{
					Level = charData.Level,
					Parameter = charactersUpgradesBalancingData.Abilityparameter,
					Duration = charactersUpgradesBalancingData.Abilityduration,
					ChargingTime = charactersUpgradesBalancingData.Abilitychargetime
				});
				characterNameText.text = charData.Name.ToUpper() + " <color=#E65205FF>" + (ScriptLocalization.general_level + " " + charData.Level).ToUpper() + "</color>";
				CreateCharacter(CharacterId);
			}
			CharacterAnimatorController componentInChildren = characterObject.GetComponentInChildren<CharacterAnimatorController>();
			if (componentInChildren != null)
			{
				componentInChildren.PrefetchAnimationAsync(CharacterId, CharAnimationType.JumpAndWin);
				componentInChildren.ClearForceAnimation(charData.Id);
			}
			if (isUpgradeAnimationTime)
			{
				isUpgradeAnimationTime = false;
				ShowUpgradeAnimation(charData, charactersUpgradesBalancingData, characterBeforeUpgrade.Stamina, upgradeStaminaIncrease, flag, componentInChildren);
				upgradePart.SetState(false);
				DOVirtual.DelayedCall(SettingsService.SettingsSO.ChangeLevelTextEffectDelayAbilityPresent, delegate
				{
					upgradePart.SetState(true);
				});
			}
			characterBeforeUpgrade = charData;
		}

		private void CreateCharacter(int characterId)
		{
			if (!isSameCharacter)
			{
				if (characterObject != null)
				{
					Object.Destroy(characterObject.gameObject);
				}
				RectTransform characterUIAsset = base.uiContext.GetCharacterUIAsset(characterId);
				characterObject = Object.Instantiate(characterUIAsset);
				characterObject.transform.SetParent(characterHolder);
				characterObject.transform.localScale = Vector3.one;
				characterObject.transform.localPosition = Vector3.zero;
				RectTransform obj = characterObject;
				Vector2 zero = Vector2.zero;
				characterObject.offsetMin = zero;
				obj.offsetMax = zero;
				Image componentInChildren = characterObject.GetComponentInChildren<Image>();
				charPositioner.Init(componentInChildren, 1.8f);
				CharacterAnimatorController component = characterObject.GetComponent<CharacterAnimatorController>();
				if (component != null)
				{
					component.Init(characterId, ScreenType.CharSelect);
				}
			}
		}

		private void UpgradeCharacterClicked()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isCharacterSelectScreen = true;
				uIEntity.AddUICharacterUpgraded(CharacterId);
				isUpgradeAnimationTime = true;
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

		private void ShowUpgradeAnimation(CSCharacterData newCharData, CharactersUpgradesBalancingData upgradeData, int oldCharracterStamina, int upgradeStaminaIncrease, bool enoughPartsToUpgrade, CharacterAnimatorController charController)
		{
			SettingsSO so = SettingsService.SettingsSO;
			DOVirtual.DelayedCall(so.StaminaEffectDelay, delegate
			{
				staminaDisplay.Animate(oldCharracterStamina, newCharData.Stamina, upgradeStaminaIncrease, enoughPartsToUpgrade, so.StaminaBounceEffectDuration, so.StaminaBounceEffectInBetweenPunchesOffset, so.StaminaBounceEffectFirstBounceStrength, so.StaminaBounceEffectLastBounceStrength);
			});
			DOVirtual.DelayedCall(so.StaminaFieldBounceDelay, delegate
			{
				_staminaBounceEffect.Show(so.StaminaFieldBouncePunch * Vector3.one, so.StaminaFieldBounceDuration, so.BouncePunchAnimationCurve);
			});
			if (abilityExistsPart.activeSelf)
			{
				DOVirtual.DelayedCall(so.AbilityBounceEffectDelay, delegate
				{
					_abilityBounceEffect.Show(so.AbilityBouncePunch * Vector3.one, so.AbilityBounceEffectDuration, so.BouncePunchAnimationCurve);
				});
				DOVirtual.DelayedCall(so.AbilityChangeTextDelay, delegate
				{
					abilityDisplay.SetData(newCharData.Ability, new CSAbilityDisplay.AbilityInfo
					{
						Level = newCharData.Level,
						Parameter = upgradeData.Abilityparameter,
						Duration = upgradeData.Abilityduration,
						ChargingTime = upgradeData.Abilitychargetime
					});
				});
			}
			float delay = ((!abilityExistsPart.activeSelf) ? so.WinAnimationDelayAbilityAbsent : so.WinAnimationDelayAbilityPresent);
			DOVirtual.DelayedCall(delay, delegate
			{
				if (charController != null)
				{
					charController.ForceAnimation(newCharData.Id, CharAnimationType.JumpAndWin);
					if (jumpTween != null)
					{
						jumpTween.Kill();
					}
					RectTransform component = charController.GetComponent<RectTransform>();
					component.anchoredPosition = Vector2.zero;
					jumpTween = component.DOAnchorPos(so.CharacterJumpAnimationTopPosition, so.CharacterJumpAnimationDuration).SetEase(so.CharacterJumpAnimationCurve).SetUpdate(true);
				}
			});
			float delay2 = ((!abilityExistsPart.activeSelf) ? so.RibbonEffectsDelayAbilityAbsent : so.RibbonEffectsDelayAbilityPresent);
			DOVirtual.DelayedCall(delay2, delegate
			{
				_ribbonShineEffect.Show(so.RibbonShineEffectDuration);
			});
			float delay3 = ((!abilityExistsPart.activeSelf) ? so.ChangeLevelTextEffectDelayAbilityAbsent : so.ChangeLevelTextEffectDelayAbilityPresent);
			DOVirtual.DelayedCall(delay3, delegate
			{
				characterNameText.text = newCharData.Name.ToUpper() + " <color=#E65205FF>" + (ScriptLocalization.general_level + " " + newCharData.Level).ToUpper() + "</color>";
			});
			DOVirtual.DelayedCall(so.ParticlesStartTime, delegate
			{
				particles.StartEmitter();
			});
		}

		private void OnDisable()
		{
			particles.StopEmitter();
		}
	}
}
