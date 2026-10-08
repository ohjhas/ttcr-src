using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ToggleSoundsButton : MonoBehaviour, IToggleSoundListener
	{
		[SerializeField]
		private List<ToggleImageData> imageData;

		private void Start()
		{
			Contexts.sharedInstance.uI.CreateEntity().AddToggleSoundsListener(this);
			HashSet<StateEntity> entitiesWithName = Contexts.sharedInstance.state.GetEntitiesWithName("Sounds");
			SetState(entitiesWithName.Count <= 0 || entitiesWithName.SingleEntity().boolean.Value);
		}

		public void OnToggleSounds()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isSoundsToggled = true;
		}

		public void SetState(bool soundsEnabled)
		{
			if (imageData == null)
			{
				return;
			}
			for (int i = 0; i < imageData.Count; i++)
			{
				if (imageData[i].destImage != null)
				{
					imageData[i].destImage.sprite = ((!soundsEnabled) ? imageData[i].inactiveState : imageData[i].activeState);
					imageData[i].destImage.gameObject.SetActive(imageData[i].destImage.sprite != null);
				}
			}
		}
	}
}
