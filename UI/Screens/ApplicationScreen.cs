using System.Numerics;
using FlatAppStore.UI.Controls;
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

        public string Description { get; set; } = LoremIpsum.Generate(3);

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

            mainInfoLayout.AddChild(new SpacerControl(30, 1));

            var installButton = new FocusableButtonControl("#button_install", Color.GREEN);
            mainInfoLayout.AddChild(installButton, (t) => { if (t is SimpleDirectionLayoutControlTransform slt) slt.CrossAxisAlignment = CrossAxisAlignment.Center; });

            mainInfoLayout.AddChild(new FillerControl(LayoutDirection.Horizontal));
            layout.AddChild(mainInfoLayout);

            // More spacing
            layout.AddChild(new SpacerControl(1, 20));

            // Screenshots
            var screenshotCarousel = new CarouselControl("#carousel_screenshots", ScreenshotCarouselColor);
            layout.AddChild(screenshotCarousel);

            // Description
            var descriptionControl = new DescriptionControl(Description);
            var descriptionControlFocusProvider = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal); //new SingleFocusableProvider<DescriptionControl>(descriptionControl);
            descriptionControlFocusProvider.AddChild(descriptionControl);
            layout.AddChild(descriptionControlFocusProvider);

            // Add layout to scroll
            scroll.AddChild(layout);

            //Debug.DrawControlBounds(mainInfoLayout);

            // Setup focus
            mainInfoLayout.ChildGetFocus += (c) => ScrollScrollerTo(scroll, mainInfoLayout);
            mainInfoLayout.FocusProviderDown = screenshotCarousel;

            screenshotCarousel.BindChildGetFocus((c) => ScrollScrollerTo(scroll, screenshotCarousel));
            screenshotCarousel.FocusProviderUp = mainInfoLayout;
            screenshotCarousel.FocusProviderDown = descriptionControlFocusProvider;

            descriptionControlFocusProvider.ChildGetFocus += (c) => ScrollScrollerTo(scroll, descriptionControlFocusProvider);
            descriptionControlFocusProvider.FocusProviderUp = screenshotCarousel;

            installButton.Focus();

            // Add actions
            AddAction(ControllerButton.Face_Right, "#action_back", () => RemoveFromParent());

            return scroll;
        }
    }
}