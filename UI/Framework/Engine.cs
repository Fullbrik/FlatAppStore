using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using FlatAppStore.UI.Framework.Assets;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public class Engine : IDisposable
    {
        private bool disposedValue;

        public InputManager InputManager { get; } = new InputManager();

        public RootControl Root { get; }
        public NavigatorControl Navigator { get; } = new NavigatorControl();

        public float TargetResolutionWidth { get; set; } = 800;

        public Engine()
        {
            Root = new RootControl(this);
        }

        public void Run(Control starting)
        {
            Initialize();

            Navigator.AddChild(starting);

            PrintControlTree();

            DoEngineLoop();
        }

        private void Initialize()
        {
            InitializeWindow();
            InitializeDrivers();
            LoadAssets();
            InitializeControls();
        }

        private void InitializeDrivers()
        {
            InputManager.LoadInputDriver(new KeyboardInputDriver());
        }

        private void LoadAssets()
        {
            Localization.LocalizationManager.LoadLanguages();
            Icons.LoadIcons();
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
            Raylib.InitWindow(1280, 800, "Flathub App Store");
            Raylib.SetExitKey(KeyboardKey.KEY_NULL);
            //Raylib.SetWindowState(ConfigFlags.FLAG_FULLSCREEN_MODE | ConfigFlags.FLAG_VSYNC_HINT);
            Raylib.SetWindowState(ConfigFlags.FLAG_VSYNC_HINT | ConfigFlags.FLAG_WINDOW_RESIZABLE);

        }

        private void DoEngineLoop()
        {
            // Relayout the root widget
            Root.IsLayoutReady = true;
            Root.GetSize(new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));
            Root.Invalidate();

            var control = Navigator.GetChild<LayoutControl>(0).GetChild<LayoutControl>(0);

            while (!Raylib.WindowShouldClose() && Navigator.Children.Count() > 0)
            {
                if (Raylib.IsWindowResized())
                {
                    Root.Invalidate();
                }

                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.GRAY);

                Root.Draw();

                //Raylib_cs.Raylib.DrawRectangleLinesEx(control.Transform.DrawBounds, 5, Raylib_cs.Color.DARKGREEN);

                //Raylib.DrawFPS(10, 10);
                Raylib.EndDrawing();

                Updateables.UpdateAll(Raylib.GetFrameTime());
            }

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

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                InputManager.Dispose();
                Icons.UnloadIcons();
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        ~Engine()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}