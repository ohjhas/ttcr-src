using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ToggleMusicButton : MonoBehaviour, IToggleMusicListener
	{
		[SerializeField]
		private List<ToggleImageData> imageData;

		private void Start()
		{
			Contexts.sharedInstance.uI.CreateEntity().AddToggleMusicsListener(this);
			HashSet<StateEntity> entitiesWithName = Contexts.sharedInstance.state.GetEntitiesWithName("Music");
			SetState(entitiesWithName.Count <= 0 || entitiesWithName.SingleEntity().boolean.Value);
		}

		public void OnToggleMusic()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isMusicToggled = true;
		}

		public void SetState(bool musicEnabled)
		{
			for (int i = 0; i < imageData.Count; i++)
			{
				imageData[i].destImage.sprite = ((!musicEnabled) ? imageData[i].inactiveState : imageData[i].activeState);
				imageData[i].destImage.gameObject.SetActive(imageData[i].destImage.sprite != null);
			}
		}
	}
}
