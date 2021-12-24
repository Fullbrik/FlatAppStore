using System.Numerics;
using FlatAppStore.UI.Framework;
using FlatAppStore.UI.Framework.Assets;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
	public class ApplicationScreen : ScreenControl
	{
		public override string Title => "#screen_title_application";

		public override Color Background => new Color(11, 19, 28, 255);
		//public Color MainCarouselColor { get; } = new Color(35, 38, 46, 255);

		public string ApplicationName { get; set; } = "ERROR: No Application name";
		public Texture2D ApplicationIcon { get; set; } = Icons.NoApplicationIcon;

		protected override Control Build()
		{
			var scroll = new ScrollLayoutControl(LayoutDirection.Vertical);

			var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			// Space for header
			layout.AddChild(new RectControl(new Vector2(1, 50), Background), (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

			// Main application info
			var mainInfoLayout = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);
			mainInfoLayout.AddChild(new TextureControl(ApplicationIcon));
			mainInfoLayout.AddChild(new SpacerControl(20, 1));
			mainInfoLayout.AddChild(new LabelControl(ApplicationName, 30), (t) => { if (t is SimpleDirectionLayoutControlTransform slt) slt.CrossAxisAlignment = CrossAxisAlignment.Center; });
			layout.AddChild(mainInfoLayout);

			scroll.AddChild(layout);

			AddAction(ControllerButton.Face_Right, "#action_back", () => RemoveFromParent());

			return scroll;
		}
	}
}