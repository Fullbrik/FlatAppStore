using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class Engine
	{
		public static Engine Instance { get; } = new Engine();

		public InputManager InputManager { get; } = new InputManager();

		public RootControl Root { get; } = new RootControl();
		public NavigatorControl Navigator { get; } = new NavigatorControl();

		public float TargetResolutionWidth { get; set; } = 800;

		public void Run(/*float targetScale,/*int targetWidth, int targetHeight, CanvasScaleDirection scaleDirection, */ Control starting)
		{
			Initialize();

			//Canvas.TargetResolution = targetScale;
			// Canvas.TargetWidth = targetWidth;
			// Canvas.TargetHeight = targetHeight;
			// Canvas.ScaleDirection = scaleDirection;

			Navigator.AddChild(starting);

			PrintControlTree();

			DoEngineLoop();
		}

		private void Initialize()
		{
			InitializeDrivers();
			InitializeControls();
			InitializeWindow();
		}

		public void InitializeDrivers()
		{
			InputManager.LoadInputDriver(new KeyboardInputDriver());
		}

		private void InitializeControls()
		{
			Root.AddChild(Navigator);
			Updateables.RegisterGlobalUpdateable(Root);
			Root.Initialize(null, null);
		}

		private void InitializeWindow()
		{
			//Raylib.InitWindow(Raylib.GetScreenWidth(), Raylib.GetScreenHeight(), "Flathub App Store");
			Raylib.InitWindow(500, 500, "Flathub App Store");
			Raylib.SetExitKey(KeyboardKey.KEY_NULL);
			//Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE | ConfigFlags.FLAG_VSYNC_HINT);
			Raylib.SetWindowState(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);

		}

		private void DoEngineLoop()
		{
			var textureResolution = Raymath.Vector2Normalize(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight())) * TargetResolutionWidth;
			var renderTexture = Raylib.LoadRenderTexture((int)textureResolution.X, (int)textureResolution.Y);

			while (!Raylib.WindowShouldClose() && Navigator.Children.Count() > 0)
			{
				if (Raylib.IsWindowResized())
				{
					Raylib.UnloadRenderTexture(renderTexture);

					textureResolution = Raymath.Vector2Normalize(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight())) * TargetResolutionWidth;
					renderTexture = Raylib.LoadRenderTexture((int)textureResolution.X, (int)textureResolution.Y);

					//Root.Invalidate();
				}

				Root.Invalidate();

				Raylib.BeginDrawing();

				Raylib.ClearBackground(Color.BLACK);

				Raylib.BeginTextureMode(renderTexture);
				Root.Draw(null);
				Raylib.EndTextureMode();

				Raylib.DrawTexturePro(
					renderTexture.texture,
					new Rectangle(0, 0, renderTexture.texture.width, -renderTexture.texture.height),
					new Rectangle(0, 0, renderTexture.texture.width, renderTexture.texture.height),//Raylib.GetScreenWidth(), Raylib.GetScreenHeight()),
					Vector2.Zero, 0, Color.WHITE);//(renderTexture.texture, Vector2.Zero, 0, Raylib.GetScreenHeight() / textureResolution.Y, Color.WHITE);

				Raylib.DrawFPS(10, 10);
				Raylib.EndDrawing();

				Updateables.UpdateAll(Raylib.GetFrameTime());
			}

			Raylib.UnloadRenderTexture(renderTexture);
			Raylib.CloseWindow();
		}

		public void PrintControlTree()
		{
			PrintControlTreePart(Root, "");
		}

		private void PrintControlTreePart(Control control, string prefix)
		{
			System.Console.WriteLine(prefix + control.GetType().Name);

			if (control is LayoutControl layout)
			{
				foreach (var child in layout.Children)
				{
					PrintControlTreePart(child, prefix + '\t');
				}
			}
		}
	}
}