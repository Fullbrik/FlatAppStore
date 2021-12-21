using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class LabelControl : Control
	{
		public string Text { get; set; } = "";
		public int FontSize { get; set; } = 20;
		public Color Color { get; set; } = Color.BLACK;

		public override bool PerferExpandToParentWidth => false;
		public override bool PerferExpandToParentHeight => false;

		public LabelControl() { }

		public LabelControl(string text)
		{
			Text = text;
		}

		public LabelControl(string text, int fontSize)
		{
			Text = text;
			FontSize = fontSize;
		}

		public LabelControl(string text, Color color)
		{
			Text = text;
			Color = color;
		}

		public LabelControl(string text, int fontSize, Color color)
		{
			Text = text;
			FontSize = fontSize;
			Color = color;
		}

		public override void Draw()
		{
			// Draw text. Spacing is based off code from the raylib source for DrawText()
			Raylib.DrawTextEx(Raylib.GetFontDefault(), Text, new Vector2(Transform.DrawBounds.x, Transform.DrawBounds.y), FontSize, FontSize / 10, Color);
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			return Raylib.MeasureTextEx(Raylib.GetFontDefault(), Text, FontSize, FontSize / 10);
		}

		// public override Vector2 GetMinPreferredSize()
		// {
		// 	return Raylib.MeasureTextEx(Raylib.GetFontDefault(), Text, FontSize, FontSize / 10);
		// }
	}
}