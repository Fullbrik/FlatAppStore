using Raylib_cs;
using FlatAppStore.UI.Framework;
using System.Numerics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlatAppStore.UI.Framework
{
	public class CarouselControl<TData> : UserControl, IFocusLayoutProvider
	{
		public string Title { get; set; } = "";
		public Color BackgroundColor { get; set; } = new Color(0, 0, 0, 0);

		public override bool PerferExpandToParentWidth => true;

		private ScrollLayoutControl carouselPartsScroll;
		private SimpleDirectionLayoutControl carouselPartsLayout = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);

		private Rectangle leftMoverBounds, rightMoverBounds;
		private bool shouldDrawLeftMover = false, shouldDrawRightMover = false;
		public int LeftMoverTransparency { get; set; } = 0;
		public int RightMoverTransparency { get; set; } = 0;

		public float Spacing { get; set; } = 10;
		public IEnumerable<TData> Data { get; set; }
		public Func<TData, FocusableUserControl> Builder { get; set; }
		public Func<CarouselControl<TData>, Task> DataLoader { get; set; }

		public CarouselControl() { }

		public CarouselControl(Func<TData, FocusableUserControl> builder)
		{
			Builder = builder;
		}

		public CarouselControl(string title, Func<TData, FocusableUserControl> builder) : this(builder)
		{
			Title = title;
		}

		public CarouselControl(string title, Color backgroundColor, Func<TData, FocusableUserControl> builder) : this(builder)
		{
			Title = title;
			BackgroundColor = backgroundColor;
		}

		public void BindChildGetFocus(Action<FocusableUserControl> onGetFocus)
		{
			carouselPartsLayout.ChildGetFocus += onGetFocus;
		}

		public IFocusLayoutProvider FocusProviderUp { get => carouselPartsLayout?.FocusProviderUp; set => carouselPartsLayout.FocusProviderUp = value; }
		public IFocusLayoutProvider FocusProviderDown { get => carouselPartsLayout?.FocusProviderDown; set => carouselPartsLayout.FocusProviderDown = value; }
		public IFocusLayoutProvider FocusProviderLeft { get => carouselPartsLayout?.FocusProviderLeft; set => carouselPartsLayout.FocusProviderLeft = value; }
		public IFocusLayoutProvider FocusProviderRight { get => carouselPartsLayout?.FocusProviderRight; set => carouselPartsLayout.FocusProviderRight = value; }

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

		public void OnChildGetFocus(FocusableUserControl control)
		{
			carouselPartsLayout.OnChildGetFocus(control);
		}

		public void Focus()
		{
			// Focus the first one
			carouselPartsLayout.FocusGetFirst()?.Focus();
		}

		protected override Task Load()
		{
			if (DataLoader != null) return DataLoader(this);
			else return Task.CompletedTask;
		}

		protected override Control Build()
		{
			var control = new SimpleDirectionLayoutControl(LayoutDirection.Vertical);

			// Add title and its padding
			control.AddChild(new PaddingControl(new LabelControl(Title, Color.WHITE), 10, 10, 20, 20));

			// Create the scroller
			carouselPartsScroll = new ScrollLayoutControl(LayoutDirection.Horizontal);

			// Build carousel parts
			carouselPartsLayout.RemoveAllChildren();

			// Leading spacer
			carouselPartsLayout.AddChild(new SpacerControl(20, 0));

			// Add layout for all data items
			if (Data != null)
			{
				foreach (var item in Data)
				{
					carouselPartsLayout.AddChild(Builder(item));
					carouselPartsLayout.AddChild(new SpacerControl(Spacing, 0)); // And spacing
				}
			}

			// Ending spacer
			carouselPartsLayout.AddChild(new SpacerControl(20, 0));

			// Add it to the scroll
			carouselPartsScroll.AddChild(carouselPartsLayout);

			// Add the scroll
			control.AddChild(carouselPartsScroll);

			// and return
			return new PaddingControl(control, 10, 30, 0, 0);
		}

		public override void Draw()
		{
			Raylib.DrawRectangleGradientEx(new Rectangle(Transform.DrawBounds.x, Transform.DrawBounds.y + 10, Transform.DrawBounds.width, Transform.DrawBounds.height + 30), Raylib_cs.Color.BLACK, new Raylib_cs.Color(0, 0, 0, 0), new Raylib_cs.Color(0, 0, 0, 0), Raylib_cs.Color.BLACK); // Draw shaddow
			Raylib.DrawRectangleRec(Transform.DrawBounds, BackgroundColor); // Draw background

			base.Draw();

			// Recalculate and draw left mover
			leftMoverBounds = new Rectangle(carouselPartsScroll.Transform.DrawBounds.x, carouselPartsScroll.Transform.DrawBounds.y, 50, carouselPartsScroll.Transform.DrawBounds.height);
			Raylib.DrawRectangleGradientH((int)leftMoverBounds.x, (int)leftMoverBounds.y, (int)leftMoverBounds.width, (int)leftMoverBounds.height, new Color(BackgroundColor.r, BackgroundColor.g, BackgroundColor.b, LeftMoverTransparency), new Color(0, 0, 0, 0));
			Raylib.DrawText("<", (int)leftMoverBounds.x + 5, (int)leftMoverBounds.y + (int)(leftMoverBounds.height / 2f) - 50, 100, new Color(0, 0, 0, LeftMoverTransparency));

			// Recalculate and draw right mover
			rightMoverBounds = new Rectangle(carouselPartsScroll.Transform.DrawBounds.x + carouselPartsScroll.Transform.DrawBounds.width - 50, carouselPartsScroll.Transform.DrawBounds.y, 50, carouselPartsScroll.Transform.DrawBounds.height);
			Raylib.DrawRectangleGradientH((int)rightMoverBounds.x, (int)rightMoverBounds.y, (int)rightMoverBounds.width, (int)rightMoverBounds.height, new Color(0, 0, 0, 0), new Color(BackgroundColor.r, BackgroundColor.g, BackgroundColor.b, RightMoverTransparency));
			Raylib.DrawText(">", (int)rightMoverBounds.x + 15, (int)rightMoverBounds.y + (int)(rightMoverBounds.height / 2f) - 50, 100, new Color(0, 0, 0, RightMoverTransparency));
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			return new Vector2(maxSize.X, base.GetSize(maxSize).Y);
		}

		public override void OnMouseOver(Vector2 globalMousePosition, bool leftButtonDown, bool middleButtonDown, bool rightButtonDown)
		{
			base.OnMouseOver(globalMousePosition, leftButtonDown, middleButtonDown, rightButtonDown);

			// Check left mover bounds
			if (Raylib.CheckCollisionPointRec(globalMousePosition, leftMoverBounds))
			{
				if (!shouldDrawLeftMover)
				{
					shouldDrawLeftMover = true;
					AnimateProperty<int>(nameof(LeftMoverTransparency))
						.To(255, .2f)
						.Start();
				}

				if (leftButtonDown)
					if (carouselPartsLayout.FocusGetFirst().IsFocused)
						carouselPartsLayout.FocusGetLeft(carouselPartsLayout.FocusGetFirst()).Focus();
					else
						carouselPartsLayout.FocusGetFirst().Focus();
			}
			else
			{
				if (shouldDrawLeftMover)
				{
					shouldDrawLeftMover = false;
					AnimateProperty<int>(nameof(LeftMoverTransparency))
						.To(0, .2f)
						.Start();
				}
			}

			// Check right mover bounds
			if (Raylib.CheckCollisionPointRec(globalMousePosition, rightMoverBounds))
			{
				if (!shouldDrawRightMover)
				{
					shouldDrawRightMover = true;
					AnimateProperty<int>(nameof(RightMoverTransparency))
						.To(255, .2f)
						.Start();
				}

				if (leftButtonDown)
					if (carouselPartsLayout.FocusGetFirst().IsFocused)
						carouselPartsLayout.FocusGetRight(carouselPartsLayout.FocusGetFirst()).Focus();
					else
						carouselPartsLayout.FocusGetFirst().Focus();
			}
			else
			{
				if (shouldDrawRightMover)
				{
					shouldDrawRightMover = false;
					AnimateProperty<int>(nameof(RightMoverTransparency))
						.To(0, .2f)
						.Start();
				}
			}
		}
	}
}