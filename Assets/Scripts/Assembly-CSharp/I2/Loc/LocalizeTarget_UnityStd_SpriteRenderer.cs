using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStd_SpriteRenderer : LocalizeTarget<SpriteRenderer>
	{
		static LocalizeTarget_UnityStd_SpriteRenderer()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTarget_UnityStd_SpriteRenderer());
		}

		public override string GetName()
		{
			return "SpriteRenderer";
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.Sprite;
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
			SpriteRenderer target = GetTarget(cmp);
			primaryTerm = ((!(target.sprite != null)) ? string.Empty : target.sprite.name);
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			SpriteRenderer target = GetTarget(cmp);
			Sprite sprite = target.sprite;
			if (sprite == null || sprite.name != mainTranslation)
			{
				target.sprite = cmp.FindTranslatedObject<Sprite>(mainTranslation);
			}
		}
	}
}
