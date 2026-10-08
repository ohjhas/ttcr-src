namespace Playsoft.MinisRun
{
	public static class O7SdkEventsConstants
	{
		public const string EID_CHEST = "chest";

		public const string EID_CHAR_UPGRADE = "character-upgrade";

		public const string EID_COMP_UPGRADE = "companion-upgrade";

		public const string EID_RUN = "run";

		public const string EID_FINISH = "finish";

		public const string EID_PARTS_RECEIVED = "parts-received";

		public const string EID_SCREEN_ENTER = "screen-enter";

		public const string EID_SCREEN_EXIT = "screen-exit";

		public const string EID_CONTINUE_RUN = "continue-run";

		public const string EID_COMPLETE = "complete";

		public const string EID_IAP = "iap";

		public const string EID_FIRST_RUN = "first-run";

		public const string EID_SHARING_QUESTION = "sharing-question";

		public const string EID_GENDER_QUESTION = "gender-question";

		public const string EID_RESTORE = "restore";

		public const string EID_PRIVACY_POLICY_CLICK = "privacy-policy-click";

		public const string EID_EULA_CLICK = "eula-click";

		public const string EID_CONSENT_QUESTION = "consent-question";

		public const string EID_CONSENT_DECISION = "consent-decision";

		public const string EID_PREFERENCES_QUESTION = "preferences-question";

		public const string EID_PREFERENCES_DECISION = "preferences-decision";

		public const string GID_GC = "gc";

		public const string GID_RUN = "run";

		public const string GID_CHARACTER_INFO = "character-info";

		public const string GID_COMPANION_INFO = "companion-info";

		public const string GID_SCREEN = "screen";

		public const string GID_SCREEN_IAP = "screen-iap";

		public const string GID_SCREEN_RATE_THIS_APP = "screen-rate-this-app";

		public const string GID_VIDEO_OFFERS = "video-offers";

		public const string GID_IAP_CLICK = "iap-click";

		public const string GID_SESSION = "session";

		public const string GID_APP_FEATURES = "app-features";

		public const string GID_USER_STATE = "user-state";

		public const string GID_DATA_PROTECTION = "data-protection";

		public const string RESTORE_TYPE_UID = "US_UID";

		public const string RESTORE_NEW_SELECTED = "new";

		public const string RESTORE_PREVIOUS_SELECTED = "restored";

		public const string SOFT_CURRENCY_NAME = "coins";

		public const string HARD_CURRENCY_NAME = "diamonds";

		public const string SOURCE_BUTTON_REPLAY = "replay";

		public const string SOURCE_BUTTON_NEXT = "new";

		public const string FINISH_TYPE_CANCEL = "cancel";

		public const string FINISH_TYPE_WIN = "win";

		public const string FINISH_TYPE_LOSE = "lose";

		public const string UNDEFINED_VALUE = "undefined";

		public static string GetRarityName(ChestType chestType, Rarity rarity)
		{
			if (chestType == ChestType.Shop)
			{
				if (rarity == Rarity.Common)
				{
					return "basic";
				}
				return rarity.ToString().ToLower();
			}
			return chestType.ToString().ToLower();
		}
	}
}
