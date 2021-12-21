using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace FlatAppStore.UI.Framework
{
	public abstract class LayoutControl : Control
	{
		public IEnumerable<Control> Children => children;
		private readonly List<Control> children = new List<Control>();

		public bool IsLayoutPaused { get => isLayoutPaused || (Parent?.IsLayoutPaused ?? false); }
		private bool isLayoutPaused = false;

		private Control toBeAdded = null; // We don't want to render this

		public void AddChild(Control child) => AddChild(child, null);

		public void AddChild(Control child, Action<Transform> initTransform)
		{
			if (IsInitialized) toBeAdded = child; // If we aren't initialized yet, this value will never be reset and will cause bugs

			QueuePostChildLoop(() =>
			{
				if (!children.Contains(child))
				{
					children.Add(child);

					if (initTransform != null) // Bind event to transform initialization
						child.OnTransformInitialized += initTransform;

					if (IsInitialized) // Only initialize children if we are initialized.
					{
						child.Initialize(this, CreateTransform(child));

						AddedChild(child);

						toBeAdded = null;

						Invalidate();
					}
				}
			});
		}

		public void RemoveChild(Control child)
		{
			QueuePostChildLoop(() =>
			{
				children.Remove(child);

				child.Removed();

				RemovedChild(child);

				Invalidate();
			});
		}

		public void RemoveAllChildren()
		{
			if (children.Count > 0) // Only do it if we have children
			{
				// We pause the layout so we don't invalidate a bunch
				PauseLayout();

				// Loop through all children, and remove them
				StartChildLoop();
				foreach (var child in Children)
					RemoveChild(child);
				EndChildLoop();

				// We then resume
				ResumeLayout();
			}
		}

		public Control GetChild(int index)
		{
			return children[index];
		}

		public T GetChild<T>(int index)
			where T : Control
		{
			return children[index] as T;
		}


		public void PauseLayout()
		{
			isLayoutPaused = true;
		}

		public void ResumeLayout()
		{
			if (isLayoutPaused)
			{
				isLayoutPaused = false;
				Invalidate();
			}
		}

		protected abstract Transform CreateTransform(Control control);

		protected override void Initialized()
		{
			base.Initialized();

			foreach (var child in children) // Now that we initialized, we can finnish initializing all of our children
			{
				child.Initialize(this, CreateTransform(child));

				AddedChild(child);
			}
		}

		public override void Invalidate()
		{
			// if (!IsLayoutPaused)
			// {
			// 	StartChildLoop();
			// 	Control prevChild = null;
			// 	for (int i = 0; i < children.Count; i++)
			// 	{
			// 		var child = children[i];
			// 		child.Transform.LayoutData = new TransformLayoutData(i, children.Count, prevChild);
			// 		child.Invalidate();

			// 		prevChild = child;
			// 	}
			// 	EndChildLoop();
			// }

			base.Invalidate();

			if (!IsLayoutPaused)
			{
				ReLayoutChildren();
			}

			StartChildLoop();
			foreach (var child in children)
			{
				child.Invalidate();
			}
			EndChildLoop();
		}

		private Vector2 prevMaxSize = Vector2.Zero;
		private Vector2 prevSize = Vector2.Zero;

		public override Vector2 GetSize(Vector2 maxSize)
		{
			prevMaxSize = maxSize;

			StartChildLoop();
			var size = LayoutChildrenAndGetSize(maxSize);
			prevSize = size;
			EndChildLoop();

			return size;
		}
		public void ReLayoutChildren()
		{
			StartChildLoop();
			var size = LayoutChildrenAndGetSize(prevMaxSize);
			EndChildLoop();

			if (prevSize != size) Parent.ReLayoutChildren(); // We will have to relayout everything because our size changed
															 //Invalidate();
		}
		protected abstract Vector2 LayoutChildrenAndGetSize(Vector2 maxSize);

		protected void SetChildLocalBounds(Control child, Rectangle bounds)
		{
			//child.Transform.UpdateTransform(bounds);
			child.Transform.LocalBounds = bounds;
		}

		public override void Draw()
		{
			// We don't need to draw anything for ourselves since this control just does layout.

			StartChildLoop();
			foreach (var child in Children)
				if (child != toBeAdded)
					child.Draw();
			EndChildLoop();
		}

		public override void OnInput(ControllerButton button)
		{
			base.OnInput(button);

			StartChildLoop();
			foreach (var child in Children)
				child.OnInput(button);
			EndChildLoop();
		}

		private readonly Queue<Action> onEndChildloopQueue = new Queue<Action>();
		bool isInChildLoop = false;

		protected void StartChildLoop()
		{
			isInChildLoop = true;
		}

		protected void EndChildLoop()
		{
			if (isInChildLoop)
			{
				while (onEndChildloopQueue.TryDequeue(out Action action))
				{
					action();
				}

				isInChildLoop = false;
			}
		}

		protected void QueuePostChildLoop(Action action)
		{
			if (isInChildLoop)
				onEndChildloopQueue.Enqueue(action);
			else
				action();
		}

		protected virtual void AddedChild(Control child) { }
		protected virtual void RemovedChild(Control child) { }
	}
}