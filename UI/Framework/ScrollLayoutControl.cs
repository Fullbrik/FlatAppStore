using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	/*
	public class ScrollLayoutControl : LayoutControl
	{
		public int HorizontalScroll { get; set; } = 0;
		public int VerticalScroll { get; set; } = 0;

		public bool ShouldScrollHorizontal { get; set; } = false;
		public bool ShouldScrollVertical { get; set; } = false;

		public ScrollLayoutControl() { }

		public ScrollLayoutControl(LayoutDirection direction)
		{
			switch (direction)
			{
				case LayoutDirection.Horizontal:
					ShouldScrollHorizontal = true;
					ShouldScrollVertical = false;
					break;
				case LayoutDirection.Vertical:
					ShouldScrollHorizontal = false;
					ShouldScrollVertical = true;
					break;
				default:
					break;
			}
		}

		public ScrollLayoutControl(bool shouldScrollHorizontal, bool shouldScrollVertical)
		{
			ShouldScrollHorizontal = shouldScrollHorizontal;
			ShouldScrollVertical = shouldScrollVertical;
		}

		public override bool PerferExpandToParentWidth => true;
		public override bool PerferExpandToParentHeight => true;

		public override Vector2 GetMinPreferredSize()
		{
			throw new System.NotImplementedException();
		}

		protected override Transform CreateTransform(Control control)
		{
			return new ScrollLayoutControlTransform(control, this);
		}
	}

	public class ScrollLayoutControlTransform : Transform
	{
		public ScrollLayoutControl LayoutControl { get; }

		public ScrollLayoutControlTransform(Control control, ScrollLayoutControl layoutControl) : base(control)
		{
			LayoutControl = layoutControl;
		}

		protected override Raylib_cs.Rectangle GetLocalBounds()
		{
			float x = 0;
			float y = 0;
			float width = 0;
			float height = 0;

			var preferredSize = Control.GetMinPreferredSize();

			if (Control.PerferExpandToParentWidth)
			{
				width = ParentTransform.Bounds.width;
			}
			else if (LayoutControl.ShouldScrollHorizontal)
			{
				x = -LayoutControl.HorizontalScroll;
				width = preferredSize.X;
			}
			else
			{
				width = ParentTransform.Bounds.width;
			}

			if (Control.PerferExpandToParentHeight)
			{
				width = ParentTransform.Bounds.height;
			}
			else if (LayoutControl.ShouldScrollVertical)
			{
				y = -LayoutControl.VerticalScroll;
				height = preferredSize.Y;
			}
			else
			{
				height = ParentTransform.Bounds.height;
			}

			return new Raylib_cs.Rectangle(x, y, width, height);
		}
	}
    */
}