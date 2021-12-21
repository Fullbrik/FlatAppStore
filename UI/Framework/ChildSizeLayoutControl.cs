using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	/*
	public abstract class ChildSizeLayoutControl : LayoutControl
	{
        
		public override bool PerferExpandToParentWidth => perferExpandToParentWidth;
		public override bool PerferExpandToParentHeight => perferExpandToParentHeight;
		private bool perferExpandToParentWidth;
		private bool perferExpandToParentHeight;

		public ReadOnlyCollection<Control> ControlsThatPerferToExpandToParentWidth { get => controlsThatPerferToExpandToParentWidth.AsReadOnly(); }
		private readonly List<Control> controlsThatPerferToExpandToParentWidth = new List<Control>();

		public ReadOnlyCollection<Control> ControlsThatPerferToExpandToParentHeight { get => controlsThatPerferToExpandToParentHeight.AsReadOnly(); }
		private readonly List<Control> controlsThatPerferToExpandToParentHeight = new List<Control>();

		private Vector2 preferredSize = new Vector2();

		public override Vector2 GetMinPreferredSize()
		{
			return preferredSize;
		}

		public override void Invalidate()
		{
			UpdatePreferredSize();

			base.Invalidate();
			//UpdatePreferredSize();
		}

		private void UpdatePreferredSize()
		{
			StartChildLoop();
			preferredSize = new Vector2();
			perferExpandToParentWidth = false;
			perferExpandToParentHeight = false;
			controlsThatPerferToExpandToParentWidth.Clear();
			controlsThatPerferToExpandToParentHeight.Clear();
			foreach (var child in Children)
			{

				if (child.PerferExpandToParentWidth)
				{
					controlsThatPerferToExpandToParentWidth.Add(child);
					perferExpandToParentWidth = true;
				}
				else
				{
					preferredSize.X += child.GetMinPreferredSize().X;
				}

				if (child.PerferExpandToParentHeight)
				{
					controlsThatPerferToExpandToParentHeight.Add(child);
					perferExpandToParentHeight = true;
				}
				else
				{
					preferredSize.Y += child.GetMinPreferredSize().Y;
				}
			}
			EndChildLoop();
		}
	}
    */
}