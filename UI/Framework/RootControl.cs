using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public class RootControl : LayoutControl, IUpdateable
    {
        public bool IsLayoutReady { get; set; } = false;

        public override InputManager InputManager => inputManager;
        private InputManager inputManager;

        public RootControl(Engine engine)
        {
            inputManager = engine.InputManager;
        }

        public override bool PerferExpandToParentWidth => true;
        public override bool PerferExpandToParentHeight => true;

        protected override bool IsInputAbsorbed => isInputAbsorbed;
        private bool isInputAbsorbed = false;

        public void Update(float deltaTime)
        {
            // Check for input
            InputManager.CheckInput(OnInput, OnScroll);

            isInputAbsorbed = false;
        }

        protected override void AbsorbInput()
        {
            isInputAbsorbed = true;
        }

        public override void OnInput(ControllerButton button)
        {
            StartChildLoop();
            foreach (var child in Children)
                if (!isInputAbsorbed) // If we have an input absorber, make sure we are that absorber
                    child.OnInput(button);
            EndChildLoop();
        }

        protected override Transform CreateTransform(Control control)
        {
            return new Transform(control);
        }

        protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
        {
            if (IsLayoutReady)
            {
                foreach (var child in Children)
                {
                    child.GetSize(maxSize);

                    SetChildLocalBounds(child, new Rectangle(0, 0, maxSize.X, maxSize.Y));
                }
            }

            return maxSize;
        }
    }
}