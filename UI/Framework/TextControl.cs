using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public abstract class TextControl : Control
	{
		public string Text
		{
			get => text;
			set
			{
				text = value;

				if (string.IsNullOrEmpty(text)) localizedText = "";
				else if (text.StartsWith("#")) localizedText = Localization.LocalizationManager.GetLocalizedString(value);
				else localizedText = text;
			}
		}
		private string text = "";
		public string LocalizedText { get => localizedText; }
		private string localizedText = "";
		public Font Font { get; set; } = Raylib.GetFontDefault();
		public int FontSize { get; set; } = 20;

		public Color Color { get; set; } = Color.BLACK;
	}
}