using System;
using System.Collections.Generic;

namespace FlatAppStore.UI.Framework
{
	public abstract class LayoutControl : Control
	{
		public IEnumerable<Control> Children => children;
		private readonly List<Control> children = new List<Control>();

		private Control toBeAdded = null;

		public void AddChild(Control child)
		{
			toBeAdded = child;
			QueuePostChildLoop(() =>
			{
				if (!children.Contains(child))
				{
					children.Add(child);
					child.Initialize(this, CreateTransform(child));

					AddedChild(child);

					toBeAdded = null;

					Invalidate();
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

		public override void Invalidate()
		{
			base.Invalidate();

			StartChildLoop();
			foreach (var child in children)
				child.Invalidate();
			EndChildLoop();
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

		private void StartChildLoop()
		{
			isInChildLoop = true;
		}

		private void EndChildLoop()
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