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
		public Color ScreenshotCarouselColor { get; } = new Color(35, 38, 46, 255);

		public override int VerticalScrollAmount => scroll?.VerticalScroll ?? 0;

		public string ApplicationName { get; set; } = "Loading Application Name...";
		public string DeveloperName { get; set; } = "Loading Developer...";
		public Texture2D ApplicationIcon { get; set; } = Icons.NoApplicationIcon;

		public string Description = LoremIpsum.Generate(3);

		private ScrollLayoutControl scroll;

		protected override Control Build()
		{
			scroll = new ScrollLayoutControl(LayoutDirection.Vertical);
			scroll.UseScrollWheelVertical = true;

			var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			// Space for header
			layout.AddChild(new RectControl(new Vector2(1, 50), Background), (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

			// More padding
			layout.AddChild(new SpacerControl(1, 20));

			// Main application info
			var mainInfoLayout = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);
			mainInfoLayout.AddChild(new FillerControl(LayoutDirection.Horizontal));

			mainInfoLayout.AddChild(new TextureControl(ApplicationIcon));

			mainInfoLayout.AddChild(new SpacerControl(20, 1));

			var titleAndDeveloperNameLayout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);
			titleAndDeveloperNameLayout.AddChild(new LabelControl(ApplicationName, 30, Color.WHITE));
			titleAndDeveloperNameLayout.AddChild(new SpacerControl(1, 5));
			titleAndDeveloperNameLayout.AddChild(new LabelControl(DeveloperName, 25, Color.LIGHTGRAY));

			mainInfoLayout.AddChild(titleAndDeveloperNameLayout, (t) => { if (t is SimpleDirectionLayoutControlTransform slt) slt.CrossAxisAlignment = CrossAxisAlignment.Center; });
			mainInfoLayout.AddChild(new FillerControl(LayoutDirection.Horizontal));
			layout.AddChild(mainInfoLayout);

			// More spacing
			layout.AddChild(new SpacerControl(1, 20));

			// Screenshots
			var screenshotCarousel = new CarouselControl("#carousel_screenshots", ScreenshotCarouselColor);
			layout.AddChild(screenshotCarousel);

			// More spacing
			layout.AddChild(new SpacerControl(1, 20));

			// Description
			layout.AddChild(new PaddingControl(new MultiLineTextControl(Description, Color.GRAY), 0, 0, 20, 20));

			// Add layout to scroll
			scroll.AddChild(layout);

			//Debug.DrawControlBounds(mainInfoLayout);

			// Add actions
			AddAction(ControllerButton.Face_Right, "#action_back", () => RemoveFromParent());

			return scroll;
		}
	}
}