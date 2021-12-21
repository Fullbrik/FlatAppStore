using FlatAppStore.UI.Framework;

namespace FlatAppStore.UI.Screens
{
	public class SearchScreen : ScreenControl
	{
		public override string Title => "Search";

		protected override Control Build()
		{
			AddAction(ControllerButton.Face_Right, "Back", () => RemoveFromParent());

			var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			var header = new RectControl(Raylib_cs.Color.BLACK);
			header.Height = 100;
			layout.AddChild(header, (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

			return layout;
		}
	}
}