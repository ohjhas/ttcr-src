using UnityEngine;
using UnityEngine.UI;

namespace I2.Loc
{
	public class LocalizeTarget_UnityUI_Image : LocalizeTarget<Image>
	{
		static LocalizeTarget_UnityUI_Image()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTarget_UnityUI_Image());
		}

		public override string GetName()
		{
			return "Image";
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

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			Image target = GetTarget(cmp);
			return (!(target.sprite == null)) ? eTermType.Sprite : eTermType.Texture;
		}

		public override eTermType GetSecondaryTermType(Localize cmp)
		{
			return eTermType.Text;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			Image target = GetTarget(cmp);
			primaryTerm = ((!target.mainTexture) ? string.Empty : target.mainTexture.name);
			if (target.sprite != null && target.sprite.name != primaryTerm)
			{
				primaryTerm = primaryTerm + "." + target.sprite.name;
			}
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Image target = GetTarget(cmp);
			Sprite sprite = target.sprite;
			if (sprite == null || sprite.name != mainTranslation)
			{
				target.sprite = cmp.FindTranslatedObject<Sprite>(mainTranslation);
			}
		}
	}
}
