using System;
using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
	public class StartupScreen : ScreenControl
	{
		bool didAdd = false;

		public override string Title => "Startup";

		protected override Control Build()
		{
			var control = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			var rect1 = new RectControl(Color.RED);
			rect1.Size = new System.Numerics.Vector2(100, 50);
			control.AddChild(rect1);

			var rect2 = new RectControl(Color.ORANGE);
			rect2.Size = new System.Numerics.Vector2(100, 100);
			rect2.ExpandToParent = true;
			control.AddChild(rect2);

			AddAction(ControllerButton.Face_Right, "Back", () => RemoveFromParent());
			AddAction(ControllerButton.DPAD_Up, "Invalidate", () => Navigator.Invalidate());

			return control;
		}

		public override void OnInput(ControllerButton button)
		{
			base.OnInput(button);

			if (!didAdd)
			{
				switch (button)
				{
					case ControllerButton.Face_Down:
						//didAdd = true;
						Navigator.AddChild(new SearchScreen());
						break;
					default:
						break;
				}
			}
		}
	}
}