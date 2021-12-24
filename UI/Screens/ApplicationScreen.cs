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

        public string ApplicationName { get; set; } = "ERROR: No Application name";
        public Texture2D ApplicationIcon { get; set; } = Icons.NoApplicationIcon;

        private ScrollLayoutControl scroll;

        protected override Control Build()
        {
            scroll = new ScrollLayoutControl(LayoutDirection.Vertical);
            scroll.UseScrollWheelVertical = true;

            var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

            // Space for header
            layout.AddChild(new RectControl(new Vector2(1, 50), Background), (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

            layout.AddChild(new SpacerControl(1, 20));

            // Main application info
            var mainInfoLayout = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);
            mainInfoLayout.AddChild(new FillerControl(LayoutDirection.Horizontal));
            mainInfoLayout.AddChild(new TextureControl(ApplicationIcon));
            mainInfoLayout.AddChild(new SpacerControl(20, 1));
            mainInfoLayout.AddChild(new LabelControl(ApplicationName, 30, Color.WHITE), (t) => { if (t is SimpleDirectionLayoutControlTransform slt) slt.CrossAxisAlignment = CrossAxisAlignment.Center; });
            mainInfoLayout.AddChild(new FillerControl(LayoutDirection.Horizontal));
            layout.AddChild(mainInfoLayout);

            layout.AddChild(new SpacerControl(1, 20));

            var screenshotCarousel = new CarouselControl("#carousel_screenshots", ScreenshotCarouselColor);
            layout.AddChild(screenshotCarousel);

            scroll.AddChild(layout);

            //Debug.DrawControlBounds(mainInfoLayout);

            AddAction(ControllerButton.Face_Right, "#action_back", () => RemoveFromParent());

            return scroll;
        }
    }
}