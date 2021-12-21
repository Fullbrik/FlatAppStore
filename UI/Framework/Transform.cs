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
		//public Rectangle GlobalBounds { get; private set; }
		// public Rectangle LocalBounds
		// {
		// 	get => localBounds;
		// 	set
		// 	{
		// 		localBounds = value;
		// 		UpdateTransform();
		// 	}
		// }
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

	/*
	public record struct TransformLayoutData(int Index, int Count, Control prevChild);

	public abstract class Transform : PropertyAnimateable
	{
		public Control Control { get; }
		public Transform ParentTransform { get => Control?.Parent?.Transform; }

		public TransformLayoutData LayoutData { get; internal set; }

		public Transform(Control control)
		{
			Control = control;
		}

		public float OffsetX { get; set; }
		public float OffsetY { get; set; }

		public float OffsetWidth { get; set; }
		public float OffsetHeight { get; set; }

		public Vector2 Center { get => center; }
		private Vector2 center;

		public Rectangle Bounds { get => bounds; }
		private Rectangle bounds;

		public Rectangle LocalBounds { get => localBounds; }
		private Rectangle localBounds;


		protected override void OnPropertyUpdated<T>(Tween<T> tween)
		{
			base.OnPropertyUpdated(tween);
			Control.Invalidate();
		}

		public void UpdateBounds()
		{
			var baseX = (ParentTransform != null) ? ParentTransform.Bounds.x : 0;
			var baseY = (ParentTransform != null) ? ParentTransform.Bounds.y : 0;

			localBounds = GetLocalBounds();
			localBounds.x += OffsetX;
			localBounds.y += OffsetY;
			localBounds.width += OffsetWidth;
			localBounds.height += OffsetHeight;


			bounds = new Rectangle(baseX + localBounds.x, baseY + localBounds.y, localBounds.width, localBounds.height);

			center = new Vector2(Bounds.x + Bounds.width / 2, Bounds.y + Bounds.height / 2);
		}

		protected abstract Rectangle GetLocalBounds();
	}
    */
}