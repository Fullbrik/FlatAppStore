using System;
using System.Numerics;
using FlatAppStore.UI.Controls;
using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Screens
{
    public class StartupScreen : ScreenControl
    {
        public override string Title => "#screen_title_startup";

        public override Color Background { get; } = new Color(11, 19, 28, 255);

        public Color MainCarouselColor { get; } = new Color(35, 38, 46, 255);

        public override int VerticalScrollAmount => scroll?.VerticalScroll ?? 0;

        private ScrollLayoutControl scroll;

        protected override Control Build()
        {
            scroll = new ScrollLayoutControl(LayoutDirection.Vertical);
            scroll.UseScrollWheelVertical = true;

            var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

            layout.AddChild(new RectControl(new Vector2(1, 50), MainCarouselColor), (t) => (t as SimpleDirectionLayoutControlTransform).CrossAxisAlignment = CrossAxisAlignment.Stretch);

            var mainCarousel = new CarouselControl("#popular_apps", MainCarouselColor);
            mainCarousel.OnTransformInitialized += (t) => mainCarousel.Focus();
            mainCarousel.BindChildGetFocus((c) => ScrollMainScrollTo(mainCarousel));
            layout.AddChild(mainCarousel);

            //layout.AddChild(new SpacerControl(0, 20)); // Add Spacer

            var category1 = new CarouselControl("#category_music_and_video", Background);
            category1.BindChildGetFocus((c) => ScrollMainScrollTo(category1));
            layout.AddChild(category1);

            var category2 = new CarouselControl("#category_communication", Background);
            category2.BindChildGetFocus((c) => ScrollMainScrollTo(category2));
            layout.AddChild(category2);

            // Setup layout for focus order thing with multi layouts. Just read the code it'll make sense.
            mainCarousel.FocusProviderDown = category1;

            category1.FocusProviderUp = mainCarousel;
            category1.FocusProviderDown = category2;

            category2.FocusProviderUp = category1;

            scroll.AddChild(layout);

            AddAction(ControllerButton.Face_Up, "Play Opening Animation", () => Navigator.PlayOpeningAnimation());
            AddAction(ControllerButton.Face_Down, "#action_select", () => Navigator.AddChild(new ApplicationScreen()));
            AddAction(ControllerButton.Face_Right, "#action_back", () => Navigator.PlayClosingAnimation());

            return scroll;
        }

        private void ScrollMainScrollTo(Control control)
        {
            if (scroll.ShouldScrollHorizontal)
                scroll.AnimateProperty<int>("HorizontalScroll").To((int)(control.Transform.LocalBounds.x - control.Transform.DrawBounds.width / 2f), .2f).Start();

            if (scroll.ShouldScrollVertical)
                scroll.AnimateProperty<int>("VerticalScroll").To((int)(control.Transform.LocalBounds.y - control.Transform.DrawBounds.height / 2f), .2f).Start();
        }
    }
}