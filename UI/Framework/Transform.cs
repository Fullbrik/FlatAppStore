using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public abstract class Transform : PropertyAnimateable
	{
		public Control Control { get; }
		public Transform ParentTransform { get => Control?.Parent?.Transform; }

		public Transform(Control control)
		{
			Control = control;
		}

		public float OffsetX { get; set; }
		public float OffsetY { get; set; }

		public float OffsetWidth { get; set; }
		public float OffsetHeight { get; set; }

		// public Rectangle Bounds
		// {
		// 	get => bounds;
		// 	protected set
		// 	{
		// 		bounds = value;

		// 		// Set the global bounds to also include the parent's bounds
		// 		GlobalBounds = (Control.Parent != null && Control.Parent.Transform != null) ?
		// 				new Rectangle(
		// 					value.x + Control.Parent.Transform.GlobalBounds.x,
		// 					value.y + Control.Parent.Transform.GlobalBounds.y,
		// 					value.width, value.height)
		// 				: value;
		// 	}
		// }
		// private Rectangle bounds;
		// public Rectangle GlobalBounds { get; private set; }

		public Rectangle Bounds { get => bounds; }
		private Rectangle bounds;


		protected override void OnPropertyUpdated<T>(Tween<T> tween)
		{
			base.OnPropertyUpdated(tween);
			Control.Invalidate();
		}

		public void UpdateBounds()
		{
			var baseX = (ParentTransform != null) ? ParentTransform.Bounds.x : 0;
			var baseY = (ParentTransform != null) ? ParentTransform.Bounds.y : 0;

			var localBounds = GetLocalBounds();

			bounds = new Rectangle(baseX + localBounds.x + OffsetX, baseY + localBounds.y + OffsetY, localBounds.width + OffsetWidth, localBounds.height + OffsetHeight);
		}

		protected abstract Rectangle GetLocalBounds();
	}
}