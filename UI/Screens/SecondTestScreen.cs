using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
	public class SecondTestScreen : ScreenControl
	{
		protected override Control BuildScreen()
		{
			var control = new RectControl(Color.GOLD);
			return control;
		}

		public override void OnInput(ControllerButton button)
		{
			base.OnInput(button);

			switch (button)
			{
				case ControllerButton.Face_Right:
					RemoveFromParent();
					break;
				default:
					break;
			}
		}
	}
}