using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public class RootControl : LayoutControl, IUpdateable
	{
		public bool IsLayoutReady { get; set; } = false;

		public override InputManager InputManager => inputManager;
		private InputManager inputManager;

		public RootControl(Engine engine)
		{
			inputManager = engine.InputManager;
		}

		public override bool PerferExpandToParentWidth => true;
		public override bool PerferExpandToParentHeight => true;

		public void Update(float deltaTime)
		{
			// Check for input
			InputManager.CheckInput(OnInput, OnScroll);
		}

		protected override Transform CreateTransform(Control control)
		{
			return new Transform(control);
		}

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			if (IsLayoutReady)
			{
				foreach (var child in Children)
				{
					child.GetSize(maxSize);

					SetChildLocalBounds(child, new Rectangle(0, 0, maxSize.X, maxSize.Y));
				}
			}

			return maxSize;
		}
	}
}