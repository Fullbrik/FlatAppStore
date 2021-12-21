using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class RoundedRectContainerControl : LayoutControl
	{
		public Color Color { get; set; } = Color.WHITE;
		public float Roundness { get; set; } = 1;

		public Vector2 ShaddowOffset { get; set; } = Vector2.Zero;

		public override bool PerferExpandToParentWidth => false;

		public override bool PerferExpandToParentHeight => false;

		public RoundedRectContainerControl() { }

		public RoundedRectContainerControl(Control child)
		{
			AddChild(child);
		}

		public RoundedRectContainerControl(Control child, Vector2 shaddowOffset)
		{
			ShaddowOffset = shaddowOffset;

			AddChild(child);
		}

		public RoundedRectContainerControl(Control child, Color color)
		{
			Color = color;

			AddChild(child);
		}

		protected override Transform CreateTransform(Control control)
		{
			return new Transform(control);
		}

		public override void Draw()
		{
			var drawBounds = Transform.DrawBounds;
			// Draw shaddow first
			Raylib.DrawRectangleRounded(
							new Rectangle(drawBounds.x + ShaddowOffset.X, drawBounds.y + ShaddowOffset.Y, drawBounds.width, drawBounds.height),
							Roundness,
							20,
							new Color(150, 150, 150, 100));

			Raylib.DrawRectangleRounded(Transform.DrawBounds, Roundness, 20, Color);

			base.Draw();
		}

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			var currentSize = Vector2.Zero;

			foreach (var child in Children)
			{
				var childSize = child.GetSize(maxSize);

				if (childSize.X > currentSize.X) currentSize.X = childSize.X;
				if (childSize.Y > currentSize.Y) currentSize.Y = childSize.Y;

				SetChildLocalBounds(child, new Rectangle(0, 0, childSize.X, childSize.Y));
			}

			return currentSize;
		}
	}
}