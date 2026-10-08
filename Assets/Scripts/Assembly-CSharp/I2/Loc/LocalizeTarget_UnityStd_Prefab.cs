using UnityEngine;

namespace I2.Loc
{
	public class LocalizeTarget_UnityStd_Prefab : LocalizeTarget<GameObject>
	{
		static LocalizeTarget_UnityStd_Prefab()
		{
			AutoRegister();
		}

		[I2RuntimeInitialize]
		private static void AutoRegister()
		{
			LocalizationManager.RegisterTarget(new LocalizeTarget_UnityStd_Prefab());
		}

		public override string GetName()
		{
			return "Prefab";
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
			if (cmp.mTarget as GameObject != null)
			{
				return true;
			}
			TermData termData = LocalizationManager.GetTermData(cmp.Term);
			return termData != null && termData.TermType == eTermType.GameObject;
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
			GameObject target = GetTarget(cmp);
			if ((bool)target && target.name == mainTranslation)
			{
				return;
			}
			Transform transform = cmp.transform;
			string text = mainTranslation;
			int num = mainTranslation.LastIndexOfAny(LanguageSource.CategorySeparators);
			if (num >= 0)
			{
				text = text.Substring(num + 1);
			}
			Transform transform2 = InstantiateNewPrefab(cmp, mainTranslation);
			if (transform2 == null)
			{
				return;
			}
			transform2.name = text;
			for (int num2 = transform.childCount - 1; num2 >= 0; num2--)
			{
				Transform child = transform.GetChild(num2);
				if (child != transform2)
				{
					Object.Destroy(child.gameObject);
				}
			}
		}

		private Transform InstantiateNewPrefab(Localize cmp, string mainTranslation)
		{
			GameObject gameObject = cmp.FindTranslatedObject<GameObject>(mainTranslation);
			if (gameObject == null)
			{
				return null;
			}
			GameObject gameObject2 = cmp.mTarget as GameObject;
			cmp.mTarget = Object.Instantiate(gameObject);
			if (cmp.mTarget == null)
			{
				return null;
			}
			Transform transform = cmp.transform;
			Transform transform2 = (cmp.mTarget as GameObject).transform;
			transform2.SetParent(transform);
			Transform transform3 = ((!gameObject2) ? transform : gameObject2.transform);
			transform2.rotation = transform3.rotation;
			transform2.position = transform3.position;
			return transform2;
		}

		public override bool CanLocalize(Localize cmp)
		{
			return cmp.transform.childCount > 0;
		}
	}
}
