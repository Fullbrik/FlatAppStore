using System;
using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
	public class StartupScreen : ScreenControl
	{
		bool didAdd = false;

		protected override Control BuildScreen()
		{
			var control = new RectControl(Color.BLUE);
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
						Navigator.AddChild(new SecondTestScreen());
						break;
					case ControllerButton.Face_Right:
						Raylib_cs.Raylib.CloseWindow();
						break;
					default:
						break;
				}
			}
		}
	}
}