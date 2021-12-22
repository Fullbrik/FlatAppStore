using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public class SpacerControl : Control
    {
        public override bool PerferExpandToParentWidth => false;

        public override bool PerferExpandToParentHeight => false;

        public Vector2 Size { get => size; set => size = value; }
        public float Width { get => size.X; set => size.X = value; }
        public float Height { get => size.Y; set => size.Y = value; }
        private Vector2 size = new Vector2(0, 0);

        public SpacerControl() { }

        public SpacerControl(float width, float height)
        {
            Width = width;
            Height = height;
        }

        public SpacerControl(Vector2 size)
        {
            this.size = size;
        }

        public override void Draw() { }

        public override Vector2 GetSize(Vector2 maxSize)
        {
            return Size;
        }
    }
}