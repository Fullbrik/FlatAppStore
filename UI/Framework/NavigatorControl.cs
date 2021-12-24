using System.Linq;
using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class NavigatorControl : LayoutControl
	{
		public override NavigatorControl Navigator => this;

		public Color FadeColor { get; } = new Color(11, 19, 28, 255);
		public int FadeAmount { get; set; } = 0;

		public int OpeningFadeAmount { get; set; } = 255;

		public int HeaderHeight { get; } = 50;
		public HeaderControl Header { get; } = new HeaderControl();

		public int FooterHeight { get; } = 50;
		public FooterControl Footer { get; } = new FooterControl();

		public Control CurrentPage { get; set; }

		public override bool PerferExpandToParentWidth => true;

		public override bool PerferExpandToParentHeight => true;

		private Control removedPage;

		protected override Transform CreateTransform(Control control)
		{
			return new NavigatorControlTransform(control, FooterHeight);
		}

		protected override void Initialized()
		{
			Header.Initialize(this, new Transform(Header));
			Header.Transform.OffsetY = -HeaderHeight;
			Footer.Initialize(this, new Transform(Footer));
			Footer.Transform.OffsetY = FooterHeight;

			base.Initialized();

			PlayOpeningAnimation();
		}

		public void PlayOpeningAnimation()
		{
			// Opening animations for header and footer and opening fade
			Header.Transform.AnimateProperty<float>("OffsetY")
				.StartWith(-HeaderHeight)
				.Delay(.5f)
				.To(0, .5f)
				.Start();

			Footer.Transform.AnimateProperty<float>("OffsetY")
				.StartWith(FooterHeight)
				.Delay(.5f)
				.To(0, .5f)
				.Start();

			AnimateProperty<int>(nameof(OpeningFadeAmount))
				.StartWith(255)
				.Delay(1.3f)
				.To(0, .5f)
				.Start();
		}

		public void PlayClosingAnimation()
		{
			// Opening animations for header and footer and opening fade
			Header.Transform.AnimateProperty<float>("OffsetY")
				.StartWith(0)
				.Delay(.5f)
				.To(-HeaderHeight, .5f)
				.Delay(0.1f)
				.Do(() => RemoveAllChildren()) // Clear all children when done
				.Start();

			Footer.Transform.AnimateProperty<float>("OffsetY")
				.StartWith(0)
				.Delay(.5f)
				.To(FooterHeight, .5f)
				.Start();

			AnimateProperty<int>(nameof(OpeningFadeAmount))
				.StartWith(0)
				.To(255, .5f)
				.Start();
		}

		public override void Invalidate()
		{
			base.Invalidate();

			Header.Invalidate();
			Footer.Invalidate();
		}

		protected override void AddedChild(Control child)
		{
			child.Transform.OffsetX = Transform.DrawBounds.width;

			base.AddedChild(child);

			// Play an animation on the current page
			if (CurrentPage != null)
			{
				AnimateProperty<int>("FadeAmount")
					.StartWith(0)
					.To(255, .4f)
					.Delay(.3f)
					.To(0, 0)
					.Start();
			}

			// Set the new current page
			CurrentPage = child;

			// Animate the page to slide in
			child.Transform.AnimateProperty<float>("OffsetX")
				.StartWith(Transform.DrawBounds.width)
				.Delay(0.5f)
				.To(0, 0.2f)
				.Start();

			UpdateFooter();
		}

		protected override void RemovedChild(Control child)
		{
			base.RemovedChild(child);

			if (CurrentPage == child)
			{
				CurrentPage = Children.LastOrDefault();

				removedPage = child;

				child.Transform.AnimateProperty<float>("OffsetX")
					.To(Transform.DrawBounds.width, 0.2f)
					.Do(() => removedPage = null)
					.Start();
			}

			UpdateFooter();
		}

		public void UpdateFooter()
		{
			if (CurrentPage != null && CurrentPage is ScreenControl screen)
			{
				Footer.PageTitle = screen.Title;
				Footer.ActionNames = screen.ActionNames;
			}
			else
			{
				Footer.PageTitle = "";
				Footer.ActionNames = new System.Collections.Generic.Dictionary<ControllerButton, string>();
			}

			Footer.Invalidate();
		}

		public override void Draw()
		{
			StartChildLoop();
			for (int i = 0; i < Children.Count; i++)
			{
				if (i == Children.Count - 1) // If this is the last child, draw the fade first
					if (FadeAmount != 0)
						Raylib.DrawRectangleRec(Transform.DrawBounds, new Color(FadeColor.r, FadeColor.g, FadeColor.b, FadeAmount));

				Children[i].Draw();
			}
			EndChildLoop();

			if (removedPage != null)
				removedPage.Draw();

			Raylib.DrawRectangleRec(Transform.DrawBounds, new Color(FadeColor.r, FadeColor.g, FadeColor.b, OpeningFadeAmount));

			Header.Draw();
			Footer.Draw();
		}

		public override void OnInput(ControllerButton button)
		{
			if (OpeningFadeAmount != 0) return; // We don't want to do input in the middle of the opening animation.

			// Only pass input to current page
			if (CurrentPage != null) CurrentPage.OnInput(button);
		}

		public override void OnScroll(float amount)
		{
			if (OpeningFadeAmount != 0) return; // We don't want to do input in the middle of the opening animation.

			base.OnScroll(amount);
		}

		public override void OnMouseOver(Vector2 globalMousePosition, bool leftButtonDown, bool middleButtonDown, bool rightButtonDown)
		{
			if (OpeningFadeAmount != 0) return; // We don't want to do input in the middle of the opening animation.

			// Only pass mouse to current page
			if (CurrentPage != null) CurrentPage.OnMouseOver(globalMousePosition, leftButtonDown, middleButtonDown, rightButtonDown);
		}

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			foreach (var child in Children)
			{
				float x = 0;
				float y = 0;

				var childMaxSize = maxSize - new Vector2(0, FooterHeight);

				// If the child has the proper transform, apply padding
				if (child.Transform is NavigatorControlTransform transform)
				{
					x = transform.PaddingLeft;
					y = transform.PaddingRight;
					childMaxSize -= new Vector2(transform.PaddingLeft + transform.PaddingRight, transform.PaddingTop + transform.PaddingBottom);
				}

				var childSize = child.GetSize(childMaxSize);

				SetChildLocalBounds(child, new Raylib_cs.Rectangle(x, y, maxSize.X, maxSize.Y - FooterHeight));
			}

			//Layout Header
			{
				var childSize = Header.GetSize(new Vector2(maxSize.X, HeaderHeight));
				SetChildLocalBounds(Header, new Raylib_cs.Rectangle(0, 0, maxSize.X, HeaderHeight));
			}

			// Layout Footer
			{
				var childSize = Footer.GetSize(new Vector2(maxSize.X, FooterHeight));
				SetChildLocalBounds(Footer, new Raylib_cs.Rectangle(0, maxSize.Y - FooterHeight, maxSize.X, FooterHeight));
			}

			return maxSize;
		}
	}

	public class NavigatorControlTransform : Transform
	{
		private int footerHeight = 0;

		public float PaddingTop { get; set; }
		public float PaddingBottom { get; set; }
		public float PaddingRight { get; set; }
		public float PaddingLeft { get; set; }

		public NavigatorControlTransform(Control control, int footerHeight) : base(control)
		{
			this.footerHeight = footerHeight;
		}
	}
}