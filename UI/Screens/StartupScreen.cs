using System;
using FlatAppStore.UI.Controls;
using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
	public class StartupScreen : ScreenControl
	{
		public override string Title => "#screen_title_startup";

		public override Color Background => background;
		Color background = new Color(11, 19, 28, 255);

		ScrollLayoutControl scroll;

		protected override Control Build()
		{
			scroll = new ScrollLayoutControl(LayoutDirection.Vertical);
			scroll.UseScrollWheelVertical = true;

			var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			var carousel = new CarouselControl("#popular_apps");
			layout.AddChild(carousel);

			layout.AddChild(new SpacerControl(0, 20)); // Add Spacer

			var rect1 = new RectControl(Color.RED);
			rect1.Size = new System.Numerics.Vector2(100, 50);
			layout.AddChild(rect1);

			var rect2 = new RectControl(Color.ORANGE);
			rect2.Size = new System.Numerics.Vector2(500, 500);
			layout.AddChild(rect2);

			scroll.AddChild(layout);

			AddAction(ControllerButton.Face_Down, "#action_select", () => Navigator.AddChild(new SearchScreen()));
			AddAction(ControllerButton.Face_Right, "#action_back", () => RemoveFromParent());

			return scroll;
		}

		public override void Draw()
		{
			base.Draw();

			// Draw header bar for better visibility when we scroll a bit
			float progress = Math.Clamp(scroll.VerticalScroll, 0, 50) / 50f;
			int transparency = (int)(progress * 255);

			Raylib.DrawRectangle((int)Transform.DrawBounds.x, (int)Transform.DrawBounds.y, (int)Transform.DrawBounds.width, 50, new Color(0, 0, 0, transparency));
		}
	}
}