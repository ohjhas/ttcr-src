using Entitas.Unity;
using UnityEngine;

public class TriggerEnterBehaviour : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		EntityLink entityLink = base.gameObject.GetEntityLink();
		EntityLink entityLink2 = collision.gameObject.GetEntityLink();
		if (entityLink2 == null)
		{
			entityLink2 = collision.gameObject.GetComponentInParent<EntityLink>();
		}
		if (entityLink != null && entityLink2 != null)
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.AddCollision(entityLink.entity, entityLink2.entity);
			inputEntity.isCollisionTrigger = true;
		}
	}
}
