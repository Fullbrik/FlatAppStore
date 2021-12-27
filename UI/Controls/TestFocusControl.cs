using System.Threading.Tasks;
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

		protected override Task Load()
		{
			return Task.CompletedTask;
		}

		protected override Control Build()
		{
			var control = new RectControl(Color);
			control.Size = new System.Numerics.Vector2(500, 300);

			return control;
		}

		public override void DoAction()
		{
			throw new System.NotImplementedException();
		}
	}
}