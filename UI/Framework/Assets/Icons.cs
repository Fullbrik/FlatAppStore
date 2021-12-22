using System.Collections.Generic;
using Raylib_cs;

namespace FlatAppStore.UI.Framework.Assets
{
	public static class Icons
	{
		public static Texture2D Search { get; private set; }

		private static readonly List<Texture2D> loadedTextures = new List<Texture2D>();

		public static void LoadIcons()
		{
			Search = LoadIcon("/Assets/Icons/Search.png");
		}

		public static void UnloadIcons()
		{
			foreach (var texture in loadedTextures)
			{
				AssetLoader.UnloadTexture2D(texture);
			}
		}

		private static Texture2D LoadIcon(string path)
		{
			var texture = AssetLoader.LoadTexture2D(path);
			loadedTextures.Add(texture);
			return texture;
		}
	}
}