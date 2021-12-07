using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
    public abstract class ChildSizeLayoutControl : LayoutControl
    {
        public override bool PerferExpandToParent => perferExpandToParent;
        private bool perferExpandToParent;

        public ReadOnlyCollection<Control> ControlsThatPerferToExpandToParent { get => controlsThatPerferToExpandToParent.AsReadOnly(); }
        private readonly List<Control> controlsThatPerferToExpandToParent = new List<Control>();

        private Vector2 preferredSize = new Vector2();

        public override Vector2 GetMinPreferredSize()
        {
            return preferredSize;
        }

        public override void Invalidate()
        {
            UpdatePreferredSize();

            base.Invalidate();
        }

        private void UpdatePreferredSize()
        {
            StartChildLoop();
            preferredSize = new Vector2();
            perferExpandToParent = false;
            controlsThatPerferToExpandToParent.Clear();
            foreach (var child in Children)
                if (child.PerferExpandToParent) { controlsThatPerferToExpandToParent.Add(child); perferExpandToParent = true; }
                else preferredSize += child.GetMinPreferredSize();
            EndChildLoop();
        }
    }
}