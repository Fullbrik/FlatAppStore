using System;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public abstract class Control : PropertyAnimateable
    {
        public LayoutControl Parent { get; private set; }
        public virtual NavigatorControl Navigator { get => Parent.Navigator; }
        public virtual InputManager InputManager { get => Parent?.InputManager; }

        public virtual Theme Theme { get => Parent?.Theme ?? Theme.Default; }

        public Transform Transform { get; private set; }

        public event Action<Transform> OnTransformInitialized;
        public bool IsInitialized { get; private set; }

        public void Initialize(LayoutControl parent, Transform transform)
        {
            Parent = parent;
            Transform = transform;

            IsInitialized = true;

            Initialized();

            OnTransformInitialized?.Invoke(Transform);

            Invalidate();
        }

        protected virtual void Initialized() { }
        public virtual void Removed() { }

        public virtual void Invalidate()
        {
            if (Transform != null)
                Transform.UpdateTransform();
        }

        public virtual void OnMouseOver(Vector2 globalMousePosition, bool leftButtonDown, bool middleButtonDown, bool rightButtonDown) { }

        public abstract bool PerferExpandToParentWidth { get; }
        public abstract bool PerferExpandToParentHeight { get; }

        public abstract Vector2 GetSize(Vector2 maxSize);
        public abstract void Draw();


        public void RemoveFromParent()
        {
            if (Parent != null) Parent.RemoveChild(this);
        }

        public virtual void OnInput(ControllerButton button) { }
        public virtual void OnScroll(float amount) { }

        protected virtual bool IsInputAbsorbed { get => Parent?.IsInputAbsorbed ?? false; }

        protected virtual void AbsorbInput()
        {
            if (Parent != null) Parent.AbsorbInput();
        }
    }
}