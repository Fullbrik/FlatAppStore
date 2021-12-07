using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public class SizeLayoutControl : LayoutControl
    {
        public float Width { get => size.X; set => size.X = value; }
        public float Height { get => size.Y; set => size.Y = value; }
        public Vector2 Size { get => size; set => size = value; }
        private Vector2 size = new Vector2(1, 1);

        public override bool PerferExpandToParent => false;

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

        public override Vector2 GetMinPreferredSize()
        {
            return Size;
        }

        protected override Transform CreateTransform(Control control)
        {
            return new FillParentTransform(control);
        }
    }
}