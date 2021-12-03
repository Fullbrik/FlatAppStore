using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class RectStyle
	{
		public Color Color { get; set; }
		public float Roundness { get; set; }

		public Color BorderColor { get; set; }
		public int BorderThickness { get; set; }


		public void Draw(Rectangle bounds)
		{
			// Draw shape
			Raylib.DrawRectangleRounded(bounds, Roundness, 4, Color);

			// Draw border
			Raylib.DrawRectangleRoundedLines(bounds, Roundness, 4, BorderThickness, BorderColor);
		}
	}
}