namespace FlatAppStore.UI.Framework
{
    public abstract class UserControl : LayoutControl
    {
        protected override Transform CreateTransform(Control control)
		{
			return new FillParentTransform(control);
		}

		protected override void Initialized()
		{
			AddChild(BuildScreen());
		}

		protected abstract Control BuildScreen();
    }
}