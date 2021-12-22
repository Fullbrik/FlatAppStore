using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class RectControl : Control
	{
		public Color Color { get; set; } = Color.BLACK;

		public override bool PerferExpandToParentWidth => ExpandToParent;
		public override bool PerferExpandToParentHeight => ExpandToParent;

		public bool ExpandToParent { get; set; } = false;

		public float Width { get => size.X; set => size.X = value; }
		public float Height { get => size.Y; set => size.Y = value; }
		public Vector2 Size { get => size; set => size = value; }
		private Vector2 size = new Vector2(1, 1);

		public RectControl() { }

		public RectControl(Color color)
		{
			Color = color;
		}

		public override void Draw()
		{
			Raylib.DrawRectangleRec(Transform.DrawBounds, Color);
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			if (ExpandToParent) return maxSize;
			else return Size;
		}
	}
}