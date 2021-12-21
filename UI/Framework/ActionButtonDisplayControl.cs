using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class ActionButtonDisplayControl : UserControl
	{
		public string Name { get; set; }
		public ControllerButton Button { get; set; }

		private TextureControl iconControl = new TextureControl();

		public ActionButtonDisplayControl() { }

		public ActionButtonDisplayControl(string name, ControllerButton button)
		{
			Name = name;
			Button = button;
		}

		protected override Control Build()
		{
			var control = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);

			iconControl.ScaleMode = TextureScaleMode.FitHeight;
			control.AddChild(iconControl, (t) =>
			{
				if (t is SimpleDirectionLayoutControlTransform transform)
				{
					transform.CrossAxisAlignment = CrossAxisAlignment.Center;
				}

			});

			control.AddChild(new LabelControl(Name, Raylib_cs.Color.WHITE), (t) =>
			{
				if (t is SimpleDirectionLayoutControlTransform transform)
				{
					transform.CrossAxisAlignment = CrossAxisAlignment.Center;
				}
			});

			return control;
		}

		public override void Invalidate()
		{
			base.Invalidate();

			UpdateIcon();
		}

		public override void OnInput(ControllerButton button)
		{
			base.OnInput(button);

			UpdateIcon();
		}

		private void UpdateIcon()
		{
			iconControl.Texture = InputManager.CurrentDriver.GetButtonIcon(Button);
		}
	}
}