using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class RectControl : Control
	{
		public Color Color { get; set; }

		public override bool PerferExpandToParent => false;

		public RectControl(Color color)
		{
			Color = color;
		}

		public override void Draw()
		{
			Raylib.DrawRectangleRec(Transform.Bounds, Color);
		}

		public override Vector2 GetMinPreferredSize()
		{
			return new Vector2(1, 1);
		}
	}
}