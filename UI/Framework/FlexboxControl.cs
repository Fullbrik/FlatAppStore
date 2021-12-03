using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public enum FlexDirection
	{
		Horizontal,
		Vertical
	}

	public class FlexboxControl : LayoutControl
	{
		public FlexDirection Direction { get; set; }

		public override bool PerferExpandToParent => true;

		public FlexboxControl() { }

		public FlexboxControl(FlexDirection direction)
		{
			Direction = direction;
		}

		protected override Transform CreateTransform(Control control)
		{
			return new FlexboxControlTransform(control, this);
		}

		public override Vector2 GetMinPreferredSize()
		{
			throw new System.NotImplementedException();
		}
	}

	public class FlexboxControlTransform : Transform
	{
		public FlexboxControl Flexbox { get; }

		public FlexboxControlTransform(Control control, FlexboxControl flexbox) : base(control)
		{
			Flexbox = flexbox;
		}

		protected override Raylib_cs.Rectangle GetLocalBounds()
		{
			throw new System.NotImplementedException();
		}
	}
}