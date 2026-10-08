using UnityEngine;

public class CloudBehaviour : MonoBehaviour, IPoolSpawnedListener
{
	[SerializeField]
	private float distanceFade;

	private static readonly Color fadeColor = new Color(1f, 1f, 1f, 0f);

	private SpriteRenderer spriteRenderer;

	public void OnSpawned(GameEntity e)
	{
		spriteRenderer.color = fadeColor;
	}

	private void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void FixedUpdate()
	{
		GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
		if (playerEntity != null && playerEntity.hasView)
		{
			float t = Mathf.Abs(base.transform.position.x - playerEntity.view.gameObject.transform.position.x) / distanceFade;
			spriteRenderer.color = Color.Lerp(Color.white, fadeColor, t);
		}
	}
}
