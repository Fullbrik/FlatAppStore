using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public abstract class FocusableUserControl : UserControl
	{
		public static FocusableUserControl CurrentFocusedWidget { get; private set; }

		public bool IsFocused { get => isFocused; }
		private bool isFocused = false;

		public virtual int FocusBorderThickness { get => 5; }
		public virtual Color FocusBorderColor { get => Color.GREEN; }

		public float FocusBorderAmount { get; set; } = 0; // Used for animation

		public void Focus()
		{
			if (CurrentFocusedWidget != null)
			{
				CurrentFocusedWidget.isFocused = false;
				CurrentFocusedWidget.OnUnfocused();
			}

			isFocused = true;
			CurrentFocusedWidget = this;
			OnFocused();
		}

		protected virtual void OnFocused()
		{
			AnimateProperty<float>(nameof(FocusBorderAmount))
				.To(1, .1f)
				.Start();
		}
		protected virtual void OnUnfocused()
		{
			AnimateProperty<float>(nameof(FocusBorderAmount))
				.To(0, .1f)
				.Start();
		}

		public override void Draw()
		{
			base.Draw();

			Raylib.DrawRectangleLinesEx(Transform.DrawBounds, (int)(FocusBorderThickness * FocusBorderAmount), FocusBorderColor);
		}

		public override void OnInput(ControllerButton button)
		{
			base.OnInput(button);

			if (IsFocused) // Make sure we are focused
			{
				if (Parent is IFocusLayoutProvider focusLayoutProvider)
				{
					switch (button)
					{
						case ControllerButton.DPAD_Up:
							focusLayoutProvider.FocusGetUp(this)?.Focus();
							Parent.AbsorbNextInput(this);
							break;
						case ControllerButton.DPAD_Down:
							focusLayoutProvider.FocusGetDown(this)?.Focus();
							Parent.AbsorbNextInput(this);
							break;
						case ControllerButton.DPAD_Left:
							focusLayoutProvider.FocusGetLeft(this)?.Focus();
							Parent.AbsorbNextInput(this);
							break;
						case ControllerButton.DPAD_Right:
							focusLayoutProvider.FocusGetRight(this)?.Focus();
							Parent.AbsorbNextInput(this);
							break;
						default:
							break;
					}
				}
			}
		}
	}
}