using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace I2.Loc
{
	public static class GoogleTranslation
	{
		private static List<WWW> mCurrentTranslations = new List<WWW>();

		public static bool CanTranslate()
		{
			return LocalizationManager.Sources.Count > 0 && !string.IsNullOrEmpty(LocalizationManager.GetWebServiceURL());
		}

		public static void Translate(string text, string LanguageCodeFrom, string LanguageCodeTo, Action<string, string> OnTranslationReady)
		{
			Dictionary<string, TranslationQuery> queries = new Dictionary<string, TranslationQuery>();
			LanguageCodeTo = GoogleLanguages.GetGoogleLanguageCode(LanguageCodeTo);
			if (LanguageCodeTo == LanguageCodeFrom)
			{
				OnTranslationReady(text, null);
				return;
			}
			if (string.IsNullOrEmpty(LanguageCodeTo))
			{
				OnTranslationReady(string.Empty, null);
				return;
			}
			CreateQueries(text, LanguageCodeFrom, LanguageCodeTo, queries);
			Translate(queries, delegate(Dictionary<string, TranslationQuery> results, string error)
			{
				if (!string.IsNullOrEmpty(error) || results.Count == 0)
				{
					OnTranslationReady(null, error);
				}
				else
				{
					string arg = RebuildTranslation(text, queries, LanguageCodeTo);
					OnTranslationReady(arg, null);
				}
			});
		}

		public static string ForceTranslate(string text, string LanguageCodeFrom, string LanguageCodeTo)
		{
			Dictionary<string, TranslationQuery> dictionary = new Dictionary<string, TranslationQuery>();
			AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dictionary);
			WWW translationWWW = GetTranslationWWW(dictionary);
			while (!translationWWW.isDone)
			{
			}
			if (!string.IsNullOrEmpty(translationWWW.error))
			{
				return string.Empty;
			}
			byte[] bytes = translationWWW.bytes;
			string text2 = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
			if (text2.StartsWith("<!DOCTYPE html>") || text2.StartsWith("<HTML>"))
			{
				return string.Empty;
			}
			return text2;
		}

		public static void CreateQueries(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!text.Contains("[i2p_"))
			{
				AddQuery(text, LanguageCodeFrom, LanguageCodeTo, dict);
				return;
			}
			int num = 0;
			int num2 = text.IndexOf("[i2p_");
			if (num2 == 0)
			{
				num = text.IndexOf("]", num2) + 1;
				num2 = text.IndexOf("[i2p_");
				if (num2 < 0)
				{
					num2 = text.Length;
				}
			}
			string text2 = text.Substring(num, num2 - num);
			Regex regex = new Regex("{\\[(.*?)\\]}");
			for (ePluralType ePluralType2 = ePluralType.Zero; ePluralType2 <= ePluralType.Plural; ePluralType2++)
			{
				if (GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, ePluralType2.ToString()))
				{
					string input = text2;
					input = regex.Replace(input, GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType2).ToString());
					AddQuery(input, LanguageCodeFrom, LanguageCodeTo, dict);
				}
			}
		}

		private static void AddQuery(string text, string LanguageCodeFrom, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!dict.ContainsKey(text))
			{
				dict[text] = new TranslationQuery
				{
					Text = text,
					LanguageCode = LanguageCodeFrom,
					TargetLanguagesCode = new string[1] { LanguageCodeTo }
				};
				return;
			}
			TranslationQuery value = dict[text];
			if (Array.IndexOf(value.TargetLanguagesCode, LanguageCodeTo) < 0)
			{
				value.TargetLanguagesCode = value.TargetLanguagesCode.Concat(new string[1] { LanguageCodeTo }).Distinct().ToArray();
			}
			dict[text] = value;
		}

		public static string RebuildTranslation(string text, Dictionary<string, TranslationQuery> dict, string LanguageCodeTo)
		{
			if (!text.Contains("[i2p_"))
			{
				return GetTranslation(text, LanguageCodeTo, dict);
			}
			int num = 0;
			int num2 = text.IndexOf("[i2p_");
			if (num2 == 0)
			{
				num = text.IndexOf("]", num2) + 1;
				num2 = text.IndexOf("[i2p_");
				if (num2 < 0)
				{
					num2 = text.Length;
				}
			}
			string text2 = text.Substring(num, num2 - num);
			Match match = Regex.Match(text2, "{\\[(.*?)\\]}");
			string text3 = ((match != null) ? match.Value : string.Empty);
			StringBuilder stringBuilder = new StringBuilder();
			string text4 = text2;
			int pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType.Plural);
			text4 = text4.Replace(text3, pluralTestNumber.ToString());
			string translation = GetTranslation(text4, LanguageCodeTo, dict);
			string text5 = translation.Replace(pluralTestNumber.ToString(), text3);
			stringBuilder.Append(text5);
			for (ePluralType ePluralType2 = ePluralType.Zero; ePluralType2 < ePluralType.Plural; ePluralType2++)
			{
				if (GoogleLanguages.LanguageHasPluralType(LanguageCodeTo, ePluralType2.ToString()))
				{
					text4 = text2;
					pluralTestNumber = GoogleLanguages.GetPluralTestNumber(LanguageCodeTo, ePluralType2);
					text4 = text4.Replace(text3, pluralTestNumber.ToString());
					translation = GetTranslation(text4, LanguageCodeTo, dict);
					translation = translation.Replace(pluralTestNumber.ToString(), text3);
					if (!string.IsNullOrEmpty(translation) && translation != text5)
					{
						stringBuilder.Append("[i2p_");
						stringBuilder.Append(ePluralType2.ToString());
						stringBuilder.Append(']');
						stringBuilder.Append(translation);
					}
				}
			}
			return stringBuilder.ToString();
		}

		private static string GetTranslation(string text, string LanguageCodeTo, Dictionary<string, TranslationQuery> dict)
		{
			if (!dict.ContainsKey(text))
			{
				return null;
			}
			TranslationQuery translationQuery = dict[text];
			int num = Array.IndexOf(translationQuery.TargetLanguagesCode, LanguageCodeTo);
			if (num < 0)
			{
				return string.Empty;
			}
			if (translationQuery.Results == null)
			{
				return string.Empty;
			}
			return translationQuery.Results[num];
		}

		public static string UppercaseFirst(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			char[] array = s.ToLower().ToCharArray();
			array[0] = char.ToUpper(array[0]);
			return new string(array);
		}

		public static string TitleCase(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return string.Empty;
			}
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(s);
		}

		public static void Translate(Dictionary<string, TranslationQuery> requests, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady, bool usePOST = true)
		{
			WWW translationWWW = GetTranslationWWW(requests, usePOST);
			CoroutineManager.Start(WaitForTranslation(translationWWW, OnTranslationReady, requests));
		}

		public static WWW GetTranslationWWW(Dictionary<string, TranslationQuery> requests, bool usePOST = true)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, TranslationQuery> request in requests)
			{
				TranslationQuery value = request.Value;
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("<I2Loc>");
				}
				stringBuilder.Append(value.LanguageCode);
				stringBuilder.Append(":");
				for (int i = 0; i < value.TargetLanguagesCode.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append(value.TargetLanguagesCode[i]);
				}
				stringBuilder.Append("=");
				string text = ((!(TitleCase(value.Text) == value.Text)) ? request.Key : request.Key.ToLowerInvariant());
				if (usePOST)
				{
					stringBuilder.Append(text);
					continue;
				}
				stringBuilder.Append(Uri.EscapeUriString(text));
				if (stringBuilder.Length <= 4000)
				{
					continue;
				}
				break;
			}
			if (usePOST)
			{
				WWWForm wWWForm = new WWWForm();
				wWWForm.AddField("action", "Translate");
				wWWForm.AddField("list", stringBuilder.ToString());
				return new WWW(LocalizationManager.GetWebServiceURL(), wWWForm);
			}
			return new WWW(string.Format("{0}?action=Translate&list={1}", LocalizationManager.GetWebServiceURL(), stringBuilder.ToString()));
		}

		private static IEnumerator WaitForTranslation(WWW www, Action<Dictionary<string, TranslationQuery>, string> OnTranslationReady, Dictionary<string, TranslationQuery> requests)
		{
			mCurrentTranslations.Add(www);
			while (!www.isDone)
			{
				yield return null;
			}
			int numWWW = mCurrentTranslations.Count;
			mCurrentTranslations.Remove(www);
			if (numWWW == mCurrentTranslations.Count)
			{
				yield break;
			}
			string errorMsg = www.error;
			if (!string.IsNullOrEmpty(errorMsg))
			{
				if (errorMsg.Contains("necessary data rewind wasn't possible"))
				{
					Translate(requests, OnTranslationReady, false);
				}
				else
				{
					OnTranslationReady(requests, www.error);
				}
			}
			else
			{
				byte[] bytes = www.bytes;
				string html = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
				errorMsg = ParseTranslationResult(html, requests);
				OnTranslationReady(requests, errorMsg);
			}
		}

		public static string ParseTranslationResult(string html, Dictionary<string, TranslationQuery> requests)
		{
			if (html.StartsWith("<!DOCTYPE html>") || html.StartsWith("<HTML>"))
			{
				if (html.Contains("The script completed but did not return anything"))
				{
					return "The current Google WebService is not supported.\nPlease, delete the WebService from the Google Drive and Install the latest version.";
				}
				if (html.Contains("Service invoked too many times in a short time"))
				{
					return string.Empty;
				}
				return "There was a problem contacting the WebService. Please try again later\n" + html;
			}
			string[] array = html.Split(new string[1] { "<I2Loc>" }, StringSplitOptions.None);
			string[] separator = new string[1] { "<i2>" };
			int num = 0;
			string[] array2 = requests.Keys.ToArray();
			string[] array3 = array2;
			foreach (string text in array3)
			{
				TranslationQuery value = requests[text];
				value.Results = array[num++].Split(separator, StringSplitOptions.None);
				if (TitleCase(text) == text)
				{
					for (int j = 0; j < value.Results.Length; j++)
					{
						value.Results[j] = TitleCase(value.Results[j]);
					}
				}
				requests[text] = value;
			}
			return null;
		}

		public static bool IsTranslating()
		{
			return mCurrentTranslations.Count > 0;
		}

		public static void CancelCurrentGoogleTranslations()
		{
			mCurrentTranslations.Clear();
		}
	}
}
