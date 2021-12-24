using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public abstract class ScreenControl : UserControl
    {
        public abstract string Title { get; }

        public IReadOnlyDictionary<ControllerButton, string> ActionNames { get => actionNames; }
        private readonly Dictionary<ControllerButton, string> actionNames = new Dictionary<ControllerButton, string>();

        private readonly Dictionary<ControllerButton, Action> actionButtons = new Dictionary<ControllerButton, Action>();

        public override bool PerferExpandToParentWidth => true;
        public override bool PerferExpandToParentHeight => true;

        public abstract Color Background { get; }
        public virtual int VerticalScrollAmount { get => 0; }

        protected void AddAction(ControllerButton button, string name, Action action)
        {
            actionNames.Add(button, name);
            actionButtons.Add(button, action);
        }

        public override void Draw()
        {
            Raylib.DrawRectangleRec(Transform.DrawBounds, Background); // Draw background

            base.Draw();

            // Draw header bar for better visibility when we scroll a bit
            float progress = Math.Clamp(VerticalScrollAmount, 0, Navigator.HeaderHeight) / (float)Navigator.HeaderHeight; // Better to divide with floats
            int transparency = (int)(progress * 255);

            Raylib.DrawRectangle((int)Transform.DrawBounds.x, (int)Transform.DrawBounds.y, (int)Transform.DrawBounds.width, Navigator.HeaderHeight, new Color(0, 0, 0, transparency));
        }

        public override void OnInput(ControllerButton button)
        {
            base.OnInput(button);

            if (actionButtons.ContainsKey(button)) actionButtons[button]();
        }
    }
}