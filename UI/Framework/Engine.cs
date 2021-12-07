using System.Collections.Generic;
using System.Linq;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public class Engine
    {
        public static Engine Instance { get; } = new Engine();

        public InputManager InputManager { get; } = new InputManager();

        public RootControl Root { get; } = new RootControl();
        public NavigatorControl Navigator { get; } = new NavigatorControl();

        public void Run(Control starting)
        {
            Initialize();

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
            Raylib.SetWindowState(ConfigFlags.FLAG_VSYNC_HINT);
        }

        private void DoEngineLoop()
        {
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();

                Raylib.ClearBackground(Color.BLACK);
                Root.Draw();
                Raylib.DrawFPS(10, 10);
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
    }
}