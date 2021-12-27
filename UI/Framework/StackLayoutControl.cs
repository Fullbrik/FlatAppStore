using System.Collections.Generic;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public class StackLayoutControl : LayoutControl
    {
        public override bool PerferExpandToParentWidth => perferExpandToParentWidth;
        private bool perferExpandToParentWidth = false;

        public override bool PerferExpandToParentHeight => perferExpandToParentHeight;
        private bool perferExpandToParentHeight = false;

        protected override Transform CreateTransform(Control control)
        {
            return new Transform(control);
        }

        protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
        {
            throw new System.NotImplementedException();

            // perferExpandToParentWidth = false;
            // perferExpandToParentHeight = false;

            // float width = 0, hight = 0;

            // foreach (var child in Children)
            // {
            //     var childSize = child.GetSize(maxSize);

            //     if (child.PerferExpandToParentWidth) perferExpandToParentWidth = true;
            //     else if (childSize.X > width) width = childSize.X;

            //     if (child.PerferExpandToParentHeight) perferExpandToParentHeight = true;
            //     else if (childSize.Y > hight) hight = childSize.Y;

            //     SetChildLocalBounds(child, new Raylib_cs.Rectangle(0, 0, child.pe));
            // }

            // return maxSize;
        }
    }
}