using System;
using FlatAppStore.UI.Controls;
using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
    public class StartupScreen : ScreenControl
    {
        public override string Title => "Startup";

        public override Color Background => background;
        Color background = new Color(11, 19, 28, 255);

        protected override Control Build()
        {
            var control = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

            var carousel = new CarouselControl("Popular apps");
            control.AddChild(carousel);

            control.AddChild(new SpacerControl(0, 20)); // Add Spacer

            var rect1 = new RectControl(Color.RED);
            rect1.Size = new System.Numerics.Vector2(100, 50);
            control.AddChild(rect1);

            var rect2 = new RectControl(Color.ORANGE);
            rect2.Size = new System.Numerics.Vector2(100, 100);
            rect2.ExpandToParent = true;
            control.AddChild(rect2);

            AddAction(ControllerButton.DPAD_Up, "Invalidate", () => Navigator.Invalidate());
            AddAction(ControllerButton.Face_Down, "Select", () => Navigator.AddChild(new SearchScreen()));
            AddAction(ControllerButton.Face_Right, "Back", () => RemoveFromParent());

            return control;
        }
    }
}