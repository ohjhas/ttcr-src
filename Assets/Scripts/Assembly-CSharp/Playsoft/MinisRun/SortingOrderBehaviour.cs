using UnityEngine;

namespace Playsoft.MinisRun
{
	public class SortingOrderBehaviour : MonoBehaviour, IPoolSpawnedListener
	{
		private SpriteRenderer spriteRenderer;

		private int order;

		private int initialOrder;

		private GameEntity entity;

		private void Awake()
		{
			spriteRenderer = GetComponent<SpriteRenderer>();
			if (spriteRenderer == null)
			{
				spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			}
		}

		public void OnSpawned(GameEntity e)
		{
			if (e.isObstacle)
			{
				SetOrderBasingOnPos(e, 0, -999, e.position.Value, 20f, -10f, 30f, 0.1f);
			}
			else if (e.isPlatform)
			{
				SetOrderBasingOnPos(e, -1000, -1999, e.position.Value, 20f, -10f, 30f, 0.1f);
			}
			else if (e.isCollectibleGiantToken || e.isCollectibleMagnet || e.isCollectibleScoreMultiplier || e.isCollectibleSpeedUp || e.isCollectibleUpgradeScore)
			{
				SetOrderBasingOnPos(e, -2000, -2999, e.position.Value, 20f, -10f, 30f, 0.1f);
			}
			else if (e.isCollectible)
			{
				SetOrderBasingOnPos(e, -3000, -3999, e.position.Value, 20f, -10f, 30f, 0.1f);
			}
		}

		private void SetOrderBasingOnPos(GameEntity e, int topMargin, int bottomMargin, Vector3 position, float maxY, float minY, float maxX, float xToYRatio)
		{
			initialOrder = spriteRenderer.sortingOrder;
			entity = e;
			float value = Mathf.Clamp(position.y, minY, maxY);
			float num = Mathf.InverseLerp(minY, maxY, value);
			float num2 = position.x % maxX / maxX;
			float t = num + xToYRatio * num2;
			order = (int)Mathf.Lerp(bottomMargin, topMargin, t);
			spriteRenderer.sortingOrder = order;
		}

		private void Update()
		{
			spriteRenderer.sortingOrder = order;
			if (entity != null && entity.isRaccoonDropAnimation)
			{
				spriteRenderer.sortingOrder = initialOrder;
			}
		}
	}
}
