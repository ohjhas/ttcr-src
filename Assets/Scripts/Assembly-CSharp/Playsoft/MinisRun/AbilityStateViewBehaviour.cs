using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class AbilityStateViewBehaviour : BaseUI, IAbilityChangedListener, IAbilityStateChangedListener
	{
		private Image stateImage;

		private Image fillingImage;

		private Image glowImage;

		private bool isGlowImageAnimating;

		private Tweener tweener;

		private float glowStartTime;

		private double glowEndTime;

		private bool forceAnimateGlow;

		private double glowDuration = 2.0;

		protected override void Setup()
		{
			stateImage = GetUIComponent<Image>("StateImage");
			fillingImage = GetUIComponent<Image>("FillingImage");
			glowImage = GetUIComponent<Image>("GlowImage");
			glowImage.gameObject.SetActive(false);
			GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
			gameEntity.AddAbilityChangedListener(this);
			gameEntity.AddAbilityStateChangedListener(this);
		}

		public void OnAbilityChanged(string abilityAssetName)
		{
			Sprite sprite = SettingsService.AssetsSettingsSO.GetSprite(abilityAssetName);
			if (sprite == null)
			{
				base.gameObject.SetActive(false);
			}
			stateImage.sprite = sprite;
		}

		public void OnAbilityStateChanged(float percentCharged)
		{
			if (base.gameObject.activeInHierarchy && !isGlowImageAnimating && !forceAnimateGlow)
			{
				fillingImage.fillAmount = percentCharged;
			}
			bool shouldShowGlow = percentCharged >= 1f;
			ShowGlowImage(shouldShowGlow);
		}

		private void ShowGlowImage(bool shouldShowGlow)
		{
			if (shouldShowGlow)
			{
				if (!isGlowImageAnimating)
				{
					glowStartTime = Time.time;
					TweenGlowImage();
					fillingImage.fillAmount = 1f;
				}
			}
			else if (isGlowImageAnimating)
			{
				float num = Time.time - glowStartTime;
				if ((double)num < glowDuration && !forceAnimateGlow)
				{
					forceAnimateGlow = true;
				}
				else if (!forceAnimateGlow)
				{
					StopTweenGlowImage();
				}
			}
		}

		private void TweenGlowImage()
		{
			isGlowImageAnimating = true;
			glowImage.color = new Color(1f, 1f, 1f, 1f);
			glowImage.gameObject.SetActive(true);
			tweener = glowImage.DOFade(0f, 0.5f).SetLoops(-1, LoopType.Yoyo);
		}

		private void StopTweenGlowImage()
		{
			tweener.Kill();
			glowImage.gameObject.SetActive(false);
			isGlowImageAnimating = false;
			forceAnimateGlow = false;
		}

		private void Update()
		{
			if (forceAnimateGlow)
			{
				glowEndTime += Time.deltaTime;
				if (glowEndTime > 2.0)
				{
					glowEndTime = 0.0;
					StopTweenGlowImage();
				}
			}
		}
	}
}
