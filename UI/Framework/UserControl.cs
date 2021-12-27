using System.Numerics;
using System.Threading.Tasks;

namespace FlatAppStore.UI.Framework
{
	public abstract class UserControl : LayoutControl
	{
		public bool IsLoading { get => isLoading; }
		private bool isLoading;

		public Task LoadTask { get; private set; }


		public virtual bool DoLoad { get => false; }
		public override bool PerferExpandToParentWidth => false;
		public override bool PerferExpandToParentHeight => false;

		protected override Transform CreateTransform(Control control)
		{
			return new Transform(control);
		}

		public async Task WaitUntilLoadingComplete()
		{
			while (IsLoading)
			{
				await Task.Delay(1);
			}
		}

		protected override async void Initialized()
		{
			isLoading = true;
			LoadTask = Load();
			await LoadTask;
			Rebuild();
			//Parent.ReLayoutChildren();
			isLoading = false;
		}

		public void Rebuild()
		{
			PauseLayout();

			// Remove all previous children
			RemoveAllChildren();

			// Rebuild everything
			AddChild(Build());

			ReLayoutChildren();

			ResumeLayout();
		}

		protected abstract Task Load();

		protected abstract Control Build();

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			var currentSize = Vector2.Zero;

			foreach (var child in Children)
			{
				var childSize = child.GetSize(maxSize);

				if (childSize.X > currentSize.X) currentSize.X = childSize.X;
				if (childSize.Y > currentSize.Y) currentSize.Y = childSize.Y;

				SetChildLocalBounds(child, new Raylib_cs.Rectangle(0, 0, childSize.X, childSize.Y));
			}

			return currentSize;
		}
	}
}