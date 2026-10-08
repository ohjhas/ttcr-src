using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ToggleAdTrackingButton : MonoBehaviour, IO7SdkAdTrackingListener
	{
		[SerializeField]
		private List<ToggleImageData> imageData;

		private void Start()
		{
			Contexts.sharedInstance.uI.CreateEntity().AddO7SdkAdTrackingListener(this);
			HashSet<StateEntity> entitiesWithName = Contexts.sharedInstance.state.GetEntitiesWithName("AdTrackingEnabled");
			UpdateAdTrackingButton(entitiesWithName.Count <= 0 || entitiesWithName.SingleEntity().boolean.Value);
		}

		public void UpdateAdTrackingButton(bool isTrackingEnabled)
		{
			for (int i = 0; i < imageData.Count; i++)
			{
				imageData[i].destImage.sprite = ((!isTrackingEnabled) ? imageData[i].inactiveState : imageData[i].activeState);
				imageData[i].destImage.gameObject.SetActive(imageData[i].destImage.sprite != null);
			}
		}

		public void OnToggleTracking()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.isO7SdkAdTracking = true;
		}
	}
}
