using System.Numerics;
using Raylib_cs;

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
		public float TargetResolution { get; set; }
		//public int TargetWidth { get; set; }
		//public int TargetHeight { get; set; }

		//public CanvasScaleDirection ScaleDirection { get; set; }

		public Vector2 ScaleAmount
		{
			get
			{
				Vector2 viewport = new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
				var viewportNormalized = Raymath.Vector2Normalize(viewport);

				return viewport / (TargetResolution * viewportNormalized);
			}
			// get => ScaleDirection switch
			// {
			// 	CanvasScaleDirection.Width => TargetWidth / Raylib.GetScreenWidth(), // The width we want / The actual width
			// 	CanvasScaleDirection.None or _ => 1 // Don't do any scaling
			// };
		}

		public void DrawRectangleRounded(Rectangle rec, float roundness, int segments, Color color)
		{
			Raylib.DrawRectangleRounded(ScaleRect(rec), roundness, segments, color);
		}

		public void DrawRectangleRec(Rectangle rec, Color color)
		{
			Raylib.DrawRectangleRec(ScaleRect(rec), color);
		}

		private Rectangle ScaleRect(Rectangle rectangle)
		{
			return new Rectangle(rectangle.x * ScaleAmount.X, rectangle.y * ScaleAmount.Y, rectangle.width * ScaleAmount.X, rectangle.height * ScaleAmount.Y);
		}
	}
}