using System;
using System.Numerics;
using FlatAppStore.UI.Controls;
using FlatAppStore.UI.Framework;
using FlatAppStore.UI.Framework.Assets;
using FlatAppStore.UI.Models;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
    public class StartupScreen : ScreenControl
    {
        public override string Title => "#screen_title_startup";

        public override Color Background { get => Theme.PrimaryColor; }

        public Color MainCarouselColor { get => Theme.SecondaryColor; }

        public override int VerticalScrollAmount => scroll?.VerticalScroll ?? 0;

        private ScrollLayoutControl scroll;
        private CarouselControl<ApplicationData> mainCarousel;

        protected override Control Build()
        {
            scroll = new ScrollLayoutControl(LayoutDirection.Vertical);
            scroll.UseScrollWheelVertical = true;

            var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

            layout.AddChild(new RectControl(new Vector2(1, 50), MainCarouselColor), (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

            mainCarousel = new CarouselControl<ApplicationData>("#carousel_popular_apps", MainCarouselColor, (app) => new ApplicationPreviewControl(app));
            mainCarousel.Data = new ApplicationData[] { new ApplicationData("My Application", "com.my.application", "My Developer", UI.Framework.LoremIpsum.Generate(1), Icons.NoApplicationIcon, new string[] { }), new ApplicationData("My Application", "com.my.application", "My Developer", UI.Framework.LoremIpsum.Generate(2), Icons.NoApplicationIcon, new string[] { }), new ApplicationData("My Application", "com.my.application", "My", UI.Framework.LoremIpsum.Generate(3), Icons.NoApplicationIcon, new string[] { }), new ApplicationData("My", "com.my.application", "My", UI.Framework.LoremIpsum.Generate(1), Icons.NoApplicationIcon, new string[] { }), new ApplicationData("My", "com.my.application", "My", UI.Framework.LoremIpsum.Generate(1), Icons.NoApplicationIcon, new string[] { }), new ApplicationData("My", "com.my.application", "My Developer", UI.Framework.LoremIpsum.Generate(1), Icons.NoApplicationIcon, new string[] { }), new ApplicationData("My", "com.my.application", "My Developer", UI.Framework.LoremIpsum.Generate(1), Icons.NoApplicationIcon, new string[] { }), };
            mainCarousel.BindChildGetFocus((c) => ScrollScrollerTo(scroll, mainCarousel));
            layout.AddChild(mainCarousel);

            //layout.AddChild(new SpacerControl(0, 20)); // Add Spacer

            var category1 = new CarouselControl<ApplicationData>("#category_music_and_video", Background, (app) => new ApplicationPreviewControl(app));
            category1.BindChildGetFocus((c) => ScrollScrollerTo(scroll, category1));
            layout.AddChild(category1);

            var category2 = new CarouselControl<ApplicationData>("#category_communication", Background, (app) => new ApplicationPreviewControl(app));
            category2.BindChildGetFocus((c) => ScrollScrollerTo(scroll, category2));
            layout.AddChild(category2);

            // Setup layout for focus order thing with multi layouts. Just read the code it'll make sense.
            mainCarousel.FocusProviderDown = category1;

            category1.FocusProviderUp = mainCarousel;
            category1.FocusProviderDown = category2;

            category2.FocusProviderUp = category1;

            scroll.AddChild(layout);

            AddAction(ControllerButton.Face_Up, "Play Opening Animation", () => Navigator.PlayOpeningAnimation());
            AddAction(ControllerButton.Face_Down, "#action_select", () => FocusableUserControl.CurrentFocusedWidget?.DoAction());
            AddAction(ControllerButton.Face_Right, "#action_back", () => Navigator.PlayClosingAnimation());

            return scroll;
        }

        public override void OnBecomeMainScreen()
        {
            base.OnBecomeMainScreen();

            mainCarousel.FocusGetFirst()?.Focus();
        }
    }
}