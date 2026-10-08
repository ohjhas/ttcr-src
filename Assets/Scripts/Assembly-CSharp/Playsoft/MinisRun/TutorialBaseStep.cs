using UnityEngine;

namespace Playsoft.MinisRun
{
	public abstract class TutorialBaseStep : ScriptableObject
	{
		[SerializeField]
		private string id;

		[SerializeField]
		private string groupId;

		[SerializeField]
		private bool trackState;

		[SerializeField]
		private GroupRepeating repeating;

		[Space(5f)]
		[Header("Opotions")]
		[SerializeField]
		private string targetId;

		[SerializeField]
		private bool enableLockLayer;

		[SerializeField]
		private bool layerTransparent;

		[SerializeField]
		private bool moveTargetToTop;

		[SerializeField]
		private HandOptions handOptions;

		[SerializeField]
		private HighlightOptions highlightOptions;

		[Space(5f)]
		[Header("Triggers")]
		[SerializeField]
		private TutorialTrigger enableTrigger;

		[SerializeField]
		private TutorialTrigger disableTrigger;

		[Header("Actions")]
		[SerializeField]
		private TutorialAction[] enableActions;

		[SerializeField]
		private TutorialAction[] disableActions;

		public string Id
		{
			get
			{
				return id;
			}
		}

		public string GroupId
		{
			get
			{
				return groupId;
			}
		}

		public bool TrackState
		{
			get
			{
				return trackState;
			}
		}

		public GroupRepeating Repeating
		{
			get
			{
				return repeating;
			}
		}

		public string TargetId
		{
			get
			{
				return targetId;
			}
		}

		public bool EnableLockLayer
		{
			get
			{
				return enableLockLayer;
			}
		}

		public bool LayerTransparent
		{
			get
			{
				return layerTransparent;
			}
		}

		public bool MoveTargetToTop
		{
			get
			{
				return moveTargetToTop;
			}
		}

		public HandOptions HandOptions
		{
			get
			{
				return handOptions;
			}
		}

		public HighlightOptions HighlightOptions
		{
			get
			{
				return highlightOptions;
			}
		}

		public TutorialTrigger EnableTrigger
		{
			get
			{
				return enableTrigger;
			}
		}

		public TutorialTrigger DisableTrigger
		{
			get
			{
				return disableTrigger;
			}
		}

		public TutorialAction[] EnableActions
		{
			get
			{
				return enableActions;
			}
		}

		public TutorialAction[] DisableActions
		{
			get
			{
				return disableActions;
			}
		}
	}
}
