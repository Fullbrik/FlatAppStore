using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Raylib_cs;

namespace FlatAppStore.UI.Framework.Assets
{
	public static class AssetLoader
	{
		public static string RootDir { get => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }

		public static IEnumerable<string> EnumerateAssetFolder(string path)
		{
			string fullPath = ExpandAssetPath(path);

			if (!Directory.Exists(fullPath)) throw new System.Exception($"Unable to find asset folder \"{path}\"");

			return Directory.EnumerateFiles(fullPath).Select((path) => Path.GetRelativePath(RootDir, path)); // We want asset paths to be relative to the root
		}

		public static Texture2D LoadTexture2D(string path)
		{
			string fullPath = ExpandAssetPath(path);
			EnsureFullPath(fullPath, path);

			var texture = Raylib.LoadTexture(fullPath);

			return texture;
		}

		public static void UnloadTexture2D(Texture2D texture)
		{
			Raylib.UnloadTexture(texture);
		}

		public static Localization.Language LoadLanguage(string path)
		{
			string fullPath = ExpandAssetPath(path);
			EnsureFullPath(fullPath, path);

			string name = Path.GetFileNameWithoutExtension(path);

			var text = File.ReadAllText(fullPath);

			return Localization.Language.Parse(name, text);
		}

		private static string ExpandAssetPath(string path)
		{
			var platformPath = path.Replace('/', Path.DirectorySeparatorChar).Replace('\\', Path.DirectorySeparatorChar);

			if (platformPath.StartsWith(Path.DirectorySeparatorChar)) platformPath = platformPath.Remove(0, 1); // Remove leading /

			return Path.Combine(RootDir, platformPath);
		}

		private static void EnsureFullPath(string fullPath, string path)
		{
			if (!File.Exists(fullPath)) throw new System.Exception($"Unable to find asset \"{path}\"");
		}
	}
}