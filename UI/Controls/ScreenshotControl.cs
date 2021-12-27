using System.Threading.Tasks;
using FlatAppStore.UI.Framework;
using Raylib_cs;

namespace FlatAppStore.UI.Controls
{
	public class ScreenshotControl : FocusableUserControl
	{
		public Texture2D Screenshot { get => screenshot; set { screenshot = value; if (IsInitialized) Rebuild(); } }
		private Texture2D screenshot;

		public ScreenshotControl() { }

		public ScreenshotControl(Texture2D screenshot)
		{
			this.screenshot = screenshot;
		}

		protected override Task Load()
		{
			return Task.CompletedTask;
		}

		protected override Control Build()
		{
			return new TextureControl(Screenshot);
		}

		public override void DoAction()
		{

		}
	}
}