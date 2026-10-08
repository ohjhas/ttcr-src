using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public static class O7SdkEventsHelper
	{
		public static string GetGDPR_P2_Event_Name(GDPRShowState state)
		{
			string result = string.Empty;
			switch (state)
			{
			case GDPRShowState.DontShow:
				result = string.Empty;
				break;
			case GDPRShowState.FirstShow:
				result = "initial";
				break;
			case GDPRShowState.NotFirstShow:
				result = "updated";
				break;
			case GDPRShowState.FromSettings:
				result = "settings";
				break;
			}
			return result;
		}

		public static string GetGDPR_P1_Event_Name(GDPRShowState state)
		{
			string result = string.Empty;
			switch (state)
			{
			case GDPRShowState.DontShow:
				result = string.Empty;
				break;
			case GDPRShowState.FirstShow:
			case GDPRShowState.NotFirstShow:
				result = "auto";
				break;
			case GDPRShowState.FromSettings:
				result = "manual";
				break;
			}
			return result;
		}

		public static string GetGDPRConsentProvidersJson()
		{
			string empty = string.Empty;
			StateEntity o7SdkEntity = Contexts.sharedInstance.state.o7SdkEntity;
			if (o7SdkEntity == null)
			{
				return empty;
			}
			if (o7SdkEntity.hasO7SdkGdprAdProviders)
			{
				List<AdProvider> providers = o7SdkEntity.o7SdkGdprAdProviders.providers.Where((AdProvider x) => x.IsConsentProvided).ToList();
				List<AdProvider> providers2 = o7SdkEntity.o7SdkGdprAdProviders.providers.Where((AdProvider x) => !x.IsConsentProvided).ToList();
				List<string> providers3 = GetProviders(providers);
				List<string> providers4 = GetProviders(providers2);
				return JsonUtility.ToJson(new LogAdProviders(providers3, providers4));
			}
			return empty;
		}

		private static List<string> GetProviders(List<AdProvider> providers)
		{
			List<string> list = new List<string>();
			foreach (AdProvider provider in providers)
			{
				list.Add(provider.displayName);
			}
			return list;
		}
	}
}
