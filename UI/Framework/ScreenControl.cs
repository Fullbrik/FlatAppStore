using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public abstract class ScreenControl : UserControl
	{
		public override bool PerferExpandToParent => true;

		public override Vector2 GetMinPreferredSize()
		{
			throw new System.NotImplementedException();
		}
	}
}