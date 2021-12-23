using System;
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

    public class SimpleDirectionLayoutControl : LayoutControl, IFocusLayoutProvider
    {
        public event Action<FocusableUserControl> ChildGetFocus;

        public LayoutDirection LayoutDirection { get; set; }
        public CrossAxisAlignment DefaultCrossAxisAlignment { get; set; } = CrossAxisAlignment.Begin;

        public IFocusLayoutProvider FocusProviderUp { get; set; }
        public IFocusLayoutProvider FocusProviderDown { get; set; }
        public IFocusLayoutProvider FocusProviderLeft { get; set; }
        public IFocusLayoutProvider FocusProviderRight { get; set; }

        public override bool PerferExpandToParentWidth => perferExpandHorizontal;

        public override bool PerferExpandToParentHeight => perferExpandVertical;

        private bool perferExpandHorizontal, perferExpandVertical;

        public SimpleDirectionLayoutControl() { }

        public SimpleDirectionLayoutControl(LayoutDirection layoutDirection)
        {
            LayoutDirection = layoutDirection;
        }


        protected override Transform CreateTransform(Control control)
        {
            return new SimpleDirectionLayoutControlTransform(control, DefaultCrossAxisAlignment);
        }

        protected override Vector2 LayoutChildrenAndGetSize(Vector2 maxSize)
        {
            perferExpandHorizontal = false;
            perferExpandVertical = false;

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

                if (child.PerferExpandToParentWidth) perferExpandHorizontal = true;
                if (child.PerferExpandToParentHeight) perferExpandVertical = true;

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
            float currentWidth = 0;
            float currentHeight = 0;
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

                        currentWidth = x + width;

                        if (childSize.Y > currentHeight) currentHeight = childSize.Y;
                        break;
                    case LayoutDirection.Vertical:
                        GetLayoutValuesForChild(child, child.PerferExpandToParentHeight, expandedChildrenCount, childSize.Y, childSize.X, maxSize.Y, maxSize.X, totalNonFillerSize, out x, out height, out width);

                        if (prevChild != null) // If there are not previous children, we are the starting one
                            y = prevChild.Transform.LocalBounds.y + prevChild.Transform.LocalBounds.height;

                        currentHeight = y + height;
                        if (childSize.X > currentWidth) currentWidth = childSize.X;
                        break;
                    default: // If there is an error, just have the child fill out as much as possible
                        width = childSize.X;
                        height = childSize.Y;
                        break;
                }

                SetChildLocalBounds(child, new Raylib_cs.Rectangle(x, y, width, height));

                prevChild = child;
            }

            return new Vector2(currentWidth, currentHeight);
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

        public FocusableUserControl FocusGetFirst()
        {
            for (int i = 0; i < Children.Count; i++)
            {
                if (Children[i] is FocusableUserControl child) return child;
            }

            return null;
        }

        public FocusableUserControl FocusGetRight(FocusableUserControl focusableUserControl)
        {
            if (LayoutDirection == LayoutDirection.Horizontal)
            {
                int index = Children.IndexOf(focusableUserControl);

                if (index < 0) throw new System.Exception("Cannot find next widget because the current on isn't in this collection");

                for (int i = index + 1; i != index; i++) // Move to the right, starting at the next index. If we get back to the index, then return null because we couldn't find anything.
                {
                    if (i >= Children.Count)
                        i = 0; // If we reach the furthest right, loop back around

                    if (Children[i] is FocusableUserControl child) return child;
                }

                return null;
            }

            return FocusProviderRight?.FocusGetFirst(); // Return the first focusable of the provider to the right.
        }

        public FocusableUserControl FocusGetLeft(FocusableUserControl focusableUserControl)
        {
            if (LayoutDirection == LayoutDirection.Horizontal)
            {
                int index = Children.IndexOf(focusableUserControl);

                if (index < 0) throw new System.Exception("Cannot find next widget because the current on isn't in this collection");

                for (int i = index - 1; i != index; i--) // Move to the left, starting at the next index. If we get back to the index, then return null because we couldn't find anything.
                {
                    if (i < 0) i = Children.Count - 1; // If we reach the furthest left, loop back around

                    if (Children[i] is FocusableUserControl child) return child;
                }

                return null;
            }

            return FocusProviderLeft?.FocusGetFirst(); // Return the first focusable of the provider to the left.
        }

        public FocusableUserControl FocusGetUp(FocusableUserControl focusableUserControl)
        {
            if (LayoutDirection == LayoutDirection.Vertical)
            {
                int index = Children.IndexOf(focusableUserControl);

                if (index < 0) throw new System.Exception("Cannot find next widget because the current on isn't in this collection");

                for (int i = index - 1; i != index; i--) // Move up, starting at the next index. If we get back to the index, then return null because we couldn't find anything.
                {
                    if (i < 0) i = Children.Count - 1; // If we reach the furthest up, loop back around

                    if (Children[i] is FocusableUserControl child) return child;
                }

                return null;
            }

            return FocusProviderUp?.FocusGetFirst(); // Return the first focusable of the provider to the up.
        }

        public FocusableUserControl FocusGetDown(FocusableUserControl focusableUserControl)
        {
            if (LayoutDirection == LayoutDirection.Vertical)
            {
                int index = Children.IndexOf(focusableUserControl);

                if (index < 0) throw new System.Exception("Cannot find next widget because the current on isn't in this collection");

                for (int i = index + 1; i != index; i++) // Move down, starting at the next index. If we get back to the index, then return null because we couldn't find anything.
                {
                    if (i >= Children.Count)
                        i = 0; // If we reach the furthest down, loop back around

                    if (Children[i] is FocusableUserControl child)
                        return child;
                }

                return null;
            }

            return FocusProviderDown?.FocusGetFirst(); // Return the first focusable of the provider to the down.
        }

        public void OnChildGetFocus(FocusableUserControl control)
        {
            ChildGetFocus?.Invoke(control);
        }
    }

    public class SimpleDirectionLayoutControlTransform : Transform
    {
        public CrossAxisAlignment CrossAxisAlignment { get; set; }

        public SimpleDirectionLayoutControlTransform(Control control, CrossAxisAlignment crossAxisAlignment) : base(control)
        {
            CrossAxisAlignment = crossAxisAlignment;
        }
    }
}