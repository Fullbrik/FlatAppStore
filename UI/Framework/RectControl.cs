using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class RectControl : Control
	{
		public Color Color { get; set; } = Color.BLACK;

		public override bool PerferExpandToParent => ExpandToParent;

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

		public override void Draw(Canvas canvas)
		{
			Raylib.DrawRectangleRec(Transform.Bounds, Color);
		}

		public override Vector2 GetMinPreferredSize()
		{
			return Size;
		}
	}
}