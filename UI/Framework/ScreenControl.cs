using System;
using System.Collections.Generic;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public abstract class ScreenControl : UserControl
	{
		public abstract string Title { get; }

		public IReadOnlyDictionary<ControllerButton, string> ActionNames { get => actionNames; }
		private readonly Dictionary<ControllerButton, string> actionNames = new Dictionary<ControllerButton, string>();

		private readonly Dictionary<ControllerButton, Action> actionButtons = new Dictionary<ControllerButton, Action>();

		public override bool PerferExpandToParentWidth => true;
		public override bool PerferExpandToParentHeight => true;

		protected void AddAction(ControllerButton button, string name, Action action)
		{
			actionNames.Add(button, name);
			actionButtons.Add(button, action);
		}

		public override void OnInput(ControllerButton button)
		{
			base.OnInput(button);

			if (actionButtons.ContainsKey(button)) actionButtons[button]();
		}
	}
}