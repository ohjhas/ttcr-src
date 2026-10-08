using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlaySoundAction : MonoBehaviour
	{
		public void PlaySound(string soundId)
		{
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect(soundId);
		}
	}
}
