using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Controls
{
	public class TestFocusControl : FocusableUserControl
	{
		public Color Color { get; set; }

		public TestFocusControl() { }

		public TestFocusControl(Color color)
		{
			Color = color;
		}

		protected override Control Build()
		{
			var control = new RectControl(Color);
			control.Size = new System.Numerics.Vector2(200, 400);

			return control;
		}
	}
}