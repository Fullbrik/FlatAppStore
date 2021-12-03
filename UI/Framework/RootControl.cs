using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class RootControl : LayoutControl, IUpdateable
	{
		public override bool PerferExpandToParent => true;

		public override Vector2 GetMinPreferredSize()
		{
			throw new System.NotImplementedException();
		}

		public void Update(float deltaTime)
		{
			// Check for input
			Engine.Instance.InputManager.CheckInput(OnInput);
		}

		protected override Transform CreateTransform(Control control)
		{
			return new RootControlTransform(control);
		}
	}

	public class RootControlTransform : Transform
	{
		public RootControlTransform(Control control) : base(control)
		{
		}

		protected override Rectangle GetLocalBounds()
		{
			return new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
		}
	}
}