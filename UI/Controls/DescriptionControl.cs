using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Controls
{
    public class DescriptionControl : FocusableUserControl
    {
        public string Description { get; set; }

        public DescriptionControl() { }

        public DescriptionControl(string description)
        {
            Description = description;
        }

        protected override Control Build()
        {
            var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

            // Spacing
            layout.AddChild(new SpacerControl(1, 20));

            layout.AddChild(new PaddingControl(new LabelControl("#title_description", 40, Color.WHITE), 0, 0, 100, 100));
            layout.AddChild(new PaddingControl(new MultiLineTextControl(Description, Color.GRAY), 10, 10, 100, 100));

            return layout;
        }

        public override void DoAction()
        {

        }
    }
}