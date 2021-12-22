using Raylib_cs;
using FlatAppStore.UI.Framework;
using System.Numerics;

namespace FlatAppStore.UI.Controls
{
    public class CarouselControl : UserControl, IFocusLayoutProvider
    {
        public string Title { get; set; } = "";
        public Color BackgroundColor { get; set; } = new Color(35, 38, 46, 255);

        public override bool PerferExpandToParentWidth => true;

        private SimpleDirectionLayoutControl carouselPartsLayout;

        public CarouselControl() { }

        public CarouselControl(string title)
        {
            Title = title;
        }

        public CarouselControl(string title, Color backgroundColor)
        {
            Title = title;
            BackgroundColor = backgroundColor;
        }

        public FocusableUserControl FocusGetDown(FocusableUserControl focusableUserControl)
        {
            return carouselPartsLayout.FocusGetDown(focusableUserControl);
        }

        public FocusableUserControl FocusGetFirst()
        {
            return carouselPartsLayout.FocusGetFirst();
        }

        public FocusableUserControl FocusGetLeft(FocusableUserControl focusableUserControl)
        {
            return carouselPartsLayout.FocusGetLeft(focusableUserControl);
        }

        public FocusableUserControl FocusGetRight(FocusableUserControl focusableUserControl)
        {
            return carouselPartsLayout.FocusGetRight(focusableUserControl);
        }

        public FocusableUserControl FocusGetUp(FocusableUserControl focusableUserControl)
        {
            return carouselPartsLayout.FocusGetUp(focusableUserControl);
        }

        protected override Control Build()
        {
            var control = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

            // Make room for header
            control.AddChild(new SpacerControl(0, 50));

            // Add title and its padding
            control.AddChild(new PaddingControl(new LabelControl(Title, Color.WHITE), 10, 10, 0, 0));

            // Build carousel parts
            carouselPartsLayout = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);

            carouselPartsLayout.AddChild(new TestFocusControl(Color.BROWN));
            carouselPartsLayout.AddChild(new SpacerControl(10, 0));
            carouselPartsLayout.AddChild(new TestFocusControl(Color.ORANGE));
            carouselPartsLayout.AddChild(new SpacerControl(10, 0));
            carouselPartsLayout.AddChild(new TestFocusControl(Color.BLUE));

            // Focus the first one
            carouselPartsLayout.FocusGetFirst()?.Focus();

            var scroll = new ScrollLayoutControl(LayoutDirection.Horizontal);

            // Add it
            scroll.AddChild(carouselPartsLayout);
            control.AddChild(scroll);

            // and return
            return new PaddingControl(control, 0, 20, 10, 10);
        }

        public override void Draw()
        {
            Raylib.DrawRectangleGradientEx(new Rectangle(Transform.DrawBounds.x, Transform.DrawBounds.y + 10, Transform.DrawBounds.width, Transform.DrawBounds.height + 30), Raylib_cs.Color.BLACK, new Raylib_cs.Color(0, 0, 0, 0), new Raylib_cs.Color(0, 0, 0, 0), Raylib_cs.Color.BLACK); // Draw shaddow
            Raylib.DrawRectangleRec(Transform.DrawBounds, BackgroundColor); // Draw background

            base.Draw();
        }

        public override Vector2 GetSize(Vector2 maxSize)
        {
            return new Vector2(maxSize.X, base.GetSize(maxSize).Y);
        }
    }
}