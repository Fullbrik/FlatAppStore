using FlatAppStore.UI.Framework;
using FlatAppStore.UI.Models;
using FlatAppStore.UI.Screens;
using Raylib_cs;

namespace FlatAppStore.UI.Controls
{
    public class ApplicationPreviewControl : FocusableUserControl
    {
        public ApplicationData ApplicationData { get; set; }

        public Color BackgroundColor { get; set; } = Color.DARKGRAY;

        PaddingControl layoutPadding = new PaddingControl();

        public ApplicationPreviewControl() { }

        public ApplicationPreviewControl(ApplicationData data)
        {
            ApplicationData = data;
        }

        protected override Control Build()
        {
            var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);
            layout.DefaultCrossAxisAlignment = CrossAxisAlignment.Center;
            layout.AddChild(new PaddingControl(new TextureControl(ApplicationData.Icon), 25, 25, 25, 25));

            layout.AddChild(new PaddingControl(new LabelControl(ApplicationData.Name, Color.WHITE), 0, 10, 10, 10));

            layoutPadding.RemoveAllChildren();
            layoutPadding.AddChild(layout);

            if (IsFocused) { layoutPadding.PaddingTop = 5; layoutPadding.PaddingBottom = 5; layoutPadding.PaddingLeft = 5; layoutPadding.PaddingRight = 5; }

            return layoutPadding;
        }

        public override void Draw()
        {
            // Draw background
            Raylib.DrawRectangleRec(Transform.DrawBounds, BackgroundColor);

            base.Draw();
        }

        protected override void OnFocused()
        {
            base.OnFocused();

            AnimateProperty<Color>(nameof(BackgroundColor))
                .To(Theme.PrimaryColor, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingTop")
                .StartWith(0)
                .To(5, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingBottom")
                .StartWith(0)
                .To(5, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingRight")
                .StartWith(0)
                .To(5, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingLeft")
                .StartWith(0)
                .To(5, .2f)
                .Start();
        }

        protected override void OnUnfocused()
        {
            base.OnUnfocused();

            AnimateProperty<Color>(nameof(BackgroundColor))
                .To(Color.DARKGRAY, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingTop")
                .StartWith(5)
                .To(0, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingBottom")
                .StartWith(5)
                .To(0, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingRight")
                .StartWith(5)
                .To(0, .2f)
                .Start();

            layoutPadding.AnimateProperty<float>("PaddingLeft")
                .StartWith(5)
                .To(0, .2f)
                .Start();
        }

        public override void DoAction()
        {
            Navigator.AddChild(new ApplicationScreen(ApplicationData));
        }
    }
}