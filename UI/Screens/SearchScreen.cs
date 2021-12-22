using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
	public class SearchScreen : ScreenControl
	{
		public override string Title => "Search";

		public override Color Background => background;
		Color background = new Color(11, 19, 28, 255);

		protected override Control Build()
		{
			AddAction(ControllerButton.Face_Right, "Back", () => RemoveFromParent());

			var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			var header = new RectControl(Raylib_cs.Color.BLACK);
			header.Height = 50;
			layout.AddChild(header, (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

			return layout;
		}
	}
}