using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public enum LayoutDirection
    {
        Horizontal,
        Vertical
    }

    public class SimpleDirectionLayoutControl : ChildSizeLayoutControl
    {
        public LayoutDirection LayoutDirection { get; set; }

        public SimpleDirectionLayoutControl() { }

        public SimpleDirectionLayoutControl(LayoutDirection layoutDirection)
        {
            LayoutDirection = layoutDirection;
        }


        protected override Transform CreateTransform(Control control)
        {
            return new SimpleDirectionLayoutControlTransform(control, this);
        }
    }

    public class SimpleDirectionLayoutControlTransform : Transform
    {
        SimpleDirectionLayoutControl LayoutControl { get; }

        public SimpleDirectionLayoutControlTransform(Control control, SimpleDirectionLayoutControl layoutControl) : base(control)
        {
            LayoutControl = layoutControl;
        }

        protected override Raylib_cs.Rectangle GetLocalBounds()
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
                    height = ParentTransform.Bounds.height;

                    if (Control.PerferExpandToParent) throw new System.NotImplementedException();
                    else width = Control.GetMinPreferredSize().X;

                    if (LayoutData.prevChild != null) // If there are not previous children, we are the starting one
                        x = LayoutData.prevChild.Transform.LocalBounds.x + LayoutData.prevChild.Transform.LocalBounds.width;

                    break;
                default:
                    throw new System.Exception("Invalid layout direction.");
            }

            return new Raylib_cs.Rectangle(x, y, width, height);
        }
    }
}