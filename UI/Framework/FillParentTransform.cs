namespace FlatAppStore.UI.Framework
{
	public class FillParentTransform : Transform
	{
		public FillParentTransform(Control control) : base(control)
		{
		}

		protected override Raylib_cs.Rectangle GetLocalBounds()
		{
			return new Raylib_cs.Rectangle(0, 0, ParentTransform.Bounds.width, ParentTransform.Bounds.height);
		}
	}
}