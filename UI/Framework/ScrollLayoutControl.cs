using System;
using System.Collections.Generic;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{

    public class ScrollLayoutControl : LayoutControl
    {
        public bool UseScrollWheelHorizontal { get; set; } = false;
        public bool UseScrollWheelVertical { get; set; } = false;
        public int ScrollWheelSpeed { get; set; } = 20;

        public bool ShouldScrollHorizontal { get; set; } = false;
        public bool ShouldScrollVertical { get; set; } = false;

        public int HorizontalScroll { get => horizontalScroll; set { horizontalScroll = Math.Clamp(value, 0, MaxHorizontalScroll); ReLayoutChildren(); } }
        private int horizontalScroll = 0;
        public int VerticalScroll { get => verticalScroll; set { verticalScroll = Math.Clamp(value, 0, MaxVerticalScroll); ReLayoutChildren(); } }
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

                var childSize = child.GetSize(childMaxSize);

                // We don't want to do this if the child is expanding
                if (!child.PerferExpandToParentWidth && childSize.X > currentWidth) currentWidth = childSize.X;
                if (!child.PerferExpandToParentHeight && childSize.Y > currentHeight) currentHeight = childSize.Y;

                // If we are scrolling in that direction and the child won't expand in that direction, we will give the scroll. Otherwise, give 0 because controls that expand don't scroll
                float x = (ShouldScrollHorizontal && !child.PerferExpandToParentWidth) ? -HorizontalScroll : 0;
                float y = (ShouldScrollVertical && !child.PerferExpandToParentHeight) ? -VerticalScroll : 0;

                SetChildLocalBounds(child, new Raylib_cs.Rectangle(x, y, childSize.X, childSize.Y));
            }

            // Ensure we can't scroll to the point where the scroll goes past the parent
            if (currentWidth > maxSize.X) MaxHorizontalScroll = (int)(currentWidth - maxSize.X);
            else MaxHorizontalScroll = (int)currentWidth;

            if (currentHeight > maxSize.Y) MaxVerticalScroll = (int)(currentHeight - maxSize.Y);
            else MaxVerticalScroll = (int)currentHeight;

            if (ShouldScrollVertical && MaxVerticalScroll < 0)
                Debug.DoNothing();

            return new Vector2((PerferExpandToParentWidth) ? maxSize.X : currentWidth, (PerferExpandToParentHeight) ? maxSize.Y : currentHeight);
        }

        public override void OnScroll(float amount)
        {
            base.OnScroll(amount);

            if (UseScrollWheelHorizontal && ShouldScrollHorizontal)
            {
                HorizontalScroll += (int)(ScrollWheelSpeed * amount);
            }

            if (UseScrollWheelVertical && ShouldScrollVertical)
            {
                VerticalScroll -= (int)(ScrollWheelSpeed * amount);
            }
        }
    }
}