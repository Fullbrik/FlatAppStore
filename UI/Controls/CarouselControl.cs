using Raylib_cs;
using FlatAppStore.UI.Framework;
using System.Numerics;

namespace FlatAppStore.UI.Controls
{
	public class CarouselControl : UserControl, IFocusLayoutProvider
	{
		public string Title { get; set; } = "";
		public Color BackgroundColor { get; set; } = new Color(35, 38, 46, 255);

		public override bool PerferExpandToParentWidth => true;

		private ScrollLayoutControl carouselPartsScroll;
		private SimpleDirectionLayoutControl carouselPartsLayout;

		public CarouselControl() { }

		public CarouselControl(string title)
		{
			Title = title;
		}

		public CarouselControl(string title, Color backgroundColor)
		{
			Title = title;
			BackgroundColor = backgroundColor;
		}

		public FocusableUserControl FocusGetDown(FocusableUserControl focusableUserControl)
		{
			return carouselPartsLayout.FocusGetDown(focusableUserControl);
		}

		public FocusableUserControl FocusGetFirst()
		{
			return carouselPartsLayout.FocusGetFirst();
		}

		public FocusableUserControl FocusGetLeft(FocusableUserControl focusableUserControl)
		{
			return carouselPartsLayout.FocusGetLeft(focusableUserControl);
		}

		public FocusableUserControl FocusGetRight(FocusableUserControl focusableUserControl)
		{
			return carouselPartsLayout.FocusGetRight(focusableUserControl);
		}

		public FocusableUserControl FocusGetUp(FocusableUserControl focusableUserControl)
		{
			return carouselPartsLayout.FocusGetUp(focusableUserControl);
		}

		protected override Control Build()
		{
			var control = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			// Make room for header
			control.AddChild(new SpacerControl(0, 50));

			// Add title and its padding
			control.AddChild(new PaddingControl(new LabelControl(Title, Color.WHITE), 10, 10, 0, 0));

			// Create the scroller
			carouselPartsScroll = new ScrollLayoutControl(LayoutDirection.Horizontal);

			// Build carousel parts
			carouselPartsLayout = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);

			carouselPartsLayout.AddChild(new TestFocusControl(Color.BROWN));
			carouselPartsLayout.AddChild(new SpacerControl(10, 0));
			carouselPartsLayout.AddChild(new TestFocusControl(Color.ORANGE));
			carouselPartsLayout.AddChild(new SpacerControl(10, 0));
			carouselPartsLayout.AddChild(new TestFocusControl(Color.BLUE));
			carouselPartsLayout.AddChild(new SpacerControl(10, 0));
			carouselPartsLayout.AddChild(new TestFocusControl(Color.DARKPURPLE));

			// Focus the first one
			carouselPartsLayout.FocusGetFirst()?.Focus();

			// Add it to the scroll
			carouselPartsScroll.AddChild(carouselPartsLayout);

			// Add the scroll
			control.AddChild(carouselPartsScroll);

			// and return
			return new PaddingControl(control, 0, 20, 10, 10);
		}

		public override void Draw()
		{
			Raylib.DrawRectangleGradientEx(new Rectangle(Transform.DrawBounds.x, Transform.DrawBounds.y + 10, Transform.DrawBounds.width, Transform.DrawBounds.height + 30), Raylib_cs.Color.BLACK, new Raylib_cs.Color(0, 0, 0, 0), new Raylib_cs.Color(0, 0, 0, 0), Raylib_cs.Color.BLACK); // Draw shaddow
			Raylib.DrawRectangleRec(Transform.DrawBounds, BackgroundColor); // Draw background

			base.Draw();

			if (carouselPartsScroll != null)
			{
				// Draw left cover
				if (carouselPartsScroll.HorizontalScroll > 0)
				{
					// Draw solid part
					Raylib.DrawRectangle((int)Transform.DrawBounds.x, (int)carouselPartsScroll.Transform.DrawBounds.y, 10, (int)carouselPartsScroll.Transform.DrawBounds.height, BackgroundColor);

					// Draw shaddow
					Raylib.DrawRectangleGradientH((int)Transform.DrawBounds.x + 10, (int)carouselPartsScroll.Transform.DrawBounds.y, 5, (int)carouselPartsScroll.Transform.DrawBounds.height, BackgroundColor, new Color(0, 0, 0, 0));
				}

				// Draw right cover
				if (carouselPartsScroll.HorizontalScroll < carouselPartsScroll.MaxHorizontalScroll)
				{
					// Draw solid part
					Raylib.DrawRectangle((int)(carouselPartsScroll.Transform.DrawBounds.x + carouselPartsScroll.Transform.DrawBounds.width), (int)carouselPartsScroll.Transform.DrawBounds.y, 10, (int)carouselPartsScroll.Transform.DrawBounds.height, BackgroundColor);

					// Draw shaddow
					Raylib.DrawRectangleGradientH((int)(carouselPartsScroll.Transform.DrawBounds.x + carouselPartsScroll.Transform.DrawBounds.width - 5), (int)carouselPartsScroll.Transform.DrawBounds.y, 5, (int)carouselPartsScroll.Transform.DrawBounds.height, new Color(0, 0, 0, 0), BackgroundColor);
				}
			}
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			return new Vector2(maxSize.X, base.GetSize(maxSize).Y);
		}
	}
}