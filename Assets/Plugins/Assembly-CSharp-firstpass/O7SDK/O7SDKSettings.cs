using System;
using UnityEngine;

namespace O7SDK
{
	[CreateAssetMenu(fileName = "O7SDKSettings", menuName = "O7SDK Settings")]
	public class O7SDKSettings : ScriptableObject
	{
		public enum BuildType
		{
			Devel = 0,
			Prod = 1,
			RC = 2
		}

		[Serializable]
		private struct Builds
		{
			[SerializeField]
			public BuildSettings Devel;

			[SerializeField]
			public BuildSettings Prod;

			[SerializeField]
			public BuildSettings RC;
		}

		[Serializable]
		public struct BuildSettings
		{
			[SerializeField]
			public bool debugMode;

			[SerializeField]
			public bool developmentBackedn;

			[SerializeField]
			public bool logging;

			[SerializeField]
			public BuildType buildType;
		}

		[Serializable]
		public struct iOSSettings
		{
			[SerializeField]
			public string appId;

			[SerializeField]
			public string urlShema;
		}

		[Serializable]
		public struct RestoreStateSettings
		{
			[SerializeField]
			public string endpoint;

			[SerializeField]
			public string signatureMagic;
		}

		private const string SETTING_ASSET = "O7SDKSettings";

		[SerializeField]
		public iOSSettings iOS;

		[SerializeField]
		public RestoreStateSettings restoreState;

		[SerializeField]
		private Builds builds;

		public BuildSettings build
		{
			get
			{
				return builds.RC;
			}
		}

		public static O7SDKSettings GetSettings()
		{
			O7SDKSettings o7SDKSettings = Resources.Load<O7SDKSettings>("O7SDKSettings");
			if (o7SDKSettings == null)
			{
				throw new InvalidOperationException(string.Format("SDK settings not found by path {0}", "O7SDKSettings"));
			}
			return o7SDKSettings;
		}
	}
}
