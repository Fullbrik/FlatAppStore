using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public abstract class ScreenControl : LayoutControl
	{
		public override bool PerferExpandToParent => true;

		protected override Transform CreateTransform(Control control)
		{
			return new FillParentTransform(control);
		}

		protected override void Initialized()
		{
			AddChild(BuildScreen());
		}

		protected abstract Control BuildScreen();

		public override Vector2 GetMinPreferredSize()
		{
			throw new System.NotImplementedException();
		}
	}
}