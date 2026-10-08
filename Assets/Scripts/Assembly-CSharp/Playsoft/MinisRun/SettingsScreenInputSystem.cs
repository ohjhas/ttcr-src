using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class SettingsScreenInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public SettingsScreenInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.SettingsScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.isUIOpenContacts)
				{
					OpenContact();
				}
				else if (entity.isUIOpenPrivacyAndLegacy)
				{
					OpenPrivacyAndLegal();
				}
				else if (entity.isUIOpenEPrivacy)
				{
					OpenEPrivacy();
				}
				else if (entity.isUIOpenEula)
				{
					OpenEula();
				}
				else if (entity.isUIOpenPrivacyPolicy)
				{
					OpenPrivacyPolicy();
				}
				else if (entity.isUIOpenCountryCodes)
				{
					OpenCountryCodes();
				}
				else if (entity.isUIOpenGdpr)
				{
					OpenGdprScreen();
				}
				entity.isDestroyed = true;
			}
		}

		private void OpenContact()
		{
			string empty = string.Empty;
			empty = stateContext.balancingDataEntity.privacyAndLegacyBalancingData.Data.Contactadressandroid;
			stateContext.CreateEntity().AddOpenUrl(empty);
		}

		private void OpenPrivacyAndLegal()
		{
			uiContext.CloseDialog(ScreenId.SettingsScreen);
			uiContext.OpenDialog<PrivacyAndLegalPopup>();
		}

		private void OpenEPrivacy()
		{
			uiContext.CloseDialog(ScreenId.PrivacyAndLegacyPopup);
			uiContext.OpenDialog<EPrivacyPopup>();
		}

		private void OpenEula()
		{
			stateContext.CreateEntity().AddOpenUrl(stateContext.balancingDataEntity.privacyAndLegacyBalancingData.Data.Eulaadress);
		}

		private void OpenPrivacyPolicy()
		{
			stateContext.CreateEntity().isOpenPrivacyUrl = true;
		}

		private void OpenCountryCodes()
		{
			uiContext.CloseDialog(ScreenId.PrivacyAndLegacyPopup);
			uiContext.OpenDialog<CountryCodesPopup>();
		}

		private void OpenGdprScreen()
		{
			uiContext.CloseDialog(ScreenId.SettingsScreen);
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprShowDecider = true;
			stateEntity.isO7SdkGdprShowFromSetting = true;
		}
	}
}
