using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CandyStoreBehaviour : MonoBehaviour
	{
		[SerializeField]
		private float Speed;

		[SerializeField]
		private int FrontOrder;

		[SerializeField]
		private int BackOrder;

		[SerializeField]
		private SpriteRenderer[] Parts;

		public void Update()
		{
			if (ShouldMove())
			{
				base.transform.position += Speed * Time.deltaTime * Vector3.left;
			}
		}

		public void SetOrderOfParts(bool[] isFront)
		{
			for (int i = 0; i < Parts.Length; i++)
			{
				Parts[i].sortingOrder = ((!isFront[i]) ? BackOrder : FrontOrder);
			}
		}

		private bool ShouldMove()
		{
			GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			if (worldEntity == null || worldEntity.world.Distance <= 0f || playerEntity == null || playerEntity.isDead || playerEntity.hasLevelFinished)
			{
				return false;
			}
			return true;
		}
	}
}
