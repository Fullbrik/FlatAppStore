using FlatAppStore.UI.Framework;

namespace FlatAppStore.UI.Screens
{
    public class SearchScreen : ScreenControl
    {
        protected override Control BuildScreen()
        {
            var layout = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

            var header = new RectControl(new Raylib_cs.Color(53, 66, 74, 50));
            header.Height = 100;
            layout.AddChild(header);

            return layout;
        }
    }
}