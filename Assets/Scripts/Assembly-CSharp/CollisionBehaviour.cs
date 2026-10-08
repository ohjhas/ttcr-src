using Entitas.Unity;
using UnityEngine;

public class CollisionBehaviour : MonoBehaviour
{
	private void OnCollisionEnter2D(Collision2D collision)
	{
		EntityLink entityLink = base.gameObject.GetEntityLink();
		EntityLink entityLink2 = collision.gameObject.GetEntityLink();
		if (entityLink != null && entityLink2 != null)
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.AddCollision(entityLink.entity, entityLink2.entity);
		}
	}
}
