using System.Linq;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class NavigatorControl : LayoutControl
	{
		public override NavigatorControl Navigator => this;

		public Control CurrentPage { get; set; }

		public override bool PerferExpandToParent => true;

		private Control removedPage;

		protected override Transform CreateTransform(Control control)
		{
			return new FillParentTransform(control);
		}

		protected override void AddedChild(Control child)
		{
			base.AddedChild(child);

			// Play an animation on the current page
			if (CurrentPage != null)
			{
				CurrentPage.Transform.AnimateProperty<float>("OffsetX")
					.To(20, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Start();

				CurrentPage.Transform.AnimateProperty<float>("OffsetY")
					.To(20, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Start();

				CurrentPage.Transform.AnimateProperty<float>("OffsetWidth")
					.To(-40, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Start();

				CurrentPage.Transform.AnimateProperty<float>("OffsetHeight")
					.To(-40, 0.15f)
					.Delay(0.1f)
					.To(0, 0)
					.Start();
			}

			// Set the new current page
			CurrentPage = child;

			// Animate the page to slide in
			child.Transform.AnimateProperty<float>("OffsetX")
				.To(Transform.Bounds.width, 0)
				.To(0, 0.2f)
				.Start();
		}

		protected override void RemovedChild(Control child)
		{
			base.RemovedChild(child);

			if (CurrentPage == child)
			{
				CurrentPage = Children.Last();

				removedPage = child;

				child.Transform.AnimateProperty<float>("OffsetX")
					.To(Transform.Bounds.width, 0.2f)
					.Do(() => removedPage = null)
					.Start();
			}
		}

		public override void Draw(Canvas canvas)
		{
			base.Draw(canvas);

			if (removedPage != null)
				removedPage.Draw(canvas);
		}

		public override void OnInput(ControllerButton button)
		{
			// Only pass input to current page
			if (CurrentPage != null) CurrentPage.OnInput(button);
		}

		public override Vector2 GetMinPreferredSize()
		{
			throw new System.NotImplementedException();
		}
	}
}