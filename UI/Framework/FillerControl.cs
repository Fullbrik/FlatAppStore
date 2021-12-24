using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public class FillerControl : Control
    {
        public override bool PerferExpandToParentWidth => perferExpandToParentWidth;
        private bool perferExpandToParentWidth = true;
        public override bool PerferExpandToParentHeight => perferExpandToParentHeight;
        private bool perferExpandToParentHeight = true;

        public FillerControl() { }

        public FillerControl(bool perferExpandToParentWidth, bool perferExpandToParentHeight)
        {
            this.perferExpandToParentWidth = perferExpandToParentWidth;
            this.perferExpandToParentHeight = perferExpandToParentHeight;
        }

        public FillerControl(LayoutDirection layoutDirection)
        {
            switch (layoutDirection)
            {
                case LayoutDirection.Horizontal:
                    perferExpandToParentWidth = true;
                    perferExpandToParentHeight = false;
                    break;
                case LayoutDirection.Vertical:
                    perferExpandToParentWidth = false;
                    perferExpandToParentHeight = true;
                    break;
                default:
                    break;
            }
        }


        public override void Draw()
        {
        }

        public override Vector2 GetSize(Vector2 maxSize)
        {
            return new Vector2(perferExpandToParentWidth ? maxSize.X : 1, perferExpandToParentHeight ? maxSize.Y : 1);
        }
    }
}