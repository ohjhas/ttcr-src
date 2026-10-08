using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public abstract class ShopEntry : MonoBehaviour
	{
		[SerializeField]
		protected TextMeshProUGUI titleText;

		[SerializeField]
		protected TextMeshProUGUI costText;

		[SerializeField]
		protected TextMeshProUGUI amountText;

		[SerializeField]
		protected Image contentImage;

		[SerializeField]
		protected Button buyButton;

		public virtual void RemoveAllListeners()
		{
			buyButton.onClick.RemoveAllListeners();
		}

		public virtual void Disable()
		{
			buyButton.interactable = false;
		}
	}
}
