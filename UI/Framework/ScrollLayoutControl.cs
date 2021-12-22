using System.Collections.Generic;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{

    public class ScrollLayoutControl : LayoutControl
    {
        public bool ShouldScrollHorizontal { get; set; } = false;
        public bool ShouldScrollVertical { get; set; } = false;

        public int HorizontalScroll { get => horizontalScroll; set { horizontalScroll = value; ReLayoutChildren(); } }
        private int horizontalScroll = 0;
        public int VerticalScroll { get => verticalScroll; set { verticalScroll = value; ReLayoutChildren(); } }
        private int verticalScroll = 0;

        public int MaxHorizontalScroll { get; private set; }
        public int MaxVerticalScroll { get; private set; }

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

        public override bool PerferExpandToParentWidth => ShouldScrollHorizontal || expandWidth;
        private bool expandWidth = false;
        public override bool PerferExpandToParentHeight => ShouldScrollVertical || expandHeight;
        private bool expandHeight = false;

        protected override Transform CreateTransform(Control control)
        {
            return new Transform(control);
        }

        protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
        {
            float currentWidth = 0, currentHeight = 0;






            List<Control> expandedControls = new List<Control>();

            foreach (var child in Children)
            {
                // If we should scroll in that direction, we allow the size to be the biggest it can be. Otherwise, it will just be whatever max we got.
                var childMaxSize = new Vector2((ShouldScrollHorizontal) ? float.MaxValue : maxSize.X, (ShouldScrollVertical) ? float.MaxValue : maxSize.Y);

                if (child.PerferExpandToParentWidth)
                {
                    expandWidth = true;
                    childMaxSize.X = maxSize.X; // If we are going to expand the width, we can't have an infinate width otherwise it might not work right
                }


                if (child.PerferExpandToParentHeight)
                {
                    expandHeight = true;
                    childMaxSize.Y = maxSize.Y; // If we are going to expand the height, we can't have an infinate height otherwise it might not work right
                }

                var childSize = child.GetSize(childMaxSize); // We just want the child to fill ourselves

                // We don't want to do this if the child is expanding
                if (!child.PerferExpandToParentWidth && childSize.X > currentWidth) currentWidth = childSize.X;
                if (!child.PerferExpandToParentHeight && childSize.Y > currentHeight) currentHeight = childSize.Y;

                // If we are scrolling in that direction and the child won't expand in that direction, we will give the scroll. Otherwise, give 0 because controls that expand don't scroll
                float x = (ShouldScrollHorizontal && !child.PerferExpandToParentWidth) ? HorizontalScroll : 0;
                float y = (ShouldScrollVertical && !child.PerferExpandToParentHeight) ? VerticalScroll : 0;

                SetChildLocalBounds(child, new Raylib_cs.Rectangle(x, y, childSize.X, childSize.Y));
            }

            MaxHorizontalScroll = (int)currentWidth;
            MaxVerticalScroll = (int)currentHeight;

            return new Vector2((PerferExpandToParentWidth) ? maxSize.X : currentWidth, (PerferExpandToParentHeight) ? maxSize.Y : currentHeight);
        }
    }
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