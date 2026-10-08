using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ShopScreen : BaseScreen
	{
		[Space(5f)]
		[Header("Shop Screen")]
		[SerializeField]
		private ScrollRect scrollRect;

		[SerializeField]
		private RectTransform contentRectTransform;

		[SerializeField]
		private Button closeButton;

		[SerializeField]
		private ShopGroup[] shopGroups;

		private bool isNewDisplay;

		public ShopGroupType ShopEntryPoint { private get; set; }

		public override ScreenId Id
		{
			get
			{
				return ScreenId.ShopScreen;
			}
		}

		public override void SetVisible(bool visible)
		{
			isNewDisplay = !base.gameObject.activeInHierarchy;
			base.SetVisible(visible);
			if (visible)
			{
				closeButton.onClick.AddListener(OnClosedClicked);
			}
			else
			{
				closeButton.onClick.RemoveListener(OnClosedClicked);
			}
		}

		public override void SetData(UIEntity parameters)
		{
			base.SetData(parameters);
			base.uiContext.CheckPlayerProgress();
			CurrencyAndExperienceHelper.OpenScreenForShopScreen();
			ShopGroup[] array = shopGroups;
			foreach (ShopGroup shopGroup in array)
			{
				if (!(null == shopGroup))
				{
					shopGroup.gameObject.SetActive(true);
					shopGroup.SetData(parameters);
					shopGroup.AdjustBasedOnAspect(AspectRatioHelper.IsLowAspectRatio());
				}
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
			Canvas.ForceUpdateCanvases();
			PositionShop(!isNewDisplay);
			isNewDisplay = false;
		}

		private void PositionShop(bool scroolToPositon)
		{
			if (ShopEntryPoint == ShopGroupType.NotDefined)
			{
				return;
			}
			float num = 0f;
			ShopGroup[] array = shopGroups;
			foreach (ShopGroup shopGroup in array)
			{
				if (!(null == shopGroup))
				{
					if (shopGroup.GroupType == ShopEntryPoint)
					{
						break;
					}
					num += ((RectTransform)shopGroup.transform).rect.height;
				}
			}
			Vector2 vector = new Vector2(contentRectTransform.localPosition.x, num);
			if (scroolToPositon)
			{
				contentRectTransform.DOLocalMove(vector, 0.7f).SetUpdate(true);
			}
			else
			{
				contentRectTransform.localPosition = vector;
			}
			ShopEntryPoint = ShopGroupType.NotDefined;
		}

		private void OnClosedClicked()
		{
			Contexts.sharedInstance.uI.CreateEntity().isUIShopScreenExit = true;
		}
	}
}
