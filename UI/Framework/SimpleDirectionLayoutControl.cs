using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public enum LayoutDirection
	{
		Horizontal,
		Vertical
	}

	public enum CrossAxisAlignment
	{
		Begin,
		Center,
		End,
		Stretch
	}

	public class SimpleDirectionLayoutControl : LayoutControl
	{
		public LayoutDirection LayoutDirection { get; set; }

		// Will evaluate to check if we want to expand vertically (in which case we will fill the width) or we want to fill the main axis
		public override bool PerferExpandToParentWidth => LayoutDirection == LayoutDirection.Vertical || perferExpandMainAxis;

		public override bool PerferExpandToParentHeight => LayoutDirection == LayoutDirection.Horizontal || perferExpandMainAxis;

		private bool perferExpandMainAxis;

		public SimpleDirectionLayoutControl() { }

		public SimpleDirectionLayoutControl(LayoutDirection layoutDirection)
		{
			LayoutDirection = layoutDirection;
		}


		protected override Transform CreateTransform(Control control)
		{
			return new SimpleDirectionLayoutControlTransform(control);
		}

		protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
		{
			perferExpandMainAxis = false;

			// Get a count of all the children that want to expand on the main axis
			int expandedChildrenCount = Children.Where((child) => LayoutDirection switch
			{
				LayoutDirection.Horizontal => child.PerferExpandToParentWidth,
				LayoutDirection.Vertical => child.PerferExpandToParentHeight,
				_ => true
			}).Count();

			// Get all sizes first so we can layout any fillers later
			Dictionary<Control, Vector2> controlAndSize = new Dictionary<Control, Vector2>();
			float totalNonFillerSize = 0;
			foreach (var child in Children)
			{
				var childSize = child.GetSize(maxSize);
				controlAndSize.Add(child, childSize);

				switch (LayoutDirection)
				{
					case LayoutDirection.Horizontal:
						if (!child.PerferExpandToParentWidth) totalNonFillerSize += childSize.X;
						break;
					case LayoutDirection.Vertical:
						if (!child.PerferExpandToParentHeight) totalNonFillerSize += childSize.Y;
						break;
					default:
						break;
				}
			}

			Control prevChild = null;
			float currentMainAxisSize = 0;
			foreach (var child in Children)
			{
				float x = 0;
				float y = 0;

				float width = 0;
				float height = 0;

				var childSize = controlAndSize[child];

				switch (LayoutDirection)
				{
					case LayoutDirection.Horizontal:
						GetLayoutValuesForChild(child, child.PerferExpandToParentWidth, expandedChildrenCount, childSize.X, childSize.Y, maxSize.X, maxSize.Y, totalNonFillerSize, out y, out width, out height);

						if (prevChild != null) // If there are not previous children, we are the starting one
							x = prevChild.Transform.LocalBounds.x + prevChild.Transform.LocalBounds.width;

						currentMainAxisSize = x + width;
						break;
					case LayoutDirection.Vertical:
						GetLayoutValuesForChild(child, child.PerferExpandToParentHeight, expandedChildrenCount, childSize.Y, childSize.X, maxSize.Y, maxSize.X, totalNonFillerSize, out x, out height, out width);

						if (prevChild != null) // If there are not previous children, we are the starting one
							y = prevChild.Transform.LocalBounds.y + prevChild.Transform.LocalBounds.height;

						currentMainAxisSize = y + height;
						break;
					default: // If there is an error, just have the child fill out as much as possible
						width = childSize.X;
						height = childSize.Y;
						break;
				}

				SetChildLocalBounds(child, new Raylib_cs.Rectangle(x, y, width, height));

				prevChild = child;
			}

			if (expandedChildrenCount > 0)
			{
				perferExpandMainAxis = true;
				return maxSize;
			}
			else
			{
				return LayoutDirection switch
				{
					LayoutDirection.Horizontal => new Vector2(currentMainAxisSize, maxSize.Y),
					LayoutDirection.Vertical => new Vector2(maxSize.X, currentMainAxisSize),
					_ => maxSize // If there is an error, just fill the parent
				};
			}
		}

		private void GetLayoutValuesForChild(Control child, bool expandMainAxis, int expandedChildrenCount, float preferredMainAxis, float preferredCrossAxis, float parentMainAxisSize, float parentCrossAxisSize, float totalNonFillerSize, out float crossAxisPos, out float mainAxisSize, out float crossAxisSize)
		{
			crossAxisPos = 0;



			if (expandMainAxis)
			{
				mainAxisSize // If we want to expand, we expand to fill available space. This space is divided evenly for all controls that want this.
					= (parentMainAxisSize - totalNonFillerSize) // Get remaining space. Remember, the parent's preferred size is just the space not taken up by controls that want to fill
						/ expandedChildrenCount; // And divide it evenly

				crossAxisSize = parentCrossAxisSize; // Just stretch the whole thing
			}
			else
			{
				mainAxisSize = preferredMainAxis;
				crossAxisSize = preferredCrossAxis;

				switch ((child.Transform as SimpleDirectionLayoutControlTransform)?.CrossAxisAlignment)
				{
					case CrossAxisAlignment.Begin: // It is already begin aligned by default, so we don't need to do anything
						break;
					case CrossAxisAlignment.Center:
						var halfPerferredCross = crossAxisSize / 2;
						crossAxisPos += (parentCrossAxisSize / 2) - halfPerferredCross;
						break;
					case CrossAxisAlignment.End:
						throw new System.NotImplementedException();
					case CrossAxisAlignment.Stretch:
						crossAxisSize = parentCrossAxisSize;
						break;
					default:
						break;
				}
			}
		}
	}

	public class SimpleDirectionLayoutControlTransform : Transform
	{
		public CrossAxisAlignment CrossAxisAlignment { get; set; }

		public SimpleDirectionLayoutControlTransform(Control control) : base(control)
		{
		}

		/*protected override Raylib_cs.Rectangle GetLocalBounds()
		{
			float x = 0;
			float y = 0;

			float width = 0;
			float height = 0;

			switch (LayoutControl.LayoutDirection)
			{
				case LayoutDirection.Vertical:
					width = ParentTransform.Bounds.width;

					if (Control.PerferExpandToParent)
						height // If we want to expand, we expand to fill available space. This space is divided evenly for all controls that want this.
							= (ParentTransform.Bounds.height - LayoutControl.GetMinPreferredSize().Y) // Get remaining space
								/ LayoutControl.ControlsThatPerferToExpandToParent.Count; // And divide it evenly
					else
						height = Control.GetMinPreferredSize().Y;

					if (LayoutData.prevChild != null) // If there are not previous children, we are the starting one
						y = LayoutData.prevChild.Transform.LocalBounds.y + LayoutData.prevChild.Transform.LocalBounds.height;

					break;
				case LayoutDirection.Horizontal:
					var preferredSize = Control.PerferExpandToParent ? Vector2.Zero : Control.GetMinPreferredSize();

					GetLayoutValues(preferredSize.X, preferredSize.Y, ParentTransform.Bounds.width, ParentTransform.Bounds.height, LayoutControl.GetMinPreferredSize().X, out x, out y, out width, out height);

					if (LayoutData.prevChild != null) // If there are not previous children, we are the starting one
						x = LayoutData.prevChild.Transform.LocalBounds.x + LayoutData.prevChild.Transform.LocalBounds.width;
					break;
				default:
					throw new System.Exception("Invalid layout direction.");
			}

			return new Raylib_cs.Rectangle(x, y, width, height);
		}*/
	}
}