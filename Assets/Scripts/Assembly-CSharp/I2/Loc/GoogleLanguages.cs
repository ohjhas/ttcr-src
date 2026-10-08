using System;
using System.Collections.Generic;
using UnityEngine;

namespace I2.Loc
{
	public static class GoogleLanguages
	{
		public struct LanguageCodeDef
		{
			public string Code;

			public string GoogleCode;

			public bool HasJoinedWords;

			public int PluralRule;
		}

		public static Dictionary<string, LanguageCodeDef> mLanguageDef = new Dictionary<string, LanguageCodeDef>(StringComparer.Ordinal)
		{
			{
				"Afrikaans",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "af"
				}
			},
			{
				"Albanian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sq"
				}
			},
			{
				"Arabic",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar"
				}
			},
			{
				"Arabic/Algeria",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-DZ",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Bahrain",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-BH",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Egypt",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-EG",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Iraq",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-IQ",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Jordan",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-JO",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Kuwait",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-KW",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Lebanon",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-LB",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Libya",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-LY",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Morocco",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-MA",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Oman",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-OM",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Qatar",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-QA",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Saudi Arabia",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-SA",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Syria",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-SY",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Tunisia",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-TN",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/U.A.E.",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-AE",
					GoogleCode = "ar"
				}
			},
			{
				"Arabic/Yemen",
				new LanguageCodeDef
				{
					PluralRule = 11,
					Code = "ar-YE",
					GoogleCode = "ar"
				}
			},
			{
				"Armenian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "hy"
				}
			},
			{
				"Azerbaijani",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "az"
				}
			},
			{
				"Basque",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "eu"
				}
			},
			{
				"Basque/Spain",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "eu-ES",
					GoogleCode = "eu"
				}
			},
			{
				"Belarusian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "be"
				}
			},
			{
				"Bosnian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "bs"
				}
			},
			{
				"Bulgariaa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "bg"
				}
			},
			{
				"Catalan",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ca"
				}
			},
			{
				"Chinese",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Hong Kong",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-HK",
					GoogleCode = "zh-TW",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Macau",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-MO",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/PRC",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-CN",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Simplified",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-CN",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Singapore",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-SG",
					GoogleCode = "zh-CN",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Taiwan",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-TW",
					GoogleCode = "zh-TW",
					HasJoinedWords = true
				}
			},
			{
				"Chinese/Traditional",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "zh-TW",
					GoogleCode = "zh-TW",
					HasJoinedWords = true
				}
			},
			{
				"Croatian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "hr"
				}
			},
			{
				"Croatian/Bosnia and Herzegovina",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "hr-BA",
					GoogleCode = "hr"
				}
			},
			{
				"Czech",
				new LanguageCodeDef
				{
					PluralRule = 7,
					Code = "cs"
				}
			},
			{
				"Danish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "da"
				}
			},
			{
				"Dutch",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nl"
				}
			},
			{
				"Dutch/Belgium",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nl-BE",
					GoogleCode = "nl"
				}
			},
			{
				"Dutch/Netherlands",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nl-NL",
					GoogleCode = "nl"
				}
			},
			{
				"English",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en"
				}
			},
			{
				"English/Australia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-AU",
					GoogleCode = "en"
				}
			},
			{
				"English/Belize",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-BZ",
					GoogleCode = "en"
				}
			},
			{
				"English/Canada",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-CA",
					GoogleCode = "en"
				}
			},
			{
				"English/Caribbean",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-CB",
					GoogleCode = "en"
				}
			},
			{
				"English/Ireland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-IE",
					GoogleCode = "en"
				}
			},
			{
				"English/Jamaica",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-JM",
					GoogleCode = "en"
				}
			},
			{
				"English/New Zealand",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-NZ",
					GoogleCode = "en"
				}
			},
			{
				"English/Republic of the Philippines",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-PH",
					GoogleCode = "en"
				}
			},
			{
				"English/South Africa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-ZA",
					GoogleCode = "en"
				}
			},
			{
				"English/Trinidad",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-TT",
					GoogleCode = "en"
				}
			},
			{
				"English/United Kingdom",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-GB",
					GoogleCode = "en"
				}
			},
			{
				"English/United States",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-US",
					GoogleCode = "en"
				}
			},
			{
				"English/Zimbabwe",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "en-ZW",
					GoogleCode = "en"
				}
			},
			{
				"Esperanto",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "eo"
				}
			},
			{
				"Estonian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "et"
				}
			},
			{
				"Faeroese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "fo",
					GoogleCode = "-"
				}
			},
			{
				"Filipino",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "tl"
				}
			},
			{
				"Finnish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "fi"
				}
			},
			{
				"French",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr"
				}
			},
			{
				"French/Belgium",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-BE",
					GoogleCode = "fr"
				}
			},
			{
				"French/Canada",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-CA",
					GoogleCode = "fr"
				}
			},
			{
				"French/France",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-FR",
					GoogleCode = "fr"
				}
			},
			{
				"French/Luxembourg",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-LU",
					GoogleCode = "fr"
				}
			},
			{
				"French/Principality of Monaco",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-MC",
					GoogleCode = "fr"
				}
			},
			{
				"French/Switzerland",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "fr-CH",
					GoogleCode = "fr"
				}
			},
			{
				"Galician",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gl"
				}
			},
			{
				"Galician/Spain",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gl-ES",
					GoogleCode = "gl"
				}
			},
			{
				"Georgian",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ka"
				}
			},
			{
				"German",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de"
				}
			},
			{
				"German/Austria",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-AT",
					GoogleCode = "de"
				}
			},
			{
				"German/Germany",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-DE",
					GoogleCode = "de"
				}
			},
			{
				"German/Liechtenstein",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-LI",
					GoogleCode = "de"
				}
			},
			{
				"German/Luxembourg",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-LU",
					GoogleCode = "de"
				}
			},
			{
				"German/Switzerland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "de-CH",
					GoogleCode = "de"
				}
			},
			{
				"Greek",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "el"
				}
			},
			{
				"Gujarati",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "gu"
				}
			},
			{
				"Hebrew",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "he",
					GoogleCode = "iw"
				}
			},
			{
				"Hindi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "hi"
				}
			},
			{
				"Hungarian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "hu"
				}
			},
			{
				"Icelandic",
				new LanguageCodeDef
				{
					PluralRule = 14,
					Code = "is"
				}
			},
			{
				"Indonesian",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "id"
				}
			},
			{
				"Irish",
				new LanguageCodeDef
				{
					PluralRule = 10,
					Code = "ga"
				}
			},
			{
				"Italian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "it"
				}
			},
			{
				"Italian/Italy",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "it-IT",
					GoogleCode = "it"
				}
			},
			{
				"Italian/Switzerland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "it-CH",
					GoogleCode = "it"
				}
			},
			{
				"Japanese",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ja",
					HasJoinedWords = true
				}
			},
			{
				"Kannada",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kn"
				}
			},
			{
				"Kazakh",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "kk"
				}
			},
			{
				"Korean",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ko"
				}
			},
			{
				"Kurdish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ku"
				}
			},
			{
				"Kyrgyz",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ky"
				}
			},
			{
				"Latin",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "la"
				}
			},
			{
				"Latvian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "lv"
				}
			},
			{
				"Lithuanian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "lt"
				}
			},
			{
				"Macedonian",
				new LanguageCodeDef
				{
					PluralRule = 13,
					Code = "mk"
				}
			},
			{
				"Malay",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ms"
				}
			},
			{
				"Malay/Brunei Darussalam",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ms-BN",
					GoogleCode = "ms"
				}
			},
			{
				"Malay/Malaysia",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "ms-MY",
					GoogleCode = "ms"
				}
			},
			{
				"Malayalam",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ml"
				}
			},
			{
				"Maltese",
				new LanguageCodeDef
				{
					PluralRule = 12,
					Code = "mt"
				}
			},
			{
				"Maori",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "mi"
				}
			},
			{
				"Marathi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "mr"
				}
			},
			{
				"Mongolian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "mn"
				}
			},
			{
				"Northern Sotho",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ns",
					GoogleCode = "st"
				}
			},
			{
				"Norwegian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nb",
					GoogleCode = "no"
				}
			},
			{
				"Norwegian/Nynorsk",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "nn",
					GoogleCode = "no"
				}
			},
			{
				"Pashto",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ps"
				}
			},
			{
				"Persian",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "fa"
				}
			},
			{
				"Polish",
				new LanguageCodeDef
				{
					PluralRule = 8,
					Code = "pl"
				}
			},
			{
				"Portuguese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "pt"
				}
			},
			{
				"Portuguese/Brazil",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "pt-BR",
					GoogleCode = "pt"
				}
			},
			{
				"Portuguese/Portugal",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "pt-PT",
					GoogleCode = "pt"
				}
			},
			{
				"Punjabi",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "pa"
				}
			},
			{
				"Quechua",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu",
					GoogleCode = "-"
				}
			},
			{
				"Quechua/Bolivia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu-BO",
					GoogleCode = "-"
				}
			},
			{
				"Quechua/Ecuador",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu-EC",
					GoogleCode = "-"
				}
			},
			{
				"Quechua/Peru",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "qu-PE",
					GoogleCode = "-"
				}
			},
			{
				"Rhaeto-Romanic",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "rm",
					GoogleCode = "ro"
				}
			},
			{
				"Romanian",
				new LanguageCodeDef
				{
					PluralRule = 4,
					Code = "ro"
				}
			},
			{
				"Russian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "ru"
				}
			},
			{
				"Russian/Republic of Moldova",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "ru-MO",
					GoogleCode = "ru"
				}
			},
			{
				"Serbian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "sr"
				}
			},
			{
				"Serbian/Bosnia and Herzegovina",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "sr-BA",
					GoogleCode = "sr"
				}
			},
			{
				"Serbian/Serbia and Montenegro",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "sr-SP",
					GoogleCode = "sr"
				}
			},
			{
				"Slovak",
				new LanguageCodeDef
				{
					PluralRule = 7,
					Code = "sk"
				}
			},
			{
				"Slovenian",
				new LanguageCodeDef
				{
					PluralRule = 9,
					Code = "sl"
				}
			},
			{
				"Spanish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es"
				}
			},
			{
				"Spanish/Argentina",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-AR",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Bolivia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-BO",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Castilian",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-ES",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Chile",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-CL",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Colombia",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-CO",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Costa Rica",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-CR",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Dominican Republic",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-DO",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Ecuador",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-EC",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/El Salvador",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-SV",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Guatemala",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-GT",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Honduras",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-HN",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Mexico",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-MX",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Nicaragua",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-NI",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Panama",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PA",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Paraguay",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PY",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Peru",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PE",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Puerto Rico",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-PR",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Spain",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es"
				}
			},
			{
				"Spanish/Uruguay",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-UY",
					GoogleCode = "es"
				}
			},
			{
				"Spanish/Venezuela",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "es-VE",
					GoogleCode = "es"
				}
			},
			{
				"Swahili",
				new LanguageCodeDef
				{
					Code = "sw"
				}
			},
			{
				"Swedish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sv"
				}
			},
			{
				"Swedish/Finland",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sv-FI",
					GoogleCode = "sv"
				}
			},
			{
				"Swedish/Sweden",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "sv-SE",
					GoogleCode = "sv"
				}
			},
			{
				"Tamil",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ta"
				}
			},
			{
				"Tatar",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "tt",
					GoogleCode = "-"
				}
			},
			{
				"Telugu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "te"
				}
			},
			{
				"Thai",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "th",
					HasJoinedWords = true
				}
			},
			{
				"Turkish",
				new LanguageCodeDef
				{
					PluralRule = 0,
					Code = "tr"
				}
			},
			{
				"Ukrainian",
				new LanguageCodeDef
				{
					PluralRule = 5,
					Code = "uk"
				}
			},
			{
				"Urdu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "ur"
				}
			},
			{
				"Uzbek",
				new LanguageCodeDef
				{
					PluralRule = 2,
					Code = "uz"
				}
			},
			{
				"Vietnamese",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "vi"
				}
			},
			{
				"Welsh",
				new LanguageCodeDef
				{
					PluralRule = 16,
					Code = "cy"
				}
			},
			{
				"Xhosa",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "xh"
				}
			},
			{
				"Yiddish",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "yi"
				}
			},
			{
				"Zulu",
				new LanguageCodeDef
				{
					PluralRule = 1,
					Code = "zu"
				}
			}
		};

		public static string GetLanguageCode(string Filter, bool ShowWarnings = false)
		{
			if (string.IsNullOrEmpty(Filter))
			{
				return string.Empty;
			}
			string[] filters = Filter.ToLowerInvariant().Split(" /(),".ToCharArray());
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (LanguageMatchesFilter(item.Key, filters))
				{
					return item.Value.Code;
				}
			}
			if (ShowWarnings)
			{
				Debug.Log(string.Format("Language '{0}' not recognized. Please, add the language code to GoogleTranslation.cs", Filter));
			}
			return string.Empty;
		}

		public static List<string> GetLanguagesForDropdown(string Filter, string CodesToExclude)
		{
			string[] filters = Filter.ToLowerInvariant().Split(" /(),".ToCharArray());
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (string.IsNullOrEmpty(Filter) || LanguageMatchesFilter(item.Key, filters))
				{
					string text = string.Concat("[" + item.Value.Code + "]");
					if (!CodesToExclude.Contains(text))
					{
						list.Add(item.Key + " " + text);
					}
				}
			}
			for (int num = list.Count - 2; num >= 0; num--)
			{
				string text2 = list[num].Substring(0, list[num].IndexOf(" ["));
				if (list[num + 1].StartsWith(text2))
				{
					list[num] = text2 + "/" + list[num];
					list.Insert(num + 1, text2 + "/");
				}
			}
			return list;
		}

		public static string GetClosestLanguage(string Filter)
		{
			if (string.IsNullOrEmpty(Filter))
			{
				return string.Empty;
			}
			string[] filters = Filter.ToLowerInvariant().Split(" /(),".ToCharArray());
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (LanguageMatchesFilter(item.Key, filters))
				{
					return item.Key;
				}
			}
			return string.Empty;
		}

		private static bool LanguageMatchesFilter(string Language, string[] Filters)
		{
			Language = Language.ToLowerInvariant();
			int i = 0;
			for (int num = Filters.Length; i < num; i++)
			{
				if (Filters[i] != string.Empty)
				{
					if (!Language.Contains(Filters[i].ToLower()))
					{
						return false;
					}
					Language = Language.Remove(Language.IndexOf(Filters[i]), Filters[i].Length);
				}
			}
			return true;
		}

		public static string GetFormatedLanguageName(string Language)
		{
			string empty = string.Empty;
			int num = Language.IndexOf(" [");
			if (num > 0)
			{
				Language = Language.Substring(0, num);
			}
			num = Language.IndexOf('/');
			if (num > 0)
			{
				empty = Language.Substring(0, num);
				if (Language == empty + "/" + empty)
				{
					return empty;
				}
				Language = Language.Replace("/", " (") + ")";
			}
			return Language;
		}

		public static string GetCodedLanguage(string Language, string code)
		{
			string languageCode = GetLanguageCode(Language);
			if (string.Compare(code, languageCode, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return Language;
			}
			return Language + " [" + code + "]";
		}

		public static void UnPackCodeFromLanguageName(string CodedLanguage, out string Language, out string code)
		{
			if (string.IsNullOrEmpty(CodedLanguage))
			{
				Language = string.Empty;
				code = string.Empty;
				return;
			}
			int num = CodedLanguage.IndexOf("[");
			if (num < 0)
			{
				Language = CodedLanguage;
				code = GetLanguageCode(Language);
			}
			else
			{
				Language = CodedLanguage.Substring(0, num).Trim();
				code = CodedLanguage.Substring(num + 1, CodedLanguage.IndexOf("]", num) - num - 1);
			}
		}

		public static string GetGoogleLanguageCode(string InternationalCode)
		{
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (InternationalCode == item.Value.Code)
				{
					if (item.Value.GoogleCode == "-")
					{
						return null;
					}
					return string.IsNullOrEmpty(item.Value.GoogleCode) ? InternationalCode : item.Value.GoogleCode;
				}
			}
			return InternationalCode;
		}

		public static List<string> GetAllInternationalCodes()
		{
			HashSet<string> hashSet = new HashSet<string>();
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				hashSet.Add(item.Value.Code);
			}
			return new List<string>(hashSet);
		}

		public static bool LanguageCode_HasJoinedWord(string languageCode)
		{
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (languageCode == item.Value.GoogleCode || languageCode == item.Value.Code)
				{
					return item.Value.HasJoinedWords;
				}
			}
			return false;
		}

		private static int GetPluralRule(string langCode)
		{
			if (langCode.Length > 2)
			{
				langCode = langCode.Substring(0, 2);
			}
			langCode = langCode.ToLower();
			foreach (KeyValuePair<string, LanguageCodeDef> item in mLanguageDef)
			{
				if (item.Value.Code == langCode)
				{
					return item.Value.PluralRule;
				}
			}
			return 0;
		}

		public static bool LanguageHasPluralType(string langCode, string pluralType)
		{
			switch (pluralType)
			{
			case "Plural":
			case "Zero":
			case "One":
				return true;
			default:
				switch (GetPluralRule(langCode))
				{
				case 3:
					return pluralType == "Two" || pluralType == "Few";
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
					return pluralType == "Few";
				case 9:
					return pluralType == "Two" || pluralType == "Few";
				case 10:
				case 11:
				case 15:
				case 16:
					return pluralType == "Two" || pluralType == "Few" || pluralType == "Many";
				case 12:
					return pluralType == "Few" || pluralType == "Many";
				case 13:
					return pluralType == "Two";
				default:
					return false;
				}
			}
		}

		public static ePluralType GetPluralType(string langCode, int n)
		{
			switch (n)
			{
			case 0:
				return ePluralType.Zero;
			case 1:
				return ePluralType.One;
			default:
				switch (GetPluralRule(langCode))
				{
				case 0:
					return ePluralType.Plural;
				case 1:
					return (n == 1) ? ePluralType.One : ePluralType.Plural;
				case 2:
					return (n <= 1) ? ePluralType.One : ePluralType.Plural;
				case 3:
				{
					int result4;
					switch (n)
					{
					case 1:
					case 11:
						result4 = 1;
						break;
					case 2:
					case 12:
						result4 = 2;
						break;
					default:
						result4 = ((!inRange(n, 3, 10) && !inRange(n, 13, 19)) ? 5 : 3);
						break;
					}
					return (ePluralType)result4;
				}
				case 4:
					return (n == 1) ? ePluralType.One : ((!inRange(n % 100, 1, 19)) ? ePluralType.Plural : ePluralType.Few);
				case 5:
					return (n % 10 == 1 && n % 100 != 11) ? ePluralType.One : ((n % 10 < 2 || (n % 100 >= 10 && n % 100 < 20)) ? ePluralType.Plural : ePluralType.Few);
				case 6:
					return (n % 10 == 1 && n % 100 != 11) ? ePluralType.One : ((!inRange(n % 10, 2, 4) || inRange(n % 100, 12, 14)) ? ePluralType.Plural : ePluralType.Few);
				case 7:
					return (n == 1) ? ePluralType.One : ((!inRange(n, 2, 4)) ? ePluralType.Plural : ePluralType.Few);
				case 8:
					return (n == 1) ? ePluralType.One : ((!inRange(n % 10, 2, 4) || inRange(n % 100, 12, 14)) ? ePluralType.Plural : ePluralType.Few);
				case 9:
					return (n % 100 == 1) ? ePluralType.One : ((n % 100 == 2) ? ePluralType.Two : ((!inRange(n % 100, 3, 4)) ? ePluralType.Plural : ePluralType.Few));
				case 10:
				{
					int result3;
					switch (n)
					{
					case 1:
						result3 = 1;
						break;
					case 2:
						result3 = 2;
						break;
					default:
						result3 = ((!inRange(n, 3, 6)) ? ((!inRange(n, 7, 10)) ? 5 : 4) : 3);
						break;
					}
					return (ePluralType)result3;
				}
				case 11:
				{
					int result2;
					switch (n)
					{
					case 0:
						result2 = 0;
						break;
					case 1:
						result2 = 1;
						break;
					case 2:
						result2 = 2;
						break;
					default:
						result2 = ((!inRange(n % 100, 3, 10)) ? ((n % 100 < 11) ? 5 : 4) : 3);
						break;
					}
					return (ePluralType)result2;
				}
				case 12:
					return (n == 1) ? ePluralType.One : (inRange(n % 100, 1, 10) ? ePluralType.Few : ((!inRange(n % 100, 11, 19)) ? ePluralType.Plural : ePluralType.Many));
				case 13:
					return (n % 10 == 1) ? ePluralType.One : ((n % 10 != 2) ? ePluralType.Plural : ePluralType.Two);
				case 14:
					return (n % 10 == 1 && n % 100 != 11) ? ePluralType.One : ePluralType.Plural;
				case 15:
					return (n % 10 == 1 && n % 100 != 11 && n % 100 != 71 && n % 100 != 91) ? ePluralType.One : ((n % 10 == 2 && n % 100 != 12 && n % 100 != 72 && n % 100 != 92) ? ePluralType.Two : (((n % 10 == 3 || n % 10 == 4 || n % 10 == 9) && n % 100 != 13 && n % 100 != 14 && n % 100 != 19 && n % 100 != 73 && n % 100 != 74 && n % 100 != 79 && n % 100 != 93 && n % 100 != 94 && n % 100 != 99) ? ePluralType.Few : ((n % 1000000 != 0) ? ePluralType.Plural : ePluralType.Many)));
				case 16:
				{
					int result;
					switch (n)
					{
					case 0:
						result = 0;
						break;
					case 1:
						result = 1;
						break;
					case 2:
						result = 2;
						break;
					case 3:
						result = 3;
						break;
					case 6:
						result = 4;
						break;
					default:
						result = 5;
						break;
					}
					return (ePluralType)result;
				}
				default:
					return ePluralType.Plural;
				}
			}
		}

		public static int GetPluralTestNumber(string langCode, ePluralType pluralType)
		{
			switch (pluralType)
			{
			case ePluralType.Zero:
				return 0;
			case ePluralType.One:
				return 1;
			case ePluralType.Few:
				return 3;
			case ePluralType.Many:
				switch (GetPluralRule(langCode))
				{
				case 10:
					return 8;
				case 11:
				case 12:
					return 13;
				case 15:
					return 1000000;
				default:
					return 6;
				}
			default:
				return 936;
			}
		}

		private static bool inRange(int amount, int min, int max)
		{
			return amount >= min && amount <= max;
		}
	}
}
