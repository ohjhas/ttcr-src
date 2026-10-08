using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStd_Child : LocalizeTarget<GameObject>
	{
		static LocalizeTarget_UnityStd_Child()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTarget_UnityStd_Child());
		}

		public override string GetName()
		{
			return "Child";
		}

		public override eTermType GetPrimaryTermType(Localize cmp)
		{
			return eTermType.GameObject;
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

		public override bool FindTarget(Localize cmp)
		{
			TermData termData = LocalizationManager.GetTermData(cmp.Term);
			return termData != null && termData.TermType == eTermType.Child;
		}

		public override bool HasTarget(Localize cmp)
		{
			return true;
		}

		public override void GetFinalTerms(Localize cmp, string Main, string Secondary, out string primaryTerm, out string secondaryTerm)
		{
			GameObject target = GetTarget(cmp);
			primaryTerm = target.name;
			secondaryTerm = null;
		}

		public override void DoLocalize(Localize cmp, string mainTranslation, string secondaryTranslation)
		{
			Transform transform = cmp.transform;
			string text = mainTranslation;
			int num = mainTranslation.LastIndexOfAny(LanguageSource.CategorySeparators);
			if (num >= 0)
			{
				text = text.Substring(num + 1);
			}
			for (int i = 0; i < transform.childCount; i++)
			{
				Transform child = transform.GetChild(i);
				child.gameObject.SetActive(child.name == text);
			}
		}

		public override bool CanLocalize(Localize cmp)
		{
			return cmp.transform.childCount > 0;
		}
	}
}
