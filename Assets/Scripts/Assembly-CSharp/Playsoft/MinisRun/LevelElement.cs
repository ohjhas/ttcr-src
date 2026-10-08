using UnityEngine;

namespace Playsoft.MinisRun
{
	public class LevelElement : MonoBehaviour, ISerializationCallbackReceiver
	{
		public LevelElementData Data = new LevelElementData();

		private void OnDrawGizmos()
		{
			if (Data.Trigger.IsEnabled)
			{
				Color color = Gizmos.color;
				Gizmos.color = Color.black;
				Gizmos.color += ((!Data.Trigger.KillZone.IsEnabled) ? Color.black : Color.red);
				Gizmos.color += ((!Data.Trigger.CameraSize.IsEnabled) ? Color.black : Color.yellow);
				Gizmos.color += ((!Data.Trigger.SkinChange.IsEnabled) ? Color.black : Color.magenta);
				Gizmos.color += ((!Data.Trigger.SpeedChange.IsEnabled) ? Color.black : Color.blue);
				Gizmos.color += ((!Data.Trigger.LevelEnd.IsEnabled) ? Color.black : Color.green);
				Gizmos.color += ((!Data.Trigger.Tutorial.IsEnabled) ? Color.black : Color.cyan);
				float num = Mathf.Max(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b);
				Gizmos.color /= num;
				Gizmos.color = new Color(Gizmos.color.r, Gizmos.color.g, Gizmos.color.b, 0.5f);
				Gizmos.DrawCube(base.transform.position, GetComponent<BoxCollider2D>().size);
				Gizmos.color = color;
			}
		}

		public void OnAfterDeserialize()
		{
		}

		public void OnBeforeSerialize()
		{
			Data.Position = GetOriginalPosition();
		}

		public void InitWithData(LevelElementData elementData)
		{
			Data = elementData;
			base.name = Data.AssetName;
			base.transform.position = Data.Position;
			SetupSwingingIfNeeded();
		}

		public void SetAssetName(string assetName)
		{
			Data.AssetName = assetName;
		}

		public void FindParent()
		{
			string value = (Data.Platform.IsEnabled ? "Platforms" : (Data.Obstacle.IsEnabled ? "Obstacles" : ((!Data.Trigger.IsEnabled) ? "Collectable" : "Triggers")));
			if (base.transform.parent == null || !base.transform.parent.name.Equals(value))
			{
				GameObject gameObject = GameObject.Find(value);
				base.transform.SetParent((!(gameObject != null)) ? null : gameObject.transform);
			}
		}

		private void SetupSwingingIfNeeded()
		{
			if (Data.Obstacle.Swinging.IsEnabled)
			{
				BoxCollider2D componentInChildren = GetComponentInChildren<BoxCollider2D>();
				Vector3 vector = new Vector3(componentInChildren.size.x * (Data.Obstacle.Swinging.Anchor.x - 0.5f), componentInChildren.size.y * (Data.Obstacle.Swinging.Anchor.y - 0.5f));
				base.transform.position += vector;
				componentInChildren.transform.localPosition = -vector;
			}
		}

		private Vector3 GetOriginalPosition()
		{
			if (Data.Obstacle.Swinging.IsEnabled)
			{
				BoxCollider2D componentInChildren = GetComponentInChildren<BoxCollider2D>();
				if (componentInChildren != null)
				{
					return base.transform.position + componentInChildren.transform.localPosition;
				}
			}
			return base.transform.position;
		}
	}
}
