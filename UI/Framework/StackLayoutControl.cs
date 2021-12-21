using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class StackLayoutControl : LayoutControl
	{
		public override bool PerferExpandToParentWidth => throw new System.NotImplementedException();

		public override bool PerferExpandToParentHeight => throw new System.NotImplementedException();

		protected override Transform CreateTransform(Control control)
		{
			return new Transform(control);
		}

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			foreach (var child in Children)
			{
				var size = child.GetSize(maxSize);

				SetChildLocalBounds(child, new Raylib_cs.Rectangle(0, 0, size.X, size.Y));
			}

			return maxSize;
		}
	}
}