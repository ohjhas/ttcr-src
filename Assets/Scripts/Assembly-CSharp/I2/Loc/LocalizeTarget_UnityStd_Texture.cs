using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStd_Texture : LocalizeTarget<GUITexture>
	{
		static LocalizeTarget_UnityStd_Texture()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTarget_UnityStd_Texture());
		}

		public override string GetName()
		{
			return "GUITexture";
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Texture;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override bool CanUseSecondaryTerm()
		{
			return false;
		}

		public override bool AllowMainTermToBeRTL()
		{
			return false;
		}

		public override bool AllowSecondTermToBeRTL()
		{
			return false;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			GUITexture target = GetTarget(cmp);
			primaryTerm = ((!target.texture) ? string.Empty : target.texture.name);
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			GUITexture target = GetTarget(cmp);
			Texture texture = target.texture;
			if (texture != null && texture.name != mainTranslation)
			{
				target.texture = cmp.FindTranslatedObject<Texture>(mainTranslation);
			}
		}
	}
}
