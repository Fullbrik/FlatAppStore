using Raylib_cs;

namespace FlatAppStore.UI.Models
{
    public record ApplicationData(string Name, string ID, string Developer, string Description, Texture2D Icon, string[] ScreenshotURLs)
    {
        public static ApplicationData Default { get; } = new ApplicationData("Error: No App Name", "error.no.id", "Error: No Dev", UI.Framework.LoremIpsum.Generate(1), UI.Framework.Assets.Icons.NoApplicationIcon, new string[] { });
    }
}