using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class PaddingControl : LayoutControl
	{
		public float PaddingTop { get; set; }
		public float PaddingBottom { get; set; }
		public float PaddingRight { get; set; }
		public float PaddingLeft { get; set; }

		public override bool PerferExpandToParentWidth => false;

		public override bool PerferExpandToParentHeight => false;

		public PaddingControl() { }

		public PaddingControl(float top, float bottom, float right, float left) : this()
		{
			PaddingTop = top;
			PaddingBottom = bottom;
			PaddingRight = right;
			PaddingLeft = left;
		}

		public PaddingControl(Control control, float top, float bottom, float right, float left)
			: this(top, bottom, right, left)
		{
			AddChild(control);
		}

		// public override Vector2 GetMinPreferredSize()
		// {
		// 	return base.GetMinPreferredSize() + new Vector2(PaddingLeft + PaddingRight, PaddingTop + PaddingBottom); // Don't forget we are a bit bigger because of pading
		// }

		protected override Transform CreateTransform(Control control)
		{
			return new Transform(control);
		}
		public override void Draw()
		{
			base.Draw();
		}

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			var childMaxSize = maxSize + new Vector2(-PaddingLeft - PaddingRight, -PaddingTop - PaddingBottom);

			if (childMaxSize.X < 0) childMaxSize.X = 0;
			if (childMaxSize.Y < 0) childMaxSize.Y = 0;

			// Find out what the size is going to be so we can layout properly
			var currentSize = Vector2.Zero;
			foreach (var child in Children)
			{
				var childSize = child.GetSize(childMaxSize);

				if (childSize.X > currentSize.X) currentSize.X = childSize.X;
				if (childSize.Y > currentSize.Y) currentSize.Y = childSize.Y;
			}

			// Calculate the bounds
			//var childBounds = new Raylib_cs.Rectangle(PaddingLeft, PaddingTop, currentSize.X - PaddingLeft - PaddingRight, currentSize.Y - PaddingTop - PaddingBottom);
			var childBounds = new Raylib_cs.Rectangle(PaddingLeft, PaddingTop, currentSize.X, currentSize.Y);

			if (childBounds.x < 0) childBounds.x = 0;
			if (childBounds.y < 0) childBounds.y = 0;

			// And apply it
			foreach (var child in Children)
			{
				SetChildLocalBounds(child, childBounds);
			}

			return currentSize + new Vector2(PaddingLeft + PaddingRight, PaddingTop + PaddingBottom);
		}
	}

	// public class PaddingControlTransform : Transform
	// {
	// 	public PaddingControl Padding { get; }

	// 	public PaddingControlTransform(Control control, PaddingControl padding) : base(control)
	// 	{
	// 		Padding = padding;
	// 	}

	// 	protected override Raylib_cs.Rectangle GetLocalBounds()
	// 	{
	// 		return new Raylib_cs.Rectangle(
	// 			Padding.PaddingLeft,
	// 			Padding.PaddingTop,
	// 			ParentTransform.Bounds.width - Padding.PaddingLeft - Padding.PaddingRight,
	// 			ParentTransform.Bounds.height - Padding.PaddingTop - Padding.PaddingBottom);
	// 	}
	// }
}