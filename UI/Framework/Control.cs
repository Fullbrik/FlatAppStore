using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public abstract class Control : PropertyAnimateable
    {
        public LayoutControl Parent { get; private set; }
        public virtual NavigatorControl Navigator { get => Parent.Navigator; }

        public Transform Transform { get; private set; }

        public bool IsInitialized { get; private set; }

        public void Initialize(LayoutControl parent, Transform transform)
        {
            Parent = parent;
            Transform = transform;

            IsInitialized = true;

            Initialized();

            Invalidate();
        }

        protected virtual void Initialized() { }
        public virtual void Removed() { }

        public virtual void Invalidate()
        {
            if (Transform != null) Transform.UpdateBounds();
        }

        public abstract bool PerferExpandToParent { get; }
        public abstract Vector2 GetMinPreferredSize();
        public abstract void Draw();


        public void RemoveFromParent()
        {
            if (Parent != null) Parent.RemoveChild(this);
        }

        public virtual void OnInput(ControllerButton button) { }
    }
}