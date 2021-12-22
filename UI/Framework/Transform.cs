using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class Transform : PropertyAnimateable
	{
		public Control Control { get; }
		public Transform ParentTransform { get => Control?.Parent?.Transform; }

		public float OffsetX { get; set; }
		public float OffsetY { get; set; }

		public float OffsetWidth { get; set; }
		public float OffsetHeight { get; set; }

		public Rectangle DrawBounds { get => new Rectangle(globalBounds.x + OffsetX, globalBounds.y + OffsetY, globalBounds.width + OffsetWidth, globalBounds.height + OffsetHeight); }
		private Rectangle globalBounds;
		public Rectangle LocalBounds { get; internal set; }

		public Transform(Control control)
		{
			Control = control;
		}

		protected override void OnPropertyUpdated<T>(Tween<T> tween)
		{
			base.OnPropertyUpdated(tween);
			Control.Invalidate();
		}

		public void UpdateTransform()
		{
			var baseX = (ParentTransform != null) ? ParentTransform.DrawBounds.x : 0;
			var baseY = (ParentTransform != null) ? ParentTransform.DrawBounds.y : 0;

			//LocalBounds = localBounds;

			globalBounds = new Rectangle(baseX + LocalBounds.x, baseY + LocalBounds.y, LocalBounds.width, LocalBounds.height);
		}
	}
}