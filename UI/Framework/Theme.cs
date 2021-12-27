using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public class Theme
    {
        public static Theme Default { get; } = new Theme();

        public Color PrimaryColor { get; set; } = new Color(11, 19, 28, 255);
        public Color SecondaryColor { get; set; } = new Color(35, 38, 46, 255);
    }
}