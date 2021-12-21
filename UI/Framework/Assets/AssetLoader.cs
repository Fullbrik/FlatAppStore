using System.IO;
using System.Reflection;
using Raylib_cs;

namespace FlatAppStore.UI.Framework.Assets
{
	public static class AssetLoader
	{
		public static string RootDir { get => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }

		public static Texture2D LoadTexture2D(string path)
		{
			string fullPath = ExpandAssetPath(path);

			var texture = Raylib.LoadTexture(fullPath);

			return texture;
		}

		public static void UnloadTexture2D(Texture2D texture)
		{
			Raylib.UnloadTexture(texture);
		}

		private static string ExpandAssetPath(string path)
		{
			var platformPath = path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

			if (platformPath.StartsWith(Path.DirectorySeparatorChar)) platformPath = platformPath.Remove(0, 1); // Remove leading /

			return Path.Combine(RootDir, platformPath);
		}
	}
}