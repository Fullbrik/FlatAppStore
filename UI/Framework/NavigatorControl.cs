using System.Linq;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class NavigatorControl : LayoutControl
	{
		public override NavigatorControl Navigator => this;

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
			Footer.Initialize(this, new Transform(Footer));

			base.Initialized();
		}

		public override void Invalidate()
		{
			base.Invalidate();

			Header.Invalidate();
			Footer.Invalidate();
		}

		protected override void AddedChild(Control child)
		{
			base.AddedChild(child);

			// Play an animation on the current page
			if (CurrentPage != null)
			{
				CurrentPage.Transform.AnimateProperty<float>("PaddingLeft")
					.To(20, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Start();

				CurrentPage.Transform.AnimateProperty<float>("PaddingRight")
					.To(20, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Start();

				CurrentPage.Transform.AnimateProperty<float>("PaddingTop")
					.To(20, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Start();

				CurrentPage.Transform.AnimateProperty<float>("PaddingBottom")
					.BindUpdated(() => ReLayoutChildren()) // We want to relayout the children as we animate. We only need to do this on one tween though
					.To(20, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Do(() => ReLayoutChildren()) // Make sure we do one last relayout after we animate
					.Start();
			}

			// Set the new current page
			CurrentPage = child;

			// Animate the page to slide in
			child.Transform.AnimateProperty<float>("OffsetX")
				.To(Transform.DrawBounds.width, 0)
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
			base.Draw();

			if (removedPage != null)
				removedPage.Draw();

			Header.Draw();
			Footer.Draw();
		}

		public override void OnInput(ControllerButton button)
		{
			// Only pass input to current page
			if (CurrentPage != null) CurrentPage.OnInput(button);
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