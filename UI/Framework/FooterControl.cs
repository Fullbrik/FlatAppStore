using System;
using System.Collections.Generic;
using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class FooterControl : UserControl
	{
		public string PageTitle { get => pageTitleLabel.Text; set => pageTitleLabel.Text = value; }
		private LabelControl pageTitleLabel = new LabelControl("Loading...", Raylib_cs.Color.WHITE);

		public IReadOnlyDictionary<ControllerButton, string> ActionNames
		{
			get => actionNamess;
			set
			{
				actionNamess = value;

				UpdateActionsList();
			}
		}
		private IReadOnlyDictionary<ControllerButton, string> actionNamess;
		private SimpleDirectionLayoutControl actionsLayout = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);

		public override bool PerferExpandToParentWidth => true;
		public override bool PerferExpandToParentHeight => false;

		// public override Vector2 GetMinPreferredSize()
		// {
		// 	return new Vector2(50, 50);
		// }

		protected override Control Build()
		{
			AddChild(new GradientRectControl(new Raylib_cs.Color(0, 0, 0, 0), Raylib_cs.Color.BLACK, Raylib_cs.Color.BLACK, new Raylib_cs.Color(0, 0, 0, 0)) { ExpandToParent = true }, (t) => { t.OffsetY = -5; t.OffsetHeight = -30; }); // Add Shaddow
			AddChild(new RectControl(Raylib_cs.Color.BLACK) { ExpandToParent = true }); // Add background

			var control = new SimpleDirectionLayoutControl(LayoutDirection.Horizontal);

			control.AddChild(
				new PaddingControl(
					new RoundedRectContainerControl(
						new PaddingControl(
							new LabelControl("STORE", Raylib_cs.Color.BLACK),
							5, 5, 15, 15
						),
						new Vector2(1, 1)
					),
				10, 10, 15, 20)
			);

			control.AddChild(
				new PaddingControl(
					pageTitleLabel, 15, 15, 0, 0
				)
			);

			control.AddChild(new FillerControl());

			control.AddChild(actionsLayout);

			return control;
		}

		private void UpdateActionsList()
		{
			actionsLayout.RemoveAllChildren();

			foreach (var actionName in ActionNames)
			{
				// Key is the button, while value is the name of the action
				actionsLayout.AddChild(new PaddingControl(new ActionButtonDisplayControl(actionName.Value, actionName.Key), 0, 0, 10, 10));
			}
		}
	}
}