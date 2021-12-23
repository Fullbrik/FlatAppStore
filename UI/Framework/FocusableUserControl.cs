using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public abstract class FocusableUserControl : UserControl
	{
		public static FocusableUserControl CurrentFocusedWidget { get; private set; }

		// If our parent or our parent's parent is a scroll layout
		public ScrollLayoutControl ScrollParent { get => (Parent is ScrollLayoutControl) ? Parent as ScrollLayoutControl : (Parent != null && Parent.Parent is ScrollLayoutControl) ? Parent.Parent as ScrollLayoutControl : null; }

		public bool IsFocused { get => isFocused; }
		private bool isFocused = false;

		public virtual int FocusBorderThickness { get => 5; }
		public virtual Color FocusBorderColor { get => Color.WHITE; }

		public float FocusBorderAmount { get; set; } = 0; // Used for animation

		public void Focus()
		{
			// Unfocus the previous widget
			if (CurrentFocusedWidget != null)
			{
				CurrentFocusedWidget.isFocused = false;
				CurrentFocusedWidget.OnUnfocused();
			}

			// Make sure we are scrolled to
			if (ScrollParent != null)
			{
				if (ScrollParent.ShouldScrollHorizontal)
					ScrollParent.AnimateProperty<int>("HorizontalScroll").To((int)(Transform.LocalBounds.x - Transform.DrawBounds.width), .2f).Start();

				if (ScrollParent.ShouldScrollVertical)
					ScrollParent.AnimateProperty<int>("VerticalScroll").To((int)(Transform.LocalBounds.y - Transform.DrawBounds.height), .2f).Start();
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

		protected override void Initialized()
		{
			base.Initialized();
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