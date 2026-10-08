using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class UDIDDisplay : MonoBehaviour, IDisplayUDIDListener
	{
		[SerializeField]
		private TextMeshProUGUI udidLabel;

		private UIEntity listenerEntity;

		private UIContext uiContext
		{
			get
			{
				return Contexts.sharedInstance.uI;
			}
		}

		private void OnEnable()
		{
			udidLabel.text = string.Empty;
			if (listenerEntity == null)
			{
				listenerEntity = uiContext.CreateEntity();
			}
			listenerEntity.ReplaceDisplayUDIDListener(this);
		}

		private void OnDisable()
		{
			if (listenerEntity != null)
			{
				if (listenerEntity.hasDisplayUDIDListener)
				{
					listenerEntity.RemoveDisplayUDIDListener();
				}
				listenerEntity.isDestroyed = true;
				listenerEntity = null;
			}
		}

		private void OnDestroy()
		{
			if (listenerEntity != null)
			{
				if (listenerEntity.hasDisplayUDIDListener)
				{
					listenerEntity.RemoveDisplayUDIDListener();
				}
				listenerEntity.isDestroyed = true;
				listenerEntity = null;
			}
		}

		public void Toggle()
		{
			StateEntity o7SdkEntity = Contexts.sharedInstance.state.o7SdkEntity;
			if (string.IsNullOrEmpty(udidLabel.text))
			{
				udidLabel.text = ((!o7SdkEntity.hasUDIDHash) ? string.Empty : o7SdkEntity.uDIDHash.UDID);
			}
			else
			{
				udidLabel.text = string.Empty;
			}
		}
	}
}
