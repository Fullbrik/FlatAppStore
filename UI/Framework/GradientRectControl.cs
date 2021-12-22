using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class GradientRectControl : Control
	{
		public override bool PerferExpandToParentWidth => ExpandToParent;
		public override bool PerferExpandToParentHeight => ExpandToParent;

		Color TopLeft { get; set; } = Color.BLACK; // Color1
		Color BottomLeft { get; set; } = Color.BLACK; // Color2
		Color BottomRight { get; set; } = Color.BLACK; // Color3
		Color TopRight { get; set; } = Color.BLACK; // Color4

		public bool ExpandToParent { get; set; } = false;

		public float Width { get => size.X; set => size.X = value; }
		public float Height { get => size.Y; set => size.Y = value; }
		public Vector2 Size { get => size; set => size = value; }
		private Vector2 size = new Vector2(1, 1);

		public GradientRectControl() { }

		public GradientRectControl(Color topLeft, Color bottomLeft, Color bottomRight, Color topRight)
		{
			TopLeft = topLeft;
			BottomLeft = bottomLeft;
			BottomRight = bottomRight;
			TopRight = topRight;
		}

		public override void Draw()
		{
			Raylib.DrawRectangleGradientEx(Transform.DrawBounds, TopLeft, BottomLeft, BottomRight, TopRight);
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			if (ExpandToParent) return maxSize;
			else return Size;
		}
	}
}