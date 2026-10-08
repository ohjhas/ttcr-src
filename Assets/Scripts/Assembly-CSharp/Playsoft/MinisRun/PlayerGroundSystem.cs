using Entitas;
using Entitas.Unity;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerGroundSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private InputContext inputContext;

		public PlayerGroundSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			inputContext = contexts.input;
		}

		public void Execute()
		{
			if (!IsPlayerAvailable())
			{
				return;
			}
			GameObject gameObject = gameContext.playerEntity.view.gameObject;
			BoxCollider2D[] components = gameObject.GetComponents<BoxCollider2D>();
			Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
			if (!(component.velocity.y <= 0f))
			{
				return;
			}
			for (int i = 0; i < components.Length; i++)
			{
				if (!components[i].isTrigger)
				{
					Vector2 vector = new Vector2(components[i].size.x * 0.5f, 0f);
					Vector2[] originPos = new Vector2[3]
					{
						(Vector2)gameObject.transform.position + components[i].offset,
						(Vector2)gameObject.transform.position + components[i].offset - vector,
						(Vector2)gameObject.transform.position + components[i].offset + vector
					};
					float maxDistance = components[i].size.y * 0.6f;
					MakeRaycasts(originPos, Vector2.down, GetContactFilter(), maxDistance);
					break;
				}
			}
		}

		private void MakeRaycasts(Vector2[] originPos, Vector2 direction, ContactFilter2D filter, float maxDistance)
		{
			RaycastHit2D[] array = new RaycastHit2D[1];
			for (int i = 0; i < originPos.Length; i++)
			{
				if (Physics2D.Raycast(originPos[i], direction, filter, array, maxDistance) == 0)
				{
					continue;
				}
				GameEntity colliderEntity = GetColliderEntity(array[0]);
				if (colliderEntity != null && colliderEntity.isPlatform)
				{
					BoxCollider2D boxCollider2D = array[0].collider as BoxCollider2D;
					float num = boxCollider2D.transform.position.y + boxCollider2D.offset.y + boxCollider2D.size.y * 0.4f;
					if (num <= originPos[i].y)
					{
						AddGrounded(colliderEntity);
						return;
					}
				}
			}
			RemoveGrounded();
		}

		private ContactFilter2D GetContactFilter()
		{
			ContactFilter2D result = default(ContactFilter2D);
			result.SetLayerMask(new LayerMask
			{
				value = int.MaxValue - (1 << gameContext.playerEntity.view.gameObject.layer)
			});
			return result;
		}

		private GameEntity GetColliderEntity(RaycastHit2D info)
		{
			EntityLink entityLink = null;
			if (info.collider != null)
			{
				entityLink = info.collider.gameObject.GetEntityLink();
				if (entityLink == null)
				{
					info.collider.gameObject.GetComponentInParent<EntityLink>();
				}
			}
			return (!(entityLink != null)) ? null : (entityLink.entity as GameEntity);
		}

		private bool IsPlayerAvailable()
		{
			return gameContext.playerEntity != null && gameContext.playerEntity.hasView && !gameContext.playerEntity.isDead;
		}

		private void RemoveGrounded()
		{
			if (gameContext.playerEntity.hasGrounded)
			{
				gameContext.playerEntity.RemoveGrounded();
			}
		}

		private void AddGrounded(IEntity colliderEntity)
		{
			if (gameContext.playerEntity.hasJumping)
			{
				InputEntity inputEntity = inputContext.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.isLanding = true;
			}
			if (!gameContext.playerEntity.hasGrounded)
			{
				gameContext.playerEntity.AddGrounded(colliderEntity);
			}
			else if (!gameContext.playerEntity.grounded.ground.Equals(colliderEntity))
			{
				gameContext.playerEntity.ReplaceGrounded(colliderEntity);
			}
		}
	}
}
