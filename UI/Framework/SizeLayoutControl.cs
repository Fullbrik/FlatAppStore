using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class SizeLayoutControl : LayoutControl
	{
		public float Width { get => size.X; set => size.X = value; }
		public float Height { get => size.Y; set => size.Y = value; }
		public Vector2 Size { get => size; set => size = value; }
		private Vector2 size = new Vector2(1, 1);

		public override bool PerferExpandToParentWidth => false;
		public override bool PerferExpandToParentHeight => false;

		public SizeLayoutControl() { }

		public SizeLayoutControl(Vector2 size)
		{
			Size = size;
		}

		public SizeLayoutControl(float width, float height)
		{
			Width = width;
			Height = height;
		}

		protected override Transform CreateTransform(Control control)
		{
			return new Transform(control);
		}

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			foreach (var child in Children)
			{
				// Update any size stuff
				child.GetSize(Size);

				SetChildLocalBounds(child, new Raylib_cs.Rectangle(0, 0, Size.X, Size.Y));
			}

			return Size;
		}
	}
}