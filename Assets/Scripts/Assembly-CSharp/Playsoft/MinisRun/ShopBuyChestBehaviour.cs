using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ShopBuyChestBehaviour : TutorialElementBehaviour
	{
		[SerializeField]
		private ShopChestEntry chestEntry;

		protected override bool ShouldHighlight()
		{
			return TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.Shop) && chestEntry.Id == 1;
		}
	}
}
