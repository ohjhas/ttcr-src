using Entitas;

namespace Playsoft.MinisRun
{
	public class UIPreloadAssetsSystem : IInitializeSystem, ISystem
	{
		public UIPreloadAssetsSystem(Contexts contexts)
		{
		}

		public void Initialize()
		{
			AssetsSettingsSO assetsSettingsSO = SettingsService.AssetsSettingsSO;
			SettingsSO settingsSO = SettingsService.SettingsSO;
		}
	}
}
