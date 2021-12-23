using System.Collections.Generic;

namespace FlatAppStore.UI.Framework.Localization
{
	public record Language(string Name, Dictionary<string, string> Localizations)
	{
		public static Language Parse(string name, string text)
		{
			Dictionary<string, string> localizations = new Dictionary<string, string>();

			var lines = text.Split('\n');

			for (int i = 0; i < lines.Length; i++) // Loop through each line
			{
				var line = lines[i];

				if (string.IsNullOrWhiteSpace(line)) continue; // Skip if empty line
				if (char.IsWhiteSpace(line[0])) continue; // Skip if starts with whitespace

				bool hasStartedLocalizedText = false;
				string localizationName = "";
				string localizationText = "";

				for (int j = 0; j < line.Length; j++) // Loop through each character
				{
					if (!hasStartedLocalizedText) // Start off by getting the name of the localization
					{
						if (line[j] == '=') hasStartedLocalizedText = true;
						else localizationName += line[j];
					}
					else
					{
						if (line[j] == '\\') // Escape next character
						{
							if (j + 1 >= line.Length)
							{
								i++; // Move to next line
								line = lines[i];
								localizationText += '\n';

								j = -1; // Because we will add one at the end, but we really want it to be zero
							}
							else
							{
								j++;

								if (line[j] == '\\')
								{
									localizationText += '\\';
								}
							}
						}
						else
						{
							localizationText += line[j];
						}
					}
				}

				localizations.Add(localizationName, localizationText);
			}

			return new Language(name, localizations);
		}
	}
}