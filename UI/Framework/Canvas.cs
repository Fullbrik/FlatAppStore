namespace FlatAppStore.UI.Framework
{
    public enum CanvasScaleDirection
    {
        None,
        Width,
        Height
    }

    public class Canvas
    {
        public int TargetWidth { get; set; }
        public int TargetHeight { get; set; }

        public CanvasScaleDirection ScaleDirection { get; set; }
    }
}