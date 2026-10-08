using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "SoundSettings", menuName = "SO/SoundSettings")]
	public class SettingsSoundsSO : ScriptableObject
	{
		[Header("Music")]
		public AudioClip OutgameMusic;

		public AudioClip IngameMusic;
	}
}
