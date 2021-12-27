using System.Threading.Tasks;
using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
	public class SearchScreen : ScreenControl
	{
		public override string Title => "#screen_title_search";

		public override Color Background => Theme.PrimaryColor;

		protected override Task Load()
		{
			return Task.CompletedTask;
		}

		protected override Control Build()
		{
			AddAction(ControllerButton.Face_Right, "#action_back", () => RemoveFromParent());

			var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			var header = new RectControl(Raylib_cs.Color.BLACK);
			header.Height = 50;
			layout.AddChild(header, (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

			return layout;
		}
	}
}