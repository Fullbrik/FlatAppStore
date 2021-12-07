using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public record struct TransformLayoutData(int Index, int Count, Control prevChild);

    public abstract class Transform : PropertyAnimateable
    {
        public Control Control { get; }
        public Transform ParentTransform { get => Control?.Parent?.Transform; }

        public TransformLayoutData LayoutData { get; internal set; }

        public Transform(Control control)
        {
            Control = control;
        }

        public float OffsetX { get; set; }
        public float OffsetY { get; set; }

        public float OffsetWidth { get; set; }
        public float OffsetHeight { get; set; }

        public Rectangle Bounds { get => bounds; }
        private Rectangle bounds;

        public Rectangle LocalBounds { get => localBounds; }
        private Rectangle localBounds;


        protected override void OnPropertyUpdated<T>(Tween<T> tween)
        {
            base.OnPropertyUpdated(tween);
            Control.Invalidate();
        }

        public void UpdateBounds()
        {
            var baseX = (ParentTransform != null) ? ParentTransform.Bounds.x : 0;
            var baseY = (ParentTransform != null) ? ParentTransform.Bounds.y : 0;

            localBounds = GetLocalBounds();
            localBounds.x += OffsetX;
            localBounds.y += OffsetY;
            localBounds.width += OffsetWidth;
            localBounds.height += OffsetHeight;


            bounds = new Rectangle(baseX + localBounds.x, baseY + localBounds.y, localBounds.width, localBounds.height);
        }

        protected abstract Rectangle GetLocalBounds();
    }
}