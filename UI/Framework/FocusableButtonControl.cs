using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public class FocusableButtonControl : FocusableUserControl
    {
        public string Text { get => textLabel.Text; set => textLabel.Text = value; }
        public Color TextColor { get => textLabel.Color; set => textLabel.Color = value; }
        private LabelControl textLabel = new LabelControl();

        public Color BackgroundColor { get; set; } = Color.WHITE;

        public FocusableButtonControl() { }

        public FocusableButtonControl(string text)
        {
            Text = text;
        }

        public FocusableButtonControl(string text, Color backgroundColor)
        {
            Text = text;
            BackgroundColor = backgroundColor;
        }

        protected override Control Build()
        {
            return new PaddingControl(textLabel, 20, 20, 20, 20);
        }

        public override void Draw()
        {
            Raylib.DrawRectangleRec(Transform.DrawBounds, BackgroundColor);

            base.Draw();
        }
    }
}