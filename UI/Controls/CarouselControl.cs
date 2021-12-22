using Raylib_cs;
using FlatAppStore.UI.Framework;
using System.Numerics;

namespace FlatAppStore.UI.Controls
{
	public class CarouselControl : UserControl, IFocusLayoutProvider
	{
		public override bool PerferExpandToParentWidth => true;

		public Color BackgroundColor { get; set; } = new Color(35, 38, 46, 255);

		private SimpleDirectionLayoutControl mainControl;

		public FocusableUserControl FocusGetDown(FocusableUserControl focusableUserControl)
		{
			return mainControl.FocusGetDown(focusableUserControl);
		}

		public FocusableUserControl FocusGetFirst()
		{
			return mainControl.FocusGetFirst();
		}

		public FocusableUserControl FocusGetLeft(FocusableUserControl focusableUserControl)
		{
			return mainControl.FocusGetLeft(focusableUserControl);
		}

		public FocusableUserControl FocusGetRight(FocusableUserControl focusableUserControl)
		{
			return mainControl.FocusGetRight(focusableUserControl);
		}

		public FocusableUserControl FocusGetUp(FocusableUserControl focusableUserControl)
		{
			return mainControl.FocusGetUp(focusableUserControl);
		}

		protected override Control Build()
		{
			mainControl = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);



			mainControl.AddChild(new FlatAppStore.UI.Controls.TestFocusControl(Color.BROWN));
			mainControl.AddChild(new FlatAppStore.UI.Controls.TestFocusControl(Color.ORANGE));
			mainControl.AddChild(new FlatAppStore.UI.Controls.TestFocusControl(Color.BLUE));

			mainControl.FocusGetFirst()?.Focus();

			return new PaddingControl(mainControl, 60, 10, 10, 10);
		}

		public override void Draw()
		{
			Raylib.DrawRectangleGradientEx(new Rectangle(Transform.DrawBounds.x, Transform.DrawBounds.y + 10, Transform.DrawBounds.width, Transform.DrawBounds.height + 30), Raylib_cs.Color.BLACK, new Raylib_cs.Color(0, 0, 0, 0), new Raylib_cs.Color(0, 0, 0, 0), Raylib_cs.Color.BLACK); // Draw shaddow
			Raylib.DrawRectangleRec(Transform.DrawBounds, BackgroundColor); // Draw background

			base.Draw();
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			return new Vector2(maxSize.X, base.GetSize(maxSize).Y);
		}
	}
}