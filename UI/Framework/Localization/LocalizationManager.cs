using System;
using System.Collections.Generic;
using System.Linq;
using FlatAppStore.UI.Framework.Assets;

namespace FlatAppStore.UI.Framework.Localization
{
	public static class LocalizationManager
	{
		public static Language CurrentLanguage { get; private set; }

		public static event Action<Language> OnCurrentLanguageChanged;

		public static IEnumerable<string> Languages { get => languages.Keys; }
		private static readonly Dictionary<string, Language> languages = new Dictionary<string, Language>();

		public static void LoadLanguages()
		{
			foreach (var asset in AssetLoader.EnumerateAssetFolder("/Assets/Localization")) // Loop through each asset in the localization folder
			{
				if (asset.EndsWith(".lang")) // If it is a language file
				{
					var language = AssetLoader.LoadLanguage(asset);
					languages.Add(language.Name, language);
				}
			}

			SetCurrentLanguage(Languages.First());
		}

		public static string GetLocalizedString(string strName)
		{
			return CurrentLanguage.Localizations[strName];
		}

		public static void SetCurrentLanguage(string language)
		{
			if (languages.ContainsKey(language))
			{
				CurrentLanguage = languages[language];
				OnCurrentLanguageChanged?.Invoke(CurrentLanguage);
			}
			else
			{
				throw new Exception("Unable to find language " + language);
			}
		}
	}
}