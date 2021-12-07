using System;
using System.Collections.Generic;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public abstract class LayoutControl : Control
	{
		public IEnumerable<Control> Children => children;
		private readonly List<Control> children = new List<Control>();

		private Control toBeAdded = null; // We don't want to render this

		public void AddChild(Control child)
		{
			if (IsInitialized) toBeAdded = child; // If we aren't initialized yet, this value will never be reset and will cause bugs
			QueuePostChildLoop(() =>
			{
				if (!children.Contains(child))
				{
					children.Add(child);

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
			base.Invalidate();

			StartChildLoop();
			Control prevChild = null;
			for (int i = 0; i < children.Count; i++)
			{
				var child = children[i];
				child.Transform.LayoutData = new TransformLayoutData(i, children.Count, prevChild);
				child.Invalidate();

				prevChild = child;
			}
			EndChildLoop();
		}

		public override void Draw(Canvas canvas)
		{
			// We don't need to draw anything for ourselves since this control just does layout.

			StartChildLoop();
			foreach (var child in Children)
				if (child != toBeAdded)
					child.Draw(canvas);
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