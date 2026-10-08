using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "LandSkinSettingsSO", menuName = "SO/LandSkinSettingsSO")]
	public class LandSkinSettingsSO : ScriptableObject
	{
		public Sprite FrontLayer;

		public Sprite MiddleLayer;

		public Sprite BackLayer;
	}
}
