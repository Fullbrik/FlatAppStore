using System.Numerics;

namespace FlatAppStore.UI.Framework
{
	public class FillerControl : Control
	{
		public override bool PerferExpandToParentWidth => true;
		public override bool PerferExpandToParentHeight => true;

		public override void Draw()
		{
		}

		public override Vector2 GetSize(Vector2 maxSize)
		{
			return maxSize;
		}
	}
}