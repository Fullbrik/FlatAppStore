using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public abstract class UserControl : LayoutControl
	{
		public override bool PerferExpandToParentWidth => false;

		public override bool PerferExpandToParentHeight => false;

		protected override Transform CreateTransform(Control control)
		{
			return new Transform(control);
		}

		protected override void Initialized()
		{
			Rebuild();
		}

		public void Rebuild()
		{
			PauseLayout();

			// Remove all previous children
			RemoveAllChildren();

			// Rebuild everything
			AddChild(Build());

			ReLayoutChildren();

			ResumeLayout();
		}

		protected abstract Control Build();

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			var currentSize = Vector2.Zero;

			foreach (var child in Children)
			{
				var childSize = child.GetSize(maxSize);

				if (childSize.X > currentSize.X) currentSize.X = childSize.X;
				if (childSize.Y > currentSize.Y) currentSize.Y = childSize.Y;

				SetChildLocalBounds(child, new Raylib_cs.Rectangle(0, 0, childSize.X, childSize.Y));
			}

			return currentSize;
		}
	}
}