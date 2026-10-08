using Entitas.Unity;
using UnityEngine;

public class RaccoonOutroAnimationTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		EntityLink entityLink = collision.gameObject.GetEntityLink();
		if (!(entityLink == null) && entityLink.entity != null)
		{
			GameEntity gameEntity = (GameEntity)entityLink.entity;
			if (gameEntity != null && gameEntity.isPlayer)
			{
				gameEntity.isRaccoonOutro = true;
			}
		}
	}
}
