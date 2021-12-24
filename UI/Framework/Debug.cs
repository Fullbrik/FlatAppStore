using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
    public static class Debug
    {
        public static bool DrawDebug { get; set; } = true;

        public static Color DebugColor { get; set; } = Color.DARKGREEN;

        private static Control controlToDrawDebug;

        public static void DrawControlBounds(Control control)
        {
            controlToDrawDebug = control;
        }

        public static void Draw()
        {
            if (DrawDebug)
            {
                // Control debug
                if (controlToDrawDebug != null) Raylib.DrawRectangleLinesEx(controlToDrawDebug.Transform.DrawBounds, 5, DebugColor);
            }
        }

        public static void DoNothing() { }
    }
}