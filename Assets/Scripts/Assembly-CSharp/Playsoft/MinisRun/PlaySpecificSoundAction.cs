using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlaySpecificSoundAction : MonoBehaviour
	{
		[SerializeField]
		private string soundId;

		public void PlaySound()
		{
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect(soundId);
		}
	}
}
